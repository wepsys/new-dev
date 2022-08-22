using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wepsys.DianaHr.Core.Entities
{
    public class Roles : BaseEntity
    {
        [Required]
        public Guid roleId { get; set; }
        
        [Required(ErrorMessage="Invalid Length"), StringLength(100)]
        public string roleName { get; set; }

        [Required]
        public DateTime Created { get; set; } 
        
        [Required]
        public DateTime Updated { get; set; }
        
        [Required(ErrorMessage="Invalid Length"), StringLength(600)]
        public string UpdatedBy { get; set; }
        
        [Required(ErrorMessage="Invalid Length"), StringLength(600)]
        public string Createdby { get; set; }
    }
}

