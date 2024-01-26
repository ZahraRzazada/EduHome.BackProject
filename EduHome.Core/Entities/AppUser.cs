using System;
using Microsoft.AspNetCore.Identity;

namespace EduHome.Core.Entities
{
	public class AppUser:IdentityUser
	{
		public string FullName { get; set; } = null!;
	}
}

