namespace GTA5OnlineTools.Features.SDK;

public static class Outfits
{
    // Outfit Editor Globals from VenomKY
    private const int oWardrobeG = 2359296;
    private const int oWPointA = 5567;
    private const int oWPointB = 681;
    private const int oWComponent = 1335;
    private const int oWComponentTex = 1609;
    private const int oWProp = 1883;
    private const int oWPropTex = 2094;

    /// <summary>
    /// 范围0~19
    /// </summary>
    public static int OutfitIndex = 0;

    /// <summary>
    /// 通过索引获取服装名字
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public static string GetOutfitNameByIndex()
    {
        return Hacks.ReadGAString(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 1126 - (OutfitIndex * 5));
    }

    /// <summary>
    /// 通过索引设置服装名字
    /// </summary>
    /// <param name="index"></param>
    /// <param name="name"></param>
    public static void SetOutfitNameByIndex(string name)
    {
        Hacks.WriteGAString(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 1126 - (OutfitIndex * 5), name);
    }

    //////////////////////// TOP ////////////////////////

    public static int TOP
    {
        get => Hacks.ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 14);
        set => Hacks.WriteGA(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 14, value);
    }

    public static int TOP_TEX
    {
        get => Hacks.ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 14);
        set => Hacks.WriteGA(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 14, value);
    }

    //////////////////////// UNDERSHIRT ////////////////////////

    public static int UNDERSHIRT
    {
        get => Hacks.ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 11);
        set => Hacks.WriteGA(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 11, value);
    }

    public static int UNDERSHIRT_TEX
    {
        get => Hacks.ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 11);
        set => Hacks.WriteGA(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 11, value);
    }

    //////////////////////// LEGS ////////////////////////

    public static int LEGS
    {
        get => Hacks.ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 7);
        set => Hacks.WriteGA(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 7, value);
    }

    public static int LEGS_TEX
    {
        get => Hacks.ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 7);
        set => Hacks.WriteGA(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 7, value);
    }

    //////////////////////// FEET ////////////////////////

    public static int FEET
    {
        get => Hacks.ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 9);
        set => Hacks.WriteGA(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 9, value);
    }

    public static int FEET_TEX
    {
        get => Hacks.ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 9);
        set => Hacks.WriteGA(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 9, value);
    }

    //////////////////////// ACCESSORIES ////////////////////////

    public static int ACCESSORIES
    {
        get => Hacks.ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 10);
        set => Hacks.WriteGA(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 10, value);
    }

    public static int ACCESSORIES_TEX
    {
        get => Hacks.ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 10);
        set => Hacks.WriteGA(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 10, value);
    }

    //////////////////////// BAGS ////////////////////////

    public static int BAGS
    {
        get => Hacks.ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 8);
        set => Hacks.WriteGA(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 8, value);
    }

    public static int BAGS_TEX
    {
        get => Hacks.ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 8);
        set => Hacks.WriteGA(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 8, value);
    }

    //////////////////////// GLOVES ////////////////////////

    public static int GLOVES
    {
        get => Hacks.ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 6);
        set => Hacks.WriteGA(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 6, value);
    }

    public static int GLOVES_TEX
    {
        get => Hacks.ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 6);
        set => Hacks.WriteGA(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 6, value);
    }

    //////////////////////// DECALS ////////////////////////

    public static int DECALS
    {
        get => Hacks.ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 13);
        set => Hacks.WriteGA(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 13, value);
    }

    public static int DECALS_TEX
    {
        get => Hacks.ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 13);
        set => Hacks.WriteGA(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 13, value);
    }

    //////////////////////// MASK ////////////////////////

    public static int MASK
    {
        get => Hacks.ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 4);
        set => Hacks.WriteGA(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 4, value);
    }

    public static int MASK_TEX
    {
        get => Hacks.ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 4);
        set => Hacks.WriteGA(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 4, value);
    }

    //////////////////////// ARMOR ////////////////////////

    public static int ARMOR
    {
        get => Hacks.ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 12);
        set => Hacks.WriteGA(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponent + (OutfitIndex * 13) + 12, value);
    }

    public static int ARMOR_TEX
    {
        get => Hacks.ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 12);
        set => Hacks.WriteGA(oWardrobeG + (0 * oWPointA) + oWPointB + oWComponentTex + (OutfitIndex * 13) + 12, value);
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //////////////////////// HATS ////////////////////////

    public static int HATS
    {
        get => Hacks.ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWProp + (OutfitIndex * 10) + 3);
        set => Hacks.WriteGA(oWardrobeG + (0 * oWPointA) + oWPointB + oWProp + (OutfitIndex * 10) + 3, value);
    }

    public static int HATS_TEX
    {
        get => Hacks.ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWPropTex + (OutfitIndex * 10) + 3);
        set => Hacks.WriteGA(oWardrobeG + (0 * oWPointA) + oWPointB + oWPropTex + (OutfitIndex * 10) + 3, value);
    }

    //////////////////////// GLASSES ////////////////////////

    public static int GLASSES
    {
        get => Hacks.ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWProp + (OutfitIndex * 10) + 4);
        set => Hacks.WriteGA(oWardrobeG + (0 * oWPointA) + oWPointB + oWProp + (OutfitIndex * 10) + 4, value);
    }

    public static int GLASSES_TEX
    {
        get => Hacks.ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWPropTex + (OutfitIndex * 10) + 4);
        set => Hacks.WriteGA(oWardrobeG + (0 * oWPointA) + oWPointB + oWPropTex + (OutfitIndex * 10) + 4, value);
    }

    //////////////////////// EARS ////////////////////////

    public static int EARS
    {
        get => Hacks.ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWProp + (OutfitIndex * 10) + 5);
        set => Hacks.WriteGA(oWardrobeG + (0 * oWPointA) + oWPointB + oWProp + (OutfitIndex * 10) + 5, value);
    }

    public static int EARS_TEX
    {
        get => Hacks.ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWPropTex + (OutfitIndex * 10) + 5);
        set => Hacks.WriteGA(oWardrobeG + (0 * oWPointA) + oWPointB + oWPropTex + (OutfitIndex * 10) + 5, value);
    }

    //////////////////////// WATCHES ////////////////////////

    public static int WATCHES
    {
        get => Hacks.ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWProp + (OutfitIndex * 10) + 9);
        set => Hacks.WriteGA(oWardrobeG + (0 * oWPointA) + oWPointB + oWProp + (OutfitIndex * 10) + 9, value);
    }

    public static int WATCHES_TEX
    {
        get => Hacks.ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWPropTex + (OutfitIndex * 10) + 9);
        set => Hacks.WriteGA(oWardrobeG + (0 * oWPointA) + oWPointB + oWPropTex + (OutfitIndex * 10) + 9, value);
    }

    //////////////////////// WRIST ////////////////////////

    public static int WRIST
    {
        get => Hacks.ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWProp + (OutfitIndex * 10) + 10);
        set => Hacks.WriteGA(oWardrobeG + (0 * oWPointA) + oWPointB + oWProp + (OutfitIndex * 10) + 10, value);
    }

    public static int WRIST_TEX
    {
        get => Hacks.ReadGA<int>(oWardrobeG + (0 * oWPointA) + oWPointB + oWPropTex + (OutfitIndex * 10) + 10);
        set => Hacks.WriteGA(oWardrobeG + (0 * oWPointA) + oWPointB + oWPropTex + (OutfitIndex * 10) + 10, value);
    }
}
