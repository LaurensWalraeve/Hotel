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


        public void AddRegistration(CustomerRegistration customerRegistration, List<RegistrationMember> registrationMembers)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Add customer registration
                        string sqlCustomerRegistration = @"
                                INSERT INTO CustomerRegistration (CustomerID, ActivityID, TotalCost, Status) 
                                VALUES (@CustomerID, @ActivityID, @TotalCost, @Status); 
                                SELECT SCOPE_IDENTITY();";  // To get the newly created ID

                        using (SqlCommand cmd = new SqlCommand(sqlCustomerRegistration, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@CustomerID", customerRegistration.Customer.Id);
                            cmd.Parameters.AddWithValue("@ActivityID", customerRegistration.Activity.ActivityID);
                            cmd.Parameters.AddWithValue("@TotalCost", customerRegistration.TotalCost);
                            cmd.Parameters.AddWithValue("@Status", 1); // Assuming '1' is for active/true

                            cmd.ExecuteNonQuery();
                        }

                        // Add each registration member
                        foreach (var registrationMember in registrationMembers)
                        {
                            string sqlRegistrationMember = @"
                                    INSERT INTO RegistrationMembers (CustomerID, ActivityID, MemberName, MemberBirthDay, Status) 
                                    VALUES (@CustomerID, @ActivityID, @MemberName, @MemberBirthDay, @Status);";

                            using (SqlCommand cmd = new SqlCommand(sqlRegistrationMember, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@CustomerID", registrationMember.Customer.Id);
                                cmd.Parameters.AddWithValue("@ActivityID", registrationMember.Activity.ActivityID);
                                cmd.Parameters.AddWithValue("@MemberName", registrationMember.Member.Name);

                                var memberBirthdayDateTime = new DateTime(registrationMember.Member.Birthday.Year, registrationMember.Member.Birthday.Month, registrationMember.Member.Birthday.Day);
                                cmd.Parameters.AddWithValue("@MemberBirthDay", memberBirthdayDateTime);

                                cmd.Parameters.AddWithValue("@Status", 1); // Assuming '1' is for active/true

                                cmd.ExecuteNonQuery();
                            }
                        }

                        // Commit the transaction
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        // Roll back the transaction
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

    }

}
