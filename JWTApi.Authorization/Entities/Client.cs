using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JWTApi.Authorization.Entities
{
    public class Client
    {
        [Required]
        [MaxLength(100)]
        public string AppName { get; set; }


        [Required]
        [MaxLength(200)]
        public string RedirectUrl { get; set; }


        [Required]
        [MaxLength(100)]
        public string CompanyName { get; set; }
    }
}