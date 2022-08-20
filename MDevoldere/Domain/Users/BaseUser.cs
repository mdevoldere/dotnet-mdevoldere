using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDevoldere.Domain.Users
{
    public class BaseUser : Model
    {
        [Required]
        public string Username { get; set; }

        public int Level { get; set; }

        public BaseUser()
        {
            Username = "Guest";
            Level = 0;
        }
    }
}
