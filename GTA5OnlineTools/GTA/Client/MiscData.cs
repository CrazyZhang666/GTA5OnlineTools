namespace GTA5OnlineTools.GTA.Client;

public static class MiscData
{
    public class FlagInfo
    {
        public string Name;
        public int ID;
    }

    public static List<FlagInfo> Blips = new()
    {
        new FlagInfo(){ Name="游艇", ID=455 },
        new FlagInfo(){ Name="CEO仓库", ID=473 },
        new FlagInfo(){ Name="CEO办公室", ID=475 },
        new FlagInfo(){ Name="摩托帮会所", ID=492 },
        new FlagInfo(){ Name="大麻种植场", ID=496 },
        new FlagInfo(){ Name="可卡因工厂", ID=497 },
        new FlagInfo(){ Name="伪证件办公室", ID=498 },
        new FlagInfo(){ Name="冰毒实验室", ID=499 },
        new FlagInfo(){ Name="假钞工厂", ID=500 },
        new FlagInfo(){ Name="CEO载具仓库", ID=524 },
        new FlagInfo(){ Name="地堡", ID=557 },
        new FlagInfo(){ Name="机库", ID=569 },
        new FlagInfo(){ Name="设施", ID=590 },
        new FlagInfo(){ Name="夜总会", ID=614 },
        new FlagInfo(){ Name="恐霸", ID=632 },
        new FlagInfo(){ Name="游戏厅", ID=740 },
        new FlagInfo(){ Name="虎鲸", ID=760 },
        new FlagInfo(){ Name="车友会", ID=777 },
        new FlagInfo(){ Name="事务所", ID=826 },
    };

    public static List<FlagInfo> Sessions = new()
    {
        new FlagInfo(){ Name="离开线上模式", ID=-1 },
        new FlagInfo(){ Name="公共战局", ID=0 },
        new FlagInfo(){ Name="创建公共战局", ID=1 },
        new FlagInfo(){ Name="私人帮会战局", ID=2 },
        new FlagInfo(){ Name="帮会战局", ID=3 },
        new FlagInfo(){ Name="加入好友", ID=9 },
        new FlagInfo(){ Name="私人好友战局", ID=6 },
        new FlagInfo(){ Name="单人战局", ID=10 },
        new FlagInfo(){ Name="仅限邀请战局", ID=11 },
        new FlagInfo(){ Name="加入帮会伙伴", ID=12 },
    };

    public static List<FlagInfo> RPxNs = new()
    {
        new FlagInfo(){ Name="RPx1", ID=1 },
        new FlagInfo(){ Name="RPx2", ID=2 },
        new FlagInfo(){ Name="RPx5", ID=5 },
        new FlagInfo(){ Name="RPx10", ID=10 },
        new FlagInfo(){ Name="RPx20", ID=20 },
        new FlagInfo(){ Name="RPx50", ID=50 },
        new FlagInfo(){ Name="RPx100", ID=100 },
        new FlagInfo(){ Name="RPx500", ID=500 },
        new FlagInfo(){ Name="RPx1000", ID=1000 },
    };

    public static List<FlagInfo> REPxNs = new()
    {
        new FlagInfo(){ Name="REPx1", ID=1 },
        new FlagInfo(){ Name="REPx2", ID=2 },
        new FlagInfo(){ Name="REPx5", ID=5 },
        new FlagInfo(){ Name="REPx10", ID=10 },
        new FlagInfo(){ Name="REPx20", ID=20 },
        new FlagInfo(){ Name="REPx50", ID=50 },
        new FlagInfo(){ Name="REPx100", ID=100 },
        new FlagInfo(){ Name="REPx500", ID=500 },
        new FlagInfo(){ Name="REPx1000", ID=1000 },
    };

    // Set Global_1946791 to 1, otherwise you'll see regular crates.
    // Set Global_1946637 to one of these: 2, 4, 6, 7, 8, 9.
    // Now you'll see the unique cargo in your terrorbyte.

    public static List<FlagInfo> CEOCargos = new()
    {
        new FlagInfo(){ Name="医疗用品", ID=0 },
        new FlagInfo(){ Name="烟酒", ID=1 },
        new FlagInfo(){ Name="古董艺术品", ID=2 },
        new FlagInfo(){ Name="电子产品", ID=3 },
        new FlagInfo(){ Name="武器弹药", ID=4 },
        new FlagInfo(){ Name="迷幻药", ID=5 },
        new FlagInfo(){ Name="宝石", ID=6 },
        new FlagInfo(){ Name="动物材料", ID=7 },
        new FlagInfo(){ Name="仿制品", ID=8 },
        new FlagInfo(){ Name="珠宝", ID=9 },
        new FlagInfo(){ Name="银块", ID=10 },
    };

