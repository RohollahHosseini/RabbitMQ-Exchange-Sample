
///Basket Service

using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory();

factory.Uri = new Uri("amqp://guest:guest@localhost:5672");

var connection =await factory.CreateConnectionAsync();

var channel = await connection.CreateChannelAsync();

await channel.QueueDeclareAsync("order.create", false, false, false, null, false);

string message = $"Send shopping cart information to place an order Time {DateTime.Now}";

var body=Encoding.UTF8.GetBytes(message);

 await channel.BasicPublishAsync(exchange:"",routingKey: "order.create",body);   
