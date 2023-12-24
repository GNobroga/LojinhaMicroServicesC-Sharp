
using Lojinha.Cart.API.Messages;

namespace Lojinha.Cart.API.RabbitMQSender;
public interface IRabbitMQMessageSender
{
    void Send(CheckoutDTO cart, string queueName);
}