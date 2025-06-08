using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System;

namespace Course
{
    partial class StudentListForm
    {
        private DataGridView dgvStudents;
        private Button btnMarkPerformance;
        private TextBox txtSearch;
        private ComboBox cmbFilterField;
        private ComboBox cmbSortField;
        private ComboBox cmbSortDirection;
        private Button btnApplyFilters;
        private Button btnDeleteStudent;
        private Button btnEditStudent;
        private Button btnResetFilters;


        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StudentListForm));
            this.btnResetFilters = new System.Windows.Forms.Button();
            this.dgvStudents = new System.Windows.Forms.DataGridView();
            this.btnMarkPerformance = new System.Windows.Forms.Button();
            this.btnDeleteStudent = new System.Windows.Forms.Button();
            this.btnEditStudent = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cmbFilterField = new System.Windows.Forms.ComboBox();
            this.cmbSortField = new System.Windows.Forms.ComboBox();
            this.cmbSortDirection = new System.Windows.Forms.ComboBox();
            this.btnApplyFilters = new System.Windows.Forms.Button();
            this.lblFilter = new System.Windows.Forms.Label();
            this.lblSort = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudents)).BeginInit();
            this.SuspendLayout();
            // 
            // btnResetFilters
            // 
            this.btnResetFilters.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnResetFilters.Location = new System.Drawing.Point(995, 51);
            this.btnResetFilters.Name = "btnResetFilters";
            this.btnResetFilters.Size = new System.Drawing.Size(136, 44);
            this.btnResetFilters.TabIndex = 11;
            this.btnResetFilters.Text = "Сбросить";
            this.btnResetFilters.UseVisualStyleBackColor = true;
            this.btnResetFilters.Click += new System.EventHandler(this.btnResetFilters_Click);
            // 
            // dgvStudents
            // 
            this.dgvStudents.AllowUserToAddRows = false;
            this.dgvStudents.AllowUserToDeleteRows = false;
            this.dgvStudents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStudents.BackgroundColor = System.Drawing.Color.Pink;
            this.dgvStudents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStudents.Location = new System.Drawing.Point(0, 110);
            this.dgvStudents.Name = "dgvStudents";
            this.dgvStudents.ReadOnly = true;
            this.dgvStudents.RowHeadersWidth = 62;
            this.dgvStudents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStudents.Size = new System.Drawing.Size(1143, 609);
            this.dgvStudents.TabIndex = 0;
            // 
            // btnMarkPerformance
            // 
            this.btnMarkPerformance.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnMarkPerformance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnMarkPerformance.Location = new System.Drawing.Point(0, 719);
            this.btnMarkPerformance.Name = "btnMarkPerformance";
            this.btnMarkPerformance.Size = new System.Drawing.Size(1143, 40);
            this.btnMarkPerformance.TabIndex = 1;
            this.btnMarkPerformance.Text = "Отметить успеваемость";
            this.btnMarkPerformance.UseVisualStyleBackColor = true;
            this.btnMarkPerformance.Click += new System.EventHandler(this.btnMarkPerformance_Click_1);
            // 
            // btnDeleteStudent
            // 
            this.btnDeleteStudent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteStudent.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDeleteStudent.Location = new System.Drawing.Point(586, 678);
            this.btnDeleteStudent.Name = "btnDeleteStudent";
            this.btnDeleteStudent.Size = new System.Drawing.Size(557, 41);
            this.btnDeleteStudent.TabIndex = 2;
            this.btnDeleteStudent.Text = "Удалить ученика";
            this.btnDeleteStudent.UseVisualStyleBackColor = true;
            this.btnDeleteStudent.Click += new System.EventHandler(this.btnDeleteStudent_Click);
            // 
            // btnEditStudent
            // 
            this.btnEditStudent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditStudent.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnEditStudent.Location = new System.Drawing.Point(0, 678);
            this.btnEditStudent.Name = "btnEditStudent";
            this.btnEditStudent.Size = new System.Drawing.Size(589, 41);
            this.btnEditStudent.TabIndex = 3;
            this.btnEditStudent.Text = "Редактировать ученика";
            this.btnEditStudent.UseVisualStyleBackColor = true;
            this.btnEditStudent.Click += new System.EventHandler(this.btnEditStudent_Click_1);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtSearch.ForeColor = System.Drawing.Color.Gray;
            this.txtSearch.Location = new System.Drawing.Point(12, 58);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(200, 30);
            this.txtSearch.TabIndex = 4;
            this.txtSearch.Text = "Поиск...";
            this.txtSearch.GotFocus += new System.EventHandler(this.txtSearch_GotFocus);
            this.txtSearch.LostFocus += new System.EventHandler(this.txtSearch_LostFocus);
            // 
            // cmbFilterField
            // 
            this.cmbFilterField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterField.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbFilterField.FormattingEnabled = true;
            this.cmbFilterField.Items.AddRange(new object[] {
            "Все",
            "Фамилия",
            "Имя",
            "Класс"});
            this.cmbFilterField.Location = new System.Drawing.Point(230, 56);
            this.cmbFilterField.Name = "cmbFilterField";
            this.cmbFilterField.Size = new System.Drawing.Size(120, 33);
            this.cmbFilterField.TabIndex = 5;
            // 
            // cmbSortField
            // 
            this.cmbSortField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSortField.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbSortField.FormattingEnabled = true;
            this.cmbSortField.Items.AddRange(new object[] {
            "Фамилия",
            "Имя",
            "Класс",
            "Дата рождения"});
            this.cmbSortField.Location = new System.Drawing.Point(385, 56);
            this.cmbSortField.Name = "cmbSortField";
            this.cmbSortField.Size = new System.Drawing.Size(143, 33);
            this.cmbSortField.TabIndex = 6;
            // 
            // cmbSortDirection
            // 
            this.cmbSortDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSortDirection.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbSortDirection.FormattingEnabled = true;
            this.cmbSortDirection.Items.AddRange(new object[] {
            "По возрастанию",
            "По убыванию"});
            this.cmbSortDirection.Location = new System.Drawing.Point(534, 56);
            this.cmbSortDirection.Name = "cmbSortDirection";
            this.cmbSortDirection.Size = new System.Drawing.Size(229, 33);
            this.cmbSortDirection.TabIndex = 7;
            // 
            // btnApplyFilters
            // 
            this.btnApplyFilters.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnApplyFilters.Location = new System.Drawing.Point(812, 51);
            this.btnApplyFilters.Name = "btnApplyFilters";
            this.btnApplyFilters.Size = new System.Drawing.Size(177, 44);
            this.btnApplyFilters.TabIndex = 8;
            this.btnApplyFilters.Text = "Применить";
            this.btnApplyFilters.UseVisualStyleBackColor = true;
            this.btnApplyFilters.Click += new System.EventHandler(this.BtnApplyFilters_Click);
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFilter.Location = new System.Drawing.Point(225, 22);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(93, 25);
            this.lblFilter.TabIndex = 9;
            this.lblFilter.Text = "Фильтр:";
            // 
            // lblSort
            // 
            this.lblSort.AutoSize = true;
            this.lblSort.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSort.Location = new System.Drawing.Point(380, 22);
            this.lblSort.Name = "lblSort";
            this.lblSort.Size = new System.Drawing.Size(128, 25);
            this.lblSort.TabIndex = 10;
            this.lblSort.Text = "Сортировка:";
            // 
            // StudentListForm
            // 
            this.BackColor = System.Drawing.Color.Cyan;
            this.ClientSize = new System.Drawing.Size(1143, 759);
            this.Controls.Add(this.btnApplyFilters);
            this.Controls.Add(this.cmbSortDirection);
            this.Controls.Add(this.cmbSortField);
            this.Controls.Add(this.cmbFilterField);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnEditStudent);
            this.Controls.Add(this.btnDeleteStudent);
            this.Controls.Add(this.btnMarkPerformance);
            this.Controls.Add(this.dgvStudents);
            this.Controls.Add(this.lblFilter);
            this.Controls.Add(this.lblSort);
            this.Controls.Add(this.btnResetFilters);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StudentListForm";
            this.Text = "Список студентов";
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudents)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

       

        private Label lblFilter;
        private Label lblSort;
    }
}