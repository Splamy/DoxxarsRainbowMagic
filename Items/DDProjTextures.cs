using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace DoxxarsRainbowMagic.Items;

public class DDProjTextures
{
	public static Asset<Texture2D> Proj_20;

	public static Asset<Texture2D> Proj_44;

	public static Asset<Texture2D> Proj_45;

	public static Asset<Texture2D> Proj_83;

	public static Asset<Texture2D> Proj_84;

	public static Asset<Texture2D> Proj_88;

	public static Asset<Texture2D> Proj_100;

	public static Asset<Texture2D> Proj_257;

	public static Asset<Texture2D> Proj_389;

	public static void LoadProjTextures()
	{
		if (!Main.dedServ)
		{
			Proj_20 = TextureAssets.Projectile[20];
			TextureAssets.Projectile[20] = ModContent.Request<Texture2D>("DoxxarsRainbowMagic/Textures/Proj/Proj_20");
			Proj_44 = TextureAssets.Projectile[44];
			TextureAssets.Projectile[44] = ModContent.Request<Texture2D>("DoxxarsRainbowMagic/Textures/Proj/Proj_44");
			Proj_45 = TextureAssets.Projectile[45];
			TextureAssets.Projectile[45] = ModContent.Request<Texture2D>("DoxxarsRainbowMagic/Textures/Proj/Proj_45");
			Proj_83 = TextureAssets.Projectile[83];
			TextureAssets.Projectile[83] = ModContent.Request<Texture2D>("DoxxarsRainbowMagic/Textures/Proj/Proj_83");
			Proj_84 = TextureAssets.Projectile[84];
			TextureAssets.Projectile[84] = ModContent.Request<Texture2D>("DoxxarsRainbowMagic/Textures/Proj/Proj_84");
			Proj_88 = TextureAssets.Projectile[88];
			TextureAssets.Projectile[88] = ModContent.Request<Texture2D>("DoxxarsRainbowMagic/Textures/Proj/Proj_88");
			Proj_100 = TextureAssets.Projectile[100];
			TextureAssets.Projectile[100] = ModContent.Request<Texture2D>("DoxxarsRainbowMagic/Textures/Proj/Proj_100");
			Proj_257 = TextureAssets.Projectile[257];
			TextureAssets.Projectile[257] = ModContent.Request<Texture2D>("DoxxarsRainbowMagic/Textures/Proj/Proj_257");
			Proj_389 = TextureAssets.Projectile[389];
			TextureAssets.Projectile[389] = ModContent.Request<Texture2D>("DoxxarsRainbowMagic/Textures/Proj/Proj_389");
		}
	}

	public static void UnloadProjTextures()
	{
		if (!Main.dedServ)
		{
			TextureAssets.Projectile[20] = Proj_20;
			TextureAssets.Projectile[83] = Proj_83;
			TextureAssets.Projectile[84] = Proj_84;
			TextureAssets.Projectile[88] = Proj_88;
			TextureAssets.Projectile[100] = Proj_100;
			TextureAssets.Projectile[257] = Proj_257;
			TextureAssets.Projectile[389] = Proj_389;
		}
	}
}
