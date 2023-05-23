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
    public partial class Provider : Form
    {
        public Provider()
        {
            InitializeComponent();
        }
        //closebtn
        private void closebtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены что хотите выйти на главную страницу?","Warning", MessageBoxButtons.YesNo,
        MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                this.Hide();
                glavnaya glavnaya = new glavnaya();
                glavnaya.Show();
            }
        }

        private void closebtn_MouseEnter(object sender, EventArgs e)
        {
            closebtn.ForeColor = Color.Red;
        }

        private void closebtn_MouseLeave(object sender, EventArgs e)
        {
            closebtn.ForeColor = Color.White;
        }
        // 
        // loaddata
        private void loadprov()
        {
            DB db = new DB();
            string query = "";
            query = "SELECT p_id AS id, p_phone AS номер,p_adres AS адрес, p_name AS имя,p_surname as фамилия FROM provider";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, db.getConnection());
            db.openConnection();
            DataTable datatable = new DataTable();
            datatable.Clear();
            adapter.Fill(datatable);
            dataGridView1.DataSource = datatable;
            db.closeConnection();
        }

        private void Provider_Load(object sender, EventArgs e)
        {
            loadprov();
        }
        //
        //ins btn
        private void insbutton_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "") 
            {
                string pphone = textBox2.Text;
                string padres = textBox3.Text;
                string pname = textBox4.Text;
                string psurname = textBox5.Text;
                DB db = new DB();
                DataTable table = new DataTable();

                MySqlDataAdapter adapter = new MySqlDataAdapter();

                MySqlCommand command = new MySqlCommand("INSERT INTO provider(p_phone,p_adres,p_name,p_surname) VALUES(@ph,@pa,@pn,@ps)", db.getConnection());
                command.Parameters.Add("@ph", MySqlDbType.VarChar).Value = pphone;
                command.Parameters.Add("@pa", MySqlDbType.VarChar).Value = padres;
                command.Parameters.Add("@pn", MySqlDbType.VarChar).Value = pname;
                command.Parameters.Add("@ps", MySqlDbType.VarChar).Value = psurname;
                adapter.SelectCommand = command;
                adapter.Fill(table);
                loadprov();
                textBox2.Text = ""; textBox3.Text = ""; textBox4.Text = ""; textBox5.Text = "";
                MessageBox.Show("Строка добавлена!");
            }
            else
            {
                MessageBox.Show("Проверьте верно ли указаны данные");
                textBox2.Text = ""; textBox3.Text = ""; textBox4.Text = ""; textBox5.Text = "";
            }
        }
        //
        //delbtn
        private void deletebtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены что вы хотите удалить строку?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                DB db = new DB();
                db.openConnection();
                MySqlCommand command = new MySqlCommand("DELETE FROM provider where p_id = @id", db.getConnection());

                command.Parameters.Add("@id", MySqlDbType.VarChar).Value = dataGridView1.CurrentRow.Cells["id"].Value.ToString();//сравнение айди со строкой
                command.ExecuteNonQuery();
                loadprov();
                db.closeConnection();
                MessageBox.Show("Строка удалена !");
            }
        }
        //
        
    }
}
