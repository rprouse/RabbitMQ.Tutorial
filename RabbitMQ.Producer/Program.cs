using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

var factory = new ConnectionFactory { HostName = "192.168.2.14" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "hello",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

var message = new Message( Guid.NewGuid(), "Hello World!" );
var json = JsonSerializer.Serialize(message);
var body = Encoding.UTF8.GetBytes(json);

channel.BasicPublish(exchange: string.Empty,
                     routingKey: "hello",
                     basicProperties: null,
                     body: body);

Console.WriteLine($" [x] Sent {message}");
