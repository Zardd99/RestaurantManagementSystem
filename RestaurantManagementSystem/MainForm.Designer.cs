﻿using System.Drawing;
using System.Windows.Forms;

namespace RestaurantManagementSystem
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private MenuStrip menuStrip;
        private ToolStripMenuItem menuManagementToolStripMenuItem;
        private ToolStripMenuItem usersToolStripMenuItem;
        private ToolStripMenuItem menuToolStripMenuItem;
        private ToolStripMenuItem ordersToolStripMenuItem;
        private ToolStripMenuItem reviewsToolStripMenuItem;
        private ToolStripMenuItem suppliersToolStripMenuItem;
        private ToolStripMenuItem receiptsToolStripMenuItem;
        private ToolStripMenuItem categoriesToolStripMenuItem;
        private ToolStripMenuItem logoutToolStripMenuItem;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;
        private Label lblWelcome;
        private Panel panelHeader;
        private Panel panelMain;
        private Panel panelCards;
        private Button btnUsers;
        private Button btnMenu;
        private Button btnOrders;
        private Button btnReviews;
        private Button btnSuppliers;
        private Button btnReceipts;
        private Button btnCategories;
        private Label lblUserInfo;
        private PictureBox pictureBoxLogo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            menuStrip = new MenuStrip();
            menuManagementToolStripMenuItem = new ToolStripMenuItem();
            usersToolStripMenuItem = new ToolStripMenuItem();
            menuToolStripMenuItem = new ToolStripMenuItem();
            ordersToolStripMenuItem = new ToolStripMenuItem();
            reviewsToolStripMenuItem = new ToolStripMenuItem();
            suppliersToolStripMenuItem = new ToolStripMenuItem();
            receiptsToolStripMenuItem = new ToolStripMenuItem();
            categoriesToolStripMenuItem = new ToolStripMenuItem();
            logoutToolStripMenuItem = new ToolStripMenuItem();
            statusStrip = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            panelHeader = new Panel();
            lblWelcome = new Label();
            lblUserInfo = new Label();
            pictureBoxLogo = new PictureBox();
            panelMain = new Panel();
            panelCards = new Panel();
            btnUsers = new Button();
            btnMenu = new Button();
            btnOrders = new Button();
            btnReviews = new Button();
            btnSuppliers = new Button();
            btnReceipts = new Button();
            btnCategories = new Button();

            menuStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).BeginInit();
            panelMain.SuspendLayout();
            panelCards.SuspendLayout();
            SuspendLayout();

            // 
            // menuStrip
            // 
            menuStrip.BackColor = Color.FromArgb(44, 62, 80);
            menuStrip.Font = new Font("Segoe UI", 10F);
            menuStrip.Items.AddRange(new ToolStripItem[] { menuManagementToolStripMenuItem, logoutToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Padding = new Padding(10, 5, 0, 5);
            menuStrip.Size = new Size(1200, 34);
            menuStrip.TabIndex = 0;

            // 
            // menuManagementToolStripMenuItem
            // 
            menuManagementToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                usersToolStripMenuItem, menuToolStripMenuItem, ordersToolStripMenuItem,
                reviewsToolStripMenuItem, suppliersToolStripMenuItem, receiptsToolStripMenuItem,
                categoriesToolStripMenuItem });
            menuManagementToolStripMenuItem.ForeColor = Color.White;
            menuManagementToolStripMenuItem.Name = "menuManagementToolStripMenuItem";
            menuManagementToolStripMenuItem.Size = new Size(155, 24);
            menuManagementToolStripMenuItem.Text = "📋 Management Modules";

            // 
            // usersToolStripMenuItem
            // 
            usersToolStripMenuItem.Name = "usersToolStripMenuItem";
            usersToolStripMenuItem.Size = new Size(150, 24);
            usersToolStripMenuItem.Text = "👥 Users";
            usersToolStripMenuItem.Click += usersToolStripMenuItem_Click;

            // 
            // menuToolStripMenuItem
            // 
            menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            menuToolStripMenuItem.Size = new Size(150, 24);
            menuToolStripMenuItem.Text = "🍽️ Menu";
            menuToolStripMenuItem.Click += menuToolStripMenuItem_Click;

            // 
            // ordersToolStripMenuItem
            // 
            ordersToolStripMenuItem.Name = "ordersToolStripMenuItem";
            ordersToolStripMenuItem.Size = new Size(150, 24);
            ordersToolStripMenuItem.Text = "📦 Orders";
            ordersToolStripMenuItem.Click += ordersToolStripMenuItem_Click;

            // 
            // reviewsToolStripMenuItem
            // 
            reviewsToolStripMenuItem.Name = "reviewsToolStripMenuItem";
            reviewsToolStripMenuItem.Size = new Size(150, 24);
            reviewsToolStripMenuItem.Text = "⭐ Reviews";
            reviewsToolStripMenuItem.Click += reviewsToolStripMenuItem_Click;

            // 
            // suppliersToolStripMenuItem
            // 
            suppliersToolStripMenuItem.Name = "suppliersToolStripMenuItem";
            suppliersToolStripMenuItem.Size = new Size(150, 24);
            suppliersToolStripMenuItem.Text = "🚚 Suppliers";
            suppliersToolStripMenuItem.Click += suppliersToolStripMenuItem_Click;

            // 
            // receiptsToolStripMenuItem
            // 
            receiptsToolStripMenuItem.Name = "receiptsToolStripMenuItem";
            receiptsToolStripMenuItem.Size = new Size(150, 24);
            receiptsToolStripMenuItem.Text = "🧾 Receipts";
            receiptsToolStripMenuItem.Click += receiptsToolStripMenuItem_Click;

            // 
            // categoriesToolStripMenuItem
            // 
            categoriesToolStripMenuItem.Name = "categoriesToolStripMenuItem";
            categoriesToolStripMenuItem.Size = new Size(150, 24);
            categoriesToolStripMenuItem.Text = "📑 Categories";
            categoriesToolStripMenuItem.Click += categoriesToolStripMenuItem_Click;

            // 
            // logoutToolStripMenuItem
            // 
            logoutToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
            logoutToolStripMenuItem.ForeColor = Color.White;
            logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            logoutToolStripMenuItem.Size = new Size(85, 24);
            logoutToolStripMenuItem.Text = "🚪 Logout";
            logoutToolStripMenuItem.Click += logoutToolStripMenuItem_Click;

            // 
            // statusStrip
            // 
            statusStrip.BackColor = Color.FromArgb(236, 240, 241);
            statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel });
            statusStrip.Location = new Point(0, 678);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(1, 0, 16, 0);
            statusStrip.Size = new Size(1200, 22);
            statusStrip.TabIndex = 1;

            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(39, 17);
            statusLabel.Text = "Ready";

            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.FromArgb(52, 152, 219);
            panelHeader.Controls.Add(pictureBoxLogo);
            panelHeader.Controls.Add(lblUserInfo);
            panelHeader.Controls.Add(lblWelcome);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 34);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1200, 120);
            panelHeader.TabIndex = 2;

            // 
            // pictureBoxLogo
            // 
            pictureBoxLogo.BackColor = Color.White;
            pictureBoxLogo.Location = new Point(30, 20);
            pictureBoxLogo.Name = "pictureBoxLogo";
            pictureBoxLogo.Size = new Size(80, 80);
            pictureBoxLogo.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBoxLogo.TabIndex = 2;
            pictureBoxLogo.TabStop = false;

            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblWelcome.ForeColor = Color.White;
            lblWelcome.Location = new Point(130, 30);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(552, 45);
            lblWelcome.TabIndex = 0;
            lblWelcome.Text = "Restaurant Management System";

            // 
            // lblUserInfo
            // 
            lblUserInfo.AutoSize = true;
            lblUserInfo.Font = new Font("Segoe UI", 11F);
            lblUserInfo.ForeColor = Color.White;
            lblUserInfo.Location = new Point(130, 75);
            lblUserInfo.Name = "lblUserInfo";
            lblUserInfo.Size = new Size(200, 20);
            lblUserInfo.TabIndex = 1;
            lblUserInfo.Text = "Welcome back!";

            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(236, 240, 241);
            panelMain.Controls.Add(panelCards);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 154);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(30);
            panelMain.Size = new Size(1200, 524);
            panelMain.TabIndex = 3;

            // 
            // panelCards
            // 
            panelCards.AutoScroll = true;
            panelCards.Controls.Add(btnUsers);
            panelCards.Controls.Add(btnMenu);
            panelCards.Controls.Add(btnOrders);
            panelCards.Controls.Add(btnReviews);
            panelCards.Controls.Add(btnSuppliers);
            panelCards.Controls.Add(btnReceipts);
            panelCards.Controls.Add(btnCategories);
            panelCards.Dock = DockStyle.Fill;
            panelCards.Location = new Point(30, 30);
            panelCards.Name = "panelCards";
            panelCards.Size = new Size(1140, 464);
            panelCards.TabIndex = 0;

            // Create card-style buttons
            var buttonWidth = 250;
            var buttonHeight = 150;
            var spacing = 30;
            var buttonsPerRow = 4;

            // 
            // btnUsers
            // 
            btnUsers = CreateModuleButton("👥 Users", 0, buttonWidth, buttonHeight, spacing, buttonsPerRow);
            btnUsers.Click += usersToolStripMenuItem_Click;

            // 
            // btnMenu
            // 
            btnMenu = CreateModuleButton("🍽️ Menu", 1, buttonWidth, buttonHeight, spacing, buttonsPerRow);
            btnMenu.Click += menuToolStripMenuItem_Click;

            // 
            // btnOrders
            // 
            btnOrders = CreateModuleButton("📦 Orders", 2, buttonWidth, buttonHeight, spacing, buttonsPerRow);
            btnOrders.Click += ordersToolStripMenuItem_Click;

            // 
            // btnReviews
            // 
            btnReviews = CreateModuleButton("⭐ Reviews", 3, buttonWidth, buttonHeight, spacing, buttonsPerRow);
            btnReviews.Click += reviewsToolStripMenuItem_Click;

            // 
            // btnSuppliers
            // 
            btnSuppliers = CreateModuleButton("🚚 Suppliers", 4, buttonWidth, buttonHeight, spacing, buttonsPerRow);
            btnSuppliers.Click += suppliersToolStripMenuItem_Click;

            // 
            // btnReceipts
            // 
            btnReceipts = CreateModuleButton("🧾 Receipts", 5, buttonWidth, buttonHeight, spacing, buttonsPerRow);
            btnReceipts.Click += receiptsToolStripMenuItem_Click;

            // 
            // btnCategories
            // 
            btnCategories = CreateModuleButton("📑 Categories", 6, buttonWidth, buttonHeight, spacing, buttonsPerRow);
            btnCategories.Click += categoriesToolStripMenuItem_Click;

            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 700);
            Controls.Add(panelMain);
            Controls.Add(panelHeader);
            Controls.Add(statusStrip);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Restaurant Management System";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).EndInit();
            panelMain.ResumeLayout(false);
            panelCards.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private Button CreateModuleButton(string text, int index, int width, int height, int spacing, int perRow)
        {
            var row = index / perRow;
            var col = index % perRow;

            var btn = new Button
            {
                Text = text,
                Size = new Size(width, height),
                Location = new Point(col * (width + spacing), row * (height + spacing)),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter
            };

            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(52, 152, 219);
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(41, 128, 185);

            btn.MouseEnter += (s, e) => {
                btn.BackColor = Color.FromArgb(52, 152, 219);
                btn.ForeColor = Color.White;
            };

            btn.MouseLeave += (s, e) => {
                btn.BackColor = Color.White;
                btn.ForeColor = Color.FromArgb(44, 62, 80);
            };

            return btn;
        }
    }
}