using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using TodoLibrary.Data.Categories;
using TodoLibrary.Data.Tasks;
using TodoLibrary.Data.UserTasks;
using TodoLibrary.Models;

namespace TodoUI.ViewModels
{
    public class UserTasksViewModel : Conductor<object>, IHandle<UserTasksModel>
    {
        private BindableCollection<UserTasksModel> _userTasks;
        private readonly EventAggregatorProvider _eventTracker;
        private readonly UserModel _user;
        private readonly IUserTasksData _userTasksData;
        private readonly ITasksData _tasksData;
        private readonly ICategoriesData _categoriesData;
        private bool _unfinishedOnly;
        private UserTasksModel _selectedUserTask;
        private bool _userTasksIsVisible = true;

        public UserTasksViewModel(UserModel user, IUserTasksData userTasksData, ITasksData tasksData, ICategoriesData categoriesData)
        {
            _user = user;
            _userTasksData = userTasksData;
            _tasksData = tasksData;
            _categoriesData = categoriesData;

            _eventTracker = EventAggregatorProvider.GetInstance();
            _eventTracker.TrackerEventAggregator.SubscribeOnUIThread(this);
            UserTasks = new BindableCollection<UserTasksModel>(_userTasksData.GetUserTasks(user.Id));
        }

        public string UserHeader => $"{_user.FirstName} tasks";

        public bool UserTasksIsVisible
        {
            get => _userTasksIsVisible;
            set
            {
                _userTasksIsVisible = value;
                NotifyOfPropertyChange(() => UserTasksIsVisible);
            }
        }

        public BindableCollection<UserTasksModel> UserTasks
        {
            get => _userTasks;
            set
            {
                _userTasks = value;
                NotifyOfPropertyChange(() => UserTasks);
            }
        }

        public UserTasksModel SelectedUserTask
        {
            get => _selectedUserTask;
            set
            {
                _selectedUserTask = value;
            }
        }

        public bool UnfinishedOnly
        {
            get => _unfinishedOnly;
            set
            {
                _unfinishedOnly = value;
                NotifyOfPropertyChange(() => UnfinishedOnly);
            }
        }

        public void LoadStats()
        {
            //todo: Load another view
        }

        public void CreateUserTask()
        {
            UserTasksIsVisible = !UserTasksIsVisible;
            ActivateItemAsync(new AddTaskViewModel(_user, _tasksData, _categoriesData), new CancellationToken());
        }

        public void CompleteUserTask()
        {
            //todo: Mark task as completed
        }

        public bool CanCompleteUserTask()
        {
            //todo: Check if task is completed
            return true;
        }

        public Task HandleAsync(UserTasksModel message, CancellationToken cancellationToken)
        {
            UserTasksIsVisible = true;

            if (message.Task == null)
            {
                return Task.CompletedTask;
            }

            UserTasks.Add(message);
            
            return Task.CompletedTask;
        }
    }
}
