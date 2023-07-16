using System.IO;
//using DDmod.Helper;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DoxxarsRainbowMagic.Items;

public class MagicWeaponGlobalProjectile : GlobalProjectile
{
	public bool MagicWeapon;

	public int MagicWeaponCD;

	public int MagicWeaponTimes;

	public int MaxMagicWeaponCD;

	public int MaxMagicWeaponTimes;

	public int MagicWeaponLife;

	public int MaxMagicWeaponLife;

	public override bool InstancePerEntity => true;

	public override bool PreAI(Projectile projectile)
	{
		MaxMagicWeaponCD = projectile.Player().MPlayer().MagicWeaponCD;
		MaxMagicWeaponTimes = projectile.Player().MPlayer().MagicWeaponTimes;
		return base.PreAI(projectile);
	}

	public override void ReceiveExtraAI(Projectile projectile, BitReader bitReader, BinaryReader binaryReader)
	{
		MagicWeaponCD = binaryReader.ReadInt32();
		MagicWeaponTimes = binaryReader.ReadInt32();
		MaxMagicWeaponCD = binaryReader.ReadInt32();
		MaxMagicWeaponTimes = binaryReader.ReadInt32();
		MagicWeaponLife = binaryReader.ReadInt32();
		MaxMagicWeaponLife = binaryReader.ReadInt32();
	}

	public override void SendExtraAI(Projectile projectile, BitWriter bitWriter, BinaryWriter binaryWriter)
	{
		binaryWriter.Write(MagicWeaponCD);
		binaryWriter.Write(MagicWeaponTimes);
		binaryWriter.Write(MaxMagicWeaponCD);
		binaryWriter.Write(MaxMagicWeaponTimes);
		binaryWriter.Write(MagicWeaponLife);
		binaryWriter.Write(MaxMagicWeaponLife);
	}
}
