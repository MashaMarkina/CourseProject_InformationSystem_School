using SchoolInformationSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Course
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Создаем WelcomeForm
            var welcomeForm = new WelcomeForm();

            // Показываем WelcomeForm немодально
            welcomeForm.Show();

            // Запускаем цикл сообщений
            Application.Run();
        }
    }
}
