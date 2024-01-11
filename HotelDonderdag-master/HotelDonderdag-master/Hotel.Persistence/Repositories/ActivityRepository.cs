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
    public class ActivityRepository : IActivityRepository
    {
        private string connectionString;

        public ActivityRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IReadOnlyList<Activity> GetActivities(string filter)
        {
            var activities = new List<Activity>();
            StringBuilder sql = new StringBuilder();
            sql.Append(@"
SELECT a.ActivityID, a.Description, a.Location, a.Duration, a.ActivityName, 
       a.DateScheduled, a.AvailableSpots, a.AdultPrice, a.ChildPrice, a.Discount, a.Status,
       o.OrganizerID, o.Name, o.Email, o.Phone, o.Address
FROM Activity a
INNER JOIN Organizer o ON a.OrganizerID = o.OrganizerID WHERE a.Status = 1");

            if (!string.IsNullOrEmpty(filter))
            {
                sql.Append(" AND a.OrganizerID = @OrganizerID AND a.Status = 1");
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
                if (!string.IsNullOrEmpty(filter))
                {
                    cmd.Parameters.AddWithValue("@OrganizerID", filter);
                }

                conn.Open(); ;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var activity = new Activity
                        {
                            ActivityID = reader.GetInt32(0),
                            Description = reader.GetString(1),
                            Location = reader.GetString(2),
                            Duration = reader.GetInt32(3),
                            ActivityName = reader.GetString(4),
                            DateScheduled = reader.GetDateTime(5),
                            AvailableSpots = reader.GetInt32(6),
                            AdultPrice = reader.GetDecimal(7),
                            ChildPrice = reader.GetDecimal(8),
                            Discount = reader.GetDecimal(9),
                            Status = reader.GetBoolean(10),
                            Organizer = new Organizer
                            {
                                OrganizerID = reader.GetInt32(11),
                                Name = reader.GetString(12),
                                Email = reader.GetString(13),
                                Phone = reader.GetString(14),
                                Address = new Address(reader.GetString(15)) // Assuming Address is a custom object
                            }
                        };
                        activities.Add(activity);
                    }
                }
            }
            return activities;
        }

        public void AddActivity(Activity activity)
        {
            string sql = "INSERT INTO Activity (OrganizerID, Description, Location, Duration, ActivityName, DateScheduled, AvailableSpots, AdultPrice, ChildPrice, Discount, Status) VALUES (@OrganizerID, @Description, @Location, @Duration, @ActivityName, @DateScheduled, @AvailableSpots, @AdultPrice, @ChildPrice, @Discount, @Status)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@OrganizerID", activity.Organizer.OrganizerID);
                cmd.Parameters.AddWithValue("@Description", activity.Description);
                cmd.Parameters.AddWithValue("@Location", activity.Location);
                cmd.Parameters.AddWithValue("@Duration", activity.Duration);
                cmd.Parameters.AddWithValue("@ActivityName", activity.ActivityName);
                cmd.Parameters.AddWithValue("@DateScheduled", activity.DateScheduled);
                cmd.Parameters.AddWithValue("@AvailableSpots", activity.AvailableSpots);
                cmd.Parameters.AddWithValue("@AdultPrice", activity.AdultPrice);
                cmd.Parameters.AddWithValue("@ChildPrice", activity.ChildPrice);
                cmd.Parameters.AddWithValue("@Discount", activity.Discount);
                cmd.Parameters.AddWithValue("@Status", 1);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public void UpdateActivity(Activity activity)
        {
            string sql = "UPDATE Activity SET OrganizerID = @OrganizerID, Description = @Description, Location = @Location, Duration = @Duration, ActivityName = @ActivityName, DateScheduled = @DateScheduled, AvailableSpots = @AvailableSpots, AdultPrice = @AdultPrice, ChildPrice = @ChildPrice, Discount = @Discount WHERE ActivityID = @ActivityID";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@ActivityID", activity.ActivityID);
                cmd.Parameters.AddWithValue("@OrganizerID", activity.Organizer.OrganizerID);
                cmd.Parameters.AddWithValue("@Description", activity.Description);
                cmd.Parameters.AddWithValue("@Location", activity.Location);
                cmd.Parameters.AddWithValue("@Duration", activity.Duration);
                cmd.Parameters.AddWithValue("@ActivityName", activity.ActivityName);
                cmd.Parameters.AddWithValue("@DateScheduled", activity.DateScheduled);
                cmd.Parameters.AddWithValue("@AvailableSpots", activity.AvailableSpots);
                cmd.Parameters.AddWithValue("@AdultPrice", activity.AdultPrice);
                cmd.Parameters.AddWithValue("@ChildPrice", activity.ChildPrice);
                cmd.Parameters.AddWithValue("@Discount", activity.Discount);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public void DeleteActivity(Activity activity)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        SqlCommand cmd = conn.CreateCommand();
                        cmd.Transaction = transaction;

                        try
                        {
                            // Update Status in CustomerRegistrations linked to this Activity
                            cmd.CommandText = "UPDATE CustomerRegistration SET Status = 0 WHERE ActivityID = @ActivityID";
                            cmd.Parameters.AddWithValue("@ActivityID", activity.ActivityID);
                            cmd.ExecuteNonQuery();

                            // Update Status in RegistrationMembers linked to this Activity
                            cmd.CommandText = "UPDATE RegistrationMembers SET Status = 0 WHERE ActivityID = @ActivityID";
                            cmd.ExecuteNonQuery();

                            // Finally, update Status in Activity
                            cmd.CommandText = "UPDATE Activity SET Status = 0 WHERE ActivityID = @ActivityID";
                            cmd.ExecuteNonQuery();

                            transaction.Commit();
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ActivityRepositoryException("Error deleting activity", ex);
            }
        }

    }
}
