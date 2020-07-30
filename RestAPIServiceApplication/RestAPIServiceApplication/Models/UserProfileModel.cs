using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RestAPIServiceApplication.Models
{
    public class UserProfileModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SurveysAnswered { get; set; }
        public byte[] UserProfileImage { get; set; }

        [NotMapped]
        public TokenResponse Token { get; set; }
    }
}
