using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace RestAPIServiceApplication.Interfaces
{
    public interface IBaseViewModel
    {
        void OnPropertyChanged([CallerMemberName] string propertyName = null);
    }
}
