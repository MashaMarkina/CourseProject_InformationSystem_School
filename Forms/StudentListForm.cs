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

namespace Course
{
    /// <summary>
    /// Форма отображения, редактирования, фильтрации и удаления списка студентов.
    /// </summary>
    public partial class StudentListForm : Form
    {
        /// <summary>
        /// Менеджер для операций с данными студентов.
        /// </summary>
        private readonly StudentManager _studentManager = new StudentManager();

        /// <summary>
        /// Ссылка на главную форму, откуда открыта эта форма.
        /// </summary>
        private readonly MainForm _mainForm;

        /// <summary>
        /// Менеджер для операций с классами студентов.
        /// </summary>
        private readonly ClassManager _classManager;

        /// <summary>
        /// Конструктор формы StudentListForm.
        /// Настраивает DataGridView и загружает данные студентов.
        /// </summary>
        /// <param name="mainForm">Главная форма приложения.</param>
        internal StudentListForm(MainForm mainForm)
        {
            InitializeComponent();
            ConfigureDataGridView();
            LoadStudents();
            _mainForm = mainForm;
            _classManager = new ClassManager();
        }

        /// <summary>
        /// Конфигурирование DataGridView:
        /// Отключается авто-генерация столбцов.
        /// Очищаются старые колонки.
        /// Создаются и добавляются необходимые колонки вручную:
        ///     ID (скрыт), Фамилия, Имя, Отчество, Класс.
        /// </summary>
        private void ConfigureDataGridView()
        {
            dgvStudents.AutoGenerateColumns = false;
            dgvStudents.Columns.Clear();

            // Создаем столбцы вручную
            DataGridViewColumn[] columns = {
        new DataGridViewTextBoxColumn {
            Name = "colStudentId",
            DataPropertyName = "StudentId",
            HeaderText = "ID",
            Visible = false
        },
        new DataGridViewTextBoxColumn {
            Name = "colLastName",
            DataPropertyName = "LastName",
            HeaderText = "Фамилия",
            Width = 150
        },
        new DataGridViewTextBoxColumn {
            Name = "colFirstName",
            DataPropertyName = "FirstName",
            HeaderText = "Имя",
            Width = 150
        },
        new DataGridViewTextBoxColumn {
            Name = "colMiddleName",
            DataPropertyName = "MiddleName",
            HeaderText = "Отчество",
            Width = 150
        },
        new DataGridViewTextBoxColumn {
            Name = "colClassName",
            DataPropertyName = "ClassName",
            HeaderText = "Класс",
            Width = 80
        }
    };

            dgvStudents.Columns.AddRange(columns);
        }

        /// <summary>
        /// Загружает список студентов из БД и отображает его в DataGridView.
        /// При ошибке показывает сообщение.
        /// </summary>
        //private void LoadStudents()
        //{
        //    try
        //    {
        //        dgvStudents.DataSource = _studentManager.GetAllStudents();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка загрузки: {ex.Message}", "Ошибка",
        //                      MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void LoadStudents()
        //{
        //    try
        //    {
        //        dgvStudents.DataSource = _studentManager.GetAllStudents();
        //        ApplyFiltersAndSorting(); // Применяем текущие фильтры и сортировку
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка загрузки: {ex.Message}", "Ошибка",
        //                      MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}


