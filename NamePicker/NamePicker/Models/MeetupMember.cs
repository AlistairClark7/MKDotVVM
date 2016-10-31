using System;

namespace NamePicker.Models
{
    public class MeetupMember
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool HasWon { get; set; }
    }
}