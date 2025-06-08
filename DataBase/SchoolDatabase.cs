using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course
{
    /// <summary>
    /// Класс для работы с базой данных School.
    /// Предоставляет методы для выполнения SQL-запросов.
    /// </summary>
    internal class SchoolDatabase
    {
        /// <summary>
        /// Строка подключения к базе данных.
        /// </summary>
        private string connectionString;

        /// <summary>
        /// Конструктор. Инициализирует строку подключения к базе данных.
        /// </summary>
        public SchoolDatabase()
        {
            connectionString = "Data Source=LAPTOP-N4F3GBR9;Initial Catalog=School;Integrated Security=True;";
        }

        /// <summary>
        /// Создаёт и возвращает объект SqlConnection.
        /// </summary>
        /// <returns>Объект подключения к базе данных.</returns>
        private SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        /// <summary>
        /// Выполняет SQL-запрос, который возвращает данные (например, SELECT).
        /// </summary>
        /// <param name="query">SQL-запрос.</param>
        /// <param name="parameters">Параметры запроса (если есть).</param>
        /// <returns>Таблица с результатами запроса.</returns>
        public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand(query, connection))
            {
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                var dataTable = new DataTable();
                var adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(dataTable);

                return dataTable;
            }
        }

        /// <summary>
        /// Выполняет SQL-запрос, не возвращающий результат (например, INSERT, UPDATE, DELETE).
        /// </summary>
        /// <param name="query">SQL-запрос.</param>
        /// <param name="parameters">Параметры запроса (если есть).</param>
        /// <returns>Количество затронутых строк.</returns>
        public int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand(query, connection))
            {
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                connection.Open();
                return command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Выполняет SQL-запрос, возвращающий одно значение (например, агрегатная функция COUNT).
        /// </summary>
        /// <param name="query">SQL-запрос.</param>
        /// <param name="parameters">Параметры запроса (если есть).</param>
        /// <returns>Объект с возвращённым значением.</returns>
        public object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand(query, connection))
            {
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                connection.Open();
                return command.ExecuteScalar();
            }
        }
    }
}
