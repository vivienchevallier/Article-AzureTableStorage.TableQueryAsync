using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Table.Queryable;

namespace AzureTableStorage.TableQueryAsync
{
    class Program
    {
        static void Main(string[] args)
        {
            ExecuteSimpleTableQuery(CancellationToken.None).Wait();

            Console.ReadLine();
        }

        private static async Task ExecuteSimpleTableQuery(CancellationToken ct)
        {
            var table = await GetTable(ct);

            var query = table.CreateQuery<MyTableEntity>()
                .Where(r => !r.MyProperty.Equals(string.Empty))
                .Take(1200)
                .AsTableQuery();

            var results = await query.ExecuteAsync(ct);

            Console.WriteLine($"Results {(results.Any() ? string.Empty : "not ")}found.");
        }

        private static async Task<CloudTable> GetTable(CancellationToken ct)
        {
            //Retrieve the storage account from the connection string.
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            //Create the table client.
            var tableClient = storageAccount.CreateCloudTableClient();

            //Retrieve a reference to the table.
            var table = tableClient.GetTableReference("mystoragetable");

            //Create the table if it doesn't exist.
            await table.CreateIfNotExistsAsync(ct);

            return table;
        }
    }
}
