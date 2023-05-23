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
    public partial class admin : Form
    {
        public admin()
        {
            InitializeComponent();
            loaduser();

        }
        /* кнопка закрытия*/
        private void close_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
        "Вы уверены что хотите закрыть окно ?",
        "Warning",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                glavnaya glavnaya = new glavnaya();
                this.Close();
                glavnaya.Show();
                

            }
        }

        private void close_MouseEnter(object sender, EventArgs e)
        {
            close.ForeColor = Color.Red;
        }

        private void close_MouseLeave(object sender, EventArgs e)
        {
            close.ForeColor = Color.White;
        }
        /* заполнение таблицы*/
        private void loaduser()
        {
            DB db = new DB();
            string query = "";
            query = "SELECT id AS id, login AS логин, pass AS пароль, u_dolgnoct AS должность  FROM users";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, db.getConnection());
            db.openConnection();
            DataTable datatable = new DataTable();
            datatable.Clear();
            adapter.Fill(datatable);
            userTable.DataSource = datatable;
            db.closeConnection();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        /*кнопка добавления*/
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && (textBox3.Text == "Админ"|| textBox3.Text == "Менеджер"|| textBox3.Text == "Продавец"))
            {
                string loginUser = textBox1.Text;
                string loginPass = textBox2.Text;
                string udolgnoct = textBox3.Text;

                DB db = new DB();

                DataTable table = new DataTable();

                MySqlDataAdapter adapter = new MySqlDataAdapter();

                MySqlCommand command = new MySqlCommand("INSERT INTO users (login, pass, u_dolgnoct) VALUES (@ul,@up,@ud)", db.getConnection());
                command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = loginUser;
                command.Parameters.Add("@up", MySqlDbType.VarChar).Value = loginPass;
                command.Parameters.Add("@ud", MySqlDbType.VarChar).Value = udolgnoct;
                adapter.SelectCommand = command;
                adapter.Fill(table);
                loaduser();
                textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = "";
                MessageBox.Show("Строка добавлена!");
            }
            else 
            { 
                MessageBox.Show("Проверьте корректность данных и не оставляйте пустые поля!");
                textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = "";
            }
        }
        //удаление строки
        private void button3_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();
            MySqlCommand command = new MySqlCommand("DELETE FROM users where id = @id", db.getConnection());

            command.Parameters.Add("@id", MySqlDbType.VarChar).Value = userTable.CurrentRow.Cells["id"].Value.ToString();//сравнение айди со строкой
            command.ExecuteNonQuery();
            loaduser();
            db.closeConnection();
            MessageBox.Show("Строка удалена!");
        }

        
        //
    }
}