        /// <summary>
        /// Обработчик кнопки "Выставить успеваемость":
        /// Проверяет, выбран ли студент.
        /// Получает ID и имя.
        /// Открывает модальную форму PerformanceForm.
        /// </summary>
        private void btnMarkPerformance_Click_1(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите студента для выставления успеваемости", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dgvStudents.SelectedRows.Count == 0) return;

            try
            {
                DataRowView row = (DataRowView)dgvStudents.SelectedRows[0].DataBoundItem;

                int studentId = (int)row["StudentId"];
                string lastName = row["LastName"].ToString();
                string firstName = row["FirstName"].ToString();

                using (var form = new PerformanceForm(studentId, $"{lastName} {firstName}"))
                {
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения данных: {ex.Message}\n\nПроверьте структуру данных.",
                              "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Обработчик кнопки "Редактировать студента":
        /// Проверяет выбран ли студент.
        /// Получает подробные данные студента.
        /// Создаёт форму редактирования вручную с полями и кнопками.
        /// Проверяет ввод, нормализует класс.
        /// Обновляет данные через StudentManager.
        /// При успешном сохранении обновляет DataGridView и уведомляет пользователя.
        /// </summary>
        private void btnEditStudent_Click_1(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите студента для редактирования", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataRowView row = (DataRowView)dgvStudents.SelectedRows[0].DataBoundItem;
                int studentId = Convert.ToInt32(row["StudentId"]);

                // Получаем полные данные студента
                DataTable studentData = _studentManager.GetStudentDetails(studentId);
                if (studentData.Rows.Count == 0) return;

                DataRow student = studentData.Rows[0];

                // Создаем форму редактирования
                using (Form editForm = new Form())
                {
                    editForm.Text = "Редактирование студента";
                    editForm.Size = new Size(450, 450);
                    editForm.StartPosition = FormStartPosition.CenterParent;
                    editForm.FormBorderStyle = FormBorderStyle.FixedDialog;

                    // Создаем элементы управления
                    var txtLastName = new TextBox { Text = student["LastName"].ToString(), Location = new Point(120, 20), Width = 250 };
                    var txtFirstName = new TextBox { Text = student["FirstName"].ToString(), Location = new Point(120, 50), Width = 250 };
                    var txtMiddleName = new TextBox { Text = student["MiddleName"].ToString(), Location = new Point(120, 80), Width = 250 };
                    var txtPhone = new TextBox { Text = student["Phone"].ToString(), Location = new Point(120, 110), Width = 250 };
                    var txtEmail = new TextBox { Text = student["Email"].ToString(), Location = new Point(120, 140), Width = 250 };
                    var txtPassword = new TextBox { Text = string.Empty, Location = new Point(120, 170), Width = 250, PasswordChar = '*' };
                    var txtAddress = new TextBox { Text = student["Address"].ToString(), Location = new Point(120, 200), Width = 250 };
                    var txtClass = new TextBox { Text = student["ClassName"].ToString(), Location = new Point(120, 230), Width = 250 };
                    var dtpBirthDate = new DateTimePicker
                    {
                        Value = Convert.ToDateTime(student["BirthDate"]),
                        Location = new Point(120, 260),
                        Width = 250
                    };

                    var btnSave = new Button { Text = "Сохранить", DialogResult = DialogResult.OK, Location = new Point(150, 320) };
                    var btnCancel = new Button { Text = "Отмена", DialogResult = DialogResult.Cancel, Location = new Point(250, 320) };

                    // Добавляем подписи
                    var labels = new[]
                    {
                new Label { Text = "Фамилия:", Location = new Point(20, 23) },
                new Label { Text = "Имя:", Location = new Point(20, 53) },
                new Label { Text = "Отчество:", Location = new Point(20, 83) },
                new Label { Text = "Телефон:", Location = new Point(20, 113) },
                new Label { Text = "Email:", Location = new Point(20, 143) },
                new Label { Text = "Пароль:", Location = new Point(20, 173) },
                new Label { Text = "Адрес:", Location = new Point(20, 203) },
                new Label { Text = "Класс:", Location = new Point(20, 233) },
                new Label { Text = "Дата рождения:", Location = new Point(20, 263) }
            };

                    // Добавляем элементы на форму
                    editForm.Controls.AddRange(labels);
                    editForm.Controls.AddRange(new Control[] {
                txtLastName, txtFirstName, txtMiddleName,
                txtPhone, txtEmail, txtPassword,
                txtAddress, txtClass, dtpBirthDate,
                btnSave, btnCancel
            });

                    // Обработчик сохранения
                    btnSave.Click += (s, args) => {
                        if (!ValidateStudentInput(txtLastName.Text, txtFirstName.Text, txtPhone.Text,
                                               txtEmail.Text, txtPassword.Text, txtClass.Text))
                        {
                            editForm.DialogResult = DialogResult.None;
                            return;
                        }

                        // Нормализация класса
                        string normalizedClass = NormalizeClassName(txtClass.Text.Trim());
                        int? classId = _classManager.GetClassIdByName(normalizedClass);

                        if (!classId.HasValue)
                        {
                            MessageBox.Show("Указанный класс не найден!", "Ошибка",
                                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                            editForm.DialogResult = DialogResult.None;
                            return;
                        }

                        // Обновление данных
                        bool result = _studentManager.UpdateStudent(
                            studentId,
                            txtLastName.Text.Trim(),
                            txtFirstName.Text.Trim(),
                            txtMiddleName.Text.Trim(),
                            dtpBirthDate.Value,
                            txtPhone.Text.Trim(),
                            txtEmail.Text.Trim(),
                            string.IsNullOrEmpty(txtPassword.Text) ? null : txtPassword.Text.Trim(),
                            txtAddress.Text.Trim(),
                            classId.Value
                        );

                        if (!result)
                        {
                            MessageBox.Show("Ошибка при обновлении данных", "Ошибка",
                                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                            editForm.DialogResult = DialogResult.None;
                        }
                    };

                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadStudents(); // Обновляем список
                        MessageBox.Show("Данные успешно обновлены", "Успех",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при редактировании: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Проверка корректности введённых данных:
        /// Обязательные поля: фамилия, имя, телефон, класс.
        /// Если указан Email — проверяется формат.
        ///  В случае ошибок показывает предупреждение.
        /// </summary>
        private bool ValidateStudentInput(string lastName, string firstName, string phone,
                                        string email, string password, string className)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                MessageBox.Show("Фамилия обязательна для заполнения!", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(firstName))
            {
                MessageBox.Show("Имя обязательно для заполнения!", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Телефон обязателен для заполнения!", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                try
                {
                    var mailAddress = new System.Net.Mail.MailAddress(email);
                }
                catch
                {
                    MessageBox.Show("Некорректный формат email!", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            if (string.IsNullOrWhiteSpace(className))
            {
                MessageBox.Show("Класс обязателен для заполнения!", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Приведение названия класса к стандарту:
        /// Убирает пробелы и кавычки.
        /// Делит на цифры и буквы.
        /// Возвращает строку: "<цифры><буквы>", всё в верхнем регистре.
        /// </summary>
        private string NormalizeClassName(string className)
        {
            className = className.Replace(" ", "").Replace("\"", "").ToUpper();

            // Разделяем цифры и буквы
            string numberPart = new string(className.Where(char.IsDigit).ToArray());
            string letterPart = new string(className.Where(char.IsLetter).ToArray());

            return $"{numberPart}{letterPart}";
        }

        /// <summary>
        /// Обработчик кнопки "Удалить студента":
        /// Проверяет, выбран ли студент.
        /// Получает ID и удаляет через StudentManager.
        /// Если успешно — обновляет таблицу.
        /// </summary>
        private void btnDeleteStudent_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите студента для удаления", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Самый надежный способ получить ID
                DataRowView rowView = (DataRowView)dgvStudents.SelectedRows[0].DataBoundItem;
                int studentId = Convert.ToInt32(rowView["StudentId"]);

                if (_studentManager.DeleteStudent(studentId))
                {
                    LoadStudents(); // Обновляем список
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Обновляем метод LoadStudents
        private void LoadStudents()
        {
            try
            {
                var data = _studentManager.GetAllStudents();
                dgvStudents.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик события фокусировки поля поиска:
        /// Убирает плейсхолдер "Поиск…" и перекрашивает текст.
        /// </summary>
        private void txtSearch_GotFocus(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Поиск...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = System.Drawing.Color.Black;
            }
        }

        /// <summary>
        /// Обработчик события, когда поле поиска теряет фокус:
        /// Если пусто — восстанавливает плейсхолдер и цвет.
        /// </summary>
        private void txtSearch_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Поиск...";
                txtSearch.ForeColor = System.Drawing.Color.Gray;
            }
        }

        /// <summary>
        /// Обработчик изменения текста в поле поиска:
        /// Игнорирует плейсхолдер.
        /// Запускает фильтрацию и сортировку.
        /// </summary>
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Поиск…" && txtSearch.ForeColor == Color.Gray) return;
            ApplyFiltersAndSorting();
        }

        /// <summary>
        /// Обработчик кнопки “Применить фильтры”: запускает фильтрацию/сортировку.
        /// </summary>
        private void BtnApplyFilters_Click(object sender, EventArgs e)
        {
            ApplyFiltersAndSorting();
        }

        /// <summary>
        /// Обработчик кнопки “Сбросить фильтры”:
        /// Сбрасывает поля фильтрации.
        /// Загружает оригинальные данные.
        /// </summary>
        private void btnResetFilters_Click(object sender, EventArgs e)
        {
            try
            {
                
                txtSearch.Text = "Поиск...";
                txtSearch.ForeColor = Color.Gray;
                cmbFilterField.SelectedIndex = 0;
                cmbSortField.SelectedIndex = 0;
                cmbSortDirection.SelectedIndex = 0;

                // Обновляем данные
                LoadStudents();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сбросе фильтров: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Метод фильтрации и сортировки:
        /// Проверяет заполнение фильтров.
        /// Получает данные студентов.
        /// Применяет фильтрацию по фамилии, имени или классу.
        /// Применяет сортировку (ASC/DESC).
        /// При ошибке — восстанавливает исходные данные.
        /// </summary>
        private void ApplyFiltersAndSorting()
        {
            try
            {
                // Проверяем, что все необходимые элементы инициализированы
                if (cmbFilterField.SelectedItem == null || cmbSortField.SelectedItem == null ||
                    cmbSortDirection.SelectedItem == null)
                {
                    MessageBox.Show("Не все параметры фильтрации выбраны!", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataTable data = _studentManager.GetAllStudents();
                if (data == null || data.Rows.Count == 0)
                {
                    dgvStudents.DataSource = null;
                    return;
                }

                DataView view = data.DefaultView;

                // Применяем фильтрацию (только если не плейсхолдер)
                if (txtSearch.Text != "Поиск..." || txtSearch.ForeColor != Color.Gray)
                {
                    string searchText = txtSearch.Text.Trim();
                    if (!string.IsNullOrEmpty(searchText))
                    {
                        string filterField = cmbFilterField.SelectedItem.ToString();
                        string filterExpression = "";

                        switch (filterField)
                        {
                            case "Фамилия":
                                filterExpression = $"LastName LIKE '%{searchText}%'";
                                break;
                            case "Имя":
                                filterExpression = $"FirstName LIKE '%{searchText}%'";
                                break;
                            case "Класс":
                                filterExpression = $"ClassName LIKE '%{searchText}%'";
                                break;
                            default: // "Все"
                                filterExpression = $"(LastName LIKE '%{searchText}%' OR " +
                                                 $"FirstName LIKE '%{searchText}%' OR " +
                                                 $"ClassName LIKE '%{searchText}%')";
                                break;
                        }

                        view.RowFilter = filterExpression;
                    }
                }

                // Применяем сортировку
                string sortField = cmbSortField.SelectedItem.ToString();
                string sortDirection = cmbSortDirection.SelectedItem.ToString() == "По возрастанию" ? "ASC" : "DESC";

                string sortExpression = "";
                switch (sortField)
                {
                    case "Фамилия":
                        sortExpression = $"LastName {sortDirection}, FirstName {sortDirection}";
                        break;
                    case "Имя":
                        sortExpression = $"FirstName {sortDirection}, LastName {sortDirection}";
                        break;
                    case "Класс":
                        sortExpression = $"ClassName {sortDirection}, LastName ASC";
                        break;
                    case "Дата рождения":
                        sortExpression = $"BirthDate {sortDirection}";
                        break;
                }

                view.Sort = sortExpression;
                dgvStudents.DataSource = view.ToTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при фильтрации/сортировке: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);

                // В случае ошибки показываем все данные без фильтров
                try
                {
                    LoadStudents();
                }
                catch
                {
                    dgvStudents.DataSource = null;
                }
            }
        }
    }
}
