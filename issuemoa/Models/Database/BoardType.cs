using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace issuemoa.Models.Database
{
    public class BoardType
    {
        public BoardType() { }

        [Key]
        public int BoardTypeId { get; set; }

        [Required]
        public string Description { get; set; }
    }
}