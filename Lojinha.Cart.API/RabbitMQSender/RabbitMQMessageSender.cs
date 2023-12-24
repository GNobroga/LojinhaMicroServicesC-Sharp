using System.Text;
using System.Text.Json;
using Lojinha.Cart.API.Dtos;
using Lojinha.Cart.API.Messages;
using RabbitMQ.Client;

namespace Lojinha.Cart.API.RabbitMQSender;

public class RabbitMQMessageSender : IRabbitMQMessageSender
{

    private IConnection _connection;

    public RabbitMQMessageSender()
    {
        _connection = new ConnectionFactory { HostName = "localhost" }.CreateConnection();
    }

    public void Send(CheckoutDTO message, string queueName)
    {

        using var channel = _connection.CreateModel();

        // channel.QueueDeclare(
        //     queue: queueName,
        //     durable: false,
        //     autoDelete: false,
        //     arguments: null
        // );

        channel.BasicPublish(
            exchange: string.Empty,
            routingKey: queueName,
            body: Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message, new JsonSerializerOptions {
                WriteIndented = true
            }))
        );
    }
}