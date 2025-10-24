
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

///Order Servise

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672");

var connection = await factory.CreateConnectionAsync();

var channel = await connection.CreateChannelAsync();

///ساخت صف 
await channel.QueueDeclareAsync("orderService", false, false, false);

///exchange کردن به یک  bind  
///الگوی دریافت پیام این صف به این صورت است  order.*

await channel.QueueBindAsync("orderService", "orderTopic","order.*");


var consumer = new AsyncEventingBasicConsumer(channel);

consumer.ReceivedAsync += async (sender, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();

    string messages = Encoding.UTF8.GetString(body);

    Console.WriteLine($"Received Message: {messages}");
};

await channel.BasicConsumeAsync(queue: "orderService", autoAck: true, consumer);

Console.ReadLine();