using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using Hotel.Persistence.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Persistence.Repositories
{
    public class OrganizerRepository : IOrganizerRepository
    {
        private string connectionString;

        public OrganizerRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IReadOnlyList<Organizer> GetOrganizers(string filter)
        {
            var organizers = new List<Organizer>();
            string sql = "SELECT OrganizerID, Name, Email, Phone, Address FROM Organizer WHERE Status = 1";

            if (!string.IsNullOrEmpty(filter))
            {
                sql += " WHERE Name LIKE @filter OR Email LIKE @filter";
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (!string.IsNullOrEmpty(filter))
                {
                    cmd.Parameters.AddWithValue("@filter", $"%{filter}%");
                }

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var organizer = new Organizer
                        {
                            OrganizerID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Email = reader.GetString(2),
                            Phone = reader.GetString(3),
                            Address = new Address(reader.GetString(4))

                        };
                        organizers.Add(organizer);
                    }
                }
            }
            return organizers;
        }

        public void AddOrganizer(Organizer organizer)
        {
            string sql = "INSERT INTO Organizer (Name, Email, Phone, Address, Status) VALUES (@Name, @Email, @Phone, @Address, @Status)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Name", organizer.Name);
                cmd.Parameters.AddWithValue("@Email", organizer.Email);
                cmd.Parameters.AddWithValue("@Phone", organizer.Phone);
                cmd.Parameters.AddWithValue("@Address", organizer.Address.ToAddressLine());
                cmd.Parameters.AddWithValue("@Status", 1);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateOrganizer(Organizer organizer)
        {
            string sql = "UPDATE Organizer SET Name = @Name, Email = @Email, Phone = @Phone, Address = @Address WHERE OrganizerID = @OrganizerID";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Name", organizer.Name);
                cmd.Parameters.AddWithValue("@Email", organizer.Email);
                cmd.Parameters.AddWithValue("@Phone", organizer.Phone);
                cmd.Parameters.AddWithValue("@Address", organizer.Address.ToAddressLine());
                cmd.Parameters.AddWithValue("@OrganizerID", organizer.OrganizerID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteOrganizer(Organizer organizer)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Start a transaction
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        SqlCommand cmd = conn.CreateCommand();
                        cmd.Transaction = transaction;

                        try
                        {
                            // Step 1: Update Status in Activities linked to this Organizer
                            cmd.CommandText = "UPDATE Activity SET Status = 0 WHERE OrganizerID = @OrganizerID";
                            cmd.Parameters.AddWithValue("@OrganizerID", organizer.OrganizerID);
                            cmd.ExecuteNonQuery();

                            // Step 2: Update Status in CustomerRegistrations linked to Activities of this Organizer
                            cmd.CommandText = "UPDATE CustomerRegistration SET Status = 0 WHERE ActivityID IN (SELECT ActivityID FROM Activity WHERE OrganizerID = @OrganizerID)";
                            cmd.ExecuteNonQuery();

                            // Step 3: Update Status in RegistrationMembers linked to CustomerRegistrations of Activities of this Organizer
                            cmd.CommandText = "UPDATE RegistrationMembers SET Status = 0 WHERE CustomerID IN (SELECT CustomerID FROM CustomerRegistration WHERE ActivityID IN (SELECT ActivityID FROM Activity WHERE OrganizerID = @OrganizerID))";
                            cmd.ExecuteNonQuery();

                            // Step 4: Finally, update Status in Organizer
                            cmd.CommandText = "UPDATE Organizer SET Status = 0 WHERE OrganizerID = @OrganizerID";
                            cmd.ExecuteNonQuery();

                            // Commit the transaction
                            transaction.Commit();
                        }
                        catch
                        {
                            // Attempt to roll back the transaction
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new OrganizerRepositoryException("Error deleting organizer", ex);
            }
        }




    }
}
