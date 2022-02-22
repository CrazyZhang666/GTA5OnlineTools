using GTA5OnlineTools.Features.Core;
using GTA5OnlineTools.Features.Data;

namespace GTA5OnlineTools.Features.SDK
{
    public class Teleport
    {
        public static float PlayerX
        {
            get => Memory.Read<float>(Globals.WorldPTR, Offsets.PlayerPositionX);
            set
            {
                Memory.Write(Globals.WorldPTR, Offsets.PlayerPositionX, value);
                Memory.Write(Globals.WorldPTR, Offsets.PlayerVisualX, value);
            }
        }

        public static float PlayerY
        {
            get => Memory.Read<float>(Globals.WorldPTR, Offsets.PlayerPositionY);
            set
            {
                Memory.Write(Globals.WorldPTR, Offsets.PlayerPositionY, value);
                Memory.Write(Globals.WorldPTR, Offsets.PlayerVisualY, value);
            }
        }

        public static float PlayerZ
        {
            get => Memory.Read<float>(Globals.WorldPTR, Offsets.PlayerPositionZ);
            set
            {
                Memory.Write(Globals.WorldPTR, Offsets.PlayerPositionZ, value);
                Memory.Write(Globals.WorldPTR, Offsets.PlayerVisualZ, value);
            }
        }

        public static float VehicleX
        {
            get => Memory.Read<float>(Globals.WorldPTR, Offsets.VehiclePositionX);
            set
            {
                Memory.Write(Globals.WorldPTR, Offsets.VehiclePositionX, value);
                Memory.Write(Globals.WorldPTR, Offsets.VehicleVisualX, value);
            }
        }

        public static float VehicleY
        {
            get => Memory.Read<float>(Globals.WorldPTR, Offsets.VehiclePositionY);
            set
            {
                Memory.Write(Globals.WorldPTR, Offsets.VehiclePositionY, value);
                Memory.Write(Globals.WorldPTR, Offsets.VehicleVisualY, value);
            }
        }

        public static float VehicleZ
        {
            get => Memory.Read<float>(Globals.WorldPTR, Offsets.VehiclePositionZ);
            set
            {
                Memory.Write(Globals.WorldPTR, Offsets.VehiclePositionZ, value);
                Memory.Write(Globals.WorldPTR, Offsets.VehicleVisualZ, value);
            }
        }

        /// <summary>
        /// 传送功能
        /// </summary>
        public static void SetTeleportV3Pos(Vector3 pos)
        {
            if (pos != Vector3.Zero)
            {
                if (Memory.Read<byte>(Globals.WorldPTR, Offsets.InVehicle) == 0x00)
                {
                    VehicleX = pos.X;
                    VehicleY = pos.Y;
                    VehicleZ = pos.Z;
                }
                else
                {
                    PlayerX = pos.X;
                    PlayerY = pos.Y;
                    PlayerZ = pos.Z;
                }
            }
        }

        /// <summary>
        /// 导航点坐标
        /// </summary>
        public static Vector3 WaypointCoords()
        {
            Vector3 v3 = Vector3.Zero;
            int dwIcon, dwColor;

            for (int i = 2000; i > 1; i--)
            {
                dwIcon = Memory.Read<int>(Globals.BlipPTR + (i * 8), new int[] { 0x40 });
                dwColor = Memory.Read<int>(Globals.BlipPTR + (i * 8), new int[] { 0x48 });

                if (dwIcon == 8 && dwColor == 84)
                {
                    v3.X = Memory.Read<float>(Globals.BlipPTR + (i * 8), new int[] { 0x10 });
                    v3.Y = Memory.Read<float>(Globals.BlipPTR + (i * 8), new int[] { 0x14 });
                    v3.Z = Memory.Read<float>(Globals.BlipPTR + (i * 8), new int[] { 0x18 });

                    v3.Z = v3.Z == 20.0f ? -225.0f : v3.Z + 1.0f;

                    return v3;
                }
            }

            return v3;
        }

