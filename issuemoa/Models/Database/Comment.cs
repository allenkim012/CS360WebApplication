using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace issuemoa.Models.Database
{
    public class Comment
    {
        public Comment() { }

        [Key]
        public int CommentId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UploadDate { get; set; }

        [Required]
        public string Text { get; set; }

        [DefaultValue(0)]
        public int LikeCount { get; set; }

        [DefaultValue(0)]
        public int HateCount { get; set; }

        [Required]
        public int WriterId { get; set; }

        [ForeignKey("WriterId")]
        public virtual User Writer { get; set; }

        [Required]
        public int ForumId { get; set; }

        [ForeignKey("ForumId")]
        public virtual Forum Forum { get; set; }
    }
}