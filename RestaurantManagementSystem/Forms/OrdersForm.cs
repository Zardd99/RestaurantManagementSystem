using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RestaurantManagementSystem.Managers;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.ViewModels;

namespace RestaurantManagementSystem
{
    public partial class OrdersForm : Form
    {
        private readonly OrderManager _orderManager;
        private List<Order> _orders;
        private BindingList<OrderViewModel> _orderViewModels;

        public OrdersForm(OrderManager orderManager)
        {
            _orderManager = orderManager;
            InitializeComponent();
            InitializeDataGridView();
            LoadOrders();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // OrdersForm
            // 
            ClientSize = new Size(800, 450);
            Name = "OrdersForm";
            Text = "Order Management";
            Load += OrdersForm_Load;
            ResumeLayout(false);
        }

        private DataGridView dataGridView;
        private Button btnCreate;
        private Button btnUpdateStatus;
        private Button btnRefresh;
        private Button btnViewStats;
        private ComboBox cmbStatusFilter;

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

            var panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40
            };

            btnCreate = new Button { Text = "Create", Location = new Point(10, 10), Size = new Size(75, 23) };
            btnUpdateStatus = new Button { Text = "Update Status", Location = new Point(90, 10), Size = new Size(100, 23) };
            btnRefresh = new Button { Text = "Refresh", Location = new Point(195, 10), Size = new Size(75, 23) };
            btnViewStats = new Button { Text = "Stats", Location = new Point(275, 10), Size = new Size(75, 23) };

            cmbStatusFilter = new ComboBox
            {
                Location = new Point(355, 10),
                Size = new Size(120, 23),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            cmbStatusFilter.Items.AddRange(new object[] { "All", "pending", "confirmed", "preparing", "ready", "served", "cancelled" });
            cmbStatusFilter.SelectedIndex = 0;

            btnCreate.Click += btnCreate_Click;
            btnUpdateStatus.Click += btnUpdateStatus_Click;
            btnRefresh.Click += btnRefresh_Click;
            btnViewStats.Click += btnViewStats_Click;
            cmbStatusFilter.SelectedIndexChanged += cmbStatusFilter_SelectedIndexChanged;

            panel.Controls.AddRange(new Control[] { btnCreate, btnUpdateStatus, btnRefresh, btnViewStats, cmbStatusFilter });

            Controls.Add(dataGridView);
            Controls.Add(panel);
        }

        private async void LoadOrders()
        {
            try
            {
                _orders = await _orderManager.GetOrdersAsync();
                ApplyFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading orders: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilter()
        {
            var filtered = _orders;

            if (cmbStatusFilter.SelectedItem?.ToString() != "All")
            {
                filtered = filtered.Where(o => o.Status == cmbStatusFilter.SelectedItem.ToString()).ToList();
            }

            _orderViewModels = new BindingList<OrderViewModel>(
                filtered.Select(OrderViewModel.FromModel).ToList()
            );
            dataGridView.DataSource = _orderViewModels;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            var orderForm = new OrderForm();
            if (orderForm.ShowDialog() == DialogResult.OK)
            {
                LoadOrders();
            }
        }

        private async void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0) return;

            var selectedOrder = dataGridView.SelectedRows[0].DataBoundItem as OrderViewModel;
            if (selectedOrder != null)
            {
                var statusForm = new StatusUpdateForm(selectedOrder.Status);
                if (statusForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        await _orderManager.UpdateOrderStatusAsync(selectedOrder.Id, statusForm.SelectedStatus);
                        LoadOrders();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error updating order: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async void btnViewStats_Click(object sender, EventArgs e)
        {
            try
            {
                var stats = await _orderManager.GetOrderStatsAsync();
                var statsForm = new OrderStatsForm(stats);
                statsForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading stats: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadOrders();
        }

        private void cmbStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void OrdersForm_Load(object sender, EventArgs e)
        {

        }
    }
}