using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using System.ComponentModel.DataAnnotations;

namespace UnitOfWorkSample.Models
{
    /// <summary>
    /// The ViewModel class for the Index.cshtml and Edit.cshtml
    /// Usually we have to use always 1 ViewModel class for 1 view, but the information for the both views quite similar
    /// </summary>
    public class CategoryViewModel
    {
        #region Properties
        public int ID { get; set; }

        [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Name { get; set; }
        #endregion

        #region Constructors
        public CategoryViewModel()
        {
            // create a default constructor, because the MVC needs it when the form is submitted, 
            // in order to create object of this type as parameter in an action
        }
        public CategoryViewModel(Category category)
        {
            this.Name = category.Name;
            this.ID = category.ID;
        }
        #endregion

        #region public methods
        public void UpdateDbModel(Category dbCategory)
        {
            dbCategory.Name = this.Name;
        }
        #endregion
    }
}