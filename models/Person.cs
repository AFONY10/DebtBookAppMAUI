using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;

namespace TheDebtBook.models
{
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Role { get; set; }

        public string Name { get; set; }

        public Person() { }

        public Person(string creditorDebtor, string name) { Role = creditorDebtor; Name = name; }
    }
}
