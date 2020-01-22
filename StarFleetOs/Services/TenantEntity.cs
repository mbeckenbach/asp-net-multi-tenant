using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarFleetOs.Services
{
    public abstract class TenantEntity
    {
        public Guid TenantId { get; set; }
    }
}
