using System;
using System.Drawing;
using System.Windows.Forms;

namespace RestaurantManagementSystem
{
    public partial class StatusUpdateForm : Form
    {
        public string SelectedStatus { get; private set; }

        public StatusUpdateForm(string currentStatus)
        {
            InitializeComponent();
            InitializeForm(currentStatus);
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // StatusUpdateForm
            // 
            ClientSize = new Size(300, 200);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "StatusUpdateForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Update Order Status";
            Load += StatusUpdateForm_Load;
            ResumeLayout(false);
        }

        private ComboBox cmbStatus;
        private Button btnUpdate;
        private Button btnCancel;

        private void InitializeForm(string currentStatus)
        {
            var panel = new Panel { Dock = DockStyle.Fill };

            var lblStatus = new Label { Text = "New Status:", Location = new Point(20, 30), AutoSize = true };
            cmbStatus = new ComboBox
            {
                Location = new Point(100, 27),
                Size = new Size(150, 23),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            cmbStatus.Items.AddRange(new object[]
            {
                "pending",
                "confirmed",
                "preparing",
                "ready",
                "served",
                "cancelled"
            });

            // Set current status
            if (!string.IsNullOrEmpty(currentStatus))
            {
                cmbStatus.SelectedItem = currentStatus;
            }
            else
            {
                cmbStatus.SelectedIndex = 0;
            }

            btnUpdate = new Button { Text = "Update", Location = new Point(60, 80), Size = new Size(80, 30), DialogResult = DialogResult.OK };
            btnCancel = new Button { Text = "Cancel", Location = new Point(160, 80), Size = new Size(80, 30), DialogResult = DialogResult.Cancel };

            btnUpdate.Click += btnUpdate_Click;

            panel.Controls.AddRange(new Control[] { lblStatus, cmbStatus, btnUpdate, btnCancel });
            Controls.Add(panel);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cmbStatus.SelectedItem != null)
            {
                SelectedStatus = cmbStatus.SelectedItem.ToString();
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Please select a status.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void StatusUpdateForm_Load(object sender, EventArgs e)
        {

        }
    }
}