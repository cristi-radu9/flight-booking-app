using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPaw.Entities
{
    [Serializable]
    public class Route
    {
        
       public string route { get; set; }
        public Route()
        {

        }
        public Route(string route)
        {
            this.route = route;
        }
    }
}
