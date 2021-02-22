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
        private bool _unfinishedOnly = true;
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
            LoadUserTasks();
        }

        private void LoadUserTasks()
        {
            List<UserTasksModel> selectedUserTasks;
            
            if (UnfinishedOnly)
            {
                selectedUserTasks = _userTasksData.GetUserTasks(_user.Id).Where(x => x.IsFinished != UnfinishedOnly)
                    .ToList();
            }
            else
            {
                selectedUserTasks = _userTasksData.GetUserTasks(_user.Id).ToList();
            }
            
            UserTasks = new BindableCollection<UserTasksModel>(selectedUserTasks); // true
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
                NotifyOfPropertyChange(() => UserTasks);
            }
        }

        public bool UnfinishedOnly
        {
            get => _unfinishedOnly;
            set
            {
                _unfinishedOnly = value;
                NotifyOfPropertyChange(() => UnfinishedOnly);
                LoadUserTasks();
            }
        }

        public void LoadStats()
        {
            var allUserTasks = _userTasksData.GetUserTasks(_user.Id);

            if (ActiveItem != null && ActiveItem.GetType() == typeof(StatisticsViewModel))
            {
                DeactivateItemAsync(ActiveItem, true, new CancellationToken());
            }
            else
            {
                ActivateItemAsync(new StatisticsViewModel(allUserTasks), new CancellationToken());
            }

            UserTasksIsVisible = !UserTasksIsVisible;


            if (UserTasksIsVisible && ActiveItem != null)
            {
                MessageBox.Show("Bug appeared");
                UserTasksIsVisible = false;
            }

            NotifyOfPropertyChange(() => CanCreateUserTask);
            SelectedUserTask = null;
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
            SelectedUserTask.IsFinished = true;
            _userTasksData.CompleteUserTask(SelectedUserTask.Id);
            
            UserTasks.Refresh();
            
            if (UnfinishedOnly)
            {
                UserTasks.Remove(SelectedUserTask);
            }
            
            NotifyOfPropertyChange(() => CanCompleteUserTask);
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
