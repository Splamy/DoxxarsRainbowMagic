using System;
using System.IO;
//using DDmod.Content.Buffs.DeBuffs;
//using DDmod.Content.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DoxxarsRainbowMagic.Items;

public class MeleeProjectile : GlobalProjectile
{
    public bool SwordHitbox;

    public int DaggerDashDistance;

    public Vector2[] oldVels;

    public float oldVels2;

    public int DelayedKill;

    public Vector2 oldPlayer;

    public bool ClearInvincibility;

    public override bool InstancePerEntity => true;

    public override void SetDefaults(Projectile projectile)
    {
        if (projectile.aiStyle == 161)
        {
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 12;
        }
        if (projectile.type == 153)
        {
            NPCHit(projectile, 60);
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 50;
        }
        if (projectile.type == 543)
        {
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
        }
        if (projectile.type == 542)
        {
            NPCHit(projectile, 30);
        }
        if (projectile.type == 156)
        {
            NPCHit(projectile, 30);
        }
    }

    public void NPCHit(Projectile projectile, int Hit)
    {
        projectile.usesLocalNPCImmunity = true;
        projectile.localNPCHitCooldown = Hit;
        projectile.usesIDStaticNPCImmunity = false;
    }

    public override void ReceiveExtraAI(Projectile projectile, BitReader bitReader, BinaryReader binaryReader)
    {
    }

    public override void SendExtraAI(Projectile projectile, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
    }

