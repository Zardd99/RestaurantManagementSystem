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
    public partial class ReceiptsForm : Form
    {
        private readonly ReceiptManager _receiptManager;
        private List<Receipt> _receipts;
        private BindingList<Receipt> _receiptViewModels;

        public ReceiptsForm(ReceiptManager receiptManager)
        {
            _receiptManager = receiptManager;
            InitializeComponent();
            InitializeDataGridView();
            LoadReceipts();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            ClientSize = new Size(800, 450);
            Name = "ReceiptsForm";
            Text = "Receipt Management";
            Load += ReceiptsForm_Load;
            ResumeLayout(false);
        }

        private DataGridView dataGridView;
        private Button btnCreate;
        private Button btnView;
        private Button btnRefresh;
        private Button btnPrint;

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

            // Configure columns for the new structure
            dataGridView.Columns.Clear();
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ReceiptNumber",
                HeaderText = "Receipt #",
                Name = "ReceiptNumber"
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Customer.Name",
                HeaderText = "Customer",
                Name = "CustomerName"
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TotalAmount",
                HeaderText = "Amount",
                Name = "TotalAmount",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PaymentMethod",
                HeaderText = "Payment Method",
                Name = "PaymentMethod"
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PaymentStatus",
                HeaderText = "Status",
                Name = "PaymentStatus"
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "IssuedAt",
                HeaderText = "Date",
                Name = "IssuedAt"
            });

            var panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40
            };

            btnCreate = new Button { Text = "Create", Location = new Point(10, 10), Size = new Size(75, 23) };
            btnView = new Button { Text = "View", Location = new Point(90, 10), Size = new Size(75, 23) };
            btnRefresh = new Button { Text = "Refresh", Location = new Point(170, 10), Size = new Size(75, 23) };
            btnPrint = new Button { Text = "Print", Location = new Point(250, 10), Size = new Size(75, 23) };

            btnCreate.Click += btnCreate_Click;
            btnView.Click += btnView_Click;
            btnRefresh.Click += btnRefresh_Click;
            btnPrint.Click += btnPrint_Click;

            panel.Controls.AddRange(new Control[] { btnCreate, btnView, btnRefresh, btnPrint });

            Controls.Add(dataGridView);
            Controls.Add(panel);
        }

        private async void LoadReceipts()
        {
            try
            {
                _receipts = await _receiptManager.GetReceiptsAsync();
                _receiptViewModels = new BindingList<Receipt>(_receipts);
                dataGridView.DataSource = _receiptViewModels;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading receipts: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            // Implementation for creating receipt
            MessageBox.Show("Create receipt functionality would be implemented here", "Create Receipt",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                var selectedReceipt = dataGridView.SelectedRows[0].DataBoundItem as Receipt;
                if (selectedReceipt != null)
                {
                    // Show receipt details
                    MessageBox.Show($"Receipt: {selectedReceipt.ReceiptNumber}\nAmount: {selectedReceipt.TotalAmount:C}",
                        "Receipt Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadReceipts();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                // Implementation for printing receipt
                MessageBox.Show("Print functionality would be implemented here", "Print",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ReceiptsForm_Load(object sender, EventArgs e)
        {

        }
    }
}