using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace issuemoa.Models.Database
{
    public class Forum
    {
        public Forum() 
        {
            Comments = new HashSet<Comment>();
        }

        [Key]
        public int ForumId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "ImageURL can not be greater then 50 character")]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UploadDate { get; set; }

        public string ImageUrl { get; set; }

        [DefaultValue(0)]
        public int LikeCount { get; set; }

        [DefaultValue(0)]
        public int HateCount { get; set; }

        [DefaultValue(0)]
        public int ViewCount { get; set; }

        [Required]
        public int WriterId { get; set; }
        [ForeignKey("WriterId")]
        public virtual User Writer { get; set; }

        [Required]
        public int BoardId { get; set; }

        [ForeignKey("BoardId")]
        public virtual Board Board { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}