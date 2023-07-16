using DDmod.DrawPlayer;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace DoxxarsRainbowMagic.Items;

public class MagicWeaponPlayer : ModPlayer
{
    public bool GhostFireLantern;

    public bool BoneNecklace;

    public bool WoodSpiritSword;

    public bool EnchantedVoodooDoll;

    public bool LegendaryGel;

    public bool DeadLeavesSpirit;

    public bool HunyuanPearlUmbrella;

    public int Shield;

    public int MaxShield;

    public int MagicWeaponCD;

    public int MagicWeaponTimes;

    public override void ResetEffects()
    {
        GhostFireLantern = false;
        BoneNecklace = false;
        EnchantedVoodooDoll = false;
        WoodSpiritSword = false;
        LegendaryGel = false;
        DeadLeavesSpirit = false;
        HunyuanPearlUmbrella = false;
    }

    public override void OnHurt(Player.HurtInfo info)
    {
        if (Shield > 0)
        {
            var maxReduce = Math.Min(Shield, info.Damage);
            Shield -= maxReduce;
            Player.immune = true;
            Player.immuneTime = 60;
            Player.immuneNoBlink = true;
            Player.GetModPlayer<DrawDDPlayer>().ShieldHit = 5f;

            if (Shield > 0)
            {
                FreeDodge(info);
                return;
            }
        }
        base.OnHurt(info);
    }
}
