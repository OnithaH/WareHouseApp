using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WareHouseApp
{
    public partial class DashBoard : Form
    {
        public DashBoard()
        {
            InitializeComponent();
            SetupButtons();
        }

        private void SetupButtons()
        {
            // Set button texts and events
            button1.Text = "Inventory Management";
            button2.Text = "Customer Management";
            button3.Text = "Employee Management";
            button4.Text = "User Management";
            button5.Text = "Reports";
            button6.Text = "Settings";

            button7.Text = "Profile";
            button8.Text = "Settings";
            button9.Text = "Logout";

            // Add click events
            button1.Click += Button1_Click; // Inventory
            button2.Click += Button2_Click; // Customers
            button3.Click += Button3_Click; // Employees
            button4.Click += Button4_Click; // Users
            button5.Click += Button5_Click; // Reports (Coming Soon)
            button6.Click += Button6_Click; // Settings (Coming Soon)

            button7.Click += Button7_Click; // Profile (Coming Soon)
            button8.Click += Button8_Click; // Settings (Coming Soon)
            button9.Click += Button9_Click; // Logout
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            // Open Inventory Management Form
            InventoryForm inventoryForm = new InventoryForm();
            inventoryForm.ShowDialog();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            // Open Customer Management Form
            CustomerForm customerForm = new CustomerForm();
            customerForm.ShowDialog();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            // Open Employee Management Form
            EmployeeForm employeeForm = new EmployeeForm();
            employeeForm.ShowDialog();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            // Open User Management Form
            UserForm userForm = new UserForm();
            userForm.ShowDialog();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Reports feature coming soon!", "Coming Soon", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Settings feature coming soon!", "Coming Soon", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Profile feature coming soon!", "Coming Soon", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Settings feature coming soon!", "Coming Soon", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
                Application.Restart(); // This will restart the application and show login form
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit(); // Close entire application when dashboard is closed
        }
        private void DashBoard_Load(object sender, EventArgs e)
        {

        }
    }
}