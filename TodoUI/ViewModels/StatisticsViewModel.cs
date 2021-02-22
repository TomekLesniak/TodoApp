using System;
using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;
using TodoLibrary.Logic;
using TodoLibrary.Models;

namespace TodoUI.ViewModels
{
    /// <summary>
    /// View model for corresponding StatisticsView
    /// </summary>
    public class StatisticsViewModel : Screen
    {
        private readonly List<UserTasksModel> _userTasks;
        private readonly IStatistics _statistics;
        private int _totalTasks;
        private decimal _failurePercentage;
        private decimal _completedPercentage;

        /// <summary>
        /// Initialize all statistics.
        /// </summary>
        /// <param name="userTasks">List of all tasks for given user</param>
        public StatisticsViewModel(List<UserTasksModel> userTasks)
        {
            _userTasks = userTasks;
            _statistics = new Statistics(userTasks);
            
            InitializeStatistics();
        }
        
        private void InitializeStatistics()
        {
            _totalTasks = _statistics.GetTotalAmountOfTasks();
            _failurePercentage = _statistics.GetFailurePercentage();
            _completedPercentage = _statistics.GetCompletedPercentage();
        }

        /// <summary>
        /// Display amount of tasks created.
        /// </summary>
        public string TotalTasks => _totalTasks.ToString();
        
        /// <summary>
        /// Displays percentage of tasks failed.
        /// </summary>
        public string FailurePercentage => $"{_failurePercentage}%";
        
        /// <summary>
        /// Displays percentage of tasks completed.
        /// </summary>
        public string CompletedPercentage => $"{_completedPercentage}%";
    }
}
