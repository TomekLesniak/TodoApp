using System;
using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;
using TodoLibrary.Models;

namespace TodoUI.ViewModels
{
    public class AddTaskViewModel : Screen
    {
        private readonly EventAggregatorProvider _eventTracker;
        
        public AddTaskViewModel()
        {
            _eventTracker = EventAggregatorProvider.GetInstance();
        }

        public void CreateTask()
        {
            _eventTracker.TrackerEventAggregator.PublishOnUIThreadAsync(new UserTasksModel());
            this.TryCloseAsync();
        }
    }
}
