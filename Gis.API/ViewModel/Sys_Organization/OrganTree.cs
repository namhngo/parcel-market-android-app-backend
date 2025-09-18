using Gis.Core.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gis.Core.Enums;

namespace Gis.API.ViewModel.Sys_Organization
{
    public class OrganTree:absTree<OrganTree>
    {
        public OrganizationType Type { get; set; }
    }
}
