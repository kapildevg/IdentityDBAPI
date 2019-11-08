using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;

namespace IdentityAPI
{
    public class StorageAccountHelper
    {
        private string storageConnectionString;
        private CloudStorageAccount storageAccount;
        private CloudQueueClient qClient;
        private CloudTableClient tableClient;
        public string StorageConnectionString
        {
            get { return this.storageConnectionString; }
            set
            {
                this.storageConnectionString = value;
                storageAccount = CloudStorageAccount.Parse(this.storageConnectionString);

            }
        }


        public async Task SendMessageAsync(string message, string queueName)
        {
            qClient = storageAccount.CreateCloudQueueClient();
            var queue = qClient.GetQueueReference(queueName);
            await queue.CreateIfNotExistsAsync();
            CloudQueueMessage queueMessage = new CloudQueueMessage(message);
            await queue.AddMessageAsync(queueMessage);
        }
        
    }
}
