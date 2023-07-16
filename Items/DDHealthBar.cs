using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace DoxxarsRainbowMagic.Items;

[Label("$Mods.DDmod.Config.HealthBar")]
internal class DDHealthBar : ModConfig
{
    public static DDHealthBar Instance;

    [Header("$Mods.DDmod.Config.HealthBar")]
    [DefaultValue(true)]
    [Label("$Mods.DDmod.Config.ShowTarget")]
    public bool ShowTarget;

    [DefaultValue(true)]
    [Label("$Mods.DDmod.Config.ShowPercentage")]
    public bool ShowPercentage;

    [DefaultValue(true)]
    [Label("$Mods.DDmod.Config.ShowLife")]
    public bool ShowLife;

    [DefaultValue(true)]
    [Label("$Mods.DDmod.Config.ShowName")]
    public bool ShowName;

    public override ConfigScope Mode => ConfigScope.ClientSide;
}
