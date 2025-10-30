using RestaurantManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace RestaurantManagementSystem
{
    public partial class MenuItemForm : Form
    {
        public MenuItemCreateRequest MenuItem { get; private set; }
        private readonly MenuItem _existingItem;

        public MenuItemForm(MenuItem existingItem = null)
        {
            _existingItem = existingItem;
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Load existing data if editing
            if (_existingItem != null)
            {
                LoadExistingData();
            }
        }

        private void LoadExistingData()
        {
            txtName.Text = _existingItem.Name;
            txtDescription.Text = _existingItem.Description;
            numPrice.Value = _existingItem.Price;
            txtCategory.Text = _existingItem.CategoryId; // Store the ID for updates
            txtImage.Text = _existingItem.Image;
            numPreparationTime.Value = _existingItem.PreparationTime;
            chkAvailability.Checked = _existingItem.Availability;
            chkChefSpecial.Checked = _existingItem.ChefSpecial;

            // Set dietary tags
            if (_existingItem.DietaryTags != null)
            {
                for (int i = 0; i < clbDietaryTags.Items.Count; i++)
                {
                    var tag = clbDietaryTags.Items[i].ToString();
                    clbDietaryTags.SetItemChecked(i, _existingItem.DietaryTags.Contains(tag));
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                MenuItem = new MenuItemCreateRequest
                {
                    Name = txtName.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    Price = numPrice.Value,
                    Category = txtCategory.Text.Trim(),
                    Image = txtImage.Text.Trim(),
                    PreparationTime = (int)numPreparationTime.Value,
                    Availability = chkAvailability.Checked,
                    ChefSpecial = chkChefSpecial.Checked,
                    DietaryTags = GetSelectedDietaryTags()
                };
                DialogResult = DialogResult.OK;
            }
        }

        private List<string> GetSelectedDietaryTags()
        {
            var selectedTags = new List<string>();
            foreach (var item in clbDietaryTags.CheckedItems)
            {
                selectedTags.Add(item.ToString());
            }
            return selectedTags;
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter a name for the menu item.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Please enter a description for the menu item.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescription.Focus();
                return false;
            }

            if (numPrice.Value <= 0)
            {
                MessageBox.Show("Please enter a valid price greater than 0.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numPrice.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCategory.Text))
            {
                MessageBox.Show("Please enter a category for the menu item.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCategory.Focus();
                return false;
            }

            return true;
        }
    }
}
