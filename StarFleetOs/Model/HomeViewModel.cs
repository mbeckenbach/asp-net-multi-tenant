using StarFleetOs.Database.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
