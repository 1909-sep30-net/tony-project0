using System.Collections.Generic;

namespace YourStore.Library
{
    public interface IUser
    {
        string FirstName { get; set; }
        int Id { get; set; }
        string LastName { get; set; }
        int Zip { get; set; }
        string Username { get; set; }
        string Pass { get; set; }
    }
}