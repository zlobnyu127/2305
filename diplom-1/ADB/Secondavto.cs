using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace ADB
{
    public partial class Secondavto : Form
    {
        public Secondavto()
        {
            InitializeComponent();
        }
        
        int x = 0;
        string query = "";

        // load
        private void loaddata()
        {
            
            DB db = new DB();
            query = "Select av_name, av_dvig, av_complectaction, av_c_opic, av_color, av_trans, av_price, av_foto FROM avtomobiles WHERE av_name = @id.n and av_status = 'В наличии'";
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(query, db.getConnection());
            command.Parameters.Add("@id.n", MySqlDbType.String).Value = id.n;
            adapter.SelectCommand = command;
            db.openConnection();
            DataTable datatable = new DataTable();
            datatable.Clear();
            adapter.Fill(datatable);
            if (datatable.Rows.Count > 0)
            {
                DataRow row = datatable.Rows[x];
                label1.Text = row[0].ToString();
                label2.Text = row[1].ToString();
                label3.Text = row[2].ToString();
                label7.Text = row[3].ToString();
                label4.Text = row[4].ToString();
                label9.Text = row[5].ToString();
                label5.Text = row[6].ToString();
                byte[] img = (byte[])(row[7]);
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
            else { MessageBox.Show("Ни один элемент не соответствует вашему запросу"); }
            db.closeConnection();

        }
        private void Secondavto_Load(object sender, EventArgs e)
        {
            int q = 0;
            int count = 0;
            string y = "";
            
            DB d = new DB();
            d.openConnection();
            MySqlCommand comman = new MySqlCommand("SELECT COUNT(*) FROM avtomobiles Group by av_color", d.getConnection());
            MySqlDataReader reade = comman.ExecuteReader();
            reade.Read();
            if (reade.HasRows)
            {
                y = reade[0].ToString();
                count = Int32.Parse(y);
            }
            d.closeConnection();
            
            DB db = new DB();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("select av_color from avtomobiles group by av_color", db.getConnection());
            adapter.SelectCommand = command;
            db.openConnection();
            DataTable datatable = new DataTable();
            datatable.Clear();
            adapter.Fill(datatable);
            while (q < count+1)
            {
                DataRow row = datatable.Rows[q];
                comboBox1.Items.Add(row[0].ToString());
                q++;
            }
            loaddata();

        }
        //nazad
        private void button1_Click(object sender, EventArgs e)
        {
            if (x != 0)
            {
                x--;
                loaddata();   
            }
            else MessageBox.Show("Конец");

        }
        //vpered
        private void button2_Click(object sender, EventArgs e)
        {
            string y = "";
            int count = 0;
            DB d = new DB();
            d.openConnection();
            MySqlCommand comman = new MySqlCommand("SELECT COUNT(*) FROM avtomobiles where av_name = @id.n and av_status = 'В наличии'", d.getConnection());
            comman.Parameters.Add("@id.n",MySqlDbType.String).Value = id.n;
            MySqlDataReader reade = comman.ExecuteReader();
            reade.Read();
            if (reade.HasRows)
            {
                
                y = reade[0].ToString();
                 count = Int32.Parse(y);
            }
            d.closeConnection();
            if (x < count-1)
            {
                x++;
                loaddata();
            }
            else MessageBox.Show("Конец");
            
        }


    }
}
