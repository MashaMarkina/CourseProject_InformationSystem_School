using System;
using System.Data;
using System.Data.SqlClient;

namespace Course
{
    /// <summary>
    /// Менеджер для операций с таблицей учащихся (Students).
    /// Позволяет добавлять, обновлять, удалять и искать учащихся.
    /// </summary>
    internal class StudentManager
    {
        /// <summary>
        /// Экземпляр класса доступа к базе данных.
        /// </summary>
        private readonly SchoolDatabase _db;

        /// <summary>
        /// Конструктор. Инициализирует соединение с базой данных.
        /// </summary>
        public StudentManager()
        {
            _db = new SchoolDatabase();
        }

        /// <summary>
        /// Добавляет нового ученика в базу данных.
        /// </summary>
        /// <returns>ID нового ученика.</returns>
        public int AddStudent(string lastName, string firstName, string middleName,
                            DateTime birthDate, string phone, string email,
                            string password, string address, int classId)
        {
            string query = @"INSERT INTO Students 
                           (LastName, FirstName, MiddleName, BirthDate, 
                            Phone, Email, Password, Address, ClassId)
                           VALUES 
                           (@LastName, @FirstName, @MiddleName, @BirthDate, 
                            @Phone, @Email, @Password, @Address, @ClassId);
                           SELECT SCOPE_IDENTITY();";

            SqlParameter[] parameters = {
                new SqlParameter("@LastName", lastName),
                new SqlParameter("@FirstName", firstName),
                new SqlParameter("@MiddleName", string.IsNullOrEmpty(middleName) ? (object)DBNull.Value : middleName),
                new SqlParameter("@BirthDate", birthDate),
                new SqlParameter("@Phone", string.IsNullOrEmpty(phone) ? (object)DBNull.Value : phone),
                new SqlParameter("@Email", string.IsNullOrEmpty(email) ? (object)DBNull.Value : email),
                new SqlParameter("@Password", password), // Пароль теперь обязательный
                new SqlParameter("@Address", string.IsNullOrEmpty(address) ? (object)DBNull.Value : address),
                new SqlParameter("@ClassId", classId)
            };

            return Convert.ToInt32(_db.ExecuteScalar(query, parameters));
        }

        /// <summary>
        /// Возвращает подробную информацию об ученике по его ID.
        /// </summary>
        public DataTable GetStudentDetails(int studentId)
        {
            string query = @"SELECT s.*, c.ClassName 
                   FROM Students s
                   JOIN Classes c ON s.ClassId = c.ClassId
                   WHERE s.StudentId = @StudentId";

            SqlParameter[] parameters = { new SqlParameter("@StudentId", studentId) };
            return _db.ExecuteQuery(query, parameters);
        }

        /// <summary>
        /// Обновляет данные ученика.
        /// </summary>
        public bool UpdateStudent(int studentId, string lastName, string firstName,
                                string middleName, DateTime birthDate, string phone,
                                string email, string password, string address, int classId)
        {
            string query = @"UPDATE Students SET
                   LastName = @LastName,
                   FirstName = @FirstName,
                   MiddleName = @MiddleName,
                   BirthDate = @BirthDate,
                   Phone = @Phone,
                   Email = @Email,
                   Password = ISNULL(@Password, Password),
                   Address = @Address,
                   ClassId = @ClassId
                   WHERE StudentId = @StudentId";

            SqlParameter[] parameters = {
        new SqlParameter("@StudentId", studentId),
        new SqlParameter("@LastName", lastName),
        new SqlParameter("@FirstName", firstName),
        new SqlParameter("@MiddleName", middleName ?? (object)DBNull.Value),
        new SqlParameter("@BirthDate", birthDate),
        new SqlParameter("@Phone", phone),
        new SqlParameter("@Email", email ?? (object)DBNull.Value),
        new SqlParameter("@Password", password ?? (object)DBNull.Value),
        new SqlParameter("@Address", address ?? (object)DBNull.Value),
        new SqlParameter("@ClassId", classId)
    };

            return _db.ExecuteNonQuery(query, parameters) > 0;
        }

