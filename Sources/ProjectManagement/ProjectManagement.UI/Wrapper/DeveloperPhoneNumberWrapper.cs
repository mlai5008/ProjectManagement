using ProjectManagement.Domain.Models;

namespace ProjectManagement.UI.Wrapper
{
    public class DeveloperPhoneNumberWrapper: ModelWrapper<DeveloperPhoneNumber>
    {
        #region Ctor
        public DeveloperPhoneNumberWrapper(DeveloperPhoneNumber model) : base(model)
        {}
        #endregion

        #region Properties
        public string Number
        {
            get => GetValue<string>();
            set => SetValue(value);
        } 
        #endregion
    }
}