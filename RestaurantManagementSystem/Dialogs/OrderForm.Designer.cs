using System.Drawing;
using System.Windows.Forms;

namespace RestaurantManagementSystem
{
    partial class OrderForm
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtCustomer;
        private ComboBox cmbOrderType;
        private NumericUpDown numTableNumber;
        private DataGridView gridOrderItems;
        private Button btnAddItem;
        private Button btnRemoveItem;
        private Button btnSave;
        private Button btnCancel;
        private Label lblTotal;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // OrderForm
            // 
            ClientSize = new Size(500, 400);
            Name = "OrderForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Create Order";
            Load += OrderForm_Load;
            ResumeLayout(false);
        }

        private void InitializeForm()
        {
            var panel = new Panel { Dock = DockStyle.Top, Height = 120 };

            // Customer
            var lblCustomer = new Label { Text = "Customer:", Location = new Point(10, 15), AutoSize = true };
            txtCustomer = new TextBox { Location = new Point(80, 12), Size = new Size(150, 20) };

            // Order Type
            var lblOrderType = new Label { Text = "Order Type:", Location = new Point(10, 45), AutoSize = true };
            cmbOrderType = new ComboBox { Location = new Point(80, 42), Size = new Size(150, 20) };
            cmbOrderType.Items.AddRange(new object[] { "dine-in", "takeaway", "delivery" });
            cmbOrderType.SelectedIndex = 0;

            // Table Number
            var lblTable = new Label { Text = "Table No:", Location = new Point(10, 75), AutoSize = true };
            numTableNumber = new NumericUpDown { Location = new Point(80, 72), Size = new Size(60, 20), Minimum = 1, Maximum = 50 };

            panel.Controls.AddRange(new Control[] { lblCustomer, txtCustomer, lblOrderType, cmbOrderType, lblTable, numTableNumber });

            // Order Items Grid
            gridOrderItems = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            // Buttons Panel
            var buttonsPanel = new Panel { Dock = DockStyle.Bottom, Height = 80 };

            btnAddItem = new Button { Text = "Add Item", Location = new Point(10, 10), Size = new Size(80, 23) };
            btnRemoveItem = new Button { Text = "Remove Item", Location = new Point(100, 10), Size = new Size(80, 23) };
            lblTotal = new Label { Text = "Total: $0.00", Location = new Point(200, 14), AutoSize = true, Font = new Font("Arial", 10, FontStyle.Bold) };

            btnSave = new Button { Text = "Save", Location = new Point(300, 40), Size = new Size(80, 30), DialogResult = DialogResult.OK };
            btnCancel = new Button { Text = "Cancel", Location = new Point(390, 40), Size = new Size(80, 30), DialogResult = DialogResult.Cancel };

            // Event Handlers
            btnAddItem.Click += btnAddItem_Click;
            btnRemoveItem.Click += btnRemoveItem_Click;
            btnSave.Click += btnSave_Click;

            buttonsPanel.Controls.AddRange(new Control[] { btnAddItem, btnRemoveItem, lblTotal, btnSave, btnCancel });

            // Add all to form
            Controls.Add(gridOrderItems);
            Controls.Add(buttonsPanel);
            Controls.Add(panel);
        }
    }
}
