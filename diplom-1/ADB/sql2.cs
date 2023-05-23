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
    public partial class sql2 : Form
    {
        public sql2()
        {
            InitializeComponent();
        }
        //exit
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
        private void loadsql()
        {
            DB db = new DB();
            string query = "";
            query = "SELECT p_name AS имя, p_surname AS фамилия, p_phone AS Номер,c_id AS VIN, c_model AS Модель, c_color AS Цвет  FROM cars, provider WHERE pr_id = p_id;";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, db.getConnection());
            db.openConnection();
            DataTable datatable = new DataTable();
            datatable.Clear();
            adapter.Fill(datatable);
            dataGridView1.DataSource = datatable;
            db.closeConnection();
        }
        private void sql2_Load(object sender, EventArgs e)
        {
            loadsql();
        }
    }
}
