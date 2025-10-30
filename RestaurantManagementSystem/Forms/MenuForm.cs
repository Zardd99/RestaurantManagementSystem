using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Services;

namespace RestaurantManagementSystem
{
    public partial class MenuForm : Form
    {
        private readonly IApiClient _apiClient;
        private List<MenuItem> _menuItems;
        private BindingSource _bindingSource;

        public MenuForm(IApiClient apiClient)
        {
            _apiClient = apiClient;
            InitializeComponent();
            InitializeDataGridView();
            LoadMenuItems();
        }

        private void InitializeComponent()
        {
            dataGridView = new DataGridView();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnRefresh = new Button();
            txtSearch = new TextBox();
            lblSearch = new Label();
            panelControls = new Panel();
            ((ISupportInitialize)dataGridView).BeginInit();
            panelControls.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.Location = new Point(12, 50);
            dataGridView.Name = "dataGridView";
            dataGridView.ReadOnly = true;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.Size = new Size(760, 350);
            dataGridView.TabIndex = 0;
            dataGridView.CellContentClick += dataGridView_CellContentClick;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(12, 8);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 23);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(93, 8);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(75, 23);
            btnEdit.TabIndex = 1;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(174, 8);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(255, 8);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(75, 23);
            btnRefresh.TabIndex = 3;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(450, 10);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(200, 23);
            txtSearch.TabIndex = 4;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(400, 13);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(45, 15);
            lblSearch.TabIndex = 5;
            lblSearch.Text = "Search:";
            // 
            // panelControls
            // 
            panelControls.Controls.Add(lblSearch);
            panelControls.Controls.Add(txtSearch);
            panelControls.Controls.Add(btnRefresh);
            panelControls.Controls.Add(btnDelete);
            panelControls.Controls.Add(btnEdit);
            panelControls.Controls.Add(btnAdd);
            panelControls.Dock = DockStyle.Top;
            panelControls.Location = new Point(0, 0);
            panelControls.Name = "panelControls";
            panelControls.Size = new Size(784, 40);
            panelControls.TabIndex = 1;
            // 
            // MenuForm
            // 
            ClientSize = new Size(784, 411);
            Controls.Add(panelControls);
            Controls.Add(dataGridView);
            Name = "MenuForm";
            Text = "Menu Management";
            ((ISupportInitialize)dataGridView).EndInit();
            panelControls.ResumeLayout(false);
            panelControls.PerformLayout();
            ResumeLayout(false);
        }

        private DataGridView dataGridView;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnRefresh;
        private TextBox txtSearch;
        private Label lblSearch;
        private Panel panelControls;

        private void InitializeDataGridView()
        {
            _bindingSource = new BindingSource();
            dataGridView.DataSource = _bindingSource;
        }

        private async void LoadMenuItems()
        {
            try
            {
                _menuItems = await _apiClient.GetMenuItemsAsync();
                _bindingSource.DataSource = _menuItems;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading menu items: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new MenuItemForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    await _apiClient.CreateMenuItemAsync(form.MenuItem);
                    LoadMenuItems();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error creating menu item: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a menu item to edit.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedItem = dataGridView.SelectedRows[0].DataBoundItem as MenuItem;
            if (selectedItem != null)
            {
                var form = new MenuItemForm(selectedItem);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        await _apiClient.UpdateMenuItemAsync(selectedItem.Id, form.MenuItem);
                        LoadMenuItems();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error updating menu item: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a menu item to delete.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedItem = dataGridView.SelectedRows[0].DataBoundItem as MenuItem;
            if (selectedItem != null)
            {
                var result = MessageBox.Show($"Are you sure you want to delete '{selectedItem.Name}'?",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        await _apiClient.DeleteMenuItemAsync(selectedItem.Id);
                        LoadMenuItems();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting menu item: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadMenuItems();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                _bindingSource.DataSource = _menuItems;
            }
            else
            {
                var filtered = _menuItems.Where(m =>
                    (m.Name?.Contains(txtSearch.Text, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (m.Description?.Contains(txtSearch.Text, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (m.CategoryName?.Contains(txtSearch.Text, StringComparison.OrdinalIgnoreCase) ?? false)
                ).ToList();
                _bindingSource.DataSource = filtered;
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}