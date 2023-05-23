using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADB
{
    public partial class firstavto : Form
    {
        public firstavto()
        {
            InitializeComponent();
            
        }
        int sum = 1;
        //NAZAD
        private void button1_Click(object sender, EventArgs e)
        {

            if (sum >= 1)
            {
                sum--;
                DB db = new DB();
                db.openConnection();
                MySqlCommand command = new MySqlCommand("SELECT image FROM avto WHERE a_id = @sum", db.getConnection());
                command.Parameters.Add("@sum", MySqlDbType.Int64).Value = sum;
                MySqlDataReader Reader = command.ExecuteReader();
                Reader.Read();
                if (Reader.HasRows)
                {
                    byte[] img = (byte[])(Reader[0]);
                    if (img == null)
                    {                       
                        pictureBox1.Image = null;
                    }
                    else
                    {
                        DB d = new DB();
                        d.openConnection();
                        MySqlCommand comman = new MySqlCommand("Select a_name,a_price,a_info FROM avto WHERE a_id = @sum", d.getConnection());
                        comman.Parameters.Add("@sum", MySqlDbType.Int64).Value = sum;
                        MySqlDataReader reade = comman.ExecuteReader();
                        reade.Read();
                        if (reade.HasRows)
                        {
                            label1.Text = reade[0].ToString();
                            label2.Text = reade[1].ToString();
                            label3.Text = reade[2].ToString();
                        }
                        d.closeConnection();



                        MemoryStream ms = new MemoryStream(img);
                        pictureBox1.Image = Image.FromStream(ms);
                        
                    }
                }
                

                else { MessageBox.Show(" Конец "); sum++;  }
                db.closeConnection();

            }
        }
        //VPERED
        private void button2_Click(object sender, EventArgs e)
        {
            
            if (sum < 10)
            {
                sum++;
                DB db = new DB();
                db.openConnection();
                MySqlCommand command = new MySqlCommand("Select image from avto WHERE a_id = @sum ", db.getConnection());
                command.Parameters.Add("@sum", MySqlDbType.Int64).Value = sum;
                MySqlDataReader Reader = command.ExecuteReader();
                Reader.Read();
                if (Reader.HasRows)
                {
                    
                    byte[] img = (byte[])(Reader[0]);
                    if (img == null)
                    {
                        pictureBox1.Image = null;
                    }
                    else
                    {
                        DB d = new DB();
                        d.openConnection();
                        MySqlCommand comman = new MySqlCommand("Select a_name,a_price,a_info FROM avto WHERE a_id = @sum", d.getConnection());
                        comman.Parameters.Add("@sum", MySqlDbType.Int64).Value = sum;
                        MySqlDataReader reade = comman.ExecuteReader();
                        reade.Read();
                        if (reade.HasRows)
                        {
                            label1.Text = reade[0].ToString();
                            label2.Text = reade[1].ToString();
                            label3.Text = reade[2].ToString();
                        }
                        d.closeConnection();



                        MemoryStream ms = new MemoryStream(img);
                        pictureBox1.Image = Image.FromStream(ms);
                    }
                    
                }
                
                else { MessageBox.Show(" Конец "); sum--; }
                db.closeConnection();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*
            if( sum == 1) { id.cou = 1; }
            else if( sum== 2) { id.cou = sum + sum; }
            else if (sum==3) { id.cou = sum + sum + 1; }
            else if (sum == 4) { id.cou = sum + sum + 2; }*/
            id.n = label1.Text;
            
            Secondavto secondavto = new Secondavto();
            secondavto.Show();
            this.Hide();


        }
        //LOAD
        private void firstavto_Load(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();
            MySqlCommand command = new MySqlCommand("Select image from avto WHERE a_id = @sum ", db.getConnection());
            command.Parameters.Add("@sum", MySqlDbType.Int64).Value = sum;
            MySqlDataReader Reader = command.ExecuteReader();
            Reader.Read();
            if (Reader.HasRows)
            {
                
                byte[] img = (byte[])(Reader[0]);
                if (img == null)
                {
                    pictureBox1.Image = null;
                }
                else
                {
                    
                    MemoryStream ms = new MemoryStream(img);
                    pictureBox1.Image = Image.FromStream(ms);
                }
            }
            else { MessageBox.Show(" Конец ");  }
            db.closeConnection();
            DB d = new DB();
            d.openConnection();
            MySqlCommand comman = new MySqlCommand("Select a_name,a_price,a_info FROM avto WHERE a_id = @sum", d.getConnection());
            comman.Parameters.Add("@sum", MySqlDbType.Int64).Value = sum;
            MySqlDataReader reade = comman.ExecuteReader();
            reade.Read();
            if (reade.HasRows)
            {
                label1.Text = reade[0].ToString();
                label2.Text = reade[1].ToString();
                label3.Text = reade[2].ToString();
            }
            d.closeConnection();
        }
    }
}
