using wrssolutions.Data.Repository.Dapper;
using wrssolutions.Domain.Entities.Auth;
using wrssolutions.Domain.MongoEntities.LoggerApplication;
using wrssolutions.DTO.Dto;
using wrssolutions.Services.Interfaces;
using wrssolutions.Services.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Net;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace General.Tests
{
    public class GeneralTests : IDisposable
    {

        private ISvcRabbitMQ _svcRabbitMQ;
        private ITestOutputHelper _testOutputHelper;
        private readonly string conn = @"Server=tcp:DESKTOP-4LRV5C3\\SQLEXPRESS,1433;Database=wrssolutions;User Id=wrssolutions; Password=1!@wrssolutions#$;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true;Pooling=false;";

        // setup
        public GeneralTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;

            var services = new ServiceCollection();
            //AddTransient open new instance and dispose 

            services.AddTransient<ISvcLoggerMongo, SvcLoggerMongo>();
            services.AddTransient<ISvcRabbitMQ, SvcRabbitMQ>(_provider => new SvcRabbitMQ(new Mock<ISvcLoggerMongo>().Object, "localhost"));
            var serviceProvider = services.BuildServiceProvider();

            _svcRabbitMQ = serviceProvider.GetService<ISvcRabbitMQ>()!;
        }

        // teardown
        public void Dispose()
        {
            _testOutputHelper = default!;
            _svcRabbitMQ = default!;
        }

        [Theory(DisplayName = "TestRequestRabbitMQService")]
        [InlineData("messages", 1, "Wermes")]
        [InlineData("messages", 2, "Test Name")]
        public void TestRequestRabbitMQService(string eventName, int Id, string Name)
        {
            _testOutputHelper.WriteLine(string.Format("Create object to send request Rabbit {0}", eventName));

            bool result = _svcRabbitMQ.BasicPublish(eventName, new dtoMessagesRabbitMQ()
            {
                Id = Id,
                Name = Name,
            });

            _testOutputHelper.WriteLine(string.Format("outup value {0}", result));

            Assert.True(result);
        }


        [Fact]
        public void TestDapperMethod()
        {
            if (!string.IsNullOrEmpty(conn))
            {
                //Arrange
                DppRepository fnc = new DppRepository(conn);

                //Act 
                var result = fnc.QueryFirstOrDefault<string>("SELECT 1 VALUE");

                //Assety
                Assert.Equal("1", result);
            }
        }
    }
}
