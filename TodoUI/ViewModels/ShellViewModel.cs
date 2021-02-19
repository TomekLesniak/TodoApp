using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using TodoLibrary.Data.Tasks;
using TodoLibrary.Data.Users;
using TodoLibrary.Data.UserTasks;
using TodoLibrary.Models;

namespace TodoUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<UserModel>
    {
        private readonly IUsersData _usersData;
        private readonly IUserTasksData _userTasksData;
        private readonly ITasksData _tasksData;
        private readonly EventAggregatorProvider _eventTracker;
        private BindableCollection<UserModel> _availableUsers;
        private UserModel _selectedUser;

        public ShellViewModel(IUsersData usersData, IUserTasksData userTasksData, ITasksData tasksData)
        {
            _eventTracker = EventAggregatorProvider.GetInstance();
            _eventTracker.TrackerEventAggregator.SubscribeOnUIThread(this);
         
            _usersData = usersData;
            _userTasksData = userTasksData;
            _tasksData = tasksData;
            AvailableUsers = new BindableCollection<UserModel>(_usersData.GetUsers());
        }


        public BindableCollection<UserModel> AvailableUsers
        {
            get => _availableUsers;
            set
            {
                _availableUsers = value;
                NotifyOfPropertyChange(() => AvailableUsers);
            }
        }

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

        public void AddUser()
        {
            ActivateItemAsync(new AddUserViewModel(), new CancellationToken());
            NotifyOfPropertyChange(() => CanAddUser);
        }
        
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

        public void RemoveUser()
        {
            _usersData.RemoveUser(SelectedUser);
            AvailableUsers.Remove(SelectedUser);
        }

        public bool CanRemoveUser
        {
            get
            {
                return SelectedUser != null;
            }
        }

        public Task HandleAsync(UserModel message, CancellationToken cancellationToken)
        {
            NotifyOfPropertyChange(() => CanAddUser);

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
                ActivateItemAsync(new UserTasksViewModel(SelectedUser, _userTasksData, _tasksData), new CancellationToken());
            }
        }
    }
}
