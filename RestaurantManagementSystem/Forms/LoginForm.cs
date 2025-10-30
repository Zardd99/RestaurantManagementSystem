using System;
using System.Windows.Forms;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Services;

namespace RestaurantManagementSystem.Forms
{
    public partial class LoginForm : Form
    {
        private readonly IApiClient _apiClient;

        public LoginForm(IApiClient apiClient)
        {
            _apiClient = apiClient;
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var request = new LoginRequest
                {
                    Email = txtEmail.Text.Trim(),
                    Password = txtPassword.Text
                };

                var response = await _apiClient.LoginAsync(request);

                if (response.Success)
                {
                    _apiClient.Token = response.Token;

                    var mainForm = Program.ServiceProvider.GetService<MainForm>();
                    mainForm.CurrentUser = response.User;
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Login failed. Please check your credentials.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}