    //public override bool PreAI(Projectile projectile)
    //{
    //    if (oldVels != null && oldVels.Length != 0)
    //    {
    //        for (int num = oldVels.Length - 1; num > 0; num--)
    //        {
    //            oldVels[num] = oldVels[num - 1];
    //            if (projectile.oldPos[num] == Vector2.Zero)
    //            {
    //                oldVels[num] = Vector2.Zero;
    //            }
    //        }
    //        if (oldVels2 != 0f)
    //        {
    //            oldVels[0] = projectile.velocity + projectile.velocity * (oldVels2 * projectile.scale);
    //        }
    //    }
    //    if (projectile.MeleePoroj().DelayedKill > 2)
    //    {
    //        projectile.timeLeft = projectile.MeleePoroj().DelayedKill;
    //        projectile.MeleePoroj().DelayedKill--;
    //        oldVels[0] = Vector2.Zero;
    //        return false;
    //    }
    //    if (projectile.MeleePoroj().DelayedKill >= 1)
    //    {
    //        projectile.Kill();
    //    }
    //    if (projectile.aiStyle == 161)
    //    {
    //        Texture2D texture2D = (Texture2D)TextureAssets.Projectile[projectile.type];
    //        Player player = Main.player[projectile.owner];
    //        Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
    //        if (Main.myPlayer == projectile.owner && projectile.DPoroj().vector[0] == Vector2.Zero)
    //        {
    //            projectile.DPoroj().vector[0] = Main.MouseWorld - vector;
    //        }
    //        Vector2 vector2 = projectile.DPoroj().vector[0].PerfectNormalize().RotatedBy(projectile.ai[1]);
    //        projectile.velocity = vector2 * 2f;
    //        if (projectile.ai[0] == 0f && player.controlUseTile && projectile.localAI[1] == 0f && !player.HasBuff(ModContent.BuffType<SpecialAttackCD>()))
    //        {
    //            player.AddBuff(ModContent.BuffType<SpecialAttackCD>(), 600);
    //            projectile.DPoroj().Bool[0] = true;
    //        }
    //        if (projectile.DPoroj().Bool[0])
    //        {
    //            if (player.velocity.Length() < 30f)
    //            {
    //                float num2 = player.velocity.Y;
    //                if (num2 < 0f)
    //                {
    //                    num2 = 0f - num2;
    //                }
    //                if (num2 == 0f)
    //                {
    //                    projectile.DPoroj().Bool[1] = true;
    //                }
    //                player.velocity += projectile.DPoroj().vector[0].PerfectNormalize() * 3f;
    //            }
    //            else
    //            {
    //                player.velocity = player.velocity.PerfectNormalize() * 30f;
    //            }
    //        }
    //        projectile.ai[0] += 1f;
    //        if (projectile.localAI[0] < 0f)
    //        {
    //            projectile.localAI[0] += 1f;
    //            projectile.ai[0] -= 1f;
    //        }
    //        else if (projectile.localAI[1] == 3f)
    //        {
    //            projectile.ai[0] += 1f;
    //        }
    //        if (projectile.ai[0] == 2f)
    //        {
    //            SoundEngine.PlaySound(in SoundID.Item1, projectile.position);
    //        }
    //        if (projectile.ai[0] > 15 + (projectile.localAI[1] == 3f ? 10 : 0) + (projectile.DPoroj().Bool[0] ? DaggerDashDistance : 0))
    //        {
    //            if (projectile.localAI[1] == 0f)
    //            {
    //                player.noFallDmg = true;
    //                if (projectile.DPoroj().Bool[0])
    //                {
    //                    player.velocity = Vector2.Zero;
    //                }
    //                projectile.DPoroj().Bool[0] = false;
    //                projectile.ai[1] = -0.4f;
    //            }
    //            else if (projectile.localAI[1] == 1f)
    //            {
    //                projectile.ai[1] = 0.4f;
    //            }
    //            else if (projectile.localAI[1] == 2f)
    //            {
    //                projectile.ai[1] = 0f;
    //                projectile.localAI[0] = -24f;
    //            }
    //            projectile.ai[0] = 0f;
    //            projectile.localAI[1] += 1f;
    //        }
    //        if (projectile.ai[0] == 15 + (projectile.localAI[1] == 3f ? 9 : 0) + (projectile.DPoroj().Bool[0] ? DaggerDashDistance : 0))
    //        {
    //            projectile.DPoroj().Bool[2] = true;
    //            projectile.netUpdate = true;
    //        }
    //        player.itemTime = 5;
    //        player.itemAnimation = 5;
    //        if (projectile.localAI[1] >= 4f)
    //        {
    //            projectile.Kill();
    //        }
    //        projectile.rotation = projectile.velocity.ToRotation() + (float)Math.PI / 2f;
    //        float num3 = 0f;
    //        if (player.direction == -1)
    //        {
    //            num3 = 3.14f;
    //        }
    //        float num4 = projectile.velocity.ToRotation();
    //        player.itemRotation = num4 + num3;
    //        float num5 = projectile.ai[0];
    //        if (num5 > texture2D.Width && projectile.localAI[1] != 3f)
    //        {
    //            num5 = texture2D.Width;
    //        }
    //        projectile.Center = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: false, addGfxOffY: false) + projectile.velocity * num5;
    //        projectile.spriteDirection = !(Vector2.Dot(projectile.velocity, Vector2.UnitX) < 0f) ? 1 : -1;
    //        player.heldProj = projectile.whoAmI;
    //        projectile.netUpdate = true;
    //        return false;
    //    }
    //    if (projectile.type == 154)
    //    {
    //        if (projectile.ai[0] == 6f)
    //        {
    //            if (projectile.DPoroj().Times[1] < 0.5f)
    //            {
    //                projectile.DPoroj().Times[1] += 0.025f;
    //            }
    //            else
    //            {
    //                if (projectile.DPoroj().Times[2] > 0f)
    //                {
    //                    projectile.DPoroj().Times[2] -= 1f;
    //                }
    //                for (int i = 0; i < 200; i++)
    //                {
    //                    NPC nPC = Main.npc[i];
    //                    if (nPC.CanBeChasedBy() && Main.rand.NextBool(10) && (nPC.Center - projectile.Center).Length() < 128f && projectile.DPoroj().Times[2] <= 0f && projectile.DPoroj().Times[0] < 10f)
    //                    {
    //                        projectile.DPoroj().Times[2] = 10f;
    //                        Projectile.NewProjectile(projectile.GetSource_FromAI(), nPC.Center, Vector2.Zero, ModContent.ProjectileType<MeleeSuckBloodPlayer>(), projectile.damage, 0f, projectile.owner, projectile.whoAmI, projectile.type);
    //                    }
    //                }
    //            }
    //        }
    //        else if (projectile.DPoroj().Times[1] > 0f)
    //        {
    //            projectile.DPoroj().Times[1] -= 0.5f;
    //        }
    //    }
    //    if (projectile.type == 25)
    //    {
    //        if (projectile.ai[0] == 1f && Main.rand.NextBool(10))
    //        {
    //            Projectile.NewProjectile(projectile.GetSource_FromAI(), projectile.Center, projectile.velocity / 1.5f, ModContent.ProjectileType<CorruptionBall>(), projectile.damage / 2, 0f, projectile.owner);
    //        }
    //        if (projectile.ai[0] == 6f)
    //        {
    //            projectile.DPoroj().Times[1] += 1f;
    //            if (projectile.DPoroj().Times[1] % 30f == 0f)
    //            {
    //                for (int j = -1; j <= 1; j++)
    //                {
    //                    Projectile.NewProjectile(projectile.GetSource_FromAI(), projectile.Center, new Vector2(0f, -5f).RotatedBy(Main.rand.NextFloat(0.9f, 0.1f) * j), ModContent.ProjectileType<Spikes>(), projectile.damage / 2, 0f, projectile.owner);
    //                }
    //            }
    //        }
    //    }
    //    if (projectile.type == 153)
    //    {
    //        Player player2 = Main.player[projectile.owner];
    //        Vector2 vector3 = player2.RotatedRelativePoint(player2.MountedCenter, reverseRotation: true);
    //        if (Main.myPlayer == projectile.owner)
    //        {
    //            if (projectile.DPoroj().vector[0] == Vector2.Zero)
    //            {
    //                projectile.DPoroj().vector[0] = Main.MouseWorld - vector3;
    //            }
    //            if (projectile.DPoroj().Times[0] == 10f)
    //            {
    //                player2.Dplayer().PlayerShake(5, 8f);
    //            }
    //        }
    //        Vector2 vector4 = projectile.DPoroj().vector[0].PerfectNormalize().RotatedBy(projectile.ai[1]);
    //        projectile.velocity = vector4 * 2f;
    //        if (projectile.DPoroj().Times[0] > 0f)
    //        {
    //            projectile.DPoroj().Times[0] -= 1f;
    //            projectile.ai[1] -= 0.04f * player2.direction;
    //            projectile.extraUpdates = 0;
    //        }
    //        else
    //        {
    //            projectile.extraUpdates = 4;
    //        }
    //        if (projectile.localAI[1] == 0f)
    //        {
    //            projectile.ai[0] += 0.7f + player2.GetTotalAttackSpeed(projectile.DamageType) - player2.ActiveItem().useAnimation / 100f;
    //            if (projectile.ai[0] > 55f)
    //            {
    //                projectile.localAI[1] += 1f;
    //                SoundStyle style = SoundID.DD2_DarkMageAttack;
    //                style.Pitch = -0.8f;
    //                SoundEngine.PlaySound(in style, projectile.position);
    //            }
    //        }
    //        else if (projectile.localAI[1] == 1f)
    //        {
    //            projectile.ai[1] += 0.05f * player2.direction;
    //            if (projectile.ai[1] > 6.91150427f || projectile.ai[1] < -6.91150427f || projectile.DPoroj().Times[0] == 1f)
    //            {
    //                projectile.localAI[1] += 1f;
    //            }
    //        }
    //        else
    //        {
    //            projectile.ai[0] -= 1f + player2.GetTotalAttackSpeed(projectile.DamageType) - player2.ActiveItem().useAnimation / 100f;
    //            if (projectile.ai[0] <= 30f)
    //            {
    //                projectile.localAI[1] += 1f;
    //            }
    //        }
    //        player2.itemTime = 5;
    //        player2.itemAnimation = 5;
    //        if (projectile.localAI[1] >= 3f)
    //        {
    //            projectile.Kill();
    //        }
    //        projectile.rotation = projectile.velocity.ToRotation() + (float)Math.PI / 2f;
    //        float num6 = 0f;
    //        if (player2.direction == -1)
    //        {
    //            num6 = 3.14f;
    //        }
    //        float num7 = projectile.velocity.ToRotation();
    //        player2.itemRotation = num7 + num6;
    //        float num8 = projectile.ai[0];
    //        projectile.Center = player2.RotatedRelativePoint(player2.MountedCenter, reverseRotation: false, addGfxOffY: false) + projectile.velocity * num8;
    //        projectile.spriteDirection = !(Vector2.Dot(projectile.velocity, Vector2.UnitX) < 0f) ? 1 : -1;
    //        player2.heldProj = projectile.whoAmI;
    //        projectile.netUpdate = true;
    //        return false;
    //    }
    //    if (projectile.type == 543 && projectile.localAI[1] > 0f)
    //    {
    //        projectile.localAI[1] -= 1f;
    //        projectile.localAI[0] -= 1f;
    //    }
    //    if (projectile.type == 542)
    //    {
    //        projectile.localNPCHitCooldown = 10;
    //        if (!Main.dayTime)
    //        {
    //            projectile.localAI[0] = 0f;
    //            projectile.alpha = 140;
    //            projectile.velocity *= 1.05f;
    //            projectile.tileCollide = false;
    //        }
    //        else
    //        {
    //            projectile.tileCollide = true;
    //            projectile.alpha = 0;
    //        }
    //    }
    //    if (projectile.type == 157)
    //    {
    //        projectile.ProjScaleChange();
    //        Dust obj = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 75, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100)];
    //        obj.noGravity = true;
    //        obj.scale = projectile.scale;
    //        obj.velocity = Main.rand.NextVector2Unit() * Main.rand.NextFloat(2f, 5f);
    //    }
    //    if (projectile.type == 156)
    //    {
    //        projectile.ProjScaleChange();
    //        Dust obj2 = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 73, projectile.oldVelocity.X, projectile.oldVelocity.Y, 255)];
    //        obj2.noGravity = true;
    //        obj2.scale = projectile.scale;
    //        obj2.velocity = Main.rand.NextVector2Unit() * Main.rand.NextFloat(2f, 5f);
    //    }
    //    if (projectile.type == 132)
    //    {
    //        projectile.ProjScaleChange();
    //        if (projectile.DPoroj().vector[0] == Vector2.Zero)
    //        {
    //            projectile.DPoroj().vector[0] = projectile.velocity;
    //        }
    //        DDHelper.BackAndForth(-0.2f, 0.2f, 0.02f, ref projectile.localAI[0], ref projectile.DPoroj().Bool[0]);
    //        projectile.velocity = projectile.DPoroj().vector[0].RotatedBy(projectile.localAI[0]);
    //    }
    //    return base.PreAI(projectile);
    //}

