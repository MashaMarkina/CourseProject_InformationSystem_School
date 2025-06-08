using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SchoolInformationSystem;

namespace Course.Forms
{
    /// <summary>
    /// Форма для добавления или редактирования данных студента.
    /// </summary>
    public partial class AddStudentForm : Form
    {
        private readonly MainForm _main;
        private readonly StudentManager _studentManager;
        private readonly ClassManager _classManager;
        private int? _studentId; // ID студента для редактирования (null — добавление нового)

        /// <summary>
        /// Конструктор для добавления нового студента.
        /// </summary>
        /// <param name="main">Главная форма (не используется напрямую).</param>
        public AddStudentForm(MainForm main)
        {
            InitializeComponent();
            _main = main;
            _studentManager = new StudentManager();
            _classManager = new ClassManager();
        }

        /// <summary>
        /// Конструктор для редактирования существующего студента.
        /// </summary>
        /// <param name="studentId">ID студента, данные которого будут загружены для редактирования.</param>
        public AddStudentForm(int? studentId = null)
        {
            InitializeComponent();
            _studentManager = new StudentManager();
            _classManager = new ClassManager();
            _studentId = studentId;

            if (studentId.HasValue)
                LoadStudentData();
        }


        /// <summary>
        /// Загружает данные студента из базы по ID и заполняет поля формы.
        /// </summary>
        private void LoadStudentData()
        {
            DataTable studentData = _studentManager.GetStudentById(_studentId.Value);
            if (studentData != null && studentData.Rows.Count > 0)
            {
                DataRow row = studentData.Rows[0];

                txtLastName.Text = row["LastName"].ToString();
                txtFirstName.Text = row["FirstName"].ToString();
                txtMiddleName.Text = row["MiddleName"] is DBNull ? string.Empty : row["MiddleName"].ToString();
                dtpBirthDate.Value = Convert.ToDateTime(row["BirthDate"]);
                txtPhone.Text = row["Phone"] is DBNull ? string.Empty : row["Phone"].ToString();
                txtEmail.Text = row["Email"] is DBNull ? string.Empty : row["Email"].ToString();
                txtPassword.Text = row["Password"].ToString();
                txtAddress.Text = row["Address"] is DBNull ? string.Empty : row["Address"].ToString();

                // Загружаем название класса вместо ID
                int classId = Convert.ToInt32(row["ClassId"]);
                txtClass.Text = _classManager.GetClassNameById(classId);
            }
        }

        /// <summary>
        /// Обработчик кнопки "Сохранить". Выполняет валидацию, а затем сохраняет данные в базу.
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                // Нормализуем ввод класса (удаляем пробелы и приводим к единому формату)
                string className = NormalizeClassName(txtClass.Text.Trim());

                // Проверяем существование класса в БД
                int? classId = _classManager.GetClassIdByName(className);
                if (!classId.HasValue)
                {
                    MessageBox.Show("Указанный класс не найден в базе данных!", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool result;
                if (_studentId.HasValue)
                {
                    result = _studentManager.UpdateStudent(
                        _studentId.Value,
                        txtLastName.Text.Trim(),
                        txtFirstName.Text.Trim(),
                        txtMiddleName.Text.Trim(),
                        dtpBirthDate.Value,
                        txtPhone.Text.Trim(),
                        txtEmail.Text.Trim(),
                        txtPassword.Text.Trim(),
                        txtAddress.Text.Trim(),
                        classId.Value);
                }
                else
                {
                    result = _studentManager.AddStudent(
                        txtLastName.Text.Trim(),
                        txtFirstName.Text.Trim(),
                        txtMiddleName.Text.Trim(),
                        dtpBirthDate.Value,
                        txtPhone.Text.Trim(),
                        txtEmail.Text.Trim(),
                        txtPassword.Text.Trim(),
                        txtAddress.Text.Trim(),
                        classId.Value) > 0;
                }

                if (result)
                {
                    MessageBox.Show("Данные сохранены успешно!", "Успех",
                                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Приводит название класса к стандартному формату (например, "11 А" -> "11А").
        /// </summary>
        private string NormalizeClassName(string className)
        {
            // Удаляем все пробелы и кавычки, затем форматируем
            className = className.Replace(" ", "").Replace("\"", "");

            // Проверяем формат (цифры+буква)
            if (className.Length < 2)
                return className;

            // Форматируем как "11А"
            string numberPart = string.Empty;
            string letterPart = string.Empty;

            foreach (char c in className)
            {
                if (char.IsDigit(c))
                    numberPart += c;
                else if (char.IsLetter(c))
                    letterPart += c.ToString().ToUpper();
            }

            return $"{numberPart}{letterPart}";
        }

        /// <summary>
        /// Проверяет корректность введённых пользователем данных.
        /// Возвращает true, если данные валидны.
        /// </summary>
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Фамилия обязательна для заполнения!", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Имя обязательно для заполнения!", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Email обязателен для заполнения!\nПример: student@school.ru", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Проверка номера телефона (обязательное поле)
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Номер телефона обязателен для заполнения!\nПример: +7(912)345-67-89", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Нормализация номера телефона (оставляем только цифры)
            string digitsOnly = new string(txtPhone.Text.Where(char.IsDigit).ToArray());

            // Проверка длины номера (10 цифр без кода страны, 11 с кодом)
            if (digitsOnly.Length < 11 || digitsOnly.Length > 12)
            {
                MessageBox.Show("Некорректный формат телефона!\nПравильный формат:\n+7(912)345-67-89",
                              "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

           

            // Проверка формата email
            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(txtEmail.Text);
            }
            catch
            {
                MessageBox.Show("Некорректный формат email!\nПравильный формат: student@school.ru(.com)", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Пароль обязателен для заполнения!", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtClass.Text))
            {
                MessageBox.Show("Класс обязателен для заполнения!", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}
