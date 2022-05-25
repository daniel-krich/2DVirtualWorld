using My2DWorldShared.Enums;
using My2DWorldShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2DWorldShared.PacketsOut
{
	public class PacketAuthenticateConnection : PacketOutBase
	{
		public bool Succeeded { get; set; }
		public string? Message { get; set; }
		public ServerInfoModel[]? Servers { get; set; }

        public PacketAuthenticateConnection()
        {
			Id = OutComingPacketId.ValidateAuthentication;
        }

        public PacketAuthenticateConnection(string message) : this()
        {
			Succeeded = false;
			Message = message;
		}
	}
}
