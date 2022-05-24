using My2DWorldShared.DataEntities;
using My2DWorldShared.Enums;
using My2DWorldShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2DWorldShared.PacketsOut
{
	public class PacketUpdatePosition : PacketOutBase
	{
		public string? PlayerName { get; set; }
		public float X { get; set; }
		public float Y { get; set; }

		public PacketUpdatePosition()
		{
			Id = OutComingPacketId.UpdatePosition;
		}

        public PacketUpdatePosition(string? playername, float x, float y) : this()
        {
			PlayerName = playername;
			X = x;
			Y = y;
        }
	}
}
