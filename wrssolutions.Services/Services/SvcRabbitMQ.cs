
using wrssolutions.Domain.MongoEntities.LoggerMongo;
using wrssolutions.Services.Interfaces;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace wrssolutions.Services.Services
{
    public class SvcRabbitMQ : ISvcRabbitMQ
    {
        private readonly ISvcLoggerMongo _svcLoggerMongo;
        private readonly ConnectionFactory factory;

        public SvcRabbitMQ(
                ISvcLoggerMongo svcLoggerMongo,
                string hostName)

        {
            _svcLoggerMongo = svcLoggerMongo;
             factory = new ConnectionFactory() { HostName = hostName };
        }

        public bool BasicPublish(string eventName, object model)
        {
            try
            {
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(eventName, false, false, false, null);

                        var message = JsonSerializer.Serialize(model);
                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish("", eventName, null, body);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex?.Message!;
                //lOG
                
                _svcLoggerMongo.Insert(new LoggerMongo()
                {
                    Error = error
                });

                return false;
            }
        }
    }
}
