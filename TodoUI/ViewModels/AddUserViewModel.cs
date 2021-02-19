using System;
using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;
using TodoLibrary.Models;

namespace TodoUI.ViewModels
{
    public class AddUserViewModel : Screen
    {
        private readonly EventAggregatorProvider _eventTracker;
        private string _lastName;
        private string _firstName;

        public AddUserViewModel(EventAggregatorProvider eventTracker)
        {
            _eventTracker = eventTracker;
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                NotifyOfPropertyChange(() => FirstName);
                NotifyOfPropertyChange(() => CanCreateUser);
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                NotifyOfPropertyChange(() => LastName);
                NotifyOfPropertyChange(() => CanCreateUser);
            }
        }

        public void CreateUser()
        {
            var user = new UserModel()
            {
                FirstName = FirstName,
                LastName = LastName
            };

            _eventTracker.TrackerEventAggregator.PublishOnUIThreadAsync(user);
            this.TryCloseAsync();
        }

        public bool CanCreateUser
        {
            get => !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName);
        }

        public void CancelCreation()
        {
            _eventTracker.TrackerEventAggregator.PublishOnUIThreadAsync(new UserModel());
            this.TryCloseAsync();
        }
    }
}
