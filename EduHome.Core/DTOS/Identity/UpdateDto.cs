﻿using System;
namespace EduHome.Core.DTOS
{
	public class UpdateDto
	{
        public string UserName { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
    }
}