        /// <summary>
        /// Удаляет ученика и все связанные с ним записи об успеваемости.
        /// </summary>
        public bool DeleteStudent(int studentId)
        {
            // 1. Удаляем связанные записи из SchoolPerformance
            string deleteSchoolPerformanceQuery = @"DELETE FROM SchoolPerformance WHERE StudentId = @StudentId";

            // Создаем ПЕРВЫЙ параметр (для SchoolPerformance)
            SqlParameter studentIdParamPerformance = new SqlParameter("@StudentId", studentId);
            SqlParameter[] parametersPerformance = { studentIdParamPerformance };

            // Выполняем запрос на удаление из SchoolPerformance
            int rowsAffectedPerformance = _db.ExecuteNonQuery(deleteSchoolPerformanceQuery, parametersPerformance);
            Console.WriteLine($"Удалено {rowsAffectedPerformance} записей из SchoolPerformance для StudentId = {studentId}");

            // 2. Удаляем студента из Students
            string deleteStudentQuery = @"DELETE FROM Students WHERE StudentId = @StudentId";

            // Создаем ВТОРОЙ параметр (для Students)
            SqlParameter studentIdParamStudent = new SqlParameter("@StudentId", studentId);
            SqlParameter[] parametersStudent = { studentIdParamStudent };


            // Выполняем запрос на удаление из Students
            int rowsAffectedStudent = _db.ExecuteNonQuery(deleteStudentQuery, parametersStudent);

            return rowsAffectedStudent > 0;
        }

        /// <summary>
        /// Получает данные об ученике по ID.
        /// </summary>
        public DataTable GetStudentById(int studentId)
        {
            string query = @"SELECT StudentId, LastName, FirstName, MiddleName, 
                           BirthDate, Phone, Email, Password, Address, ClassId
                           FROM Students 
                           WHERE StudentId = @StudentId";

            return _db.ExecuteQuery(query, new[] { new SqlParameter("@StudentId", studentId) });
        }

        /// <summary>
        /// Получает список учеников, принадлежащих определенному классу.
        /// </summary>
        public DataTable GetStudentsByClass(int classId)
        {
            string query = @"SELECT StudentId, LastName, FirstName, MiddleName, BirthDate, Phone
                           FROM Students 
                           WHERE ClassId = @ClassId
                           ORDER BY LastName, FirstName";

            return _db.ExecuteQuery(query, new[] { new SqlParameter("@ClassId", classId) });
        }

        /// <summary>
        /// Выполняет поиск учеников по фамилии.
        /// </summary>
        public DataTable SearchStudents(string lastName)
        {
            string query = @"SELECT s.StudentId, s.LastName, s.FirstName, s.MiddleName, c.ClassName
                           FROM Students s
                           JOIN Classes c ON s.ClassId = c.ClassId
                           WHERE s.LastName LIKE @LastName
                           ORDER BY c.ClassName, s.LastName";

            return _db.ExecuteQuery(query, new[] { new SqlParameter("@LastName", $"%{lastName}%") });
        }


        /// <summary>
        /// Получает полный список всех учеников с их основными данными.
        /// </summary>
        public DataTable GetAllStudents()
        {
            string query = @"SELECT 
                    s.StudentId, 
                    s.LastName, 
                    s.FirstName, 
                    s.MiddleName, 
                    s.BirthDate,
                    s.Phone,
                    s.Email,
                    s.Address,
                    c.ClassName
                    FROM Students s
                    JOIN Classes c ON s.ClassId = c.ClassId
                    ORDER BY c.ClassName, s.LastName, s.FirstName";

            return _db.ExecuteQuery(query);
        }

        /// <summary>
        /// Проверяет, существуют ли в базе введенные email и пароль.
        /// Используется для аутентификации.
        /// </summary>
        public bool ValidateStudentCredentials(string email, string password)
        {
            string query = "SELECT COUNT(*) FROM Students WHERE Email = @Email AND Password = @Password";

            SqlParameter[] parameters = {
                new SqlParameter("@Email", email),
                new SqlParameter("@Password", password)
            };

            int count = Convert.ToInt32(_db.ExecuteScalar(query, parameters));
            return count > 0;
        }
    }
}