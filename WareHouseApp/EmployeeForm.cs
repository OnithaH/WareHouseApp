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
    public partial class EmployeeForm : Form
    {
        private DataGridView dgvEmployees;
        private TextBox txtEmployeeName;
        private TextBox txtPosition;
        private TextBox txtSalary;
        private TextBox txtSearch;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnSearch;
        private Button btnRefresh;
        private Button btnClose;
        private Label lblEmployeeName;
        private Label lblPosition;
        private Label lblSalary;
        private Label lblSearch;
        private Label lblTitle;

        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\AppData.mdf;Integrated Security=True;";

        public EmployeeForm()
        {
            InitializeComponent();
            LoadEmployeeData();
        }

        

        private void LoadEmployeeData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT EmployeeID, EmployeeName, Position, Salary FROM Employees";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dgvEmployees.DataSource = dataTable;

                        if (dgvEmployees.Columns.Count > 0)
                        {
                            dgvEmployees.Columns["EmployeeID"].HeaderText = "ID";
                            dgvEmployees.Columns["EmployeeName"].HeaderText = "Employee Name";
                            dgvEmployees.Columns["Position"].HeaderText = "Position";
                            dgvEmployees.Columns["Salary"].HeaderText = "Salary";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading employee data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvEmployees_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvEmployees.SelectedRows[0];
                txtEmployeeName.Text = row.Cells["EmployeeName"].Value?.ToString() ?? "";
                txtPosition.Text = row.Cells["Position"].Value?.ToString() ?? "";
                txtSalary.Text = row.Cells["Salary"].Value?.ToString() ?? "";
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtEmployeeName.Text))
                {
                    MessageBox.Show("Please enter employee name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtSalary.Text, out decimal salary) || salary < 0)
                {
                    MessageBox.Show("Please enter a valid salary.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Employees (EmployeeName, Position, Salary) VALUES (@name, @position, @salary)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", txtEmployeeName.Text);
                        command.Parameters.AddWithValue("@position", txtPosition.Text ?? "");
                        command.Parameters.AddWithValue("@salary", salary);

                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Employee added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadEmployeeData();
                            ClearInputs();
                        }
                        else
                        {
                            MessageBox.Show("Failed to add employee.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding employee: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvEmployees.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select an employee to update.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtEmployeeName.Text))
                {
                    MessageBox.Show("Please enter employee name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtSalary.Text, out decimal salary) || salary < 0)
                {
                    MessageBox.Show("Please enter a valid salary.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int employeeId = Convert.ToInt32(dgvEmployees.SelectedRows[0].Cells["EmployeeID"].Value);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Employees SET EmployeeName = @name, Position = @position, Salary = @salary WHERE EmployeeID = @id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", txtEmployeeName.Text);
                        command.Parameters.AddWithValue("@position", txtPosition.Text ?? "");
                        command.Parameters.AddWithValue("@salary", salary);
                        command.Parameters.AddWithValue("@id", employeeId);

                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Employee updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadEmployeeData();
                            ClearInputs();
                        }
                        else
                        {
                            MessageBox.Show("Failed to update employee.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating employee: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvEmployees.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select an employee to delete.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("Are you sure you want to delete this employee?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    int employeeId = Convert.ToInt32(dgvEmployees.SelectedRows[0].Cells["EmployeeID"].Value);

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM Employees WHERE EmployeeID = @id";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@id", employeeId);

                            int deleteResult = command.ExecuteNonQuery();

                            if (deleteResult > 0)
                            {
                                MessageBox.Show("Employee deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadEmployeeData();
                                ClearInputs();
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete employee.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting employee: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string searchTerm = txtSearch.Text.Trim();

                if (string.IsNullOrEmpty(searchTerm))
                {
                    LoadEmployeeData();
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT EmployeeID, EmployeeName, Position, Salary FROM Employees WHERE EmployeeName LIKE @search OR Position LIKE @search";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@search", "%" + searchTerm + "%");
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dgvEmployees.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching employees: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadEmployeeData();
            ClearInputs();
            txtSearch.Text = "";
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearInputs()
        {
            txtEmployeeName.Text = "";
            txtPosition.Text = "";
            txtSalary.Text = "";
        }
    }
}