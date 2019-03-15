using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace DomainEntities
{
   public class User : IdentityUser
    {
        string FirstName { get; set; }
    }
}
