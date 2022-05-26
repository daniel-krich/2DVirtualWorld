using My2DWorldShared.DataEntities;

namespace My2DWorldServer.Services
{
    public interface IGameInformer
    {
        public Task SendAuthenticateConnection();
        public Task SendAuthenticateRejectConnection(string reason);
        public Task SendMapChange(int mapId, int exitId);
        public Task SendPlayerUpdatePosition(float x, float y);
        public Task SendPushUserInformation();
        public Task SendPushUserMessageToRoom(UserEntity? user, string? message);
        public Task SendJoinedRoomToAll(UserEntity? user);
        public Task SendExitedRoomToAll(UserEntity? user);
        public Task SendPlayerGameLoad(GameEntity? game);
    }
}
