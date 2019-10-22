using System;
using System.Collections.Generic;

namespace DB.Entities
{
    public partial class RoleTypes
    {
        public RoleTypes()
        {
            Employees = new HashSet<Employees>();
        }

        public int Id { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<Employees> Employees { get; set; }
    }
}
