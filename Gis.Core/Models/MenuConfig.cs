﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gis.Core.Models
{
    public class MenuConfig
    {
        public string Code { get; set; }        
        public string Name { get; set; }        
        public string Url { get; set; }        
        public string Icon { get; set; }
        public MenuConfig[] SubMenu { get; set; }
    }
}
