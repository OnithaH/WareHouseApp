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

namespace WareHouseApp
{
    public partial class SignupForm : Form
    {
        private TextBox txtUsername;
        private TextBox txtPassword;
        private TextBox txtConfirmPassword;
        private ComboBox cmbRole;
        private Button btnSignup;
        private Button btnCancel;
        private Label lblUsername;
        private Label lblPassword;
        private Label lblConfirmPassword;
        private Label lblRole;
        private Label lblTitle;

        public SignupForm()
        {
            InitializeComponent();
        }

        

        private void btnSignup_Click(object sender, EventArgs e)
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
                    string.IsNullOrWhiteSpace(txtPassword.Text) ||
                    string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
                {
                    MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    MessageBox.Show("Passwords do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtPassword.Text.Length < 6)
                {
                    MessageBox.Show("Password must be at least 6 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Save to database
                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\AppData.mdf;Integrated Security=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Check if username already exists
                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @username";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@username", txtUsername.Text);
                        int count = (int)checkCommand.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("Username already exists. Please choose a different username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Insert new user
                    string insertQuery = "INSERT INTO Users (Username, Password, Role) VALUES (@username, @password, @role)";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@username", txtUsername.Text);
                        insertCommand.Parameters.AddWithValue("@password", txtPassword.Text);
                        insertCommand.Parameters.AddWithValue("@role", cmbRole.SelectedItem.ToString());

                        int result = insertCommand.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Account created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed to create account. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating account: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}