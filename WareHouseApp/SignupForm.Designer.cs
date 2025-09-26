using System;
using System.Drawing;
using System.Windows.Forms;

namespace WareHouseApp
{
    partial class SignupForm
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
            this.txtUsername = new TextBox();
            this.txtPassword = new TextBox();
            this.txtConfirmPassword = new TextBox();
            this.cmbRole = new ComboBox();
            this.btnSignup = new Button();
            this.btnCancel = new Button();
            this.lblUsername = new Label();
            this.lblPassword = new Label();
            this.lblConfirmPassword = new Label();
            this.lblRole = new Label();
            this.lblTitle = new Label();
            this.SuspendLayout();

            // Form properties
            this.Text = "Sign Up";
            this.Size = new Size(400, 350);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Title
            this.lblTitle.Text = "Create New Account";
            this.lblTitle.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold);
            this.lblTitle.Location = new Point(100, 20);
            this.lblTitle.Size = new Size(200, 30);

            // Username
            this.lblUsername.Text = "Username:";
            this.lblUsername.Location = new Point(30, 70);
            this.lblUsername.Size = new Size(80, 20);

            this.txtUsername.Location = new Point(120, 68);
            this.txtUsername.Size = new Size(200, 23);

            // Password
            this.lblPassword.Text = "Password:";
            this.lblPassword.Location = new Point(30, 110);
            this.lblPassword.Size = new Size(80, 20);

            this.txtPassword.Location = new Point(120, 108);
            this.txtPassword.Size = new Size(200, 23);
            this.txtPassword.PasswordChar = '*';

            // Confirm Password
            this.lblConfirmPassword.Text = "Confirm:";
            this.lblConfirmPassword.Location = new Point(30, 150);
            this.lblConfirmPassword.Size = new Size(80, 20);

            this.txtConfirmPassword.Location = new Point(120, 148);
            this.txtConfirmPassword.Size = new Size(200, 23);
            this.txtConfirmPassword.PasswordChar = '*';

            // Role
            this.lblRole.Text = "Role:";
            this.lblRole.Location = new Point(30, 190);
            this.lblRole.Size = new Size(80, 20);

            this.cmbRole.Location = new Point(120, 188);
            this.cmbRole.Size = new Size(200, 23);
            this.cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbRole.Items.AddRange(new string[] { "User", "Admin" });
            this.cmbRole.SelectedIndex = 0;

            // Buttons
            this.btnSignup.Text = "Sign Up";
            this.btnSignup.Location = new Point(120, 240);
            this.btnSignup.Size = new Size(80, 30);
            this.btnSignup.BackColor = Color.LightBlue;
            this.btnSignup.Click += new EventHandler(this.btnSignup_Click);

            this.btnCancel.Text = "Cancel";
            this.btnCancel.Location = new Point(220, 240);
            this.btnCancel.Size = new Size(80, 30);
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            // Add controls to form
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblConfirmPassword);
            this.Controls.Add(this.txtConfirmPassword);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.cmbRole);
            this.Controls.Add(this.btnSignup);
            this.Controls.Add(this.btnCancel);

            this.ResumeLayout(false);
        }

        #endregion
    }
}