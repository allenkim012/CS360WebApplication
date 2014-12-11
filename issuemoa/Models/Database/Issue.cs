using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace issuemoa.Models.Database
{
    public class Issue
    {
        public Issue() 
        {
            PointHistories = new HashSet<PointHistory>();
        }
        [Key]
        public int IssueId { get; set; }

        [Required(ErrorMessage="IssueTitle is Required")]
        [MaxLength(30, ErrorMessage = "IssueTitle can not be greater then 30 character")]
        public string IssueTitle { get; set; }

        [MaxLength(30, ErrorMessage = "ImageURL can not be greater then 30 character")]
        public string ImageURL { get; set; }

        [Required(ErrorMessage = "StartDate is Required")]
        public DateTime StartDate { get; set; }

        [DefaultValue(0)]
        public int DonatedPoints { get; set; }

        [Required(ErrorMessage = "IsTopIssue is Required")]
        [DefaultValue(false)]
        public bool IsTopIssue { get; set; }

        public ICollection<PointHistory> PointHistories { get; set; }
    }
}