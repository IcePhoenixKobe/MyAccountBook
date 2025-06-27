using MyAccountBook.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MyAccountBook.Services
{
    public class AccountBookService
    {
        private readonly IMongoCollection<TransactionDetails> _transactions;

        public AccountBookService(IOptions<DatabaseSetting> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _transactions = mongoDatabase.GetCollection<TransactionDetails>(databaseSettings.Value.CollectionName);
        }

        public async Task<List<TransactionDetails>> GetTransactionsAsync()
        {
            return await _transactions.Find(_ => true).ToListAsync();
        }
    }
}