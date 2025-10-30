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
    public partial class ReviewsForm : Form
    {
        private readonly ReviewManager _reviewManager;
        private List<Review> _reviews;
        private BindingList<Review> _reviewViewModels;

        public ReviewsForm(ReviewManager reviewManager)
        {
            _reviewManager = reviewManager;
            InitializeComponent();
            InitializeDataGridView();
            LoadReviews();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // ReviewsForm
            // 
            ClientSize = new Size(800, 450);
            Name = "ReviewsForm";
            Text = "Review Management";
            Load += ReviewsForm_Load;
            ResumeLayout(false);
        }

        private DataGridView dataGridView;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnRefresh;

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

            btnAdd = new Button { Text = "Add", Location = new Point(10, 10), Size = new Size(75, 23) };
            btnEdit = new Button { Text = "Edit", Location = new Point(90, 10), Size = new Size(75, 23) };
            btnDelete = new Button { Text = "Delete", Location = new Point(170, 10), Size = new Size(75, 23) };
            btnRefresh = new Button { Text = "Refresh", Location = new Point(250, 10), Size = new Size(75, 23) };

            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnDelete.Click += btnDelete_Click;
            btnRefresh.Click += btnRefresh_Click;

            panel.Controls.AddRange(new Control[] { btnAdd, btnEdit, btnDelete, btnRefresh });

            Controls.Add(dataGridView);
            Controls.Add(panel);
        }

        private async void LoadReviews()
        {
            try
            {
                _reviews = await _reviewManager.GetReviewsAsync();
                _reviewViewModels = new BindingList<Review>(_reviews);
                dataGridView.DataSource = _reviewViewModels;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading reviews: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Implementation for adding review
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Implementation for editing review
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0) return;

            var selectedReview = dataGridView.SelectedRows[0].DataBoundItem as Review;
            if (selectedReview != null)
            {
                var result = MessageBox.Show("Delete this review?", "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        await _reviewManager.DeleteReviewAsync(selectedReview.Id);
                        LoadReviews();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting review: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadReviews();
        }

        private void ReviewsForm_Load(object sender, EventArgs e)
        {

        }
    }
}