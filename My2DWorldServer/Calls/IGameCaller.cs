using My2DWorldShared.PacketsIn;
using System.Net.WebSockets;

namespace My2DWorldServer.Calls
{
    public interface IGameCaller
    {
        public abstract Task OnAuthenticate(PacketAuthenticate packet);
        public abstract Task OnChangeServer(PacketChangeServer packet);
        public abstract Task OnEquipItem(PacketEquipItem packet);
        public abstract Task OnPlayerMove(PacketPlayerMove packet);
        public abstract Task OnChatMessage(PacketChatMessage packet);
        public abstract Task OnMapChange(PacketMapChange packet);
        public abstract Task OnGameLoad(PacketGameLoad packet);
        public abstract Task OnGameQuit(PacketGameQuit packet);
        public abstract Task OnGameProgressUpdate(PacketGameProgressUpdate packet);
        public abstract Task OnShopLoad(PacketShopLoad packet);
        public abstract Task OnShopBuy(PacketShopBuy packet);
    }
}
