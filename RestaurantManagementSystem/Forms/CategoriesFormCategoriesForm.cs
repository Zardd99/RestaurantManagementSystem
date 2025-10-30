using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RestaurantManagementSystem.Managers;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.Forms
{
    public partial class CategoriesForm : Form
    {
        private readonly CategoryManager _categoryManager;
        private List<Category> _categories;
        private BindingList<Category> _categoryViewModels;

        public CategoriesForm(CategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
            InitializeComponent();
            InitializeDataGridView();
            LoadCategories();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "CategoriesForm";
            this.Text = "Category Management";
            this.ResumeLayout(false);
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

        private async void LoadCategories()
        {
            try
            {
                _categories = await _categoryManager.GetCategoriesAsync();
                _categoryViewModels = new BindingList<Category>(_categories);
                dataGridView.DataSource = _categoryViewModels;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Implementation for adding category
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Implementation for editing category
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0) return;

            var selectedCategory = dataGridView.SelectedRows[0].DataBoundItem as Category;
            if (selectedCategory != null)
            {
                var result = MessageBox.Show($"Delete category {selectedCategory.Name}?", "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        await _categoryManager.DeleteCategoryAsync(selectedCategory.Id);
                        LoadCategories();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting category: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCategories();
        }
    }
}