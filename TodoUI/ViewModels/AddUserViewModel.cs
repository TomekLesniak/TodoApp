using System;
using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;
using TodoLibrary.Models;

namespace TodoUI.ViewModels
{
    /// <summary>
    /// View model for corresponding AddUserView
    /// </summary>
    public class AddUserViewModel : Screen
    {
        private readonly EventAggregatorProvider _eventTracker;
        private string _lastName;
        private string _firstName;

        /// <summary>
        /// Initialize required information to work with screen.
        /// </summary>
        public AddUserViewModel()
        {
            _eventTracker = EventAggregatorProvider.GetInstance();
        }

        /// <summary>
        /// New user first name
        /// </summary>
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

        /// <summary>
        /// New user lastname.
        /// </summary>
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

        /// <summary>
        /// When pressed, creates new user from populated form and saves to database.
        /// </summary>
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

        /// <summary>
        /// Check if all fields are valid
        /// </summary>
        public bool CanCreateUser
        {
            get => !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName);
        }

        /// <summary>
        /// Closing this view.
        /// </summary>
        public void CancelCreation()
        {
            _eventTracker.TrackerEventAggregator.PublishOnUIThreadAsync(new UserModel());
            this.TryCloseAsync();
        }
    }
}
