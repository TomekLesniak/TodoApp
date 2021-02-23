using System;
using System.Collections.Generic;
using System.Text;
using TodoUI.ViewModels;
using Xunit;

namespace ToDoTests.ViewModels
{
    public class AddUserTests
    {
        [Fact]
        public void FirstName_NewFirstname_AssignNewFirstname()
        {
            var vm = new AddUserViewModel();
            var expected = "Firstname";

            vm.FirstName = expected;

            Assert.Equal(expected, vm.FirstName);
        }


        [Fact]
        public void Lastname_NewLastname_AssignNewLastname()
        {
            var vm = new AddUserViewModel();
            var expected = "Lastname";

            vm.LastName = expected;
            
            Assert.Equal(expected, vm.LastName);
        }

        [Fact]
        public void CanCreateUser_ValidForm_ReturnsTrue()
        {
            var vm = new AddUserViewModel();
            vm.FirstName = "Fn";
            vm.LastName = "Ln";
            
            Assert.True(vm.CanCreateUser);
        }

        [Fact]
        public void CanCreateUser_InvalidForm_ReturnsFalse()
        {
            var vm = new AddUserViewModel();
            vm.FirstName = "test";
            
            Assert.False(vm.CanCreateUser);
        }
    }
}
