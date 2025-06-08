using System.Drawing;
using System.Windows.Forms;

namespace Course.Forms
{
    partial class PerformanceForm
    {
        private Label lblStudentName;
        private Label lblClass;
        private TextBox txtSubject;
        private NumericUpDown nudGrade;
        private Button btnSave;
        private Button btnCancel;
        private Label label1;
        private Label label2;
        private Label label3;
        private DateTimePicker dtpDate;
        private CheckBox chkPresence;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PerformanceForm));
            this.lblStudentName = new System.Windows.Forms.Label();
            this.lblClass = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.nudGrade = new System.Windows.Forms.NumericUpDown();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.chkPresence = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudGrade)).BeginInit();
            this.SuspendLayout();
            // 
            // lblStudentName
            // 
            this.lblStudentName.AutoSize = true;
            this.lblStudentName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblStudentName.Location = new System.Drawing.Point(20, 20);
            this.lblStudentName.Name = "lblStudentName";
            this.lblStudentName.Size = new System.Drawing.Size(0, 25);
            this.lblStudentName.TabIndex = 0;
            // 
            // lblClass
            // 
            this.lblClass.AutoSize = true;
            this.lblClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblClass.Location = new System.Drawing.Point(20, 50);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(0, 25);
            this.lblClass.TabIndex = 1;
            // 
            // txtSubject
            // 
            this.txtSubject.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtSubject.Location = new System.Drawing.Point(25, 92);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(323, 30);
            this.txtSubject.TabIndex = 2;
            // 
            // nudGrade
            // 
            this.nudGrade.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nudGrade.Location = new System.Drawing.Point(25, 176);
            this.nudGrade.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudGrade.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudGrade.Name = "nudGrade";
            this.nudGrade.Size = new System.Drawing.Size(60, 30);
            this.nudGrade.TabIndex = 3;
            this.nudGrade.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSave.Location = new System.Drawing.Point(12, 374);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(153, 52);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click_1);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCancel.Location = new System.Drawing.Point(195, 374);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(127, 52);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(20, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "Предмет:";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(20, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 32);
            this.label2.TabIndex = 3;
            this.label2.Text = "Оценка (1-5):";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(20, 232);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 31);
            this.label3.TabIndex = 6;
            this.label3.Text = "Дата:";
            // 
            // dtpDate
            // 
            this.dtpDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(25, 287);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(120, 30);
            this.dtpDate.TabIndex = 4;
            // 
            // chkPresence
            // 
            this.chkPresence.AutoSize = true;
            this.chkPresence.Checked = true;
            this.chkPresence.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPresence.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chkPresence.Location = new System.Drawing.Point(166, 161);
            this.chkPresence.Name = "chkPresence";
            this.chkPresence.Size = new System.Drawing.Size(176, 29);
            this.chkPresence.TabIndex = 5;
            this.chkPresence.Text = "Присутствовал";
            // 
            // PerformanceForm
            // 
            this.BackColor = System.Drawing.Color.Cyan;
            this.ClientSize = new System.Drawing.Size(931, 639);
            this.Controls.Add(this.lblStudentName);
            this.Controls.Add(this.lblClass);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudGrade);
            this.Controls.Add(this.chkPresence);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PerformanceForm";
            this.Text = "Учет успеваемости";
            ((System.ComponentModel.ISupportInitialize)(this.nudGrade)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}