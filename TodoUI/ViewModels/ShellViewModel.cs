using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using TodoLibrary.Data.Categories;
using TodoLibrary.Data.Tasks;
using TodoLibrary.Data.Users;
using TodoLibrary.Data.UserTasks;
using TodoLibrary.Models;

namespace TodoUI.ViewModels
{
    /// <summary>
    /// View model for corresponding ShellView. Root of all views.
    /// </summary>
    public class ShellViewModel : Conductor<object>, IHandle<UserModel>
    {
        private readonly IUsersData _usersData;
        private readonly IUserTasksData _userTasksData;
        private readonly ITasksData _tasksData;
        private readonly ICategoriesData _categoriesData;
        private readonly EventAggregatorProvider _eventTracker;
        private BindableCollection<UserModel> _availableUsers;
        private UserModel _selectedUser;

        /// <summary>
        /// Initializes required information to work with screen & data.
        /// </summary>
        /// <param name="usersData">Implementation of IUsersData</param>
        /// <param name="userTasksData">Implementation of IUsersTasksData</param>
        /// <param name="tasksData">Implementation of ITasksData</param>
        /// <param name="categoriesData">Implementation of ICategoriesData</param>
        public ShellViewModel(IUsersData usersData, IUserTasksData userTasksData, ITasksData tasksData, ICategoriesData categoriesData)
        {
            _eventTracker = EventAggregatorProvider.GetInstance();
         
            _usersData = usersData;
            _userTasksData = userTasksData;
            _tasksData = tasksData;
            _categoriesData = categoriesData;
            AvailableUsers = new BindableCollection<UserModel>(_usersData?.GetUsers() ?? new List<UserModel>());
        }

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            _eventTracker.TrackerEventAggregator.SubscribeOnUIThread(this);
            return base.OnActivateAsync(cancellationToken);
        }

        protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
        {
            _eventTracker.TrackerEventAggregator.Unsubscribe(this);
            return base.OnDeactivateAsync(close, cancellationToken);
        }

        /// <summary>
        /// List of all available users
        /// </summary>
        public BindableCollection<UserModel> AvailableUsers
        {
            get => _availableUsers;
            set
            {
                _availableUsers = value;
                NotifyOfPropertyChange(() => AvailableUsers);
            }
        }

        /// <summary>
        /// Currently selected user
        /// </summary>
        public UserModel SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                NotifyOfPropertyChange(() => SelectedUser);
                NotifyOfPropertyChange(() => CanRemoveUser);
                LoadUserTasks();
            }
        }

        /// <summary>
        /// Opens the view for new user creation
        /// </summary>
        public void AddUser()
        {
            ActivateItemAsync(new AddUserViewModel(), new CancellationToken());
            NotifyOfPropertyChange(() => CanAddUser);
        }
        
        /// <summary>
        /// Checks if user creation is already taking place.
        /// </summary>
        public bool CanAddUser
        {
            get
            {
                if (ActiveItem != null)
                {
                    return (ActiveItem.GetType() != typeof(AddUserViewModel));
                }

                return true;
            }
        }

        /// <summary>
        /// Removes the user and all corresponding data.
        /// </summary>
        public void RemoveUser()
        {
            _usersData.RemoveUser(SelectedUser);
            AvailableUsers.Remove(SelectedUser);

            if (ActiveItem != null)
            {
                DeactivateItemAsync(ActiveItem, true, new CancellationToken());
            }
        }

        /// <summary>
        /// Checks if there is any user selected.
        /// </summary>
        public bool CanRemoveUser
        {
            get
            {
                return SelectedUser != null;
            }
        }

        /// <summary>
        /// Handle passed user model from CreateUserView.
        /// Adds new user to list.
        /// </summary>
        /// <param name="message">User information</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public Task HandleAsync(UserModel message, CancellationToken cancellationToken)
        {
            NotifyOfPropertyChange(() => CanAddUser);
            LoadUserTasks();

            if (string.IsNullOrWhiteSpace(message.FirstName))
            {
                return Task.CompletedTask;
            }
            
            _usersData.CreateUser(message);
            AvailableUsers.Add(message);
            
            return Task.CompletedTask;
        }

        private void LoadUserTasks()
        {
            if (SelectedUser != null)
            {
                if (ActiveItem != null)
                {
                    DeactivateItemAsync(ActiveItem, true, new CancellationToken());
                }
                
                ActivateItemAsync(new UserTasksViewModel(SelectedUser, _usersData, _userTasksData, _tasksData, _categoriesData), new CancellationToken());
            }
        }
        
    }
}
