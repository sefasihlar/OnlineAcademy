﻿using Microsoft.AspNetCore.Identity;

namespace NLayer.Core.Concrate
{
    public class AppRole : IdentityRole<int>
    {
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public Boolean Condition { get; set; }

        public List<RoleUser> Users { get; set; }
    }
}