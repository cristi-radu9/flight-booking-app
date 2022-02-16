using ProiectPaw.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ProiectPaw
{
    public partial class Main : Form
    {
        List<Company> companies = new List<Company>();
        public Main()
        {

            InitializeComponent();
        }

        private void btnBook_Click(object sender, EventArgs e)
        {

            Company c = new Company(cbCompany.Text, new Route(tbRoute.Text), null);

                var editForm = new BookForm(c);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                companies.Add(c);
                DisplayCompanies();
                cbCompany.Text = tbRoute.Text = string.Empty;
            }
        }

        public void DisplayCompanies()
        {
            lvBook.Items.Clear();
            int cnt = 0;

            foreach (var company in companies)
            {
                var listViewItem = new ListViewItem(company.name);
                listViewItem.SubItems.Add(company.route.route);
                listViewItem.SubItems.Add(company.client.id.ToString());
                listViewItem.SubItems.Add(company.client.name);
                listViewItem.SubItems.Add(company.client.fatherName);
                listViewItem.SubItems.Add(company.client.birth.ToShortDateString());
                listViewItem.SubItems.Add(company.client.email);
                listViewItem.SubItems.Add(company.client.phone);
                listViewItem.SubItems.Add(company.client.address);

                //new
                listViewItem.Tag = company;

                lvBook.Items.Add(listViewItem);
                cnt++;
            }
            toolStripStatusLabel1.Text = "Items in list: " + cnt;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvBook.SelectedItems.Count != 1)
                MessageBox.Show("Choose a participant!");
            else
            {
                if (MessageBox.Show(
                    "Are you sure?",
                    "Delete Participant",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    //remove the participant
                    var listViewItem = lvBook.SelectedItems[0];
                    Company c = (Company)listViewItem.Tag;
                    companies.Remove(c);
                    DisplayCompanies();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Route route = new Route("");

            var routeForm = new RouteForm(route);
            if (routeForm.ShowDialog() == DialogResult.OK)
                tbRoute.Text = route.route;
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvBook.SelectedItems.Count != 1)
            {
                MessageBox.Show("Choose a participant!");
            }
            else
            {
                var listViewItem = lvBook.SelectedItems[0];
                Company company = (Company)listViewItem.Tag;

                var editForm = new EditForm(company);
                if (editForm.ShowDialog() == DialogResult.OK)
                    DisplayCompanies();
            }
        }
        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode.ToString() == "B")
            {
                btnBook_Click(sender, e);
            }
        }

       

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Airline reservation. Choose company route then book ");
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font font = new Font("Consolas", 20);
            Brush brush = new SolidBrush(Color.DarkOrange);

            PageSettings pageSettings = printDocument1.DefaultPageSettings;

            var totalH = pageSettings.PaperSize.Height;
            var totalW = pageSettings.PaperSize.Width;

            var marginLeft = pageSettings.Margins.Left;
            var marginTop = pageSettings.Margins.Top;

            var totalWritableW = totalW - 2 * marginLeft;

            var cellW = totalWritableW / 2;
            var cellH = 40;

            Pen pen = new Pen(brush);

            var xPos = 100;
            var yPos = 100;

            graphics.DrawString("List of flights", font, brush, 300, yPos);

            yPos += 100;

            // draw table header
            graphics.DrawRectangle(pen, xPos, yPos, cellW, cellH);
            graphics.DrawRectangle(pen, xPos + cellW, yPos, cellW, cellH);

            //draw header text
            graphics.DrawString("Company Name", font, brush, xPos, yPos);
            graphics.DrawString("Route", font, brush, xPos + cellW, yPos);

            yPos += cellH;

            foreach (Company c in companies)
            {
                //draw table

                graphics.DrawRectangle(pen, xPos, yPos, cellW, cellH);
                graphics.DrawRectangle(pen, xPos + cellW, yPos, cellW, cellH);

                //draw strings
                graphics.DrawString(c.name, font, brush, xPos, yPos);
                graphics.DrawString(c.route.route, font, brush, xPos + cellW, yPos);

                yPos += cellH;
            }
        }

        private void serializeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
                BinaryFormatter formatter = new BinaryFormatter();

                using (FileStream s = File.Create("mainSerialzied.bin"))
                {
                    formatter.Serialize(s, companies);
                }
        }

        private void deserializeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream s = File.OpenRead("mainSerialzied.bin"))
            {
                companies = (List<Company>)formatter.Deserialize(s);
                DisplayCompanies();
            }
        }

        private void writeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Title = "Export";
            saveFile.Filter = "All text files (*.txt)|*.txt";
            saveFile.RestoreDirectory = true;

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(saveFile.FileName);
                
                foreach (Company c in companies)
                {
                    writer.WriteLine(c.name +" "+ c.route.route + " " + c.client.id + " " + c.client.name + " " + c.client.fatherName + " " + c.client.birth + " " + c.client.email + " " + c.client.phone + " " + c.client.address);
                    
                }
                writer.Close();
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvBook.SelectedItems.Count != 1)
            {
                MessageBox.Show("Choose a participant!");
            }
            else
            {
                var listViewItem = lvBook.SelectedItems[0];
                Company company = (Company)listViewItem.Tag;

                var editForm = new EditForm(company);
                if (editForm.ShowDialog() == DialogResult.OK)
                    DisplayCompanies();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            companies.Remove(lvBook.SelectedItems[0].Tag as Company);
            DisplayCompanies();
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            companies.Sort(new Comp());
            DisplayCompanies();
        }

        private void serializeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Company>));
            using(FileStream s = File.Create("XmlOut.xml"))
            {
                serializer.Serialize(s, companies);
            }
        }

        private void deserializeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Company>));
            using(FileStream s = File.OpenRead("XmlOut.xml"))
            {
                companies = (List<Company>)serializer.Deserialize(s);
                DisplayCompanies();
            }
        }

        private void readToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Company> comp = new List<Company>();
            string[] line = File.ReadAllLines("Text.txt");
            for(int i=0;i<line.Length;i++)
            {
                string[] values = line[i].Split(' ');
                comp.Add(new Company(values[0], new Route(values[1]), new Booking(int.Parse(values[2]), values[3], values[4], DateTime.Parse(values[5]), values[6], values[7], values[8])));

            }
            companies = comp;
            DisplayCompanies();
        }
    }
    
}
