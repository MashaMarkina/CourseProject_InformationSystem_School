using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Course.Forms;
using SchoolInformationSystem;

namespace Course
{
    /// <summary>
    /// Главная форма приложения. Содержит меню для навигации по функциональным разделам.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Ссылка на форму приветствия. Может использоваться для возврата назад или передачи данных.
        /// </summary>
        private readonly WelcomeForm _welcome;

        /// <summary>
        /// Конструктор главной формы, принимающий форму приветствия как параметр.
        /// </summary>
        /// <param name="welcome">Экземпляр формы WelcomeForm.</param>
        public MainForm(WelcomeForm welcome)
        {
            InitializeComponent();
            _welcome = welcome;

        }

        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Открывает форму для добавления нового студента.
        /// </summary>
        private void menuAddStudent_Click_1(object sender, EventArgs e)
        {
            var registerStudentForm = new AddStudentForm(this);
            registerStudentForm.Show();
        }

        /// <summary>
        /// Открывает форму списка студентов.
        /// </summary>
        private void menuStudentsList_Click(object sender, EventArgs e)
        {
            var studentListForm = new StudentListForm(this);
            studentListForm.Show();
        }

        /// <summary>
        /// Открывает форму для добавления и управления классами.
        /// </summary>
        private void добавитьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var classeForm = new ClassesForm(this);
            classeForm.Show();
        }

        /// <summary>
        /// Открывает форму списка студентов для выставления успеваемости.
        /// </summary>
        private void отметитьУспеваемостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var studentListForm = new StudentListForm(this);
            studentListForm.Show();
        }

        /// <summary>
        /// Открывает форму с отчётом по успеваемости студентов.
        /// </summary>
        private void menuPerfomanceReport_Click_1(object sender, EventArgs e)
        {
            var performanceReportForm = new PerformanceReportForm(this);
            performanceReportForm.Show();
        }
    }
}


