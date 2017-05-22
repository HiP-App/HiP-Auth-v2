
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PaderbornUniversity.SILab.Hip.Auth.Models;

namespace PaderbornUniversity.SILab.Hip.Auth.Utility
{
    public class IdentityConfig
    {
        public static IReadOnlyCollection<IdentityRole> GetRoles()
        {
            return new List<IdentityRole>
            {
                new IdentityRole("Student") { NormalizedName = "Student" },
                new IdentityRole("Supervisor") { NormalizedName = "Supervisor" },
                new IdentityRole("Administrator") { NormalizedName = "Administrator" }
            };
        }
    }
}