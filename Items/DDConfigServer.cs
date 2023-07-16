using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace DoxxarsRainbowMagic.Items;

[Label("$Mods.DDmod.Config.ConfigServer")]
internal class DDConfigServer : ModConfig
{
    public static DDConfigServer Instance;

    [Header("$Mods.DDmod.Config.ConfigServer")]
    [DefaultValue(false)]
    [Label("$Mods.DDmod.Config.ForceMechanism")]
    public bool ForceMechanism;

    [DefaultValue(true)]
    [Label("$Mods.DDmod.Config.Staffdamage")]
    public bool Staffdamage;

    [DefaultValue(true)]
    [Label("$Mods.DDmod.Config.BossAnimation")]
    public bool BossAnimation;

    [DefaultValue(false)]
    [Label("$Mods.DDmod.Config.Somersault")]
    public bool Somersault;

    [DefaultValue(false)]
    [Label("$Mods.DDmod.Config.Swim")]
    public bool Swim;

    public override ConfigScope Mode => ConfigScope.ServerSide;
}
