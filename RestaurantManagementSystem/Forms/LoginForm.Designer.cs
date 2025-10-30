namespace RestaurantManagementSystem.Forms
{
    partial class LoginForm : Form
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtEmail;
        private TextBox txtPassword;
        private Button btnLogin;
        private Label lblEmail;
        private Label lblPassword;
        private Label lblTitle;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblEmail = new Label();
            this.txtEmail = new TextBox();
            this.lblPassword = new Label();
            this.txtPassword = new TextBox();
            this.btnLogin = new Button();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold);
            this.lblTitle.Location = new Point(80, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(240, 26);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Restaurant Management";
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // lblEmail
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new Point(50, 90);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new Size(35, 13);
            this.lblEmail.TabIndex = 1;
            this.lblEmail.Text = "Email:";

            // txtEmail
            this.txtEmail.Location = new Point(120, 87);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new Size(200, 20);
            this.txtEmail.TabIndex = 2;

            // lblPassword
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new Point(50, 130);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new Size(56, 13);
            this.lblPassword.TabIndex = 3;
            this.lblPassword.Text = "Password:";

            // txtPassword
            this.txtPassword.Location = new Point(120, 127);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new Size(200, 20);
            this.txtPassword.TabIndex = 4;

            // btnLogin
            this.btnLogin.Location = new Point(120, 170);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new Size(120, 30);
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new EventHandler(this.btnLogin_Click);

            // LoginForm
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(384, 241);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}