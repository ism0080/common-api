using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace Common.AWS.Clients.DynamoDB;

public class DynamoDBClient : IDynamoDBClient
{
  private readonly IAmazonDynamoDB _client;

  public DynamoDBClient(IAmazonDynamoDB client)
  {
    _client = client;
  }

  public async Task<bool> CreateTable(string tableName, List<AttributeDefinition> attributeDefinitions, List<KeySchemaElement> keySchemaElements, ProvisionedThroughput provisionedThroughput)
  {
    var response = await _client.CreateTableAsync(new CreateTableRequest
    {
      TableName = tableName,
      AttributeDefinitions = attributeDefinitions,
      KeySchema = keySchemaElements,
      ProvisionedThroughput = provisionedThroughput
    });

    var request = new DescribeTableRequest
    {
      TableName = response.TableDescription.TableName,
    };

    TableStatus status;

    int sleepDuration = 2000;

    do
    {
      Thread.Sleep(sleepDuration);

      var describeTableResponse = await _client.DescribeTableAsync(request);
      status = describeTableResponse.Table.TableStatus;
    }
    while (status != "ACTIVE");

    return status == TableStatus.ACTIVE;
  }

  public async Task<bool> PutItemAsync(Dictionary<string, AttributeValue> item, string tableName)
  {
    var request = new PutItemRequest
    {
      TableName = tableName,
      Item = item,
    };

    var response = await _client.PutItemAsync(request);
    return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
  }

  public async Task<Dictionary<string, AttributeValue>> GetItemAsync(Dictionary<string, AttributeValue> key, string tableName)
  {
    var request = new GetItemRequest
    {
      Key = key,
      TableName = tableName,
    };

    var response = await _client.GetItemAsync(request);
    return response.Item;
  }

  public async Task<bool> UpdateItemAsync(Dictionary<string, AttributeValue> key, Dictionary<string, AttributeValueUpdate> updates, string tableName)
  {
    var request = new UpdateItemRequest
    {
      AttributeUpdates = updates,
      Key = key,
      TableName = tableName,
    };

    var response = await _client.UpdateItemAsync(request);

    return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
  }

  public async Task<bool> DeleteItemAsync(Dictionary<string, AttributeValue> key, string tableName)
  {
    var request = new DeleteItemRequest
    {
      TableName = tableName,
      Key = key,
    };

    var response = await _client.DeleteItemAsync(request);
    return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
  }
}