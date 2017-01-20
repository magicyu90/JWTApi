namespace JWTApi.Authorization.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DoctorAccount")]
    public partial class DoctorAccount
    {
        [Key]
        public Guid DoctorID { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public int Status { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime LastChangeTime { get; set; }

        public DateTime LastStatusChangeTime { get; set; }

        public DateTime LastLoginTime { get; set; }

        public DateTime LastLogoutTime { get; set; }

        public DateTime LastPWChangedTime { get; set; }

        public DateTime LastLockoutTime { get; set; }

        public int FailedPWAttemptCount { get; set; }

        [Required]
        [StringLength(42)]
        public string FindPasswordCode { get; set; }

        [Required]
        [StringLength(500)]
        public string Comment { get; set; }

        public int IsActive { get; set; }

        [Required]
        [StringLength(20)]
        public string LT { get; set; }

        [Required]
        [StringLength(400)]
        public string OpenID { get; set; }
    }
}
