namespace VIS_Projekt
{
    partial class Form3
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
            cbChooseStorage = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            lstCompanyProducts = new ListBox();
            lstSupplierProducts = new ListBox();
            cbChooseCompany = new ComboBox();
            cbChooseSupplier = new ComboBox();
            btnRequestForm = new Button();
            SuspendLayout();
            // 
            // cbChooseStorage
            // 
            cbChooseStorage.FormattingEnabled = true;
            cbChooseStorage.Location = new Point(177, 6);
            cbChooseStorage.Name = "cbChooseStorage";
            cbChooseStorage.Size = new Size(220, 40);
            cbChooseStorage.TabIndex = 3;
            cbChooseStorage.SelectedIndexChanged += cbChooseStorage_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(145, 32);
            label1.TabIndex = 2;
            label1.Text = "Vyber Sklad:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(442, 138);
            label2.Name = "label2";
            label2.Size = new Size(170, 32);
            label2.TabIndex = 4;
            label2.Text = "Zobrazit firmu:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(1644, 138);
            label3.Name = "label3";
            label3.Size = new Size(234, 32);
            label3.TabIndex = 5;
            label3.Text = "Zobrazit Dodavatele:";
            // 
            // lstCompanyProducts
            // 
            lstCompanyProducts.FormattingEnabled = true;
            lstCompanyProducts.Location = new Point(12, 283);
            lstCompanyProducts.Name = "lstCompanyProducts";
            lstCompanyProducts.Size = new Size(1095, 612);
            lstCompanyProducts.TabIndex = 6;
            // 
            // lstSupplierProducts
            // 
            lstSupplierProducts.FormattingEnabled = true;
            lstSupplierProducts.Location = new Point(1201, 283);
            lstSupplierProducts.Name = "lstSupplierProducts";
            lstSupplierProducts.Size = new Size(1095, 612);
            lstSupplierProducts.TabIndex = 7;
            // 
            // cbChooseCompany
            // 
            cbChooseCompany.FormattingEnabled = true;
            cbChooseCompany.Location = new Point(414, 187);
            cbChooseCompany.Name = "cbChooseCompany";
            cbChooseCompany.Size = new Size(220, 40);
            cbChooseCompany.TabIndex = 8;
            cbChooseCompany.SelectedIndexChanged += cbChooseCompany_SelectedIndexChanged;
            // 
            // cbChooseSupplier
            // 
            cbChooseSupplier.FormattingEnabled = true;
            cbChooseSupplier.Location = new Point(1645, 187);
            cbChooseSupplier.Name = "cbChooseSupplier";
            cbChooseSupplier.Size = new Size(220, 40);
            cbChooseSupplier.TabIndex = 9;
            cbChooseSupplier.SelectedIndexChanged += cbChooseSupplier_SelectedIndexChanged;
            // 
            // btnRequestForm
            // 
            btnRequestForm.Location = new Point(1021, 64);
            btnRequestForm.Name = "btnRequestForm";
            btnRequestForm.Size = new Size(262, 46);
            btnRequestForm.TabIndex = 10;
            btnRequestForm.Text = "Vytvořit objednávku";
            btnRequestForm.UseVisualStyleBackColor = true;
            btnRequestForm.Click += btnRequestForm_Click;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2308, 907);
            Controls.Add(btnRequestForm);
            Controls.Add(cbChooseSupplier);
            Controls.Add(cbChooseCompany);
            Controls.Add(lstSupplierProducts);
            Controls.Add(lstCompanyProducts);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(cbChooseStorage);
            Controls.Add(label1);
            Name = "Form3";
            Text = "Form3";
            Load += Form3_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cbChooseStorage;
        private Label label1;
        private Label label2;
        private Label label3;
        private ListBox lstCompanyProducts;
        private ListBox lstSupplierProducts;
        private ComboBox cbChooseCompany;
        private ComboBox cbChooseSupplier;
        private Button btnRequestForm;
    }
}