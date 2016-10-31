using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;

namespace NamePicker.ViewModels
{
	public class MasterPageViewModel : DotvvmViewModelBase
    {

        public string Title { get; set; }


        public MasterPageViewModel()
        {
            Title = "Hello from DotVVM!";
        }
    }
}