        /// <summary>
        /// 目标点坐标
        /// </summary>
        public static Vector3 ObjectiveCoords()
        {
            Vector3 v3 = Vector3.Zero;
            int dwIcon, dwColor;

            for (int i = 2000; i > 1; i--)
            {
                dwIcon = Memory.Read<int>(Globals.BlipPTR + (i * 8), new int[] { 0x40 });
                dwColor = Memory.Read<int>(Globals.BlipPTR + (i * 8), new int[] { 0x48 });

                //  黄点
                if (dwIcon == 1 &&
                    (dwColor == 5 || dwColor == 60 || dwColor == 66))
                {

                    v3.X = Memory.Read<float>(Globals.BlipPTR + (i * 8), new int[] { 0x10 });
                    v3.Y = Memory.Read<float>(Globals.BlipPTR + (i * 8), new int[] { 0x14 });
                    v3.Z = Memory.Read<float>(Globals.BlipPTR + (i * 8), new int[] { 0x18 }) + 1.0f;

                    return v3;
                }
            }

            for (int i = 2000; i > 1; i--)
            {
                dwIcon = Memory.Read<int>(Globals.BlipPTR + (i * 8), new int[] { 0x40 });
                dwColor = Memory.Read<int>(Globals.BlipPTR + (i * 8), new int[] { 0x48 });

                if ((dwIcon == 1 || dwIcon == 225 || dwIcon == 427 || dwIcon == 478 || dwIcon == 501 || dwIcon == 523 || dwIcon == 556) &&
                    (dwColor == 1 || dwColor == 2 || dwColor == 3 || dwColor == 54 || dwColor == 78))
                {
                    v3.X = Memory.Read<float>(Globals.BlipPTR + (i * 8), new int[] { 0x10 });
                    v3.Y = Memory.Read<float>(Globals.BlipPTR + (i * 8), new int[] { 0x14 });
                    v3.Z = Memory.Read<float>(Globals.BlipPTR + (i * 8), new int[] { 0x18 }) + 1.0f;

                    return v3;
                }
            }

            for (int i = 2000; i > 1; i--)
            {
                dwIcon = Memory.Read<int>(Globals.BlipPTR + (i * 8), new int[] { 0x40 });
                dwColor = Memory.Read<int>(Globals.BlipPTR + (i * 8), new int[] { 0x48 });

                if ((dwIcon == 432 || dwIcon == 443) &&
                    (dwColor == 59))
                {
                    v3.X = Memory.Read<float>(Globals.BlipPTR + (i * 8), new int[] { 0x10 });
                    v3.Y = Memory.Read<float>(Globals.BlipPTR + (i * 8), new int[] { 0x14 });
                    v3.Z = Memory.Read<float>(Globals.BlipPTR + (i * 8), new int[] { 0x18 }) + 1.0f;

                    return v3;
                }
            }

            return v3;
        }

        /// <summary>
        /// 目标点坐标自定义
        /// </summary>
        public static Vector3 ObjectiveCoordsCustom(int target_Icon)
        {
            Vector3 v3 = Vector3.Zero;
            int dwIcon;

            for (int i = 2000; i > 1; i--)
            {
                dwIcon = Memory.Read<int>(Globals.BlipPTR + (i * 8), new int[] { 0x40 });

                if (dwIcon == target_Icon)
                {
                    v3.X = Memory.Read<float>(Globals.BlipPTR + (i * 8), new int[] { 0x10 });
                    v3.Y = Memory.Read<float>(Globals.BlipPTR + (i * 8), new int[] { 0x14 });
                    v3.Z = Memory.Read<float>(Globals.BlipPTR + (i * 8), new int[] { 0x18 }) + 1.0f;

                    return v3;
                }
            }
            return v3;
        }

