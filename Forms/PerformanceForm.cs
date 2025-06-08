//using System;
//using System.Data;
//using System.Windows.Forms;
//using Course.DatabaseManager;
//using SchoolInformationSystem;

//namespace Course.Forms
//{
//    public partial class PerformanceForm : Form
//    {
//        private readonly StudentManager _studentManager;
//        private readonly PerformanceManager _performanceManager;
//        private readonly MainForm _mainForm;
//        private readonly int _studentId;

//        // Унифицированный конструктор
//        public PerformanceForm(MainForm mainForm, int studentId)
//        {
//            InitializeComponent();

//            _mainForm = mainForm;
//            _studentManager = new StudentManager();
//            _performanceManager = new PerformanceManager();
//            _studentId = studentId;

//            LoadStudentInfo();
//            dtpDate.Value = DateTime.Today;
//        }

//        // Старый конструктор оставляем для совместимости, но делаем его приватным
//        private PerformanceForm(int studentId) : this(null, studentId)
//        {
//        }

//        private void LoadStudentInfo()
//        {
//            DataTable studentData = _studentManager.GetStudentById(_studentId);
//            if (studentData.Rows.Count > 0)
//            {
//                DataRow row = studentData.Rows[0];
//                lblStudentName.Text = $"{row["LastName"]} {row["FirstName"]} {row["MiddleName"]}";
//                // Раскомментируйте, когда реализуете GetStudentClass
//                // lblClass.Text = $"Класс: {_studentManager.GetStudentClass(_studentId)}";
//            }
//        }

//        private void btnSave_Click_1(object sender, EventArgs e)
//        {
//            if (!ValidateInput()) return;

//            try
//            {
//                string subject = txtSubject.Text.Trim();
//                int gradeValue = (int)nudGrade.Value;
//                DateTime date = dtpDate.Value;
//                bool presence = chkPresence.Checked;

//                // Используем поле _studentId вместо жестко заданного значения
//                bool success = _performanceManager.AddPerformanceRecord(
//                    subject, gradeValue, date, presence, _studentId);

//                if (success)
//                {
//                    MessageBox.Show("Данные успешно сохранены!", "Успех",
//                                MessageBoxButtons.OK, MessageBoxIcon.Information);
//                    this.DialogResult = DialogResult.OK;
//                    this.Close();
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}", "Ошибка",
//                            MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private bool ValidateInput()
//        {
//            if (string.IsNullOrWhiteSpace(txtSubject.Text))
//            {
//                MessageBox.Show("Введите название предмета!", "Ошибка",
//                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                txtSubject.Focus();
//                return false;
//            }

//            if (nudGrade.Value < 1 || nudGrade.Value > 5)
//            {
//                MessageBox.Show("Оценка должна быть от 1 до 5!", "Ошибка",
//                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                nudGrade.Focus();
//                return false;
//            }

//            return true;
//        }

//        private void btnCancel_Click_1(object sender, EventArgs e)
//        {
//            this.DialogResult = DialogResult.Cancel;
//            this.Close();
//        }
//    }
//}

using System;
using System.Windows.Forms;
using Course.DatabaseManager;
using SchoolInformationSystem;

namespace Course.Forms
{
    /// <summary>
    /// Форма для добавления записи об успеваемости учащегося.
    /// </summary>
    public partial class PerformanceForm : Form
    {
        /// <summary>
        /// Менеджер для работы с базой данных по успеваемости.
        /// </summary>
        private readonly PerformanceManager _performanceManager;

        /// <summary>
        /// ID учащегося, для которого создается запись об успеваемости.
        /// </summary>
        private readonly int _studentId;

        /// <summary>
        /// Имя учащегося (отображается в заголовке формы).
        /// </summary>
        private readonly string _studentName;

        /// <summary>
        /// Конструктор формы. Принимает ID учащегося и его имя.
        /// </summary>
        /// <param name="studentId">ID учащегося.</param>
        /// <param name="studentName">ФИО учащегося.</param>
        public PerformanceForm(int studentId, string studentName)
        {
            InitializeComponent();
            _performanceManager = new PerformanceManager();
            _studentId = studentId;
            _studentName = studentName;

            // Настройка интерфейса
            this.Text = $"Учет успеваемости: {_studentName}";
            lblStudentName.Text = _studentName;
            dtpDate.Value = DateTime.Today;
        }


        /// <summary>
        /// Обработчик события кнопки "Сохранить".
        /// Сохраняет запись об успеваемости в базу данных.
        /// </summary>
        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                bool success = _performanceManager.AddPerformanceRecord(
                    txtSubject.Text.Trim(),
                    (int)nudGrade.Value,
                    dtpDate.Value,
                    chkPresence.Checked,
                    _studentId);

                if (success)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Проверяет корректность введенных данных.
        /// </summary>
        /// <returns>True, если все поля заполнены корректно; иначе — false.</returns>
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtSubject.Text))
            {
                MessageBox.Show("Введите название предмета", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSubject.Focus();
                return false;
            }

            return true;
        }
    }
}