using TheDebtBook.models;

namespace TheDebtBook.data
{
    public struct AllPerson
    {
        public Person personInfo { get; set; }
        public int totalBalance { get; set; }
        public AllPerson(){}

        public AllPerson(Person PersonInfo, int TotalBalance)
        {
            personInfo = PersonInfo;
            totalBalance = TotalBalance;
        }
        
        
    }
}