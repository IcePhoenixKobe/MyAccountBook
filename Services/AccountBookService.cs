using MyAccountBook.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MyAccountBook.Services
{
    public class AccountBookService
    {
        private readonly IMongoCollection<TransactionDetails> _transactions;
        private readonly IMongoCollection<ItemCategory> _itemCategories;

        public AccountBookService(IOptions<DatabaseSetting> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _transactions = mongoDatabase.GetCollection<TransactionDetails>(databaseSettings.Value.CollectionName);
            _itemCategories = mongoDatabase.GetCollection<ItemCategory>(databaseSettings.Value.ItemCategoryCollectionName);
        }

        public async Task<List<TransactionDetails>> GetTransactionsAsync()
        {
            return await _transactions.Find(_ => true).ToListAsync();
        }

        public async Task AddTransactionAsync(TransactionDetails transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            await _transactions.InsertOneAsync(transaction);
        }

        public async Task<List<ItemCategory>> GetItemCategoriesAsync()
        {
            return await _itemCategories.Find(_ => true).ToListAsync();
        }
    }
}