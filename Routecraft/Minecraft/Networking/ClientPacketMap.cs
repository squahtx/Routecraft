using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Routecraft.Minecraft.Networking.Packets;

namespace Routecraft.Minecraft.Networking
{
    public class ClientPacketMap : PacketMap
    {
        public ClientPacketMap()
        {
            this.Add(PacketType.KeepAlive,          typeof(KeepAlivePacket));
            this.Add(PacketType.LoginRequest,       typeof(LoginRequestPacket));
            this.Add(PacketType.Handshake,          typeof(HandshakeClientPacket));
            this.Add(PacketType.Chat,               typeof(ChatPacket));
            this.Add(PacketType.UseEntity,          typeof(UseEntityPacket));
            this.Add(PacketType.Respawn,            typeof(RespawnPacket));
            this.Add(PacketType.Flying,             typeof(FlyingPacket));
            this.Add(PacketType.PlayerPosition,     typeof(PlayerPositionPacket));
            this.Add(PacketType.PlayerLook,         typeof(PlayerLookPacket));
            this.Add(PacketType.PlayerLookMove,     typeof(PlayerLookMoveClientPacket));
            this.Add(PacketType.BlockDig,           typeof(BlockDigPacket));
            this.Add(PacketType.Place,              typeof(PlacePacket));
            this.Add(PacketType.BlockItemSwitch,    typeof(BlockItemSwitchPacket));
            this.Add(PacketType.Animation,          typeof(AnimationPacket));
            this.Add(PacketType.EntityAction,       typeof(EntityActionPacket));
            this.Add(PacketType.CloseWindow,        typeof(CloseWindowPacket));
            this.Add(PacketType.WindowClick,        typeof(WindowClickPacket));
            this.Add(PacketType.EnchantItem,        typeof(EnchantItemPacket));
            this.Add(PacketType.UpdateSign,         typeof(UpdateSignPacket));
            this.Add(PacketType.PluginMessage,      typeof(PluginMessagePacket));
            this.Add(PacketType.ServerPing,         typeof(ServerPingPacket));
            this.Add(PacketType.Disconnect,         typeof(DisconnectPacket));
        }
    }
}
