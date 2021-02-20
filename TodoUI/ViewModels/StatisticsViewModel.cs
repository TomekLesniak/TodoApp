using System;
using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;
using TodoLibrary.Logic;
using TodoLibrary.Models;

namespace TodoUI.ViewModels
{
    public class StatisticsViewModel : Screen
    {
        private readonly List<UserTasksModel> _userTasks;
        private readonly IStatistics _statistics;
        private int _totalTasks;
        private decimal _failurePercentage;
        private decimal _completedPercentage;

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

        public string TotalTasks => _totalTasks.ToString();
        public string FailurePercentage => $"{_failurePercentage}%";
        public string CompletedPercentage => $"{_completedPercentage}%";
    }
}
