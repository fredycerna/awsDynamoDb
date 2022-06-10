## PoC  using NetCore Api + AWS DynamoDb + Integration using AWS DynamoDb docker image

This is a PoC created with the purpose of explore the viability to use AWS DynamoDb in a .NetCore 6 Microservice
the features incorporated are:


### Basic .Netcore 6 microservice structure using a solution with 3 projects.
* awsDynamoDbPoc.Core (including repository, services and extensions methods)
* awsDynamoDbPoc.Web (incorporating AWS DynamoDb client configuration, basic API, injection of services)
* awsDynamoDbPoc.Test (A basic integration test )


### AWS Credentials Configuration
The credentials and AWS region could be set in the Program.cs in the awsDynamoDbPoC.Web otherwise the configuration will be took form the default AWS CLI default configuration  
```c#
  builder.Services.AddDefaultAWSOptions(new AWSOptions
    {
        Region = Amazon.RegionEndpoint.USWest2,
        //Credentials = new BasicAWSCredentials("IAM User AccessKey", "IAM User Secret")
    });
```

## Integration Tests
The integration test created is using a in memory AWS DynamoDB using a docker image [amazon/dynamodb-local](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DynamoDBLocal.DownloadingAndRunning.html)  with [Docker.DotNet](https://github.com/dotnet/Docker.DotNet). 

```c#
    private readonly Uri _defaultWindowsDockerEngineUri = new Uri("unix:///var/run/docker.sock");
    private readonly Uri _defaultLinuxDockerEngineUri = new Uri("unix:///var/run/docker.sock");
```
## License
[MIT](https://choosealicense.com/licenses/mit/)