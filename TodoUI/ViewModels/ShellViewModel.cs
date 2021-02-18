﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using TodoLibrary.Data.Tasks;
using TodoLibrary.Data.Users;
using TodoLibrary.Data.UserTasks;
using TodoLibrary.Models;

namespace TodoUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<UserModel>
    {
        private readonly IUsersData _usersData;
        private readonly EventAggregatorProvider _eventTracker;
        private BindableCollection<UserModel> _availableUsers;
        private UserModel _selectedUser;

        public ShellViewModel(IUsersData usersData, EventAggregatorProvider eventTracker)
        {
            _eventTracker = eventTracker;
            _eventTracker.TrackerEventAggregator.SubscribeOnUIThread(this);
         
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
            ActivateItemAsync(new AddUserViewModel(_eventTracker), new CancellationToken());
            NotifyOfPropertyChange(() => CanAddUser);
        }
        
        public bool CanAddUser
        {
            get
            {
                if (ActiveItem != null)
                {
                    return (ActiveItem.GetType() != typeof(AddUserViewModel));
                }

                return true;
            }
        }

        public void RemoveUser()
        {
            _usersData.RemoveUser(SelectedUser);
            AvailableUsers.Remove(SelectedUser);
        }

        public bool CanRemoveUser
        {
            get
            {
                return SelectedUser != null;
            }
        }

        public Task HandleAsync(UserModel message, CancellationToken cancellationToken)
        {
            NotifyOfPropertyChange(() => CanAddUser);

            if (string.IsNullOrWhiteSpace(message.FirstName))
            {
                return Task.CompletedTask;
            }
            
            _usersData.CreateUser(message);
            AvailableUsers.Add(message);
            
            return Task.CompletedTask;
        }
    }
}
