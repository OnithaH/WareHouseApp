using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WareHouseApp.People;
using WareHouseApp.Resources.AppStrings;

namespace WareHouseApp
{
    public partial class Form1 : Form
    {
        Admin admin = new Admin();
        LoginPage loginPage = new LoginPage();

        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void butLogin_Click(object sender, EventArgs e)
        {
            string UserNameTxt = txtName.Text.Trim();
            string PassTxt = txtPassword.Text.Trim();

            // Basic validation
            if (string.IsNullOrEmpty(UserNameTxt) || string.IsNullOrEmpty(PassTxt))
            {
                MessageBox.Show("Please enter both username and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool loginSuccess = admin.Login(UserNameTxt, PassTxt);

                if (loginSuccess)
                {
                    // Open dashboard and hide login form
                    DashBoard dashboard = new DashBoard();
                    dashboard.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show(loginPage.LoginErrorTitleEn, loginPage.LoginErrorMessageEn, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login Error", "An error occurred during login: " + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                txtPassword.Text = ""; // Clear password field
            }
        }
    }
}