using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Interfaces
{
    public interface IActivityRepository
    {
        IReadOnlyList<Activity> GetActivities(string filter);
        void AddActivity(Activity activity);
        void UpdateActivity(Activity activity);
        void DeleteActivity(Activity activity);
    }
}
