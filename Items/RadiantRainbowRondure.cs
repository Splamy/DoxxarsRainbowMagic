using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace DoxxarsRainbowMagic.Items
{
    public class RadiantRainbowRondure : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.useStyle = ItemUseStyleID.HoldUp;
            //Item.useAnimation = 23;
            Item.useTime = 30;
            Item.channel = true;
            Item.noMelee = true;
            Item.damage = 1200;
            Item.knockBack = 4f;
            Item.autoReuse = false;
            Item.useTurn = false;
            Item.rare = 13;
            Item.DamageType = DamageClass.Magic;
            Item.value = Item.sellPrice(2, 0, 0, 0);
            Item.UseSound = SoundID.Item15;
            Item.shoot = Mod.Find<ModProjectile>("RadiantRainbowRay").Type;
            Item.mana = 10;
            Item.shootSpeed = 0f;
        }
    }
}