using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem
{
    public partial class OrderForm : Form
    {
        private BindingList<OrderItem> _orderItems;
        public OrderCreateRequest OrderRequest { get; private set; }
        private const decimal TAX_RATE = 0.10m; // 10% tax

        public OrderForm()
        {
            InitializeComponent();
            InitializeData();
            WireEvents();
        }

        private void InitializeData()
        {
            _orderItems = new BindingList<OrderItem>();
            gridOrderItems.DataSource = _orderItems;
            ConfigureGrid();
            UpdateSummary();
            UpdateTableVisibility();
        }

        private void WireEvents()
        {
            // Button events
            btnAddItem.Click += btnAddItem_Click;
            btnEditItem.Click += btnEditItem_Click;
            btnRemoveItem.Click += btnRemoveItem_Click;
            btnSave.Click += btnSave_Click;
            btnCancel.Click += btnCancel_Click;

            // Other events
            cmbOrderType.SelectedIndexChanged += cmbOrderType_SelectedIndexChanged;
            _orderItems.ListChanged += OrderItems_ListChanged;
            gridOrderItems.SelectionChanged += GridOrderItems_SelectionChanged;
        }

        private void ConfigureGrid()
        {
            gridOrderItems.AutoGenerateColumns = false;
            gridOrderItems.Columns.Clear();

            // Item name column
            gridOrderItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MenuItem.Name", // Updated to access Name property
                HeaderText = "Menu Item",
                Name = "MenuItem",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = new DataGridViewCellStyle { Padding = new Padding(5) }
            });

            // Rest of the columns remain the same...
            // Quantity column
            gridOrderItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Quantity",
                HeaderText = "Qty",
                Name = "Quantity",
                Width = 60,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            // Price column
            gridOrderItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Price",
                HeaderText = "Unit Price",
                Name = "Price",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleRight },
                ReadOnly = true
            });

            // Total column (calculated)
            var totalColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Total",
                Name = "Total",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleRight },
                ReadOnly = true
            };
            gridOrderItems.Columns.Add(totalColumn);

            // Instructions column
            gridOrderItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SpecialInstructions",
                HeaderText = "Special Instructions",
                Name = "Instructions",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = new DataGridViewCellStyle { Font = new Font("Segoe UI", 8F), ForeColor = Color.Gray }
            });
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (var itemForm = new OrderItemForm())
                {
                    if (itemForm.ShowDialog() == DialogResult.OK && itemForm.OrderItem != null)
                    {
                        _orderItems.Add(itemForm.OrderItem);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding item: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            if (gridOrderItems.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an item to edit.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var selectedIndex = gridOrderItems.SelectedRows[0].Index;
                if (selectedIndex >= 0 && selectedIndex < _orderItems.Count)
                {
                    var itemToEdit = _orderItems[selectedIndex];
                    using (var itemForm = new OrderItemForm(itemToEdit))
                    {
                        if (itemForm.ShowDialog() == DialogResult.OK && itemForm.OrderItem != null)
                        {
                            _orderItems[selectedIndex] = itemForm.OrderItem;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing item: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (gridOrderItems.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an item to remove.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedIndex = gridOrderItems.SelectedRows[0].Index;
            if (selectedIndex >= 0 && selectedIndex < _orderItems.Count)
            {
                var item = _orderItems[selectedIndex];
                var itemName = item.MenuItem?.Name ?? "Unknown Item";
                var result = MessageBox.Show(
                    $"Remove '{itemName}' from the order?",
                    "Confirm Removal",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    _orderItems.RemoveAt(selectedIndex);
                }
            }
        }

        private void GridOrderItems_SelectionChanged(object sender, EventArgs e)
        {
            bool hasSelection = gridOrderItems.SelectedRows.Count > 0;
            btnEditItem.Enabled = hasSelection;
            btnRemoveItem.Enabled = hasSelection;
        }

        private void OrderItems_ListChanged(object sender, ListChangedEventArgs e)
        {
            UpdateSummary();
            UpdateButtonStates();
        }

        private void UpdateSummary()
        {
            decimal subtotal = _orderItems.Sum(item => item.Price * item.Quantity);
            decimal tax = subtotal * TAX_RATE;
            decimal total = subtotal + tax;

            lblSubtotalValue.Text = $"{subtotal:C2}";
            lblTaxValue.Text = $"{tax:C2}";
            lblTotalValue.Text = $"{total:C2}";

            // Update the calculated total column in the grid
            foreach (DataGridViewRow row in gridOrderItems.Rows)
            {
                if (row.DataBoundItem is OrderItem item)
                {
                    row.Cells["Total"].Value = item.Price * item.Quantity;
                }
            }
        }

        private void UpdateButtonStates()
        {
            bool hasItems = _orderItems.Count > 0;
            btnSave.Enabled = hasItems && !string.IsNullOrWhiteSpace(txtCustomer.Text);

            // Visual feedback for save button
            btnSave.BackColor = btnSave.Enabled ? Color.SteelBlue : Color.Gray;
        }

        private void cmbOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTableVisibility();
        }

        private void UpdateTableVisibility()
        {
            bool isDineIn = cmbOrderType.SelectedItem?.ToString() == "dine-in";
            lblTable.Visible = isDineIn;
            numTableNumber.Visible = isDineIn;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                CreateOrderRequest();
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to cancel? All unsaved changes will be lost.",
                "Confirm Cancel",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void CreateOrderRequest()
        {
            decimal subtotal = _orderItems.Sum(item => item.Price * item.Quantity);
            decimal tax = subtotal * TAX_RATE;

            OrderRequest = new OrderCreateRequest
            {
                Customer = txtCustomer.Text.Trim(),
                OrderType = cmbOrderType.SelectedItem?.ToString() ?? "dine-in",
                TableNumber = cmbOrderType.SelectedItem?.ToString() == "dine-in"
                    ? (int?)numTableNumber.Value
                    : null,
                Items = _orderItems.ToList(),
                TotalAmount = subtotal + tax
            };
        }

        private bool ValidateForm()
        {
            // Validate customer name
            if (string.IsNullOrWhiteSpace(txtCustomer.Text))
            {
                MessageBox.Show("Please enter customer name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCustomer.Focus();
                return false;
            }

            // Validate order type
            if (cmbOrderType.SelectedItem == null)
            {
                MessageBox.Show("Please select an order type.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbOrderType.Focus();
                return false;
            }

            // Validate items
            if (_orderItems.Count == 0)
            {
                MessageBox.Show("Please add at least one item to the order.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnAddItem.Focus();
                return false;
            }

            // Validate table number for dine-in
            if (cmbOrderType.SelectedItem?.ToString() == "dine-in" && numTableNumber.Value < 1)
            {
                MessageBox.Show("Please enter a valid table number.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numTableNumber.Focus();
                return false;
            }

            return true;
        }

        // Real-time validation as user types
        private void txtCustomer_TextChanged(object sender, EventArgs e)
        {
            UpdateButtonStates();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            txtCustomer.Focus();
        }

        private void OrderForm_Load(object sender, EventArgs e)
        {

        }
    }
}