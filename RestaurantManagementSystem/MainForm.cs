using Microsoft.Extensions.DependencyInjection;
using RestaurantManagementSystem.Forms;
using RestaurantManagementSystem.Managers;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Services;
using System;
using System.Windows.Forms;

namespace RestaurantManagementSystem
{
    public partial class MainForm : Form
    {
        private readonly IApiClient _apiClient;
        private readonly UserManager _userManager;
        private readonly OrderManager _orderManager;
        private readonly ReviewManager _reviewManager;
        private readonly SupplierManager _supplierManager;
        private readonly ReceiptManager _receiptManager;
        private readonly CategoryManager _categoryManager;
        private readonly MenuItemManager _menuItemManager;

        public User CurrentUser { get; set; }

        public MainForm(IApiClient apiClient, UserManager userManager, OrderManager orderManager,
                       ReviewManager reviewManager, SupplierManager supplierManager,
                       ReceiptManager receiptManager, CategoryManager categoryManager,
                       MenuItemManager menuItemManager)
        {
            _apiClient = apiClient;
            _userManager = userManager;
            _orderManager = orderManager;
            _reviewManager = reviewManager;
            _supplierManager = supplierManager;
            _receiptManager = receiptManager;
            _categoryManager = categoryManager;
            _menuItemManager = menuItemManager;

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

        // Event Handlers for Menu Items
        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var usersForm = new UsersForm(_userManager);
                usersForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Users form: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var menuForm = new MenuForm(_apiClient); // Pass _apiClient instead of _menuItemManager
                menuForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Menu form: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ordersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var ordersForm = new OrdersForm(_orderManager);
                ordersForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Orders form: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void reviewsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var reviewsForm = new ReviewsForm(_reviewManager);
                reviewsForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Reviews form: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void suppliersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var suppliersForm = new SuppliersForm(_supplierManager);
                suppliersForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Suppliers form: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Missing event handlers that were causing errors
        private void receiptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var receiptsForm = new ReceiptsForm(_receiptManager);
                receiptsForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Receipts form: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void categoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var categoriesForm = new CategoriesForm(_categoryManager);
                categoriesForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Categories form: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Missing MainForm_Load event handler
        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdateStatusBar();

            // Additional initialization can go here
            if (CurrentUser != null)
            {
                lblWelcome.Text = $"Welcome {CurrentUser.Name} to Restaurant Management System";

                // Apply role-based access control
                ApplyRoleBasedAccess();
            }
        }

        private void ApplyRoleBasedAccess()
        {
            if (CurrentUser == null) return;

            // Example role-based access control
            switch (CurrentUser.Role.ToLower())
            {
                case "customer":
                    // Customers might have limited access
                    usersToolStripMenuItem.Visible = false;
                    suppliersToolStripMenuItem.Visible = false;
                    break;

                case "waiter":
                    // Waiters can manage orders and receipts
                    usersToolStripMenuItem.Visible = false;
                    suppliersToolStripMenuItem.Visible = false;
                    break;

                case "chef":
                    // Chefs can manage menu and orders
                    usersToolStripMenuItem.Visible = false;
                    suppliersToolStripMenuItem.Visible = false;
                    break;

                case "cashier":
                    // Cashiers can manage orders and receipts
                    usersToolStripMenuItem.Visible = false;
                    suppliersToolStripMenuItem.Visible = false;
                    break;

                    // Admin and Manager have full access by default
            }
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

        private void panelCards_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}