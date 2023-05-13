using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telephone_ISS_ACS.UserService.DataAccessLayer.Entities;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Net.Http;

namespace Telephone_ISS_ACS.UserService.DataAccessLayer.Concrete
{
    public class ReportProducer
    {
        public static async Task SendToConsumer(byte[] body)
        {
            try
            {
                var data = "Your report is preparing right now.";
                HttpClient client = new HttpClient();
                Uri uri = new Uri($"https://localhost:44352/Preparing");  
                HttpResponseMessage response = await client.PostAsJsonAsync(uri, data);

                var factory = new ConnectionFactory() { Uri = new Uri("amqp://iss_acs:123456@localhost:5672") };
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.QueueDeclare(queue: "hello",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);

            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}
