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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Pago().Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'workShopDataSet3.empleado' Puede moverla o quitarla según sea necesario.
            this.empleadoTableAdapter1.Fill(this.workShopDataSet3.empleado);
            // TODO: esta línea de código carga datos en la tabla 'workShopDataSet.empleado' Puede moverla o quitarla según sea necesario.
            this.empleadoTableAdapter.Fill(this.workShopDataSet.empleado);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "insert into empleado(nombre,apellido,direccion,cargo) values (@nombre,@apellido,@direccion,@cargo)";
            SqlCommand cmd = new SqlCommand(query, Conexion.conectar());
            cmd.Parameters.AddWithValue("@nombre", textBox1.Text);
            cmd.Parameters.AddWithValue("@apellido", textBox2.Text);
            cmd.Parameters.AddWithValue("@direccion", textBox3.Text);
            cmd.Parameters.AddWithValue("@cargo", textBox4.Text);

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Se guardo el empleado!!");
                updateDataGridView("select * from empleado", "empleado");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string query = "delete from empleado where id_empleado=@id";
            SqlCommand cmd = new SqlCommand(query,Conexion.conectar());
            cmd.Parameters.AddWithValue("@id", comboBox1.Text);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Se Elimino el empleado!!");
                updateDataGridView("select * from empleado", "empleado");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String query = "update empleado set nombre=@nombre , apellido=@apellido , direccion=@direccion , cargo=@cargo where id_empleado=@id";
            SqlCommand cmd = new SqlCommand(query, Conexion.conectar());
            cmd.Parameters.AddWithValue("@nombre", textBox1.Text);
            cmd.Parameters.AddWithValue("@apellido", textBox2.Text);
            cmd.Parameters.AddWithValue("@direccion", textBox3.Text);
            cmd.Parameters.AddWithValue("@cargo", textBox4.Text);

            cmd.Parameters.AddWithValue("@id", comboBox1.Text);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Se Actualizo el empleado!!");
                updateDataGridView("select * from empleado", "empleado");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void updateDataGridView(String query, String tabla)
        {
            SqlDataAdapter ada=new SqlDataAdapter(query,Conexion.conectar());
            DataSet ds = new DataSet();

            ada.Fill(ds, tabla);
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = tabla;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string query ="Select * from empleado where nombre like '%" + textBox5.Text + "%'";
            updateDataGridView(query,"empleado");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }
    }
}
