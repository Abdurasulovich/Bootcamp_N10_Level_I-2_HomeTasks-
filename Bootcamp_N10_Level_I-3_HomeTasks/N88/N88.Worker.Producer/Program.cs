using ConsoleApp1;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

// create connection factory
var connectionFactory = new ConnectionFactory
{
    HostName = "localhost"
};

// create connection
var connection = await connectionFactory.CreateConnectionAsync();

// create channel
var channel = await connection.CreateChannelAsync();

// create exchange
channel.ExchangeDeclare(
    exchange: MessagingConstants.IdentityExchange,
    type: ExchangeType.Direct,
    durable: true);

// create queue
await channel.QueueDeclareAsync(
    queue: MessagingConstants.IdentityQueue,
    durable: true,
    exclusive: false,
    autoDelete: false);

// bind queue
await channel.QueueBindAsync(
    exchange: MessagingConstants.IdentityExchange,
    queue: MessagingConstants.IdentityQueue,
    routingKey: MessagingConstants.IdentityQueue);

// produce message
var userCreatedEvent = new UserCreatedEvent(Guid.NewGuid());
var message = JsonConvert.SerializeObject(userCreatedEvent);
var body = Encoding.UTF8.GetBytes(message);

// publish message
await channel.BasicPublishAsync(
    exchange: MessagingConstants.IdentityExchange,
    routingKey: MessagingConstants.IdentityQueue,
    body: body);