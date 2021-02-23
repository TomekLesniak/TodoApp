using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TodoLibrary.Models;
using TodoUI.ViewModels;
using Xunit;

namespace ToDoTests.ViewModels
{
    public class StatisticsTests
    {
        [Fact]
        public void TotalTasks_ListContainsTasks_ReturnsCorrectAmountOfTasks()
        {
            var tasks = new List<UserTasksModel>()
            {
                new UserTasksModel(),
                new UserTasksModel(),
                new UserTasksModel(),
                new UserTasksModel(),
            };
            var vm = new StatisticsViewModel(tasks);


            Assert.Contains("4", vm.TotalTasks);
        }

        [Fact]
        public void TotalTasks_ListWithNoTasks_ReturnsZero()
        {
            var tasks = new List<UserTasksModel>();
            var vm = new StatisticsViewModel(tasks);


            Assert.Contains("0", vm.TotalTasks);
        }

        [Fact]
        public void FailurePercentage_ListWithNoTasks_ReturnsZeroPercentage()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-us");
            
            var tasks = new List<UserTasksModel>();
            var vm = new StatisticsViewModel(tasks);

            Assert.Contains("0.0%", vm.FailurePercentage);
        }

        [Fact]
        public void FailurePercentage_ListContainsTask_ReturnsCalculatedFailurePercentage()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-us");

            var tasks = new List<UserTasksModel>
            {
                new UserTasksModel(){IsFinished = false, DateDeadLine = DateTime.Today.AddDays(-1)},
                new UserTasksModel(){IsFinished = false, DateDeadLine = DateTime.Today.AddDays(-1)},
                new UserTasksModel(){IsFinished = false, DateDeadLine = DateTime.Today.AddDays(-1)},
                new UserTasksModel(){IsFinished = false, DateDeadLine = DateTime.Today.AddDays(1)},
            };

            var vm = new StatisticsViewModel(tasks);

            Assert.Contains("75.00%", vm.FailurePercentage);
        }

        [Fact]
        public void CompletedPercentage_ListWithNoTasks_ReturnsZeroPercentage()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-us");

            var tasks = new List<UserTasksModel>();
            var vm = new StatisticsViewModel(tasks);

            Assert.Contains("0.0%", vm.CompletedPercentage);
        }

        [Fact]
        public void CompletedPercentage_ListContainsTask_ReturnsCalculatedFailurePercentage()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-us");

            var tasks = new List<UserTasksModel>
            {
                new UserTasksModel(){IsFinished = true, DateDeadLine = DateTime.Today.AddDays(-1)},
                new UserTasksModel(){IsFinished = true, DateDeadLine = DateTime.Today.AddDays(-1)},
                new UserTasksModel(){IsFinished = true, DateDeadLine = DateTime.Today.AddDays(-1)},
                new UserTasksModel(){IsFinished = false, DateDeadLine = DateTime.Today.AddDays(1)},
            };

            var vm = new StatisticsViewModel(tasks);

            Assert.Contains("75.00%", vm.CompletedPercentage);
        }
    }
}
