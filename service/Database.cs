using SQLite;
using TheDebtBook.models;

namespace TheDebtBook.service
{
    public class TheDebtBookDatabase
    {
        #region Construction
        private readonly SQLiteAsyncConnection _dbConnection;
        public TheDebtBookDatabase()
        {
            var dataDir = FileSystem.AppDataDirectory;
            var dataPath = Path.Combine(dataDir, "DebtBook.db3");
            var dbOptions = new SQLiteConnectionString(dataPath, true);
            _dbConnection = new SQLiteAsyncConnection(dbOptions);
            _ = Initialize();
        }
        private async Task Initialize()
        {
            await _dbConnection.CreateTableAsync<Person>();
            await _dbConnection.CreateTableAsync<Balance>();
        }
        #endregion

        #region PersonOps
        public async Task<List<Person>> GetAllPeople()
        {
            return await _dbConnection.Table<Person>().ToListAsync();
        }
        public async Task<Person> GetPerson(int id)
        {
            return await _dbConnection.Table<Person>().Where(t => t.Id == id).FirstOrDefaultAsync();
        }
        public async Task<int> GetIdOfLastPerson()
        {
            var people = await GetAllPeople();
            if (people.Count > 0) return people.Last().Id;
            else return -1;
        }
        public async Task<int> AddPerson(Person person)
        {
            return await _dbConnection.InsertAsync(person);
        }
        public async Task<int> DeletePerson(Person person)
        {
            return await _dbConnection.DeleteAsync(person);
        }
        public async Task<int> DeletePersonById(int id)
        {
            return await _dbConnection.Table<Person>().Where(t => t.Id == id).DeleteAsync();
        }
        public async Task<int> UpdatePerson(Person person)
        {
            return await _dbConnection.UpdateAsync(person);
        }
        #endregion

        #region BalanceOps
        public async Task<List<Balance>> GetAllBalancesOfPerson(int personId)
        {
            return await _dbConnection.Table<Balance>().Where(t => t.PersonId == personId).ToListAsync();
        }
        public async Task<Balance> GetOneBalanceOfPerson(int id)
        {
            return await _dbConnection.Table<Balance>().Where(t => t.Id == id).FirstOrDefaultAsync();
        }
        public async Task<int> UpdateBalance(Balance balance)
        {
            return await _dbConnection.UpdateAsync(balance);
        }
        public async Task<int> AddBalance(Balance balance)
        {
            return await _dbConnection.InsertAsync(balance);
        }
        public async Task<int> DeleteBalance(Balance balance)
        {
            return await _dbConnection.DeleteAsync(balance);
        }
        public async Task<int> DeleteAllBalancesOfPersonId(int personId)
        {
            return await _dbConnection.Table<Balance>().Where(t => t.PersonId == personId).DeleteAsync();
        }
        #endregion
    }
}
