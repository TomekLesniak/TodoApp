using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        private BindableCollection<UserModel> _availableUsers;
        private UserModel _selectedUser;

        public ShellViewModel(IUsersData usersData)
        {
            _usersData = usersData;

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
            }
        }

        public void AddUser()
        {
            MessageBox.Show($"{SelectedUser.FullName}");
        }

        public void RemoveUser()
        {

        }

        public bool CanRemoveUser
        {
            get
            {
                return SelectedUser != null;
            }
        }
    }
}
