using RestAPIServiceApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace RestAPIServiceApplication.ViewModels
{
    [NotMapped]
    public class UserProfileViewModel : UserProfileModel
    {
        #region Class fields
        #endregion

        #region Properties
        public ImageSource UserProfileImageSource
        {
            get
            {
                if (this.UserProfileImage != null)
                    return 
                        ImageSource.FromStream(() => new MemoryStream(this.UserProfileImage));
                return Device.RuntimePlatform == Device.UWP ? "Assets/profileImage.png" : "profileImage";
            }
        } 
        #endregion
    }
}
