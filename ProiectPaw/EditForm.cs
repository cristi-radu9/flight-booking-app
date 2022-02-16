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
    public partial class EditForm : Form
    {
        Company company;
        public EditForm(Company c)
        {
            company = c;
            InitializeComponent();
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            tbCompany.Text = company.name;
            tbRoute.Text = company.route.route;
            tbID.Text = company.client.id.ToString();
            tbName.Text = company.client.name;
            tbFatherName.Text = company.client.fatherName;
            dtpBirth.Value = company.client.birth;
            tbEmail.Text = company.client.email;
            tbPhone.Text = company.client.phone;
            tbAddress.Text = company.client.address;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            company.name=tbCompany.Text;
            company.route.route=tbRoute.Text;
            company.client.id=int.Parse(tbID.Text);
            company.client.name=tbName.Text;
            company.client.fatherName=tbFatherName.Text;
            company.client.birth=dtpBirth.Value;
            company.client.email=tbEmail.Text;
            company.client.phone=tbPhone.Text;
            company.client.address=tbAddress.Text;
        }

        private void tbPhone_TextChanged(object sender, EventArgs e)
        {
            string phone = tbPhone.Text;
            if (phone.Length > 10)
                throw new CustomeException();
        }
    }
}
