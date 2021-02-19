using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Caliburn.Micro;
using TodoLibrary.Data.Categories;
using TodoLibrary.Data.Tasks;
using TodoLibrary.Models;

namespace TodoUI.ViewModels
{
    public class AddTaskViewModel : Conductor<object>, IHandle<CategoryModel>
    {
        private readonly UserModel _user;
        private readonly ITasksData _tasksData;
        private readonly ICategoriesData _categoriesData;
        private readonly EventAggregatorProvider _eventTracker;
        private BindableCollection<TaskModel> _availableTasks;
        private TaskModel _selectedTask;
        private string _title = "";
        private DateTime _deadline = DateTime.Today.AddDays(1);
        private string _description = "";
        private int _priority = 1;
        private BindableCollection<CategoryModel> _availableCategories;
        private CategoryModel _selectedCategory;

        public AddTaskViewModel(UserModel user, ITasksData tasksData, ICategoriesData categoriesData)
        {
            _user = user;
            _tasksData = tasksData;
            _categoriesData = categoriesData;
            _eventTracker = EventAggregatorProvider.GetInstance();
            _eventTracker.TrackerEventAggregator.SubscribeOnUIThread(this);

            AvailableTasks = new BindableCollection<TaskModel>(_tasksData.GetTasks());
            AvailableCategories = new BindableCollection<CategoryModel>(_categoriesData.GetCategories());
        }

        public string HeaderMessage => $"Creating task for {_user.FirstName}";

        public BindableCollection<TaskModel> AvailableTasks
        {
            get => _availableTasks;
            set
            {
                _availableTasks = value;
                NotifyOfPropertyChange(() => AvailableTasks);
            }
        }

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

        public BindableCollection<CategoryModel> AvailableCategories
        {
            get => _availableCategories;
            set
            {
                _availableCategories = value;
                NotifyOfPropertyChange(() => AvailableCategories);
            }
        }

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

        public void CreateTask()
        {
            if (SelectedTask == null)
            {
                SaveNewTaskToDatabase();
            }

            var newUserTask = new UserTasksModel()
            {
                User = _user,
                DateStarted = DateTime.Today,
                DateDeadLine = Deadline,
                Priority = Priority,
                IsFinished = false,
                Task = SelectedTask,
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
        
        public void CreateCategory()
        {
            ActivateItemAsync(new AddCategoryViewModel(_categoriesData), new CancellationToken());
        }

        
        public void CancelCreation()
        {
            _eventTracker.TrackerEventAggregator.PublishOnUIThreadAsync(new UserTasksModel());
            this.TryCloseAsync();
        }

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
