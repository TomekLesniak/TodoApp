using System;
using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;
using TodoLibrary.Data.Categories;
using TodoLibrary.Models;

namespace TodoUI.ViewModels
{
    public class AddCategoryViewModel : Screen
    {
        private readonly ICategoriesData _categoriesData;
        private readonly EventAggregatorProvider _eventTracker;
        private string _categoryName = "";

        public AddCategoryViewModel(ICategoriesData categoriesData)
        {
            _categoriesData = categoriesData;
            _eventTracker = EventAggregatorProvider.GetInstance();
        }

        public string CategoryName
        {
            get => _categoryName;
            set
            {
                _categoryName = value;
                NotifyOfPropertyChange(() => CategoryName);
                NotifyOfPropertyChange(() => CanCreateCategory);
            }
        }

        public void CreateCategory()
        {
            var newCategory = new CategoryModel()
            {
                Title = CategoryName
            };
            
            _categoriesData.CreateCategory(newCategory);

            _eventTracker.TrackerEventAggregator.PublishOnUIThreadAsync(newCategory);
            this.TryCloseAsync();
        }
        
        public bool CanCreateCategory
        {
            get
            {
                return CategoryName.Length >= 3;
            }
        }
        
        
        public void CancelCreation()
        {
            _eventTracker.TrackerEventAggregator.PublishOnUIThreadAsync(new CategoryModel());
            this.TryCloseAsync();
        }
    }
}
