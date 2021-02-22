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
    /// <summary>
    /// View model for corresponding UserTasksView
    /// </summary>
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

        /// <summary>
        /// Initializes required information to work with screen & user tasks.
        /// </summary>
        /// <param name="user">User for whom the tasks are displayed for</param>
        /// <param name="usersData">IUsersData implementation</param>
        /// <param name="userTasksData">IUserTasksData implementation</param>
        /// <param name="tasksData">ITasksData implementation</param>
        /// <param name="categoriesData">ICategoriesData implementation</param>
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

        /// <summary>
        /// Display user header message
        /// </summary>
        public string UserHeader => $"{_user.FirstName} tasks";

        /// <summary>
        /// Toggles user task visibility.
        /// </summary>
        public bool UserTasksIsVisible
        {
            get => _userTasksIsVisible;
            set
            {
                _userTasksIsVisible = value;
                NotifyOfPropertyChange(() => UserTasksIsVisible);
            }
        }

        /// <summary>
        /// List of all available tasks for currently filter search.
        /// </summary>
        public BindableCollection<UserTasksModel> UserTasks
        {
            get => _userTasks;
            set
            {
                _userTasks = value;
                NotifyOfPropertyChange(() => UserTasks);
            }
        }

        /// <summary>
        /// Currently selected task
        /// </summary>
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

        /// <summary>
        /// Filter option. 
        /// </summary>
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

        /// <summary>
        /// Open the view with statistics for this user.
        /// </summary>
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
                UserTasksIsVisible = false;
            }

            NotifyOfPropertyChange(() => CanCreateUserTask);
            NotifyOfPropertyChange(() => CanCompleteUserTask);

        }
        
        /// <summary>
        /// Open the view for new task creation
        /// </summary>
        public void CreateUserTask()
        {
            UserTasksIsVisible = !UserTasksIsVisible;
            ActivateItemAsync(new AddTaskViewModel(_user, _tasksData, _categoriesData, _usersData), new CancellationToken());
            NotifyOfPropertyChange(() => CanCreateUserTask);
            NotifyOfPropertyChange(() => CanCompleteUserTask);
        }

        /// <summary>
        /// Check if no child view is currently open.
        /// </summary>
        public bool CanCreateUserTask
        {
            get
            {
                return ActiveItem == null;
            }
        }

        /// <summary>
        /// Marks selected task as completed.
        /// </summary>
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

        /// <summary>
        /// Checks if selected task can be completed
        /// </summary>
        public bool CanCompleteUserTask
        {
            get
            {
                return SelectedUserTask != null && SelectedUserTask.IsFinished == false 
                                                && ActiveItem == null;
            }
        }
        
        /// <summary>
        /// Add newly created task to user tasks.
        /// </summary>
        /// <param name="message">New UserTaskModel</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public Task HandleAsync(UserTasksModel message, CancellationToken cancellationToken)
        {
            UserTasksIsVisible = true;
            
            NotifyOfPropertyChange(() => CanCreateUserTask);
            NotifyOfPropertyChange(() => CanCompleteUserTask);


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
