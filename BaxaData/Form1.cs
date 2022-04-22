using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace BaxaData
{
    public partial class Form1 : Form
    {
        private SqlConnection connection = null;


        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\source\repos\BaxaData\BaxaData\Database2.mdf;Integrated Security=True";

            connection = new SqlConnection(connectionString);
            connection.Open();
            if (connection.State == ConnectionState.Open)
            {
                MessageBox.Show("Otkrito podkluchenie");
            }
            SqlDataAdapter adapter = new SqlDataAdapter(
            "SELECT * FROM Viraz",
            connection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            
            dataGridView2.DataSource = dataSet.Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {


            SqlCommand cmd = new SqlCommand(
                $"INSERT INTO[Viraz] (Viraz, Avtor, Tema, Djerelo) VALUES(N'{textBox1.Text}',N'{textBox2.Text}',N'{textBox3.Text}',{textBox4.Text})",
                connection);
            MessageBox.Show(cmd.ExecuteNonQuery().ToString());

        }



       




        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            comboBox1_SelectedIndexChanged(sender, e);



        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Avtor LIKE '%{textBox6.Text}%'";


                    break;
                case 1:

                    (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Tema LIKE '%{textBox6.Text}%'";
                    break;
                case 2:



                    (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Djerelo LIKE '%{textBox6.Text}%'";
                    break;
                case 3:
                    
                   void Rand()
        {
                        Random rnd = new Random();


                        int value = rnd.Next(0, 20);
                        SqlCommand cmd = new SqlCommand($"SELECT COUNT (*) FROM Viraz  WHERE ID = {value}", connection);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            object id = reader.GetValue(0);
                            int y = Convert.ToInt32(id);
                            if (y == 0)
                            {reader.Close();
                                Rand();
                            }
                            else
                            (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"ID = {value}";
                            reader.Close();
                        }
                    }
                    Rand();
                    
                    break;
            }

        }

       


    }
}
