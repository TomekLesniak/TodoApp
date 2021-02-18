using System;
using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;
using TodoLibrary.Models;

namespace TodoUI.ViewModels
{
    public class AddUserViewModel
    {
        private readonly EventAggregatorProvider _eventTracker;

        public AddUserViewModel(EventAggregatorProvider eventTracker)
        {
            _eventTracker = eventTracker;
        }

        public void CreateUser()
        {
            _eventTracker.TrackerEventAggregator.PublishOnUIThreadAsync(new UserModel(){FirstName = "Published"});
        }

        public bool CanCreateUser
        {
            get
            {
                return true;
            }
        }
    }
}
