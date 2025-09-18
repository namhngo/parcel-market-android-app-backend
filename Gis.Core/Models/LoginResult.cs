using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gis.Core.Models
{
    public class LoginResult
    {
        public Guid UserId { get; set; }
        public string[] Roles { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string AccessToken { get; set; }
        public string PermWorkFlow { get; set; }
        public List<PermFeature> PermFeatures { get; set; } 
    }
    public class PermFeature
    {
        public string featureType { get; set; }
        public string propertyNames { get; set; }
        public string title { get; set; }
    }
}
