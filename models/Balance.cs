using SQLite;

namespace TheDebtBook.models
{
    public class Balance
    {
        [PrimaryKeyAttribute, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int PersonId { get; set; }
        public int Amount { get; set; }
        public Balance(){}

        public Balance(int personId, int amount)
        {
            PersonId = personId; 
            Amount = amount;
        }
    }
}
