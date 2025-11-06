namespace VIS_Projekt
{
    partial class Form2
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
            label1 = new Label();
            comboBoxCompanies = new ComboBox();
            lstProducts = new ListBox();
            labelProductCount = new Label();
            btnBack = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(359, 147);
            label1.Name = "label1";
            label1.Size = new Size(197, 32);
            label1.TabIndex = 0;
            label1.Text = "Vybrat zákazníka:";
            // 
            // comboBoxCompanies
            // 
            comboBoxCompanies.FormattingEnabled = true;
            comboBoxCompanies.Location = new Point(314, 182);
            comboBoxCompanies.Name = "comboBoxCompanies";
            comboBoxCompanies.Size = new Size(272, 40);
            comboBoxCompanies.TabIndex = 1;
            comboBoxCompanies.SelectedIndexChanged += comboBoxCompanies_SelectedIndexChanged;
            // 
            // lstProducts
            // 
            lstProducts.FormattingEnabled = true;
            lstProducts.Location = new Point(184, 252);
            lstProducts.Name = "lstProducts";
            lstProducts.Size = new Size(550, 484);
            lstProducts.TabIndex = 2;
            lstProducts.SelectedIndexChanged += lstProducts_SelectedIndexChanged;
            // 
            // labelProductCount
            // 
            labelProductCount.AutoSize = true;
            labelProductCount.Location = new Point(337, 762);
            labelProductCount.Name = "labelProductCount";
            labelProductCount.Size = new Size(202, 32);
            labelProductCount.TabIndex = 3;
            labelProductCount.Text = "Počet produktů: 0";
            // 
            // btnBack
            // 
            btnBack.Location = new Point(371, 807);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(150, 46);
            btnBack.TabIndex = 4;
            btnBack.Text = "Zpět";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(923, 985);
            Controls.Add(btnBack);
            Controls.Add(labelProductCount);
            Controls.Add(lstProducts);
            Controls.Add(comboBoxCompanies);
            Controls.Add(label1);
            Name = "Form2";
            Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox comboBoxCompanies;
        private ListBox lstProducts;
        private Label labelProductCount;
        private Button btnBack;
    }
}