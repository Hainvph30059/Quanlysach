using System.Data.SqlClient;
using System.Data;
namespace QuanLySach
{
    delegate void delPassData(string user, string test);
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e) // xử lý dấu x
        {
            if (MessageBox.Show("Bạn có muốn đóng ứng dụng không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void btLogin_Click(object sender, EventArgs e) // xử lí đăng nhập nhân viên
        {
            // tạo đường dẫn để kết nối
            string conectionString = @"Data Source=HAINGUYEN\SQLEXPRESS;Initial Catalog=LOGINs;Integrated Security=True";
            // tạo câu truy vấn sql
            string query = "SELECT PowerAcount FROM ACOUNT WHERE UserLogin=@tk AND Passwords=@mk";
            string query2 = "SELECT UserName, PowerAcount FROM ACOUNT WHERE UserLogin=@tk AND Passwords=@mk";
            string user, test;
            // sử dụng using để giải phóng dung lượng sau khi dùng xong csdl
            using (SqlConnection connect = new SqlConnection(conectionString))
            {   // command để thực thi câu lệnh sql
                SqlCommand cmd = new SqlCommand(query, connect);
                SqlCommand cmd1 = new SqlCommand(query2, connect);
                // thêm tham số vào sqlcommand => so sánh
                cmd.Parameters.AddWithValue("@tk", txtUserName.Text);
                cmd.Parameters.AddWithValue("@mk", txtPassword.Text);
                cmd1.Parameters.AddWithValue("@tk", txtUserName.Text);
                cmd1.Parameters.AddWithValue("@mk", txtPassword.Text);
                try
                {
                    // bắt đầu chạy csdl
                    connect.Open();
                    // trả về đối tượng là các dòng kết quả của câu truy vấn
                    object data = cmd.ExecuteScalar();
                    //nếu đối tượng data có tồn tại dòng thì.
                    SqlDataReader reader = cmd1.ExecuteReader();
                    Service service = new Service();
                    delPassData del = new delPassData(service.funData);

                    if (txtUserName.Text == "")
                    {
                        MessageBox.Show("UserName không được bỏ trống");
                        txtUserName.Focus();
                        txtUserName.BackColor = Color.LightSteelBlue;
                    }
                    else if (txtPassword.Text == "")
                    {
                        MessageBox.Show("Password không được bỏ trống");
                        txtPassword.Focus();
                        txtPassword.BackColor = Color.LightSteelBlue;
                    }
                    else
                    {
                        while (reader.Read())
                            del(reader["UserName"].ToString(), reader["PowerAcount"].ToString());
                        if (data != null && (bool)data == true)
                        {
                            service.Show();
                            reader.Close();
                        }
                        else if (data != null && (bool)data == false)
                        {
                            service.Show();
                            reader.Close();
                        }
                        else
                        {
                            MessageBox.Show("Đăng nhập thất bại, tên đăng nhập hoặc mật khẩu không đúng", "Thông báo",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối đến máy chủ: " + ex.Message);

                }
                finally { connect.Close(); }
                
            }
        }
    }
}
