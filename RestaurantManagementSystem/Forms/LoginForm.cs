using Microsoft.Extensions.DependencyInjection;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Services;
using System;
using System.Windows.Forms;

namespace RestaurantManagementSystem.Forms
{
    public partial class LoginForm : Form
    {
        private readonly IApiClient _apiClient;
        private readonly IServiceProvider _serviceProvider;

        public LoginForm(IApiClient apiClient, IServiceProvider serviceProvider)
        {
            _apiClient = apiClient;
            _serviceProvider = serviceProvider;
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

                    var mainForm = _serviceProvider.GetService<MainForm>();
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

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}