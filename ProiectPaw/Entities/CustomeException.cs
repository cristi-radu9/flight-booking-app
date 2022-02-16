using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPaw.Entities
{
    public class CustomeException:Exception
    {
        public CustomeException()
        {
            System.Windows.Forms.MessageBox.Show("Too much numbers");
        }
    }
}
