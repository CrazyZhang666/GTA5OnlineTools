namespace GTA5OnlineTools.Features.SDK;

public static class Offsets
{
    public static class Mask
    {
        public const string WorldMask = "48 8B 05 ?? ?? ?? ?? 45 ?? ?? ?? ?? 48 8B 48 08 48 85 C9 74 07";
        public const string BlipMask = "4C 8D 05 ?? ?? ?? ?? 0F B7 C1";
        public const string GlobalMask = "4C 8D 05 ?? ?? ?? ?? 4D 8B 08 4D 85 C9 74 11";

        public const string ReplayInterfaceMask = "48 8D 0D ?? ?? ?? ?? 48 8B D7 E8 ?? ?? ?? ?? 48 8D 0D ?? ?? ?? ?? 8A D8 E8";
        public const string NetworkPlayerMgrMask = "48 8B 0D ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8B C8 E8 ?? ?? ?? ?? 48 8B CF";

        public const string ViewPortMask = "48 8B 15 ?? ?? ?? ?? 48 8D 2D ?? ?? ?? ?? 48 8B CD";
        public const string CCameraMask = "48 8B 05 ?? ?? ?? ?? 4A 8B 1C F0";
        public const string AimingPedMask = "48 8B 0D ?? ?? ?? ?? 48 85 C9 74 0C 48 8D 15 ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 89 1D";

        public const string WeatherMask = "48 83 EC ?? 8B 05 ?? ?? ?? ?? 8B 3D ?? ?? ?? ?? 49";

        public const string FriendListMask = "48 8B 0D ?? ?? ?? ?? 8B C6 48 8D 5C 24 70";
        public const string PickupDataMask = "48 8B 05 ?? ?? ?? ?? 48 8B 1C F8 8B";
        public const string UnkModelMask = "4C 8B 15 ?? ?? ?? ?? 49 8B 04 D2 44 39 40 08";

        public const string LocalScriptsMask = "48 8B 05 ?? ?? ?? ?? 8B CF 48 8B 0C C8 39 59 68";

        public const string UnkMask = "48 39 3D ?? ?? ?? ?? 75 2D";
    }

    ////////////////////////////////////////////////////////////////////

    public const int InitSessionState = 1574589;
    public const int InitSessionCache = 1574589 + 2;
    public const int InitSessionType = 1575017;

    ////////////////////////////////////////////////////////////////////

    public const int oMaxPeds = 256;

    // Vehicle Menus Globals
    public const int oVMCreate = 2694560;       // Create any vehicle.
    public const int oVMYCar = 2793044;         // Get my car.
    public const int oVGETIn = 2639783;         // Spawn into vehicle.
    public const int oVMSlots = 1586468;        // Get vehicle slots.

    // Some Player / Network times associated Globals
    public const int oNETTimeHelp = 2672505;
    public const int oPlayerIDHelp = 2657589;
    public const int oPlayerGA = 2672505;

    public const int SpawnPersonalVehicleIndex1 = oVMYCar + 965;
    public const int SpawnPersonalVehicleIndex2 = oVMYCar + 962;

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////

    // CPedFactory (WorldPTR)
    public const int CPed = 0x08;

    // CPed Offsets
    public const int CPed_EntityType = 0x2B;                // int 156:Player 152:Other
    public const int CPed_Invisible = 0x2C;                 // byte 0x01:on 0x24:off
    public const int CPed_CNavigation = 0x30;
    public const int CPed_VisualX = 0x90;                   // float, vector3
    public const int CPed_VisualY = 0x94;
    public const int CPed_VisualZ = 0x98;
    public const int CPed_Proof = 0x188;
    public const int CPed_God = 0x189;                      // int8 0:false 1:true
    public const int CPed_Hostility = 0x18C;
    public const int CPed_Health = 0x280;                   // float
    public const int CPed_HealthMax = 0x284;
    public const int CPed_CVehicle = 0xD10;
    public const int CPed_InVehicle = 0xE32;                // int 0:false 1:true
    public const int CPed_Ragdoll = 0x1098;
    public const int CPed_CPlayerInfo = 0x10A8;
    public const int CPed_CPedInventory = 0x10B0;
    public const int CPed_CPedWeaponManager = 0x10B8;
    public const int CPed_Seatbelt = 0x143C;                // byte 55:false 56:true
    public const int CPed_Armor = 0x150C;                   // float, 50:Online 100:Story Mode

