using System;
using System.Drawing;
using System.Windows.Forms;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem
{
    public partial class OrderItemForm : Form
    {
        public OrderItem OrderItem { get; private set; }

        public OrderItemForm()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // OrderItemForm
            // 
            ClientSize = new Size(350, 250);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "OrderItemForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Add Order Item";
            Load += OrderItemForm_Load;
            ResumeLayout(false);
        }

        private ComboBox cmbMenuItem;
        private NumericUpDown numQuantity;
        private TextBox txtSpecialInstructions;
        private Button btnAdd;
        private Button btnCancel;

        private void InitializeForm()
        {
            var panel = new Panel { Dock = DockStyle.Fill };

            var y = 20;
            var labelWidth = 120;

            // Menu Item
            var lblMenuItem = new Label { Text = "Menu Item:", Location = new Point(20, y), Width = labelWidth };
            cmbMenuItem = new ComboBox
            {
                Location = new Point(140, y),
                Size = new Size(180, 23),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            y += 35;

            // Quantity
            var lblQuantity = new Label { Text = "Quantity:", Location = new Point(20, y), Width = labelWidth };
            numQuantity = new NumericUpDown { Location = new Point(140, y), Size = new Size(60, 23), Minimum = 1, Maximum = 50, Value = 1 };
            y += 35;

            // Special Instructions
            var lblInstructions = new Label { Text = "Special Instructions:", Location = new Point(20, y), Width = labelWidth };
            txtSpecialInstructions = new TextBox { Location = new Point(140, y), Size = new Size(180, 60), Multiline = true };
            y += 70;

            // Buttons
            btnAdd = new Button { Text = "Add", Location = new Point(80, y), Size = new Size(80, 30), DialogResult = DialogResult.OK };
            btnCancel = new Button { Text = "Cancel", Location = new Point(180, y), Size = new Size(80, 30), DialogResult = DialogResult.Cancel };

            btnAdd.Click += btnAdd_Click;

            // Load menu items (you would populate this from your data)
            // cmbMenuItem.Items.AddRange(menuItems);

            panel.Controls.AddRange(new Control[]
            {
                lblMenuItem, cmbMenuItem,
                lblQuantity, numQuantity,
                lblInstructions, txtSpecialInstructions,
                btnAdd, btnCancel
            });

            Controls.Add(panel);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                OrderItem = new OrderItem
                {
                    MenuItem = cmbMenuItem.SelectedItem?.ToString() ?? string.Empty,
                    Quantity = (int)numQuantity.Value,
                    SpecialInstructions = txtSpecialInstructions.Text.Trim(),
                    Price = 0 // You would calculate this based on the selected menu item
                };
                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidateForm()
        {
            if (cmbMenuItem.SelectedItem == null)
            {
                MessageBox.Show("Please select a menu item.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbMenuItem.Focus();
                return false;
            }

            return true;
        }

        private void OrderItemForm_Load(object sender, EventArgs e)
        {

        }
    }
}