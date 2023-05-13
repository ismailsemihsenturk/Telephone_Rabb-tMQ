using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Telephone_ISS_ACS.ReportService.BusinessLayer.Managers;
using Telephone_ISS_ACS.UserService.DataAccessLayer.Entities;

namespace ReportConsumerConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { Uri = new Uri("amqp://iss_acs:123456@localhost:5672") };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            
                channel.QueueDeclare(queue: "hello",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                Console.WriteLine("[*] Waiting for messages.");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = JsonSerializer.Deserialize<List<PhoneBookEntry>>(body);
                    Console.WriteLine($" [x] Received {message}");

                    ReportHandlerService handlerService = new ReportHandlerService();
                    handlerService.HandleReport(message);
                };
                channel.BasicConsume(queue: "hello",
                                     autoAck: true,
                                     consumer: consumer);
            

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
            
        }
    }
}
