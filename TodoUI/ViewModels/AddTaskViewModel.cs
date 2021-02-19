using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Navigation;
using Caliburn.Micro;
using TodoLibrary.Data.Tasks;
using TodoLibrary.Models;

namespace TodoUI.ViewModels
{
    public class AddTaskViewModel : Screen
    {
        private readonly UserModel _user;
        private readonly ITasksData _tasksData;
        private readonly EventAggregatorProvider _eventTracker;
        private BindableCollection<TaskModel> _availableTasks;
        private TaskModel _selectedTask;
        private string _title;
        private DateTime _deadline = DateTime.Today;
        private string _description;
        private int _priority;
        private BindableCollection<CategoryModel> _availableCategories;
        private CategoryModel _selectedCategory;

        public AddTaskViewModel(UserModel user, ITasksData tasksData)
        {
            _user = user;
            _tasksData = tasksData;
            _eventTracker = EventAggregatorProvider.GetInstance();

            AvailableTasks = new BindableCollection<TaskModel>(_tasksData.GetTasks());
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
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }

        public DateTime Deadline
        {
            get => _deadline;
            set
            {
                _deadline = value;
                NotifyOfPropertyChange(() => Deadline);
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value; 
                NotifyOfPropertyChange(() => Description);
            }
        }

        public int Priority
        {
            get => _priority;
            set
            {
                _priority = value;
                NotifyOfPropertyChange(() => Priority);
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
            }
        }

        public void CreateTask()
        {
            _eventTracker.TrackerEventAggregator.PublishOnUIThreadAsync(new UserTasksModel());
            this.TryCloseAsync();
        }

        public bool CanCreateTask 
        {
            get
            {
                return true;
            }
        }
        
        public void CreateCategory()
        {
            //todo: activateitem for category creation
        }

        
        public void CancelCreation()
        {
            //todo: close form 
        }
    }
}
