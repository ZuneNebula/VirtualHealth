using AuthenticationService.Models;
using AuthenticationService.Services;
using Confluent.Kafka;
using System.Diagnostics;
using System.Text.Json;

namespace AuthenticationService.Consumer
{
    public class ApacheKafkaConsumerService: IHostedService
    {
        private readonly string topic = "test";
        private readonly string groupId = "test_group";
        private readonly string bootstrapServers = "localhost:9092";
        private readonly IServiceProvider _serviceProvider;
        public ApacheKafkaConsumerService(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = groupId,
                BootstrapServers = bootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            try
            {
                using (var consumerBuilder = new ConsumerBuilder
                <Ignore, string>(config).Build())
                {
                    consumerBuilder.Subscribe(topic);
                    var cancelToken = new CancellationTokenSource();

                    try
                    {
                        while (true)
                        {
                            var consumer = consumerBuilder.Consume(cancelToken.Token);
                            var userRequest = JsonSerializer.Deserialize<User> (consumer.Message.Value);
                            Console.WriteLine(userRequest);
                            using (var scope = _serviceProvider.CreateScope())
                            {
                                var service = scope.ServiceProvider.GetRequiredService<IUserService>();
                                service.CreateAsync(userRequest).Wait(); 
                            }
                           
                            Debug.WriteLine($"Processing Email Id:{ userRequest.Email}");
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        consumerBuilder.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
