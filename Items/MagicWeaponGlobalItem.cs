using System.Collections.Generic;
using System.IO;
//using DDmod.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI.Chat;
using Terraria.Utilities;

namespace DoxxarsRainbowMagic.Items;

public class MagicWeaponGlobalItem : GlobalItem
{
	public bool MagicWeapon;

	public MagicWeaponTypes MagicWeaponType;

	public int Level;

	public int MagicWeaponCD;

	public int MagicWeaponTimes;

	public override bool InstancePerEntity => true;

	public override void SetDefaults(Item item)
	{
	}

	public void SetMagicWeaponDefaults(Item item, MagicWeaponTypes types, int CD, int Times)
	{
		item.accessory = true;
		MagicWeapon = true;
		MagicWeaponType = types;
		MagicWeaponCD = CD;
		MagicWeaponTimes = Times;
	}

	public void MagicWeaponSpawning(Item item, Player player)
	{
		player.MPlayer().MagicWeaponCD = MagicWeaponCD;
		player.MPlayer().MagicWeaponTimes = MagicWeaponTimes;
		int num = 0;
		for (int i = 0; i < 1000; i++)
		{
			Projectile projectile = Main.projectile[i];
			if (projectile.active && projectile.owner == player.whoAmI && projectile.Mproj().MagicWeapon)
			{
				num++;
			}
		}
		if (num < 1)
		{
			int num2 = Projectile.NewProjectile(player.GetSource_FromAI(), player.Center + new Vector2(1000 * player.direction, -1000f), Vector2.Zero, item.shoot, item.damage, item.knockBack, player.whoAmI);
			Main.projectile[num2].CritChance = item.crit;
			Main.projectile[num2].Mproj().MaxMagicWeaponCD = MagicWeaponCD;
			Main.projectile[num2].Mproj().MaxMagicWeaponTimes = MagicWeaponTimes;
		}
	}

	public override bool? PrefixChance(Item item, int pre, UnifiedRandom rand)
	{
		if (MagicWeapon && Main.InReforgeMenu)
		{
			return false;
		}
		return base.PrefixChance(item, pre, rand);
	}

	public override int ChoosePrefix(Item item, UnifiedRandom rand)
	{
		if (MagicWeapon)
		{
			return ModContent.PrefixType<NullPrefix>();
		}
		return base.ChoosePrefix(item, rand);
	}

	public override bool CanEquipAccessory(Item item, Player player, int slot, bool modded)
	{
		if (MagicWeapon)
		{
			if (modded)
			{
				return slot == AccessorySystem.MagicWeaponSlots;
			}
			return false;
		}
		return base.CanEquipAccessory(item, player, slot, modded);
	}

	public override GlobalItem NewInstance(Item target)
	{
		return base.NewInstance(target);
	}

	public override void LoadData(Item item, TagCompound tag)
	{
		Level = tag.Get<int>("Level");
		if (MagicWeapon)
		{
			item.damage = item.OriginalDamage * (Level + 1);
		}
	}

	public override void SaveData(Item item, TagCompound tag)
	{
		tag["Level"] = Level;
	}

	public override void NetSend(Item item, BinaryWriter writer)
	{
		writer.Write(Level);
	}

	public override void NetReceive(Item item, BinaryReader reader)
	{
		Level = reader.ReadInt32();
		if (MagicWeapon)
		{
			item.damage = item.OriginalDamage * (Level + 1);
		}
	}

