namespace GTA5Core.Offsets;

public struct CNetworkPlayerMgr
{
    public const int CNetGamePlayerLocal = 0xF0;
    public const int CNetGamePlayer = 0x188;
    public const int PlayerLimit = 0x280;
    public const int PlayerCount = 0x28C;
}

public struct CNetGamePlayerLocal               // 0xE8
{

}

public struct CNetGamePlayer                    // 0x180
{
    public const int IsSpectating = 0x0C;
    public const int CPlayerInfo = 0xA8;

    public const int ClanName = 0xD6;           // 0xB8 + 0x1E  char m_clan_name[25]    ClanData
    public const int ClanTag = 0xEF;            // 0xB8 + 0x37  char m_clan_tag[5]
    public const int ClanMotto = 0xF4;          // 0xB8 + 0x3C  char m_clan_motto[65]

    public const int IsRockStarDev = 0x199;
    public const int IsRockStarQA = 0x19A;
    public const int IsCheater = 0x19B;
}
