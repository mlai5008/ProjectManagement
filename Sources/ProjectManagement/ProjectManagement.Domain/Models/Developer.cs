using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagement.Domain.Models
{
    public class Developer
    {
        #region Ctor
        public Developer()
        {
            PhoneNumbers = new List<DeveloperPhoneNumber>();
        } 
        #endregion

        #region Properties
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string LastName { get; set; }

        [StringLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        public Guid? ProgrammingLanguageId { get; set; }
        [ForeignKey(nameof(ProgrammingLanguageId))]
        public virtual ProgrammingLanguage FavoriteLanguage { get; set; }

        [InverseProperty(nameof(DeveloperPhoneNumber.Developer))]
        public virtual ICollection<DeveloperPhoneNumber> PhoneNumbers { get; set; }
        #endregion
    }
}