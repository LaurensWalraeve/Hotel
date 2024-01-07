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

        public void AddRegistrationCustomer(CustomerRegistration customerRegistration)
        {
            try
            {
                _reservationRepository.AddRegistrationCustomer(customerRegistration);
            }
            catch (Exception ex)
            {
                throw new ReservationManagerException("AddRegistrationCustomer");
            }
        }

        public void AddRegistrationMember(RegistrationMember registrationMember)
        {
            try
            {
                _reservationRepository.AddRegistrationMember(registrationMember);
            }
            catch (Exception ex)
            {
                throw new ReservationManagerException("AddRegistrationMember");
            }
        }
    }
}
