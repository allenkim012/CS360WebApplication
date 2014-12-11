using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace issuemoa.Models.Database
{
    public class User
    {
        public User() { }

        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage="UserName is Required")]
        [MaxLength(40, ErrorMessage="UserName can not be greater then 40 character"), MinLength(3, ErrorMessage="UserName can not be 3 character or less")]
        public string UserName { get; set; }

        [Required(ErrorMessage="Password is Required")]
        [MaxLength(100, ErrorMessage = "Password can not be greater then 100 character"), MinLength(8, ErrorMessage = "Password can not be 8 character or less")]
        public string Password { get; set; }
       
        [Required(ErrorMessage = "Name is Required")]
        [MaxLength(10, ErrorMessage = "Name can not be greater then 10 character")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [MaxLength(50, ErrorMessage = "Email can not be greater then 20 character"), MinLength(5, ErrorMessage = "Email can not be 5 character or less")]
        public string Email { get; set; }

        [DefaultValue(0)]
        public int PointsGained { get; set; }

        [DefaultValue(0)]
        public int PointsDonated { get; set; }
    }
}