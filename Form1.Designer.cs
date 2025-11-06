namespace VIS_Projekt
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtCompanyName = new TextBox();
            txtEmail = new TextBox();
            txtPhone = new TextBox();
            btnAddCompany = new Button();
            lstCompanies = new ListBox();
            lstSuppliers = new ListBox();
            btnAddSupplier = new Button();
            txtSupplierPhone = new TextBox();
            txtSupplierName = new TextBox();
            label4 = new Label();
            label6 = new Label();
            label7 = new Label();
            txtSupplierEmail = new TextBox();
            txtSupplierId = new TextBox();
            label8 = new Label();
            lstProducts = new ListBox();
            btnAddProduct = new Button();
            txtCarModel = new TextBox();
            txtProductType = new TextBox();
            txtProductName = new TextBox();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            txtPrice = new TextBox();
            label12 = new Label();
            btnOpen = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(138, 47);
            label1.Name = "label1";
            label1.Size = new Size(147, 32);
            label1.TabIndex = 0;
            label1.Text = "Název firmy:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(138, 99);
            label2.Name = "label2";
            label2.Size = new Size(76, 32);
            label2.TabIndex = 1;
            label2.Text = "Email:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(138, 157);
            label3.Name = "label3";
            label3.Size = new Size(98, 32);
            label3.TabIndex = 2;
            label3.Text = "Telefon:";
            // 
            // txtCompanyName
            // 
            txtCompanyName.Location = new Point(314, 44);
            txtCompanyName.Name = "txtCompanyName";
            txtCompanyName.Size = new Size(322, 39);
            txtCompanyName.TabIndex = 3;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(314, 99);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(322, 39);
            txtEmail.TabIndex = 4;
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(314, 154);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(322, 39);
            txtPhone.TabIndex = 5;
            // 
            // btnAddCompany
            // 
            btnAddCompany.Location = new Point(138, 336);
            btnAddCompany.Name = "btnAddCompany";
            btnAddCompany.Size = new Size(178, 46);
            btnAddCompany.TabIndex = 6;
            btnAddCompany.Text = "Přidat firmu";
            btnAddCompany.UseVisualStyleBackColor = true;
            btnAddCompany.Click += btnAddCompany_Click;
            // 
            // lstCompanies
            // 
            lstCompanies.FormattingEnabled = true;
            lstCompanies.Location = new Point(27, 398);
            lstCompanies.Name = "lstCompanies";
            lstCompanies.Size = new Size(609, 548);
            lstCompanies.TabIndex = 7;
            // 
            // lstSuppliers
            // 
            lstSuppliers.FormattingEnabled = true;
            lstSuppliers.Location = new Point(650, 398);
            lstSuppliers.Name = "lstSuppliers";
            lstSuppliers.Size = new Size(609, 548);
            lstSuppliers.TabIndex = 15;
            // 
            // btnAddSupplier
            // 
            btnAddSupplier.Location = new Point(761, 336);
            btnAddSupplier.Name = "btnAddSupplier";
            btnAddSupplier.Size = new Size(179, 46);
            btnAddSupplier.TabIndex = 14;
            btnAddSupplier.Text = "Přidat výrobce";
            btnAddSupplier.UseVisualStyleBackColor = true;
            btnAddSupplier.Click += btnAddSupplier_Click;
            // 
            // txtSupplierPhone
            // 
            txtSupplierPhone.Location = new Point(937, 102);
            txtSupplierPhone.Name = "txtSupplierPhone";
            txtSupplierPhone.Size = new Size(322, 39);
            txtSupplierPhone.TabIndex = 13;
            // 
            // txtSupplierName
            // 
            txtSupplierName.Location = new Point(937, 47);
            txtSupplierName.Name = "txtSupplierName";
            txtSupplierName.Size = new Size(322, 39);
            txtSupplierName.TabIndex = 11;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(761, 105);
            label4.Name = "label4";
            label4.Size = new Size(98, 32);
            label4.TabIndex = 10;
            label4.Text = "Telefon:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(761, 50);
            label6.Name = "label6";
            label6.Size = new Size(85, 32);
            label6.TabIndex = 8;
            label6.Text = "Název:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(763, 156);
            label7.Name = "label7";
            label7.Size = new Size(76, 32);
            label7.TabIndex = 16;
            label7.Text = "Email:";
            // 
            // txtSupplierEmail
            // 
            txtSupplierEmail.Location = new Point(937, 156);
            txtSupplierEmail.Name = "txtSupplierEmail";
            txtSupplierEmail.Size = new Size(322, 39);
            txtSupplierEmail.TabIndex = 17;
            // 
            // txtSupplierId
            // 
            txtSupplierId.Location = new Point(1563, 211);
            txtSupplierId.Name = "txtSupplierId";
            txtSupplierId.Size = new Size(322, 39);
            txtSupplierId.TabIndex = 27;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(1389, 211);
            label8.Name = "label8";
            label8.Size = new Size(157, 32);
            label8.TabIndex = 26;
            label8.Text = "Dodavatel ID:";
            // 
            // lstProducts
            // 
            lstProducts.FormattingEnabled = true;
            lstProducts.Location = new Point(1276, 398);
            lstProducts.Name = "lstProducts";
            lstProducts.Size = new Size(609, 548);
            lstProducts.TabIndex = 25;
            // 
            // btnAddProduct
            // 
            btnAddProduct.Location = new Point(1387, 336);
            btnAddProduct.Name = "btnAddProduct";
            btnAddProduct.Size = new Size(179, 46);
            btnAddProduct.TabIndex = 24;
            btnAddProduct.Text = "Přidat produkt";
            btnAddProduct.UseVisualStyleBackColor = true;
            btnAddProduct.Click += btnAddProduct_Click;
            // 
            // txtCarModel
            // 
            txtCarModel.Location = new Point(1563, 157);
            txtCarModel.Name = "txtCarModel";
            txtCarModel.Size = new Size(322, 39);
            txtCarModel.TabIndex = 23;
            // 
            // txtProductType
            // 
            txtProductType.Location = new Point(1563, 102);
            txtProductType.Name = "txtProductType";
            txtProductType.Size = new Size(322, 39);
            txtProductType.TabIndex = 22;
            // 
            // txtProductName
            // 
            txtProductName.Location = new Point(1563, 47);
            txtProductName.Name = "txtProductName";
            txtProductName.Size = new Size(322, 39);
            txtProductName.TabIndex = 21;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(1387, 160);
            label9.Name = "label9";
            label9.Size = new Size(144, 32);
            label9.TabIndex = 20;
            label9.Text = "Model Auta:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(1387, 102);
            label10.Name = "label10";
            label10.Size = new Size(57, 32);
            label10.TabIndex = 19;
            label10.Text = "Typ:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(1387, 50);
            label11.Name = "label11";
            label11.Size = new Size(85, 32);
            label11.TabIndex = 18;
            label11.Text = "Název:";
            // 
            // txtPrice
            // 
            txtPrice.Location = new Point(1563, 267);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(322, 39);
            txtPrice.TabIndex = 29;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(1389, 267);
            label12.Name = "label12";
            label12.Size = new Size(73, 32);
            label12.TabIndex = 28;
            label12.Text = "Cena:";
            // 
            // btnOpen
            // 
            btnOpen.Location = new Point(830, 963);
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new Size(220, 46);
            btnOpen.TabIndex = 30;
            btnOpen.Text = "Vybrat Firmu";
            btnOpen.UseVisualStyleBackColor = true;
            btnOpen.Click += btnOpen_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1920, 1033);
            Controls.Add(btnOpen);
            Controls.Add(txtPrice);
            Controls.Add(label12);
            Controls.Add(txtSupplierId);
            Controls.Add(label8);
            Controls.Add(lstProducts);
            Controls.Add(btnAddProduct);
            Controls.Add(txtCarModel);
            Controls.Add(txtProductType);
            Controls.Add(txtProductName);
            Controls.Add(label9);
            Controls.Add(label10);
            Controls.Add(label11);
            Controls.Add(txtSupplierEmail);
            Controls.Add(label7);
            Controls.Add(lstSuppliers);
            Controls.Add(btnAddSupplier);
            Controls.Add(txtSupplierPhone);
            Controls.Add(txtSupplierName);
            Controls.Add(label4);
            Controls.Add(label6);
            Controls.Add(lstCompanies);
            Controls.Add(btnAddCompany);
            Controls.Add(txtPhone);
            Controls.Add(txtEmail);
            Controls.Add(txtCompanyName);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtCompanyName;
        private TextBox txtEmail;
        private TextBox txtPhone;
        private Button btnAddCompany;
        private ListBox lstCompanies;
        private ListBox lstSuppliers;
        private Button btnAddSupplier;
        private TextBox txtSupplierPhone;
        private TextBox txtSupplierName;
        private Label label4;
        private Label label6;
        private Label label7;
        private TextBox txtSupplierEmail;
        private TextBox txtSupplierId;
        private Label label8;
        private ListBox lstProducts;
        private Button btnAddProduct;
        private TextBox txtCarModel;
        private TextBox txtProductType;
        private TextBox txtProductName;
        private Label label9;
        private Label label10;
        private Label label11;
        private TextBox txtPrice;
        private Label label12;
        private Button btnOpen;
    }
}
