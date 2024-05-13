using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado.Net_Demo
{
    internal class Contact
    {
        public string firstName;
        public string lastName;
        public string email;
        public string phone;
        public string address;
        public string city;
        public string state;
        public int zip;

        public void AcceptRecord()
        {
            Console.WriteLine("Enter first name:");
            firstName = Console.ReadLine();
            Console.WriteLine("Enter last name:");
            lastName = Console.ReadLine();
            Console.WriteLine("Enter email:");
            email = Console.ReadLine();
            Console.WriteLine("Enter phone number:");
            phone = Console.ReadLine();
            Console.WriteLine("Enter address:");
            address = Console.ReadLine();
            Console.WriteLine("Enter city:");
            city = Console.ReadLine();
            Console.WriteLine("Enter state:");
            state = Console.ReadLine();
            Console.WriteLine("Enter zip:");
            zip = Convert.ToInt32(Console.ReadLine());
        }
    }
}
