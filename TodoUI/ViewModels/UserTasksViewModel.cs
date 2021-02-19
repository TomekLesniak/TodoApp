using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Caliburn.Micro;
using TodoLibrary.Data.UserTasks;
using TodoLibrary.Models;

namespace TodoUI.ViewModels
{
    public class UserTasksViewModel : Conductor<object>
    {
        private BindableCollection<UserTasksModel> _userTasks;
        private readonly UserModel _user;
        private readonly IUserTasksData _userTasksData;
        private bool _unfinishedOnly;
        private UserTasksModel _selectedUserTask;

        public UserTasksViewModel(UserModel user, IUserTasksData userTasksData)
        {
            _user = user;
            _userTasksData = userTasksData;

            UserTasks = new BindableCollection<UserTasksModel>(_userTasksData.GetUserTasks(user.Id));
        }

        public string UserHeader => $"{_user.FirstName} tasks";

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

        public string ProgressStatus => "W trakcie";

        public void LoadStats()
        {
            //todo: Load another view
        }

        public void CreateUserTask()
        {

        }

        public void CompleteUserTask()
        {

        }

        public bool CanCompleteUserTask()
        {
            return true;
        }
    }
}
