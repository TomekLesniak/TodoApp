using System;
using System.Collections.Generic;
using System.Text;
using TodoLibrary.Models;
using TodoUI.ViewModels;
using Xunit;

namespace ToDoTests.ViewModels
{
    public class AddTaskTests
    {
        [Fact]
        public void HeaderMessage_User_ContainsUserName()
        {
            var vm = new AddTaskViewModel(new UserModel() {FirstName = "Test"}, null, null, null);

            Assert.Contains("Test", vm.HeaderMessage);
        }

        [Fact]
        public void SelectedTask_NewTask_AssignNewTask()
        {
            var vm = new AddTaskViewModel(new UserModel() {FirstName = "Test"}, null, null, null);

            var expected = new TaskModel();
            vm.SelectedTask = expected;
            
            Assert.Equal(expected, vm.SelectedTask);
        }
        
        [Fact]
        public void Title_NewTitle_AssignNewTitle()
        {
            var vm = new AddTaskViewModel(new UserModel() { FirstName = "Test" }, null, null, null);

            var expected = "Expected";
            vm.Title = expected;

            Assert.Equal(expected, vm.Title);
        }

        [Fact]
        public void Deadline_NewDeadline_AssignNewDeadline()
        {
            var vm = new AddTaskViewModel(new UserModel() { FirstName = "Test" }, null, null, null);

            var expected = DateTime.Today;
            vm.Deadline = expected;

            Assert.Equal(expected, vm.Deadline);
        }

        [Fact]
        public void Description_NewDescription_AssignNewDescription()
        {
            var vm = new AddTaskViewModel(new UserModel() { FirstName = "Test" }, null, null, null);

            var expected = "Expected";
            vm.Description = expected;

            Assert.Equal(expected, vm.Description);
        }

        [Fact]
        public void Priority_NewPriority_AssignNewPriority()
        {
            var vm = new AddTaskViewModel(new UserModel() { FirstName = "Test" }, null, null, null);

            var expected = 3;
            vm.Priority = expected;

            Assert.Equal(expected, vm.Priority);
        }

        [Fact]
        public void SelectedCategory_NewCategory_AssignNewCategory()
        {
            var vm = new AddTaskViewModel(new UserModel() { FirstName = "Test" }, null, null, null);

            var expected = new CategoryModel();
            vm.SelectedCategory = expected;

            Assert.Equal(expected, vm.SelectedCategory);
        }

        [Fact]
        public void CanCreateTask_FormValid_ReturnsTrue()
        {
            var vm = new AddTaskViewModel(new UserModel() { FirstName = "Test" }, null, null, null);

            vm.Title = "Title";
            vm.Description = "Description";
            vm.Priority = 3;
            vm.Deadline = DateTime.Today;
            vm.SelectedCategory = new CategoryModel();

            var expected = vm.CanCreateTask;

            Assert.True(expected);
        }

        [Fact]
        public void CanCreateTask_FormInValid_ReturnsFalse()
        {
            var vm = new AddTaskViewModel(new UserModel() { FirstName = "Test" }, null, null, null);

            vm.Title = "Title";
            vm.Description = "";
            vm.Priority = 3;
            vm.Deadline = DateTime.Today;
            vm.SelectedCategory = new CategoryModel();

            var expected = vm.CanCreateTask;

            Assert.False(expected);
        }

    }
}
