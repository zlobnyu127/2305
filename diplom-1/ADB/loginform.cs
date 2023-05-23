using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ADB
{
    public partial class Loginform : Form
    {
        public Loginform()
        {
            InitializeComponent();
            this.passField.AutoSize = false;
            this.passField.Size = new Size(this.passField.Size.Width,64);

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            /*login*/
        }

        private void close_form_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void passField_TextChanged(object sender, EventArgs e)
        {

        }

        private void close_form_MouseEnter(object sender, EventArgs e)
        {
            close_form.ForeColor = Color.Red;
        }

        private void close_form_MouseLeave(object sender, EventArgs e)
        {
            close_form.ForeColor = Color.White;
        }
        Point lastPoint;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }
        /*окно авторизации, подключение к базе, проверка логина и пароля*/
        private void vxod_Click(object sender, EventArgs e)
        {
            string loginUser = textBox1.Text;
            string loginPass = passField.Text;

            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM users WHERE login= @ul AND pass = @up", db.getConnection());
            command.Parameters.Add("@ul",MySqlDbType.VarChar).Value = loginUser;
            command.Parameters.Add("@up", MySqlDbType.VarChar).Value = loginPass;

            adapter.SelectCommand = command;
            adapter.Fill(table);
                        
            if (table.Rows.Count == 1)
            {
                object cellValue = table.Rows[0][3];
                if (cellValue.ToString() == "Админ") { perem.count = 1; }
                
                else if (cellValue.ToString() == "Менеджер") { perem.count = 2 ;}
                
                else if(cellValue.ToString() == "Продавец") { perem.count = 3;}

                else 
                { perem.count = 0;}

                MessageBox.Show("Данные введены успешно");
                this.Hide();
                glavnaya glavnaya = new glavnaya();
                glavnaya.Show(); 
            }
            else 
            { 
                MessageBox.Show("Неправильно введен логин или пароль");
                textBox1.Clear();
                passField.Clear();                  
            }
        }

    }
}
