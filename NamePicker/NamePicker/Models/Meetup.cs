using System;
using System.Collections.Concurrent;

namespace NamePicker.Models
{
    public static class Meetup
    {
        public static ConcurrentDictionary<Guid, MeetupMember> Members { get; set; } = new ConcurrentDictionary<Guid, MeetupMember>();
    }
}