using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TodoLibrary.Models;

namespace TodoLibrary.Logic
{
    /// <summary>
    /// Implementation of IStatistics calculation interface
    /// </summary>
    public class Statistics : IStatistics
    {
        private readonly List<UserTasksModel> _userTasks;

        /// <summary>
        /// Initializes user tasks list.
        /// </summary>
        /// <param name="userTasks">List of all tasks of particular user</param>
        public Statistics(List<UserTasksModel> userTasks)
        {
            _userTasks = userTasks;
        }


        public int GetTotalAmountOfTasks()
        {
            int output = _userTasks.Count;
            return output;
        }

        public decimal GetCompletedPercentage()
        {
            decimal output = 0.0m;

            if(GetTotalAmountOfTasks() > 0)
            {
                int completedTasks = _userTasks.Count(x => x.IsFinished == true);
                int totalTasks = GetTotalAmountOfTasks();

                output = Math.Round(((decimal)completedTasks / totalTasks) * 100, 2);
            }

            return output;
        }

        public decimal GetFailurePercentage()
        {
            decimal output = 0.0m;

            if (GetTotalAmountOfTasks() > 0)
            {
                int failedTasks = _userTasks.Count(x => x.IsFinished == false && x.DateDeadLine < DateTime.Today);
                if (failedTasks > 0)
                {
                    int totalTasks = GetTotalAmountOfTasks();
                    output = Math.Round(((decimal) failedTasks / totalTasks) * 100, 2);
                }
            }

            return output;
        }
    }
}
