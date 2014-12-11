using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace issuemoa.Models.Database
{
    public class PointHistory
    {
        public PointHistory() {}

        [Key]
        public int PointHistoryId { get; set; }

        [Required]
        public int ChangeAmount { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ChangeDate { get; set; }

        [Required]
        public int PointTypeId { get; set; }

        [ForeignKey("PointTypeId")]
        public virtual PointType PointType { get; set; }

        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public int? IssueId { get; set; }
        [ForeignKey("IssueId")]
        public virtual Issue Issue { get; set; }
    }
}