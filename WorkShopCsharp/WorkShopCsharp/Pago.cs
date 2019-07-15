using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WorkShopCsharp
{
    public partial class Pago : Form
    {
        public Pago()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Hide(); 
        }

        private void Pago_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'workShopDataSet2.empleado' Puede moverla o quitarla según sea necesario.
            this.empleadoTableAdapter.Fill(this.workShopDataSet2.empleado);
            // TODO: esta línea de código carga datos en la tabla 'workShopDataSet1.pago' Puede moverla o quitarla según sea necesario.
            this.pagoTableAdapter.Fill(this.workShopDataSet1.pago);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Double ihss = 0;
            Double fosovi = 0;
            Double neto_pagar = 0;
            Double total_deduciones = 0;
            string empelado = comboBox1.Text;
            Double pago = double.Parse(textBox1.Text);

            if (pago > 7000)
            {
                ihss = pago * 0.035;
            }
            else
            {
                ihss = 7000 * 0.035;
            }

            fosovi = pago * 0.025;
            total_deduciones = fosovi + ihss;
            neto_pagar = pago - total_deduciones;

            string query = "insert into pago(pago,ihss,fosovi,net_pagar,total_deducciones,empleado) values (@pago,@ihss,@fosovi,@neto,@total,@empleado)";
            SqlCommand cmd = new SqlCommand(query,Conexion.conectar());
            cmd.Parameters.AddWithValue("@pago",pago);
            cmd.Parameters.AddWithValue("@ihss", ihss);
            cmd.Parameters.AddWithValue("@fosovi", fosovi);
            cmd.Parameters.AddWithValue("@neto", neto_pagar);
            cmd.Parameters.AddWithValue("@total", total_deduciones);
            cmd.Parameters.AddWithValue("@empleado", empelado);

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Pago Realizado!!");
                updateDataGridView("Select * from pago","pago");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        public void updateDataGridView(String query, String tabla)
        {
            SqlDataAdapter ada = new SqlDataAdapter(query, Conexion.conectar());
            DataSet ds = new DataSet();

            ada.Fill(ds, tabla);
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = tabla;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String query = "Select * from pago where empleado=" + comboBox1.Text;
            updateDataGridView(query, "pago");
        }
    }
}
