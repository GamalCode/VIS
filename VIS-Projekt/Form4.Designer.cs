namespace VIS_Projekt
{
    partial class Form4
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
            btnLogin = new Button();
            label1 = new Label();
            label2 = new Label();
            txtName = new TextBox();
            txtPassword = new TextBox();
            SuspendLayout();
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(242, 326);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(286, 51);
            btnLogin.TabIndex = 0;
            btnLogin.Text = "Přihlásit se";
            btnLogin.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(191, 161);
            label1.Name = "label1";
            label1.Size = new Size(90, 32);
            label1.TabIndex = 1;
            label1.Text = "Jméno:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(191, 236);
            label2.Name = "label2";
            label2.Size = new Size(79, 32);
            label2.TabIndex = 2;
            label2.Text = "Heslo:";
            // 
            // txtName
            // 
            txtName.Location = new Point(374, 161);
            txtName.Name = "txtName";
            txtName.Size = new Size(320, 39);
            txtName.TabIndex = 3;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(374, 229);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(320, 39);
            txtPassword.TabIndex = 4;
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(844, 508);
            Controls.Add(txtPassword);
            Controls.Add(txtName);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnLogin);
            Name = "Form4";
            Text = "Form4";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnLogin;
        private Label label1;
        private Label label2;
        private TextBox txtName;
        private TextBox txtPassword;
    }
}