using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory()
{
    HostName = "localhost"
};

using var connection = await factory.CreateConnectionAsync();

using var channel = await connection.CreateChannelAsync();

await channel.QueueDeclareAsync(
    queue: "hello-java",
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null);

var messge = "Hello messaging";
var body = Encoding.UTF8.GetBytes(messge);

await channel
    .BasicPublishAsync(
    exchange: string.Empty,
    routingKey: "hello-java",
    body: body);



