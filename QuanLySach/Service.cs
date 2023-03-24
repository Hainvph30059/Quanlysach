using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace QuanLySach
{

    public partial class Service : Form
    {
        string _conectionString = @"Data Source=HAINGUYEN\SQLEXPRESS;Initial Catalog=LOGINs;Integrated Security=True";
        public Service()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Service_Load(object sender, EventArgs e)
        {
            dgvBookList.Hide();
            lbBookInfo.Hide();
            loadSach();
        }
        public void loadSach()
        {
            string query = "SELECT ID, bookName AS N'Tên sách', author AS N'Tác giả', bookLoan AS N'Giá thuê(ngày)', bookPrice AS N'Giá bán', totalQuantity AS N'Số lượng', actualQuantity AS N'Số lượng trong kho', bookType AS N'Loại sách' FROM book";
            using (SqlConnection con = new SqlConnection(_conectionString))
            {
                con.Open();
                SqlDataAdapter data = new SqlDataAdapter(query, con);
                DataSet list = new DataSet();
                data.Fill(list);
                dgvBookList.DataSource = list.Tables[0];
                dgvBookList.Columns["ID"].Width = 65;
                dgvBookList.Columns["Tên sách"].Width = 200;
                dgvBookList.Columns["Giá thuê(ngày)"].Width = 87;
                dgvBookList.Columns["Tác giả"].Width = 130;
                dgvBookList.Columns["Loại sách"].Width = 200;
                con.Close();
            }
        }
        public void funData(string data, string test)
        {
            
            if(test == "True")
            {
                textBox1.Text = "Quản lí: " + data;
            }
            else
            {
                textBox1.Text = "Nhân viên: " + data;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox3.BackColor = SystemColors.Highlight;
            lbBookInfo.Show();
            dgvBookList.Show();
        }
    }
}
