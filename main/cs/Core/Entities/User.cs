using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Wepsys.DianaHr.Core.Entities
{
    /// <summary>
    /// Represent a user from the system
    /// </summary>
    public class User
    {        
        /// <summary>
        /// Represent's the unique ID of the user
        /// </summary>  

        public Guid Id { get; set; }

        /// <summary>
        /// Represent's the email of the user
        /// </summary>
        
        [Required(ErrorMessage="Invalid Length"), StringLength(500)]
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Represent's the email of the user
        /// </summary>

        [Required(ErrorMessage="Invalid Length"), StringLength(500)]
        public string DisplayName { get; set; } = string.Empty;

        /// email validator using attribute data annotation and regex

        [Required(ErrorMessage="Invalid Length"), StringLength(254), MinLength(6)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// This is to assign User's Role. Name the their titles within the company
        /// </summary>
        public string  Role { get; set; }  = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iuser"></param>
        /// <returns></returns>

       
    }
}
