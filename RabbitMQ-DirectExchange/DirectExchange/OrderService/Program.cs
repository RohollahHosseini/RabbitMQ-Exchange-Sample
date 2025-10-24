using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

///Order Servise

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672");

var connection =await factory.CreateConnectionAsync();

var channel = await connection.CreateChannelAsync();

var consumer=new AsyncEventingBasicConsumer(channel);

consumer.ReceivedAsync += async (sender, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();

    string messages = Encoding.UTF8.GetString(body);

    Console.WriteLine($"Received Message: {messages}");
};

await channel.BasicConsumeAsync(queue: "order.create",autoAck:true,consumer);

Console.ReadLine();


