using RabbitMQ.Client;
using System.Text;

///Basket Service
var factory = new ConnectionFactory();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
var connection= await factory.CreateConnectionAsync();
var channel=await connection.CreateChannelAsync();

///Fanout Exchaneg
///نیاز به تعریف صف نداریم

await channel.ExchangeDeclareAsync(exchange:"order",ExchangeType.Fanout,false);

string message = $"Send shopping cart information to place an order Time {DateTime.Now}";

var body = Encoding.UTF8.GetBytes(message);

await channel.BasicPublishAsync(exchange:"order", routingKey: "",body);

