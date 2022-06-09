using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace awsDynamoDbPoC.Tests.Setup;

public class TestDataSetup
{
    private static readonly IAmazonDynamoDB dynamoDbClient = new AmazonDynamoDBClient(new AmazonDynamoDBConfig
    {
        ServiceURL = "http://localhost:8000"
    });

    public async Task CreateTable()
    {
        var tableName = "address-service-lookup";
        var request  = new CreateTableRequest
        {
            AttributeDefinitions = new List<AttributeDefinition>()
            {
                new AttributeDefinition
                {
                    AttributeName = "PostalCode",
                    AttributeType = "N"
                },
                new AttributeDefinition()
                {
                    AttributeName = "CountryIso3",
                    AttributeType = "S"
                }
            },
            KeySchema =  new List<KeySchemaElement>()
            {
                new KeySchemaElement
                {
                    AttributeName = "PostalCode",
                    KeyType = "HASH"
                },
                new KeySchemaElement
                {
                    AttributeName = "CountryIso3",
                    KeyType = "RANGE"
                }
            },
            BillingMode = BillingMode.PAY_PER_REQUEST,
            TableName = tableName
        };
        await dynamoDbClient.CreateTableAsync(request);
        await WaitUntilTableActive(tableName);
    }

    private static async Task WaitUntilTableActive(string tableName)
    {
        var status = string.Empty;
        do
        {
            Thread.Sleep(5000);
            try
            {
                status = await GetTableStatus(tableName);
            }
            catch
            {
                Console.WriteLine("Table is not ready ");
            }
        } while (!status.Equals("ACTIVE"));
    }

    private static async Task<string> GetTableStatus(string tableName)
    {
        var response = await dynamoDbClient.DescribeTableAsync(new DescribeTableRequest
        {
            TableName = tableName
        });

        return response.Table.TableStatus;
    }
}