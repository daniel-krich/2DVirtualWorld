using My2DWorldShared.PacketsIn;
using System.Net.WebSockets;

namespace My2DWorldServer.Calls
{
    public interface IGameCaller
    {
        public Task OnAuthenticate(PacketAuthenticate packet);
        public Task OnChangeServer(PacketChangeServer packet);
        public Task OnTryEquipItem(PacketEquipItem packet);
        public Task OnPlayerMove(PacketPlayerMove packet);
        public Task OnChatMessage(PacketChatMessage packet);
        public Task OnMapChange(PacketMapChange packet);
        public Task OnGameLoad(PacketGameLoad packet);
        public Task OnGameQuit(PacketGameQuit packet);
        public Task OnGameProgressUpdate(PacketGameProgressUpdate packet);
        public Task OnShopLoad(PacketShopLoad packet);
        public Task OnShopBuy(PacketShopBuy packet);
        public Task OnRequestChangeServer(PacketRequestChangeServer packet);
        public Task OnQuitServer();
        public Task OnRequestInventoryBatch(PacketRequestInventoryBatch packet);
    }
}
