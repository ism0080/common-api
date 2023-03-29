using Amazon.DynamoDBv2.Model;

namespace Common.AWS.Clients.DynamoDB;

public interface IDynamoDBClient
{
  Task<bool> CreateTable(string tableName, List<AttributeDefinition> attributeDefinitions, List<KeySchemaElement> keySchemaElements, ProvisionedThroughput provisionedThroughput);
  Task<bool> PutItemAsync(Dictionary<string, AttributeValue> item, string tableName);
  Task<Dictionary<string, AttributeValue>> GetItemAsync(Dictionary<string, AttributeValue> key, string tableName);
  Task<bool> UpdateItemAsync(Dictionary<string, AttributeValue> key, Dictionary<string, AttributeValueUpdate> updates, string tableName);
  Task<bool> DeleteItemAsync(Dictionary<string, AttributeValue> key, string tableName);
}