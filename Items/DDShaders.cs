using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace DoxxarsRainbowMagic.Items;

public class DDShaders
{
	public static Effect NormalSmear;

	public static Effect 贴图拖尾;

	public static Effect 硬边缘拖尾;

	public static Effect 静态拖尾;

	public static Effect 刀光;

	public static Effect 纯色刀光;

	public static Effect 火焰;

	public static Effect 扭曲;

	public static Effect 压缩;

	public static Effect Distort;

	public static Effect 环性进度条;

	public static Effect 能量盾;

	public static Effect 测试能量盾;

	public static Effect 渲染滤镜;

	public static void LoadShaders()
	{
		if (!Main.dedServ)
		{
			Ref<Effect> pixelShaderRef = Main.PixelShaderRef;
			NormalSmear = (Effect)ModContent.Request<Effect>("DoxxarsRainbowMagic/Effects/NormalSmear", AssetRequestMode.ImmediateLoad);
			GameShaders.Misc["NormalSmear"] = new MiscShaderData(new Ref<Effect>(NormalSmear), "TrailPass");
			贴图拖尾 = (Effect)ModContent.Request<Effect>("DoxxarsRainbowMagic/Effects/贴图拖尾", AssetRequestMode.ImmediateLoad);
			GameShaders.Misc["贴图拖尾"] = new MiscShaderData(new Ref<Effect>(贴图拖尾), "TrailPass");
			硬边缘拖尾 = (Effect)ModContent.Request<Effect>("DoxxarsRainbowMagic/Effects/硬边缘拖尾", AssetRequestMode.ImmediateLoad);
			GameShaders.Misc["硬边缘拖尾"] = new MiscShaderData(new Ref<Effect>(硬边缘拖尾), "TrailPass");
			静态拖尾 = (Effect)ModContent.Request<Effect>("DoxxarsRainbowMagic/Effects/静态拖尾", AssetRequestMode.ImmediateLoad);
			GameShaders.Misc["静态拖尾"] = new MiscShaderData(new Ref<Effect>(静态拖尾), "TrailPass");
			刀光 = (Effect)ModContent.Request<Effect>("DoxxarsRainbowMagic/Effects/刀光", AssetRequestMode.ImmediateLoad);
			GameShaders.Misc["刀光"] = new MiscShaderData(new Ref<Effect>(刀光), "TrailPass");
			纯色刀光 = (Effect)ModContent.Request<Effect>("DoxxarsRainbowMagic/Effects/纯色刀光", AssetRequestMode.ImmediateLoad);
			GameShaders.Misc["纯色刀光"] = new MiscShaderData(new Ref<Effect>(纯色刀光), "TrailPass");
			环性进度条 = (Effect)ModContent.Request<Effect>("DoxxarsRainbowMagic/Effects/环性进度条", AssetRequestMode.ImmediateLoad);
			GameShaders.Misc["环性进度条"] = new MiscShaderData(new Ref<Effect>(环性进度条), "Pass0");
			能量盾 = (Effect)ModContent.Request<Effect>("DoxxarsRainbowMagic/Effects/能量盾", AssetRequestMode.ImmediateLoad);
			GameShaders.Misc["能量盾"] = new MiscShaderData(new Ref<Effect>(能量盾), "Pass0");
			测试能量盾 = (Effect)ModContent.Request<Effect>("DoxxarsRainbowMagic/Effects/测试能量盾", AssetRequestMode.ImmediateLoad);
			GameShaders.Misc["测试能量盾"] = new MiscShaderData(new Ref<Effect>(测试能量盾), "Pass0");
			渲染滤镜 = (Effect)ModContent.Request<Effect>("DoxxarsRainbowMagic/Effects/渲染滤镜", AssetRequestMode.ImmediateLoad);
			GameShaders.Misc["渲染滤镜"] = new MiscShaderData(new Ref<Effect>(渲染滤镜), "Pass0");
			Distort = (Effect)ModContent.Request<Effect>("DoxxarsRainbowMagic/Effects/DistortEffect", AssetRequestMode.ImmediateLoad);
			火焰 = (Effect)ModContent.Request<Effect>("DoxxarsRainbowMagic/Effects/火焰", AssetRequestMode.ImmediateLoad);
			GameShaders.Misc["火焰"] = new MiscShaderData(new Ref<Effect>(火焰), "TrailPass");
			扭曲 = (Effect)ModContent.Request<Effect>("DoxxarsRainbowMagic/Effects/扭曲", AssetRequestMode.ImmediateLoad);
			压缩 = (Effect)ModContent.Request<Effect>("DoxxarsRainbowMagic/Effects/压缩", AssetRequestMode.ImmediateLoad);
			GameShaders.Misc["压缩"] = new MiscShaderData(new Ref<Effect>(压缩), "ShieldPass");
			GameShaders.Misc["ForceField2"] = new MiscShaderData(pixelShaderRef, "ForceField");
		}
	}
}
