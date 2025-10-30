using System.Drawing;
using System.Windows.Forms;

namespace RestaurantManagementSystem
{
    partial class OrderItemForm
    {
        private System.ComponentModel.IContainer components = null;

        // Main containers
        private Panel panelMain;
        private Panel panelHeader;
        private Panel panelForm;
        private Panel panelFooter;

        // Form controls
        private ComboBox cmbMenuItem;
        private NumericUpDown numQuantity;
        private TextBox txtSpecialInstructions;
        private Label lblUnitPrice;
        private Label lblTotalPrice;
        private Label lblUnitPriceValue;
        private Label lblTotalPriceValue;
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
            // OrderItemForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(434, 400);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MaximumSize = new Size(450, 500);
            MinimizeBox = false;
            MinimumSize = new Size(450, 400);
            Name = "OrderItemForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Add Order Item";
            Load += OrderItemForm_Load;
            ResumeLayout(false);
        }

        private void InitializeForm()
        {
            CreateHeaderPanel();
            CreateFormPanel();
            CreateFooterPanel();

            // Main layout
            panelMain = new Panel { Dock = DockStyle.Fill, Padding = new Padding(15) };

            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));

            mainLayout.Controls.Add(panelHeader, 0, 0);
            mainLayout.Controls.Add(panelForm, 0, 1);
            mainLayout.Controls.Add(panelFooter, 0, 2);

            panelMain.Controls.Add(mainLayout);
            this.Controls.Add(panelMain);
        }

        private void CreateHeaderPanel()
        {
            panelHeader = new Panel { Dock = DockStyle.Fill };

            var lblTitle = new Label
            {
                Text = "Add Item to Order",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.SteelBlue
            };

            panelHeader.Controls.Add(lblTitle);
        }

        private void CreateFormPanel()
        {
            panelForm = new Panel { Dock = DockStyle.Fill };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 5,
                Padding = new Padding(10)
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            // Menu Item
            var lblMenuItem = new Label
            {
                Text = "Menu Item:",
                TextAlign = ContentAlignment.MiddleRight,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            cmbMenuItem = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9F)
            };
            layout.SetRow(lblMenuItem, 0);
            layout.SetRow(cmbMenuItem, 0);

            // Quantity
            var lblQuantity = new Label
            {
                Text = "Quantity:",
                TextAlign = ContentAlignment.MiddleRight,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            numQuantity = new NumericUpDown
            {
                Dock = DockStyle.Fill,
                Minimum = 1,
                Maximum = 50,
                Value = 1,
                Font = new Font("Segoe UI", 9F)
            };
            layout.SetRow(lblQuantity, 1);
            layout.SetRow(numQuantity, 1);

            // Unit Price
            var lblUnitPrice = new Label
            {
                Text = "Unit Price:",
                TextAlign = ContentAlignment.MiddleRight,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            lblUnitPriceValue = new Label
            {
                Text = "$0.00",
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Green
            };
            layout.SetRow(lblUnitPrice, 2);
            layout.SetRow(lblUnitPriceValue, 2);

            // Total Price
            var lblTotalPrice = new Label
            {
                Text = "Total Price:",
                TextAlign = ContentAlignment.MiddleRight,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            lblTotalPriceValue = new Label
            {
                Text = "$0.00",
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.DarkGreen
            };
            layout.SetRow(lblTotalPrice, 3);
            layout.SetRow(lblTotalPriceValue, 3);

            // Special Instructions
            var lblInstructions = new Label
            {
                Text = "Special Instructions:",
                TextAlign = ContentAlignment.MiddleRight,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            txtSpecialInstructions = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                Font = new Font("Segoe UI", 9F),
                ScrollBars = ScrollBars.Vertical
            };
            layout.SetRow(lblInstructions, 4);
            layout.SetRow(txtSpecialInstructions, 4);

            // Add all controls to layout
            layout.Controls.Add(lblMenuItem, 0, 0);
            layout.Controls.Add(cmbMenuItem, 1, 0);
            layout.Controls.Add(lblQuantity, 0, 1);
            layout.Controls.Add(numQuantity, 1, 1);
            layout.Controls.Add(lblUnitPrice, 0, 2);
            layout.Controls.Add(lblUnitPriceValue, 1, 2);
            layout.Controls.Add(lblTotalPrice, 0, 3);
            layout.Controls.Add(lblTotalPriceValue, 1, 3);
            layout.Controls.Add(lblInstructions, 0, 4);
            layout.Controls.Add(txtSpecialInstructions, 1, 4);

            panelForm.Controls.Add(layout);
        }

        private void CreateFooterPanel()
        {
            panelFooter = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10) };

            var buttonsPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20, 10, 20, 10)
            };

            btnSave = new Button
            {
                Text = "💾 Add to Order",
                Size = new Size(120, 35),
                Location = new Point(80, 5),
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                DialogResult = DialogResult.OK
            };

            btnCancel = new Button
            {
                Text = "❌ Cancel",
                Size = new Size(120, 35),
                Location = new Point(210, 5),
                BackColor = Color.LightGray,
                Font = new Font("Segoe UI", 9F),
                DialogResult = DialogResult.Cancel
            };

            buttonsPanel.Controls.AddRange(new Control[] { btnSave, btnCancel });
            panelFooter.Controls.Add(buttonsPanel);
        }
    }
}