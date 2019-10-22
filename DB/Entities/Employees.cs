using System;
using System.Collections.Generic;

namespace DB.Entities
{
    public partial class Employees
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int StoreId { get; set; }
        public string UserName { get; set; }
        public string Pass { get; set; }
        public int RoleId { get; set; }
        public int Zip { get; set; }

        public virtual RoleTypes Role { get; set; }
        public virtual Stores Store { get; set; }
    }
}
