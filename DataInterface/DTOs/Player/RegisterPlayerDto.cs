using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInterface.DTOs.Player
{
    public class RegisterPlayerDto
    {
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public RegisterPlayerDto(string userName, string firstName, string lastName)
        {
            Username = userName;
            FirstName = firstName;
            LastName = lastName;
        }

    }
}
