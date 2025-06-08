using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Course;

namespace SchoolInformationSystem
{
    public partial class WelcomeForm : Form
    {
        /// <summary>
        /// Конструктор формы WelcomeForm.
        /// Инициализирует компоненты формы.
        /// </summary>
        public WelcomeForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик события загрузки формы WelcomeForm.
        /// Выполняется при первом открытии формы.
        /// </summary>
        private void WelcomeForm_Load(object sender, EventArgs e)
        {
            // Здесь можно добавить инициализацию при загрузке формы, если потребуется.
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Продолжить" (btnContinue).
        /// Создаёт и отображает основную форму (MainForm), скрывая текущую форму WelcomeForm.
        /// После закрытия MainForm снова отображает WelcomeForm.
        /// </summary>
        private void btnContinue_Click(object sender, EventArgs e)
        {
            var mainForm = new MainForm(this);

            // Подписка на событие FormClosed основной формы
            mainForm.FormClosed += (s, args) =>
            {
                // Показ WelcomeForm после закрытия MainForm
                this.Show();
            };

            // Отображение MainForm
            mainForm.Show();

            // Скрытие текущей формы WelcomeForm
            this.Hide();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Выход" (btnExit).
        /// Закрывает всё приложение.
        /// </summary>
        private void btnExit_Click(object sender, System.EventArgs e)
        {
            Application.Exit(); // Полностью завершает работу приложения
        }

        /// <summary>
        /// Переопределённый метод закрытия формы.
        /// Гарантирует, что при закрытии WelcomeForm приложение завершится.
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit(); // Полное завершение при закрытии WelcomeForm
            }
            base.OnFormClosing(e);
        }

        /// <summary>
        /// Дублирующий обработчик кнопки "Продолжить".
        /// Логика идентична btnContinue_Click.
        /// Возможно, вызовется в зависимости от конфигурации событий в дизайнере.
        /// </summary>
        private void btnContinue_Click_1(object sender, EventArgs e)
        {
            var mainForm = new MainForm(this);

            mainForm.FormClosed += (s, args) =>
            {
                this.Show();
            };

            mainForm.Show();
            this.Hide();
        }

        /// <summary>
        /// Дублирующий обработчик кнопки "Выход".
        /// Закрывает WelcomeForm, что приведёт к завершению приложения.
        /// </summary>
        private void btnExit_Click_1(object sender, EventArgs e)
        {
            Close(); // Закрытие текущей формы, завершение приложения будет обработано в OnFormClosing
        }
    }
}
