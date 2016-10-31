using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using DotVVM.Framework.Controls;
using DotVVM.Framework.ViewModel;
using NamePicker.Models;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel.Validation;
using Validation = DotVVM.Framework.Controls.Validation;

namespace NamePicker.ViewModels
{
	public class NamePickerViewModel : MasterPageViewModel
	{
	    public NamePickerViewModel()
	    {
            Members = new GridViewDataSet<MeetupMember>()
            {
                PageSize = 5
            };
        }

        public GridViewDataSet<MeetupMember> Members { get; set; }

        [Required]
        public string NewName { get; set; }

        public string ErrorMessage { get; set; }

	    public void AddName()
	    {
	        var newGuid = Guid.NewGuid();
	        var name = new MeetupMember
	        {
	            Id = newGuid,
	            Name = NewName
	        };
	        Meetup.Members[newGuid] = name;
	    }

	    public void RemoveName(Guid id)
	    {
	        MeetupMember removed;
	        Meetup.Members.TryRemove(id, out removed);
	    }

        public override Task PreRender()
        {
            Members.LoadFromQueryable(Meetup.Members.Values.AsQueryable().OrderBy(m => m.Name));
            NewName = string.Empty;

            return base.PreRender();
        }
	}
}

