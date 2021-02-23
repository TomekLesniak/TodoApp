using System;
using System.Collections.Generic;
using System.Text;
using TodoLibrary.Logic;
using TodoLibrary.Models;
using Xunit;

namespace ToDoTests.ClassLibrary
{
    
    public class StatisticsTest
    {
        [Fact]
        public void GetTotalAmountOfTasks_ValidList_ReturnsCorrectAmount()
        {
            var userTasks = new List<UserTasksModel>
            {
                new UserTasksModel(),
                new UserTasksModel(),
                new UserTasksModel(),
            };
            IStatistics stats = new Statistics(userTasks);

            var actual = stats.GetTotalAmountOfTasks();

            Assert.Equal(3, actual);
        }

        [Fact]
        public void GetTotalAmountOfTasks_NoUserTasks_ReturnsZero()
        {
            var userTasks = new List<UserTasksModel>();
            IStatistics stats = new Statistics(userTasks);

            var actual = stats.GetTotalAmountOfTasks();

            Assert.Equal(0, actual);
        }

        [Fact]
        public void GetCompletedPercentage_ValidList_ReturnsCalculatedCompletionInPercentage()
        {
            var userTasks = new List<UserTasksModel>
            {
                new UserTasksModel(){IsFinished = true},
                new UserTasksModel(){IsFinished = true},
                new UserTasksModel(){IsFinished = false},
            };
            IStatistics stats = new Statistics(userTasks);

            var actual = stats.GetCompletedPercentage();
            var expected = 66.67m;
            
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetCompletedPercentage_AllCompleted_ReturnsCalculatedCompletionInPercentage()
        {
            var userTasks = new List<UserTasksModel>
            {
                new UserTasksModel(){IsFinished = true},
                new UserTasksModel(){IsFinished = true},
                new UserTasksModel(){IsFinished = true},
            };
            IStatistics stats = new Statistics(userTasks);

            var actual = stats.GetCompletedPercentage();
            var expected = 100.0m;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetCompletedPercentage_NothingCompleted_ReturnsCalculatedCompletionInPercentage()
        {
            var userTasks = new List<UserTasksModel>
            {
                new UserTasksModel(){IsFinished = false},
                new UserTasksModel(){IsFinished = false},
                new UserTasksModel(){IsFinished = false},
            };
            IStatistics stats = new Statistics(userTasks);

            var actual = stats.GetCompletedPercentage();
            var expected = 0.0m;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetFailurePercentage_ValidList_ReturnsCalculatedCompletionInPercentage()
        {
            var userTasks = new List<UserTasksModel>
            {
                new UserTasksModel(){IsFinished = true, DateDeadLine = DateTime.Today.AddDays(1)},
                new UserTasksModel(){IsFinished = true, DateDeadLine = DateTime.Today.AddDays(1)},
                new UserTasksModel(){IsFinished = false, DateDeadLine = DateTime.Today.AddDays(-1)},
            };
            IStatistics stats = new Statistics(userTasks);

            var actual = stats.GetFailurePercentage();
            var expected = 33.33m;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetFailurePercentage_AllCompleted_ReturnsCalculatedCompletionInPercentage()
        {
            var userTasks = new List<UserTasksModel>
            {
                new UserTasksModel(){IsFinished = true, DateDeadLine = DateTime.Today.AddDays(1)},
                new UserTasksModel(){IsFinished = true, DateDeadLine = DateTime.Today.AddDays(1)},
                new UserTasksModel(){IsFinished = true, DateDeadLine = DateTime.Today.AddDays(11)},
            };
            IStatistics stats = new Statistics(userTasks);

            var actual = stats.GetFailurePercentage();
            var expected = 0;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetFailurePercentage_NothingCompleted_ReturnsCalculatedCompletionInPercentage()
        {
            var userTasks = new List<UserTasksModel>
            {
                new UserTasksModel(){IsFinished = false, DateDeadLine = DateTime.Today.AddDays(-1)},
                new UserTasksModel(){IsFinished = false, DateDeadLine = DateTime.Today.AddDays(-1)},
                new UserTasksModel(){IsFinished = false, DateDeadLine = DateTime.Today.AddDays(-1)},
            };
            IStatistics stats = new Statistics(userTasks);

            var actual = stats.GetFailurePercentage();
            var expected = 100.0m;

            Assert.Equal(expected, actual);
        }
    }
}
