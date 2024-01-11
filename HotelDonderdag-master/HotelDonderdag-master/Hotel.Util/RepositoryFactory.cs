using Hotel.Domain.Interfaces;
using Hotel.Persistence.Repositories;
using System.Configuration;

namespace Hotel.Util
{
    public static class RepositoryFactory
    {
        public static ICustomerRepository CustomerRepository { get { return new CustomerRepository("Data Source=DESKTOP-TENLUD8\\SQLEXPRESS;Initial Catalog=HotelDonderdag;Integrated Security=True"); } }
        public static IMemberRepository MemberRepository { get { return new MemberRepository("Data Source=DESKTOP-TENLUD8\\SQLEXPRESS;Initial Catalog=HotelDonderdag;Integrated Security=True"); } }
        public static IOrganizerRepository OrganizerRepository { get { return new OrganizerRepository("Data Source=DESKTOP-TENLUD8\\SQLEXPRESS;Initial Catalog=HotelDonderdag;Integrated Security=True"); } }
        public static IActivityRepository ActivityRepository { get { return new ActivityRepository("Data Source=DESKTOP-TENLUD8\\SQLEXPRESS;Initial Catalog=HotelDonderdag;Integrated Security=True"); } }
        public static IReservationRepository ReservationRepository { get { return new ReservationRepository("Data Source=DESKTOP-TENLUD8\\SQLEXPRESS;Initial Catalog=HotelDonderdag;Integrated Security=True"); } }

    }
}