using ProjectManagement.Domain.Models;
using System;

namespace ProjectManagement.UI.Wrapper
{
    public class ProgrammingLanguageWrapper : ModelWrapper<ProgrammingLanguage>
    {
        public ProgrammingLanguageWrapper(ProgrammingLanguage model) : base(model)
        { }

        public Guid Id => Model.Id;

        public string Name
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
    }
}