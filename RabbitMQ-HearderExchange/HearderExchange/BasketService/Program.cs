using RabbitMQ.Client;
using System.ComponentModel.DataAnnotations;
using System.Text;

///Basket Service
var factory = new ConnectionFactory();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
var connection= await factory.CreateConnectionAsync();
var channel=await connection.CreateChannelAsync();


///Header Exchaneg
///نیاز به تعریف صف نداریم

await channel.ExchangeDeclareAsync(exchange:"orderHeaders",ExchangeType.Headers,false);

string message = $"Send shopping cart information to place an order Time {DateTime.Now}";

var body = Encoding.UTF8.GetBytes(message);

//Header ساخت
var header=new Dictionary<string, object>() 
{
    {"subject","order" },
    {"action","create" }
};


///استفاده میکنیم properties به این صورت از 

var properties = new BasicProperties()
{
     Headers=header,
};

  await channel.BasicPublishAsync(exchange: "orderHeaders",routingKey: "",mandatory:false,basicProperties: properties, body);


