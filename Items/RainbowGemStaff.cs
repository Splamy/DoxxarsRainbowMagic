using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoxxarsRainbowMagic.Items
{
	public class RainbowGemStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Rainbow Gem Staff"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			//Tooltip.SetDefault("These gems are truly truly truly outrageous!");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(3, 21));
        }


		public override void SetDefaults()
		{
			Item.damage = 25;
			Item.DamageType = DamageClass.Magic;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 23;
			Item.useAnimation = 23;
			Item.useStyle = ItemUseStyleID.Rapier;
            Item.knockBack = 6;
			Item.value = 0;
			Item.rare = -12;
			Item.UseSound = SoundID.Item43;
			Item.autoReuse = true;
			Item.shootSpeed = 13f;
			Item.noMelee = true;
			Item.mana = 10;
            Item.shoot = Mod.Find<ModProjectile>("RainbowGemProjectile").Type;

        }

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 offset = new Vector2(velocity.X * 5, velocity.Y * 5);
			position += offset;
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            return false;
        }


		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.AmethystStaff);
			recipe.AddIngredient(ItemID.TopazStaff);
			recipe.AddIngredient(ItemID.SapphireStaff);
			recipe.AddIngredient(ItemID.EmeraldStaff);
			recipe.AddIngredient(ItemID.RubyStaff);
			recipe.AddIngredient(ItemID.DiamondStaff);
			recipe.AddIngredient(ItemID.AmberStaff);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}

		public override bool AltFunctionUse(Player player)//You use this to allow the item to be right clicked
		{
			return true;
		}

        /*public override void HoldItem(Player player)
        {
            player.itemRotation = 0f;
            player.itemLocation.Y = player.Center.Y;
            player.itemLocation.X = player.Center.X - 18 * player.direction;      
		}*/





    }
}

