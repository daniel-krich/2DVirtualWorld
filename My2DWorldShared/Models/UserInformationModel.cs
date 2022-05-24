using My2DWorldShared.PacketsOut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2DWorldShared.Models
{
	public class UserInformationModel
	{
		public string? PlayerName { get; set; }
		public float PositionX { get; set; }
		public float PositionY { get; set; }
		public PhysicalInfoModel? PhysicalInfo { get; set; }
		public StatsInfoModel? StatsInfo { get; set; }
		public EquipedLoadoutModel[]? Loadout { get; set; }
	}
}
