using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace DoxxarsRainbowMagic.Items;

public class MagicStarCircleLayer : PlayerDrawLayer
{
    public override Position GetDefaultPosition()
    {
        return new BeforeParent(PlayerDrawLayers.HandOnAcc);
    }

    public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
    {
        Player drawPlayer = drawInfo.drawPlayer;
        AttributesPlayer attributesPlayer = drawPlayer.Aplayer();
        if (drawInfo.shadow == 0f && !drawPlayer.dead && (attributesPlayer.GuardianOfTheStarCD > 6000 || attributesPlayer.GuardianOfTheStarCD < 0))
        {
            return attributesPlayer.GuardianOfTheStar;
        }
        return false;
    }

    protected override void Draw(ref PlayerDrawSet drawInfo)
    {
        Player drawPlayer = drawInfo.drawPlayer;
        Texture2D value = ModContent.Request<Texture2D>("DoxxarsRainbowMagic/Image/MagicStarCircle").Value;
        Vector2 position = drawPlayer.Center - Main.screenPosition + new Vector2(0f, drawPlayer.gfxOffY);
        Color color = new Color(0, 100, 255, 0);
        Vector2 origin = new Vector2(value.Width / 2f, value.Height / 2f);
        float num = 1f;
        if (drawPlayer.Aplayer().GuardianOfTheStarCD < 0)
        {
            color *= -drawPlayer.Aplayer().GuardianOfTheStarCD / 30f;
            num += 1f - (-drawPlayer.Aplayer().GuardianOfTheStarCD) / 30f;
        }
        SpriteEffects effect = drawPlayer.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        drawInfo.DrawDataCache.Add(new DrawData(value, position, null, color, drawPlayer.Aplayer().GuardianOfTheStarCD / 20f, origin, num, effect, 0));
        drawInfo.DrawDataCache.Add(new DrawData(value, position, null, color, drawPlayer.Aplayer().GuardianOfTheStarCD / 20f, origin, num, effect, 0));
        drawInfo.DrawDataCache.Add(new DrawData(value, position, null, color, drawPlayer.Aplayer().GuardianOfTheStarCD / 20f, origin, num, effect, 0));
        value = DDTextures.VoidStar.Value;
        origin = new Vector2(value.Width / 2f, value.Height / 2f);
        drawInfo.DrawDataCache.Add(new DrawData(value, position, null, color, drawPlayer.Aplayer().GuardianOfTheStarCD / 20f, origin, num, effect, 0));
    }
}