    public static List<FlagInfo> CEOSpecialCargos = new()
    {
        new FlagInfo(){ Name="古董艺术品（华丽彩蛋）", ID=2 },
        new FlagInfo(){ Name="武器弹药（黄金火神机枪）", ID=4 },
        new FlagInfo(){ Name="宝石（一大颗钻石）", ID=6 },
        new FlagInfo(){ Name="动物材料（稀有皮草）", ID=7 },
        new FlagInfo(){ Name="仿制品（电影胶卷）", ID=8 },
        new FlagInfo(){ Name="珠宝（稀有怀表）", ID=9 },
    };

    public static List<FlagInfo> MerryWeatherServices = new()
    {
        new FlagInfo(){ Name="请求弹药空投", ID=886 },
        new FlagInfo(){ Name="请求重型防弹装甲", ID=896 },
        new FlagInfo(){ Name="请求牛鲨睾酮", ID=894 },
        new FlagInfo(){ Name="请求船只接送", ID=887 },
        new FlagInfo(){ Name="请求直升机接送", ID=888 },
    };

    public static List<FlagInfo> LocalWeathers = new()
    {
        new FlagInfo(){ Name="默认", ID=-1 },
        new FlagInfo(){ Name="格外晴朗", ID=0 },
        new FlagInfo(){ Name="晴朗", ID=1 },
        new FlagInfo(){ Name="多云", ID=2 },
        new FlagInfo(){ Name="阴霾", ID=3 },
        new FlagInfo(){ Name="大雾", ID=4 },
        new FlagInfo(){ Name="阴天", ID=5 },
        new FlagInfo(){ Name="下雨", ID=6 },
        new FlagInfo(){ Name="雷雨", ID=7 },
        new FlagInfo(){ Name="雨转晴", ID=8 },
        new FlagInfo(){ Name="阴雨", ID=9 },
        new FlagInfo(){ Name="下雪", ID=10 },
        new FlagInfo(){ Name="暴雪", ID=11 },
        new FlagInfo(){ Name="小雪", ID=12 },
        new FlagInfo(){ Name="圣诞", ID=13 },
        new FlagInfo(){ Name="万圣节", ID=14 },
    };

