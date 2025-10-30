using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RestaurantManagementSystem.Managers;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem
{
    public partial class SuppliersForm : Form
    {
        private readonly SupplierManager _supplierManager;
        private List<Supplier> _suppliers;
        private BindingList<Supplier> _supplierViewModels;

        public SuppliersForm(SupplierManager supplierManager)
        {
            _supplierManager = supplierManager;
            InitializeComponent();
            InitializeDataGridView();
            LoadSuppliers();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // SuppliersForm
            // 
            ClientSize = new Size(800, 450);
            Name = "SuppliersForm";
            Text = "Supplier Management";
            Load += SuppliersForm_Load;
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

        private async void LoadSuppliers()
        {
            try
            {
                _suppliers = await _supplierManager.GetSuppliersAsync();
                _supplierViewModels = new BindingList<Supplier>(_suppliers);
                dataGridView.DataSource = _supplierViewModels;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading suppliers: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Implementation for adding supplier
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Implementation for editing supplier
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0) return;

            var selectedSupplier = dataGridView.SelectedRows[0].DataBoundItem as Supplier;
            if (selectedSupplier != null)
            {
                var result = MessageBox.Show($"Delete supplier {selectedSupplier.Name}?", "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        await _supplierManager.DeleteSupplierAsync(selectedSupplier.Id);
                        LoadSuppliers();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting supplier: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadSuppliers();
        }

        private void SuppliersForm_Load(object sender, EventArgs e)
        {

        }
    }
}