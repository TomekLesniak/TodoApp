using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Caliburn.Micro;
using TodoLibrary.Data.Categories;
using TodoLibrary.Data.Tasks;
using TodoLibrary.Data.Users;
using TodoLibrary.Models;

namespace TodoUI.ViewModels
{
    /// <summary>
    /// View model for corresponding AddTaskView
    /// </summary>
    public class AddTaskViewModel : Conductor<object>, IHandle<CategoryModel>
    {
        private readonly UserModel _user;
        private readonly ITasksData _tasksData;
        private readonly ICategoriesData _categoriesData;
        private readonly IUsersData _usersData;
        private readonly EventAggregatorProvider _eventTracker;
        private BindableCollection<TaskModel> _availableTasks;
        private TaskModel _selectedTask;
        private string _title = "";
        private DateTime _deadline = DateTime.Today.AddDays(1);
        private string _description = "";
        private int _priority = 1;
        private BindableCollection<CategoryModel> _availableCategories;
        private CategoryModel _selectedCategory;

        /// <summary>
        /// Initializes required information to work with screen & tasks.
        /// </summary>
        /// <param name="user">User for whom the task is being created for</param>
        /// <param name="tasksData">Implementation of ITasksData</param>
        /// <param name="categoriesData">Implementation of ICategoriesData</param>
        /// <param name="usersData">Implementation of IUsersData</param>
        public AddTaskViewModel(UserModel user, ITasksData tasksData, ICategoriesData categoriesData, IUsersData usersData)
        {
            _user = user;
            _tasksData = tasksData;
            _categoriesData = categoriesData;
            _usersData = usersData;
            _eventTracker = EventAggregatorProvider.GetInstance();

            AvailableTasks = new BindableCollection<TaskModel>(_tasksData.GetTasks());
            AvailableCategories = new BindableCollection<CategoryModel>(_categoriesData.GetCategories());
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
        /// Display header message.
        /// </summary>
        public string HeaderMessage => $"Creating task for {_user.FirstName}";

        /// <summary>
        /// List of available tasks that has been created
        /// </summary>
        public BindableCollection<TaskModel> AvailableTasks
        {
            get => _availableTasks;
            set
            {
                _availableTasks = value;
                NotifyOfPropertyChange(() => AvailableTasks);
            }
        }

        /// <summary>
        /// Currently selected task
        /// </summary>
        public TaskModel SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                NotifyOfPropertyChange(() => SelectedTask);
                PopulateFields();
            }
        }

        private void PopulateFields()
        {
            if(SelectedTask != null)
            {
                Title = SelectedTask.Title;
                Description = SelectedTask.Description;
                SelectedCategory = SelectedTask.CategoryModel;
            }
        }

        /// <summary>
        /// New task title field.
        /// </summary>
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NotifyOfPropertyChange(() => Title);
                NotifyOfPropertyChange(() => CanCreateTask);
            }
        }

        /// <summary>
        /// New task deadline date
        /// </summary>
        public DateTime Deadline
        {
            get => _deadline;
            set
            {
                _deadline = value;
                NotifyOfPropertyChange(() => Deadline);
                NotifyOfPropertyChange(() => CanCreateTask);
            }
        }

        /// <summary>
        /// New task description
        /// </summary>
        public string Description
        {
            get => _description;
            set
            {
                _description = value; 
                NotifyOfPropertyChange(() => Description);
                NotifyOfPropertyChange(() => CanCreateTask);
            }
        }

        /// <summary>
        /// New task priority
        /// </summary>
        public int Priority
        {
            get => _priority;
            set
            {
                _priority = value;
                NotifyOfPropertyChange(() => Priority);
                NotifyOfPropertyChange(() => CanCreateTask);
            }
        }

        /// <summary>
        /// List of all available categories created.
        /// </summary>
        public BindableCollection<CategoryModel> AvailableCategories
        {
            get => _availableCategories;
            set
            {
                _availableCategories = value;
                NotifyOfPropertyChange(() => AvailableCategories);
            }
        }

        /// <summary>
        /// Currently selected category for new task
        /// </summary>
        public CategoryModel SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                NotifyOfPropertyChange(() => SelectedCategory);
                NotifyOfPropertyChange(() => AvailableCategories);
                NotifyOfPropertyChange(() => CanCreateTask);
            }
        }

        /// <summary>
        /// When pressed, creates new task from populated form and saves to database.
        /// </summary>
        public void CreateTask()
        {
            if (SelectedTask == null)
            {
                SaveNewTaskToDatabase();
            }

            var newUserTask = new UserTasksModel()
            {
                DateStarted = DateTime.Today,
                DateDeadLine = Deadline,
                Priority = Priority,
                IsFinished = false,
                Task = SelectedTask,
                User = _user
            };

            
            _eventTracker.TrackerEventAggregator.PublishOnUIThreadAsync(newUserTask);
            this.TryCloseAsync();
        }

        private void SaveNewTaskToDatabase()
        {
            var newTask = new TaskModel
            {
                Title = Title,
                CategoryModel = SelectedCategory,
                Description = Description
            };

            _tasksData.CreateTask(newTask);
            AvailableTasks.Add(newTask);
            SelectedTask = newTask;
        }

        /// <summary>
        /// Check if all fields are valid.
        /// </summary>
        public bool CanCreateTask 
        {
            get
            {
                if(Title.Length > 0 && Description.Length > 0 && Priority > 0 
                   && Deadline >= DateTime.Today && SelectedCategory != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        
        /// <summary>
        /// Opens the view for new category creation.
        /// </summary>
        public void CreateCategory()
        {
            ActivateItemAsync(new AddCategoryViewModel(_categoriesData), new CancellationToken());
        }

        /// <summary>
        /// Closing this view.
        /// </summary>
        public void CancelCreation()
        {
            _eventTracker.TrackerEventAggregator.PublishOnUIThreadAsync(new UserTasksModel());
            this.TryCloseAsync();
        }

        /// <summary>
        /// Handle passed category model from CreateCategoryView.
        /// Sets SelectedCategory to new category passed in.
        /// </summary>
        /// <param name="message">Category information</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public Task HandleAsync(CategoryModel message, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(message.Title))
            {
                return Task.CompletedTask;
            }
            
            AvailableCategories.Add(message);
            SelectedCategory = message;

            return Task.CompletedTask;
        }
    }
}
