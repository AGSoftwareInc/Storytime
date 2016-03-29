using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Contact
    {
        public Contact(string userid, string phonenumber, string firstname, string lastname)
        {
            this.UserId = userid;
            this.PhoneNumber = phonenumber;
            this.FirstName = firstname;
            this.LastName = lastname;
        }

        public Contact() { }

        public string UserId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
