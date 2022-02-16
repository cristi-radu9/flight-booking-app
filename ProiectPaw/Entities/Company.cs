using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPaw.Entities
{
    [Serializable]
    public class Company
    {
        public String name { get; set; }
        public Route route;
        public Booking client;

        public Company()
        {
            
        }

        public Company(String name,Route route,Booking client)
        {
            this.name = name;
            this.route = route;
            this.client = client;
        }
        

        
    }

    public class Comp : IComparer<Company>
    {
        public int Compare(Company x, Company y)
        {
            return string.Compare(x.name, y.name);
        }
    }
}
