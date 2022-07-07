using ProjectManagement.Domain.Models;
using System;

namespace ProjectManagement.UI.Wrapper
{
    public class MeetingWrapper : ModelWrapper<Meeting>
    {
        #region Ctor
        public MeetingWrapper(Meeting model) : base(model)
        {
        } 
        #endregion

        #region Properties
        public Guid Id => Model.Id;

        public string Title
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public DateTime DateFrom
        {
            get => GetValue<DateTime>();
            set
            {
                SetValue(value);
                if (DateTo < DateFrom)
                {
                    DateTo = DateFrom;
                }
            }
        }

        public DateTime DateTo
        {
            get => GetValue<DateTime>();
            set
            {
                SetValue(value);
                if (DateTo < DateFrom)
                {
                    DateFrom = DateTo;
                }
            }
        } 
        #endregion
    }
}