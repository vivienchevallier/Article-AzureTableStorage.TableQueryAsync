using Microsoft.WindowsAzure.Storage.Table;

namespace AzureTableStorage.TableQueryAsync
{
    public class MyTableEntity : TableEntity
    {
        public MyTableEntity() { }

        public MyTableEntity(string partitionKey, string rowKey)
        {
            this.PartitionKey = partitionKey;
            this.RowKey = rowKey;
        }

        public string MyProperty { get; set; }
    }
}
