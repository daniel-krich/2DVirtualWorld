using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2DWorldShared.Models
{
	public class MapInformationModel
	{
		public string? Name { get; set; }
		public string? FilePath { get; set; }
		public float SpawnX { get; set; }
		public float SpawnY { get; set; }
		public MapExitInfo[]? MapLeave { get; set; }
		public MapNpcInfo[]? Npcs { get; set; }
		public UserInformationModel?[]? Users { get; set; }
	}

	public class MapExitInfo
	{
		public int ExitId { get; set; }
		public int MapExitId { get; set; }
		public string? MapExitName { get; set; }
		public float ArrowAngle { get; set; }
		public float EntranceX { get; set; }
		public float EntranceY { get; set; }
		public float ExitTeleportX { get; set; }
		public float ExitTeleportY { get; set; }
	}

	public class MapNpcInfo
	{
		public NpcInfo? Npc { get; set; }
		public float PositionX { get; set; }
		public float PositionY { get; set; }
		public float ScaleX { get; set; }
		public float ScaleY { get; set; }
	}

	public class NpcInfo
	{
		public int NpcId { get; set; }
		public string? Name { get; set; }
		public string? FilePath { get; set; }
		public string?[]? Speeches { get; set; }
		public string? About { get; set; }
		public MiniGameInfo[]? Games { get; set; }
	}

	public class MiniGameInfo
	{
		public int? GameId { get; set; }
		public string? GameName { get; set; }
		public string? FilePath { get; set; }
    }

}
