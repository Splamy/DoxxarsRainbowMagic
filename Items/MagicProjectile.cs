using System;
using System.IO;
//using DDmod.Content.NPCs;
//using DDmod.Textures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
//using Terraria.Audio;
//using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DoxxarsRainbowMagic.Items;

public class MagicProjectile : GlobalProjectile
{
    public static Asset<Texture2D> MagicDaggerGlow;

    public override bool InstancePerEntity => true;

    /*public override void Load()
    {
        if (!Main.dedServ)
        {
            MagicDaggerGlow = ModContent.Request<Texture2D>("DDmod/Content/Projectiles/Magic/Staff/MagicDagger2");
        }
    }*/

    public override void SetDefaults(Projectile projectile)
    {
        if (projectile.type == 7)
        {
            NPCHit(projectile, 40);
        }
        if (projectile.type == 20)
        {
            NPCHit(projectile, 30);
        }
        if (projectile.type == 27)
        {
            NPCHit(projectile, 10);
        }
        if (projectile.type == 93)
        {
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
        }
        if (projectile.type == 44 || projectile.type == 45)
        {
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
            NPCHit(projectile, 30);
        }
        if (projectile.type == 95 || projectile.type == 101)
        {
            NPCHit(projectile, 30);
        }
        if (projectile.type == 280)
        {
            NPCHit(projectile, 30);
        }
        if (projectile.type == ProjectileID.BookOfSkullsSkull)
        {
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
            NPCHit(projectile, 30);
        }
        if (projectile.type == 254)
        {
            projectile.scale *= 1.5f;
        }
        if (projectile.type == 255)
        {
            projectile.timeLeft = 400;
            projectile.extraUpdates = 40;
            projectile.DPoroj().Times[0] = 1f;
            projectile.timeLeft *= 50;
        }
        if (projectile.type == 409)
        {
            NPCHit(projectile, 60);
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

    public override bool PreAI(Projectile projectile)
    {
        if (projectile.type == 409 && projectile.DPoroj().track < 30)
        {
            projectile.localAI[1] += 1f;
            if (projectile.localAI[1] > 10f && Main.rand.NextBool(3))
            {
                for (int i = 0; i < 6; i++)
                {
                    Vector2 spinningpoint = Vector2.Normalize(projectile.velocity) * new Vector2(projectile.width, projectile.height) / 2f;
                    spinningpoint = spinningpoint.RotatedBy((i - 2) * Math.PI / 6.0) + projectile.Center;
                    Vector2 vector = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - (float)Math.PI / 2f).ToRotationVector2() * Main.rand.Next(3, 8);
                    Dust obj = Main.dust[Dust.NewDust(spinningpoint + vector, 0, 0, 217, vector.X * 2f, vector.Y * 2f, 100, default, 1.4f)];
                    obj.noGravity = true;
                    obj.noLight = true;
                    obj.velocity /= 4f;
                    obj.velocity -= projectile.velocity;
                }
                projectile.alpha -= 5;
                if (projectile.alpha < 50)
                {
                    projectile.alpha = 50;
                }
                projectile.rotation += projectile.velocity.X * 0.1f;
                projectile.frame = (int)(projectile.localAI[1] / 3f) % 3;
                Lighting.AddLight((int)projectile.Center.X / 16, (int)projectile.Center.Y / 16, 0.1f, 0.4f, 0.6f);
            }
            return false;
        }
        if (projectile.type == 244)
        {
            bool flag = false;
            NPC nPC = projectile.FindTargetWithinRange(500f);
            DDHelper.BackAndForth(30f, 80f, 0.2f, ref projectile.DPoroj().Times[0], ref projectile.DPoroj().Bool[0]);
            projectile.netUpdate = true;
            if (nPC != null && nPC.CanBeChasedBy(projectile))
            {
                Vector2 vector2 = Vector2.Subtract(nPC.Center - new Vector2(0f, nPC.height / 2 + projectile.DPoroj().Times[0]), projectile.Center);
                vector2.X += nPC.velocity.X * 3f;
                float Value = vector2.Length() / 50f;
                DDHelper.MaxandMinF(ref Value, 1f, 12f);
                vector2.Normalize();
                vector2 *= Value;
                projectile.velocity = (projectile.velocity * 9f + vector2) / 10f;
                flag = true;
            }
            if (!flag)
            {
                projectile.velocity = Vector2.Zero;
            }
        }
        if (projectile.type == 837)
        {
            projectile.spriteDirection = projectile.direction;
            if (projectile.direction < 0)
            {
                projectile.rotation = projectile.velocity.ToRotation() + (float)Math.PI;
            }
            else
            {
                projectile.rotation = projectile.velocity.ToRotation();
            }
            NPC nPC2 = NPCdirection.FindClosest(projectile.Center, 500f, checkCanHit: false);
            projectile.alpha = 0;
            if (nPC2 != null && nPC2.CanBeChasedBy(projectile) && projectile.DPoroj().track > 80)
            {
                Vector2 vector3 = Vector2.Subtract(nPC2.Center, projectile.Center);
                if (projectile.ai[0] == 0f)
                {
                    projectile.ai[0] = 1f;
                    for (float num = 0f; num < projectile.scale; num += 0.01f)
                    {
                        Dust obj2 = Main.dust[Dust.NewDust(projectile.position, 1, 1, 6, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100)];
                        obj2.noGravity = true;
                        obj2.scale = 2f;
                        obj2.velocity = Main.rand.NextVector2Unit(vector3.ToRotation() - (float)Math.PI / 4f, (float)Math.PI / 2f) * (0f - Main.rand.NextFloat(3f, 6.5f));
                    }
                    projectile.velocity = vector3.PerfectNormalize() * 30f;
                }
                vector3.Normalize();
                vector3 *= 4f;
                projectile.velocity = (projectile.velocity * 20f + vector3) / 21f;
            }
            projectile.frame++;
            if (projectile.frame > 2)
            {
                projectile.frame = 0;
            }
            for (int j = 0; j < 2; j++)
            {
                int num2 = 4;
                Dust obj3 = Main.dust[Dust.NewDust(projectile.position + new Vector2(num2), projectile.width - num2 * 2, projectile.height - num2 * 2, 6, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default, 2f)];
                obj3.position -= projectile.velocity * 2f;
                obj3.noGravity = true;
                obj3.velocity.X *= 0.3f;
                obj3.velocity.Y *= 0.3f;
            }
            return false;
        }
        if (projectile.type == 45)
        {
            projectile.ai[0] -= 0.25f;
            NPC nPC3 = projectile.FindTargetWithinRange(500f, checkCanHit: true);
            if (nPC3 != null && nPC3.CanBeChasedBy(projectile) && projectile.DPoroj().track > 80 && projectile.DPoroj().track < 1200)
            {
                Vector2 vector4 = Vector2.Subtract(nPC3.Center, projectile.Center);
                vector4.Normalize();
                vector4 *= 12f;
                projectile.velocity = (projectile.velocity * 9f + vector4) / 10f;
            }
        }
        if (projectile.type == 27)
        {
            for (int k = 0; k < 2; k++)
            {
                Vector2 vector5 = projectile.velocity / 3f * k;
                int num3 = 4;
                Dust obj4 = Main.dust[Dust.NewDust(projectile.position + new Vector2(num3), projectile.width - num3 * 2, projectile.height - num3 * 2, 172, 0f, 0f, 100, default, 1.2f)];
                obj4.noGravity = true;
                obj4.velocity *= 0.1f;
                obj4.velocity += projectile.velocity * 0.1f;
                obj4.position -= vector5;
            }
            if (Main.rand.NextBool(5))
            {
                int num4 = 4;
                Dust obj5 = Main.dust[Dust.NewDust(projectile.position + new Vector2(num4), projectile.width - num4 * 2, projectile.height - num4 * 2, 172, 0f, 0f, 100, default, 0.6f)];
                obj5.velocity *= 0.25f;
                obj5.velocity += projectile.velocity * 0.5f;
            }
            if (projectile.ai[1] >= 20f)
            {
                projectile.velocity.Y += 0.2f;
            }
            projectile.rotation += 0.3f * projectile.direction;
            if (projectile.velocity.Y > 16f)
            {
                projectile.velocity.Y = 16f;
            }
            return false;
        }
        if (projectile.type == 294)
        {
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] > 9f)
            {
                projectile.alpha = 255;
                Dust obj6 = Main.dust[Dust.NewDust(projectile.position, 1, 1, 173)];
                obj6.scale = 1f;
                obj6.alpha = 255;
                obj6.velocity = Vector2.Zero;
            }
            return false;
        }
        if (projectile.type == 280)
        {
            projectile.scale -= 0.002f;
            if (projectile.scale <= 0f)
            {
                projectile.Kill();
            }
            projectile.velocity.Y += 0.075f;
            Dust obj7 = Main.dust[Dust.NewDust(projectile.position, 1, 1, 170, 0f, 0f, 100)];
            obj7.noGravity = true;
            obj7.scale = 1.3f;
            obj7.velocity = projectile.velocity * 0.5f;
            if (projectile.velocity.Y > 0f && projectile.ai[0] == 0f)
            {
                NPC nPC4 = projectile.FindTargetWithinRange(500f, checkCanHit: true);
                if (nPC4 != null && nPC4.CanBeChasedBy(projectile))
                {
                    Vector2 vector6 = Vector2.Subtract(nPC4.Center, projectile.Center);
                    if (vector6.Y > 0f)
                    {
                        vector6.Normalize();
                        vector6 *= 7f;
                        projectile.velocity.X = (projectile.velocity.X * 9f + vector6.X) / 10f;
                    }
                }
            }
            return false;
        }
        if (projectile.type == 94)
        {
            projectile.ArmorPenetration = 10;
        }
        if (projectile.type == 254)
        {
            if (projectile.velocity.X > 0f)
            {
                projectile.rotation += (Math.Abs(projectile.velocity.Y) + Math.Abs(projectile.velocity.X)) * 0.001f;
            }
            else
            {
                projectile.rotation -= (Math.Abs(projectile.velocity.Y) + Math.Abs(projectile.velocity.X)) * 0.001f;
            }
            projectile.frameCounter++;
            if (projectile.frameCounter > 6)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame > 4)
                {
                    projectile.frame = 0;
                }
            }
            if (Math.Sqrt((double)(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y)) > 2.0)
            {
                projectile.velocity *= 0.98f;
            }
            for (int l = 0; l < 1000; l++)
            {
                if (l != projectile.whoAmI && Main.projectile[l].active && Main.projectile[l].owner == projectile.owner && Main.projectile[l].type == projectile.type && projectile.timeLeft > Main.projectile[l].timeLeft && Main.projectile[l].timeLeft > 30)
                {
                    Main.projectile[l].timeLeft = 30;
                }
            }
            if (projectile.timeLeft <= 30)
            {
                projectile.alpha += 20;
                return false;
            }
            bool flag2 = false;
            NPC nPC5 = projectile.FindTargetWithinRange(400f, checkCanHit: true);
            if (nPC5 != null && nPC5.CanBeChasedBy(projectile))
            {
                flag2 = true;
            }
            if (Main.myPlayer == projectile.owner)
            {
                float num5 = (Main.MouseWorld - projectile.Center).Length() / 30f;
                if (num5 < 1f)
                {
                    num5 = 1f;
                }
                if (num5 > 10f)
                {
                    num5 = 10f;
                }
                projectile.velocity = (Main.MouseWorld - projectile.Center).PerfectNormalize() * num5;
                projectile.netUpdate = true;
            }
            //if (Main.projectile[(int)projectile.ai[1]].type == ModContent.ProjectileType<MagnetSphere>() && Main.projectile[(int)projectile.ai[1]].active)
            //{
            //    projectile.timeLeft = 600;
            //}
            if (flag2)
            {
                projectile.localAI[0] += 1f;
                if (projectile.localAI[0] > 25f || projectile.ai[0] == 1f)
                {
                    Vector2 position = projectile.Center + projectile.velocity.PerfectNormalize() * 10f;
                    int type2 = 160;
                    for (float num6 = 0f; num6 < 30f; num6 += 1f)
                    {
                        Dust obj8 = Main.dust[Dust.NewDust(position, 1, 1, type2, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100)];
                        obj8.noGravity = true;
                        obj8.scale = 1.2f;
                        obj8.velocity = Main.rand.NextVector2Unit() * Main.rand.NextFloat(1f, 30f);
                    }
                    projectile.localAI[0] = 0f;
                    if (projectile.ai[0] == 1f)
                    {
                        for (int m = -1; m <= 1; m++)
                        {
                            Vector2 vector7 = (Vector2.Subtract(nPC5.Center, projectile.Center).PerfectNormalize() * projectile.velocity.Length()).RotatedBy(Main.rand.NextFloat(-1.1f, 1.1f));
                            Projectile.NewProjectile(projectile.GetSource_FromThis(), position, vector7.PerfectNormalize() * 5f, 255, projectile.damage, projectile.knockBack, projectile.owner);
                        }
                        projectile.ai[0] = 0f;
                    }
                    else
                    {
                        Vector2 vector8 = (Vector2.Subtract(nPC5.Center, projectile.Center).PerfectNormalize() * projectile.velocity.Length()).RotatedBy(Main.rand.NextFloat(-1.1f, 1.1f));
                        Projectile.NewProjectile(projectile.GetSource_FromThis(), position, vector8.PerfectNormalize() * 5f, 255, projectile.damage, projectile.knockBack, projectile.owner);
                    }
                }
            }
            else
            {
                projectile.localAI[0] += 1f;
                if (projectile.localAI[0] > 25f || projectile.ai[0] == 1f)
                {
                    Vector2 position2 = projectile.Center + projectile.velocity.PerfectNormalize() * 10f;
                    int type3 = 160;
                    for (float num7 = 0f; num7 < 30f; num7 += 1f)
                    {
                        Dust obj9 = Main.dust[Dust.NewDust(position2, 1, 1, type3, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100)];
                        obj9.noGravity = true;
                        obj9.scale = 1.2f;
                        obj9.velocity = Main.rand.NextVector2Unit() * Main.rand.NextFloat(1f, 30f);
                    }
                    projectile.localAI[0] = 0f;
                    if (projectile.ai[0] == 1f)
                    {
                        for (int n = -1; n <= 1; n++)
                        {
                            Vector2 vector9 = Main.rand.NextVector2Unit();
                            int num8 = Projectile.NewProjectile(projectile.GetSource_FromThis(), position2, vector9.PerfectNormalize() * 5f, 255, projectile.damage, projectile.knockBack, projectile.owner);
                            Main.projectile[num8].DPoroj().vector[0] = vector9.PerfectNormalize();
                        }
                        projectile.ai[0] = 0f;
                    }
                    else
                    {
                        Vector2 vector10 = Main.rand.NextVector2Unit();
                        int num9 = Projectile.NewProjectile(projectile.GetSource_FromThis(), position2, vector10.PerfectNormalize() * 5f, 255, projectile.damage, projectile.knockBack, projectile.owner);
                        Main.projectile[num9].DPoroj().vector[0] = vector10.PerfectNormalize();
                    }
                }
            }
            return false;
        }
        if (projectile.type == 255)
        {
            if (Main.rand.NextBool(5) && projectile.DPoroj().track > 3)
            {
                NPC nPC6 = projectile.FindTargetWithinRange(500f, checkCanHit: true);
                if (nPC6 == null || !nPC6.CanBeChasedBy(projectile))
                {
                    Vector2 vector11 = projectile.velocity = (projectile.DPoroj().vector[0] * projectile.velocity.Length()).RotatedBy(Main.rand.NextFloat(-1f, 1f));
                }
                else
                {
                    float num10 = Vector2.Subtract(nPC6.Center, projectile.Center).Length() / 300f;
                    if (num10 > 1f)
                    {
                        num10 = 1f;
                    }
                    Vector2 vector12 = projectile.velocity = (Vector2.Subtract(nPC6.Center, projectile.Center).PerfectNormalize() * projectile.velocity.Length()).RotatedBy(Main.rand.NextFloat(0f - num10, num10));
                }
            }
            projectile.DPoroj().Times[0] -= 0.003f;
            projectile.alpha = 255;
            Dust obj10 = Main.dust[Dust.NewDust(projectile.position, 1, 1, 160)];
            obj10.velocity = Vector2.Zero;
            obj10.scale = projectile.DPoroj().Times[0];
            if (projectile.scale < 0.01f)
            {
                projectile.active = false;
            }
            return false;
        }
        return base.PreAI(projectile);
    }

    public override bool TileCollideStyle(Projectile projectile, ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
    {
        if (projectile.type == 95)
        {
            fallThrough = projectile.localAI[0] == 1f;
        }
        return base.TileCollideStyle(projectile, ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
    }

    public void Eye2(Projectile projectile)
    {
        Vector2 center = projectile.Center;
        int num = 4;
        for (int i = -num; i <= num; i++)
        {
            for (int j = -num; j <= num; j++)
            {
                int num2 = (int)(i + center.X / 16f);
                int num3 = (int)(j + center.Y / 16f);
                Vector2 vector = new Vector2(num2, num3);
                if (vector.X > 0f && vector.Y > 0f && vector.X < Main.maxTilesX && vector.Y < Main.maxTilesY)
                {
                    if (Main.tile[(int)vector.X, (int)vector.Y].HasTile && Math.Sqrt(i * i + j * j) <= num + 0.5)
                    {
                        WorldGen.SquareTileFrame((int)vector.X, (int)vector.Y);
                        WorldGen.KillTile((int)vector.X, (int)vector.Y);
                    }
                    if (Main.tile[(int)vector.X, (int)vector.Y].WallType > 0 && Math.Sqrt(i * i + j * j) <= num + 0.5)
                    {
                        WorldGen.SquareTileFrame((int)vector.X, (int)vector.Y);
                        WorldGen.KillWall((int)vector.X, (int)vector.Y);
                    }
                }
            }
        }
    }

    public override bool? CanDamage(Projectile projectile)
    {
        return base.CanDamage(projectile);
    }

    //public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
    //{
    //    _ = Main.player[projectile.owner];
    //    if (target.HasBuff(30) && projectile.type == 245)
    //    {
    //        damage = (int)(damage * 1.2f);
    //    }
    //}

    /*public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
    {
        _ = Main.player[projectile.owner];
        if (projectile.type == 409)
        {
            projectile.localNPCImmunity[target.whoAmI] = projectile.localNPCHitCooldown;
            Main.npc[target.whoAmI].immune[projectile.owner] = 1;
        }
        if (projectile.type == 93)
        {
            int type = 57;
            for (float num = 0f; num < 20f; num += 1f)
            {
                Dust obj = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, type, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100)];
                obj.noGravity = true;
                obj.scale = 1.3f;
                obj.velocity = Main.rand.NextVector2Unit(projectile.velocity.ToRotation() - (float)Math.PI / 4f, 1.57f) * Main.rand.NextFloat(projectile.velocity.Length() / 4f, projectile.velocity.Length() / 2f);
            }
        }
        if (projectile.type == 27)
        {
            NPC nPC = NPCdirection.FindClosest(projectile.Center, 600f, checkCanHit: true, target);
            if (nPC != null)
            {
                projectile.velocity = (nPC.Center - projectile.Center).PerfectNormalize() * projectile.velocity.Length();
            }
            int type2 = 172;
            for (float num2 = 0f; num2 < 1f; num2 += 0.05f)
            {
                Dust obj2 = Main.dust[Dust.NewDust(projectile.position, 1, 1, type2, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100)];
                obj2.noGravity = true;
                obj2.scale = 1f;
                obj2.velocity = Main.rand.NextVector2Unit() * Main.rand.NextFloat(2.5f, 3.5f);
            }
            projectile.damage = (int)(projectile.damage * 0.85f);
        }
        if (projectile.type == 45 && projectile.DPoroj().track > 80)
        {
            projectile.DPoroj().track = 1200;
        }
        if (projectile.type == 837)
        {
            target.AddBuff(24, 300);
        }
        if (projectile.type == 101)
        {
            target.AddBuff(39, 420);
        }
        if (projectile.type == 245 && Main.rand.NextBool(5))
        {
            target.AddBuff(30, 120);
        }
    }*/

    /*public override void Kill(Projectile projectile, int timeLeft)
    {
        if (projectile.type == 93)
        {
            int type = 57;
            for (float num = 0f; num < 80f; num += 1f)
            {
                Dust obj = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, type, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100)];
                obj.noGravity = true;
                obj.scale = 1.3f;
                obj.velocity = Main.rand.NextVector2Unit(projectile.velocity.ToRotation() - (float)Math.PI / 4f, 1.57f) * Main.rand.NextFloat(projectile.velocity.Length() / 4f, projectile.velocity.Length() / 2f);
            }
        }
        if (projectile.type == 255)
        {
            int type2 = 160;
            for (float num2 = 0f; num2 < 20f; num2 += 1f)
            {
                Dust obj2 = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, type2, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100)];
                obj2.noGravity = true;
                obj2.scale = 1.2f;
                obj2.velocity = Main.rand.NextVector2Unit() * Main.rand.NextFloat(1f, 10f);
            }
            SoundStyle style = SoundID.Thunder;
            style.Pitch = 0.5f;
            SoundEngine.PlaySound(in style, projectile.position);
        }
    }*/

    /*public override bool PreDraw(Projectile Projectile, ref Color lightColor)
    {
        Texture2D value = TextureAssets.Projectile[Projectile.type].Value;
        Texture2D value2 = DDTextures.VoidStar.Value;
        if (Projectile.type == 93)
        {
            Vector2 vector = new Vector2(Projectile.width, Projectile.height) / 2f;
            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Vector2 position = Projectile.oldPos[i] + vector - Main.screenPosition;
                    Color color = new Color(236, 236, 51, 0) * ((Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length / 2f);
                    Main.spriteBatch.Draw(MagicDaggerGlow.Value, position, null, color, Projectile.oldRot[i], MagicDaggerGlow.Size() / 2f, Projectile.scale * 1.2f * ((Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length * 1.2f), SpriteEffects.None, 0f);
                    color = new Color(20, 20, 201, 0) * ((Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length / 2f);
                    Main.spriteBatch.Draw(MagicDaggerGlow.Value, position, null, color, Projectile.oldRot[i], MagicDaggerGlow.Size() / 2f, Projectile.scale * 0.5f * ((Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length * 1.2f), SpriteEffects.None, 0f);
                }
            }
            Main.spriteBatch.Draw(value, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, value.Width, value.Height), Color.White, Projectile.rotation, new Vector2(value.Width / 2, value.Height / 2), Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
        if (Projectile.type == 44 || Projectile.type == 45)
        {
            Vector2 vector2 = new Vector2(Projectile.width, Projectile.height) / 2f;
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                for (int l = 0; l < 3; l++)
                {
                    Vector2 position2 = Projectile.oldPos[k] + vector2 - Main.screenPosition;
                    Color color2 = new Color(132, 2, 253, 0) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length) * 0.5f;
                    Main.spriteBatch.Draw(value, position2, null, color2, Projectile.oldRot[k], value.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
                }
            }
            Main.spriteBatch.Draw(value, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, value.Width, value.Height), new Color(255, 255, 255, 0), Projectile.rotation, new Vector2(value.Width / 2, value.Height / 2), Projectile.scale, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(value, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, value.Width, value.Height), new Color(255, 255, 255, 0), Projectile.rotation, new Vector2(value.Width / 2, value.Height / 2), Projectile.scale, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(value, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, value.Width, value.Height), new Color(255, 255, 255, 0), Projectile.rotation, new Vector2(value.Width / 2, value.Height / 2), Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
        if (Projectile.type == 837)
        {
            if (Projectile.ai[0] == 0f)
            {
                return true;
            }
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.direction < 0)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Vector2 vector3 = new Vector2(Projectile.width, Projectile.height) / 2f;
            for (int m = 0; m < Projectile.oldPos.Length; m++)
            {
                for (int n = 0; n < 3; n++)
                {
                    Vector2 position3 = Projectile.oldPos[m] + vector3 - Main.screenPosition;
                    Color color3 = new Color(253, 62, 3, 0) * ((Projectile.oldPos.Length - m) / (float)Projectile.oldPos.Length) * 0.5f;
                    Main.spriteBatch.Draw(value, position3, new Rectangle(0, value.Height / 3 * Projectile.frame, value.Width, value.Height / 3), color3, Projectile.oldRot[m], new Vector2(value.Width, value.Height / 3) / 2f, Projectile.scale * 1.3f, effects, 0f);
                    Main.spriteBatch.Draw(value, position3, new Rectangle(0, value.Height / 3 * Projectile.frame, value.Width, value.Height / 3), color3, Projectile.oldRot[m], new Vector2(value.Width, value.Height / 3) / 2f, Projectile.scale * 1.3f, effects, 0f);
                }
            }
            return false;
        }
        if (Projectile.type == 254)
        {
            Vector2 vector4 = new Vector2(Projectile.width, Projectile.height) / 2f;
            Main.spriteBatch.Draw(value, Projectile.position + vector4 - Main.screenPosition, new Rectangle(0, value.Height / 5 * Projectile.frame, value.Width, value.Height / 5), Projectile.GetAlpha(Color.White), Projectile.rotation, new Vector2(value.Width / 2, value.Height / 10), Projectile.scale, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(value2, Projectile.position + vector4 - Main.screenPosition, null, new Color(2, 254, 201, 0) * 0.8f, Projectile.rotation, value2.Size() / 2f, Projectile.scale / 2f, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(value2, Projectile.position + vector4 - Main.screenPosition, null, new Color(253, 1, 54, 0) * 0.8f, Projectile.rotation, value2.Size() / 2f, Projectile.scale / 3f, SpriteEffects.None, 0f);
            return false;
        }
        return base.PreDraw(Projectile, ref lightColor);
    }*/
}
