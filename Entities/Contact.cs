using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Contact
    {
        public Contact(string userid, string phonenumber, bool usesapp)
        {
            this.UserId = userid;
            this.PhoneNumber = phonenumber;
            this.UsesApp = usesapp;
        }

        public Contact() { }

        public string UserId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        [PetaPoco.Ignore]
        public bool UsesApp { get; set; }
    }
}
