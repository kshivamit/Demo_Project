using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CandidateDL
{
    public class Candidate
    {
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Candidate Name is required.")]
        public string CandidateName { get; set; }

        [Required(ErrorMessage = "Father Name is required.")]
        public string FatherName { get; set; }

        [Required(ErrorMessage = "EmailId is required.")]
        public string EmailId { get; set; }
    }
}
