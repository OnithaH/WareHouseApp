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
    public partial class UserForm : Form
    {
        private DataGridView dgvUsers;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private ComboBox cmbRole;
        private TextBox txtSearch;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnSearch;
        private Button btnRefresh;
        private Button btnClose;
        private Label lblUsername;
        private Label lblPassword;
        private Label lblRole;
        private Label lblSearch;
        private Label lblTitle;

        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\AppData.mdf;Integrated Security=True;";

        public UserForm()
        {
            InitializeComponent();
            LoadUserData();
        }

        private void LoadUserData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT UserID, Username, Role FROM Users";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dgvUsers.DataSource = dataTable;

                        if (dgvUsers.Columns.Count > 0)
                        {
                            dgvUsers.Columns["UserID"].HeaderText = "ID";
                            dgvUsers.Columns["Username"].HeaderText = "Username";
                            dgvUsers.Columns["Role"].HeaderText = "Role";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading user data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvUsers.SelectedRows[0];
                txtUsername.Text = row.Cells["Username"].Value?.ToString() ?? "";
                txtPassword.Text = ""; // Don't display password for security

                string role = row.Cells["Role"].Value?.ToString() ?? "";
                if (cmbRole.Items.Contains(role))
                {
                    cmbRole.SelectedItem = role;
                }
                else
                {
                    cmbRole.SelectedIndex = -1;
                }
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    MessageBox.Show("Please enter username.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Please enter password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cmbRole.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a role.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtPassword.Text.Length < 6)
                {
                    MessageBox.Show("Password must be at least 6 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

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

                    string query = "INSERT INTO Users (Username, Password, Role) VALUES (@username, @password, @role)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", txtUsername.Text);
                        command.Parameters.AddWithValue("@password", txtPassword.Text);
                        command.Parameters.AddWithValue("@role", cmbRole.SelectedItem.ToString());

                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadUserData();
                            ClearInputs();
                        }
                        else
                        {
                            MessageBox.Show("Failed to add user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvUsers.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a user to update.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    MessageBox.Show("Please enter username.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cmbRole.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a role.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int userId = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["UserID"].Value);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query;
                    SqlCommand command;

                    // Update with or without password change
                    if (string.IsNullOrWhiteSpace(txtPassword.Text))
                    {
                        // Update without changing password
                        query = "UPDATE Users SET Username = @username, Role = @role WHERE UserID = @id";
                        command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@username", txtUsername.Text);
                        command.Parameters.AddWithValue("@role", cmbRole.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@id", userId);
                    }
                    else
                    {
                        // Update with password change
                        if (txtPassword.Text.Length < 6)
                        {
                            MessageBox.Show("Password must be at least 6 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        query = "UPDATE Users SET Username = @username, Password = @password, Role = @role WHERE UserID = @id";
                        command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@username", txtUsername.Text);
                        command.Parameters.AddWithValue("@password", txtPassword.Text);
                        command.Parameters.AddWithValue("@role", cmbRole.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@id", userId);
                    }

                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("User updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadUserData();
                        ClearInputs();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvUsers.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a user to delete.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string username = dgvUsers.SelectedRows[0].Cells["Username"].Value?.ToString();

                // Prevent deletion of admin user
                if (username?.ToLower() == "admin")
                {
                    MessageBox.Show("Cannot delete the admin user.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("Are you sure you want to delete this user?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    int userId = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["UserID"].Value);

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM Users WHERE UserID = @id";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@id", userId);

                            int deleteResult = command.ExecuteNonQuery();

                            if (deleteResult > 0)
                            {
                                MessageBox.Show("User deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadUserData();
                                ClearInputs();
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string searchTerm = txtSearch.Text.Trim();

                if (string.IsNullOrEmpty(searchTerm))
                {
                    LoadUserData();
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT UserID, Username, Role FROM Users WHERE Username LIKE @search OR Role LIKE @search";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@search", "%" + searchTerm + "%");
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dgvUsers.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching users: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadUserData();
            ClearInputs();
            txtSearch.Text = "";
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearInputs()
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            cmbRole.SelectedIndex = -1;
        }
        private void UserForm_Load(object sender, EventArgs e)
        {

        }
    }
}