using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Course.DatabaseManager
{
    /// <summary>
    /// Менеджер для работы с данными об успеваемости студентов.
    /// Включает добавление, обновление, удаление и отчётность по оценкам и посещаемости.
    /// </summary>
    public class PerformanceManager
    {
        /// <summary>
        /// Объект для подключения к базе данных.
        /// </summary>
        private readonly SchoolDatabase _db;

        /// <summary>
        /// Конструктор класса. Инициализирует подключение к базе данных.
        /// </summary>
        public PerformanceManager()
        {
            _db = new SchoolDatabase();
        }

        /// <summary>
        /// Добавляет новую запись об успеваемости студента.
        /// </summary>
        /// <param name="subjectName">Название предмета.</param>
        /// <param name="gradeValue">Оценка.</param>
        /// <param name="date">Дата.</param>
        /// <param name="presence">Присутствие (true/false).</param>
        /// <param name="studentId">ID студента.</param>
        /// <returns>True, если добавление прошло успешно.</returns>
        public bool AddPerformanceRecord(string subjectName, int gradeValue, DateTime date, bool presence, int studentId)
        {
            string query = @"INSERT INTO SchoolPerformance 
                           (SubjectName, GradeValue, Date, Presence, StudentId)
                           VALUES 
                           (@SubjectName, @GradeValue, @Date, @Presence, @StudentId)";

            SqlParameter[] parameters = {
                new SqlParameter("@SubjectName", subjectName),
                new SqlParameter("@GradeValue", gradeValue),
                new SqlParameter("@Date", date),
                new SqlParameter("@Presence", presence),
                new SqlParameter("@StudentId", studentId)
            };

            return _db.ExecuteNonQuery(query, parameters) > 0;
        }


        /// <summary>
        /// Получает все записи об успеваемости конкретного студента.
        /// </summary>
        /// <param name="studentId">ID студента.</param>
        /// <returns>Таблица с записями об успеваемости.</returns>
        public DataTable GetStudentPerformance(int studentId)
        {
            string query = @"SELECT SchoolPerformanceId, SubjectName, GradeValue, 
                           CONVERT(varchar, Date, 104) as Date, Presence,
                           CASE WHEN Presence = 1 THEN 'Присутствовал' ELSE 'Отсутствовал' END as PresenceStatus
                           FROM SchoolPerformance 
                           WHERE StudentId = @StudentId
                           ORDER BY Date DESC";

            return _db.ExecuteQuery(query, new[] { new SqlParameter("@StudentId", studentId) });
        }

        /// <summary>
        /// Обновляет оценку и присутствие для конкретной записи.
        /// </summary>
        /// <param name="recordId">ID записи об успеваемости.</param>
        /// <param name="newGradeValue">Новая оценка.</param>
        /// <param name="newPresence">Новое значение присутствия.</param>
        /// <returns>True, если обновление прошло успешно.</returns>
        public bool UpdatePerformanceRecord(int recordId, int newGradeValue, bool newPresence)
        {
            string query = @"UPDATE SchoolPerformance 
                           SET GradeValue = @GradeValue, 
                           Presence = @Presence 
                           WHERE SchoolPerformanceId = @RecordId";

            SqlParameter[] parameters = {
                new SqlParameter("@RecordId", recordId),
                new SqlParameter("@GradeValue", newGradeValue),
                new SqlParameter("@Presence", newPresence)
            };

            return _db.ExecuteNonQuery(query, parameters) > 0;
        }

        /// <summary>
        /// Удаляет запись об успеваемости по её ID.
        /// </summary>
        /// <param name="recordId">ID записи.</param>
        /// <returns>True, если удаление прошло успешно.</returns>
        public bool DeletePerformanceRecord(int recordId)
        {
            string query = "DELETE FROM SchoolPerformance WHERE SchoolPerformanceId = @RecordId";
            return _db.ExecuteNonQuery(query, new[] { new SqlParameter("@RecordId", recordId) }) > 0;
        }

        /// <summary>
        /// Генерирует отчёт по успеваемости студентов с фильтрацией по классу, предмету и дате.
        /// </summary>
        /// <param name="classId">ID класса (может быть null).</param>
        /// <param name="subjectId">ID предмета (может быть null).</param>
        /// <param name="startDate">Начальная дата отчета.</param>
        /// <param name="endDate">Конечная дата отчета.</param>
        /// <returns>Таблица с результатами отчета.</returns>
        public DataTable GeneratePerformanceReport(int? classId, int? subjectId, DateTime startDate, DateTime endDate)
        {
            string query = @"
                SELECT 
                    s.StudentId,
                    s.LastName + ' ' + s.FirstName AS StudentFullName,
                    c.ClassName,
                    sp.SubjectName,
                    AVG(sp.GradeValue) AS AverageGrade,
                    COUNT(sp.GradeValue) AS GradesCount,
                    SUM(CASE WHEN sp.Presence = 0 THEN 1 ELSE 0 END) AS AbsencesCount
                FROM SchoolPerformance sp
                INNER JOIN Students s ON sp.StudentId = s.StudentId
                INNER JOIN Classes c ON s.ClassId = c.ClassId
                WHERE sp.Date BETWEEN @StartDate AND @EndDate
                    AND (@ClassId IS NULL OR c.ClassId = @ClassId)
                    AND (@SubjectId IS NULL OR sp.SubjectName = 
                        (SELECT SubjectName FROM Subjects WHERE SubjectId = @SubjectId))
                GROUP BY s.StudentId, s.LastName, s.FirstName, c.ClassName, sp.SubjectName
                ORDER BY c.ClassName, s.LastName, s.FirstName";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };

            if (classId.HasValue)
                parameters.Add(new SqlParameter("@ClassId", classId.Value));
            else
                parameters.Add(new SqlParameter("@ClassId", DBNull.Value));

            if (subjectId.HasValue)
                parameters.Add(new SqlParameter("@SubjectId", subjectId.Value));
            else
                parameters.Add(new SqlParameter("@SubjectId", DBNull.Value));

            return _db.ExecuteQuery(query, parameters.ToArray());
        }

        /// <summary>
        /// Получает список предметов и статистику по каждому предмету для конкретного студента.
        /// </summary>
        /// <param name="studentId">ID студента.</param>
        /// <returns>Таблица с предметами, средней оценкой и количеством пропусков.</returns>
        public DataTable GetSubjectsPerformance(int studentId)
        {
            string query = @"
                SELECT 
                    SubjectName,
                    AVG(GradeValue) AS AvgGrade,
                    COUNT(GradeValue) AS GradesCount,
                    SUM(CASE WHEN Presence = 0 THEN 1 ELSE 0 END) AS AbsencesCount
                FROM SchoolPerformance
                WHERE StudentId = @StudentId
                GROUP BY SubjectName
                ORDER BY SubjectName";

            return _db.ExecuteQuery(query, new[] { new SqlParameter("@StudentId", studentId) });
        }

        /// <summary>
        /// Получает расширенный отчёт по успеваемости с возможностью фильтрации по предмету и классу.
        /// </summary>
        /// <param name="classId">ID класса (может быть null).</param>
        /// <param name="subjectName">Название предмета (может быть null).</param>
        /// <param name="startDate">Начальная дата отчета.</param>
        /// <param name="endDate">Конечная дата отчета.</param>
        /// <returns>Таблица с отчётом по успеваемости.</returns>
        public DataTable GetPerformanceReport(int? classId, string subjectName, DateTime startDate, DateTime endDate)
        {
            try
            {
                string query = @"
    SELECT 
        s.StudentId,
        s.LastName + ' ' + s.FirstName AS StudentFullName,
        c.ClassName,
        sp.SubjectName,
        AVG(CAST(sp.GradeValue AS FLOAT)) AS AverageGrade,
        COUNT(sp.GradeValue) AS GradesCount,
        SUM(CASE WHEN sp.Presence = 0 THEN 1 ELSE 0 END) AS AbsencesCount
    FROM SchoolPerformance sp
    INNER JOIN Students s ON sp.StudentId = s.StudentId
    INNER JOIN Classes c ON s.ClassId = c.ClassId
    WHERE sp.Date BETWEEN @StartDate AND @EndDate
        AND (@ClassId IS NULL OR c.ClassId = @ClassId)
        AND (@SubjectName IS NULL OR sp.SubjectName = @SubjectName)
    GROUP BY s.StudentId, s.LastName, s.FirstName, c.ClassName, sp.SubjectName
    ORDER BY c.ClassName, s.LastName, s.FirstName";

                var parameters = new List<SqlParameter>
        {
            new SqlParameter("@StartDate", startDate),
            new SqlParameter("@EndDate", endDate),
            new SqlParameter("@SubjectName", string.IsNullOrEmpty(subjectName) ? (object)DBNull.Value : subjectName),
            new SqlParameter("@ClassId", classId ?? (object)DBNull.Value)
        };

                DataTable result = _db.ExecuteQuery(query, parameters.ToArray());

                // Логирование результата запроса
                Debug.WriteLine($"Запрос выполнен. Получено строк: {result.Rows.Count}");
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка в GetPerformanceReport: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Получает список всех уникальных названий предметов из таблицы SchoolPerformance.
        /// </summary>
        /// <returns>Таблица с названиями предметов.</returns>
        public DataTable GetAllSubjects()
        {
            string query = "SELECT DISTINCT SubjectName FROM SchoolPerformance ORDER BY SubjectName";
            return _db.ExecuteQuery(query);
        }
    }
}
