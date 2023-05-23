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
    public partial class sql1 : Form
    {
        public sql1()
        {
            InitializeComponent();
        }
        private void loadsql()
        {
            DB db = new DB();
            string query = "";
            query = "SELECT b_fio AS ФИО,b_phone AS Номер,c_id AS VIN, c_model AS Модель, c_color AS Цвет  FROM cars, buyers WHERE c_id = b_cid;";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, db.getConnection());
            db.openConnection();
            DataTable datatable = new DataTable();
            datatable.Clear();
            adapter.Fill(datatable);
            dataGridView1.DataSource = datatable;
            db.closeConnection();
        }

        private void sql1_Load(object sender, EventArgs e)
        {
            loadsql();
        }

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
    }
}
