using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace DoxxarsRainbowMagic.Items;

public class DDTextures
{
	public static Asset<Texture2D> GlowTrail;

	public static Asset<Texture2D> EnergyShieldLight;

	public static Asset<Texture2D> MiniVoidStar;

	public static Asset<Texture2D> VoidStar;

	public static Asset<Texture2D> VoidStarPure;

	public static Asset<Texture2D> Starlight;

	public static Asset<Texture2D> Scanning;

	public static Asset<Texture2D> Wire;

	public static Asset<Texture2D> Nullpng;

	public static Asset<Texture2D> WhitePng;

	public static Asset<Texture2D>[] Circle = new Asset<Texture2D>[8];

	public static Asset<Texture2D> Round;

	public static Asset<Texture2D> CircularProgressBar;

	public static Asset<Texture2D> Bullet;

	public static Asset<Texture2D> Perlin;

	public static Asset<Texture2D> 远古背景;

	public static Asset<Texture2D> 远古背景2;

	public static Asset<Texture2D> Nav_Prev;

	public static Asset<Texture2D> Shield;

	public static Asset<Texture2D> ShieldValue;

	public static Asset<Texture2D> Wave;

	public static Asset<Texture2D> Items_65;

	public static void LoadTextures()
	{
		if (Main.dedServ)
		{
			return;
		}
		GlowTrail = ModContent.Request<Texture2D>("DoxxarsRainbowMagic/Image/GlowTrail");
		EnergyShieldLight = ModContent.Request<Texture2D>("DoxxarsRainbowMagic/Image/EnergyShieldLight");
		MiniVoidStar = ModContent.Request<Texture2D>("DoxxarsRainbowMagic/Image/MiniVoidStar");
		远古背景 = ModContent.Request<Texture2D>("DoxxarsRainbowMagic/Image/远古背景");
		远古背景2 = ModContent.Request<Texture2D>("DoxxarsRainbowMagic/Image/远古背景2");
		VoidStar = ModContent.Request<Texture2D>("DoxxarsRainbowMagic/Image/VoidStar");
		VoidStarPure = ModContent.Request<Texture2D>("DoxxarsRainbowMagic/Image/VoidStarPure");
		Starlight = ModContent.Request<Texture2D>("DoxxarsRainbowMagic/Image/Starlight");
		Scanning = ModContent.Request<Texture2D>("DoxxarsRainbowMagic/Image/Scanning");
		Wire = ModContent.Request<Texture2D>("DoxxarsRainbowMagic/Image/Wire");
		Nullpng = ModContent.Request<Texture2D>("DoxxarsRainbowMagic/Image/Nullpng");
		WhitePng = ModContent.Request<Texture2D>("DoxxarsRainbowMagic/Image/WhitePng");
		Shield = ModContent.Request<Texture2D>("DoxxarsRainbowMagic/Image/Shield");
		ShieldValue = ModContent.Request<Texture2D>("DoxxarsRainbowMagic/Image/ShieldValue");
		for (int i = 0; i < 8; i++)
		{
			if (i == 0 || i == 1)
			{
				Circle[i] = ModContent.Request<Texture2D>("DoxxarsRainbowMagic/Image/Circle");
			}
			else
			{
				Circle[i] = ModContent.Request<Texture2D>("DoxxarsRainbowMagic/Image/Circle" + i);
			}
		}
		Round = ModContent.Request<Texture2D>("DoxxarsRainbowMagic/Image/Round");
		CircularProgressBar = ModContent.Request<Texture2D>("DoxxarsRainbowMagic/Image/CircularProgressBar");
		Bullet = ModContent.Request<Texture2D>("DoxxarsRainbowMagic/Image/Bullet");
		Perlin = ModContent.Request<Texture2D>("DoxxarsRainbowMagic/Image/Perlin");
		Wave = ModContent.Request<Texture2D>("DoxxarsRainbowMagic/Image/Wave");
	}

	public static void UnloadTextures()
	{
		if (!Main.dedServ)
		{
			GlowTrail = null;
			EnergyShieldLight = null;
			MiniVoidStar = null;
			VoidStar = null;
			VoidStarPure = null;
			Starlight = null;
			Scanning = null;
			Wire = null;
			Nullpng = null;
			WhitePng = null;
			Shield = null;
			ShieldValue = null;
			for (int i = 0; i < 8; i++)
			{
				Circle[i] = null;
			}
			Round = null;
			CircularProgressBar = null;
			Bullet = null;
			Perlin = null;
			远古背景 = null;
			远古背景2 = null;
			Wave = null;
		}
	}
}
