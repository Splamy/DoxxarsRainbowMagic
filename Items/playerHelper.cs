using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;

namespace DoxxarsRainbowMagic.Items;

internal static class playerHelper
{
    internal static readonly FieldInfo UImageFieldMisc = typeof(MiscShaderData).GetField("_uImage1", BindingFlags.Instance | BindingFlags.NonPublic);

    public static void CalculatePerspectiveMatricies(out Matrix viewMatrix, out Matrix projectionMatrix)
    {
        Vector2 zoom = Main.GameViewMatrix.Zoom;
        Matrix matrix = Matrix.CreateScale(zoom.X, zoom.Y, 1f);
        int width = Main.instance.GraphicsDevice.Viewport.Width;
        int height = Main.instance.GraphicsDevice.Viewport.Height;
        viewMatrix = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.Up);
        viewMatrix *= Matrix.CreateTranslation(0f, 0f - height, 0f);
        viewMatrix *= Matrix.CreateRotationZ((float)Math.PI);
        if (Main.LocalPlayer.gravDir == -1f)
        {
            viewMatrix *= Matrix.CreateScale(1f, -1f, 1f) * Matrix.CreateTranslation(0f, height, 0f);
        }
        viewMatrix *= matrix;
        projectionMatrix = Matrix.CreateOrthographicOffCenter(0f, width * zoom.X, 0f, height * zoom.Y, 0f, 1f) * matrix;
    }

    public static MiscShaderData SetShaderTexture(this MiscShaderData shader, Asset<Texture2D> texture)
    {
        UImageFieldMisc.SetValue(shader, texture);
        return shader;
    }

    public static Color MulticolorLerp(float increment, params Color[] colors)
    {
        increment %= 0.999f;
        int num = (int)(increment * colors.Length);
        Color value = colors[num];
        Color value2 = colors[(num + 1) % colors.Length];
        return Color.Lerp(value, value2, increment * colors.Length % 1f);
    }
}
