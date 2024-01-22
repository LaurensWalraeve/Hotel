using Hotel.Domain.Exceptions;
using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Managers
{
    public class ReservationManager
    {
        private IReservationRepository _reservationRepository;

        public ReservationManager(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public void AddRegistration(CustomerRegistration customerRegistration, List<RegistrationMember> registrationMembers)
        {
            try
            {
                _reservationRepository.AddRegistration(customerRegistration, registrationMembers);
            }
            catch (Exception ex)
            {
                throw new ReservationManagerException("AddRegistrationCustomer");
            }
        }


    }
}
