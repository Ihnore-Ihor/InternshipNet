using Npgsql;
using System;
using System.Data;

namespace InternshipNet.Services
{
    public class AdoNetService
    {
        private readonly string _connectionString = "Host=localhost;Port=5432;Database=InternshipNetDb;Username=postgres;Password=p";

        // Call a stored procedure to update application status
        public void UpdateStatus(int studentId, int internshipId, int newStatus)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();

                // Call the stored procedure using a SQL CALL command
                using (var cmd = new NpgsqlCommand("CALL sp_UpdateApplicationStatus(@sId, @iId, @st)", conn))
                {
                    cmd.Parameters.AddWithValue("sId", studentId);
                    cmd.Parameters.AddWithValue("iId", internshipId);
                    cmd.Parameters.AddWithValue("st", newStatus);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Get total number of student applications using a SELECT query
        public int GetTotalApplicationsCount()
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM \"StudentApplications\"", conn))
                {
                    // ExecuteScalar returns a single value (the first cell of the result)
                    var result = cmd.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
        }
    }
}