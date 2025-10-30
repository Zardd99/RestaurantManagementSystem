using Microsoft.Extensions.DependencyInjection;
using RestaurantManagementSystem.Forms;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Services;
using System;
using System.Windows.Forms;

namespace RestaurantManagementSystem
{
    public partial class MainForm : Form
    {
        private readonly IApiClient _apiClient;
        public User CurrentUser { get; set; }

        public MainForm(IApiClient apiClient)
        {
            _apiClient = apiClient;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateStatusBar();
        }

        private void UpdateStatusBar()
        {
            if (CurrentUser != null)
            {
                statusLabel.Text = $"Logged in as: {CurrentUser.Name} ({CurrentUser.Role})";
            }
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var usersForm = Program.ServiceProvider.GetService<UsersForm>();
            usersForm.ShowDialog();
        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var menuForm = Program.ServiceProvider.GetService<MenuForm>();
            menuForm.ShowDialog();
        }

        private void ordersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ordersForm = Program.ServiceProvider.GetService<OrdersForm>();
            ordersForm.ShowDialog();
        }

        private void reviewsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var reviewsForm = Program.ServiceProvider.GetService<ReviewsForm>();
            reviewsForm.ShowDialog();
        }

        private void suppliersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var suppliersForm = Program.ServiceProvider.GetService<SuppliersForm>();
            suppliersForm.ShowDialog();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to logout?", "Logout",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _apiClient.Token = null;
                CurrentUser = null;

                var loginForm = Program.ServiceProvider.GetService<LoginForm>();
                loginForm.Show();
                this.Close();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && CurrentUser != null)
            {
                var result = MessageBox.Show("Are you sure you want to exit?", "Exit",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}