    // CPed CNavigation Offsets
    public const int CPed_CNavigation_RightX = 0x20;        // float, vector3
    public const int CPed_CNavigation_RightY = 0x24;
    public const int CPed_CNavigation_RightZ = 0x28;
    public const int CPed_CNavigation_ForwardX = 0x30;      // float, vector3
    public const int CPed_CNavigation_ForwardY = 0x34;
    public const int CPed_CNavigation_ForwardZ = 0x38;
    public const int CPed_CNavigation_UpX = 0x40;           // float, vector3
    public const int CPed_CNavigation_UpY = 0x44;
    public const int CPed_CNavigation_UpZ = 0x48;
    public const int CPed_CNavigation_PositionX = 0x50;     // float, vector3
    public const int CPed_CNavigation_PositionY = 0x54;
    public const int CPed_CNavigation_PositionZ = 0x58;

    // CPed CVehicle Offsets
    public const int CPed_CVehicle_CModelInfo = 0x20;
    public const int CPed_CVehicle_Invisible = 0x2C;        // byte 0x01:on 0x27:off
    public const int CPed_CVehicle_CNavigation = 0x30;
    public const int CPed_CVehicle_VisualX = 0x90;          // float, vector3
    public const int CPed_CVehicle_VisualY = 0x94;
    public const int CPed_CVehicle_VisualZ = 0x98;
    public const int CPed_CVehicle_State = 0xD8;            // int 0:Player 1:NPC 2:Unused 3:Destroyed
    public const int CPed_CVehicle_God = 0x189;             // int8 0:false 1:true
    public const int CPed_CVehicle_Health = 0x280;          // float
    public const int CPed_CVehicle_HealthMax = 0x284;
    public const int CPed_CVehicle_HealthBody = 0x820;
    public const int CPed_CVehicle_HealthPetrolTank = 0x824;
    public const int CPed_CVehicle_HealthEngine = 0x8E8;
    public const int CPed_CVehicle_Passenger = 0xC42;       // byte 载具座位人数

    // CPed CVehicle CModelInfo Offsets
    public const int CPed_CVehicle_CModelInfo_ModelHash = 0x18;         // int
    public const int CPed_CVehicle_CModelInfo_CamDist = 0x38;           // float
    public const int CPed_CVehicle_CModelInfo_Name = 0x298;             // string [10]
    public const int CPed_CVehicle_CModelInfo_Maker = 0x2A4;            // string [10]
    public const int CPed_CVehicle_CModelInfo_Extras = 0x58B;           // short
    public const int CPed_CVehicle_CModelInfo_Parachute = 0x58C;

    // CPed CVehicle CNavigation Offsets
    public const int CPed_CVehicle_CNavigation_PositionX = 0x50;        // float, vector3
    public const int CPed_CVehicle_CNavigation_PositionY = 0x54;
    public const int CPed_CVehicle_CNavigation_PositionZ = 0x58;

    // CPed CPlayerInfo Offsets
    public const int CPed_CPlayerInfo_RlGamerInfo = 0x20;
    public const int CPed_CPlayerInfo_RockstarId = 0x28;                    // int64
    public const int CPed_CPlayerInfo_RelayIP = 0x64;                       // byte
    public const int CPed_CPlayerInfo_RelayPort = 0x68;                     // short
    public const int CPed_CPlayerInfo_ExternalIP = 0x6C;
    public const int CPed_CPlayerInfo_ExternalPort = 0x70;
    public const int CPed_CPlayerInfo_InternalIP = 0x74;
    public const int CPed_CPlayerInfo_InternalPort = 0x78;
    public const int CPed_CPlayerInfo_Name = 0xA4;                          // string[20]
    public const int CPed_CPlayerInfo_HostToken = 0x80;                     // int64
    public const int CPed_CPlayerInfo_RockstarID = 0x90;
    public const int CPed_CPlayerInfo_SwimSpeed = 0x170;                    // float
    public const int CPed_CPlayerInfo_WaterProof = 0x188;
    public const int CPed_CPlayerInfo_WalkSpeed = 0x18C;
    public const int CPed_CPlayerInfo_GameState = 0x1D8;                    // byte
    public const int CPed_CPlayerInfo_CPed = 0x1E8;
    public const int CPed_CPlayerInfo_FrameFlags = 0x219;
    public const int CPed_CPlayerInfo_WantedCanChange = 0x73C;              // float
    public const int CPed_CPlayerInfo_NPCIgnore = 0x870;                    // int32
    public const int CPed_CPlayerInfo_WantedLevel = 0x888;                  // int32
    public const int CPed_CPlayerInfo_WantedLevelDisplay = 0x88C;           // int32
    public const int CPed_CPlayerInfo_RunSpeed = 0xCF0;                     // float
    public const int CPed_CPlayerInfo_Stamina = 0xCF4;                      // float
    public const int CPed_CPlayerInfo_StaminaRegen = 0xCF8;                 // float
    public const int CPed_CPlayerInfo_WeaponDamageMult = 0xD0C;             // float
    public const int CPed_CPlayerInfo_WeaponDefenceMult = 0xD10;
    public const int CPed_CPlayerInfo_MeleeWeaponDamageMult = 0xD18;
    public const int CPed_CPlayerInfo_MeleeDamageMult = 0xD1C;
    public const int CPed_CPlayerInfo_MeleeDefenceMult = 0xD20;
    public const int CPed_CPlayerInfo_MeleeWeaponDefenceMult = 0xD2C;

