using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OwinAuthe.Models
{
    public class UserDisplayModel
    {
        public string AccessToken { get; set; }
        public string AvatarUrl { get; set; }
        public string Name { get; set; }
    }
}