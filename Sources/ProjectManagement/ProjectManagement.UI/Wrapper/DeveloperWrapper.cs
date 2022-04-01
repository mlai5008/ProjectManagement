using ProjectManagement.Domain.Models;
using System;
using System.Collections.Generic;

namespace ProjectManagement.UI.Wrapper
{
    public class DeveloperWrapper : ModelWrapper<Developer>
    {
        #region Ctor
        public DeveloperWrapper(Developer developer) : base(developer)
        { }
        #endregion

        #region Properties
        public Guid Id => Model.Id;

        public string FirstName
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string LastName
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string Email
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        #endregion

        #region Methods
        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(FirstName):
                    if (string.Equals(FirstName, "Robot", StringComparison.OrdinalIgnoreCase))
                    {
                        yield return "Robot are not valid Dev";
                    }
                    break;
            }
        } 
        #endregion
    }
}