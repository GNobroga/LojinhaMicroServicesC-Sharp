using System.Text;
using System.Text.Json;
using Lojinha.Order.API.Entities;
using Lojinha.Order.API.Messages;
using Lojinha.Order.API.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Lojinha.Order.API.RabbitMQ;

public class RabbitMQCheckoutConsumer : BackgroundService
{

    private readonly OrderRepository _orderRepository;

    private IConnection _connection;

    private IModel _channel;

    public RabbitMQCheckoutConsumer(OrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
       
       _connection = new ConnectionFactory { HostName = "localhost" }.CreateConnection();
       _channel = _connection.CreateModel();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {

        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (channel, evt) => 
        {
            var dto = JsonSerializer.Deserialize<CheckoutDTO>(Encoding.UTF8.GetString(evt.Body.Span));
            ProcessOrder(dto!).Wait();
            _channel.BasicAck(evt.DeliveryTag, false); // Confirma que foi processado e a mensagem pode ser removida da fila
        };

        _channel.QueueDeclare(queue: "checkout_queue");

        _channel.BasicConsume(
            queue: "checkout_queue",
            consumer: consumer,
            autoAck: false
        );

        return Task.CompletedTask;
    }

    public async Task ProcessOrder(CheckoutDTO dto)
    {
        OrderEntity order = new()
        {
            UserId = dto.UserId,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            OrderDetails = new List<OrderDetail>(),
            CardNumber = dto.CardNumber,
            CVV = dto.CVV,
            Total = dto.Total,
            Email = dto.Email,
            ExpiryMothYear = dto.ExpiryMothYear,
            Status = false,
            Phone = dto.Phone,
            DateTime = dto.DateTime
        };

        dto.CartDetails.ToList().ForEach(detail => {
            
            var orderDetail = new OrderDetail
            {
                ItemId = detail.ItemId,
                ProductName = detail.Item.Name,
                Price = detail.Item.Price,
                Quantity = detail.Quantity,
                CouponCode = detail.CouponCode
            };

            order.OrderDetails.Add(orderDetail);
        });

        await _orderRepository.AddAsync(order);
    }
}