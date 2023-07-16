//using DDmod.Content.Projectiles.Magic;
//using DDmod.Content.Projectiles.Magic.Book;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoxxarsRainbowMagic.Items;

public class MagicGlobalItem : GlobalItem
{
	public override bool InstancePerEntity => true;

	public override void SetDefaults(Item item)
	{
        if (false) { }
        //else if (item.type == 3069)
        //{
        //	item.useStyle = ItemUseStyleID.Rapier;
        //	item.shoot = ModContent.ProjectileType<WandofSparking>();
        //	item.noUseGraphic = true;
        //	item.channel = true;
        //	item.autoReuse = true;
        //}
        //else if (item.type == 739)
        //{
        //	item.useStyle = ItemUseStyleID.Rapier;
        //	item.shoot = ModContent.ProjectileType<AmethystStaff>();
        //	item.noUseGraphic = true;
        //	item.channel = true;
        //	item.autoReuse = true;
        //}
        //else if (item.type == 740)
        //{
        //	item.useStyle = ItemUseStyleID.Rapier;
        //	item.shoot = ModContent.ProjectileType<TopazStaff>();
        //	item.noUseGraphic = true;
        //	item.channel = true;
        //	item.autoReuse = true;
        //}
        //else if (item.type == 741)
        //{
        //	item.useStyle = ItemUseStyleID.Rapier;
        //	item.shoot = ModContent.ProjectileType<SapphireStaff>();
        //	item.noUseGraphic = true;
        //	item.channel = true;
        //	item.autoReuse = true;
        //}
        //else if (item.type == 742)
        //{
        //	item.useStyle = ItemUseStyleID.Rapier;
        //	item.shoot = ModContent.ProjectileType<EmeraldStaff>();
        //	item.noUseGraphic = true;
        //	item.channel = true;
        //	item.autoReuse = true;
        //}
        //else if (item.type == 743)
        //{
        //	item.useStyle = ItemUseStyleID.Rapier;
        //	item.shoot = ModContent.ProjectileType<RubyStaff>();
        //	item.noUseGraphic = true;
        //	item.channel = true;
        //	item.autoReuse = true;
        //}
        else if (item.type == ItemID.DiamondStaff)
		{
			item.useStyle = ItemUseStyleID.Rapier;
			item.shoot = ModContent.ProjectileType<DiamondStaff>();
			item.noUseGraphic = true;
			item.channel = true;
			item.autoReuse = true;
		}
        //else if (item.type == 3377)
        //{
        //	item.useStyle = ItemUseStyleID.Rapier;
        //	item.shoot = ModContent.ProjectileType<AmberStaff>();
        //	item.noUseGraphic = true;
        //	item.channel = true;
        //	item.autoReuse = true;
        //}
        //else if (item.type == 64)
        //{
        //	item.useStyle = ItemUseStyleID.Rapier;
        //	item.shoot = ModContent.ProjectileType<Vilethorn>();
        //	item.noUseGraphic = true;
        //	item.channel = true;
        //	item.autoReuse = true;
        //	item.mana = 3;
        //}
        //else if (item.type == 165)
        //{
        //	item.damage = 30;
        //	item.useStyle = ItemUseStyleID.Rapier;
        //	item.shoot = ModContent.ProjectileType<WaterBolt>();
        //	item.noUseGraphic = true;
        //	item.channel = true;
        //	item.autoReuse = true;
        //}
        //else if (item.type == ItemID.DemonScythe)
        //{
        //	item.useStyle = ItemUseStyleID.Rapier;
        //	item.shoot = ModContent.ProjectileType<DemonScythe>();
        //	item.noUseGraphic = true;
        //	item.channel = true;
        //	item.autoReuse = true;
        //}
        else if (item.type == ItemID.BookofSkulls)
		{
			item.useStyle = ItemUseStyleID.Rapier;
			item.shoot = ModContent.ProjectileType<BookofSkulls>();
			item.noUseGraphic = true;
			item.channel = true;
			item.autoReuse = true;
		}
		//else if (item.type == 519)
		//{
		//	item.useStyle = ItemUseStyleID.Rapier;
		//	item.shoot = ModContent.ProjectileType<CursedFlames>();
		//	item.noUseGraphic = true;
		//	item.channel = true;
		//	item.autoReuse = true;
		//}
		//else if (item.type == 518)
		//{
		//	item.damage -= 15;
		//	item.useStyle = ItemUseStyleID.Rapier;
		//	item.shoot = ModContent.ProjectileType<CrystalStorm>();
		//	item.noUseGraphic = true;
		//	item.channel = true;
		//	item.autoReuse = true;
		//}
		//else if (item.type == 1336)
		//{
		//	item.useStyle = ItemUseStyleID.Rapier;
		//	item.shoot = ModContent.ProjectileType<GoldenShower>();
		//	item.noUseGraphic = true;
		//	item.channel = true;
		//	item.autoReuse = true;
		//}
		//else if (item.type == 1266)
		//{
		//	item.useStyle = ItemUseStyleID.Rapier;
		//	item.shoot = ModContent.ProjectileType<MagnetSphere>();
		//	item.noUseGraphic = true;
		//	item.channel = true;
		//	item.autoReuse = true;
		//}
		//else if (item.type == 2622)
		//{
		//	item.useStyle = ItemUseStyleID.Rapier;
		//	item.shoot = ModContent.ProjectileType<RazorbladeTyphoon>();
		//	item.noUseGraphic = true;
		//	item.channel = true;
		//	item.autoReuse = true;
		//}
		//else if (item.type == 3570)
		//{
		//	item.useStyle = ItemUseStyleID.Rapier;
		//	item.shoot = ModContent.ProjectileType<LunarFlareBook>();
		//	item.noUseGraphic = true;
		//	item.channel = true;
		//	item.autoReuse = true;
		//}
		//else if (item.type == 112)
		//{
		//	item.useStyle = ItemUseStyleID.Rapier;
		//	item.shoot = ModContent.ProjectileType<FlowerofFire>();
		//	item.noUseGraphic = true;
		//	item.channel = true;
		//	item.autoReuse = true;
		//}
		//else if (item.type == 517)
		//{
		//	item.damage = (int)((float)item.damage * 2.25f);
		//	item.useStyle = ItemUseStyleID.Rapier;
		//	item.shoot = ModContent.ProjectileType<MagicDagger>();
		//	item.noUseGraphic = true;
		//	item.channel = true;
		//	item.autoReuse = true;
		//}
		//else if (item.type == 127)
		//{
		//	item.shoot = ModContent.ProjectileType<GreenLaser>();
		//}
	}
}
