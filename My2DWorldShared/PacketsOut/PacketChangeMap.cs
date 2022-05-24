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
	public class PacketChangeMap : PacketOutBase
	{
		public MapInformationModel? Info { get; set; }

		public PacketChangeMap()
		{
			Id = OutComingPacketId.ChangeMap;
		}
	}
}
