using System;
using System.Collections.Generic;
using System.Text;
using TodoLibrary;
using TodoLibrary.Data.Categories;
using TodoUI.ViewModels;
using Xunit;

namespace ToDoTests.ViewModels
{
    public class AddCategoryTests
    {
        [Fact]
        public void CanCreateCategory_ValidLength_ReturnsTrue()
        {
            var vm = new AddCategoryViewModel(null);
            vm.CategoryName = "Education";

            var actual = vm.CanCreateCategory;

            Assert.True(actual);
        }

        [Fact]
        public void CanCreateCategory_InvalidLength_ReturnsFalse()
        {
            var vm = new AddCategoryViewModel(null);
            vm.CategoryName = "Ed";

            var actual = vm.CanCreateCategory;

            Assert.False(actual);
        }

        [Fact]
        public void CategoryName_ChangeValue_AssignsNewValue()
        {
            var vm = new AddCategoryViewModel(null);
            vm.CategoryName = "Test";

            var actual = vm.CategoryName;
            var expected = "Test";

            Assert.Equal(expected, actual);
        }

    }
}
