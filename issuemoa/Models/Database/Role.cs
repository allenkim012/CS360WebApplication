using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace issuemoa.Models.Database
{
    public class Role
    {
        public Role() { }

        [Key]
        public int RoleId { get; set; }

        public string Description { get; set; }
    }
}