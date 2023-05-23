using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
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
    public partial class glavnaya : Form
    {
        public glavnaya()
        {
            InitializeComponent();
        }

        private void glavnaya_Load(object sender, EventArgs e)
        {

        }
        /* кнопка закрытия */
        private void exitButton_Click(object sender, EventArgs e)
        {
        DialogResult result = MessageBox.Show(
        "Вы уверены что хотите закрыть приложение?",
        "Warning",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
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
        

        /*knopka vixod*/
        private void logout_Click_1(object sender, EventArgs e)
        {
            this.Close();
            Loginform loginform = new Loginform();
            loginform.Show();
        }//


        //open cars
        private void button2_Click(object sender, EventArgs e)
        {
            if (perem.count == 1)
            {
                this.Close();
                firstavto firstavto = new firstavto();
                firstavto.Show();
            }
            else
            {
                MessageBox.Show("вам отказано в доступе");
            }
        }//


        // open provider
        private void button3_Click(object sender, EventArgs e)
        {
            if (perem.count == 1 || perem.count == 2)
            {
                this.Close();
                Provider provider = new Provider();
                provider.Show();
            }
            else
            {
                MessageBox.Show("Вам отказано в доступе");
            }
        }//


        //open users
        private void button4_Click(object sender, EventArgs e)
        {
            if (perem.count == 1)
            {
                this.Close();
                admin admin = new admin();
                admin.Show();
                
                
                
            }
            else
            {
                MessageBox.Show("вам отказано в доступе");
            }
        }//


        // open employess
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (perem.count == 1 || perem.count == 2)
            {
                this.Close();
                employess employess = new employess();
                employess.Show();
            }
            else
            {
                MessageBox.Show("Вам отказано в доступе");
            }
        }//
        // open buyers
        private void button5_Click(object sender, EventArgs e)
        {
            if (perem.count == 1 || perem.count == 2|| perem.count == 3)
            {
                this.Hide();
                Byuers byuers = new Byuers();
                byuers.Show();
            }
            else
            {
                MessageBox.Show("Вам отказано в доступе");
            }
        }//
        //opensql
        private void button6_Click(object sender, EventArgs e)
        {
            if (perem.count == 1 || perem.count == 2 || perem.count == 3)
            {
                this.Hide();
                sql1 sql1 = new sql1(); 
                sql1.Show();
            }
            else
            {
                MessageBox.Show("Вам отказано в доступе");
            }
        }
        // open sql2
        private void button7_Click(object sender, EventArgs e)
        {
            if (perem.count == 1 || perem.count == 2 || perem.count == 3)
            {
                this.Hide();
                sql2 sql2 = new sql2();
                sql2.Show();
            }
            else
            {
                MessageBox.Show("Вам отказано в доступе");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
