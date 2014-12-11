using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace issuemoa.Models.Database
{
    public class Board
    {
        public Board() { }

        [Key]
        public int BoardId { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "ImageURL can not be greater then 50 character")]
        public string Title { get; set; }

        [Required]
        public int IssueId { get; set; }

        [ForeignKey("IssueId")]
        public virtual Issue Issue { get; set; }

        [Required]
        public int BoardTypeId { get; set; }

        [ForeignKey("BoardTypeId")]
        public virtual BoardType BoardType { get; set; }

        public virtual ICollection<Forum> ListForum { get; set; }
    }
}