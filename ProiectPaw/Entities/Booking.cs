using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPaw.Entities
{
    [Serializable]
    public class Booking
    {
        public int id { get; set; }
        public string name { get; set; }
        public string fatherName { get; set; }
        private DateTime Birthdate;
        public DateTime birth
        {
            get { return Birthdate; }
            set
            {
                if (value > DateTime.Now.Date)
                    throw new ArgumentException("Invalid age");
                Birthdate = value;
            }         
        }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        
        public Booking()
        {

        }

        public Booking(int id,string name,string fname,DateTime bdate,string email,string phone,string address)
        {
            this.id = id;
            this.name = name;
            fatherName = fname;
            Birthdate = bdate;
            this.email = email;
            this.phone = phone;
            this.address = address;
        }
    }
}
