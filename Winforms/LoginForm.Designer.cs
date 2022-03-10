namespace Winforms
{
    partial class LoginForm
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
            this.btnLogin = new System.Windows.Forms.Button();
            this.rdBtnVisitor = new System.Windows.Forms.RadioButton();
            this.rdBtnUser = new System.Windows.Forms.RadioButton();
            this.rdBtnAdmin = new System.Windows.Forms.RadioButton();
            this.txtBoxUserName = new System.Windows.Forms.TextBox();
            this.txtBoxPassword = new System.Windows.Forms.TextBox();
            this.lbUserName = new System.Windows.Forms.Label();
            this.lbPassword = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLogin.Font = new System.Drawing.Font("宋体", 9F);
            this.btnLogin.Location = new System.Drawing.Point(216, 276);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 25);
            this.btnLogin.TabIndex = 0;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // rdBtnVisitor
            // 
            this.rdBtnVisitor.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rdBtnVisitor.AutoSize = true;
            this.rdBtnVisitor.Location = new System.Drawing.Point(167, 231);
            this.rdBtnVisitor.Name = "rdBtnVisitor";
            this.rdBtnVisitor.Size = new System.Drawing.Size(58, 19);
            this.rdBtnVisitor.TabIndex = 1;
            this.rdBtnVisitor.TabStop = true;
            this.rdBtnVisitor.Text = "游客";
            this.rdBtnVisitor.UseVisualStyleBackColor = true;
            // 
            // rdBtnUser
            // 
            this.rdBtnUser.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rdBtnUser.AutoSize = true;
            this.rdBtnUser.Location = new System.Drawing.Point(231, 231);
            this.rdBtnUser.Name = "rdBtnUser";
            this.rdBtnUser.Size = new System.Drawing.Size(88, 19);
            this.rdBtnUser.TabIndex = 2;
            this.rdBtnUser.TabStop = true;
            this.rdBtnUser.Text = "用户模式";
            this.rdBtnUser.UseVisualStyleBackColor = true;
            // 
            // rdBtnAdmin
            // 
            this.rdBtnAdmin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rdBtnAdmin.AutoSize = true;
            this.rdBtnAdmin.Location = new System.Drawing.Point(325, 231);
            this.rdBtnAdmin.Name = "rdBtnAdmin";
            this.rdBtnAdmin.Size = new System.Drawing.Size(103, 19);
            this.rdBtnAdmin.TabIndex = 3;
            this.rdBtnAdmin.TabStop = true;
            this.rdBtnAdmin.Text = "管理员模式";
            this.rdBtnAdmin.UseVisualStyleBackColor = true;
            // 
            // txtBoxUserName
            // 
            this.txtBoxUserName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtBoxUserName.Location = new System.Drawing.Point(239, 117);
            this.txtBoxUserName.Multiline = true;
            this.txtBoxUserName.Name = "txtBoxUserName";
            this.txtBoxUserName.Size = new System.Drawing.Size(184, 30);
            this.txtBoxUserName.TabIndex = 4;
            // 
            // txtBoxPassword
            // 
            this.txtBoxPassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtBoxPassword.Location = new System.Drawing.Point(239, 174);
            this.txtBoxPassword.Multiline = true;
            this.txtBoxPassword.Name = "txtBoxPassword";
            this.txtBoxPassword.PasswordChar = '*';
            this.txtBoxPassword.Size = new System.Drawing.Size(184, 30);
            this.txtBoxPassword.TabIndex = 5;
            // 
            // lbUserName
            // 
            this.lbUserName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbUserName.AutoSize = true;
            this.lbUserName.Font = new System.Drawing.Font("宋体", 10F);
            this.lbUserName.Location = new System.Drawing.Point(159, 123);
            this.lbUserName.Name = "lbUserName";
            this.lbUserName.Size = new System.Drawing.Size(76, 17);
            this.lbUserName.TabIndex = 6;
            this.lbUserName.Text = "用户名：";
            // 
            // lbPassword
            // 
            this.lbPassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbPassword.AutoSize = true;
            this.lbPassword.Font = new System.Drawing.Font("宋体", 10F);
            this.lbPassword.Location = new System.Drawing.Point(172, 180);
            this.lbPassword.Name = "lbPassword";
            this.lbPassword.Size = new System.Drawing.Size(59, 17);
            this.lbPassword.TabIndex = 7;
            this.lbPassword.Text = "密码：";
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLogout.Font = new System.Drawing.Font("宋体", 9F);
            this.btnLogout.Location = new System.Drawing.Point(309, 276);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(75, 25);
            this.btnLogout.TabIndex = 8;
            this.btnLogout.Text = "注销";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 385);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.lbPassword);
            this.Controls.Add(this.lbUserName);
            this.Controls.Add(this.txtBoxPassword);
            this.Controls.Add(this.txtBoxUserName);
            this.Controls.Add(this.rdBtnAdmin);
            this.Controls.Add(this.rdBtnUser);
            this.Controls.Add(this.rdBtnVisitor);
            this.Controls.Add(this.btnLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoginForm";
            this.Text = "登录界面";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.RadioButton rdBtnVisitor;
        private System.Windows.Forms.RadioButton rdBtnUser;
        private System.Windows.Forms.RadioButton rdBtnAdmin;
        private System.Windows.Forms.TextBox txtBoxUserName;
        private System.Windows.Forms.TextBox txtBoxPassword;
        private System.Windows.Forms.Label lbUserName;
        private System.Windows.Forms.Label lbPassword;
        private System.Windows.Forms.Button btnLogout;
    }
}