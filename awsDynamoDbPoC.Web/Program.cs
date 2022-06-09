using Amazon.DynamoDBv2;
using Amazon.Extensions.NETCore.Setup;
using awsDynamoDbPoC.Core.Repositories;
using awsDynamoDbPoC.Core.Services;

namespace awsDynamoDbPoC.Web;

public class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAWSService<IAmazonDynamoDB>();
        builder.Services.AddDefaultAWSOptions(new AWSOptions
        {
            Region = Amazon.RegionEndpoint.USWest2,
            //Credentials = new BasicAWSCredentials("IAM User AccessKey", "IAM User Secret")
        });
        builder.Services.AddScoped<IAddressRepository, AddressRepository>();
        builder.Services.AddScoped<IAddressService, AddressService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}