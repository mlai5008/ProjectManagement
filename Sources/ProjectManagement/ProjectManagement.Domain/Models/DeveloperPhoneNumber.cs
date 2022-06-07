using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagement.Domain.Models
{
    public class DeveloperPhoneNumber
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Phone]
        [Required]
        public string Number { get; set; }

        public Guid DeveloperId { get; set; }

        [ForeignKey(nameof(DeveloperId))]
        [InverseProperty(nameof(ProjectManagement.Domain.Models.Developer.PhoneNumbers))]
        public Developer Developer { get; set; } 
        #endregion
    }
}