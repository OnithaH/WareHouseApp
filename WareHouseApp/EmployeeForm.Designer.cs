using System.Drawing;
using System.Windows.Forms;

namespace WareHouseApp
{
    partial class EmployeeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvEmployees = new DataGridView();
            this.txtEmployeeName = new TextBox();
            this.txtPosition = new TextBox();
            this.txtSalary = new TextBox();
            this.txtSearch = new TextBox();
            this.btnAdd = new Button();
            this.btnUpdate = new Button();
            this.btnDelete = new Button();
            this.btnSearch = new Button();
            this.btnRefresh = new Button();
            this.btnClose = new Button();
            this.lblEmployeeName = new Label();
            this.lblPosition = new Label();
            this.lblSalary = new Label();
            this.lblSearch = new Label();
            this.lblTitle = new Label();

            this.SuspendLayout();

            // Form properties
            this.Text = "Employee Management";
            this.Size = new Size(900, 650);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Title
            this.lblTitle.Text = "Employee Management System";
            this.lblTitle.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold);
            this.lblTitle.Location = new Point(300, 20);
            this.lblTitle.Size = new Size(350, 30);

            // DataGridView
            this.dgvEmployees.Location = new Point(30, 70);
            this.dgvEmployees.Size = new Size(550, 400);
            this.dgvEmployees.AllowUserToAddRows = false;
            this.dgvEmployees.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvEmployees.MultiSelect = false;
            this.dgvEmployees.SelectionChanged += DgvEmployees_SelectionChanged;

            // Labels and TextBoxes
            this.lblEmployeeName.Text = "Employee Name:";
            this.lblEmployeeName.Location = new Point(600, 90);
            this.lblEmployeeName.Size = new Size(100, 20);

            this.txtEmployeeName.Location = new Point(720, 88);
            this.txtEmployeeName.Size = new Size(150, 23);

            this.lblPosition.Text = "Position:";
            this.lblPosition.Location = new Point(600, 130);
            this.lblPosition.Size = new Size(100, 20);

            this.txtPosition.Location = new Point(720, 128);
            this.txtPosition.Size = new Size(150, 23);

            this.lblSalary.Text = "Salary:";
            this.lblSalary.Location = new Point(600, 170);
            this.lblSalary.Size = new Size(100, 20);

            this.txtSalary.Location = new Point(720, 168);
            this.txtSalary.Size = new Size(150, 23);

            // Search
            this.lblSearch.Text = "Search:";
            this.lblSearch.Location = new Point(30, 500);
            this.lblSearch.Size = new Size(60, 20);

            this.txtSearch.Location = new Point(100, 498);
            this.txtSearch.Size = new Size(200, 23);

            // Buttons
            this.btnAdd.Text = "Add";
            this.btnAdd.Location = new Point(620, 220);
            this.btnAdd.Size = new Size(80, 30);
            this.btnAdd.BackColor = Color.LightGreen;
            this.btnAdd.Click += BtnAdd_Click;

            this.btnUpdate.Text = "Update";
            this.btnUpdate.Location = new Point(720, 220);
            this.btnUpdate.Size = new Size(80, 30);
            this.btnUpdate.BackColor = Color.LightBlue;
            this.btnUpdate.Click += BtnUpdate_Click;

            this.btnDelete.Text = "Delete";
            this.btnDelete.Location = new Point(820, 220);
            this.btnDelete.Size = new Size(80, 30);
            this.btnDelete.BackColor = Color.LightCoral;
            this.btnDelete.Click += BtnDelete_Click;

            this.btnSearch.Text = "Search";
            this.btnSearch.Location = new Point(320, 496);
            this.btnSearch.Size = new Size(80, 30);
            this.btnSearch.BackColor = Color.LightYellow;
            this.btnSearch.Click += BtnSearch_Click;

            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Location = new Point(420, 496);
            this.btnRefresh.Size = new Size(80, 30);
            this.btnRefresh.BackColor = Color.LightGray;
            this.btnRefresh.Click += BtnRefresh_Click;

            this.btnClose.Text = "Close";
            this.btnClose.Location = new Point(520, 496);
            this.btnClose.Size = new Size(80, 30);
            this.btnClose.BackColor = Color.LightPink;
            this.btnClose.Click += BtnClose_Click;

            // Add controls to form
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.dgvEmployees);
            this.Controls.Add(this.lblEmployeeName);
            this.Controls.Add(this.txtEmployeeName);
            this.Controls.Add(this.lblPosition);
            this.Controls.Add(this.txtPosition);
            this.Controls.Add(this.lblSalary);
            this.Controls.Add(this.txtSalary);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnClose);

            this.ResumeLayout(false);
        }

        #endregion
    }
}