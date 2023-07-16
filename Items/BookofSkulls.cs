using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoxxarsRainbowMagic.Items;

public class BookofSkulls : ModProjectile
{
    public static Asset<Texture2D> Glow;

    public override void Load()
    {
        // DoxxarsRainbowMagic/Items/BookofSkulls_Glow.png
        Glow = ModContent.Request<Texture2D>(GlowTexture);
    }

    public override void SetDefaults()
    {
        Projectile.width = 40;
        Projectile.height = 40;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.tileCollide = false;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.ignoreWater = true;
        Projectile.usesIDStaticNPCImmunity = true;
        Projectile.idStaticNPCHitCooldown = 2;
        Projectile.coldDamage = true;
        Main.projFrames[Projectile.type] = 8;
    }

    public override bool PreAI()
    {
        Player player = Projectile.Player();
        Projectile.HoldProj(player, Projectile.DPoroj().Times[1], 0f, new Vector2(player.direction, 0f), 0f, 0f, Kill: true, Projectile.DPoroj().Times[4] >= 1f ? 1 : 0, direction: false);
        Projectile.HoldBook(player.ItemMana(), 50f, 15f, 0.1f, 3f);
        int type = 6;
        if (Projectile.DPoroj().Bool[1])
        {
            Dust obj = Main.dust[Dust.NewDust(Projectile.position + new Vector2(4f, 10f), 30, 1, type, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 100)];
            obj.noGravity = true;
            obj.scale = 1f;
            obj.velocity = new Vector2(0f, 0f - Main.rand.NextFloat(2f, 6f));
        }
        if (Projectile.ai[0] >= player.HeldItem.useAnimation && Projectile.DPoroj().Bool[1])
        {
            Vector2 vector = Projectile.velocity.PerfectNormalize();
            if (Main.myPlayer == Projectile.owner)
            {
                int num = player.ItemMana();
                player.statMana -= num;
                Vector2 vector2 = Main.MouseWorld - Projectile.Center;
                if (vector2.X > 0f)
                {
                    player.ChangeDir(1);
                }
                else
                {
                    player.ChangeDir(-1);
                }
                Projectile.netUpdate = true;
                vector = vector2.PerfectNormalize();
            }
            for (float num2 = 0f; num2 < Projectile.scale; num2 += 0.01f)
            {
                Dust obj2 = Main.dust[Dust.NewDust(Projectile.Center, 1, 1, type, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 100)];
                obj2.noGravity = true;
                obj2.scale = 1f;
                obj2.velocity = Main.rand.NextVector2Unit() * (0f - Main.rand.NextFloat(3f, 3.5f));
            }
            _ = Main.projectile[DProj.NewProjectileChange(Projectile.GetSource_FromAI(), Projectile.Center, vector * 3f, 837, Projectile.damage, Projectile.knockBack, Projectile.owner)];
            SoundStyle style = SoundID.Item8;
            SoundEngine.PlaySound(in style, Projectile.position);
            Projectile.ai[0] -= player.HeldItem.useAnimation;
            Projectile.netUpdate = true;
        }
        return false;
    }

    public override void Kill(int timeLeft)
    {
    }

    public override bool? CanDamage()
    {
        return false;
    }

    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D value = DDTextures.VoidStar.Value;
        Projectile.DPoroj().Times[0] += 0.1f;
        Texture2D value2 = DDTextures.Circle[4].Value;
        Texture2D value3 = Glow.Value;
        Color color = new Color(253, 62, 3, 0);
        for (int i = 0; i < 2; i++)
        {
            Vector2 position = Projectile.Center - Main.screenPosition + new Vector2(-2f, 7f);
            Main.spriteBatch.Draw(value, position, null, color * 0.05f, 0f, value.Size() / 2f, new Vector2(0.5f, 0.2f) * Projectile.DPoroj().Times[4], SpriteEffects.None, 0f);
            DDHelper.Compression(value2, color, 0f, Projectile.Opacity, new Vector2(2f, 6f), 1f, Projectile.DPoroj().Times[0], BlendState.Additive);
            Main.spriteBatch.Draw(value2, position, new Rectangle(0, 0, value2.Width, value2.Height), color, 0f, value2.Size() / 2f, Projectile.DPoroj().Times[4], SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(value2, position, new Rectangle(0, 0, value2.Width, value2.Height), color, 0f, value2.Size() / 2f, Projectile.DPoroj().Times[4], SpriteEffects.None, 0f);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
        }
        value2 = (Texture2D)TextureAssets.Projectile[Projectile.type];
        SpriteEffects effects = Main.player[Projectile.owner].direction != 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        Rectangle? sourceRectangle = new Rectangle(0, value2.Height / Main.projFrames[Projectile.type] * Projectile.frame, value2.Width, value2.Height / Main.projFrames[Projectile.type]);
        Vector2 origin = new Vector2(value2.Width / 2, value2.Height / Main.projFrames[Projectile.type] / 2);
        if (Main.player[Projectile.owner].direction == 1)
        {
            Main.spriteBatch.Draw(value2, Projectile.Center - Main.screenPosition, sourceRectangle, lightColor, Projectile.rotation, origin, new Vector2(Projectile.scale, Projectile.scale / 1.5f) / 1.3f, effects, 0f);
            Main.spriteBatch.Draw(value3, Projectile.Center - Main.screenPosition, sourceRectangle, color, Projectile.rotation, origin, new Vector2(Projectile.scale, Projectile.scale / 1.5f) / 1.3f, effects, 0f);
            Main.spriteBatch.Draw(value3, Projectile.Center - Main.screenPosition, sourceRectangle, color, Projectile.rotation, origin, new Vector2(Projectile.scale, Projectile.scale / 1.5f) / 1.3f, effects, 0f);
        }
        else
        {
            Main.spriteBatch.Draw(value2, Projectile.Center - Main.screenPosition, sourceRectangle, lightColor, Projectile.rotation + (float)Math.PI, origin, new Vector2(Projectile.scale, Projectile.scale / 1.5f) / 1.3f, effects, 0f);
            Main.spriteBatch.Draw(value3, Projectile.Center - Main.screenPosition, sourceRectangle, color, Projectile.rotation + (float)Math.PI, origin, new Vector2(Projectile.scale, Projectile.scale / 1.5f) / 1.3f, effects, 0f);
            Main.spriteBatch.Draw(value3, Projectile.Center - Main.screenPosition, sourceRectangle, color, Projectile.rotation + (float)Math.PI, origin, new Vector2(Projectile.scale, Projectile.scale / 1.5f) / 1.3f, effects, 0f);
        }
        return false;
    }
}
