﻿using QuanLyNhanSu.Dialog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace QuanLyNhanSu
{
    public partial class Login : Form
    {
        private string username; 
        public Login()
        {
            InitializeComponent();
        }

        private void guna2CircleButton1_Click_1(object sender, EventArgs e)
        {
            Question questionForm = new Question();
            questionForm.QuestionText = "thoát không?";
            questionForm.OkButtonText = "Có"; 

            if (questionForm.ShowDialog() == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register rg = new Register();
            rg.ShowDialog();
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Login_login_Click(object sender, EventArgs e)
        {
            username = Login_user.Text; 
            int userId = CheckLogin(username, Login_passwd.Text); 

            if (userId != -1)
            {
                pictureBox1.Image = QuanLyNhanSu.Properties.Resources.icons8_jake;
                HomPage hom = new HomPage(userId, username); 
                hom.Show();
                this.Hide();
            }
            else
            {
                pictureBox1.Image = QuanLyNhanSu.Properties.Resources.loginfalse;
                thongbao.Text = "*Sai tài khoản hoặc mật khẩu !";
                thongbao.ForeColor = Color.Red;
                //Notification notification = new Notification();
                //notification.NotificationText = "Vui lòng kiểm tra lại thông tin đăng nhập !";
                //notification.OkButtonText = "OK";
                //if (notification.ShowDialog() == DialogResult.OK)
                //{
                //    Application.Exit();
                //}
            }
        }


        private int CheckLogin(string username, string password)
        {
            using (SqlConnection connection = connectdatabase.Connect())
            {
                if (connection == null)
                {
                    Error error = new Error();
                    error.ErrorText = "Lỗi kết nối Database !";
                    error.OkButtonText = "OK";
                    return -1; 
                }

                try
                {
                    connection.Open();
                    string query = "SELECT ID FROM TaiKhoan WHERE TaiKhoan = @TaiKhoan AND MatKhau = @MatKhau";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TaiKhoan", username);
                        command.Parameters.AddWithValue("@MatKhau", password);
                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            return Convert.ToInt32(result); 
                        }
                        else
                        {
                            return -1; 
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1; 
                }
            }
        }

        private bool isPasswordVisible = false;
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible; 

            if (isPasswordVisible)
            {
                Login_passwd.PasswordChar = '\0'; 
            }
            else
            {
                Login_passwd.PasswordChar = '•';
            }
        }

        private void Register_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register register = new Register();
            register.ShowDialog();
            this.Hide();
        }
    }
}
