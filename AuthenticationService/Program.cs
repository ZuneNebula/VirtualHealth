using AuthenticationService.Config;
using AuthenticationService.Consumer;
using AuthenticationService.Models;
using AuthenticationService.Services;
using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Silverback.Messaging.Configuration;

var builder = WebApplication.CreateBuilder(args);

//Add database context
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add services to the container.
builder.Services
            .AddSilverback()
            .WithConnectionToMessageBroker(options => options.AddKafka())
            .AddKafkaEndpoints(
                endpoints => endpoints
                    .Configure(
                        config =>
                        {
                            config.BootstrapServers = "localhost:9092";
                        })
                .AddInbound(
                    endpoint => endpoint
                        .ConsumeFrom("test2")
                        .DeserializeJson(serializer => serializer.UseFixedType<User>())
                        .Configure(
                            config =>
                            {
                                config.GroupId = "test_group";
                                config.AutoOffsetReset = AutoOffsetReset.Earliest;
                            })))

            .AddSingletonSubscriber<KafkaConsumer>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
