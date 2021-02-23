using System;
using System.Collections.Generic;
using System.Text;
using TodoLibrary.Data.Categories;
using TodoLibrary.Data.Tasks;
using TodoLibrary.Models;
using TodoUI.ViewModels;
using Xunit;

namespace ToDoTests.ViewModels
{
    public class ShellTests
    {
        [Fact]
        public void CanAddUser_NoActiveItem_ReturnsTrue()
        {
            var vm = new ShellViewModel(null, null, null, null);
            
            Assert.True(vm.CanAddUser);
        }

        [Fact]
        public void CanAddUser_ActiveItem_ReturnsFalse()
        {
            var vm = new ShellViewModel(null, null, null, null);

            vm.ActiveItem = new AddUserViewModel();
            
            Assert.False(vm.CanAddUser);
        }

        [Fact]
        public void CanRemoveUser_NoUserSelected_ReturnsFalse()
        {
            var vm = new ShellViewModel(null, null, null, null);

            Assert.False(vm.CanRemoveUser);
        }

    }
}
