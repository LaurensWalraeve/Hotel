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
    public class ActivityManager
    {
        private IActivityRepository _activityRepository;

        public ActivityManager(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public IReadOnlyList<Activity> GetActivities(string filter)
        {
            try
            {
                return _activityRepository.GetActivities(filter);
            }
            catch (Exception ex)
            {
                throw new ActivityManagerException("GetActivities");
            }
        }

        public void AddActivity(Activity activity)
        {
            try
            {
                _activityRepository.AddActivity(activity);
            }
            catch (Exception ex)
            {
                throw new ActivityManagerException("AddActivity");
            }
        }

        public void UpdateActivity(Activity activity)
        {
            try
            {
                _activityRepository.UpdateActivity(activity);
            }
            catch (Exception ex)
            {
                throw new ActivityManagerException("UpdateActivity");
            }
        }

        public void DeleteActivity(Activity activity)
        {
            try
            {
                _activityRepository.DeleteActivity(activity);
            }
            catch (Exception ex)
            {
                throw new ActivityManagerException("DeleteActivity");
            }
        }
    }
}
