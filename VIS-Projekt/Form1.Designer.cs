namespace VIS_Projekt
{
    partial class Form1
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
            label2 = new Label();
            txtStorageLocation = new TextBox();
            txtStorageCapacity = new TextBox();
            lstStorage = new ListBox();
            btnAddStorage = new Button();
            btnChooseStorage = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(104, 70);
            label1.Name = "label1";
            label1.Size = new Size(170, 32);
            label1.TabIndex = 0;
            label1.Text = "Lokace Skladu:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(104, 137);
            label2.Name = "label2";
            label2.Size = new Size(186, 32);
            label2.TabIndex = 1;
            label2.Text = "Kapacita Skladu:";
            // 
            // txtStorageLocation
            // 
            txtStorageLocation.Location = new Point(361, 70);
            txtStorageLocation.Name = "txtStorageLocation";
            txtStorageLocation.Size = new Size(200, 39);
            txtStorageLocation.TabIndex = 2;
            // 
            // txtStorageCapacity
            // 
            txtStorageCapacity.Location = new Point(361, 137);
            txtStorageCapacity.Name = "txtStorageCapacity";
            txtStorageCapacity.Size = new Size(200, 39);
            txtStorageCapacity.TabIndex = 3;
            // 
            // lstStorage
            // 
            lstStorage.FormattingEnabled = true;
            lstStorage.Location = new Point(87, 255);
            lstStorage.Name = "lstStorage";
            lstStorage.Size = new Size(502, 388);
            lstStorage.TabIndex = 4;
            // 
            // btnAddStorage
            // 
            btnAddStorage.Location = new Point(278, 199);
            btnAddStorage.Name = "btnAddStorage";
            btnAddStorage.Size = new Size(150, 46);
            btnAddStorage.TabIndex = 5;
            btnAddStorage.Text = "Přidat Sklad";
            btnAddStorage.UseVisualStyleBackColor = true;
            btnAddStorage.Click += btnAddStorage_Click;
            // 
            // btnChooseStorage
            // 
            btnChooseStorage.Location = new Point(259, 663);
            btnChooseStorage.Name = "btnChooseStorage";
            btnChooseStorage.Size = new Size(185, 46);
            btnChooseStorage.TabIndex = 6;
            btnChooseStorage.Text = "Vybrat Sklad";
            btnChooseStorage.UseVisualStyleBackColor = true;
            btnChooseStorage.Click += btnChooseStorage_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(683, 872);
            Controls.Add(btnChooseStorage);
            Controls.Add(btnAddStorage);
            Controls.Add(lstStorage);
            Controls.Add(txtStorageCapacity);
            Controls.Add(txtStorageLocation);
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
        private TextBox txtStorageLocation;
        private TextBox txtStorageCapacity;
        private ListBox lstStorage;
        private Button btnAddStorage;
        private Button btnChooseStorage;
    }
}