using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Admin.Entities
{
    public class EmailEntity
    {
        [ValidateNever]
        public string FromEmailAdress { get; set; }

        [EmailAddress]
        public string? ToEmail { get; set; }


    }
}
