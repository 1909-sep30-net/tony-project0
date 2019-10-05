using System;

namespace ConsoleApp1
{
    enum RolePower{
        Admin=0,
        Customer=1
    }
    internal class UserRole
    {
        public String First { get; set; }
        public String Last { get; set; }
        public RolePower Auth { get; set; }
        
       
        static public void CheckUser( )
        {

        }

    }
}
