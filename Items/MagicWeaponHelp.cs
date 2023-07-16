using Terraria;
using Terraria.ModLoader;

namespace DoxxarsRainbowMagic.Items;

public static class MagicWeaponHelp
{
	public static MagicWeaponGlobalProjectile Mproj(this Projectile projectile)
	{
		return projectile.GetGlobalProjectile<MagicWeaponGlobalProjectile>();
	}

	public static MagicWeaponGlobalItem Mitem(this Item item)
	{
		return item.GetGlobalItem<MagicWeaponGlobalItem>();
	}

	public static MagicWeaponPlayer MPlayer(this Player player)
	{
		return player.GetModPlayer<MagicWeaponPlayer>();
	}
}
