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
            cbChooseStorage = new ComboBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            label15 = new Label();
            label16 = new Label();
            txtCompanyName = new TextBox();
            txtCompanyEmail = new TextBox();
            txtCompanyPhone = new TextBox();
            txtSupplierPhone = new TextBox();
            txtSupplierEmail = new TextBox();
            txtSupplierName = new TextBox();
            txtStockLocationInStorage = new TextBox();
            txtStockStorageID = new TextBox();
            txtStockProductID = new TextBox();
            txtProductModelCar = new TextBox();
            txtProductType = new TextBox();
            txtProductName = new TextBox();
            txtProductSupplierID = new TextBox();
            txtProductPrice = new TextBox();
            lstCompany = new ListBox();
            lstSupplier = new ListBox();
            lstProduct = new ListBox();
            lstStock = new ListBox();
            btnAddCompany = new Button();
            btnAddSupplier = new Button();
            btnAddProduct = new Button();
            btnAddStock = new Button();
            txtStockQuantity = new TextBox();
            label17 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 4);
            label1.Name = "label1";
            label1.Size = new Size(145, 32);
            label1.TabIndex = 0;
            label1.Text = "Vyber Sklad:";
            // 
            // cbChooseStorage
            // 
            cbChooseStorage.FormattingEnabled = true;
            cbChooseStorage.Location = new Point(176, 1);
            cbChooseStorage.Name = "cbChooseStorage";
            cbChooseStorage.Size = new Size(220, 40);
            cbChooseStorage.TabIndex = 1;
            cbChooseStorage.SelectedIndexChanged += cbChooseStorage_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 60);
            label2.Name = "label2";
            label2.Size = new Size(151, 32);
            label2.TabIndex = 2;
            label2.Text = "Název Firmy:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 119);
            label3.Name = "label3";
            label3.Size = new Size(76, 32);
            label3.TabIndex = 3;
            label3.Text = "Email:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 180);
            label4.Name = "label4";
            label4.Size = new Size(98, 32);
            label4.TabIndex = 4;
            label4.Text = "Telefon:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(518, 177);
            label5.Name = "label5";
            label5.Size = new Size(98, 32);
            label5.TabIndex = 7;
            label5.Text = "Telefon:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(518, 116);
            label6.Name = "label6";
            label6.Size = new Size(76, 32);
            label6.TabIndex = 6;
            label6.Text = "Email:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(518, 57);
            label7.Name = "label7";
            label7.Size = new Size(213, 32);
            label7.TabIndex = 5;
            label7.Text = "Název Dodavatele:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(1112, 177);
            label8.Name = "label8";
            label8.Size = new Size(144, 32);
            label8.TabIndex = 10;
            label8.Text = "Model Auta:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(1112, 116);
            label9.Name = "label9";
            label9.Size = new Size(57, 32);
            label9.TabIndex = 9;
            label9.Text = "Typ:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(1112, 57);
            label10.Name = "label10";
            label10.Size = new Size(189, 32);
            label10.TabIndex = 8;
            label10.Text = "Název Produktu:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(1672, 177);
            label11.Name = "label11";
            label11.Size = new Size(92, 32);
            label11.TabIndex = 13;
            label11.Text = "Lokace:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(1672, 116);
            label12.Name = "label12";
            label12.Size = new Size(120, 32);
            label12.TabIndex = 12;
            label12.Text = "ID Skladu:";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(1672, 57);
            label13.Name = "label13";
            label13.Size = new Size(146, 32);
            label13.TabIndex = 11;
            label13.Text = "ID Produktu:";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(1112, 283);
            label15.Name = "label15";
            label15.Size = new Size(170, 32);
            label15.TabIndex = 15;
            label15.Text = "ID Dodavatele:";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(1112, 230);
            label16.Name = "label16";
            label16.Size = new Size(73, 32);
            label16.TabIndex = 14;
            label16.Text = "Cena:";
            // 
            // txtCompanyName
            // 
            txtCompanyName.Location = new Point(218, 57);
            txtCompanyName.Name = "txtCompanyName";
            txtCompanyName.Size = new Size(200, 39);
            txtCompanyName.TabIndex = 17;
            // 
            // txtCompanyEmail
            // 
            txtCompanyEmail.Location = new Point(218, 119);
            txtCompanyEmail.Name = "txtCompanyEmail";
            txtCompanyEmail.Size = new Size(200, 39);
            txtCompanyEmail.TabIndex = 18;
            // 
            // txtCompanyPhone
            // 
            txtCompanyPhone.Location = new Point(218, 180);
            txtCompanyPhone.Name = "txtCompanyPhone";
            txtCompanyPhone.Size = new Size(200, 39);
            txtCompanyPhone.TabIndex = 19;
            // 
            // txtSupplierPhone
            // 
            txtSupplierPhone.Location = new Point(777, 180);
            txtSupplierPhone.Name = "txtSupplierPhone";
            txtSupplierPhone.Size = new Size(200, 39);
            txtSupplierPhone.TabIndex = 22;
            // 
            // txtSupplierEmail
            // 
            txtSupplierEmail.Location = new Point(777, 119);
            txtSupplierEmail.Name = "txtSupplierEmail";
            txtSupplierEmail.Size = new Size(200, 39);
            txtSupplierEmail.TabIndex = 21;
            // 
            // txtSupplierName
            // 
            txtSupplierName.Location = new Point(777, 57);
            txtSupplierName.Name = "txtSupplierName";
            txtSupplierName.Size = new Size(200, 39);
            txtSupplierName.TabIndex = 20;
            // 
            // txtStockLocationInStorage
            // 
            txtStockLocationInStorage.Location = new Point(1879, 177);
            txtStockLocationInStorage.Name = "txtStockLocationInStorage";
            txtStockLocationInStorage.Size = new Size(200, 39);
            txtStockLocationInStorage.TabIndex = 25;
            // 
            // txtStockStorageID
            // 
            txtStockStorageID.Location = new Point(1879, 116);
            txtStockStorageID.Name = "txtStockStorageID";
            txtStockStorageID.Size = new Size(200, 39);
            txtStockStorageID.TabIndex = 24;
            // 
            // txtStockProductID
            // 
            txtStockProductID.Location = new Point(1879, 54);
            txtStockProductID.Name = "txtStockProductID";
            txtStockProductID.Size = new Size(200, 39);
            txtStockProductID.TabIndex = 23;
            // 
            // txtProductModelCar
            // 
            txtProductModelCar.Location = new Point(1364, 180);
            txtProductModelCar.Name = "txtProductModelCar";
            txtProductModelCar.Size = new Size(200, 39);
            txtProductModelCar.TabIndex = 28;
            // 
            // txtProductType
            // 
            txtProductType.Location = new Point(1364, 119);
            txtProductType.Name = "txtProductType";
            txtProductType.Size = new Size(200, 39);
            txtProductType.TabIndex = 27;
            // 
            // txtProductName
            // 
            txtProductName.Location = new Point(1364, 57);
            txtProductName.Name = "txtProductName";
            txtProductName.Size = new Size(200, 39);
            txtProductName.TabIndex = 26;
            // 
            // txtProductSupplierID
            // 
            txtProductSupplierID.Location = new Point(1364, 286);
            txtProductSupplierID.Name = "txtProductSupplierID";
            txtProductSupplierID.Size = new Size(200, 39);
            txtProductSupplierID.TabIndex = 30;
            // 
            // txtProductPrice
            // 
            txtProductPrice.Location = new Point(1364, 230);
            txtProductPrice.Name = "txtProductPrice";
            txtProductPrice.Size = new Size(200, 39);
            txtProductPrice.TabIndex = 29;
            // 
            // lstCompany
            // 
            lstCompany.FormattingEnabled = true;
            lstCompany.Location = new Point(11, 464);
            lstCompany.Name = "lstCompany";
            lstCompany.Size = new Size(506, 452);
            lstCompany.TabIndex = 32;
            // 
            // lstSupplier
            // 
            lstSupplier.FormattingEnabled = true;
            lstSupplier.Location = new Point(577, 464);
            lstSupplier.Name = "lstSupplier";
            lstSupplier.Size = new Size(506, 452);
            lstSupplier.TabIndex = 33;
            // 
            // lstProduct
            // 
            lstProduct.FormattingEnabled = true;
            lstProduct.Location = new Point(1163, 464);
            lstProduct.Name = "lstProduct";
            lstProduct.Size = new Size(506, 452);
            lstProduct.TabIndex = 34;
            // 
            // lstStock
            // 
            lstStock.FormattingEnabled = true;
            lstStock.Location = new Point(1766, 464);
            lstStock.Name = "lstStock";
            lstStock.Size = new Size(506, 452);
            lstStock.TabIndex = 35;
            // 
            // btnAddCompany
            // 
            btnAddCompany.Location = new Point(155, 402);
            btnAddCompany.Name = "btnAddCompany";
            btnAddCompany.Size = new Size(210, 46);
            btnAddCompany.TabIndex = 36;
            btnAddCompany.Text = "Přidat Firmu:";
            btnAddCompany.UseVisualStyleBackColor = true;
            btnAddCompany.Click += btnAddCompany_Click;
            // 
            // btnAddSupplier
            // 
            btnAddSupplier.Location = new Point(720, 402);
            btnAddSupplier.Name = "btnAddSupplier";
            btnAddSupplier.Size = new Size(225, 46);
            btnAddSupplier.TabIndex = 37;
            btnAddSupplier.Text = "Přidat Dodavatele:";
            btnAddSupplier.UseVisualStyleBackColor = true;
            btnAddSupplier.Click += btnAddSupplier_Click;
            // 
            // btnAddProduct
            // 
            btnAddProduct.Location = new Point(1304, 402);
            btnAddProduct.Name = "btnAddProduct";
            btnAddProduct.Size = new Size(210, 46);
            btnAddProduct.TabIndex = 38;
            btnAddProduct.Text = "Přidat Produkt:";
            btnAddProduct.UseVisualStyleBackColor = true;
            btnAddProduct.Click += btnAddProduct_Click;
            // 
            // btnAddStock
            // 
            btnAddStock.Location = new Point(1922, 402);
            btnAddStock.Name = "btnAddStock";
            btnAddStock.Size = new Size(210, 46);
            btnAddStock.TabIndex = 39;
            btnAddStock.Text = "Přidat Stock:";
            btnAddStock.UseVisualStyleBackColor = true;
            btnAddStock.Click += btnAddStock_Click;
            // 
            // txtStockQuantity
            // 
            txtStockQuantity.Location = new Point(1879, 230);
            txtStockQuantity.Name = "txtStockQuantity";
            txtStockQuantity.Size = new Size(200, 39);
            txtStockQuantity.TabIndex = 41;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(1672, 230);
            label17.Name = "label17";
            label17.Size = new Size(104, 32);
            label17.TabIndex = 40;
            label17.Text = "Kvantita:";
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2336, 928);
            Controls.Add(txtStockQuantity);
            Controls.Add(label17);
            Controls.Add(btnAddStock);
            Controls.Add(btnAddProduct);
            Controls.Add(btnAddSupplier);
            Controls.Add(btnAddCompany);
            Controls.Add(lstStock);
            Controls.Add(lstProduct);
            Controls.Add(lstSupplier);
            Controls.Add(lstCompany);
            Controls.Add(txtProductSupplierID);
            Controls.Add(txtProductPrice);
            Controls.Add(txtProductModelCar);
            Controls.Add(txtProductType);
            Controls.Add(txtProductName);
            Controls.Add(txtStockLocationInStorage);
            Controls.Add(txtStockStorageID);
            Controls.Add(txtStockProductID);
            Controls.Add(txtSupplierPhone);
            Controls.Add(txtSupplierEmail);
            Controls.Add(txtSupplierName);
            Controls.Add(txtCompanyPhone);
            Controls.Add(txtCompanyEmail);
            Controls.Add(txtCompanyName);
            Controls.Add(label15);
            Controls.Add(label16);
            Controls.Add(label11);
            Controls.Add(label12);
            Controls.Add(label13);
            Controls.Add(label8);
            Controls.Add(label9);
            Controls.Add(label10);
            Controls.Add(label5);
            Controls.Add(label6);
            Controls.Add(label7);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(cbChooseStorage);
            Controls.Add(label1);
            Name = "Form2";
            Text = "Form2";
            Load += Form2_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox cbChooseStorage;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label15;
        private Label label16;
        private TextBox txtCompanyName;
        private TextBox txtCompanyEmail;
        private TextBox txtCompanyPhone;
        private TextBox txtSupplierPhone;
        private TextBox txtSupplierEmail;
        private TextBox txtSupplierName;
        private TextBox txtStockLocationInStorage;
        private TextBox txtStockStorageID;
        private TextBox txtStockProductID;
        private TextBox txtProductModelCar;
        private TextBox txtProductType;
        private TextBox txtProductName;
        private TextBox txtProductSupplierID;
        private TextBox txtProductPrice;
        private ListBox lstCompany;
        private ListBox lstSupplier;
        private ListBox lstProduct;
        private ListBox lstStock;
        private Button btnAddCompany;
        private Button btnAddSupplier;
        private Button btnAddProduct;
        private Button btnAddStock;
        private TextBox txtStockQuantity;
        private Label label17;
    }
}