        public static Vector3 ObjectiveCoordsCustom(int target_Icon, int target_colour)
        {
            Vector3 v3 = Vector3.Zero;
            int dwIcon;
            int dwColour;

            for (int i = 2000; i > 1; i--)
            {
                dwIcon = Memory.Read<int>(Globals.BlipPTR + (i * 8), new int[] { 0x40 });
                dwColour = Memory.Read<int>(Globals.BlipPTR + (i * 8), new int[] { 0x48 });

                if (dwIcon == target_Icon && dwColour == target_colour)
                {
                    v3.X = Memory.Read<float>(Globals.BlipPTR + (i * 8), new int[] { 0x10 });
                    v3.Y = Memory.Read<float>(Globals.BlipPTR + (i * 8), new int[] { 0x14 });
                    v3.Z = Memory.Read<float>(Globals.BlipPTR + (i * 8), new int[] { 0x18 }) + 1.0f;
                    
                    return v3;
                }
            }
            return v3;
        }

        /// <summary>
        /// 坐标向前微调
        /// </summary>
        public static void MovingFoward()
        {
            float sin = Memory.Read<float>(Globals.WorldPTR, Offsets.PlayerSin);
            float cos = Memory.Read<float>(Globals.WorldPTR, Offsets.PlayerCos);

            if (Memory.Read<byte>(Globals.WorldPTR, Offsets.InVehicle) == 0)
            {
                float x = Memory.Read<float>(Globals.WorldPTR, Offsets.VehiclePositionX);
                float y = Memory.Read<float>(Globals.WorldPTR, Offsets.VehiclePositionY);

                x += Settings.Forward * cos;
                y += Settings.Forward * sin;

                Memory.Write(Globals.WorldPTR, Offsets.VehiclePositionX, x);
                Memory.Write(Globals.WorldPTR, Offsets.VehiclePositionY, y);

                Memory.Write(Globals.WorldPTR, Offsets.VehicleVisualX, x);
                Memory.Write(Globals.WorldPTR, Offsets.VehicleVisualY, y);
            }
            else
            {
                float x = Memory.Read<float>(Globals.WorldPTR, Offsets.PlayerPositionX);
                float y = Memory.Read<float>(Globals.WorldPTR, Offsets.PlayerPositionY);

                x += Settings.Forward * cos;
                y += Settings.Forward * sin;

                Memory.Write(Globals.WorldPTR, Offsets.PlayerPositionX, x);
                Memory.Write(Globals.WorldPTR, Offsets.PlayerPositionY, y);

                Memory.Write(Globals.WorldPTR, Offsets.PlayerVisualX, x);
                Memory.Write(Globals.WorldPTR, Offsets.PlayerVisualY, y);
            }
        }

        /// <summary>
        /// 传送到导航点
        /// </summary>
        public static void ToWaypoint()
        {
            SetTeleportV3Pos(WaypointCoords());
        }

        /// <summary>
        /// 传送到目标点
        /// </summary>
        public static void ToObjective()
        {
            SetTeleportV3Pos(ObjectiveCoords());
        }

        /// <summary>
        /// 传送到Blips
        /// </summary>
        public static void ToBlips(int blipID)
        {
            int kosatkaID = MiscData.Blips.Find(t => t.Name == "虎鲸").ID;
            int yachtID = MiscData.Blips.Find(t => t.Name == "游艇").ID;

            if (blipID == kosatkaID || blipID == yachtID)
            {
                int blipColour = GetOrganizationColour();
                SetTeleportV3Pos(ObjectiveCoordsCustom(blipID, blipColour));
            }
            SetTeleportV3Pos(ObjectiveCoordsCustom(blipID));
        }

        /// <summary>
        /// 获取所在组织的颜色
        /// </summary>
        public static int GetOrganizationColour()
        {
            int dwColour;
            int dwIcon;
            for (int i = 2000; i > 1; i--)
            {
                dwIcon = Memory.Read<int>(Globals.BlipPTR + (i * 8), new int[] { 0x40 });
                dwColour = Memory.Read<int>(Globals.BlipPTR + (i * 8), new int[] { 0x48 });

                int ceoID = MiscData.Blips.Find(t => t.Name == "CEO办公室").ID;
                int mcID = MiscData.Blips.Find(t => t.Name == "摩托帮会所").ID;

                if (dwIcon == ceoID || dwIcon == mcID)
                {
                    return dwColour;
                }
            }
            return 0;
        }
    }
}
