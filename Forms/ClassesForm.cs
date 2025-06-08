using System;
using System.Windows.Forms;
using Microsoft.VisualBasic; // Добавляем для использования InputBox
using SchoolInformationSystem;

namespace Course
{
    /// <summary>
    /// Форма для управления школьными классами: добавление, редактирование, удаление.
    /// </summary>
    public partial class ClassesForm : Form
    {
        private readonly MainForm _main;
        private readonly ClassManager _classManager; // Добавляем менеджер классов

        /// <summary>
        /// Конструктор формы управления классами.
        /// </summary>
        /// <param name="main">Ссылка на главную форму.</param>
        public ClassesForm(MainForm main)
        {
            InitializeComponent();
            _main = main;
            _classManager = new ClassManager(); // Инициализируем менеджер классов
            LoadClasses(); // Загружаем классы при создании формы
        }

        /// <summary>
        /// Загружает список всех классов и отображает в DataGridView.
        /// </summary>
        private void LoadClasses()
        {
            try
            {
                // Получаем список классов и привязываем к DataGridView
                dataGridViewClasses.DataSource = _classManager.GetAllClasses();
                dataGridViewClasses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке классов: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик кнопки добавления нового класса.
        /// Запрашивает название у пользователя и добавляет класс в базу.
        /// </summary>
        private void btnAddClass_Click(object sender, EventArgs e)
        {
            try
            {
                string className = Interaction.InputBox("Введите название класса:", "Добавление класса");

                if (!string.IsNullOrWhiteSpace(className))
                {
                    if (className.Length < 2 || className.Length > 20)
                    {
                        MessageBox.Show("Название класса должно быть от 2 до 20 символов!", "Ошибка",
                                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int newClassId = _classManager.AddClass(className.Trim());
                    if (newClassId > 0)
                    {
                        MessageBox.Show("Класс успешно добавлен!", "Успех",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadClasses();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик кнопки редактирования выбранного класса.
        /// </summary>
        private void btnEditClass_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewClasses.SelectedRows.Count == 0)
                    throw new Exception("Не выбран класс для редактирования");

                int classId = (int)dataGridViewClasses.SelectedRows[0].Cells["ClassId"].Value;
                string currentName = dataGridViewClasses.SelectedRows[0].Cells["ClassName"].Value.ToString();

                string newName = Interaction.InputBox("Введите новое название класса:", "Редактирование класса", currentName);

                if (string.IsNullOrWhiteSpace(newName)) return;
                if (newName == currentName) return;

                if (newName.Length < 2 || newName.Length > 20)
                {
                    MessageBox.Show("Название класса должно быть от 2 до 20 символов!", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (_classManager.UpdateClass(classId, newName.Trim()))
                {
                    MessageBox.Show($"Класс успешно переименован в '{newName}'!", "Успех",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadClasses();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик кнопки удаления выбранного класса.
        /// Запрашивает подтверждение и удаляет класс из базы.
        /// </summary>
        private void btnDeleteClass_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewClasses.SelectedRows.Count == 0)
                    throw new Exception("Не выбран класс для удаления");

                int classId = (int)dataGridViewClasses.SelectedRows[0].Cells["ClassId"].Value;
                string className = dataGridViewClasses.SelectedRows[0].Cells["ClassName"].Value.ToString();

                if (MessageBox.Show($"Вы действительно хотите удалить класс {className}?", "Подтверждение удаления",
                                   MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (_classManager.DeleteClass(classId))
                    {
                        MessageBox.Show("Класс успешно удален!", "Успех",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadClasses();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении класса: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}