    // CPed CPedInventory Offsets
    public const int CPed_CPedInventory_AmmoModifier = 0x78;

    // CPed CPedWeaponManager Offsets
    public const int CPed_CPedWeaponManager_CWeaponInfo = 0x20;

    // CPed CPedWeaponManager CWeaponInfo Offsets
    public const int CPed_CPedWeaponManager_CWeaponInfo_ImpactType = 0x20;          // byte
    public const int CPed_CPedWeaponManager_CWeaponInfo_ImpactExplosion = 0x24;
    public const int CPed_CPedWeaponManager_CWeaponInfo_CAmmoInfo = 0x60;
    public const int CPed_CPedWeaponManager_CWeaponInfo_Spread = 0x7C;              // float
    public const int CPed_CPedWeaponManager_CWeaponInfo_ReloadMult = 0x134;
    public const int CPed_CPedWeaponManager_CWeaponInfo_LockRange = 0x288;
    public const int CPed_CPedWeaponManager_CWeaponInfo_Range = 0x28C;
    public const int CPed_CPedWeaponManager_CWeaponInfo_Recoil = 0x2F4;

    // CReplayInterface Offsets
    public const int CReplayInterface_CVehicleInterface = 0x10;
    public const int CReplayInterface_CPedInterface = 0x18;
    public const int CReplayInterface_CPickupInterface = 0x20;
    public const int CReplayInterface_CObjectInterface = 0x28;

    // CReplayInterface CVehicleInterface Offsets
    public const int CReplayInterface_CVehicleInterface_CVehicleList = 0x180;
    public const int CReplayInterface_CVehicleInterface_MaxVehicles = 0x188;        // int32
    public const int CReplayInterface_CVehicleInterface_CurVehicles = 0x190;

    // CReplayInterface CPedInterface Offsets
    public const int CReplayInterface_CPedInterface_CPedList = 0x100;
    public const int CReplayInterface_CPedInterface_MaxPeds = 0x108;                // int32
    public const int CReplayInterface_CPedInterface_CurPeds = 0x110;

    // CNetworkPlayerMgr Offsets
    public const int CNetworkPlayerMgr_CNetGamePlayerLocal = 0xE8;
    public const int CNetworkPlayerMgr_CNetGamePlayer = 0x180;
    public const int CNetworkPlayerMgr_PlayerLimit = 0x280;
    public const int CNetworkPlayerMgr_PlayerCount = 0x28C;

    // CNetworkPlayerMgr CNetGamePlayer Offsets
    public const int CNetworkPlayerMgr_CNetGamePlayer_IsSpectating = 0x0C;
    public const int CNetworkPlayerMgr_CNetGamePlayer_CPlayerInfo = 0xA0;
    public const int CNetworkPlayerMgr_CNetGamePlayer_ClanName = 0xE6;
    public const int CNetworkPlayerMgr_CNetGamePlayer_ClanTag = 0xFF;
    public const int CNetworkPlayerMgr_CNetGamePlayer_IsRockStarDev = 0x199;
    public const int CNetworkPlayerMgr_CNetGamePlayer_IsRockStarQA = 0x19A;
    public const int CNetworkPlayerMgr_CNetGamePlayer_IsCheater = 0x19B;

}
