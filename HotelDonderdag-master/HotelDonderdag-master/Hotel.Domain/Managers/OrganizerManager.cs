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
    public class OrganizerManager
    {
        private IOrganizerRepository _organizerRepository;

        public OrganizerManager(IOrganizerRepository organizerRepository)
        {
            _organizerRepository = organizerRepository;
        }

        public IReadOnlyList<Organizer> GetOrganizers(string filter)
        {
            try
            {
                return _organizerRepository.GetOrganizers(filter);
            }
            catch (Exception ex)
            {
                throw new OrganizerManagerException("GetOrganizers");
            }
        }

        public void AddOrganizer(Organizer organizer)
        {
            try
            {
                _organizerRepository.AddOrganizer(organizer);
            }
            catch (Exception ex)
            {
                throw new OrganizerManagerException("AddOrganizers");
            }
        }

        public void DeleteOrganizer(Organizer organizer)
        {
            try
            {
                _organizerRepository.DeleteOrganizer(organizer);
            }
            catch (Exception ex)
            {
                throw new OrganizerManagerException("DeleteOrganizers");
            }
        }
        public void UpdateOrganizer(Organizer organizer)
        {
            try
            {
                _organizerRepository.UpdateOrganizer(organizer);
            }
            catch (Exception)
            {

                throw new OrganizerManagerException("UpdateOrganizers");
            }
        }


    }
}
