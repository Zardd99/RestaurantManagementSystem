using System.Drawing;
using System.Windows.Forms;

namespace RestaurantManagementSystem
{
    partial class MenuItemForm
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtName;
        private TextBox txtDescription;
        private NumericUpDown numPrice;
        private TextBox txtCategory;
        private TextBox txtImage;
        private NumericUpDown numPreparationTime;
        private CheckBox chkAvailability;
        private CheckBox chkChefSpecial;
        private CheckedListBox clbDietaryTags;
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
            this.SuspendLayout();

            // Form
            this.ClientSize = new System.Drawing.Size(500, 600);
            this.Name = "MenuItemForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Text = _existingItem == null ? "Add Menu Item" : "Edit Menu Item";

            var panel = new Panel { Dock = DockStyle.Fill, AutoScroll = true };
            var y = 10;
            var labelWidth = 120;
            var controlWidth = 300;
            var controlHeight = 20;
            var spacing = 30;

            // Name
            var lblName = new Label { Text = "Name:", Location = new Point(10, y), Width = labelWidth };
            txtName = new TextBox { Location = new Point(130, y), Size = new Size(controlWidth, controlHeight) };
            y += spacing;

            // Description
            var lblDescription = new Label { Text = "Description:", Location = new Point(10, y), Width = labelWidth };
            txtDescription = new TextBox { Location = new Point(130, y), Size = new Size(controlWidth, 60), Multiline = true };
            y += 70;

            // Price
            var lblPrice = new Label { Text = "Price:", Location = new Point(10, y), Width = labelWidth };
            numPrice = new NumericUpDown { Location = new Point(130, y), Size = new Size(100, controlHeight), Minimum = 0, Maximum = 1000, DecimalPlaces = 2 };
            y += spacing;

            // Category
            var lblCategory = new Label { Text = "Category:", Location = new Point(10, y), Width = labelWidth };
            txtCategory = new TextBox { Location = new Point(130, y), Size = new Size(controlWidth, controlHeight) };
            y += spacing;

            // Image
            var lblImage = new Label { Text = "Image URL:", Location = new Point(10, y), Width = labelWidth };
            txtImage = new TextBox { Location = new Point(130, y), Size = new Size(controlWidth, controlHeight) };
            y += spacing;

            // Preparation Time
            var lblPreparationTime = new Label { Text = "Prep Time (min):", Location = new Point(10, y), Width = labelWidth };
            numPreparationTime = new NumericUpDown { Location = new Point(130, y), Size = new Size(100, controlHeight), Minimum = 1, Maximum = 300, Value = 15 };
            y += spacing;

            // Dietary Tags
            var lblDietaryTags = new Label { Text = "Dietary Tags:", Location = new Point(10, y), Width = labelWidth };
            clbDietaryTags = new CheckedListBox
            {
                Location = new Point(130, y),
                Size = new Size(controlWidth, 100),
                CheckOnClick = true
            };
            clbDietaryTags.Items.AddRange(new object[]
            {
                "vegetarian",
                "vegan",
                "gluten-free",
                "dairy-free",
                "spicy",
                "nut-free"
            });
            y += 110;

            // Availability
            chkAvailability = new CheckBox { Text = "Available", Location = new Point(130, y), Checked = true };
            y += spacing;

            // Chef Special
            chkChefSpecial = new CheckBox { Text = "Chef Special", Location = new Point(130, y) };
            y += 40;

            // Buttons
            btnSave = new Button { Text = "Save", Location = new Point(130, y), Size = new Size(80, 30), DialogResult = DialogResult.OK };
            btnCancel = new Button { Text = "Cancel", Location = new Point(220, y), Size = new Size(80, 30), DialogResult = DialogResult.Cancel };

            btnSave.Click += btnSave_Click;

            // Add controls
            panel.Controls.AddRange(new Control[]
            {
                lblName, txtName,
                lblDescription, txtDescription,
                lblPrice, numPrice,
                lblCategory, txtCategory,
                lblImage, txtImage,
                lblPreparationTime, numPreparationTime,
                lblDietaryTags, clbDietaryTags,
                chkAvailability, chkChefSpecial,
                btnSave, btnCancel
            });

            this.Controls.Add(panel);
            this.ResumeLayout(false);
        }
    }
}
