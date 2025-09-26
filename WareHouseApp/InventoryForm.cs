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
// Remove this line: using WareHouseApp.Material;

namespace WareHouseApp
{
    public partial class InventoryForm : Form
    {
        private DataGridView dgvInventory;
        private TextBox txtItemName;
        private TextBox txtQuantity;
        private TextBox txtPrice;
        private TextBox txtSearch;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnSearch;
        private Button btnRefresh;
        private Button btnClose;
        private Label lblItemName;
        private Label lblQuantity;
        private Label lblPrice;
        private Label lblSearch;
        private Label lblTitle;

        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\AppData.mdf;Integrated Security=True;";

        public InventoryForm()
        {
            InitializeComponent();
            LoadInventoryData();
        }

        private void LoadInventoryData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT ItemID, ItemName, Quantity, Price FROM Inventory";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dgvInventory.DataSource = dataTable;

                        if (dgvInventory.Columns.Count > 0)
                        {
                            dgvInventory.Columns["ItemID"].HeaderText = "ID";
                            dgvInventory.Columns["ItemName"].HeaderText = "Item Name";
                            dgvInventory.Columns["Quantity"].HeaderText = "Quantity";
                            dgvInventory.Columns["Price"].HeaderText = "Price";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading inventory data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvInventory_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvInventory.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvInventory.SelectedRows[0];
                txtItemName.Text = row.Cells["ItemName"].Value?.ToString() ?? "";
                txtQuantity.Text = row.Cells["Quantity"].Value?.ToString() ?? "";
                txtPrice.Text = row.Cells["Price"].Value?.ToString() ?? "";
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtItemName.Text))
                {
                    MessageBox.Show("Please enter item name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity < 0)
                {
                    MessageBox.Show("Please enter a valid quantity.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtPrice.Text, out decimal price) || price < 0)
                {
                    MessageBox.Show("Please enter a valid price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Inventory (ItemName, Quantity, Price) VALUES (@name, @quantity, @price)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", txtItemName.Text);
                        command.Parameters.AddWithValue("@quantity", quantity);
                        command.Parameters.AddWithValue("@price", price);

                        if (command.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Item added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadInventoryData();
                            ClearInputs();
                        }
                        else
                        {
                            MessageBox.Show("Failed to add item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvInventory.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select an item to update.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtItemName.Text))
                {
                    MessageBox.Show("Please enter item name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity < 0)
                {
                    MessageBox.Show("Please enter a valid quantity.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtPrice.Text, out decimal price) || price < 0)
                {
                    MessageBox.Show("Please enter a valid price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int itemId = Convert.ToInt32(dgvInventory.SelectedRows[0].Cells["ItemID"].Value);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Inventory SET ItemName = @name, Quantity = @quantity, Price = @price WHERE ItemID = @id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", txtItemName.Text);
                        command.Parameters.AddWithValue("@quantity", quantity);
                        command.Parameters.AddWithValue("@price", price);
                        command.Parameters.AddWithValue("@id", itemId);

                        if (command.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Item updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadInventoryData();
                            ClearInputs();
                        }
                        else
                        {
                            MessageBox.Show("Failed to update item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvInventory.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select an item to delete.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("Are you sure you want to delete this item?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    int itemId = Convert.ToInt32(dgvInventory.SelectedRows[0].Cells["ItemID"].Value);

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM Inventory WHERE ItemID = @id";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@id", itemId);

                            if (command.ExecuteNonQuery() > 0)
                            {
                                MessageBox.Show("Item deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadInventoryData();
                                ClearInputs();
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string searchTerm = txtSearch.Text.Trim();

                if (string.IsNullOrEmpty(searchTerm))
                {
                    LoadInventoryData();
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT ItemID, ItemName, Quantity, Price FROM Inventory WHERE ItemName LIKE @search";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@search", "%" + searchTerm + "%");
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dgvInventory.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching items: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadInventoryData();
            ClearInputs();
            txtSearch.Text = "";
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearInputs()
        {
            txtItemName.Text = "";
            txtQuantity.Text = "";
            txtPrice.Text = "";
        }

        private void InventoryForm_Load(object sender, EventArgs e)
        {

        }
    }
}