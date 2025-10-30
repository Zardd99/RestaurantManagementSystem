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
            this.dataGridView = new DataGridView();
            this.btnAdd = new Button();
            this.btnEdit = new Button();
            this.btnDelete = new Button();
            this.btnRefresh = new Button();
            this.txtSearch = new TextBox();
            this.lblSearch = new Label();
            this.panelControls = new Panel();

            ((ISupportInitialize)this.dataGridView).BeginInit();
            this.panelControls.SuspendLayout();
            this.SuspendLayout();

            // dataGridView
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.Location = new Point(12, 50);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new Size(760, 350);
            this.dataGridView.TabIndex = 0;

            // panelControls
            this.panelControls.Controls.Add(this.lblSearch);
            this.panelControls.Controls.Add(this.txtSearch);
            this.panelControls.Controls.Add(this.btnRefresh);
            this.panelControls.Controls.Add(this.btnDelete);
            this.panelControls.Controls.Add(this.btnEdit);
            this.panelControls.Controls.Add(this.btnAdd);
            this.panelControls.Dock = DockStyle.Top;
            this.panelControls.Location = new Point(0, 0);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new Size(784, 40);
            this.panelControls.TabIndex = 1;

            // btnAdd
            this.btnAdd.Location = new Point(12, 8);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(75, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);

            // btnEdit
            this.btnEdit.Location = new Point(93, 8);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new Size(75, 23);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new EventHandler(this.btnEdit_Click);

            // btnDelete
            this.btnDelete.Location = new Point(174, 8);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(75, 23);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);

            // btnRefresh
            this.btnRefresh.Location = new Point(255, 8);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new Size(75, 23);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);

            // txtSearch
            this.txtSearch.Location = new Point(450, 10);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new Size(200, 20);
            this.txtSearch.TabIndex = 4;
            this.txtSearch.TextChanged += new EventHandler(this.txtSearch_TextChanged);

            // lblSearch
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new Point(400, 13);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new Size(44, 13);
            this.lblSearch.TabIndex = 5;
            this.lblSearch.Text = "Search:";

            // MenuForm
            this.ClientSize = new Size(784, 411);
            this.Controls.Add(this.panelControls);
            this.Controls.Add(this.dataGridView);
            this.Name = "MenuForm";
            this.Text = "Menu Management";
            ((ISupportInitialize)this.dataGridView).EndInit();
            this.panelControls.ResumeLayout(false);
            this.panelControls.PerformLayout();
            this.ResumeLayout(false);
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
                    m.Name.Contains(txtSearch.Text, StringComparison.OrdinalIgnoreCase) ||
                    m.Description.Contains(txtSearch.Text, StringComparison.OrdinalIgnoreCase) ||
                    m.Category.Contains(txtSearch.Text, StringComparison.OrdinalIgnoreCase)
                ).ToList();
                _bindingSource.DataSource = filtered;
            }
        }
    }
}