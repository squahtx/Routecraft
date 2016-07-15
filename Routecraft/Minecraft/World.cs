using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Routecraft.Minecraft.Storage;

namespace Routecraft.Minecraft
{
    public class World
    {
        public string Path { get; private set; }

        private Dimension[] Dimensions = new Dimension[3];

        #region World Data
        public string Name = "world";
        public int Version = 19132;

        public long Time = 0;
        public long LastPlayed = 0;

        // World Generation
        public long Seed = 0;
        public bool GenerateStructures = true;
        public bool CreativeMode = false;
        public bool Hardcore = false;
        public Vector3I SpawnPos = new Vector3I(0, 64, 0);

        // Weather
        public bool Raining = false;
        public int RainTime = 0;
        public bool Thundering = false;
        public int ThunderTime = 0;
        #endregion

        public World(string path)
        {
            this.Path = path;
            if (Directory.Exists(this.Path))
            {
                if (File.Exists(this.Path + "\\level.dat"))
                {
                    this.Open();
                }
            }

            this.Dimensions[0] = new Dimension(this, DimensionType.Normal);
            this.Dimensions[1] = new Dimension(this, DimensionType.Nether);
            this.Dimensions[2] = new Dimension(this, DimensionType.End);
        }

        public Dimension GetDimension(DimensionType type)
        {
            switch (type)
            {
                case DimensionType.Normal:
                    return this.Dimensions[0];
                case DimensionType.Nether:
                    return this.Dimensions[1];
                case DimensionType.End:
                    return this.Dimensions[2];
            }
            throw new ArgumentOutOfRangeException("type");
        }

        #region Serialization
        public void Open()
        {
            NBTFile LevelFile = new NBTFile(this.Path + "\\level.dat");
            NBTCompoundTag Level = (NBTCompoundTag)((NBTCompoundTag)LevelFile.Root).Children.First();

            this.Name = Level.GetValue<string>("LevelName");
            this.Version = Level.GetValue<int>("version");

            this.Time = Level.GetValue<long>("Time");
            this.LastPlayed = Level.GetValue<long>("LastPlayed");

            this.Seed = Level.GetValue<long>("RandomSeed");
            this.GenerateStructures = Level.GetValue<byte>("MapFeatures") == 1 ? true : false;
            this.CreativeMode = Level.GetValue<int>("GameType") == 1 ? true : false;
            this.Hardcore = Level.GetValue<byte>("hardcore") == 1 ? true : false;
            this.SpawnPos = new Vector3I(Level.GetValue<int>("SpawnX"), Level.GetValue<int>("SpawnZ"), Level.GetValue<int>("SpawnY"));

            this.Raining = Level.GetValue<byte>("raining") == 1 ? true : false;
            this.RainTime = Level.GetValue<int>("rainTime");
            this.Thundering = Level.GetValue<byte>("thundering") == 1 ? true : false;
            this.ThunderTime = Level.GetValue<int>("thunderTime");
        }

        public void Save()
        {
            Debug.WriteLine("Saving world...");

            NBTCompoundTag Root = new NBTCompoundTag();
            {
                NBTCompoundTag Data = Root.AddChild<NBTCompoundTag>("Data");
                {
                    Data.AddChild<NBTStringTag>("LevelName").Value = this.Name;
                    Data.AddChild<NBTIntTag>("version").Value = this.Version;
                    Data.AddChild<NBTLongTag>("Time").Value = this.Time;
                    Data.AddChild<NBTLongTag>("LastPlayed").Value = this.LastPlayed;
                    Data.AddChild<NBTLongTag>("RandomSeed").Value = this.Seed;
                    Data.AddChild<NBTByteTag>("MapFeatures").Value = (byte)(this.GenerateStructures ? 1 : 0);
                    Data.AddChild<NBTIntTag>("GameType").Value = (byte)(this.CreativeMode ? 1 : 0);
                    Data.AddChild<NBTByteTag>("hardcore").Value = (byte)(this.Hardcore ? 1 : 0);
                    Data.AddChild<NBTIntTag>("SpawnX").Value = this.SpawnPos.x;
                    Data.AddChild<NBTIntTag>("SpawnY").Value = this.SpawnPos.z;
                    Data.AddChild<NBTIntTag>("SpawnZ").Value = this.SpawnPos.y;
                    Data.AddChild<NBTByteTag>("raining").Value = (byte)(this.Raining ? 1 : 0);
                    Data.AddChild<NBTIntTag>("rainTime").Value = this.RainTime;
                    Data.AddChild<NBTByteTag>("thundering").Value = (byte)(this.Thundering ? 1 : 0);
                    Data.AddChild<NBTIntTag>("thunderTime").Value = this.ThunderTime;
                }
            }

            NBTFile LevelFile = new NBTFile(this.Path + "\\level.dat");
            LevelFile.Root = Root;
            LevelFile.Save();

            Debug.WriteLine("Saved level.dat.");

            this.Dimensions[0].Save();
            this.Dimensions[1].Save();
            this.Dimensions[2].Save();

            Debug.WriteLine("Saved world.");
        }
        #endregion
    }
}
