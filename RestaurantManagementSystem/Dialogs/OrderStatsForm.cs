using System;
using System.Drawing;
using System.Windows.Forms;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem
{
    public partial class OrderStatsForm : Form
    {
        public OrderStatsForm(OrderStats stats)
        {
            InitializeComponent();
            InitializeForm(stats);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Name = "OrderStatsForm";
            this.Text = "Order Statistics";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ResumeLayout(false);
        }

        private void InitializeForm(OrderStats stats)
        {
            var panel = new Panel { Dock = DockStyle.Fill, AutoScroll = true };

            var y = 20;
            var labelWidth = 150;
            var valueWidth = 100;

            // Earnings Section
            var lblEarnings = new Label { Text = "EARNINGS", Location = new Point(20, y), Font = new Font("Arial", 10, FontStyle.Bold), AutoSize = true };
            y += 30;

            AddStatRow(panel, "Daily Earnings:", stats.DailyEarnings.ToString("C"), ref y, labelWidth, valueWidth);
            AddStatRow(panel, "Weekly Earnings:", stats.WeeklyEarnings.ToString("C"), ref y, labelWidth, valueWidth);
            AddStatRow(panel, "Yearly Earnings:", stats.YearlyEarnings.ToString("C"), ref y, labelWidth, valueWidth);

            y += 20;

            // Best Selling Dishes
            var lblBestSellers = new Label { Text = "BEST SELLING DISHES", Location = new Point(20, y), Font = new Font("Arial", 10, FontStyle.Bold), AutoSize = true };
            y += 30;

            if (stats.BestSellingDishes != null && stats.BestSellingDishes.Count > 0)
            {
                foreach (var dish in stats.BestSellingDishes)
                {
                    AddStatRow(panel, $"{dish.Name}:", $"{dish.Quantity} sold", ref y, labelWidth, valueWidth);
                }
            }
            else
            {
                var lblNoData = new Label { Text = "No data available", Location = new Point(20, y), AutoSize = true };
                panel.Controls.Add(lblNoData);
            }

            // Close Button
            var btnClose = new Button { Text = "Close", Location = new Point(150, y + 20), Size = new Size(80, 30), DialogResult = DialogResult.OK };
            panel.Controls.Add(btnClose);

            Controls.Add(panel);
        }

        private void AddStatRow(Panel panel, string label, string value, ref int y, int labelWidth, int valueWidth)
        {
            var lbl = new Label { Text = label, Location = new Point(20, y), Width = labelWidth };
            var lblValue = new Label { Text = value, Location = new Point(20 + labelWidth, y), Width = valueWidth, TextAlign = ContentAlignment.MiddleRight };

            panel.Controls.Add(lbl);
            panel.Controls.Add(lblValue);

            y += 25;
        }
    }
}