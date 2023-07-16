using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace DoxxarsRainbowMagic.Items;

public static class DDHelper
{
    public struct CustomVertexInfo : IVertexType
    {
        private static VertexDeclaration _vertexDeclaration = new VertexDeclaration(new VertexElement(0, VertexElementFormat.Vector2, VertexElementUsage.Position, 0), new VertexElement(8, VertexElementFormat.Color, VertexElementUsage.Color, 0), new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.TextureCoordinate, 0));

        public Vector2 Position;

        public Color Color;

        public Vector3 TexCoord;

        public VertexDeclaration VertexDeclaration => _vertexDeclaration;

        public CustomVertexInfo(Vector2 position, Color color, Vector3 texCoord)
        {
            Position = position;
            Color = color;
            TexCoord = texCoord;
        }

        public CustomVertexInfo(Vector2 position, Vector3 texCoord)
        {
            Position = position;
            Color = Color.White;
            TexCoord = texCoord;
        }
    }

    public static Color[] GetColors(this Texture2D texture)
    {
        int width = texture.Width;
        int height = texture.Height;
        Color[] array = new Color[width * height];
        texture.GetData(array);
        return array;
    }

    public static AccessoryPlayer AccPlayer(this Player player)
    {
        return player.GetModPlayer<AccessoryPlayer>();
    }

    /*public static AccessoryGlobalItem AccessoryItem(this Item item)
    {
        return item.GetGlobalItem<AccessoryGlobalItem>();
    }*/

    public static DDPlayer Dplayer(this Player player)
    {
        return player.GetModPlayer<DDPlayer>();
    }

    public static AttributesPlayer Aplayer(this Player player)
    {
        return player.GetModPlayer<AttributesPlayer>();
    }

    public static Item ActiveItem(this Player player)
    {
        if (!Main.mouseItem.IsAir)
        {
            return Main.mouseItem;
        }
        return player.HeldItem;
    }

    public static float IteUseAnimation(this Player player)
    {
        if (!Main.mouseItem.IsAir)
        {
            return Main.mouseItem.useAnimation * player.GetTotalAttackSpeed(DamageClass.Magic);
        }
        return player.HeldItem.useAnimation * player.GetTotalAttackSpeed(DamageClass.Magic);
    }

    public static int MaxandMin(ref int Value, int Max, int Min)
    {
        if (Value > Max)
        {
            Value = Max;
        }
        if (Value < Min)
        {
            Value = Min;
        }
        return Value;
    }

    public static float MaxandMinF(ref float Value, float Max, float Min)
    {
        if (Value > Max)
        {
            Value = Max;
        }
        if (Value < Min)
        {
            Value = Min;
        }
        return Value;
    }

    public static int ItemMana(this Player player)
    {
        if (!Main.mouseItem.IsAir)
        {
            return player.GetManaCost(Main.mouseItem);
        }
        return player.GetManaCost(player.HeldItem);
    }

    public static float ItemUseTime(this Player player)
    {
        if (!Main.mouseItem.IsAir)
        {
            return player.GetWeaponAttackSpeed(Main.mouseItem);
        }
        return player.GetWeaponAttackSpeed(player.HeldItem);
    }

    public static Vector2 PerfectNormalize(this Vector2 vector)
    {
        if (!(vector != Vector2.Zero))
        {
            return Vector2.Zero;
        }
        return Vector2.Normalize(vector);
    }

    public static void DirectPerfectNormalize(this ref Vector2 vector)
    {
        vector = vector.PerfectNormalize();
    }

    public static void RotationSpeed(this Player player, float Rotation, float Speed)
    {
        if (Rotation < 0f)
        {
            Rotation += (float)Math.PI * 2f;
        }
        else if ((double)Rotation > 6.283)
        {
            Rotation -= (float)Math.PI * 2f;
        }
        if (player.fullRotation < Rotation)
        {
            if (Rotation - player.fullRotation > (float)Math.PI)
            {
                player.fullRotation -= Speed;
            }
            else
            {
                player.fullRotation += Speed;
            }
        }
        else if (player.fullRotation > Rotation)
        {
            if (player.fullRotation - Rotation > (float)Math.PI)
            {
                player.fullRotation += Speed;
            }
            else
            {
                player.fullRotation -= Speed;
            }
        }
        if (player.fullRotation < 0f)
        {
            player.fullRotation += (float)Math.PI * 2f;
        }
        else if (player.fullRotation > 6.283)
        {
            player.fullRotation -= (float)Math.PI * 2f;
        }
        if (player.fullRotation > Rotation - Speed && player.fullRotation < Rotation + Speed)
        {
            player.fullRotation = Rotation;
        }
    }

    public static bool SpecifyDirection(float OwnRotation, float SpecifyRotation, float Sector)
    {
        if (SpecifyRotation < 0f)
        {
            SpecifyRotation += (float)Math.PI * 2f;
        }
        else if (SpecifyRotation > (float)Math.PI * 2f)
        {
            SpecifyRotation -= (float)Math.PI * 2f;
        }
        if (OwnRotation < 0f)
        {
            OwnRotation += (float)Math.PI * 2f;
        }
        else if (OwnRotation > (float)Math.PI * 2f)
        {
            OwnRotation -= (float)Math.PI * 2f;
        }
        if (OwnRotation > SpecifyRotation - Sector && OwnRotation < SpecifyRotation + Sector)
        {
            return true;
        }
        return false;
    }

    public static void BackAndForth(float Min, float Max, float Speed, ref float Value, ref bool Bool)
    {
        if (Bool)
        {
            Value += Speed;
        }
        else
        {
            Value -= Speed;
        }
        if (Value >= Max)
        {
            Bool = false;
        }
        if (Value <= Min)
        {
            Bool = true;
        }
        if (Value < Min)
        {
            Value = Min;
        }
        if (Value > Max)
        {
            Value = Max;
        }
    }

    public static void RotateSpeed(ref float OwnRotation, float SpecifyRotation, float Speed)
    {
        if (SpecifyRotation < 0f)
        {
            SpecifyRotation += (float)Math.PI * 2f;
        }
        else if ((double)SpecifyRotation > 6.283)
        {
            SpecifyRotation -= (float)Math.PI * 2f;
        }
        if (OwnRotation < SpecifyRotation)
        {
            if (SpecifyRotation - OwnRotation > (float)Math.PI)
            {
                OwnRotation -= Speed;
            }
            else
            {
                OwnRotation += Speed;
            }
        }
        else if (OwnRotation > SpecifyRotation)
        {
            if (OwnRotation - SpecifyRotation > (float)Math.PI)
            {
                OwnRotation += Speed;
            }
            else
            {
                OwnRotation -= Speed;
            }
        }
        if (OwnRotation < 0f)
        {
            OwnRotation += (float)Math.PI * 2f;
        }
        else if ((double)OwnRotation > 6.283)
        {
            OwnRotation -= (float)Math.PI * 2f;
        }
        if (OwnRotation > SpecifyRotation - Speed && OwnRotation < SpecifyRotation + Speed)
        {
            OwnRotation = SpecifyRotation;
        }
    }

    public static void Compression(Texture2D texture, Color color, float rotation, float opacity, Vector2 Scale, float Direction, float CircularRotation, BlendState blendMode)
    {
        Main.spriteBatch.End();
        Main.spriteBatch.Begin(SpriteSortMode.Immediate, blendMode, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
        playerHelper.CalculatePerspectiveMatricies(out var viewMatrix, out var projectionMatrix);
        GameShaders.Misc["压缩"].UseColor(color);
        GameShaders.Misc["压缩"].UseSaturation(rotation);
        GameShaders.Misc["压缩"].UseOpacity(opacity);
        GameShaders.Misc["压缩"].Shader.Parameters["usc"].SetValue(Scale);
        GameShaders.Misc["压缩"].Shader.Parameters["uDirection"].SetValue(Direction);
        GameShaders.Misc["压缩"].Shader.Parameters["uCircularRotation"].SetValue(CircularRotation);
        GameShaders.Misc["压缩"].Shader.Parameters["uImageSize0"].SetValue(texture.Size());
        GameShaders.Misc["压缩"].Shader.Parameters["overallImageSize"].SetValue(texture.Size());
        GameShaders.Misc["压缩"].Shader.Parameters["uWorldViewProjection"].SetValue(viewMatrix * projectionMatrix);
        GameShaders.Misc["压缩"].Apply();
    }

    public static void DrawExpanded(Matrix matrix, SpriteBatch spriteBatch, Vector2 position, float MaxValue, float Value, Color color, Color color2, float opacity = 1f, float scale = 1f)
    {
        Main.spriteBatch.End();
        Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, matrix);
        Color color3 = color * 0.2f;
        Color secondaryColor = color2 * 0.2f;
        color3.A = byte.MaxValue;
        secondaryColor.A = byte.MaxValue;
        ApplyBarShaders(opacity, color3, secondaryColor, MaxValue);
        spriteBatch.Draw(DDTextures.Circle[3].Value, position, null, Color.White * opacity, -(float)Math.PI / 2f, DDTextures.Circle[3].Size() / 2f, scale, SpriteEffects.FlipHorizontally, 0f);
        ApplyBarShaders(opacity, color, color2, Value);
        spriteBatch.Draw(DDTextures.Circle[3].Value, position, null, Color.White * opacity, -(float)Math.PI / 2f, DDTextures.Circle[3].Size() / 2f, scale, SpriteEffects.FlipHorizontally, 0f);
        Main.spriteBatch.End();
        Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, matrix);
    }

    public static void ApplyBarShaders(float opacity, Color color, Color SecondaryColor, float T)
    {
        GameShaders.Misc["环性进度条"].UseOpacity(opacity);
        GameShaders.Misc["环性进度条"].UseSaturation(T);
        GameShaders.Misc["环性进度条"].UseColor(color);
        GameShaders.Misc["环性进度条"].UseSecondaryColor(SecondaryColor);
        GameShaders.Misc["环性进度条"].Apply();
    }

    public static void ShieldShaders(float opacity, Color color, float Time)
    {
        GameShaders.Misc["能量盾"].UseOpacity(opacity);
        GameShaders.Misc["能量盾"].UseColor(color);
        GameShaders.Misc["能量盾"].Shader.Parameters["uTime2"].SetValue(Time);
        GameShaders.Misc["能量盾"].Apply();
    }

    /*public static void 测试NPC滤镜(float opacity, Color color, float Time)
    {
        GameShaders.Misc["渲染滤镜"].UseOpacity(opacity);
        GameShaders.Misc["渲染滤镜"].SetShaderTexture(DDTextures.远古背景);
        GameShaders.Misc["渲染滤镜"].Shader.Parameters["uImageSize1"].SetValue(DDTextures.远古背景.Size());
        GameShaders.Misc["渲染滤镜"].UseColor(color);
        GameShaders.Misc["渲染滤镜"].Shader.Parameters["renderTargetArea"].SetValue(new Vector2(300f, 2160f));
        GameShaders.Misc["渲染滤镜"].Shader.Parameters["uWorldPosition"].SetValue(Main.screenPosition + new Vector2(0f, Time));
        GameShaders.Misc["渲染滤镜"].Shader.Parameters["upscaleFactor"].SetValue(new Vector2(-0.7f));
        GameShaders.Misc["渲染滤镜"].Apply();
    }*/

    public static float ReadFloat(this BinaryReader w)
    {
        return w.ReadSingle();
    }
}
