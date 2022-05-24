namespace My2DWorldServer.Services
{
    public interface IGameInformer
    {
        public Task SendAuthenticateConnection();
        public Task SendAuthenticateRejectConnection(string reason);
        public Task SendMapChange(int mapId, int exitId = -1);
        public Task SendPlayerUpdatePosition(float x, float y);
        public Task SendPushUserInformation();
    }
}
