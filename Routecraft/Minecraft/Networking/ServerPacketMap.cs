using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Routecraft.Minecraft.Networking.Packets;

namespace Routecraft.Minecraft.Networking
{
    public class ServerPacketMap : PacketMap
    {
        public ServerPacketMap()
        {
            this.Add(PacketType.KeepAlive,          typeof(KeepAlivePacket));
            this.Add(PacketType.LoginRequest,       typeof(LoginAcceptedPacket));
            this.Add(PacketType.Handshake,          typeof(HandshakeServerPacket));
            this.Add(PacketType.Chat,               typeof(ChatPacket));
            this.Add(PacketType.UpdateTime,         typeof(UpdateTimePacket));
            this.Add(PacketType.PlayerInventory,    typeof(PlayerInventoryPacket));
            this.Add(PacketType.SpawnPosition,      typeof(SpawnPositionPacket));
            this.Add(PacketType.UpdateHealth,       typeof(UpdateHealthPacket));
            this.Add(PacketType.Respawn,            typeof(RespawnPacket));
            this.Add(PacketType.PlayerLookMove,     typeof(PlayerLookMoveServerPacket));
            this.Add(PacketType.Sleep,              typeof(SleepPacket));
            this.Add(PacketType.Animation,          typeof(AnimationPacket));
            this.Add(PacketType.NamedEntitySpawn,   typeof(NamedEntitySpawnPacket));
            this.Add(PacketType.PickupSpawn,        typeof(PickupSpawnPacket));
            this.Add(PacketType.Collect,            typeof(CollectPacket));
            this.Add(PacketType.VehicleSpawn,       typeof(VehicleSpawnPacket));
            this.Add(PacketType.MobSpawn,           typeof(MobSpawnPacket));
            this.Add(PacketType.EntityPainting,     typeof(EntityPaintingPacket));
            this.Add(PacketType.EntityExpOrbPacket, typeof(EntityExpOrbPacket));
            this.Add(PacketType.EntityVelocity,     typeof(EntityVelocityPacket));
            this.Add(PacketType.DestroyEntity,      typeof(DestroyEntityPacket));
            this.Add(PacketType.Entity,             typeof(EntityPacket));
            this.Add(PacketType.RelEntityMove,      typeof(RelEntityMovePacket));
            this.Add(PacketType.EntityLook,         typeof(EntityLookPacket));
            this.Add(PacketType.RelEntityMoveLook,  typeof(RelEntityMoveLookPacket));
            this.Add(PacketType.EntityTeleport,     typeof(EntityTeleportPacket));
            this.Add(PacketType.EntityStatus,       typeof(EntityStatusPacket));
            this.Add(PacketType.AttachEntity,       typeof(AttachEntityPacket));
            this.Add(PacketType.EntityMetadata,     typeof(EntityMetadataPacket));
            this.Add(PacketType.EntityEffect,       typeof(EntityEffectPacket));
            this.Add(PacketType.RemoveEntityEffect, typeof(RemoveEntityEffectPacket));
            this.Add(PacketType.Experience,         typeof(ExperiencePacket));
            this.Add(PacketType.PreChunk,           typeof(PreChunkPacket));
            this.Add(PacketType.MapChunk,           typeof(MapChunkPacket));
            this.Add(PacketType.MultiBlockChange,   typeof(MultiBlockChangePacket));
            this.Add(PacketType.BlockChange,        typeof(BlockChangePacket));
            this.Add(PacketType.PlayNoteBlock,      typeof(PlayNoteBlockPacket));
            this.Add(PacketType.Explosion,          typeof(ExplosionPacket));
            this.Add(PacketType.DoorChange,         typeof(DoorChangePacket));
            this.Add(PacketType.Bed,                typeof(BedPacket));
            this.Add(PacketType.Weather,            typeof(WeatherPacket));
            this.Add(PacketType.OpenWindow,         typeof(OpenWindowPacket));
            this.Add(PacketType.CloseWindow,        typeof(CloseWindowPacket));
            this.Add(PacketType.SetSlot,            typeof(SetSlotPacket));
            this.Add(PacketType.WindowItems,        typeof(WindowItemsPacket));
            this.Add(PacketType.UpdateProgressbar,  typeof(UpdateProgressbarPacket));
            this.Add(PacketType.Transaction,        typeof(TransactionPacket));
            this.Add(PacketType.CreativeSetSlot,    typeof(CreativeSetSlotPacket));
            this.Add(PacketType.UpdateSign,         typeof(UpdateSignPacket));
            this.Add(PacketType.MapData,            typeof(MapDataPacket));
            this.Add(PacketType.Statistic,          typeof(StatisticPacket));
            this.Add(PacketType.PlayerInfo,         typeof(PlayerInfoPacket));
            this.Add(PacketType.PluginMessage,      typeof(PluginMessagePacket));
            this.Add(PacketType.Disconnect,         typeof(DisconnectPacket));
        }
    }
}
