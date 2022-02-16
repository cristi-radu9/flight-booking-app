using ProiectPaw.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProiectPaw
{
    public partial class BookForm : Form
    {
        Company c;
        public BookForm(Company comp)
        {
            this.c = comp;
            InitializeComponent();
        }

        private void BookForm_Load(object sender, EventArgs e)
        {
            tbCompany.Text = c.name;
            tbRoute.Text = c.route.route;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            c.name = tbCompany.Text;
            string route = tbRoute.Text;
            Route r = new Route(route);
            c.route = r;
            
            int id = int.Parse(tbID.Text);
            string name = tbName.Text;
            string fName = tbFatherName.Text;
            DateTime bDate = dtpBirth.Value;
            string email = tbEmail.Text;
            string phone = tbPhone.Text;
            string address = tbAddress.Text;
            Booking form = new Booking(id, name, fName, bDate, email, phone, address);
            c.client = form;


        }

        private void tbName_Validating(object sender, CancelEventArgs e)
        {
            string nume = tbName.Text.Trim();
            if(nume.Length<2)
            {
                e.Cancel = true;
                epName.SetError(tbName, "<2 letters");
            }
        }

        private void tbName_Validated(object sender, EventArgs e)
        {
            epName.Clear();
        }

        private void tbPhone_TextChanged(object sender, EventArgs e)
        {
            string phone = tbPhone.Text;
            if (phone.Length > 10)
                throw new CustomeException();
        }
    }
}
