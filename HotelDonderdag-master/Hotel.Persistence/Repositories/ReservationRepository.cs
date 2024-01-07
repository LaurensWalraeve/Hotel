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
    public class ReservationRepository : IReservationRepository
    {
        private string connectionString;

        public ReservationRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void AddRegistrationCustomer(CustomerRegistration customerRegistration)
        {
            string sql = @"
    INSERT INTO CustomerRegistration (CustomerID, ActivityID, TotalCost, Status) 
    VALUES (@CustomerID, @ActivityID, @TotalCost, @Status); 
    SELECT SCOPE_IDENTITY();";  // To get the newly created ID

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@CustomerID", customerRegistration.Customer.Id);
                cmd.Parameters.AddWithValue("@ActivityID", customerRegistration.Activity.ActivityID);
                cmd.Parameters.AddWithValue("@TotalCost", customerRegistration.TotalCost);
                cmd.Parameters.AddWithValue("@Status", 1); // Assuming '1' is for active/true

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }



        public void AddRegistrationMember(RegistrationMember registrationMember)
        {
            string sql = @"
INSERT INTO RegistrationMembers (CustomerID, ActivityID, MemberName, MemberBirthDay, Status) 
VALUES (@CustomerID, @ActivityID, @MemberName, @MemberBirthDay, @Status);";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@CustomerID", registrationMember.Customer.Id);
                cmd.Parameters.AddWithValue("@ActivityID", registrationMember.Activity.ActivityID);
                cmd.Parameters.AddWithValue("@MemberName", registrationMember.Member.Name);

                // Convert DateOnly to DateTime
                var memberBirthdayDateTime = new DateTime(registrationMember.Member.Birthday.Year, registrationMember.Member.Birthday.Month, registrationMember.Member.Birthday.Day);
                cmd.Parameters.AddWithValue("@MemberBirthDay", memberBirthdayDateTime);

                cmd.Parameters.AddWithValue("@Status", 1); // Assuming '1' is for active/true

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }






    }
}
