namespace VIS_Projekt
{
    partial class Form5
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
            cbChooseCompany = new ComboBox();
            cbChooseProduct = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            lblAvailableQuantity = new Label();
            label5 = new Label();
            txtRequestQuantity = new TextBox();
            btnCreateRequest = new Button();
            lstRequests = new ListBox();
            SuspendLayout();
            // 
            // cbChooseStorage
            // 
            cbChooseStorage.FormattingEnabled = true;
            cbChooseStorage.Location = new Point(331, 45);
            cbChooseStorage.Name = "cbChooseStorage";
            cbChooseStorage.Size = new Size(242, 40);
            cbChooseStorage.TabIndex = 0;
            cbChooseStorage.SelectedIndexChanged += cbChooseStorage_SelectedIndexChanged;
            // 
            // cbChooseCompany
            // 
            cbChooseCompany.FormattingEnabled = true;
            cbChooseCompany.Location = new Point(331, 104);
            cbChooseCompany.Name = "cbChooseCompany";
            cbChooseCompany.Size = new Size(242, 40);
            cbChooseCompany.TabIndex = 1;
            cbChooseCompany.SelectedIndexChanged += cbChooseCompany_SelectedIndexChanged;
            // 
            // cbChooseProduct
            // 
            cbChooseProduct.FormattingEnabled = true;
            cbChooseProduct.Location = new Point(331, 163);
            cbChooseProduct.Name = "cbChooseProduct";
            cbChooseProduct.Size = new Size(242, 40);
            cbChooseProduct.TabIndex = 2;
            cbChooseProduct.SelectedIndexChanged += cbChooseProduct_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(35, 45);
            label1.Name = "label1";
            label1.Size = new Size(145, 32);
            label1.TabIndex = 3;
            label1.Text = "Vyber Sklad:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(35, 99);
            label2.Name = "label2";
            label2.Size = new Size(149, 32);
            label2.TabIndex = 4;
            label2.Text = "Vyber Firmu:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(35, 163);
            label3.Name = "label3";
            label3.Size = new Size(171, 32);
            label3.TabIndex = 5;
            label3.Text = "Vyber Produkt:";
            // 
            // lblAvailableQuantity
            // 
            lblAvailableQuantity.AutoSize = true;
            lblAvailableQuantity.Location = new Point(35, 222);
            lblAvailableQuantity.Name = "lblAvailableQuantity";
            lblAvailableQuantity.Size = new Size(234, 32);
            lblAvailableQuantity.TabIndex = 6;
            lblAvailableQuantity.Text = "Dostupné Množství: ";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(35, 279);
            label5.Name = "label5";
            label5.Size = new Size(258, 32);
            label5.TabIndex = 7;
            label5.Text = "Požadované Množství: ";
            // 
            // txtRequestQuantity
            // 
            txtRequestQuantity.Location = new Point(331, 272);
            txtRequestQuantity.Name = "txtRequestQuantity";
            txtRequestQuantity.Size = new Size(242, 39);
            txtRequestQuantity.TabIndex = 8;
            // 
            // btnCreateRequest
            // 
            btnCreateRequest.Location = new Point(148, 352);
            btnCreateRequest.Name = "btnCreateRequest";
            btnCreateRequest.Size = new Size(327, 46);
            btnCreateRequest.TabIndex = 9;
            btnCreateRequest.Text = "Vytvořit Objednávku";
            btnCreateRequest.UseVisualStyleBackColor = true;
            btnCreateRequest.Click += btnCreateRequest_Click;
            // 
            // lstRequests
            // 
            lstRequests.FormattingEnabled = true;
            lstRequests.Location = new Point(12, 413);
            lstRequests.Name = "lstRequests";
            lstRequests.Size = new Size(613, 516);
            lstRequests.TabIndex = 10;
            // 
            // Form5
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(637, 937);
            Controls.Add(lstRequests);
            Controls.Add(btnCreateRequest);
            Controls.Add(txtRequestQuantity);
            Controls.Add(label5);
            Controls.Add(lblAvailableQuantity);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cbChooseProduct);
            Controls.Add(cbChooseCompany);
            Controls.Add(cbChooseStorage);
            Name = "Form5";
            Text = "Form5";
            Load += Form5_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cbChooseStorage;
        private ComboBox cbChooseCompany;
        private ComboBox cbChooseProduct;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label lblAvailableQuantity;
        private Label label5;
        private TextBox txtRequestQuantity;
        private Button btnCreateRequest;
        private ListBox lstRequests;
    }
}