using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory { HostName = "host.docker.internal", UserName = "admin", Password = "admin" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "codap-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

var message = GetMessage(args);
var body = Encoding.UTF8.GetBytes(message);

var properties = channel.CreateBasicProperties();
properties.Persistent = true;

channel.BasicPublish(exchange: "amq.direct", routingKey: "payment", basicProperties: null, body: body);
Console.WriteLine($" [x] Sent {message}");

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();

static string GetMessage(string[] args)
{
  return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
}