//using DoxxarsRainbowMagic.Items.DDmod.Helper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace DoxxarsRainbowMagic.Items;

public class WingSlot : PlayerDrawLayer
{
    public override Position GetDefaultPosition()
    {
        return new BeforeParent(PlayerDrawLayers.Wings);
    }

    public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
    {
        Player drawPlayer = drawInfo.drawPlayer;
        AccessoryPlayer accessoryPlayer = drawPlayer.AccPlayer();
        if (drawInfo.shadow == 0f && !drawPlayer.dead)
        {
            return accessoryPlayer.wingslot > 0;
        }
        return false;
    }

    protected override void Draw(ref PlayerDrawSet drawinfo)
    {
        AccessoryPlayer accessoryPlayer = drawinfo.drawPlayer.AccPlayer();
        if (drawinfo.drawPlayer.wings == accessoryPlayer.wingslot)
        {
            Color white = Color.White;
            Vector2 vector = new Vector2(0f, 6f);
            Texture2D value = TextureAssets.Wings[drawinfo.drawPlayer.wings].Value;
            Vector2 vec = drawinfo.Position + drawinfo.drawPlayer.Size / 2f - Main.screenPosition + vector * drawinfo.drawPlayer.Directions - Vector2.UnitX * drawinfo.drawPlayer.direction * 14f;
            Rectangle rectangle = value.Frame(1, 6, 0, drawinfo.drawPlayer.AccPlayer().wingFrame);
            rectangle.Width -= 2;
            rectangle.Height -= 2;
            DrawData item = new DrawData(value, vec.Floor(), rectangle, white, drawinfo.drawPlayer.bodyRotation, rectangle.Size() / 2f, 1f, drawinfo.playerEffect, 0);
            item.shader = drawinfo.cWings;
            drawinfo.DrawDataCache.Add(item);
        }
    }
}