	public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
	{
		if (!MagicWeapon)
		{
			return;
		}
		tooltips.Add(new TooltipLine(Mod, "CD", Language.GetTextValue("Mods.DDmod.ItemTooltip.MagicWeaponCD") + ": " + MagicWeaponCD / 60 + Language.GetTextValue("Mods.DDmod.ItemTooltip.Second")));
		tooltips.Add(new TooltipLine(Mod, "Times", Language.GetTextValue("Mods.DDmod.ItemTooltip.MagicWeaponTimes") + ": " + MagicWeaponTimes / 60 + Language.GetTextValue("Mods.DDmod.ItemTooltip.Second")));
		foreach (TooltipLine tooltip in tooltips)
		{
			if (tooltip.Mod == "Terraria" && tooltip.Name == "Equipable")
			{
				tooltip.Text = Language.GetTextValue("Mods.DDmod.ItemTooltip.MagicWeapon");
			}
			if (tooltip.Mod == "Terraria" && tooltip.Name == "Damage")
			{
				if (MagicWeaponType == MagicWeaponTypes.Damage)
				{
					tooltip.Text = item.damage + " " + Language.GetTextValue("Mods.DDmod.ItemTooltip.Damage");
				}
				if (MagicWeaponType == MagicWeaponTypes.Restoration)
				{
					tooltip.Text = item.damage + " " + Language.GetTextValue("Mods.DDmod.ItemTooltip.Restoration");
				}
				if (MagicWeaponType == MagicWeaponTypes.Resist)
				{
					tooltip.Text = item.damage + " " + Language.GetTextValue("Mods.DDmod.ItemTooltip.Resist");
				}
			}
			if (tooltip.Mod == "Terraria" && tooltip.Name == "CritChance")
			{
				if (MagicWeaponType == MagicWeaponTypes.Restoration)
				{
					tooltip.Text = item.crit + "% " + Language.GetTextValue("Mods.DDmod.ItemTooltip.RestorationCrit");
				}
				if (MagicWeaponType == MagicWeaponTypes.Resist)
				{
					tooltip.Text = item.crit + "% " + Language.GetTextValue("Mods.DDmod.ItemTooltip.ResistCrit");
				}
			}
		}
		int level = Level;
		if (level == 0)
		{
			tooltips.Add(new TooltipLine(Mod, "普通", "普通"));
		}
		if (level == 1)
		{
			tooltips.Add(new TooltipLine(Mod, "优秀", "优秀"));
		}
	}

	public override bool PreDrawTooltipLine(Item item, DrawableTooltipLine line, ref int yOffset)
	{
		if (line.Name == "普通" && line.Mod == "DDmod")
		{
			TextShader(1f, ModContent.Request<Texture2D>("DDmod/Content/Items/MagicWeapon/Excellent"), Color.White, new Vector2(Main.GlobalTimeWrappedHourly * 20f, 0f));
			ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, line.Text, new Vector2(line.X, line.Y), Color.White, line.Rotation, line.Origin, line.BaseScale, line.MaxWidth, line.Spread);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);
			ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, line.Text, new Vector2(line.X, line.Y), Color.White * 0.75f, line.Rotation, line.Origin, line.BaseScale, line.MaxWidth, line.Spread);
			return false;
		}
		if (line.Name == "优秀" && line.Mod == "DDmod")
		{
			TextShader(1f, ModContent.Request<Texture2D>("DDmod/Content/Items/MagicWeapon/Excellent"), Color.White, new Vector2(Main.GlobalTimeWrappedHourly * 20f, 0f));
			ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, line.Text, new Vector2(line.X, line.Y), Color.White, line.Rotation, line.Origin, line.BaseScale, line.MaxWidth, line.Spread);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);
			ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, line.Text, new Vector2(line.X, line.Y), Color.White * 0.75f, line.Rotation, line.Origin, line.BaseScale, line.MaxWidth, line.Spread);
			return false;
		}
		return true;
	}

	public void TextShader(float Opacity, Asset<Texture2D> asset, Color color, Vector2 vector)
	{
		Main.spriteBatch.End();
		Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);
		GameShaders.Misc["渲染滤镜"].UseOpacity(Opacity);
		GameShaders.Misc["渲染滤镜"].SetShaderTexture(asset);
		GameShaders.Misc["渲染滤镜"].Shader.Parameters["uImageSize1"].SetValue(asset.Size() / 10f);
		GameShaders.Misc["渲染滤镜"].Shader.Parameters["renderTargetArea"].SetValue(new Vector2(asset.Width(), asset.Height()) / 5f);
		GameShaders.Misc["渲染滤镜"].Shader.Parameters["position"].SetValue(vector);
		GameShaders.Misc["渲染滤镜"].Shader.Parameters["ImageSize"].SetValue(asset.Size() / 10f);
		GameShaders.Misc["渲染滤镜"].Shader.Parameters["upscaleFactor"].SetValue(new Vector2(-0.7f));
		GameShaders.Misc["渲染滤镜"].Apply();
	}
}
