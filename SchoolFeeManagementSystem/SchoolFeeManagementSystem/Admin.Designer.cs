namespace SchoolFeeManagementSystem
{
    partial class Admin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Admin));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panelMenu = new Panel();
            btnStaffManagement = new FontAwesome.Sharp.IconButton();
            label3 = new Label();
            label1 = new Label();
            pictureBoxLogo = new PictureBox();
            btnLogout = new FontAwesome.Sharp.IconButton();
            btnReportSection = new FontAwesome.Sharp.IconButton();
            btnSetting = new FontAwesome.Sharp.IconButton();
            btnPaymentManagement = new FontAwesome.Sharp.IconButton();
            btnFeeSection = new FontAwesome.Sharp.IconButton();
            btnStudentManagement = new FontAwesome.Sharp.IconButton();
            btnDashboard = new FontAwesome.Sharp.IconButton();
            panelTitleBar = new Panel();
            guna2Panel13 = new Guna.UI2.WinForms.Guna2Panel();
            iconeExit = new FontAwesome.Sharp.IconPictureBox();
            IconeMaximaize = new FontAwesome.Sharp.IconPictureBox();
            IconeMinimaize = new FontAwesome.Sharp.IconPictureBox();
            label2 = new Label();
            HomeTitle = new Label();
            iconButtonDashboard = new FontAwesome.Sharp.IconButton();
            panelDesktopPane = new Panel();
            pictureBox1 = new PictureBox();
            panelMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).BeginInit();
            panelTitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconeExit).BeginInit();
            ((System.ComponentModel.ISupportInitialize)IconeMaximaize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)IconeMinimaize).BeginInit();
            panelDesktopPane.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.FromArgb(26, 26, 46);
            panelMenu.Controls.Add(btnStaffManagement);
            panelMenu.Controls.Add(label3);
            panelMenu.Controls.Add(label1);
            panelMenu.Controls.Add(pictureBoxLogo);
            panelMenu.Controls.Add(btnLogout);
            panelMenu.Controls.Add(btnReportSection);
            panelMenu.Controls.Add(btnSetting);
            panelMenu.Controls.Add(btnPaymentManagement);
            panelMenu.Controls.Add(btnFeeSection);
            panelMenu.Controls.Add(btnStudentManagement);
            panelMenu.Controls.Add(btnDashboard);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(336, 836);
            panelMenu.TabIndex = 0;
            // 
            // btnStaffManagement
            // 
            btnStaffManagement.FlatAppearance.BorderSize = 0;
            btnStaffManagement.FlatStyle = FlatStyle.Flat;
            btnStaffManagement.Font = new Font("Times New Roman", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnStaffManagement.ForeColor = Color.Gainsboro;
            btnStaffManagement.IconChar = FontAwesome.Sharp.IconChar.Star;
            btnStaffManagement.IconColor = Color.White;
            btnStaffManagement.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnStaffManagement.ImageAlign = ContentAlignment.MiddleLeft;
            btnStaffManagement.Location = new Point(12, 397);
            btnStaffManagement.Name = "btnStaffManagement";
            btnStaffManagement.Padding = new Padding(10, 0, 20, 0);
            btnStaffManagement.Size = new Size(300, 60);
            btnStaffManagement.TabIndex = 6;
            btnStaffManagement.Text = "Staff Management";
            btnStaffManagement.UseVisualStyleBackColor = true;
            btnStaffManagement.Click += btnStaffManagement_Click;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Times New Roman", 9.75F);
            label3.ForeColor = Color.White;
            label3.Location = new Point(70, 784);
            label3.Name = "label3";
            label3.Size = new Size(174, 15);
            label3.TabIndex = 5;
            label3.Text = "School Fee Managemet System";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 9.75F);
            label1.ForeColor = Color.White;
            label1.Location = new Point(70, 808);
            label1.Name = "label1";
            label1.Size = new Size(155, 15);
            label1.TabIndex = 4;
            label1.Text = "2026 @ All Rights Reserved";
            // 
            // pictureBoxLogo
            // 
            pictureBoxLogo.Image = (Image)resources.GetObject("pictureBoxLogo.Image");
            pictureBoxLogo.Location = new Point(12, 25);
            pictureBoxLogo.Name = "pictureBoxLogo";
            pictureBoxLogo.Size = new Size(300, 205);
            pictureBoxLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxLogo.TabIndex = 1;
            pictureBoxLogo.TabStop = false;
            pictureBoxLogo.UseWaitCursor = true;
            // 
            // btnLogout
            // 
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Times New Roman", 14.25F, FontStyle.Bold);
            btnLogout.ForeColor = Color.Gainsboro;
            btnLogout.IconChar = FontAwesome.Sharp.IconChar.SignOut;
            btnLogout.IconColor = Color.White;
            btnLogout.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnLogout.ImageAlign = ContentAlignment.MiddleLeft;
            btnLogout.Location = new Point(11, 725);
            btnLogout.Name = "btnLogout";
            btnLogout.Padding = new Padding(10, 0, 20, 0);
            btnLogout.RightToLeft = RightToLeft.No;
            btnLogout.Size = new Size(301, 56);
            btnLogout.TabIndex = 3;
            btnLogout.Text = "Logout";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            // 
            // btnReportSection
            // 
            btnReportSection.FlatAppearance.BorderSize = 0;
            btnReportSection.FlatStyle = FlatStyle.Flat;
            btnReportSection.Font = new Font("Times New Roman", 14.25F, FontStyle.Bold);
            btnReportSection.ForeColor = Color.Gainsboro;
            btnReportSection.IconChar = FontAwesome.Sharp.IconChar.File;
            btnReportSection.IconColor = Color.White;
            btnReportSection.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnReportSection.ImageAlign = ContentAlignment.MiddleLeft;
            btnReportSection.Location = new Point(12, 591);
            btnReportSection.Name = "btnReportSection";
            btnReportSection.Padding = new Padding(10, 0, 20, 0);
            btnReportSection.RightToLeft = RightToLeft.No;
            btnReportSection.Size = new Size(300, 61);
            btnReportSection.TabIndex = 3;
            btnReportSection.Text = "Report Section";
            btnReportSection.UseVisualStyleBackColor = true;
            btnReportSection.Click += btnReportSection_Click;
            // 
            // btnSetting
            // 
            btnSetting.FlatAppearance.BorderSize = 0;
            btnSetting.FlatStyle = FlatStyle.Flat;
            btnSetting.Font = new Font("Times New Roman", 14.25F, FontStyle.Bold);
            btnSetting.ForeColor = Color.Gainsboro;
            btnSetting.IconChar = FontAwesome.Sharp.IconChar.Cog;
            btnSetting.IconColor = Color.White;
            btnSetting.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnSetting.ImageAlign = ContentAlignment.MiddleLeft;
            btnSetting.Location = new Point(12, 658);
            btnSetting.Name = "btnSetting";
            btnSetting.Padding = new Padding(10, 0, 20, 0);
            btnSetting.RightToLeft = RightToLeft.No;
            btnSetting.Size = new Size(300, 61);
            btnSetting.TabIndex = 3;
            btnSetting.Text = "Setting";
            btnSetting.UseVisualStyleBackColor = true;
            btnSetting.Click += btnSetting_Click;
            // 
            // btnPaymentManagement
            // 
            btnPaymentManagement.FlatAppearance.BorderSize = 0;
            btnPaymentManagement.FlatStyle = FlatStyle.Flat;
            btnPaymentManagement.Font = new Font("Times New Roman", 14.25F, FontStyle.Bold);
            btnPaymentManagement.ForeColor = Color.Gainsboro;
            btnPaymentManagement.IconChar = FontAwesome.Sharp.IconChar.CreditCard;
            btnPaymentManagement.IconColor = Color.White;
            btnPaymentManagement.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnPaymentManagement.ImageAlign = ContentAlignment.MiddleLeft;
            btnPaymentManagement.Location = new Point(12, 531);
            btnPaymentManagement.Name = "btnPaymentManagement";
            btnPaymentManagement.Padding = new Padding(10, 0, 20, 0);
            btnPaymentManagement.RightToLeft = RightToLeft.No;
            btnPaymentManagement.Size = new Size(300, 54);
            btnPaymentManagement.TabIndex = 3;
            btnPaymentManagement.Text = "Payment Management";
            btnPaymentManagement.UseVisualStyleBackColor = true;
            btnPaymentManagement.Click += btnPaymentManagement_Click;
            // 
            // btnFeeSection
            // 
            btnFeeSection.FlatAppearance.BorderSize = 0;
            btnFeeSection.FlatStyle = FlatStyle.Flat;
            btnFeeSection.Font = new Font("Times New Roman", 14.25F, FontStyle.Bold);
            btnFeeSection.ForeColor = Color.Gainsboro;
            btnFeeSection.IconChar = FontAwesome.Sharp.IconChar.Bitcoin;
            btnFeeSection.IconColor = Color.White;
            btnFeeSection.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnFeeSection.ImageAlign = ContentAlignment.MiddleLeft;
            btnFeeSection.Location = new Point(13, 463);
            btnFeeSection.Name = "btnFeeSection";
            btnFeeSection.Padding = new Padding(10, 0, 20, 0);
            btnFeeSection.RightToLeft = RightToLeft.No;
            btnFeeSection.Size = new Size(300, 62);
            btnFeeSection.TabIndex = 3;
            btnFeeSection.Text = "Fees Section";
            btnFeeSection.UseVisualStyleBackColor = true;
            btnFeeSection.Click += btnFeeSection_Click;
            // 
            // btnStudentManagement
            // 
            btnStudentManagement.FlatAppearance.BorderSize = 0;
            btnStudentManagement.FlatStyle = FlatStyle.Flat;
            btnStudentManagement.Font = new Font("Times New Roman", 14.25F, FontStyle.Bold);
            btnStudentManagement.ForeColor = Color.Gainsboro;
            btnStudentManagement.IconChar = FontAwesome.Sharp.IconChar.User;
            btnStudentManagement.IconColor = Color.White;
            btnStudentManagement.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnStudentManagement.ImageAlign = ContentAlignment.MiddleLeft;
            btnStudentManagement.Location = new Point(13, 332);
            btnStudentManagement.Name = "btnStudentManagement";
            btnStudentManagement.Padding = new Padding(10, 0, 20, 0);
            btnStudentManagement.RightToLeft = RightToLeft.No;
            btnStudentManagement.Size = new Size(300, 59);
            btnStudentManagement.TabIndex = 3;
            btnStudentManagement.Text = "Student Management";
            btnStudentManagement.UseVisualStyleBackColor = true;
            btnStudentManagement.Click += btnStudentManagement_Click;
            // 
            // btnDashboard
            // 
            btnDashboard.FlatAppearance.BorderSize = 0;
            btnDashboard.FlatStyle = FlatStyle.Flat;
            btnDashboard.Font = new Font("Times New Roman", 14.25F, FontStyle.Bold);
            btnDashboard.ForeColor = Color.Gainsboro;
            btnDashboard.IconChar = FontAwesome.Sharp.IconChar.House;
            btnDashboard.IconColor = Color.White;
            btnDashboard.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnDashboard.ImageAlign = ContentAlignment.MiddleLeft;
            btnDashboard.Location = new Point(13, 262);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.Padding = new Padding(10, 0, 20, 0);
            btnDashboard.RightToLeft = RightToLeft.No;
            btnDashboard.Size = new Size(300, 64);
            btnDashboard.TabIndex = 3;
            btnDashboard.Text = "Dashboard";
            btnDashboard.UseVisualStyleBackColor = true;
            btnDashboard.Click += btnDashboard_Click;
            // 
            // panelTitleBar
            // 
            panelTitleBar.BackColor = Color.FromArgb(26, 26, 46);
            panelTitleBar.Controls.Add(guna2Panel13);
            panelTitleBar.Controls.Add(iconeExit);
            panelTitleBar.Controls.Add(IconeMaximaize);
            panelTitleBar.Controls.Add(IconeMinimaize);
            panelTitleBar.Controls.Add(label2);
            panelTitleBar.Controls.Add(HomeTitle);
            panelTitleBar.Controls.Add(iconButtonDashboard);
            panelTitleBar.Dock = DockStyle.Top;
            panelTitleBar.Location = new Point(336, 0);
            panelTitleBar.Name = "panelTitleBar";
            panelTitleBar.Size = new Size(1473, 92);
            panelTitleBar.TabIndex = 1;
            // 
            // guna2Panel13
            // 
            guna2Panel13.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            guna2Panel13.BackColor = Color.DarkViolet;
            guna2Panel13.BorderRadius = 30;
            guna2Panel13.BorderThickness = 1;
            guna2Panel13.CustomizableEdges = customizableEdges1;
            guna2Panel13.Location = new Point(20, 79);
            guna2Panel13.Name = "guna2Panel13";
            guna2Panel13.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Panel13.Size = new Size(1441, 2);
            guna2Panel13.TabIndex = 27;
            // 
            // iconeExit
            // 
            iconeExit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            iconeExit.BackColor = Color.FromArgb(26, 26, 46);
            iconeExit.ForeColor = Color.Red;
            iconeExit.IconChar = FontAwesome.Sharp.IconChar.Close;
            iconeExit.IconColor = Color.Red;
            iconeExit.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconeExit.Location = new Point(1429, 12);
            iconeExit.Name = "iconeExit";
            iconeExit.Size = new Size(32, 32);
            iconeExit.TabIndex = 8;
            iconeExit.TabStop = false;
            iconeExit.Click += iconeExit_Click;
            // 
            // IconeMaximaize
            // 
            IconeMaximaize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            IconeMaximaize.BackColor = Color.FromArgb(26, 26, 46);
            IconeMaximaize.ForeColor = Color.Cyan;
            IconeMaximaize.IconChar = FontAwesome.Sharp.IconChar.WindowMaximize;
            IconeMaximaize.IconColor = Color.Cyan;
            IconeMaximaize.IconFont = FontAwesome.Sharp.IconFont.Auto;
            IconeMaximaize.Location = new Point(1391, 12);
            IconeMaximaize.Name = "IconeMaximaize";
            IconeMaximaize.Size = new Size(32, 32);
            IconeMaximaize.TabIndex = 7;
            IconeMaximaize.TabStop = false;
            IconeMaximaize.Click += IconeMaximaize_Click;
            // 
            // IconeMinimaize
            // 
            IconeMinimaize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            IconeMinimaize.BackColor = Color.FromArgb(26, 26, 46);
            IconeMinimaize.IconChar = FontAwesome.Sharp.IconChar.WindowMinimize;
            IconeMinimaize.IconColor = Color.White;
            IconeMinimaize.IconFont = FontAwesome.Sharp.IconFont.Auto;
            IconeMinimaize.Location = new Point(1353, 12);
            IconeMinimaize.Name = "IconeMinimaize";
            IconeMinimaize.Size = new Size(32, 32);
            IconeMinimaize.TabIndex = 6;
            IconeMinimaize.TabStop = false;
            IconeMinimaize.Click += IconeMinimaize_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(590, 25);
            label2.Name = "label2";
            label2.Size = new Size(473, 36);
            label2.TabIndex = 5;
            label2.Text = "Student Fee Management System";
            // 
            // HomeTitle
            // 
            HomeTitle.AutoSize = true;
            HomeTitle.Font = new Font("Times New Roman", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            HomeTitle.ForeColor = Color.White;
            HomeTitle.Location = new Point(116, 38);
            HomeTitle.Name = "HomeTitle";
            HomeTitle.Size = new Size(83, 31);
            HomeTitle.TabIndex = 4;
            HomeTitle.Text = "Home";
            // 
            // iconButtonDashboard
            // 
            iconButtonDashboard.FlatAppearance.BorderSize = 0;
            iconButtonDashboard.FlatStyle = FlatStyle.Flat;
            iconButtonDashboard.Font = new Font("Times New Roman", 14.25F, FontStyle.Bold);
            iconButtonDashboard.ForeColor = Color.Gainsboro;
            iconButtonDashboard.IconChar = FontAwesome.Sharp.IconChar.House;
            iconButtonDashboard.IconColor = Color.White;
            iconButtonDashboard.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButtonDashboard.ImageAlign = ContentAlignment.MiddleLeft;
            iconButtonDashboard.Location = new Point(34, 25);
            iconButtonDashboard.Name = "iconButtonDashboard";
            iconButtonDashboard.RightToLeft = RightToLeft.No;
            iconButtonDashboard.Size = new Size(62, 44);
            iconButtonDashboard.TabIndex = 3;
            iconButtonDashboard.UseVisualStyleBackColor = true;
            // 
            // panelDesktopPane
            // 
            panelDesktopPane.BackColor = Color.FromArgb(26, 26, 46);
            panelDesktopPane.Controls.Add(pictureBox1);
            panelDesktopPane.Dock = DockStyle.Fill;
            panelDesktopPane.Location = new Point(336, 92);
            panelDesktopPane.Name = "panelDesktopPane";
            panelDesktopPane.Size = new Size(1473, 744);
            panelDesktopPane.TabIndex = 2;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(6, -10);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1464, 741);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // Admin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1809, 836);
            Controls.Add(panelDesktopPane);
            Controls.Add(panelTitleBar);
            Controls.Add(panelMenu);
            Name = "Admin";
            Text = "Admin";
            panelMenu.ResumeLayout(false);
            panelMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).EndInit();
            panelTitleBar.ResumeLayout(false);
            panelTitleBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconeExit).EndInit();
            ((System.ComponentModel.ISupportInitialize)IconeMaximaize).EndInit();
            ((System.ComponentModel.ISupportInitialize)IconeMinimaize).EndInit();
            panelDesktopPane.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelMenu;
        private Panel panelTitleBar;
        private Panel panelDesktopPane;
        private FontAwesome.Sharp.IconButton btnDashboard;
        private FontAwesome.Sharp.IconButton btnReportSection;
        private FontAwesome.Sharp.IconButton btnSetting;
        private FontAwesome.Sharp.IconButton btnPaymentManagement;
        private FontAwesome.Sharp.IconButton btnFeeSection;
        private FontAwesome.Sharp.IconButton btnStudentManagement;
        private FontAwesome.Sharp.IconButton btnLogout;
        private PictureBox pictureBoxLogo;
        private FontAwesome.Sharp.IconButton iconButtonDashboard;
        private Label HomeTitle;
        private Label label2;
        private FontAwesome.Sharp.IconPictureBox iconeExit;
        private FontAwesome.Sharp.IconPictureBox IconeMaximaize;
        private FontAwesome.Sharp.IconPictureBox IconeMinimaize;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel13;
        private Label label1;
        private Label label3;
        private PictureBox pictureBox1;
        private FontAwesome.Sharp.IconButton btnStaffManagement;
    }
}