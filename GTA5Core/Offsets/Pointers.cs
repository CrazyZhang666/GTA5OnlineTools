namespace GTA5Core.Offsets;

public static class Pointers
{
    public static long WorldPTR = 0;
    public static long BlipPTR = 0;
    public static long GlobalPTR = 0;

    public static long ReplayInterfacePTR = 0;
    public static long NetworkPlayerMgrPTR = 0;
    public static long NetworkInfoPTR = 0;

    public static long ViewPortPTR = 0;
    public static long CCameraPTR = 0;
    public static long AimingPedPTR = 0;

    public static long WeatherPTR = 0;
    //public static long TimePTR = 0;

    public static long PickupDataPTR = 0;
    //public static long UnkModelPTR = 0;

    public static long LocalScriptsPTR = 0;

    public static long UnkPTR = 0;

    public static void Reset()
    {
        WorldPTR = 0;
        BlipPTR = 0;
        GlobalPTR = 0;
        NetworkPlayerMgrPTR = 0;
        ReplayInterfacePTR = 0;
        NetworkInfoPTR = 0;
        ViewPortPTR = 0;
        CCameraPTR = 0;
        AimingPedPTR = 0;
        WeatherPTR = 0;
        //TimePTR = 0;
        PickupDataPTR = 0;
        //UnkModelPTR = 0;
        LocalScriptsPTR = 0;
        UnkPTR = 0;
    }
}
