using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var Service1 = new  ServiceReference1.SapServiceClient();

           var sesion = Service1.Autenticacion("UserAdmin", "h1gch3S4p");
           //var lst = Service1.Clientes(sesion, "2014-03-31", "2014-03-31");
           //dataGridView1.DataSource = lst;
          // var lst1 = Service1.Productos(sesion, "2014-08-01", "2014-08-30");
          // dataGridView2.DataSource = lst1;
           var lst2 = Service1.Documento(sesion, "2016-02-01", "2016-02-02");
           dataGridView3.DataSource = lst2;
           dataGridView3.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
           var lst3 = Service1.DocumentoDetalle(sesion, "2016-02-01", "2016-02-02");
           dataGridView4.DataSource = lst3;

           MessageBox.Show("sss");
        }
    }
}
