using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace issuemoa.Models.Database
{
    public class PointType
    {
        public PointType() { }

        [Key]
        public int PointTypeId { get; set; }

        [Required]
        public string Type { get; set; }
    }
}