﻿using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Interfaces
{
    public interface IReservationRepository
    {
        void AddRegistration(CustomerRegistration customerRegistration, List<RegistrationMember> registrationMembers);

    }
}
