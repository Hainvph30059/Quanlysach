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
        string _query = "SELECT ID, bookName AS N'Tên sách', author AS N'Tác giả', bookLoan AS N'Giá thuê(ngày)', bookPrice AS N'Giá bán', totalQuantity AS N'Số lượng', actualQuantity AS N'Số lượng trong kho', bookType AS N'Loại sách' FROM book";
        public Service()
        {
            InitializeComponent();  
        }

        private void Service_Load(object sender, EventArgs e)
        {  
            //  dgvBookList.Hide();
            lbBookInfo.Hide();
            loadSach(_query);
            gbQuanLy.Hide();
            gbThemSach.Hide();
        }
        public void loadSach(string query)
        {
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

            if (test == "True")
            {
                textBox1.Text = "Quản lí: " + data;
            }
            else
            {
                textBox1.Text = "Nhân viên: " + data;
            }
        }
        // Dịch vụ
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        // Dịch vụ
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }
        // Thoát
        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox3.BackColor = SystemColors.Highlight;
            lbBookInfo.Show();
            gbQuanLy.Show();
            //show chức năng quản lí
        }

        private void btThemSach_Click(object sender, EventArgs e)
        {
            gbThemSach.Show(); // ẩn control quản lí, hiển thị bảng lấy dữ liệu thêm sách
            gbQuanLy.Hide();
        }
        // Sửa sách
        private void btSuaSach_Click(object sender, EventArgs e)
        {
            string query = "UPDATE book SET bookName = @bookName, author = @author, bookLoan = @bookLoan, bookPrice = @bookPrice, totalQuantity = @totalQuantity, actualQuantity = @actualQuantity, bookType = @bookType WHERE ID = @ID";
            using (SqlConnection con = new SqlConnection(_conectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    int rowIndex = dgvBookList.CurrentRow.Index;
                    string ID = dgvBookList.Rows[rowIndex].Cells[0].Value?.ToString();
                    string name = dgvBookList.Rows[rowIndex].Cells[1].Value.ToString();
                    string author = dgvBookList.Rows[rowIndex].Cells[2].Value.ToString();
                    string giaThue = dgvBookList.Rows[rowIndex].Cells[3].Value.ToString();
                    string giaBan = dgvBookList.Rows[rowIndex].Cells[4].Value.ToString();
                    string soLuong = dgvBookList.Rows[rowIndex].Cells[5].Value.ToString();
                    string soLuongKho = dgvBookList.Rows[rowIndex].Cells[6].Value.ToString();
                    string type = dgvBookList.Rows[rowIndex].Cells[7].Value?.ToString();
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@bookName", name);
                    cmd.Parameters.AddWithValue("@author", author);
                    cmd.Parameters.AddWithValue("@bookLoan", giaThue);
                    cmd.Parameters.AddWithValue("@bookPrice", giaBan);
                    cmd.Parameters.AddWithValue("@totalQuantity", soLuong);
                    cmd.Parameters.AddWithValue("@actualQuantity", soLuongKho);
                    cmd.Parameters.AddWithValue("@bookType", type);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { con.Close(); loadSach(_query); }
            }
        }
        // Xóa sách
        private void btXoaSach_Click(object sender, EventArgs e)
        {
            string query = "DELETE FROM book WHERE ID = @ID";
            using (SqlConnection con = new SqlConnection(_conectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    int rowIndex = dgvBookList.CurrentRow.Index;
                    string ID = dgvBookList.Rows[rowIndex].Cells[0].Value.ToString();
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { con.Close(); loadSach(_query); }
            }
        }
        // Tìm sách
        private void btTimSach_Click(object sender, EventArgs e)
        {

        }
        // Thêm sách
        private void btThem_Click(object sender, EventArgs e)
        {
            bool hasInvalidRow = false;
            for (int i = 0; i < dgvThemSach.Rows.Count - 1; i++)
            {
                string name = dgvThemSach.Rows[i].Cells["TenSach"].Value?.ToString();
                string author = dgvThemSach.Rows[i].Cells["author"].Value?.ToString();
                string giaThue = dgvThemSach.Rows[i].Cells["GiaThue"].Value?.ToString();
                string giaBan = dgvThemSach.Rows[i].Cells["GiaBan"].Value?.ToString();
                string soLuong = dgvThemSach.Rows[i].Cells["SoLuong"].Value?.ToString();
                string soLuongKho = dgvThemSach.Rows[i].Cells["Kho"].Value?.ToString();
                string type = dgvThemSach.Rows[i].Cells["Type"].Value?.ToString();

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(author) || string.IsNullOrEmpty(giaThue) || string.IsNullOrEmpty(giaBan) || string.IsNullOrEmpty(soLuong) || string.IsNullOrEmpty(soLuongKho) || string.IsNullOrEmpty(type))
                {
                    hasInvalidRow = true;
                    if (string.IsNullOrEmpty(name))
                    {
                        dgvThemSach.Rows[i].Cells["TenSach"].Style.BackColor = Color.LightPink;
                        dgvThemSach.CurrentCell = dgvThemSach.Rows[i].Cells["TenSach"];
                        dgvThemSach.BeginEdit(true);
                    }
                    if (string.IsNullOrEmpty(author))
                    {
                        dgvThemSach.Rows[i].Cells["author"].Style.BackColor = Color.LightPink;
                        dgvThemSach.CurrentCell = dgvThemSach.Rows[i].Cells["author"];
                        dgvThemSach.BeginEdit(true);
                    }
                    if (string.IsNullOrEmpty(giaThue))
                    {
                        dgvThemSach.Rows[i].Cells["GiaThue"].Style.BackColor = Color.LightPink;
                        dgvThemSach.CurrentCell = dgvThemSach.Rows[i].Cells["GiaThue"];
                        dgvThemSach.BeginEdit(true);
                    }
                    if (string.IsNullOrEmpty(giaBan))
                    {
                        dgvThemSach.Rows[i].Cells["GiaBan"].Style.BackColor = Color.LightPink;
                        dgvThemSach.CurrentCell = dgvThemSach.Rows[i].Cells["GiaBan"];
                        dgvThemSach.BeginEdit(true);
                    }
                    if (string.IsNullOrEmpty(soLuong))
                    {
                        dgvThemSach.Rows[i].Cells["SoLuong"].Style.BackColor = Color.LightPink;
                        dgvThemSach.CurrentCell = dgvThemSach.Rows[i].Cells["SoLuong"];
                        dgvThemSach.BeginEdit(true);
                    }
                    if (string.IsNullOrEmpty(soLuongKho))
                    {
                        dgvThemSach.Rows[i].Cells["Kho"].Style.BackColor = Color.LightPink;
                        dgvThemSach.CurrentCell = dgvThemSach.Rows[i].Cells["Kho"];
                        dgvThemSach.BeginEdit(true);
                    }
                    if (string.IsNullOrEmpty(type))
                    {
                        dgvThemSach.Rows[i].Cells["Type"].Style.BackColor = Color.LightPink;
                        dgvThemSach.CurrentCell = dgvThemSach.Rows[i].Cells["Type"];
                        dgvThemSach.BeginEdit(true);
                    }
                }
            }
            if (hasInvalidRow)
            {
                MessageBox.Show("Please fill all columns");
                return;
            }
            // Thêm
            string query = "INSERT INTO book(bookName, author, bookLoan, bookPrice, totalQuantity, actualQuantity, bookType) VALUES (@bookName, @author, @bookLoan, @bookPrice, @totalQuantity, @actualQuantity, @bookType)";
            for (int i = 0; i < dgvThemSach.Rows.Count - 1; i++)
            {
                string name = dgvThemSach.Rows[i].Cells["TenSach"].Value?.ToString();
                string author = dgvThemSach.Rows[i].Cells["author"].Value?.ToString();
                string giaThue = dgvThemSach.Rows[i].Cells["GiaThue"].Value?.ToString();
                string giaBan = dgvThemSach.Rows[i].Cells["GiaBan"].Value?.ToString();
                string soLuong = dgvThemSach.Rows[i].Cells["SoLuong"].Value?.ToString();
                string soLuongKho = dgvThemSach.Rows[i].Cells["Kho"].Value?.ToString();
                string type = dgvThemSach.Rows[i].Cells["Type"].Value?.ToString();
                using (SqlConnection con = new SqlConnection(_conectionString))
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@bookName", name);
                        cmd.Parameters.AddWithValue("@author", author);
                        cmd.Parameters.AddWithValue("@bookLoan", Convert.ToInt32(giaThue));
                        cmd.Parameters.AddWithValue("@bookPrice", Convert.ToInt32(giaBan));
                        cmd.Parameters.AddWithValue("@totalQuantity", Convert.ToInt32(soLuong));
                        cmd.Parameters.AddWithValue("@actualQuantity", Convert.ToInt32(soLuongKho));
                        cmd.Parameters.AddWithValue("@bookType", type);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally { con.Close(); }
                }
            }
            loadSach(_query);
            gbThemSach.Hide();
            gbQuanLy.Show();
        }
        // Tài khoản
        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
        // trả sách
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string query = "SELECT ID, bookName AS N'Tên sách', author AS N'Tác giả', bookLoan AS N'Giá thuê(ngày)', bookPrice AS N'Giá bán', totalQuantity AS N'Số lượng', actualQuantity AS N'Số lượng trong kho', bookType AS N'Loại sách' FROM book Where bookName like '%"+txtSearch.Text+"%'";
            loadSach(query);
        }

    }
}
