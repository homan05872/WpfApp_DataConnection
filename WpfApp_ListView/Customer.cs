using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace WpfApp_ListView
{
    public class Customer
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }

        public Customer()
        { 

        }

        public Customer(string name)
        {
            Name = name;
        }

        public Customer(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public override string ToString()
        {
            return $"{Id} - {Name}";
        }
    }
}
