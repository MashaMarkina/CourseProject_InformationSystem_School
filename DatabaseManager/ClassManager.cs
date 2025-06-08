using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Course
{
    /// <summary>
    /// Класс, управляющий действиями с таблицей Classes.
    /// Позволяет добавлять, редактировать, удалять и получать данные о классах.
    /// </summary>
    internal class ClassManager
    {
        /// <summary>
        /// Объект для работы с базой данных.
        /// </summary>
        private SchoolDatabase db;

        /// <summary>
        /// Конструктор. Инициализирует подключение к базе данных.
        /// </summary>
        public ClassManager()
        {
            db = new SchoolDatabase();
        }

        /// <summary>
        /// Добавляет новый класс в базу данных.
        /// </summary>
        /// <param name="className">Название класса.</param>
        /// <returns>ID созданного класса.</returns>
        public int AddClass(string className)
        {
            string query = @"INSERT INTO Classes (ClassName) VALUES (@ClassName);
                        SELECT SCOPE_IDENTITY();";

            SqlParameter parameter = new SqlParameter("@ClassName", className);

            return Convert.ToInt32(db.ExecuteScalar(query, new[] { parameter }));
        }

        /// <summary>
        /// Обновляет название класса.
        /// </summary>
        /// <param name="classId">ID класса.</param>
        /// <param name="className">Новое название класса.</param>
        /// <returns>True, если обновление успешно.</returns>
        public bool UpdateClass(int classId, string className)
        {
            string query = "UPDATE Classes SET ClassName = @ClassName WHERE ClassId = @ClassId";

            SqlParameter[] parameters = {
            new SqlParameter("@ClassId", classId),
            new SqlParameter("@ClassName", className)
        };

            return db.ExecuteNonQuery(query, parameters) > 0;
        }

        // <summary>
        /// Удаляет класс из базы, если в нем нет учеников.
        /// </summary>
        /// <param name="classId">ID класса.</param>
        /// <returns>True, если удаление успешно.</returns>
        public bool DeleteClass(int classId)
        {
            // Проверяем, есть ли ученики в классе
            string checkQuery = "SELECT COUNT(*) FROM Students WHERE ClassId = @ClassId";
            int studentCount = Convert.ToInt32(db.ExecuteScalar(checkQuery, new[] { new SqlParameter("@ClassId", classId) }));

            if (studentCount > 0)
            {
                MessageBox.Show("Невозможно удалить класс, так как в нем есть ученики!");
                return false;
            }

            string deleteQuery = "DELETE FROM Classes WHERE ClassId = @ClassId";
            return db.ExecuteNonQuery(deleteQuery, new[] { new SqlParameter("@ClassId", classId) }) > 0;
        }

        /// <summary>
        /// Получает список всех классов из базы.
        /// </summary>
        /// <returns>DataTable с ID и названиями классов.</returns>
        public DataTable GetAllClasses()
        {
            string query = "SELECT ClassId, ClassName FROM Classes ORDER BY ClassName";
            return db.ExecuteQuery(query);
        }

        /// <summary>
        /// Получает данные конкретного класса по его ID.
        /// </summary>
        /// <param name="classId">ID класса.</param>
        /// <returns>DataTable с данными класса.</returns>
        public DataTable GetClassById(int classId)
        {
            string query = "SELECT ClassId, ClassName FROM Classes WHERE ClassId = @ClassId";
            return db.ExecuteQuery(query, new[] { new SqlParameter("@ClassId", classId) });
        }

        /// <summary>
        /// Получает данные класса по его точному названию.
        /// </summary>
        /// <param name="name">Название класса.</param>
        /// <returns>DataTable с данными класса.</returns>
        public DataTable GetClassByName(string name)
        {
            string query = "SELECT ClassId, ClassName FROM Classes WHERE ClassName = @ClassName";
            return db.ExecuteQuery(query, new[] { new SqlParameter("@ClassName", name) });
        }

        /// <summary>
        /// Получает ID класса по его названию (нормализованному).
        /// </summary>
        /// <param name="className">Название класса.</param>
        /// <returns>ID класса или null, если класс не найден.</returns>
        public int? GetClassIdByName(string className)
        {
            string normalizedName = NormalizeClassName(className);
            string query = "SELECT ClassId FROM Classes WHERE REPLACE(ClassName, ' ', '') = @ClassName";

            SqlParameter[] parameters = {
            new SqlParameter("@ClassName", normalizedName)
        };

            object result = db.ExecuteScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : (int?)null;
        }

        /// <summary>
        /// Получает название класса по его ID.
        /// </summary>
        /// <param name="classId">ID класса.</param>
        /// <returns>Название класса или null, если не найден.</returns>
        public string GetClassNameById(int classId)
        {
            string query = "SELECT ClassName FROM Classes WHERE ClassId = @ClassId";

            SqlParameter[] parameters = {
            new SqlParameter("@ClassId", classId)
        };

            object result = db.ExecuteScalar(query, parameters);
            return result?.ToString();
        }

        /// <summary>
        /// Нормализует название класса: удаляет пробелы и кавычки, 
        /// оставляет цифры и буквы, буквы переводит в верхний регистр.
        /// Пример: "9 А" → "9А".
        /// </summary>
        /// <param name="className">Исходное название класса.</param>
        /// <returns>Нормализованное название класса.</returns>
        private string NormalizeClassName(string className)
        {
            className = className.Replace(" ", "").Replace("\"", "");

            if (className.Length < 2)
                return className;

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
    }
}
