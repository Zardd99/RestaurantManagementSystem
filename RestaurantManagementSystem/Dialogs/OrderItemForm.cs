using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem
{
    public partial class OrderItemForm : Form
    {
        public OrderItem OrderItem { get; private set; }
        private List<MenuItem> _menuItems;
        private OrderItem _existingItem;
        private bool _isEditing;

        // Constructor for adding new item
        public OrderItemForm(List<MenuItem> menuItems = null)
        {
            _menuItems = menuItems ?? new List<MenuItem>();
            _isEditing = false;
            InitializeComponent();
            InitializeData();
            WireEvents();
        }

        // Constructor for editing existing item
        public OrderItemForm(OrderItem existingItem, List<MenuItem> menuItems = null)
        {
            _existingItem = existingItem;
            _menuItems = menuItems ?? new List<MenuItem>();
            _isEditing = true;
            InitializeComponent();
            InitializeData();
            WireEvents();
            LoadExistingItem();
        }

        private void InitializeData()
        {
            // Populate menu items combo box
            cmbMenuItem.Items.Clear();
            cmbMenuItem.Items.AddRange(_menuItems.Select(m => m.Name).ToArray());

            if (_menuItems.Any())
            {
                cmbMenuItem.SelectedIndex = 0;
            }

            UpdatePrices();

            // Update UI for editing mode
            if (_isEditing)
            {
                this.Text = "Edit Order Item";
                btnSave.Text = "💾 Update Item";
            }
        }

        private void WireEvents()
        {
            btnSave.Click += btnSave_Click;
            btnCancel.Click += btnCancel_Click;
            cmbMenuItem.SelectedIndexChanged += cmbMenuItem_SelectedIndexChanged;
            numQuantity.ValueChanged += numQuantity_ValueChanged;
            txtSpecialInstructions.TextChanged += txtSpecialInstructions_TextChanged;
        }

        private void LoadExistingItem()
        {
            if (_existingItem == null) return;

            // Find and select the menu item
            if (_existingItem.MenuItem != null)
            {
                var menuItem = _menuItems.FirstOrDefault(m => m.Id == _existingItem.MenuItem.Id);
                if (menuItem != null)
                {
                    cmbMenuItem.SelectedItem = menuItem.Name;
                }
                else
                {
                    // If menu item not found, just show the name from the existing item
                    cmbMenuItem.Items.Add(_existingItem.MenuItem.Name);
                    cmbMenuItem.SelectedIndex = cmbMenuItem.Items.Count - 1;
                }

                numQuantity.Value = _existingItem.Quantity;
                txtSpecialInstructions.Text = _existingItem.SpecialInstructions;

                // Use the price from the existing item
                lblUnitPriceValue.Text = $"{_existingItem.Price:C2}";
                UpdateTotalPrice();
            }
        }

        private void cmbMenuItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePrices();
        }

        private void numQuantity_ValueChanged(object sender, EventArgs e)
        {
            UpdateTotalPrice();
        }

        private void txtSpecialInstructions_TextChanged(object sender, EventArgs e)
        {
            // Optional: Add character count or validation
            if (txtSpecialInstructions.Text.Length > 200)
            {
                txtSpecialInstructions.BackColor = Color.LightPink;
            }
            else
            {
                txtSpecialInstructions.BackColor = Color.White;
            }
        }

        private void UpdatePrices()
        {
            if (cmbMenuItem.SelectedItem == null) return;

            var selectedItemName = cmbMenuItem.SelectedItem.ToString();
            var menuItem = _menuItems.FirstOrDefault(m => m.Name == selectedItemName);

            if (menuItem != null)
            {
                lblUnitPriceValue.Text = $"{menuItem.Price:C2}";
                UpdateTotalPrice();
            }
        }

        private void UpdateTotalPrice()
        {
            if (decimal.TryParse(lblUnitPriceValue.Text.Replace("$", "").Replace(",", ""), out decimal unitPrice))
            {
                decimal total = unitPrice * numQuantity.Value;
                lblTotalPriceValue.Text = $"{total:C2}";
            }
            else
            {
                lblTotalPriceValue.Text = "$0.00";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                CreateOrderItem();
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to cancel? All changes will be lost.",
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

        private void CreateOrderItem()
        {
            var selectedItemName = cmbMenuItem.SelectedItem.ToString();
            var menuItem = _menuItems.FirstOrDefault(m => m.Name == selectedItemName);

            decimal unitPrice = 0m;
            MenuItem selectedMenuItem = null;

            if (menuItem != null)
            {
                unitPrice = menuItem.Price;
                selectedMenuItem = menuItem;
            }
            else if (_isEditing && _existingItem?.MenuItem != null)
            {
                // For editing, keep the original menu item and price
                unitPrice = _existingItem.Price;
                selectedMenuItem = _existingItem.MenuItem;
            }

            OrderItem = new OrderItem
            {
                MenuItem = selectedMenuItem,
                Quantity = (int)numQuantity.Value,
                SpecialInstructions = txtSpecialInstructions.Text.Trim(),
                Price = unitPrice
            };
        }

        private bool ValidateForm()
        {
            // Validate menu item selection
            if (cmbMenuItem.SelectedItem == null)
            {
                MessageBox.Show("Please select a menu item.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbMenuItem.Focus();
                return false;
            }

            // Validate quantity
            if (numQuantity.Value < 1)
            {
                MessageBox.Show("Please enter a valid quantity (at least 1).", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numQuantity.Focus();
                return false;
            }

            // Validate special instructions length
            if (txtSpecialInstructions.Text.Length > 500)
            {
                MessageBox.Show("Special instructions cannot exceed 500 characters.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSpecialInstructions.Focus();
                return false;
            }

            return true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Auto-focus the first control
            if (cmbMenuItem.Items.Count > 0)
            {
                cmbMenuItem.Focus();
            }
            else
            {
                numQuantity.Focus();
            }
        }

        // Public method to update menu items (can be called from parent form)
        public void UpdateMenuItems(List<MenuItem> menuItems)
        {
            _menuItems = menuItems ?? new List<MenuItem>();

            var currentSelection = cmbMenuItem.SelectedItem;
            cmbMenuItem.Items.Clear();
            cmbMenuItem.Items.AddRange(_menuItems.Select(m => m.Name).ToArray());

            // Restore selection if possible
            if (currentSelection != null && cmbMenuItem.Items.Contains(currentSelection))
            {
                cmbMenuItem.SelectedItem = currentSelection;
            }
            else if (cmbMenuItem.Items.Count > 0)
            {
                cmbMenuItem.SelectedIndex = 0;
            }

            UpdatePrices();
        }

        private void OrderItemForm_Load(object sender, EventArgs e)
        {

        }
    }
}