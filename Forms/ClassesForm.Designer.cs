namespace Course
{
    partial class ClassesForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.DataGridView dataGridViewClasses;
        private System.Windows.Forms.Button btnAddClass;
        private System.Windows.Forms.Button btnEditClass;
        private System.Windows.Forms.Button btnDeleteClass;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClassesForm));
            this.dataGridViewClasses = new System.Windows.Forms.DataGridView();
            this.btnAddClass = new System.Windows.Forms.Button();
            this.btnEditClass = new System.Windows.Forms.Button();
            this.btnDeleteClass = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewClasses)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewClasses
            // 
            this.dataGridViewClasses.AllowUserToAddRows = false;
            this.dataGridViewClasses.AllowUserToDeleteRows = false;
            this.dataGridViewClasses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewClasses.BackgroundColor = System.Drawing.Color.Pink;
            this.dataGridViewClasses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewClasses.Location = new System.Drawing.Point(16, 14);
            this.dataGridViewClasses.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridViewClasses.Name = "dataGridViewClasses";
            this.dataGridViewClasses.ReadOnly = true;
            this.dataGridViewClasses.RowHeadersWidth = 62;
            this.dataGridViewClasses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewClasses.Size = new System.Drawing.Size(416, 370);
            this.dataGridViewClasses.TabIndex = 0;
            // 
            // btnAddClass
            // 
            this.btnAddClass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddClass.Location = new System.Drawing.Point(12, 396);
            this.btnAddClass.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAddClass.Name = "btnAddClass";
            this.btnAddClass.Size = new System.Drawing.Size(133, 37);
            this.btnAddClass.TabIndex = 1;
            this.btnAddClass.Text = "Добавить класс";
            this.btnAddClass.UseVisualStyleBackColor = true;
            this.btnAddClass.Click += new System.EventHandler(this.btnAddClass_Click);
            // 
            // btnEditClass
            // 
            this.btnEditClass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditClass.Location = new System.Drawing.Point(158, 396);
            this.btnEditClass.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEditClass.Name = "btnEditClass";
            this.btnEditClass.Size = new System.Drawing.Size(133, 37);
            this.btnEditClass.TabIndex = 2;
            this.btnEditClass.Text = "Редактировать";
            this.btnEditClass.UseVisualStyleBackColor = true;
            this.btnEditClass.Click += new System.EventHandler(this.btnEditClass_Click);
            // 
            // btnDeleteClass
            // 
            this.btnDeleteClass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteClass.Location = new System.Drawing.Point(314, 396);
            this.btnDeleteClass.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDeleteClass.Name = "btnDeleteClass";
            this.btnDeleteClass.Size = new System.Drawing.Size(133, 37);
            this.btnDeleteClass.TabIndex = 3;
            this.btnDeleteClass.Text = "Удалить";
            this.btnDeleteClass.UseVisualStyleBackColor = true;
            this.btnDeleteClass.Click += new System.EventHandler(this.btnDeleteClass_Click);
            // 
            // ClassesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cyan;
            this.ClientSize = new System.Drawing.Size(779, 444);
            this.Controls.Add(this.btnDeleteClass);
            this.Controls.Add(this.btnEditClass);
            this.Controls.Add(this.btnAddClass);
            this.Controls.Add(this.dataGridViewClasses);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(792, 477);
            this.Name = "ClassesForm";
            this.Text = "Управление классами";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewClasses)).EndInit();
            this.ResumeLayout(false);

        }
    }
}