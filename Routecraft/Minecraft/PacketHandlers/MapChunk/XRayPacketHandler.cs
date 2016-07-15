using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Routecraft.Minecraft.Networking;
using Routecraft.Minecraft.Networking.Packets;

namespace Routecraft.Minecraft.PacketHandlers.MapChunk
{
    public class XRayPacketHandler : RelayPacketHandler
    {
        public override MinecraftRelayAction HandlePacket(MinecraftRelay relay, PacketType packetType, IPacket packet)
        {
            MapChunkPacket Packet = (MapChunkPacket)packet;
            byte[] Data = new byte[Packet.DecompressedDataLength];
            Packet.GetDecompressedData(Data);

            uint MetadataStart = Packet.Volume;
            uint BlockLightStart = MetadataStart + Packet.Volume / 2;
            uint SkyLightStart = BlockLightStart + Packet.Volume / 2;

            for (uint i = 0; i < MetadataStart; i++)
            {
                byte Replacement = Data[i];
                switch (Data[i])
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 98:
                        Replacement = 20;
                        break;
                }

                // Maximize lighting
                switch (Data[i])
                {
                    case 1:
                    case 5:
                    case 14:
                    case 15:
                    case 16:
                    case 21:
                    case 56:
                    case 73:
                    case 74:
                    case 97:    // Silverfish
                        byte Lighting = Data[BlockLightStart + i / 2];
                        if ((i & 1) == 0)
                        {
                            Lighting = (byte)((Lighting & 0x0F) | 0xF0);
                        }
                        else
                        {
                            Lighting = (byte)((Lighting & 0xF0) | 0x0F);
                        }
                        Data[BlockLightStart + i / 2] = Lighting;
                        break;
                }

                Data[i] = Replacement;
            }

            Packet.SetDecompressedData(Data);
            return MinecraftRelayAction.RelayModified;
        }
    }
}
