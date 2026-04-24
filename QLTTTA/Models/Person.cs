using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTTTA.Models
{
    public abstract class Person
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public Person(string id, string fullName, string email, string phone)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            Phone = phone;
        }

        public abstract string GetInfo();
    }
}
