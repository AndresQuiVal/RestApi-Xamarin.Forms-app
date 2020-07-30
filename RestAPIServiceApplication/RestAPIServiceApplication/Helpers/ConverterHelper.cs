using RestAPIServiceApplication.Models;
using RestAPIServiceApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestAPIServiceApplication.Helpers
{
    public static class ConverterHelper
    {
        public static ContactViewModel ToContactViewModel(ContactsModel model)
        {
            return new ContactViewModel
            {
                ContactAlias = model.ContactAlias,
                ContactCompanyName = model.ContactCompanyName,
                ContactImage = model.ContactImage,
                ContactName = model.ContactName,
                ContactNumber = model.ContactNumber,
                UserId = model.UserId,
                Id = model.Id
            };
        }

        public static ContactsModel ToContactModel(ContactViewModel model)
        {
            return new ContactsModel
            {
                ContactAlias = model.ContactAlias,
                ContactCompanyName = model.ContactCompanyName,
                ContactImage = model.ContactImage,
                ContactName = model.ContactName,
                ContactNumber = model.ContactNumber,
                UserId = model.UserId,
                Id = model.Id
            };
        }

        public static UserProfileViewModel ToUserProfileViewModel(
            UserProfileModel model)
        {
            return new UserProfileViewModel
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Id = model.Id,
                SurveysAnswered = model.SurveysAnswered,
                Token = model.Token,
                UserProfileImage = model.UserProfileImage
            };
        }

        public static UserProfileModel ToUserProfileModel(UserProfileViewModel model)
        {
            return new UserProfileModel
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Id = model.Id,
                SurveysAnswered = model.SurveysAnswered,
                Token = model.Token,
                UserProfileImage = model.UserProfileImage
            };
        }
    }
}
