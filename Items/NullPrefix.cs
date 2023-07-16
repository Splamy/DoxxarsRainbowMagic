using Terraria;
using Terraria.ModLoader;

namespace DoxxarsRainbowMagic.Items;

public class NullPrefix : ModPrefix
{
	public override PrefixCategory Category => PrefixCategory.Accessory;

	public override string Name => " ";

	public override float RollChance(Item item)
	{
		return 0f;
	}

	public override bool CanRoll(Item item)
	{
		return false;
	}

	public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
	{
	}

	public override void ModifyValue(ref float valueMult)
	{
	}

	public override void Apply(Item item)
	{
	}
}
