using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.ComponentModel.DataAnnotations;
using System.Text;

var connectionFactory = new ConnectionFactory()
{
    HostName = "localhost"
};

using var connection = await connectionFactory.CreateConnectionAsync();

using var channel = await connection.CreateChannelAsync();

await channel
    .QueueDeclareAsync(
    queue: "hello-java",
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (sender, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);


    Console.WriteLine(message);
};

await channel.BasicConsumeAsync(
    queue: "hello-java",
    autoAck: true,
    consumer: consumer);

Console.ReadLine();