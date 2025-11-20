using Npgsql;
using System;
using System.Data;

namespace InternshipNet.Services
{
    public class AdoNetService
    {
        // ВАЖЛИВО: Введіть сюди ваш пароль
        private readonly string _connectionString = "Host=localhost;Port=5432;Database=InternshipNetDb;Username=postgres;Password=p";

        // Метод 1: Виклик збереженої процедури (Stored Procedure)
        public void UpdateStatus(int studentId, int internshipId, int newStatus)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();

                // Викликаємо процедуру через SQL-команду CALL
                using (var cmd = new NpgsqlCommand("CALL sp_UpdateApplicationStatus(@sId, @iId, @st)", conn))
                {
                    cmd.Parameters.AddWithValue("sId", studentId);
                    cmd.Parameters.AddWithValue("iId", internshipId);
                    cmd.Parameters.AddWithValue("st", newStatus);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Метод 2: Отримання статистики (Чистий SELECT запит через ADO.NET)
        public int GetTotalApplicationsCount()
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM \"StudentApplications\"", conn))
                {
                    // ExecuteScalar повертає одне значення (першу клітинку)
                    var result = cmd.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
        }
    }
}