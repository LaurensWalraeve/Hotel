using Hotel.Domain.Exceptions;
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
    public class MemberRepository : IMemberRepository
    {
        private string connectionString;

        public MemberRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IReadOnlyList<Member> GetMembers(string filter)
        {
            try
            {
                var members = new List<Member>();
                string sql = @"
                    SELECT 
                        m.Name AS MemberName, 
                        m.BirthDay, 
                        c.ID AS CustomerID, 
                        c.Name AS CustomerName, 
                        c.Email, 
                        c.Phone, 
                        c.Address
                    FROM Member m 
                    INNER JOIN Customer c ON m.CustomerID = c.ID 
                    WHERE m.Status = 1";

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sql += " AND c.ID = @filter";
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    if (!string.IsNullOrWhiteSpace(filter))
                    {
                        cmd.Parameters.AddWithValue("@filter", filter);
                    }
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string[] addressParts = reader["Address"].ToString().Split('|');
                            Address address = new Address(
                                city: addressParts.Length > 0 ? addressParts[0] : "",
                                postalCode: addressParts.Length > 1 ? addressParts[1] : "",
                                street: addressParts.Length > 2 ? addressParts[2] : "",
                                houseNumber: addressParts.Length > 3 ? addressParts[3] : ""
                            );

                            var customer = new Customer
                            {
                                Id = Convert.ToInt32(reader["CustomerID"]),
                                Name = reader["CustomerName"].ToString(),
                                Contact = new ContactInfo(
                                    email: reader["Email"].ToString(),
                                    phone: reader["Phone"].ToString(),
                                    address: address
                                )
                            };

                            var member = new Member(
                                name: reader["MemberName"].ToString(),
                                birthday: DateOnly.FromDateTime((DateTime)reader["BirthDay"]),
                                customer: customer
                            );
                            members.Add(member);
                        }
                    }
                }
                return members;
            }
            catch (Exception ex)
            {
                throw new MemberRepositoryException("GetMembers", ex);
            }
        }



        public void AddMember(Member member)
        {
            try
            {
                string sql = "INSERT INTO Member (Name, BirthDay, CustomerID, status) VALUES (@Name, @BirthDay, @CustomerID, @status)";
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Name", member.Name);
                    cmd.Parameters.AddWithValue("@BirthDay", member.Birthday.ToDateTime(TimeOnly.MinValue));
                    cmd.Parameters.AddWithValue("@CustomerID", member.Customer.Id);
                    cmd.Parameters.AddWithValue("@status", 1);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new MemberRepositoryException("AddMember", ex);
            }
        }

        public void UpdateMember(Member member, Member newMember)
        {
            try
            {
                string sql = @"
            UPDATE Member 
            SET 
                name = @NewName,
                birthday = @NewBirthday
            WHERE 
                name = @CurrentName AND 
                birthday = @CurrentBirthday AND 
                customerId = @CustomerID";

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // Parameters for the new data from newMember
                    cmd.Parameters.AddWithValue("@NewName", newMember.Name); // New name
                    cmd.Parameters.AddWithValue("@NewBirthday", newMember.Birthday.ToDateTime(TimeOnly.MinValue)); // New birthday

                    // Parameters to identify the record to update from member
                    cmd.Parameters.AddWithValue("@CurrentName", member.Name); // Current name
                    cmd.Parameters.AddWithValue("@CurrentBirthday", member.Birthday.ToDateTime(TimeOnly.MinValue)); // Current birthday
                    cmd.Parameters.AddWithValue("@CustomerID", member.Customer.Id); // Customer ID

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new MemberRepositoryException("UpdateMember", ex);
            }
        }



        public void DeleteMember(Member member)
        {
            try
            {
                string sql = "UPDATE Member SET status = 0 WHERE CustomerID = @CustomerID AND Name = @Name AND BirthDay = @BirthDay";
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@CustomerID", member.Customer.Id);
                    cmd.Parameters.AddWithValue("@Name", member.Name);
                    cmd.Parameters.AddWithValue("@BirthDay", member.Birthday.ToDateTime(TimeOnly.MinValue));
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new MemberRepositoryException("DeleteMember", ex);
            }
        }
    }
}
