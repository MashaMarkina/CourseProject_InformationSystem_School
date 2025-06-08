using System.Drawing;
using System.Windows.Forms;

namespace Course
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuStudents = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStudentsList = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddStudent = new System.Windows.Forms.ToolStripMenuItem();
            this.классыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.успеваемостьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отметитьУспеваемостьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReports = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPerfomanceReport = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.infoText = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStudents,
            this.классыToolStripMenuItem,
            this.успеваемостьToolStripMenuItem,
            this.menuReports});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(6, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuStudents
            // 
            this.menuStudents.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStudentsList,
            this.menuAddStudent});
            this.menuStudents.Name = "menuStudents";
            this.menuStudents.Size = new System.Drawing.Size(82, 24);
            this.menuStudents.Text = "Ученики";
            // 
            // menuStudentsList
            // 
            this.menuStudentsList.Name = "menuStudentsList";
            this.menuStudentsList.Size = new System.Drawing.Size(219, 26);
            this.menuStudentsList.Text = "Список учеников";
            this.menuStudentsList.Click += new System.EventHandler(this.menuStudentsList_Click);
            // 
            // menuAddStudent
            // 
            this.menuAddStudent.Name = "menuAddStudent";
            this.menuAddStudent.Size = new System.Drawing.Size(219, 26);
            this.menuAddStudent.Text = "Добавить ученика";
            this.menuAddStudent.Click += new System.EventHandler(this.menuAddStudent_Click_1);
            // 
            // классыToolStripMenuItem
            // 
            this.классыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьToolStripMenuItem});
            this.классыToolStripMenuItem.Name = "классыToolStripMenuItem";
            this.классыToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.классыToolStripMenuItem.Text = "Классы";
            // 
            // добавитьToolStripMenuItem
            // 
            this.добавитьToolStripMenuItem.Name = "добавитьToolStripMenuItem";
            this.добавитьToolStripMenuItem.Size = new System.Drawing.Size(200, 26);
            this.добавитьToolStripMenuItem.Text = "Добавить класс";
            this.добавитьToolStripMenuItem.Click += new System.EventHandler(this.добавитьToolStripMenuItem_Click_1);
            // 
            // успеваемостьToolStripMenuItem
            // 
            this.успеваемостьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.отметитьУспеваемостьToolStripMenuItem});
            this.успеваемостьToolStripMenuItem.Name = "успеваемостьToolStripMenuItem";
            this.успеваемостьToolStripMenuItem.Size = new System.Drawing.Size(121, 24);
            this.успеваемостьToolStripMenuItem.Text = "Успеваемость";
            // 
            // отметитьУспеваемостьToolStripMenuItem
            // 
            this.отметитьУспеваемостьToolStripMenuItem.Name = "отметитьУспеваемостьToolStripMenuItem";
            this.отметитьУспеваемостьToolStripMenuItem.Size = new System.Drawing.Size(257, 26);
            this.отметитьУспеваемостьToolStripMenuItem.Text = "Отметить успеваемость";
            this.отметитьУспеваемостьToolStripMenuItem.Click += new System.EventHandler(this.отметитьУспеваемостьToolStripMenuItem_Click);
            // 
            // menuReports
            // 
            this.menuReports.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuPerfomanceReport});
            this.menuReports.Name = "menuReports";
            this.menuReports.Size = new System.Drawing.Size(73, 24);
            this.menuReports.Text = "Отчеты";
            // 
            // menuPerfomanceReport
            // 
            this.menuPerfomanceReport.Name = "menuPerfomanceReport";
            this.menuPerfomanceReport.Size = new System.Drawing.Size(190, 26);
            this.menuPerfomanceReport.Text = "Успеваемость";
            this.menuPerfomanceReport.Click += new System.EventHandler(this.menuPerfomanceReport_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(305, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // infoText
            // 
            this.infoText.BackColor = System.Drawing.Color.Cyan;
            this.infoText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.infoText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoText.Font = new System.Drawing.Font("Arial", 11F);
            this.infoText.Location = new System.Drawing.Point(0, 28);
            this.infoText.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.infoText.Name = "infoText";
            this.infoText.ReadOnly = true;
            this.infoText.Size = new System.Drawing.Size(800, 268);
            this.infoText.TabIndex = 0;
            this.infoText.Text = "Добро пожаловать в SchoolManager!\n\n• Управление учениками\n• Ведение классов\n• Ана" +
    "лиз успеваемости\n• Генерация отчетов";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cyan;
            this.ClientSize = new System.Drawing.Size(800, 296);
            this.Controls.Add(this.infoText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "  ";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuStudents;
        private System.Windows.Forms.ToolStripMenuItem menuReports;
        private System.Windows.Forms.ToolStripMenuItem menuStudentsList;
        private System.Windows.Forms.ToolStripMenuItem menuAddStudent;
        private System.Windows.Forms.ToolStripMenuItem menuPerfomanceReport;
        private ToolStripMenuItem классыToolStripMenuItem;
        private ToolStripMenuItem добавитьToolStripMenuItem;
        private ToolStripMenuItem успеваемостьToolStripMenuItem;
        private ToolStripMenuItem отметитьУспеваемостьToolStripMenuItem;
        private Label label1;
        private RichTextBox infoText;
    }
}