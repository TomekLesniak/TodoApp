using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using TodoLibrary.Data.Tasks;
using TodoLibrary.Data.Users;
using TodoLibrary.Data.UserTasks;
using TodoLibrary.Models;

namespace TodoUI.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private readonly IUsersData _usersData;
        private readonly ITasksData _tasksData;
        private readonly IUserTasksData _userTasks;
        private BindableCollection<UserModel> _availableUsers;

        public ShellViewModel(IUsersData usersData, ITasksData tasksData, IUserTasksData userTasks)
        {
            _usersData = usersData;
            _tasksData = tasksData;
            _userTasks = userTasks;

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

        public void CreateUserTest()
        {
            _userTasks.GetUserTasks(1);
        }
    }
}
