using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RestaurantManagementSystem.Managers;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.ViewModels;

namespace RestaurantManagementSystem
{
    public partial class UsersForm : Form
    {
        private readonly UserManager _userManager;
        private List<User> _users;
        private BindingList<UserViewModel> _userViewModels;

        public UsersForm(UserManager userManager)
        {
            _userManager = userManager;
            InitializeComponent();
            InitializeDataGridView();
            LoadUsers();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            ClientSize = new Size(800, 450);
            Name = "UsersForm";
            Text = "User Management";
            Load += UsersForm_Load;
            ResumeLayout(false);
        }

        private DataGridView dataGridView;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnRefresh;

        private void InitializeDataGridView()
        {
            dataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            // Configure columns
            dataGridView.Columns.Clear();
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Name",
                HeaderText = "Name",
                Name = "Name"
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Email",
                HeaderText = "Email",
                Name = "Email"
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Role",
                HeaderText = "Role",
                Name = "Role"
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Phone",
                HeaderText = "Phone",
                Name = "Phone"
            });
            dataGridView.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "IsActive",
                HeaderText = "Active",
                Name = "IsActive"
            });

            var panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40
            };

            btnAdd = new Button { Text = "Add", Location = new Point(10, 10), Size = new Size(75, 23) };
            btnEdit = new Button { Text = "Edit", Location = new Point(90, 10), Size = new Size(75, 23) };
            btnDelete = new Button { Text = "Delete", Location = new Point(170, 10), Size = new Size(75, 23) };
            btnRefresh = new Button { Text = "Refresh", Location = new Point(250, 10), Size = new Size(75, 23) };

            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnDelete.Click += btnDelete_Click;
            btnRefresh.Click += btnRefresh_Click;

            panel.Controls.AddRange(new Control[] { btnAdd, btnEdit, btnDelete, btnRefresh });

            Controls.Add(dataGridView);
            Controls.Add(panel);
        }

        private async void LoadUsers()
        {
            try
            {
                _users = await _userManager.GetUsersAsync();
                _userViewModels = new BindingList<UserViewModel>(
                    _users.Select(UserViewModel.FromModel).ToList()
                );
                dataGridView.DataSource = _userViewModels;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Implementation for adding user
            MessageBox.Show("Add user functionality would be implemented here", "Add User",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user to edit.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Implementation for editing user
            var selectedUser = dataGridView.SelectedRows[0].DataBoundItem as UserViewModel;
            MessageBox.Show($"Edit user {selectedUser?.Name} functionality would be implemented here", "Edit User",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0) return;

            var selectedUser = dataGridView.SelectedRows[0].DataBoundItem as UserViewModel;
            if (selectedUser != null)
            {
                var result = MessageBox.Show($"Delete user {selectedUser.Name}?", "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        await _userManager.DeleteUserAsync(selectedUser.Id);
                        LoadUsers();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting user: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void UsersForm_Load(object sender, EventArgs e)
        {

        }
    }
}