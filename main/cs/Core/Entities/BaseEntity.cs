
using System;

namespace Wepsys.DianaHr.Core.Entities
{
    public class BaseEntity
    {
        public Guid roleId { get; set; }
        public DateTime Created { get; set; }
        public DateTime UpdatedBy { get; set; }

    }
}
