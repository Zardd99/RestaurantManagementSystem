using RestaurantManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace RestaurantManagementSystem
{
    public partial class OrderForm : Form
    {
        public OrderCreateRequest OrderRequest { get; private set; }

        public OrderForm()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            var itemForm = new OrderItemForm();
            if (itemForm.ShowDialog() == DialogResult.OK)
            {
                // TODO: Add item to grid
            }
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (gridOrderItems.SelectedRows.Count > 0)
            {
                gridOrderItems.Rows.RemoveAt(gridOrderItems.SelectedRows[0].Index);
                UpdateTotal();
            }
        }

        private void UpdateTotal()
        {
            decimal total = 0;
            // TODO: Calculate total from grid items
            lblTotal.Text = $"Total: ${total:F2}";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                OrderRequest = new OrderCreateRequest
                {
                    Customer = txtCustomer.Text,
                    OrderType = cmbOrderType.SelectedItem.ToString(),
                    TableNumber = cmbOrderType.SelectedItem.ToString() == "dine-in" ? (int?)numTableNumber.Value : null,
                    // TODO: Items should be populated from grid
                };
                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtCustomer.Text))
            {
                MessageBox.Show("Please enter customer information", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void OrderForm_Load(object sender, EventArgs e)
        {
            // Optional: additional initialization logic
        }
    }
}
