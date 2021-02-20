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
using TodoLibrary.Data.Users;
using TodoLibrary.Data.UserTasks;
using TodoLibrary.Models;

namespace TodoUI.ViewModels
{
    public class UserTasksViewModel : Conductor<object>, IHandle<UserTasksModel>
    {
        private BindableCollection<UserTasksModel> _userTasks;
        private readonly EventAggregatorProvider _eventTracker;
        private readonly UserModel _user;
        private readonly IUsersData _usersData;
        private readonly IUserTasksData _userTasksData;
        private readonly ITasksData _tasksData;
        private readonly ICategoriesData _categoriesData;
        private bool _unfinishedOnly;
        private UserTasksModel _selectedUserTask;
        private bool _userTasksIsVisible = true;

        public UserTasksViewModel(UserModel user, IUsersData usersData, IUserTasksData userTasksData, ITasksData tasksData, ICategoriesData categoriesData)
        {
            _user = user;
            _usersData = usersData;
            _userTasksData = userTasksData;
            _tasksData = tasksData;
            _categoriesData = categoriesData;

            _eventTracker = EventAggregatorProvider.GetInstance();
            UserTasks = new BindableCollection<UserTasksModel>(_userTasksData.GetUserTasks(user.Id));
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
                NotifyOfPropertyChange(() => CanCompleteUserTask);
                NotifyOfPropertyChange(() => SelectedUserTask);
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
            ActivateItemAsync(new AddTaskViewModel(_user, _tasksData, _categoriesData, _usersData), new CancellationToken());
            NotifyOfPropertyChange(() => CanCreateUserTask);
            SelectedUserTask = null;
        }

        public bool CanCreateUserTask
        {
            get
            {
                return ActiveItem == null;
            }
        }

        public void CompleteUserTask()
        {
            //todo: Mark task as completed
        }

        public bool CanCompleteUserTask
        {
            get
            {
                return SelectedUserTask != null && SelectedUserTask.IsFinished == false;
            }
        }
        
        public Task HandleAsync(UserTasksModel message, CancellationToken cancellationToken)
        {
            UserTasksIsVisible = true;
            NotifyOfPropertyChange(() => CanCreateUserTask);

            if (message.Task == null)
            {
                return Task.CompletedTask;
            }

            _userTasksData.CreateUserTask(message);
            UserTasks.Add(message);
            
            return Task.CompletedTask;
        }
        
    }
}
