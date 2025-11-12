using System.Drawing;
using System.Windows.Forms;

namespace RestaurantManagementSystem
{
    public static class UIColors
    {
        public static Color SidebarBackground => Color.FromArgb(30, 33, 40);
        public static Color SidebarHeader => Color.FromArgb(25, 28, 35);
        public static Color SidebarText => Color.FromArgb(200, 200, 200);
        public static Color WorkspaceBackground => Color.FromArgb(248, 250, 252);
        public static Color AccentBlue => Color.FromArgb(52, 152, 219);
        public static Color LogoutRed => Color.FromArgb(231, 76, 60);
        public static Color White => Color.White;
        public static Color DarkText => Color.FromArgb(30, 33, 40);
        public static Color MutedText => Color.FromArgb(120, 120, 120);
        public static Color DashboardText => Color.FromArgb(100, 100, 100);
        public static Color SidebarHover = Color.FromArgb(55, 60, 65);
        public static Color SidebarTextDim = Color.LightGray;
        public static Color SidebarFooter = Color.FromArgb(30, 35, 40);
        public static Color SidebarAccent = Color.FromArgb(25, 130, 200);
        public static Color AccentRed = Color.FromArgb(210, 70, 70);

        public static Color HeaderBackground = Color.White;
        public static Color HeaderText = Color.Black;
        public static Color HeaderSubText = Color.Gray;

        public static Color ContentBackground = Color.FromArgb(245, 247, 250);
    }

    public static class UIStyles
    {
        public static Font MenuButtonFont => new Font("Segoe UI", 10F);
        public static Font MenuButtonActiveFont => new Font("Segoe UI", 10F, FontStyle.Bold);
        public static Font DashboardText => new Font("Segoe UI", 13F, FontStyle.Regular);
        public static Font LogoFont = new Font("Segoe UI Semibold", 14f, FontStyle.Bold);
        public static Font HeaderTitleFont = new Font("Segoe UI Semibold", 16f, FontStyle.Bold);
        public static Font HeaderSubtitleFont = new Font("Segoe UI", 10f);
        public static Font UserNameFont = new Font("Segoe UI Semibold", 10f);
        public static Font UserRoleFont = new Font("Segoe UI", 9f);

        public static Button CreateMenuButton(string text, bool isActive = false)
        {
            var btn = new Button
            {
                Text = text,
                Size = new Size(230, 45),
                Dock = DockStyle.Top,
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0),
                BackColor = isActive ? UIColors.AccentBlue : UIColors.SidebarBackground,
                ForeColor = isActive ? UIColors.White : UIColors.SidebarText,
                Font = isActive ? MenuButtonActiveFont : MenuButtonFont,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;

            btn.MouseEnter += (s, e) => btn.BackColor = isActive ? UIColors.AccentBlue : Color.FromArgb(45, 48, 55);
            btn.MouseLeave += (s, e) => btn.BackColor = isActive ? UIColors.AccentBlue : UIColors.SidebarBackground;

            return btn;
        }
    }

    public static class UIAssets
    {
        public static Image CreateIcon(string letter)
        {
            Bitmap bmp = new(50, 50);
            using Graphics g = Graphics.FromImage(bmp);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.Transparent);
            using Brush b = new SolidBrush(UIColors.AccentBlue);
            g.FillEllipse(b, 5, 5, 40, 40);
            using Font f = new Font("Segoe UI", 14, FontStyle.Bold);
            g.DrawString(letter, f, Brushes.White, new PointF(13, 10));
            return bmp;
        }
    }
}
