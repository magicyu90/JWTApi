using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JWTApi.Resource.Models
{
    [Table("Client")]
    public class ClientModel
    {
        [Key]
        public int Id { get; set; }

        public string AppName { get; set; }

        public Guid AppId { get; set; }

        public Guid AppSecret { get; set; }

        public string RedirectUrl { get; set; }

        public string CompanyName { get; set; }


    }
}