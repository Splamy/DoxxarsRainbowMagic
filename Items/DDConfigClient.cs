using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace DoxxarsRainbowMagic.Items;

[Label("$Mods.DDmod.Config.ConfigClient")]
internal class DDConfigClient : ModConfig
{
    public static DDConfigClient Instance;

    [Header("$Mods.DDmod.Config.ConfigClient")]
    [DefaultValue(true)]
    [Label("$Mods.DDmod.Config.ItemID")]
    public bool ItemID;

    [DefaultValue(true)]
    [Label("$Mods.DDmod.Config.DustLightEffect")]
    public bool DustLightEffect;

    [DefaultValue(1)]
    [Label("$Mods.DDmod.Config.SlashEffect")]
    public float SlashEffect = 1f;

    public override ConfigScope Mode => ConfigScope.ClientSide;
}
