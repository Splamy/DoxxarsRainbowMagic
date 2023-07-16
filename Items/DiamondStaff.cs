using System;
using DoxxarsRainbowMagic.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace DoxxarsRainbowMagic.Items;

public class DiamondStaff : ModProjectile
{
    public float T1;

    public float T2 = 0.33f;

    public float T3 = 0.66f;

    public float H2 = 1f;

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
    }

    public override bool PreAI()
    {
        Player player = Main.player[Projectile.owner];
        Projectile.HoldProj(player, 24f, 0f, Vector2.Zero, (float)Math.PI / 4f);
        if (Projectile.ai[1] >= 6f && Projectile.soundDelay == 0)
        {
            Projectile.soundDelay = 30;
            SoundEngine.TryGetActiveSound(SoundEngine.PlaySound(in SoundID.Item29, Projectile.position), out var result);
            result.Sound.Pitch = -0.3f;
            Projectile.localAI[0] += 1f;
        }
        if (player.statMana <= 0 && Main.myPlayer == Projectile.owner)
        {
            Projectile.Kill();
        }
        if (Projectile.ai[1] < 6f)
        {
            if (Projectile.ai[0] > 5f)
            {
                int num = player.ItemMana() / 4;
                if (player.ItemMana() > 0 && num < 1)
                {
                    num = 1;
                }
                player.statMana -= num;
                Projectile.ai[0] -= 5f;
            }
            Projectile.ai[1] += 0.025f * player.GetTotalAttackSpeed(Projectile.DamageType);
        }
        DDHelper.MaxandMinF(ref Projectile.ai[1], 6f, 0f);
        int weaponDamage = player.GetWeaponDamage(player.HeldItem);
        Projectile.damage = (int)(weaponDamage * Projectile.ai[1]);
        return false;
    }

    public override void Kill(int timeLeft)
    {
        Player player = Main.player[Projectile.owner];
        Vector2 vector = Projectile.velocity.PerfectNormalize();
        player.velocity -= vector * 3f * Projectile.ai[1];
        if (!Collision.CanHitLine(player.Center + Projectile.velocity.PerfectNormalize() * 50f, 12, 12, player.Center, player.width / 2, player.height / 2) && ModContent.GetInstance<DDConfigServer>().Staffdamage)
        {
            if (Projectile.ai[1] > 0.5f)
            {
                Projectile obj = Main.projectile[
                    DProj.NewProjectileChange(
                        Projectile.GetSource_FromAI(),
                        Projectile.Center + Projectile.velocity.PerfectNormalize() * Projectile.SolidTileDistanceDetection(100f, 50f),
                        vector * (8f + Projectile.ai[1]),
                        ModContent.ProjectileType<RainbowGemProjectile>(),
                        Projectile.damage / 5,
                        Projectile.knockBack,
                        Projectile.owner,
                        0f,
                        0f,
                        Projectile.ai[1])
                   ];
                obj.scale = Projectile.ai[1];
                obj.hostile = true;
            }
        }
        else
        {
            Projectile obj = Main.projectile[
                DProj.NewProjectileChange(
                    Projectile.GetSource_FromAI(),
                    Projectile.Center + Projectile.velocity.PerfectNormalize() * Projectile.SolidTileDistanceDetection(100f, 50f),
                    vector * (8f + Projectile.ai[1]),
                    ModContent.ProjectileType<RainbowGemProjectile>(),
                    Projectile.damage,
                    Projectile.knockBack, 
                    Projectile.owner,
                    0f, 
                    0f,
                    Projectile.ai[1])
                ];
            obj.scale = Projectile.ai[1];
        }
    }

    public override bool? CanDamage()
    {
        return false;
    }

    public override bool PreDraw(ref Color lightColor)
    {
        T1 += 0.033f;
        T2 += 0.033f;
        T3 += 0.033f;
        if (T1 > 0.99f)
        {
            T1 = 0f;
        }
        if (T2 > 0.99f)
        {
            T2 = 0f;
        }
        if (T3 > 0.99f)
        {
            T3 = 0f;
        }
        Texture2D texture2D = (Texture2D)TextureAssets.Projectile[Projectile.type];
        Main.spriteBatch.Draw(texture2D, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, texture2D.Width, texture2D.Height), lightColor, Projectile.rotation, new Vector2(texture2D.Width / 2, texture2D.Height / 2), Projectile.scale, SpriteEffects.None, 0f);
        Player player = Main.player[Projectile.owner];
        Color color = new Color(155, 155, 155, 0);
        for (int i = 0; i < 3; i++)
        {
            Vector2 vector = (Projectile.velocity.PerfectNormalize() * (Projectile.ai[1] + (30f - T1 * 5f * Projectile.ai[1]))).RotatedBy(0.0);
            Vector2 vector2 = (Projectile.velocity.PerfectNormalize() * (Projectile.ai[1] + (30f - T2 * 5f * Projectile.ai[1]))).RotatedBy(0.0);
            Vector2 vector3 = (Projectile.velocity.PerfectNormalize() * (Projectile.ai[1] + (30f - T3 * 5f * Projectile.ai[1]))).RotatedBy(0.0);
            Main.spriteBatch.Draw(DDTextures.Circle[2].Value, Projectile.Center + vector - Main.screenPosition, null, color * (1f - T1), Projectile.rotation + (float)Math.PI / 4f, DDTextures.Circle[2].Size() / 2f, new Vector2(Projectile.ai[1] / 2f, Projectile.ai[1] / 8f) * T1, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(DDTextures.Circle[2].Value, Projectile.Center + vector2 - Main.screenPosition, null, color * (1f - T2), Projectile.rotation + (float)Math.PI / 4f, DDTextures.Circle[2].Size() / 2f, new Vector2(Projectile.ai[1] / 2f, Projectile.ai[1] / 8f) * T2, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(DDTextures.Circle[2].Value, Projectile.Center + vector3 - Main.screenPosition, null, color * (1f - T3), Projectile.rotation + (float)Math.PI / 4f, DDTextures.Circle[2].Size() / 2f, new Vector2(Projectile.ai[1] / 2f, Projectile.ai[1] / 8f) * T3, SpriteEffects.None, 0f);
        }
        for (int j = 0; j < 5; j++)
        {
            if (Projectile.ai[0] > 5f)
            {
                texture2D = DDTextures.Starlight.Value;
                Main.spriteBatch.Draw(texture2D, Projectile.Center + Projectile.velocity.PerfectNormalize() * 36f - Main.screenPosition, null, color * (1f - T1), Projectile.rotation + (float)Math.PI / 4f, texture2D.Size() / 2f, new Vector2(Projectile.ai[1] / 5f, Projectile.ai[1] / 2f) * T1, SpriteEffects.None, 0f);
            }
        }
        DDHelper.DrawExpanded(Main.GameViewMatrix.TransformationMatrix, Main.spriteBatch, player.Center - Main.screenPosition + new Vector2(0f, -50f), 0.5f, Projectile.ai[1] / 6f * 0.5f, color * 0.3f, color, 1.1f, 0.6f);
        Main.spriteBatch.DrawString(FontAssets.MouseText.Value, (int)(Projectile.ai[1] / 6f * 100f) + "%", player.Center - Main.screenPosition - new Vector2(0f, 38f), color, 0f, ChatManager.GetStringSize(FontAssets.MouseText.Value, (int)(Projectile.ai[1] / 6f * 100f) + "%", Vector2.One) / 2f, 0.75f, SpriteEffects.None, 0f);
        return false;
    }
}
