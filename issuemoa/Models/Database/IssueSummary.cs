using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace issuemoa.Models.Database
{
    [Table("IssueSummary")]
    public class IssueSummary
    {
        public IssueSummary() { }

        [Key]
        public int IssueSummaryId { get; set; }

        [Required(ErrorMessage="Summary Title is required")]
        public string SummaryTitle { get; set; }
        public string Summary { get; set; }

        public int IssueId { get; set; }

        [ForeignKey("IssueId")]
        public virtual Issue Issue { get; set; }
    }
}