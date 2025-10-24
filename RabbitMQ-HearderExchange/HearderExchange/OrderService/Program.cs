
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

///Order Servise

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672");

var connection = await factory.CreateConnectionAsync();

var channel = await connection.CreateChannelAsync();

//Header ساخت
var header = new Dictionary<string, object>()
{
    {"subject","order" },
    {"action","create" },
    {"x-match","any" }
};

///ساخت صف 
await channel.QueueDeclareAsync("orderService", false, false, false);

///exchange کردن به یک  bind  
///را به این صورت میدهیم Header
await channel.QueueBindAsync("orderService","orderHeaders","",header!);


var consumer = new AsyncEventingBasicConsumer(channel);

consumer.ReceivedAsync += async (sender, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();

    var subject = Encoding.UTF8.GetString(eventArgs.BasicProperties.Headers["subject"] as byte[]);
    string messages = Encoding.UTF8.GetString(body);

    Console.WriteLine($"Subject:{subject} , Received Message: {messages}");
};

await channel.BasicConsumeAsync(queue: "orderService", autoAck: true, consumer);

Console.ReadLine();