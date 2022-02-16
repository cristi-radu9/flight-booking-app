using ProiectPaw.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProiectPaw
{
    public partial class RouteForm : Form
    {
        List<Route> routes = new List<Route>();
        Route r;
        public RouteForm(Route r)
        {
            this.r = r;

            InitializeComponent();
        }

        
   
        private void DisplayRoutes()
        {
            lvRoute.Items.Clear();

            foreach (var route in routes)
            {
                var listViewItem = new ListViewItem(route.route);
                

                //new
                listViewItem.Tag = route;

                lvRoute.Items.Add(listViewItem);
            }
        }

        

        private void RouteForm_Load(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream s = File.OpenRead("serialzied.bin"))
            {
                routes = (List<Route>)formatter.Deserialize(s);
                DisplayRoutes();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var listViewItem = lvRoute.SelectedItems[0];
            Route route = (Route)listViewItem.Tag;
            r.route = route.route;

        }
    }
}
