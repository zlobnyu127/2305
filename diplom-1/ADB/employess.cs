using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ADB
{
    public partial class employess : Form
    {
        public employess()
        {
            InitializeComponent();
        }
        // closebtn
        private void label1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены что хотите выйти на главную страницу?", "Warning", MessageBoxButtons.YesNo,
        MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                this.Hide();
                glavnaya glavnaya = new glavnaya();
                glavnaya.Show();
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
        //
        //loaddata
        private void loademp()
        {
            DB db = new DB();
            string query = "";
            query = "SELECT e_id AS ID, e_FIO AS ФИО,e_pasp AS Паспортные_данные,e_gender AS Пол,e_post AS Должность, e_edu AS Образование,e_adres AS Адрес, e_phone as Телефон, e_sal AS Оклад FROM employes";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, db.getConnection());
            db.openConnection();
            DataTable datatable = new DataTable();
            datatable.Clear();
            adapter.Fill(datatable);
            dataGridView1.DataSource = datatable;
            db.closeConnection();
        }

        private void employess_Load(object sender, EventArgs e)
        {
            loademp();
        }
        //
        // insbtn
        private void insbutton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "" && textBox8.Text != "" && comboBox1.Text != "")
            {
                string efio = textBox1.Text;
                string epasp = textBox2.Text;
                string egender = comboBox1.Text;
                string epost = textBox4.Text;
                string eedu = textBox5.Text;
                string eadres = textBox6.Text;
                string ephone = textBox7.Text;
                string esal = textBox8.Text;
                DB db = new DB();
                DataTable table = new DataTable();

                MySqlDataAdapter adapter = new MySqlDataAdapter();

                MySqlCommand command = new MySqlCommand("INSERT INTO employes (e_fio,e_pasp,e_gender,e_post,e_edu,e_adres,e_phone,e_sal) VALUES (@ef,@ep,@eg,@epo,@eed,@ea,@eph,@esal)", db.getConnection());
                command.Parameters.Add("@ef", MySqlDbType.VarChar).Value = efio;
                command.Parameters.Add("@ep", MySqlDbType.VarChar).Value = epasp;
                command.Parameters.Add("@eg", MySqlDbType.VarChar).Value = egender;
                command.Parameters.Add("@epo", MySqlDbType.VarChar).Value = epost;
                command.Parameters.Add("@eed", MySqlDbType.VarChar).Value = eedu;
                command.Parameters.Add("@ea", MySqlDbType.VarChar).Value = eadres;
                command.Parameters.Add("@eph", MySqlDbType.VarChar).Value = ephone;
                command.Parameters.Add("@esal", MySqlDbType.VarChar).Value = esal;
                adapter.SelectCommand = command;
                adapter.Fill(table);
                loademp();
                textBox1.Text = ""; textBox2.Text = ""; textBox4.Text = ""; textBox5.Text = ""; textBox6.Text = ""; textBox7.Text = ""; textBox8.Text = ""; comboBox1.Text = "";
                MessageBox.Show("Строка добавлена!");
            }
            else
            {
                MessageBox.Show("Проверьте верно ли указаны данные");
                textBox1.Text = ""; textBox2.Text = ""; textBox4.Text = ""; textBox5.Text = ""; textBox6.Text = ""; textBox7.Text = ""; textBox8.Text = ""; comboBox1.Text = "";
            }
        }
        //
        //delbtn
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены что вы хотите удалить строку?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                DB db = new DB();
                db.openConnection();
                MySqlCommand command = new MySqlCommand("DELETE FROM employes where e_id = @id", db.getConnection());

                command.Parameters.Add("@id", MySqlDbType.VarChar).Value = dataGridView1.CurrentRow.Cells["id"].Value.ToString();//сравнение айди со строкой
                command.ExecuteNonQuery();
                loademp();
                db.closeConnection();
                MessageBox.Show("Строка удалена !");
            }
        }


        //searchbtn
        private void button3_Click(object sender, EventArgs e)
        {
            string search = textBox3.Text;
            DB db = new DB();
            DataTable datatable = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT e_id AS ID, e_FIO AS ФИО,e_pasp AS Паспортные_данные,e_gender AS Пол,e_post AS Должность, e_edu AS Образование,e_adres AS Адрес, e_phone as Телефон, e_sal AS Оклад FROM employes WHERE e_fio = @search", db.getConnection());
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
                MessageBox.Show("Вашему запросу не соответствует ни одна строка, проверьте корректность данных!","Warning!");
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {
            loademp();
        }
    }
}
