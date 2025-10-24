using RabbitMQ.Client;
using System.Text;

///Basket Service
var factory = new ConnectionFactory();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
var connection= await factory.CreateConnectionAsync();
var channel=await connection.CreateChannelAsync();

///Topic Exchaneg
///نیاز به تعریف صف نداریم

await channel.ExchangeDeclareAsync(exchange:"orderTopic",ExchangeType.Topic,false);

string message = $"Send shopping cart information to place an order Time {DateTime.Now}";

var body = Encoding.UTF8.GetBytes(message);

///براساس این الگو پیام های ما ارسال میشه 
///order.cancel
await channel.BasicPublishAsync(exchange: "orderTopic", routingKey: "order.cancel",body);

