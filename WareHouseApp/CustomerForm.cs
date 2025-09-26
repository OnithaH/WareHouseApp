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
    public partial class CustomerForm : Form
    {
        private DataGridView dgvCustomers;
        private TextBox txtCustomerName;
        private TextBox txtEmail;
        private TextBox txtPhone;
        private TextBox txtSearch;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnSearch;
        private Button btnRefresh;
        private Button btnClose;
        private Label lblCustomerName;
        private Label lblEmail;
        private Label lblPhone;
        private Label lblSearch;
        private Label lblTitle;

        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\AppData.mdf;Integrated Security=True;";

        public CustomerForm()
        {
            InitializeComponent();
            LoadCustomerData();
        }



        private void LoadCustomerData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT CustomerID, CustomerName, Email, Phone FROM Customers";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dgvCustomers.DataSource = dataTable;

                        if (dgvCustomers.Columns.Count > 0)
                        {
                            dgvCustomers.Columns["CustomerID"].HeaderText = "ID";
                            dgvCustomers.Columns["CustomerName"].HeaderText = "Customer Name";
                            dgvCustomers.Columns["Email"].HeaderText = "Email";
                            dgvCustomers.Columns["Phone"].HeaderText = "Phone";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading customer data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvCustomers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvCustomers.SelectedRows[0];
                txtCustomerName.Text = row.Cells["CustomerName"].Value?.ToString() ?? "";
                txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
                txtPhone.Text = row.Cells["Phone"].Value?.ToString() ?? "";
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCustomerName.Text))
                {
                    MessageBox.Show("Please enter customer name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Customers (CustomerName, Email, Phone) VALUES (@name, @email, @phone)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", txtCustomerName.Text);
                        command.Parameters.AddWithValue("@email", txtEmail.Text ?? "");
                        command.Parameters.AddWithValue("@phone", txtPhone.Text ?? "");

                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Customer added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadCustomerData();
                            ClearInputs();
                        }
                        else
                        {
                            MessageBox.Show("Failed to add customer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding customer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCustomers.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a customer to update.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtCustomerName.Text))
                {
                    MessageBox.Show("Please enter customer name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int customerId = Convert.ToInt32(dgvCustomers.SelectedRows[0].Cells["CustomerID"].Value);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Customers SET CustomerName = @name, Email = @email, Phone = @phone WHERE CustomerID = @id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", txtCustomerName.Text);
                        command.Parameters.AddWithValue("@email", txtEmail.Text ?? "");
                        command.Parameters.AddWithValue("@phone", txtPhone.Text ?? "");
                        command.Parameters.AddWithValue("@id", customerId);

                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Customer updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadCustomerData();
                            ClearInputs();
                        }
                        else
                        {
                            MessageBox.Show("Failed to update customer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating customer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCustomers.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a customer to delete.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("Are you sure you want to delete this customer?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    int customerId = Convert.ToInt32(dgvCustomers.SelectedRows[0].Cells["CustomerID"].Value);

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM Customers WHERE CustomerID = @id";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@id", customerId);

                            int deleteResult = command.ExecuteNonQuery();

                            if (deleteResult > 0)
                            {
                                MessageBox.Show("Customer deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadCustomerData();
                                ClearInputs();
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete customer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting customer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string searchTerm = txtSearch.Text.Trim();

                if (string.IsNullOrEmpty(searchTerm))
                {
                    LoadCustomerData();
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT CustomerID, CustomerName, Email, Phone FROM Customers WHERE CustomerName LIKE @search OR Email LIKE @search";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@search", "%" + searchTerm + "%");
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dgvCustomers.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching customers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadCustomerData();
            ClearInputs();
            txtSearch.Text = "";
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearInputs()
        {
            txtCustomerName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
        }
    }
}