    public static List<FlagInfo> ImpactExplosions = new()
    {
        new FlagInfo(){ Name="默认", ID=-1 },
        new FlagInfo(){ Name="手榴弹", ID=0 },
        new FlagInfo(){ Name="榴弹发射器", ID=1 },
        new FlagInfo(){ Name="粘弹", ID=2 },
        new FlagInfo(){ Name="燃烧瓶", ID=3 },
        new FlagInfo(){ Name="火箭弹", ID=4 },
        new FlagInfo(){ Name="坦克炮弹", ID=5 },
        new FlagInfo(){ Name="火球爆炸", ID=6 },
        new FlagInfo(){ Name="汽车爆炸", ID=7 },
        new FlagInfo(){ Name="飞机爆炸", ID=8 },
        new FlagInfo(){ Name="加油站爆炸", ID=9 },
        new FlagInfo(){ Name="摩托车爆炸", ID=10 },
        new FlagInfo(){ Name="蒸汽", ID=11 },
        new FlagInfo(){ Name="火焰", ID=12 },
        new FlagInfo(){ Name="消防栓", ID=13 },
        new FlagInfo(){ Name="燃气罐", ID=14 },
        new FlagInfo(){ Name="小艇", ID=15 },
        new FlagInfo(){ Name="船只", ID=16 },
        new FlagInfo(){ Name="卡车爆炸", ID=17 },
        new FlagInfo(){ Name="MKⅡ爆炸子弹", ID=18 },
        new FlagInfo(){ Name="烟雾弹发射器", ID=19 },
        new FlagInfo(){ Name="烟雾弹", ID=20 },
        new FlagInfo(){ Name="毒气弹", ID=21 },
        new FlagInfo(){ Name="信号弹", ID=22 },
        new FlagInfo(){ Name="带特效的爆炸", ID=23 },
        new FlagInfo(){ Name="灭火器", ID=24 },
        new FlagInfo(){ Name="可调榴弹", ID=25 },
        new FlagInfo(){ Name="火车爆炸", ID=26 },
        new FlagInfo(){ Name="油桶", ID=27 },
        new FlagInfo(){ Name="丙烷", ID=28 },
        new FlagInfo(){ Name="飞艇", ID=29 },
        new FlagInfo(){ Name="火焰 ", ID=30 },
        new FlagInfo(){ Name="油罐车", ID=31 },
        new FlagInfo(){ Name="飞机导弹", ID=32 },
        new FlagInfo(){ Name="汽车导弹", ID=33 },
        new FlagInfo(){ Name="燃油泵", ID=34 },
        new FlagInfo(){ Name="鸟屎", ID=35 },
        new FlagInfo(){ Name="电磁步枪", ID=36 },
        new FlagInfo(){ Name="飞艇爆炸", ID=37 },
        new FlagInfo(){ Name="烟花发射器", ID=38 },
        new FlagInfo(){ Name="雪球", ID=39 },
        new FlagInfo(){ Name="地雷", ID=40 },
        new FlagInfo(){ Name="女武神机炮", ID=41 },
        new FlagInfo(){ Name="游艇防御", ID=42 },
        new FlagInfo(){ Name="爆破筒", ID=43 },
        new FlagInfo(){ Name="汽车炸弹", ID=44 },
        new FlagInfo(){ Name="MK2 定向导弹", ID=45 },
        new FlagInfo(){ Name="APC炮弹", ID=46 },
        new FlagInfo(){ Name="飞机集束炸弹", ID=47 },
        new FlagInfo(){ Name="飞机瓦斯", ID=48 },
        new FlagInfo(){ Name="飞机燃烧弹", ID=49 },
        new FlagInfo(){ Name="飞机炸弹", ID=50 },
        new FlagInfo(){ Name="鱼雷", ID=51 },
        new FlagInfo(){ Name="水下鱼雷", ID=52 },
        new FlagInfo(){ Name="邦布须卡炮", ID=53 },
        new FlagInfo(){ Name="第二集束炸弹", ID=54 },
        new FlagInfo(){ Name="猎杀者连发导弹", ID=55 },
        new FlagInfo(){ Name="FH-1 猎杀者 机炮", ID=56 },
        new FlagInfo(){ Name="流氓大炮", ID=57 },
        new FlagInfo(){ Name="水下地雷", ID=58 },
        new FlagInfo(){ Name="天基炮", ID=59 },
        new FlagInfo(){ Name="轨道炮", ID=60 },
        new FlagInfo(){ Name="MK2爆炸子弹霰弹枪", ID=61 },
        new FlagInfo(){ Name="MK2导弹", ID=62 },
        new FlagInfo(){ Name="武装坦帕迫击炮", ID=63 },
        new FlagInfo(){ Name="追踪地雷", ID=64 },
        new FlagInfo(){ Name="电磁脉冲地雷", ID=65 },
        new FlagInfo(){ Name="尖刺式地雷", ID=66 },
        new FlagInfo(){ Name="感应式地雷", ID=67 },
        new FlagInfo(){ Name="定时地雷", ID=68 },
        new FlagInfo(){ Name="无人机自爆", ID=69 },
        new FlagInfo(){ Name="原子能枪", ID=70 },
        new FlagInfo(){ Name="跳雷", ID=71 },
        new FlagInfo(){ Name="脚本化导弹", ID=72 },
        new FlagInfo(){ Name="潜艇导弹", ID=82 },
        new FlagInfo(){ Name="无伤害爆炸", ID=99 },
    };

    public static List<FlagInfo> VehicleExtras = new()
    {
        new FlagInfo(){ Name="默认", ID=0x00 },
        new FlagInfo(){ Name="载具跳跃", ID=0x20 },
        new FlagInfo(){ Name="火箭推进", ID=0x40 },
        new FlagInfo(){ Name="载具降落伞", ID=0x100 },
        new FlagInfo(){ Name="跳台魔宝", ID=0x200 },
        new FlagInfo(){ Name="载具跳跃+火箭推进", ID=0x60 },
        new FlagInfo(){ Name="载具跳跃+载具降落伞", ID=0x120 },
        new FlagInfo(){ Name="火箭推进+载具降落伞", ID=0x140 },
        new FlagInfo(){ Name="载具跳跃+火箭推进+载具降落伞", ID=0x160 },
        new FlagInfo(){ Name="火箭推进+载具降落伞+跳台魔宝", ID=0x340 },
        new FlagInfo(){ Name="载具跳跃+火箭推进+载具降落伞+跳台魔宝", ID=0x360 },
    };
}
