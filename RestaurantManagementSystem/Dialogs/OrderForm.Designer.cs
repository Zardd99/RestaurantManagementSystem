using System.Drawing;
using System.Windows.Forms;

namespace RestaurantManagementSystem
{
    partial class OrderForm
    {
        private System.ComponentModel.IContainer components = null;

        // Main containers
        private Panel panelHeader;
        private Panel panelItems;
        private Panel panelFooter;
        private SplitContainer splitContainer;

        // Customer information
        private GroupBox grpCustomer;
        private TextBox txtCustomer;
        private ComboBox cmbOrderType;
        private NumericUpDown numTableNumber;
        private Label lblTable;

        // Order items
        private GroupBox grpItems;
        private DataGridView gridOrderItems;
        private Button btnAddItem;
        private Button btnRemoveItem;
        private Button btnEditItem;

        // Footer
        private GroupBox grpSummary;
        private Label lblSubtotal;
        private Label lblTax;
        private Label lblTotal;
        private Label lblSubtotalValue;
        private Label lblTaxValue;
        private Label lblTotalValue;
        private Button btnSave;
        private Button btnCancel;

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
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 600);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimumSize = new Size(600, 500);
            Name = "OrderForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Create New Order";
            Load += OrderForm_Load;
            ResumeLayout(false);
        }

        private void InitializeForm()
        {
            CreateHeaderPanel();
            CreateItemsPanel();
            CreateFooterPanel();

            // Main layout
            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 120));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 150));

            mainLayout.Controls.Add(panelHeader, 0, 0);
            mainLayout.Controls.Add(panelItems, 0, 1);
            mainLayout.Controls.Add(panelFooter, 0, 2);

            this.Controls.Add(mainLayout);
        }

        private void CreateHeaderPanel()
        {
            panelHeader = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10) };
            grpCustomer = new GroupBox
            {
                Text = "Customer Information",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 2,
                Padding = new Padding(10)
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

            // Row 1
            var lblCustomer = new Label { Text = "Customer Name:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill };
            txtCustomer = new TextBox { Dock = DockStyle.Fill, Margin = new Padding(5) };
            var lblOrderType = new Label { Text = "Order Type:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill };
            cmbOrderType = new ComboBox { Dock = DockStyle.Fill, Margin = new Padding(5) };
            cmbOrderType.Items.AddRange(new object[] { "dine-in", "takeaway", "delivery" });
            cmbOrderType.SelectedIndex = 0;

            // Row 2
            var lblEmpty = new Label { Text = "" }; // Empty cell
            var lblEmpty2 = new Label { Text = "" }; // Empty cell
            lblTable = new Label { Text = "Table Number:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill };
            numTableNumber = new NumericUpDown { Dock = DockStyle.Fill, Margin = new Padding(5), Minimum = 1, Maximum = 50, Value = 1 };

            // Add to layout
            layout.Controls.Add(lblCustomer, 0, 0);
            layout.Controls.Add(txtCustomer, 1, 0);
            layout.Controls.Add(lblOrderType, 2, 0);
            layout.Controls.Add(cmbOrderType, 3, 0);
            layout.Controls.Add(lblEmpty, 0, 1);
            layout.Controls.Add(lblEmpty2, 1, 1);
            layout.Controls.Add(lblTable, 2, 1);
            layout.Controls.Add(numTableNumber, 3, 1);

            grpCustomer.Controls.Add(layout);
            panelHeader.Controls.Add(grpCustomer);
        }

        private void CreateItemsPanel()
        {
            panelItems = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10) };
            grpItems = new GroupBox
            {
                Text = "Order Items",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

            // Data Grid
            gridOrderItems = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false
            };

            // Buttons panel
            var buttonsPanel = new Panel { Dock = DockStyle.Fill };
            btnAddItem = new Button { Text = "➕ Add Item", Location = new Point(10, 8), Size = new Size(100, 25) };
            btnEditItem = new Button { Text = "✏️ Edit Item", Location = new Point(120, 8), Size = new Size(100, 25) };
            btnRemoveItem = new Button { Text = "🗑️ Remove", Location = new Point(230, 8), Size = new Size(100, 25) };

            buttonsPanel.Controls.AddRange(new Control[] { btnAddItem, btnEditItem, btnRemoveItem });

            layout.Controls.Add(gridOrderItems, 0, 0);
            layout.Controls.Add(buttonsPanel, 0, 1);

            grpItems.Controls.Add(layout);
            panelItems.Controls.Add(grpItems);
        }

        private void CreateFooterPanel()
        {
            panelFooter = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10) };
            grpSummary = new GroupBox
            {
                Text = "Order Summary",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 4,
                Padding = new Padding(15)
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));

            // Summary labels
            lblSubtotal = new Label { Text = "Subtotal:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill, Font = new Font("Segoe UI", 9F) };
            lblSubtotalValue = new Label { Text = "$0.00", TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill, Font = new Font("Segoe UI", 9F) };

            lblTax = new Label { Text = "Tax (10%):", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill, Font = new Font("Segoe UI", 9F) };
            lblTaxValue = new Label { Text = "$0.00", TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill, Font = new Font("Segoe UI", 9F) };

            var separator = new Label { Text = "", BorderStyle = BorderStyle.Fixed3D, Height = 2, Dock = DockStyle.Fill };
            var separatorCell = new Label { Text = "", BorderStyle = BorderStyle.Fixed3D, Height = 2, Dock = DockStyle.Fill };

            lblTotal = new Label { Text = "TOTAL:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            lblTotalValue = new Label { Text = "$0.00", TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill, Font = new Font("Segoe UI", 12F, FontStyle.Bold), ForeColor = Color.Green };

            // Buttons
            var buttonsPanel = new Panel { Dock = DockStyle.Bottom, Height = 45 };
            btnSave = new Button
            {
                Text = "💾 Save Order",
                Size = new Size(120, 35),
                Location = new Point(200, 5),
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                DialogResult = DialogResult.OK
            };
            btnCancel = new Button
            {
                Text = "❌ Cancel",
                Size = new Size(120, 35),
                Location = new Point(330, 5),
                BackColor = Color.LightGray,
                Font = new Font("Segoe UI", 9F),
                DialogResult = DialogResult.Cancel
            };

            buttonsPanel.Controls.AddRange(new Control[] { btnSave, btnCancel });

            // Add to layout
            layout.Controls.Add(lblSubtotal, 0, 0);
            layout.Controls.Add(lblSubtotalValue, 1, 0);
            layout.Controls.Add(lblTax, 0, 1);
            layout.Controls.Add(lblTaxValue, 1, 1);
            layout.Controls.Add(separator, 0, 2);
            layout.Controls.Add(separatorCell, 1, 2);
            layout.Controls.Add(lblTotal, 0, 3);
            layout.Controls.Add(lblTotalValue, 1, 3);

            grpSummary.Controls.Add(layout);
            grpSummary.Controls.Add(buttonsPanel);
            panelFooter.Controls.Add(grpSummary);
        }
    }
}