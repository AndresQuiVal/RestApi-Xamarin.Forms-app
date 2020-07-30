using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RestAPIServiceApplication.Models
{
    public class Module
    {
        public ImageSource ModuleIcon { get; set; }
        public string ModuleTitle { get; set; }
        public ICommand ModuleCommand { get; set; }
    }
}
