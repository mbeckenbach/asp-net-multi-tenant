using StarFleetOs.Database.App.Models;
using System.Collections.Generic;

namespace StarFleetOs.Model
{
    public class HomeViewModel
    {
        public string ShipName { get; set; }
        public string ShipIdentifier { get; set; }
        public string Image { get; set; }
        public List<CrewMember> CrewMembers { get; set; }
    }
}
