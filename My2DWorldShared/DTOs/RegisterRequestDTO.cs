using My2DWorldShared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2DWorldShared.DTOs
{
    public class RegisterRequestDTO
    {
		public string? Username { get; set; }
		public string? Password { get; set; }
		public string? Email { get; set; }
		public GenderType Gender { get; set; }
		public int SkinColor { get; set; }
		public int EyeColor { get; set; }
		public DateTime Birthday { get; set; }
	}
}
