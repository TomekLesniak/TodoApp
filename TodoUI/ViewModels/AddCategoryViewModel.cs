using System;
using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;
using TodoLibrary.Data.Categories;
using TodoLibrary.Models;

namespace TodoUI.ViewModels
{
    /// <summary>
    /// View model for corresponding AddCategoryView
    /// </summary>
    public class AddCategoryViewModel : Screen
    {
        private readonly ICategoriesData _categoriesData;
        private readonly EventAggregatorProvider _eventTracker;
        private string _categoryName = "";

        /// <summary>
        /// Initializes required information to work with screen & categories.
        /// </summary>
        /// <param name="categoriesData">CategoriesData implementation</param>
        public AddCategoryViewModel(ICategoriesData categoriesData)
        {
            _categoriesData = categoriesData;
            _eventTracker = EventAggregatorProvider.GetInstance();
        }

        /// <summary>
        /// New category name
        /// </summary>
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

        /// <summary>
        /// When pressed, creates category from populated form and saves to database.
        /// </summary>
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
        
        
        /// <summary>
        /// Check if category length is valid.
        /// </summary>
        public bool CanCreateCategory
        {
            get
            {
                return CategoryName.Length >= 3;
            }
        }
        
        /// <summary>
        /// Closing this view.
        /// </summary>
        public void CancelCreation()
        {
            _eventTracker.TrackerEventAggregator.PublishOnUIThreadAsync(new CategoryModel());
            this.TryCloseAsync();
        }
    }
}
