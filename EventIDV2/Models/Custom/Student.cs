using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventIDV2.Models
{
    [MetadataType(typeof(StudentMetadata))]
    public partial class Student
    {
        
    }
    public class StudentMetadata
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}