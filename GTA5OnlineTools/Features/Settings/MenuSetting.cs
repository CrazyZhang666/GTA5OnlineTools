using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Features.Settings;

public static class MenuSetting
{
    public static class Player
    {
        public static bool GodMode = false;
        public static bool AntiAFK = false;
        public static bool NoRagdoll = false;

        public static bool NoCollision = false;

        public static void Reset()
        {
            GodMode = false;
            AntiAFK = false;
            NoRagdoll = false;

            NoCollision = false;
        }
    }

    public static class Vehicle
    {
        public static bool GodMode = false;
        public static bool Seatbelt = false;

        public static void Reset()
        {
            GodMode = false;
            Seatbelt = false;
        }
    }

    public static class Weapon
    {
        public static int AmmoModifierFlag = 0;

        public static void Reset()
        {
            AmmoModifierFlag = 0;
        }
    }

    public static class Online
    {
        public static bool AllowSellOnNonPublic = false;

        public static void Reset()
        {
            AllowSellOnNonPublic = false;
        }
    }

    public static class Auto
    {
        public static bool ClearWanted = false;
        public static bool KillNPC = false;
        public static bool KillHostilityNPC = false;
        public static bool KillPolice = false;

        public static void Reset()
        {
            ClearWanted = false;
            KillNPC = false;
            KillHostilityNPC = false;
            KillPolice = false;
        }
    }

    public static class Overlay
    {
        public static bool VSync = true;
        public static int FPS = 300;

        public static bool ESP_2DBox = true;
        public static bool ESP_3DBox = false;
        public static bool ESP_2DLine = true;
        public static bool ESP_Bone = false;
        public static bool ESP_2DHealthBar = true;
        public static bool ESP_HealthText = false;
        public static bool ESP_NameText = false;
        public static bool ESP_Player = true;
        public static bool ESP_NPC = true;
        public static bool ESP_Crosshair = true;

        public static bool AimBot_Enabled = false;
        public static int AimBot_BoneIndex = 0;
        public static float AimBot_Fov = 250.0f;
        public static WinVK AimBot_Key = WinVK.CONTROL;

        public static bool NoTOPMostHide = false;

        public static void Reset()
        {
            VSync = true;
            FPS = 300;

            ESP_2DBox = true;
            ESP_3DBox = false;
            ESP_2DLine = true;
            ESP_Bone = false;
            ESP_2DHealthBar = true;
            ESP_HealthText = false;
            ESP_NameText = false;
            ESP_Player = true;
            ESP_NPC = true;
            ESP_Crosshair = true;

            AimBot_Enabled = false;
            AimBot_BoneIndex = 0;
            AimBot_Fov = 250.0f;
            AimBot_Key = WinVK.CONTROL;

            NoTOPMostHide = false;
        }
    }
}
