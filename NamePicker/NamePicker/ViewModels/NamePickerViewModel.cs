using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using DotVVM.Framework.Controls;
using DotVVM.Framework.ViewModel;
using NamePicker.Models;
using System.Threading.Tasks;
using System.Web.Configuration;
using DotVVM.Framework.Configuration;
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

        public string Winner { get; set; }

        public string SuperSecretPassword { get; set; }

        public string ErrorMessage { get; set; }

	    public void AddName()
        {
            CleanUp();
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
            CleanUp();
            if (SuperSecretPassword != WebConfigurationManager.AppSettings["SuperSecretPassword"])
	        {
	            ErrorMessage = "Nice try!";
                return;
	        }

            MeetupMember removed;
	        Meetup.Members.TryRemove(id, out removed);
	    }

	    public void PickWinner()
        {
            CleanUp();

            var contenstants = Meetup.Members.Values.Where(m => !m.HasWon).ToList();
            if (!contenstants.Any())
	        {
	            ErrorMessage = "We need some contenstants!";
                return;
	        }

            var random = new Random();

	        var winner = contenstants[random.Next(contenstants.Count)];
	        winner.HasWon = true;

	        Winner = $"The winner is: {winner.Name}";

	    }

        public override Task PreRender()
        {
            Members.LoadFromQueryable(Meetup.Members.Values.AsQueryable().OrderBy(m => m.Name));
            NewName = string.Empty;

            return base.PreRender();
        }

        private void CleanUp()
        {
            ErrorMessage = string.Empty;
            Winner = string.Empty;
        }
    }
}

