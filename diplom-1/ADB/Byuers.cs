using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADB
{
    public partial class Byuers : Form
    {
        public Byuers()
        {
            InitializeComponent();
        }
        // exit
        private void exitButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
       "Вы уверены что хотите закрыть окно?",
       "Warning",
       MessageBoxButtons.YesNo,
       MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                this.Close();
                glavnaya glavnaya = new glavnaya();
                glavnaya.Show();
            }
        }

        private void exitButton_MouseEnter(object sender, EventArgs e)
        {
            exitButton.ForeColor = Color.Red;
        }

        private void exitButton_MouseLeave(object sender, EventArgs e)
        {
            exitButton.ForeColor = Color.White;
        }

        private void loadbuy()
        {
            DB db = new DB();
            string query = "";
            query = "SELECT b_id AS ID, b_fio AS ФИО, b_pasp AS Паспорт, b_phone AS Номер, b_cid AS ID_машины FROM buyers";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, db.getConnection());
            db.openConnection();
            DataTable datatable = new DataTable();
            datatable.Clear();
            adapter.Fill(datatable);
            updavto();
            dataGridView1.DataSource = datatable;
            db.closeConnection();
            
        }
        private void Byuers_Load(object sender, EventArgs e)
        {
            loadbuy();
        }
        //insbtn
        private void insbutton_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && comboBox1.Text != "")
            {
                string bfio = textBox1.Text;
                string bpasp = textBox2.Text;
                string bphone = textBox3.Text;
                string bcid = comboBox1.Text;

                DB db = new DB();
                DataTable table = new DataTable();

                MySqlDataAdapter adapter = new MySqlDataAdapter();

                MySqlCommand command = new MySqlCommand("INSERT INTO buyers (b_fio,b_pasp,b_phone,b_cid) VALUES (@bf,@bpa,@bph,@bc)", db.getConnection());
                command.Parameters.Add("@bf", MySqlDbType.VarChar).Value = bfio;
                command.Parameters.Add("@bpa", MySqlDbType.VarChar).Value = bpasp;
                command.Parameters.Add("@bph", MySqlDbType.VarChar).Value = bphone;
                command.Parameters.Add("@bc", MySqlDbType.VarChar).Value = bcid;

                adapter.SelectCommand = command;
                adapter.Fill(table);
                loadbuy();
                updavto();
                textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = ""; comboBox1.Text = "";
                MessageBox.Show("Строка добавлена!");
            }
            else
            {
                MessageBox.Show("Проверьте верно ли указаны данные");
                textBox1.Text = ""; textBox2.Text = ""; comboBox1.Text = "";
            }
        }//
        //delbtn
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены что вы хотите удалить строку?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                DB db = new DB();
                db.openConnection();
                MySqlCommand command = new MySqlCommand("DELETE FROM buyers where b_id = @id", db.getConnection());

                command.Parameters.Add("@id", MySqlDbType.VarChar).Value = dataGridView1.CurrentRow.Cells["id"].Value.ToString();//сравнение айди со строкой
                command.ExecuteNonQuery();
                loadbuy();
                db.closeConnection();
                MessageBox.Show("Строка удалена !");
            }
        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT c_id FROM cars WHERE c_availability = 'есть' ", db.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            comboBox1.Items.Clear();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                comboBox1.Items.Add(table.Rows[i]["c_id"]);
            }
        }
        private void updavto()
        {
            DB db = new DB();

            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("UPDATE cars,buyers set c_availability = 'нет' WHERE c_id = b_cid", db.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
        }
    }
}