    //public override bool TileCollideStyle(Projectile projectile, ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
    //{
    //    if (projectile.aiStyle == 15 && projectile.owner == Main.myPlayer)
    //    {
    //        fallThrough = Main.MouseWorld.Y - (projectile.position.Y + projectile.height) >= 0f;
    //        if (fallThrough)
    //        {
    //            projectile.netUpdate = true;
    //        }
    //    }
    //    return base.TileCollideStyle(projectile, ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
    //}

    //public override bool? CanDamage(Projectile projectile)
    //{
    //    if (SwordHitbox)
    //    {
    //        _ = Main.player[projectile.owner];
    //        return projectile.ai[1] >= 2f;
    //    }
    //    if (projectile.aiStyle == 161)
    //    {
    //        return projectile.localAI[1] < 3f || projectile.ai[0] >= 6f;
    //    }
    //    if (projectile.MeleePoroj().DelayedKill > 0)
    //    {
    //        return false;
    //    }
    //    return base.CanDamage(projectile);
    //}

//    public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
//    {
//        Player player = Main.player[projectile.owner];
//        if (projectile.aiStyle == 161)
//        {
//            if (projectile.localAI[1] == 0f)
//            {
//                knockback *= 0.2f;
//                if (target.knockBackResist > 0f)
//                {
//                    target.velocity = Vector2.Zero;
//                }
//                projectile.ai[0] = 15 + DaggerDashDistance;
//                if (!projectile.DPoroj().Bool[0])
//                {
//                }
//            }
//            else if (projectile.localAI[1] == 3f)
//            {
//                knockback *= 3f;
//            }
//            else
//            {
//                knockback *= 0.2f;
//            }
//            float num = player.velocity.Length();
//            if (num < 0f)
//            {
//                num = 0f - num;
//            }
//            num += 1f;
//            if (projectile.ai[1] == 0f)
//            {
//                if (projectile.DPoroj().Bool[0])
//                {
//                    if (num > 5f)
//                    {
//                        player.immune = true;
//                        player.immuneTime = 60;
//                        player.immuneNoBlink = true;
//                        player.noFallDmg = true;
//                        target.AddBuff(ModContent.BuffType<Bleed>(), 300);
//                    }
//                    player.velocity = Vector2.Zero;
//                    damage = (int)(damage * num / 10f);
//                }
//            }
//            else if (target.HasBuff(ModContent.BuffType<Bleed>()))
//            {
//                damage = (int)(damage * 1.5f);
//            }
//            if (projectile.localAI[1] == 3f)
//            {
//                damage *= 3;
//            }
//        }
//        if (projectile.type == 153)
//        {
//            knockback *= 2f - (target.Center - player.Center).Length() / 150f;
//            float num2 = Math.Abs(projectile.ai[1]);
//            if (projectile.localAI[1] == 1f && num2 > 5.02654839f)
//            {
//                damage *= 2;
//                projectile.DPoroj().Times[0] = 10f;
//                for (int i = 0; i < 10; i++)
//                {
//                    target.HitEffect(0, 0.0);
//                }
//                target.AddBuff(30, Main.rand.Next(100, 300));
//                SoundStyle style = SoundID.DD2_DarkMageAttack;
//                style.Pitch = 0.8f;
//                SoundEngine.PlaySound(in style, projectile.position);
//            }
//            if (target.HasBuff(30))
//            {
//                damage = (int)(damage * 1.2f);
//            }
//        }
//        if (projectile.type == 543)
//        {
//            if (projectile.localAI[1] > 0f)
//            {
//                knockback = 0f;
//                damage = (int)(damage * 1.5f);
//                projectile.localAI[0] -= 20f;
//            }
//            if (target.HasBuff(30))
//            {
//                damage = (int)(damage * 1.2f);
//            }
//            if (Main.rand.NextBool(10) && !projectile.DPoroj().Bool[0])
//            {
//                projectile.localAI[1] = 300f;
//                projectile.DPoroj().Bool[0] = true;
//                projectile.netUpdate = true;
//            }
//        }
//        if (projectile.type == 154 && target.HasBuff(30))
//        {
//            damage = (int)(damage * 1.2f);
//        }
//    }

//    public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
//    {
//        Player player = Main.player[projectile.owner];
//        if (projectile.type == 154)
//        {
//            int Value = damage / 50;
//            DDHelper.MaxandMin(ref Value, player.statLifeMax2 / 20, 1);
//            if (projectile.DPoroj().Times[0] < 10f)
//            {
//                projectile.DPoroj().Times[0] += Value;
//            }
//        }
//    }

//    public override void Kill(Projectile projectile, int timeLeft)
//    {
//        Player player = Main.player[projectile.owner];
//        if (projectile.type == 154 && projectile.DPoroj().Times[0] >= 1f)
//        {
//            player.statLife += (int)projectile.DPoroj().Times[0];
//            player.HealEffect((int)projectile.DPoroj().Times[0]);
//            NetMessage.SendData(66, -1, -1, null, projectile.owner, 3f);
//        }
//    }

//    public override bool PreDraw(Projectile projectile, ref Color lightColor)
//    {
//        Player player = Main.player[projectile.owner];
//        Texture2D value = TextureAssets.Projectile[projectile.type].Value;
//        SpriteEffects spriteEffects = SpriteEffects.None;
//        if (projectile.spriteDirection == 1)
//        {
//            spriteEffects = SpriteEffects.FlipHorizontally;
//        }
//        if (projectile.type == 154)
//        {
//            DDHelper.BackAndForth(0.3f, 0.6f, 0.01f, ref projectile.DPoroj().Times[3], ref projectile.DPoroj().Bool[1]);
//            Texture2D value2 = DDTextures.Perlin.Value;
//            Main.spriteBatch.Draw(value, projectile.position - Main.screenPosition + projectile.Size / 2f, null, Color.White, projectile.rotation, value.Size() / 2f, projectile.scale, (SpriteEffects)projectile.spriteDirection, 0f);
//            for (int i = 0; i < projectile.DPoroj().Times[0]; i++)
//            {
//                Main.spriteBatch.Draw(value, projectile.position - Main.screenPosition + projectile.Size / 2f, null, new Color(55, 0, 0, 0), projectile.rotation, value.Size() / 2f, projectile.scale, (SpriteEffects)projectile.spriteDirection, 0f);
//            }
//            Main.spriteBatch.End();
//            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
//            projectile.DPoroj().Times[4] += 0.01f;
//            DDHelper.ShieldShaders(1f, new Color(155, 20, 20), projectile.DPoroj().Times[4]);
//            Main.spriteBatch.Draw(value2, projectile.position - Main.screenPosition + projectile.Size / 2f, null, Color.White, -(float)Math.PI / 2f, value2.Size() / 2f, projectile.DPoroj().Times[1], SpriteEffects.None, 0f);
//            Main.spriteBatch.Draw(value2, projectile.position - Main.screenPosition + projectile.Size / 2f, null, Color.White, -(float)Math.PI / 2f, value2.Size() / 2f, projectile.DPoroj().Times[1], SpriteEffects.None, 0f);
//            Main.spriteBatch.End();
//            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
//            return false;
//        }
//        if (projectile.type == 543)
//        {
//            Main.spriteBatch.Draw(value, projectile.position - Main.screenPosition + projectile.Size / 2f, null, Color.White, projectile.rotation, value.Size() / 2f, projectile.scale, (SpriteEffects)projectile.spriteDirection, 0f);
//            if (projectile.localAI[1] == 0f)
//            {
//                return false;
//            }
//            for (int j = 0; j < projectile.oldPos.Length; j++)
//            {
//                Vector2 position = projectile.oldPos[j] + projectile.Size / 2f - Main.screenPosition;
//                Color color = new Color(255, 0, 0, 0) * ((projectile.oldPos.Length - j) / (float)projectile.oldPos.Length);
//                Main.spriteBatch.Draw(value, position, null, color, projectile.rotation, value.Size() / 2f, projectile.scale, (SpriteEffects)projectile.spriteDirection, 0f);
//            }
//            return false;
//        }
//        if (projectile.type == 153)
//        {
//            spriteEffects = SpriteEffects.None;
//            if (player.direction == 1)
//            {
//                spriteEffects = SpriteEffects.FlipHorizontally;
//            }
//            float rotation = projectile.rotation + (float)Math.PI / 4f;
//            if (player.direction == 1)
//            {
//                rotation = projectile.rotation - (float)Math.PI / 4f;
//            }
//            for (int k = 0; k < projectile.oldPos.Length; k++)
//            {
//                Vector2 vector = projectile.oldPos[k] + projectile.Size / 2f - Main.screenPosition;
//                Color color2 = new Color(255, 0, 0, 0) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
//                Main.spriteBatch.Draw(value, vector + projectile.velocity.PerfectNormalize() * 8f, new Rectangle(0, 0, 30, 30), color2, rotation, new Vector2(30f, 30f) / 2f, projectile.scale * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length), spriteEffects, 0f);
//            }
//            Main.spriteBatch.Draw(value, projectile.Center - Main.screenPosition - projectile.velocity.PerfectNormalize() * 38f, null, lightColor, rotation, new Vector2(value.Width, value.Height) / 2f, projectile.scale, spriteEffects, 0f);
//            return false;
//        }
//        return base.PreDraw(projectile, ref lightColor);
//    }

//    public override bool? Colliding(Projectile projectile, Rectangle projHitbox, Rectangle targetHitbox)
//    {
//        if (SwordHitbox)
//        {
//            Player player = Main.player[projectile.owner];
//            float amount = 0.75f;
//            float num = MathHelper.Lerp(0f, projectile.height, amount);
//            Vector2 vector = projectile.velocity.PerfectNormalize().RotatedBy(0.0);
//            float collisionPoint = 0f;
//            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), player.Center + vector * 18f, projectile.Center + vector * num, projHitbox.Width, ref collisionPoint);
//        }
//        return base.Colliding(projectile, projHitbox, targetHitbox);
//    }
}
