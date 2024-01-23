using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory { HostName = "host.docker.internal", UserName = "admin", Password = "admin" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "codap-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);

var message = "Hello World! this is a message";
var body = Encoding.UTF8.GetBytes(message);

channel.BasicPublish(exchange: "amq.direct", routingKey: "payment", basicProperties: null, body: body);
Console.WriteLine($" [x] Sent {message}");

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();