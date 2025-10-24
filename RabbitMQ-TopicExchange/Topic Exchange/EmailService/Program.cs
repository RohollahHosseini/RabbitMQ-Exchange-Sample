
using RabbitMQ.Client.Events;


using RabbitMQ.Client;
using System.Text;

///Email Servise

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672");

var connection = await factory.CreateConnectionAsync();

var channel = await connection.CreateChannelAsync();

///ساخت صف 
await channel.QueueDeclareAsync("EmailService", false, false, false);

///exchange کردن به یک  bind  
///الگو باید به این صورت باشد تا این صف پیام را دریافت کند  order.cancel
await channel.QueueBindAsync("EmailService", "orderTopic", "order.cancel");

var consumer = new AsyncEventingBasicConsumer(channel);

consumer.ReceivedAsync += async (sender, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();

    string messages = Encoding.UTF8.GetString(body);

    Console.WriteLine($"Received Message: {messages}");
};

await channel.BasicConsumeAsync(queue: "EmailService", autoAck: true, consumer);

Console.ReadLine();