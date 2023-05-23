using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;

namespace ADB
{
    public partial class avto : Form
    {
        /* SELECT * FROM cars*/
        public avto()
        {
            InitializeComponent();
                     
        }
        private void loaddata()
        {
            DB db = new DB();
            string query = "";
            query = "SELECT c_id as id , c_model as модель, c_color as цвет,c_type as тип_кузова, c_power as лошадиные_силы, c_complectation as комплектация,c_availability as наличие,c_price AS цена, pr_id as id_поставщика FROM cars";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, db.getConnection());
            db.openConnection();
            DataTable datatable = new DataTable();
            datatable.Clear();
            adapter.Fill(datatable);
            dataGridView1.DataSource = datatable;
            db.closeConnection();
        }
            
        private void avto_Load(object sender, EventArgs e)
        {
            loaddata();
        }

        /**/
        /* закрытие окна */
        private void label1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
        "Вы уверены что хотите закрыть окно ?",
        "Warning",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                this.Close();

            }
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Red;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.ForeColor = Color.White;
        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            DB db = new DB();
            
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT p_id FROM provider", db.getConnection());           
            adapter.SelectCommand= command;
            adapter.Fill(table);
            comboBox1.Items.Clear();
            for (int i =0;i<table.Rows.Count;i++)
            {
                comboBox1.Items.Add(table.Rows[i]["p_id"]);
            }
            
        }
        //
        //кнопка добавления
        private void insbutton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && comboBox4.Text != "" && textBox4.Text != "" && comboBox3.Text != "" && comboBox2.Text != "" && textBox7.Text != "" && comboBox1.Text != "")
            {
                string cmodel = textBox1.Text;
                string ccolor = textBox2.Text;
                string ctype = comboBox4.Text;
                string cpower = textBox4.Text;
                string ccomplectation = comboBox3.Text;
                string cavailability = comboBox2.Text;
                string cprice = textBox7.Text;
                string cpr = comboBox1.Text;
                DB db = new DB();

                DataTable table = new DataTable();

                MySqlDataAdapter adapter = new MySqlDataAdapter();

                MySqlCommand command = new MySqlCommand("INSERT INTO cars (c_model,c_color,c_type,c_power,c_complectation,c_availability,c_price,pr_id) VALUES (@mod,@col,@typ,@pow,@com,@avai,@pri,@pr)", db.getConnection());
                command.Parameters.Add("@mod", MySqlDbType.VarChar).Value = cmodel;
                command.Parameters.Add("@col", MySqlDbType.VarChar).Value = ccolor;
                command.Parameters.Add("@typ", MySqlDbType.VarChar).Value = ctype;
                command.Parameters.Add("@pow", MySqlDbType.VarChar).Value = cpower;
                command.Parameters.Add("@com", MySqlDbType.VarChar).Value = ccomplectation;
                command.Parameters.Add("@avai", MySqlDbType.VarChar).Value = cavailability;
                command.Parameters.Add("@pri", MySqlDbType.VarChar).Value = cprice;
                command.Parameters.Add("@pr", MySqlDbType.VarChar).Value = cpr;
                adapter.SelectCommand = command;
                adapter.Fill(table);
                loaddata();
                textBox1.Text = ""; textBox2.Text = ""; comboBox4.Text = ""; textBox4.Text = ""; comboBox3.Text = ""; comboBox2.Text = ""; textBox7.Text = "";comboBox1.Text = "";
                MessageBox.Show("Строка добавлена!");
            }
            else 
            { 
                MessageBox.Show("Проверьте верно ли все указано!");
                textBox1.Text = ""; textBox2.Text = ""; comboBox4.Text = ""; textBox4.Text = ""; comboBox3.Text = ""; comboBox2.Text = ""; textBox7.Text = ""; comboBox1.Text = "";
            }
        }
        //
        //кнопка удаления
        private void delbtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены что вы хотите удалить строку?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                DB db = new DB();
                db.openConnection();
                MySqlCommand command = new MySqlCommand("DELETE FROM cars where c_id = @id", db.getConnection());

                command.Parameters.Add("@id", MySqlDbType.VarChar).Value = dataGridView1.CurrentRow.Cells["id"].Value.ToString();//сравнение айди со строкой
                command.ExecuteNonQuery();
                loaddata();
                db.closeConnection();
                MessageBox.Show("Строка удалена !");
            }
        }
        //search
        private void button1_Click(object sender, EventArgs e)
        {
            string search = textBox3.Text;
            DB db = new DB();
            DataTable datatable = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT c_id as id , c_model as модель, c_color as цвет,c_type as тип_кузова, c_power as лошадиные_силы, c_complectation as комплектация,c_availability as наличие,c_price AS цена, pr_id as id_поставщика FROM cars WHERE c_model = @search", db.getConnection());
            command.Parameters.Add("@search", MySqlDbType.VarChar).Value = search;
            datatable.Clear();
            adapter.SelectCommand = command;
            adapter.Fill(datatable);

            if (datatable.Rows.Count > 0)
            {
                dataGridView1.DataSource = datatable;
                textBox3.Text = "";
            }
            else
            {
                textBox3.Text = "";
                MessageBox.Show("Вашему запросу не соответствует ни одна строка, проверьте корректность данных!", "Warning!");
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {
            loaddata();
        }
        //
    }

       
}

       
    

