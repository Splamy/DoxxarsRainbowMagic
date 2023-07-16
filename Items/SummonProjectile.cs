using System;
//using DDmod.Content.Buffs.DeBuffs;
//using DDmod.Content.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace DoxxarsRainbowMagic.Items;

public class SummonProjectile : GlobalProjectile
{
    protected bool MinionReset;

    protected float hitbox = 1f;

    public float ReboundSpeed;

    protected bool target;

    protected bool IgnoreTile;

    protected Player player;

    protected NPC npc;

    protected float SearchRange = 800f;

    public bool RotateAI;

    public override bool InstancePerEntity => true;

    //public override void SetDefaults(Projectile projectile)
    //{
    //    if (projectile.type == 375)
    //    {
    //        ReboundSpeed = 0.5f;
    //        MinionReset = true;
    //    }
    //    if (projectile.type == 387)
    //    {
    //        projectile.aiStyle = -1;
    //        ReboundSpeed = 0.5f;
    //        MinionReset = true;
    //    }
    //    if (projectile.type == 388)
    //    {
    //        ReboundSpeed = 0.5f;
    //        MinionReset = true;
    //        projectile.aiStyle = -1;
    //        projectile.usesIDStaticNPCImmunity = false;
    //        projectile.usesLocalNPCImmunity = true;
    //        projectile.localNPCHitCooldown = 15;
    //    }
    //    if (projectile.type == 266)
    //    {
    //        MinionReset = true;
    //        projectile.aiStyle = -1;
    //        projectile.usesIDStaticNPCImmunity = false;
    //        projectile.usesLocalNPCImmunity = true;
    //        projectile.localNPCHitCooldown = 15;
    //    }
    //    if (projectile.type == 389)
    //    {
    //        projectile.usesLocalNPCImmunity = true;
    //        projectile.localNPCHitCooldown = 60;
    //        projectile.usesIDStaticNPCImmunity = false;
    //    }
    //    if (projectile.type == 373)
    //    {
    //        ReboundSpeed = 0.5f;
    //        MinionReset = true;
    //        projectile.aiStyle = -1;
    //    }
    //    if (projectile.type == 407)
    //    {
    //        ReboundSpeed = 0.5f;
    //        MinionReset = true;
    //        projectile.aiStyle = -1;
    //    }
    //    if (projectile.type == 408)
    //    {
    //        ReboundSpeed = 0.5f;
    //        MinionReset = true;
    //        projectile.aiStyle = -1;
    //    }
    //    if (projectile.type == 533)
    //    {
    //        ReboundSpeed = 0.5f;
    //        MinionReset = true;
    //        projectile.aiStyle = -1;
    //        projectile.usesIDStaticNPCImmunity = false;
    //        projectile.usesLocalNPCImmunity = true;
    //        projectile.localNPCHitCooldown = 15;
    //    }
    //    if (projectile.type == 951)
    //    {
    //        ReboundSpeed = 0.5f;
    //        MinionReset = true;
    //        projectile.aiStyle = -1;
    //        projectile.usesIDStaticNPCImmunity = false;
    //        projectile.usesLocalNPCImmunity = true;
    //        projectile.localNPCHitCooldown = 15;
    //    }
    //    if (projectile.type >= 191 && projectile.type <= 194)
    //    {
    //        ReboundSpeed = 0.5f;
    //        MinionReset = true;
    //        projectile.aiStyle = -1;
    //        projectile.friendly = false;
    //    }
    //}

    //public override bool PreAI(Projectile projectile)
    //{
    //    player = projectile.Player();
    //    if (ReboundSpeed != 0f)
    //    {
    //        float num = projectile.width * hitbox;
    //        for (int i = 0; i < 1000; i++)
    //        {
    //            Projectile projectile2 = Main.projectile[i];
    //            if (i != projectile.whoAmI && projectile2.active && projectile2.owner == projectile.owner && Math.Abs(projectile.position.X - projectile2.position.X) + Math.Abs(projectile.position.Y - projectile2.position.Y) < num && projectile2.GetGlobalProjectile<SummonProjectile>().ReboundSpeed != 0f)
    //            {
    //                if (projectile.position.X < Main.projectile[i].position.X)
    //                {
    //                    projectile.velocity.X -= ReboundSpeed;
    //                }
    //                else
    //                {
    //                    projectile.velocity.X += ReboundSpeed;
    //                }
    //                if (projectile.position.Y < Main.projectile[i].position.Y)
    //                {
    //                    projectile.velocity.Y -= ReboundSpeed;
    //                }
    //                else
    //                {
    //                    projectile.velocity.Y += ReboundSpeed;
    //                }
    //            }
    //        }
    //    }
    //    if (MinionReset)
    //    {
    //        projectile.tileCollide = false;
    //        target = false;
    //        if (player.HasMinionAttackTargetNPC)
    //        {
    //            npc = null;
    //            npc = Main.npc[player.MinionAttackTargetNPC];
    //            if (IgnoreTile || Collision.CanHitLine(player.position, player.width, player.height, npc.position, npc.width, npc.height) && !projectile.sentry)
    //            {
    //                if (Vector2.Distance(npc.Center, projectile.Center) < SearchRange)
    //                {
    //                    target = true;
    //                }
    //            }
    //            else if (Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height) && Vector2.Distance(npc.Center, projectile.Center) < SearchRange)
    //            {
    //                target = true;
    //            }
    //        }
    //        if (!target)
    //        {
    //            npc = null;
    //            if (!projectile.sentry)
    //            {
    //                npc = NPCdirection.FindClosest(player.Center, SearchRange, IgnoreTile);
    //            }
    //            if (npc == null)
    //            {
    //                npc = NPCdirection.FindClosest(projectile.Center, SearchRange, IgnoreTile);
    //            }
    //            if (npc != null)
    //            {
    //                target = true;
    //            }
    //        }
    //    }
    //    if (projectile.type == 375)
    //    {
    //        if (Main.player[projectile.owner].dead)
    //        {
    //            Main.player[projectile.owner].impMinion = false;
    //        }
    //        if (Main.player[projectile.owner].impMinion)
    //        {
    //            projectile.timeLeft = 2;
    //        }
    //        float num2 = 12f;
    //        float num3 = (player.Center - projectile.Center).Length();
    //        projectile.frameCounter++;
    //        if (projectile.frameCounter > 4)
    //        {
    //            projectile.frame++;
    //            projectile.frameCounter = 0;
    //        }
    //        if (projectile.frame >= 8)
    //        {
    //            projectile.frame = 0;
    //        }
    //        Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, projectile.oldVelocity.X, projectile.oldVelocity.Y, 0, default, 1.4f)].noGravity = true;
    //        projectile.DPoroj().Times[0] += 0.25f;
    //        projectile.rotation = projectile.velocity.X * 0.05f;
    //        if (target)
    //        {
    //            Vector2 vector = npc.Center - projectile.Center;
    //            if (vector.X > 0f)
    //            {
    //                projectile.spriteDirection = -1;
    //            }
    //            else
    //            {
    //                projectile.spriteDirection = 0;
    //            }
    //            projectile.ai[0] += 1f;
    //            if (projectile.ai[1] <= 3f)
    //            {
    //                if (vector.Length() > 200f)
    //                {
    //                    vector = vector.PerfectNormalize();
    //                    projectile.velocity = (projectile.velocity * 20f + vector * num2) / 21f;
    //                }
    //                else
    //                {
    //                    vector = vector.PerfectNormalize();
    //                    projectile.velocity = (projectile.velocity * 20f + -vector * num2) / 21f;
    //                }
    //                if (projectile.ai[0] >= 30f)
    //                {
    //                    projectile.ai[1] += 1f;
    //                    int num4 = Projectile.NewProjectile(projectile.GetSource_FromAI(), projectile.Center + vector.PerfectNormalize() * 30f, vector * 12f, ModContent.ProjectileType<FireBall>(), projectile.damage, projectile.knockBack, projectile.owner, 0f, 1f);
    //                    Main.projectile[num4].tileCollide = false;
    //                    projectile.netUpdate = true;
    //                    projectile.ai[0] = 0f;
    //                    projectile.velocity = -vector * 5f;
    //                }
    //            }
    //            else
    //            {
    //                if (vector.Length() > 500f)
    //                {
    //                    vector = vector.PerfectNormalize();
    //                    projectile.velocity = (projectile.velocity * 20f + vector * num2) / 21f;
    //                }
    //                else
    //                {
    //                    vector = vector.PerfectNormalize();
    //                    projectile.velocity = (projectile.velocity * 20f + -vector * num2) / 21f;
    //                }
    //                if (projectile.ai[0] >= 50f)
    //                {
    //                    projectile.ai[1] = 0f;
    //                    int num5 = DProj.NewProjectileChange(projectile.GetSource_FromAI(), projectile.Center + vector.PerfectNormalize() * 30f, vector * 24f, ModContent.ProjectileType<FireBall>(), projectile.damage * 3, projectile.knockBack, projectile.owner, 0f, 1f, 2f);
    //                    Main.projectile[num5].scale = 3f;
    //                    Main.projectile[num5].tileCollide = false;
    //                    projectile.netUpdate = true;
    //                    projectile.ai[0] = 0f;
    //                    projectile.velocity = -vector * 12f;
    //                }
    //            }
    //        }
    //        else
    //        {
    //            projectile.ai[0] = 0f;
    //            if (projectile.velocity.X > 0f)
    //            {
    //                projectile.spriteDirection = -1;
    //            }
    //            else
    //            {
    //                projectile.spriteDirection = 0;
    //            }
    //            Vector2 vector2 = player.Center - projectile.Center;
    //            if (player.velocity.Length() > 20f || vector2.Length() > 300f)
    //            {
    //                vector2 = player.Center - new Vector2(0f, 100f) - projectile.Center;
    //                float num6 = num2 + player.velocity.Length();
    //                projectile.netUpdate = true;
    //                if (num3 > 3000f)
    //                {
    //                    projectile.Center = player.Center;
    //                }
    //                vector2 = vector2.PerfectNormalize();
    //                vector2 *= num6;
    //                float num7 = 60f;
    //                projectile.velocity = (projectile.velocity * num7 + vector2) / (num7 + 1f);
    //            }
    //            else
    //            {
    //                float num8 = 2f;
    //                projectile.netUpdate = true;
    //                vector2.Y -= 350f;
    //                if (num3 > 3000f)
    //                {
    //                    projectile.Center = player.Center;
    //                }
    //                if (projectile.velocity.Length() <= 5f)
    //                {
    //                    projectile.velocity.X *= 1.02f;
    //                }
    //                if (vector2.Length() > 300f)
    //                {
    //                    vector2.Normalize();
    //                    vector2 *= num8;
    //                    float num9 = 10f;
    //                    projectile.velocity = (projectile.velocity * num9 + vector2) / (num9 + 1f);
    //                }
    //            }
    //        }
    //        return false;
    //    }
    //    if (projectile.type == 387)
    //    {
    //        if (Main.player[projectile.owner].dead)
    //        {
    //            Main.player[projectile.owner].twinsMinion = false;
    //        }
    //        if (Main.player[projectile.owner].twinsMinion)
    //        {
    //            projectile.timeLeft = 2;
    //        }
    //        float num10 = 12f;
    //        float num11 = (player.Center - projectile.Center).Length();
    //        projectile.frameCounter++;
    //        if (projectile.frameCounter > 4)
    //        {
    //            projectile.frame++;
    //            projectile.frameCounter = 0;
    //        }
    //        if (projectile.frame >= 3)
    //        {
    //            projectile.frame = 0;
    //        }
    //        projectile.DPoroj().Times[0] += 0.25f;
    //        projectile.spriteDirection = -1;
    //        projectile.rotation = projectile.velocity.ToRotation();
    //        if (target)
    //        {
    //            Vector2 vector3 = npc.Center - projectile.Center;
    //            projectile.rotation = vector3.ToRotation();
    //            projectile.ai[0] += 1f;
    //            if (vector3.Length() > 200f)
    //            {
    //                vector3 = vector3.PerfectNormalize();
    //                projectile.velocity = (projectile.velocity * 20f + vector3 * num10) / 21f;
    //            }
    //            else
    //            {
    //                vector3 = vector3.PerfectNormalize();
    //                projectile.velocity = (projectile.velocity * 20f + -vector3 * num10) / 21f;
    //            }
    //            if (projectile.ai[0] >= 90f && projectile.ai[0] % 10f == 0f)
    //            {
    //                projectile.ai[1] += 1f;
    //                Projectile.NewProjectile(projectile.GetSource_FromAI(), projectile.Center + vector3.PerfectNormalize() * 30f, vector3 * 7f, 389, projectile.damage, projectile.knockBack, projectile.owner);
    //                projectile.netUpdate = true;
    //                if (projectile.ai[1] > 5f)
    //                {
    //                    projectile.ai[0] = 0f;
    //                    projectile.ai[1] = 0f;
    //                }
    //            }
    //        }
    //        else
    //        {
    //            projectile.ai[0] = 0f;
    //            Vector2 vector4 = player.Center - projectile.Center;
    //            if (player.velocity.Length() > 20f || vector4.Length() > 300f)
    //            {
    //                vector4 = player.Center - new Vector2(0f, 100f) - projectile.Center;
    //                float num12 = num10 + player.velocity.Length();
    //                projectile.netUpdate = true;
    //                if (num11 > 3000f)
    //                {
    //                    projectile.Center = player.Center;
    //                }
    //                vector4 = vector4.PerfectNormalize();
    //                vector4 *= num12;
    //                float num13 = 60f;
    //                projectile.velocity = (projectile.velocity * num13 + vector4) / (num13 + 1f);
    //            }
    //            else
    //            {
    //                float num14 = 2f;
    //                projectile.netUpdate = true;
    //                vector4.Y -= 350f;
    //                if (num11 > 3000f)
    //                {
    //                    projectile.Center = player.Center;
    //                }
    //                if (projectile.velocity.Length() <= 5f)
    //                {
    //                    projectile.velocity.X *= 1.02f;
    //                }
    //                if (vector4.Length() > 300f)
    //                {
    //                    vector4.Normalize();
    //                    vector4 *= num14;
    //                    float num15 = 10f;
    //                    projectile.velocity = (projectile.velocity * num15 + vector4) / (num15 + 1f);
    //                }
    //            }
    //        }
    //        return false;
    //    }
    //    if (projectile.type == 388)
    //    {
    //        if (Main.player[projectile.owner].dead)
    //        {
    //            Main.player[projectile.owner].twinsMinion = false;
    //        }
    //        if (Main.player[projectile.owner].twinsMinion)
    //        {
    //            projectile.timeLeft = 2;
    //        }
    //        float num16 = 12f;
    //        float num17 = (player.Center - projectile.Center).Length();
    //        projectile.frameCounter++;
    //        if (projectile.frameCounter > 4)
    //        {
    //            projectile.frame++;
    //            projectile.frameCounter = 0;
    //        }
    //        if (projectile.frame >= 3)
    //        {
    //            projectile.frame = 0;
    //        }
    //        projectile.DPoroj().Times[0] += 0.25f;
    //        projectile.spriteDirection = -1;
    //        projectile.friendly = true;
    //        if (target)
    //        {
    //            Vector2 vector5 = npc.Center - projectile.Center;
    //            if (projectile.ai[1] > 15f)
    //            {
    //                projectile.rotation = vector5.ToRotation();
    //                if (vector5.Length() > 100f)
    //                {
    //                    vector5 = vector5.PerfectNormalize();
    //                    projectile.velocity = (projectile.velocity * 20f + vector5 * num16) / 21f;
    //                }
    //                else
    //                {
    //                    vector5 = vector5.PerfectNormalize();
    //                    projectile.velocity = (projectile.velocity * 20f - vector5 * num16) / 21f;
    //                }
    //                projectile.ai[0] += 1f;
    //                if (projectile.ai[0] % 8f == 0f)
    //                {
    //                    Vector2 vector6 = vector5.RotatedBy(Main.rand.NextFloat(-0.1f, 0.1f));
    //                    DProj.NewProjectileChange(projectile.GetSource_FromAI(), projectile.Center + vector5 * 16f, vector6 * 8f, ModContent.ProjectileType<EyeFire>(), projectile.damage / 2, projectile.knockBack, projectile.owner, projectile.whoAmI);
    //                    projectile.netUpdate = true;
    //                }
    //                if (projectile.ai[0] > 300f)
    //                {
    //                    projectile.ai[0] = 0f;
    //                    projectile.ai[1] = 0f;
    //                }
    //            }
    //            else
    //            {
    //                if (projectile.ai[0] > 0f && vector5.Length() < 200f && vector5.Length() > 150f && projectile.ai[0] < 1000f)
    //                {
    //                    projectile.ai[0] = 1000f;
    //                }
    //                projectile.ai[0] += 1f;
    //                if (projectile.ai[0] > 1060f)
    //                {
    //                    if (projectile.ai[0] > 1000f && vector5.Length() > 50f || projectile.ai[0] > 1100f)
    //                    {
    //                        vector5 = vector5.PerfectNormalize();
    //                        projectile.rotation = vector5.ToRotation();
    //                        projectile.velocity = vector5 * 15f;
    //                        projectile.ai[0] = -20f;
    //                        projectile.ai[1] += 1f;
    //                        npc.netUpdate = true;
    //                    }
    //                }
    //                else if (projectile.ai[0] > 0f)
    //                {
    //                    if (projectile.ai[0] > 20f)
    //                    {
    //                        projectile.RotationSpeed(vector5.ToRotation(), 0.1f);
    //                    }
    //                    if (vector5.Length() > 200f)
    //                    {
    //                        vector5 = vector5.PerfectNormalize();
    //                        projectile.velocity = (projectile.velocity * 20f + vector5 * num16) / 21f;
    //                    }
    //                    else
    //                    {
    //                        vector5 = vector5.PerfectNormalize();
    //                        projectile.velocity = (projectile.velocity * 20f - vector5 * num16) / 21f;
    //                    }
    //                }
    //            }
    //        }
    //        else
    //        {
    //            projectile.rotation = projectile.velocity.ToRotation();
    //            projectile.ai[0] = 0f;
    //            Vector2 vector7 = player.Center - projectile.Center;
    //            if (player.velocity.Length() > 20f || vector7.Length() > 300f)
    //            {
    //                vector7 = player.Center - new Vector2(0f, 100f) - projectile.Center;
    //                float num18 = num16 + player.velocity.Length();
    //                projectile.netUpdate = true;
    //                if (num17 > 3000f)
    //                {
    //                    projectile.Center = player.Center;
    //                }
    //                vector7 = vector7.PerfectNormalize();
    //                vector7 *= num18;
    //                float num19 = 60f;
    //                projectile.velocity = (projectile.velocity * num19 + vector7) / (num19 + 1f);
    //            }
    //            else
    //            {
    //                float num20 = 2f;
    //                projectile.netUpdate = true;
    //                vector7.Y -= 350f;
    //                if (num17 > 3000f)
    //                {
    //                    projectile.Center = player.Center;
    //                }
    //                if (projectile.velocity.Length() <= 5f)
    //                {
    //                    projectile.velocity.X *= 1.02f;
    //                }
    //                if (vector7.Length() > 300f)
    //                {
    //                    vector7.Normalize();
    //                    vector7 *= num20;
    //                    float num21 = 10f;
    //                    projectile.velocity = (projectile.velocity * num21 + vector7) / (num21 + 1f);
    //                }
    //            }
    //        }
    //        return false;
    //    }
    //    if (projectile.type == 266)
    //    {
    //        if (Main.player[projectile.owner].dead)
    //        {
    //            Main.player[projectile.owner].slime = false;
    //        }
    //        if (Main.player[projectile.owner].slime)
    //        {
    //            projectile.timeLeft = 2;
    //        }
    //        projectile.friendly = target;
    //        projectile.frameCounter++;
    //        if (projectile.frameCounter > 8)
    //        {
    //            projectile.frameCounter = 0;
    //            projectile.frame++;
    //        }
    //        projectile.rotation = 0f;
    //        if (projectile.ai[1] == 0f)
    //        {
    //            if (projectile.frame > 1)
    //            {
    //                projectile.frame = 0;
    //            }
    //        }
    //        else if (projectile.frame < 2 || projectile.frame > 5)
    //        {
    //            projectile.frame = 2;
    //        }
    //        if (target)
    //        {
    //            Vector2 vector8 = npc.Center - projectile.Center;
    //            if (vector8.X > 0f)
    //            {
    //                projectile.spriteDirection = -1;
    //            }
    //            else
    //            {
    //                projectile.spriteDirection = 0;
    //            }
    //            float num22 = Math.Abs(vector8.X) / 20f;
    //            if (num22 > 10f)
    //            {
    //                num22 = 10f;
    //            }
    //            else if (num22 < 2f)
    //            {
    //                num22 = 2f;
    //            }
    //            float num23 = Math.Abs(vector8.X + vector8.Y) / 50f;
    //            if (num23 > 15f)
    //            {
    //                num23 = 15f;
    //            }
    //            else if (num23 < 6f)
    //            {
    //                num23 = 6f;
    //            }
    //            if (projectile.ai[1] == 0f)
    //            {
    //                if (projectile.velocity.Y == 0f)
    //                {
    //                    projectile.velocity.X = 0f;
    //                    if (vector8.Y > 50f || Math.Abs(vector8.X) > 5f)
    //                    {
    //                        projectile.velocity.Y = 0f - num23;
    //                        vector8 = vector8.PerfectNormalize();
    //                        projectile.velocity.X = vector8.X * num22;
    //                    }
    //                }
    //                else if (projectile.velocity.Y < 5f)
    //                {
    //                    projectile.velocity.Y += 0.4f;
    //                }
    //                if (vector8.Y < -150f || Math.Abs(vector8.X) > 500f)
    //                {
    //                    projectile.ai[1] = 1f;
    //                }
    //                ReboundSpeed = 0f;
    //            }
    //            else
    //            {
    //                if (num23 < 12f)
    //                {
    //                    num23 = 12f;
    //                }
    //                projectile.velocity = (projectile.velocity * 20f + vector8.PerfectNormalize() * num23) / 21f;
    //                projectile.rotation = projectile.velocity.X * 0.05f;
    //                if (Math.Abs(vector8.Y) < 5f || Math.Abs(vector8.X) < 5f)
    //                {
    //                    for (int j = 0; j < 7; j++)
    //                    {
    //                        if (Main.tile[(int)npc.Center.X / 16, (int)((projectile.Center.Y + projectile.height) / 16f) + j].HasTile)
    //                        {
    //                            projectile.ai[1] = 0f;
    //                        }
    //                    }
    //                }
    //                ReboundSpeed = 0.5f;
    //            }
    //        }
    //        else
    //        {
    //            ReboundSpeed = 0f;
    //            int num24 = 0;
    //            for (int k = 0; k < 1000; k++)
    //            {
    //                if (Main.projectile[k].active && Main.projectile[k].type == projectile.type && k <= projectile.whoAmI && projectile.owner == Main.myPlayer)
    //                {
    //                    num24++;
    //                }
    //            }
    //            Vector2 vector9 = player.Center - projectile.Center;
    //            vector9.X -= (10 + 40 * num24) * player.direction;
    //            float num25 = Math.Abs(vector9.X) / 50f;
    //            if (num25 > 4f)
    //            {
    //                num25 = 4f;
    //            }
    //            else if (num25 < 0.5f)
    //            {
    //                num25 = 0.5f;
    //            }
    //            float num26 = Math.Abs(vector9.X + vector9.Y) / 50f;
    //            if (num26 > 15f)
    //            {
    //                num26 = 15f;
    //            }
    //            else if (num26 < 6f)
    //            {
    //                num26 = 6f;
    //            }
    //            if (projectile.velocity.X > 0f)
    //            {
    //                projectile.spriteDirection = -1;
    //            }
    //            else
    //            {
    //                projectile.spriteDirection = 0;
    //            }
    //            if (projectile.ai[1] == 0f)
    //            {
    //                if (projectile.velocity.Y == 0f)
    //                {
    //                    projectile.velocity.X = 0f;
    //                    if (vector9.Y > 50f || Math.Abs(vector9.X) > 5f)
    //                    {
    //                        projectile.velocity.Y = 0f - num26;
    //                        vector9 = vector9.PerfectNormalize();
    //                        projectile.velocity.X = vector9.X * num25;
    //                    }
    //                    else if (vector9.X > 0f)
    //                    {
    //                        projectile.spriteDirection = -1;
    //                    }
    //                    else
    //                    {
    //                        projectile.spriteDirection = 0;
    //                    }
    //                }
    //                else if (projectile.velocity.Y < 5f)
    //                {
    //                    projectile.velocity.Y += 0.4f;
    //                }
    //                if (vector9.Y < -150f || Math.Abs(vector9.X) > 500f)
    //                {
    //                    projectile.ai[1] = 1f;
    //                }
    //            }
    //            else
    //            {
    //                projectile.velocity = (projectile.velocity * 20f + vector9.PerfectNormalize() * num26) / 21f;
    //                projectile.rotation = projectile.velocity.X * 0.05f;
    //                if (player.velocity.Y == 0f && (Math.Abs(vector9.Y) < 5f || Math.Abs(vector9.X) < 5f))
    //                {
    //                    for (int l = 0; l < 7; l++)
    //                    {
    //                        if (Main.tile[(int)((player.Center.X - (10 + 40 * num24) * player.direction) / 16f), (int)((projectile.Center.Y + projectile.height) / 16f) + l].HasTile)
    //                        {
    //                            projectile.ai[1] = 0f;
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //        projectile.tileCollide = projectile.ai[1] == 0f;
    //        return false;
    //    }
    //    if (projectile.type == 407)
    //    {
    //        if (Main.player[projectile.owner].dead)
    //        {
    //            Main.player[projectile.owner].sharknadoMinion = false;
    //        }
    //        if (Main.player[projectile.owner].sharknadoMinion)
    //        {
    //            projectile.timeLeft = 2;
    //        }
    //        float num27 = 12f;
    //        float num28 = (player.Center - projectile.Center).Length();
    //        projectile.frameCounter++;
    //        if (projectile.frameCounter > 4)
    //        {
    //            projectile.frame++;
    //            projectile.frameCounter = 0;
    //        }
    //        if (projectile.frame >= 6)
    //        {
    //            projectile.frame = 0;
    //        }
    //        projectile.spriteDirection = -1;
    //        projectile.rotation = projectile.velocity.X * 0.05f;
    //        projectile.ai[0] += 1f;
    //        if (target)
    //        {
    //            Vector2 vector10 = npc.Center - projectile.Center;
    //            if (vector10.Length() > 300f)
    //            {
    //                vector10 = vector10.PerfectNormalize();
    //                projectile.velocity = (projectile.velocity * 20f + vector10 * num27) / 21f;
    //            }
    //            else
    //            {
    //                vector10 = vector10.PerfectNormalize();
    //                projectile.velocity = (projectile.velocity * 20f + -vector10 * num27) / 21f;
    //            }
    //            if (projectile.ai[0] >= 90f)
    //            {
    //                Projectile.NewProjectile(projectile.GetSource_FromAI(), projectile.Center, vector10 * 7f, 408, projectile.damage, projectile.knockBack, projectile.owner);
    //                projectile.netUpdate = true;
    //                projectile.ai[0] = 0f;
    //            }
    //        }
    //        else
    //        {
    //            Vector2 vector11 = player.Center - projectile.Center;
    //            if (player.velocity.Length() > 20f || vector11.Length() > 300f)
    //            {
    //                vector11 = player.Center - new Vector2(0f, 100f) - projectile.Center;
    //                float num29 = num27 + player.velocity.Length();
    //                projectile.netUpdate = true;
    //                if (num28 > 3000f)
    //                {
    //                    projectile.Center = player.Center;
    //                }
    //                vector11 = vector11.PerfectNormalize();
    //                vector11 *= num29;
    //                float num30 = 60f;
    //                projectile.velocity = (projectile.velocity * num30 + vector11) / (num30 + 1f);
    //            }
    //            else
    //            {
    //                float num31 = 2f;
    //                projectile.netUpdate = true;
    //                vector11.Y -= 350f;
    //                if (num28 > 3000f)
    //                {
    //                    projectile.Center = player.Center;
    //                }
    //                if (projectile.velocity.Length() <= 5f)
    //                {
    //                    projectile.velocity.X *= 1.02f;
    //                }
    //                if (vector11.Length() > 300f)
    //                {
    //                    vector11.Normalize();
    //                    vector11 *= num31;
    //                    float num32 = 10f;
    //                    projectile.velocity = (projectile.velocity * num32 + vector11) / (num32 + 1f);
    //                }
    //            }
    //        }
    //        return false;
    //    }
    //    if (projectile.type == 373)
    //    {
    //        if (Main.player[projectile.owner].dead)
    //        {
    //            Main.player[projectile.owner].hornetMinion = false;
    //        }
    //        if (Main.player[projectile.owner].hornetMinion)
    //        {
    //            projectile.timeLeft = 2;
    //        }
    //        float num33 = 12f;
    //        float num34 = (player.Center - projectile.Center).Length();
    //        projectile.frameCounter++;
    //        if (projectile.frameCounter > 4)
    //        {
    //            projectile.frame++;
    //            projectile.frameCounter = 0;
    //        }
    //        if (projectile.frame >= 3)
    //        {
    //            projectile.frame = 0;
    //        }
    //        projectile.rotation = projectile.velocity.X * 0.05f;
    //        projectile.ai[0] += 1f;
    //        if (target)
    //        {
    //            Vector2 vector12 = npc.Center - projectile.Center + new Vector2(0f, 10f);
    //            if (vector12.X > 0f)
    //            {
    //                projectile.spriteDirection = -1;
    //            }
    //            else
    //            {
    //                projectile.spriteDirection = 0;
    //            }
    //            if (vector12.Length() > 300f)
    //            {
    //                vector12 = vector12.PerfectNormalize();
    //                projectile.velocity = (projectile.velocity * 20f + vector12 * num33) / 21f;
    //            }
    //            else
    //            {
    //                vector12 = vector12.PerfectNormalize();
    //                projectile.velocity = (projectile.velocity * 20f + -vector12 * num33) / 21f;
    //            }
    //            if (projectile.ai[0] >= 60f)
    //            {
    //                for (int m = 0; m < 3; m++)
    //                {
    //                    Vector2 vector13 = vector12.RotatedBy(Main.rand.NextFloat(-0.1f, 0.1f));
    //                    Projectile.NewProjectile(projectile.GetSource_FromAI(), projectile.Center + new Vector2(0f, 10f), vector13 * 20f, 374, projectile.damage, projectile.knockBack, projectile.owner);
    //                }
    //                projectile.netUpdate = true;
    //                projectile.ai[0] = 0f;
    //            }
    //        }
    //        else
    //        {
    //            if (projectile.velocity.X > 0f)
    //            {
    //                projectile.spriteDirection = -1;
    //            }
    //            else
    //            {
    //                projectile.spriteDirection = 0;
    //            }
    //            Vector2 vector14 = player.Center - projectile.Center;
    //            if (player.velocity.Length() > 20f || vector14.Length() > 300f)
    //            {
    //                vector14 = player.Center - new Vector2(0f, 100f) - projectile.Center;
    //                float num35 = num33 + player.velocity.Length();
    //                projectile.netUpdate = true;
    //                if (num34 > 3000f)
    //                {
    //                    projectile.Center = player.Center;
    //                }
    //                vector14 = vector14.PerfectNormalize();
    //                vector14 *= num35;
    //                float num36 = 60f;
    //                projectile.velocity = (projectile.velocity * num36 + vector14) / (num36 + 1f);
    //            }
    //            else
    //            {
    //                float num37 = 2f;
    //                projectile.netUpdate = true;
    //                vector14.Y -= 350f;
    //                if (num34 > 3000f)
    //                {
    //                    projectile.Center = player.Center;
    //                }
    //                if (projectile.velocity.Length() <= 5f)
    //                {
    //                    projectile.velocity.X *= 1.02f;
    //                }
    //                if (vector14.Length() > 300f)
    //                {
    //                    vector14.Normalize();
    //                    vector14 *= num37;
    //                    float num38 = 10f;
    //                    projectile.velocity = (projectile.velocity * num38 + vector14) / (num38 + 1f);
    //                }
    //            }
    //        }
    //        return false;
    //    }
    //    if (projectile.type == 408)
    //    {
    //        float num39 = 12f;
    //        projectile.frameCounter++;
    //        if (projectile.frameCounter > 4)
    //        {
    //            projectile.frame++;
    //            projectile.frameCounter = 0;
    //        }
    //        if (projectile.frame >= 2)
    //        {
    //            projectile.frame = 0;
    //        }
    //        projectile.alpha -= 5;
    //        if (target && projectile.ai[0] < 60f)
    //        {
    //            Vector2 vector15 = npc.Center - projectile.Center;
    //            projectile.rotation = vector15.ToRotation();
    //            if (vector15.X > 0f)
    //            {
    //                projectile.spriteDirection = 0;
    //            }
    //            else
    //            {
    //                projectile.spriteDirection = 1;
    //            }
    //            if (vector15.Length() < 250f)
    //            {
    //                projectile.ai[0] += 1f;
    //                if (projectile.ai[0] % 15f == 0f)
    //                {
    //                    int num40 = Projectile.NewProjectile(projectile.GetSource_FromAI(), projectile.Center, vector15.PerfectNormalize() * 5f + projectile.velocity / 3f, ModContent.ProjectileType<SummonBubble>(), projectile.damage / 3, projectile.knockBack, projectile.owner, 0f, 1f);
    //                    Main.projectile[num40].tileCollide = false;
    //                    projectile.netUpdate = true;
    //                }
    //            }
    //            if (vector15.Length() > 200f)
    //            {
    //                vector15 = vector15.PerfectNormalize();
    //                projectile.velocity = (projectile.velocity * 20f + vector15 * num39) / 21f;
    //            }
    //            else
    //            {
    //                vector15 = vector15.PerfectNormalize();
    //                projectile.velocity = (projectile.velocity * 20f + -vector15 * num39) / 21f;
    //            }
    //        }
    //        else
    //        {
    //            projectile.ai[0] = 60f;
    //            projectile.Track(1000, 20f, 15f);
    //            if (projectile.velocity.X > 0f)
    //            {
    //                projectile.spriteDirection = 0;
    //            }
    //            else
    //            {
    //                projectile.spriteDirection = 1;
    //            }
    //            projectile.rotation = projectile.velocity.ToRotation();
    //        }
    //        return false;
    //    }
    //    if (projectile.type == 533)
    //    {
    //        if (Main.player[projectile.owner].dead)
    //        {
    //            Main.player[projectile.owner].DeadlySphereMinion = false;
    //        }
    //        if (Main.player[projectile.owner].DeadlySphereMinion)
    //        {
    //            projectile.timeLeft = 2;
    //        }
    //        float num41 = 12f;
    //        float num42 = (player.Center - projectile.Center).Length();
    //        projectile.frameCounter++;
    //        if (projectile.frameCounter > 4)
    //        {
    //            projectile.frame++;
    //            projectile.frameCounter = 0;
    //        }
    //        if (projectile.ai[1] <= 5f)
    //        {
    //            if (projectile.frame >= 10)
    //            {
    //                projectile.frame = 5;
    //            }
    //            else if (projectile.frame < 5)
    //            {
    //                projectile.velocity = Vector2.Zero;
    //            }
    //        }
    //        else if (projectile.ai[1] <= 6f)
    //        {
    //            if (projectile.frame <= 10)
    //            {
    //                projectile.frame = 11;
    //            }
    //            if (projectile.frame >= 17)
    //            {
    //                projectile.frame = 14;
    //            }
    //            else if (projectile.frame < 14)
    //            {
    //                projectile.velocity = Vector2.Zero;
    //            }
    //        }
    //        else if (projectile.ai[1] <= 11f)
    //        {
    //            if (projectile.frame <= 16)
    //            {
    //                projectile.frame = 17;
    //            }
    //            if (projectile.frame >= 21)
    //            {
    //                projectile.frame = 17;
    //            }
    //        }
    //        if (projectile.ai[1] > 6f)
    //        {
    //            for (int n = 0; n < 4; n++)
    //            {
    //                int type = Utils.SelectRandom(Main.rand, 226, 228, 75);
    //                Dust obj = Main.dust[Dust.NewDust(projectile.Center, 0, 0, type)];
    //                Vector2 vector16 = Vector2.One.RotatedBy(n * ((float)Math.PI / 2f)).RotatedBy(projectile.rotation);
    //                obj.position = projectile.Center + vector16 * 10f;
    //                obj.velocity = vector16 * 1f;
    //                obj.scale = 0.6f + Main.rand.NextFloat() * 0.5f;
    //                obj.noGravity = true;
    //            }
    //        }
    //        if (projectile.localAI[0] < 12f)
    //        {
    //            projectile.localAI[0] += 1f;
    //            projectile.velocity = Vector2.Zero;
    //        }
    //        projectile.DPoroj().Times[0] += 0.25f;
    //        projectile.spriteDirection = -1;
    //        projectile.friendly = true;
    //        if (target)
    //        {
    //            Vector2 vector17 = npc.Center - projectile.Center;
    //            if (projectile.ai[1] <= 5f)
    //            {
    //                projectile.localNPCHitCooldown = 60;
    //                projectile.rotation += projectile.velocity.Length() * 0.08f;
    //                if (projectile.ai[0] > 0f && vector17.Length() < 200f && vector17.Length() > 150f && projectile.ai[0] < 1000f)
    //                {
    //                    projectile.ai[0] = 1000f;
    //                }
    //                projectile.ai[0] += 1f;
    //                if (projectile.ai[0] > 1060f)
    //                {
    //                    if (projectile.ai[0] > 1000f && vector17.Length() > 50f || projectile.ai[0] > 1100f)
    //                    {
    //                        vector17 = vector17.PerfectNormalize();
    //                        projectile.rotation = vector17.ToRotation();
    //                        if (projectile.ai[1] < 5f)
    //                        {
    //                            projectile.velocity = vector17 * 40f;
    //                        }
    //                        else
    //                        {
    //                            projectile.localAI[0] = 0f;
    //                        }
    //                        projectile.ai[0] = 0f;
    //                        projectile.ai[1] += 1f;
    //                        npc.netUpdate = true;
    //                    }
    //                }
    //                else if (projectile.ai[0] > 0f)
    //                {
    //                    if (vector17.Length() > 200f)
    //                    {
    //                        vector17 = vector17.PerfectNormalize();
    //                        projectile.velocity = (projectile.velocity * 20f + vector17 * num41) / 21f;
    //                    }
    //                    else
    //                    {
    //                        vector17 = vector17.PerfectNormalize();
    //                        projectile.velocity = (projectile.velocity * 20f - vector17 * num41) / 21f;
    //                    }
    //                }
    //            }
    //            else if (projectile.ai[1] <= 6f)
    //            {
    //                projectile.localNPCHitCooldown = 15;
    //                projectile.rotation += 0.3f;
    //                vector17 = vector17.PerfectNormalize();
    //                projectile.velocity = (projectile.velocity * 20f + vector17 * num41) / 21f;
    //                projectile.ai[0] += 1f;
    //                if (projectile.ai[0] > 180f)
    //                {
    //                    projectile.ai[1] += 1f;
    //                    projectile.ai[0] = 0f;
    //                    projectile.localAI[0] = 0f;
    //                }
    //            }
    //            else
    //            {
    //                projectile.localNPCHitCooldown = 60;
    //                projectile.rotation += projectile.velocity.Length() * 0.08f;
    //                if (projectile.ai[0] > 0f && vector17.Length() < 200f && vector17.Length() > 150f && projectile.ai[0] < 1000f)
    //                {
    //                    projectile.ai[0] = 1000f;
    //                }
    //                projectile.ai[0] += 1f;
    //                if (projectile.ai[0] > 1060f)
    //                {
    //                    if (projectile.ai[0] > 1000f && vector17.Length() > 50f || projectile.ai[0] > 1100f)
    //                    {
    //                        vector17 = vector17.PerfectNormalize();
    //                        projectile.rotation = vector17.ToRotation();
    //                        projectile.velocity = vector17 * 40f;
    //                        projectile.ai[1] += 1f;
    //                        projectile.ai[0] = 0f;
    //                        npc.netUpdate = true;
    //                    }
    //                }
    //                else if (projectile.ai[0] > 0f)
    //                {
    //                    if (vector17.Length() > 200f)
    //                    {
    //                        vector17 = vector17.PerfectNormalize();
    //                        projectile.velocity = (projectile.velocity * 20f + vector17 * num41) / 21f;
    //                    }
    //                    else
    //                    {
    //                        vector17 = vector17.PerfectNormalize();
    //                        projectile.velocity = (projectile.velocity * 20f - vector17 * num41) / 21f;
    //                    }
    //                }
    //            }
    //            if (projectile.ai[1] >= 12f)
    //            {
    //                projectile.localAI[0] = 0f;
    //                projectile.ai[1] = 0f;
    //            }
    //        }
    //        else
    //        {
    //            projectile.rotation += projectile.velocity.Length() * 0.08f;
    //            projectile.ai[0] = 0f;
    //            Vector2 vector18 = player.Center - projectile.Center;
    //            if (player.velocity.Length() > 20f || vector18.Length() > 300f)
    //            {
    //                vector18 = player.Center - new Vector2(0f, 100f) - projectile.Center;
    //                float num43 = num41 + player.velocity.Length();
    //                projectile.netUpdate = true;
    //                if (num42 > 3000f)
    //                {
    //                    projectile.Center = player.Center;
    //                }
    //                vector18 = vector18.PerfectNormalize();
    //                vector18 *= num43;
    //                float num44 = 60f;
    //                projectile.velocity = (projectile.velocity * num44 + vector18) / (num44 + 1f);
    //            }
    //            else
    //            {
    //                float num45 = 2f;
    //                projectile.netUpdate = true;
    //                vector18.Y -= 350f;
    //                if (num42 > 3000f)
    //                {
    //                    projectile.Center = player.Center;
    //                }
    //                if (projectile.velocity.Length() <= 5f)
    //                {
    //                    projectile.velocity.X *= 1.02f;
    //                }
    //                if (vector18.Length() > 300f)
    //                {
    //                    vector18.Normalize();
    //                    vector18 *= num45;
    //                    float num46 = 10f;
    //                    projectile.velocity = (projectile.velocity * num46 + vector18) / (num46 + 1f);
    //                }
    //            }
    //        }
    //        return false;
    //    }
    //    if (projectile.type == 951)
    //    {
    //        if (Main.player[projectile.owner].dead)
    //        {
    //            Main.player[projectile.owner].flinxMinion = false;
    //        }
    //        if (Main.player[projectile.owner].flinxMinion)
    //        {
    //            projectile.timeLeft = 2;
    //        }
    //        projectile.friendly = target;
    //        projectile.rotation = 0f;
    //        if (projectile.ai[1] == 1f)
    //        {
    //            projectile.DPoroj().Times[1] += 1f;
    //            if (projectile.DPoroj().Times[1] > 4f)
    //            {
    //                projectile.DPoroj().Times[1] = 0f;
    //                projectile.frame++;
    //            }
    //            if (projectile.frame > 2)
    //            {
    //                projectile.frame = 0;
    //            }
    //        }
    //        else
    //        {
    //            projectile.DPoroj().Times[1] += Math.Abs(projectile.velocity.X);
    //            if (projectile.DPoroj().Times[1] > 8f)
    //            {
    //                projectile.DPoroj().Times[1] -= 8f;
    //                projectile.frame++;
    //            }
    //            if (projectile.frame > 11)
    //            {
    //                projectile.frame = 0;
    //            }
    //        }
    //        projectile.tileCollide = projectile.ai[1] == 0f;
    //        if (target)
    //        {
    //            ReboundSpeed = 0.5f;
    //            Vector2 vector19 = npc.Center - projectile.Center;
    //            if (vector19.X > 0f)
    //            {
    //                projectile.spriteDirection = 0;
    //            }
    //            else
    //            {
    //                projectile.spriteDirection = -1;
    //            }
    //            float num47 = Math.Abs(vector19.X) / 20f;
    //            if (num47 > 10f)
    //            {
    //                num47 = 10f;
    //            }
    //            else if (num47 < 2f)
    //            {
    //                num47 = 2f;
    //            }
    //            float num48 = Math.Abs(vector19.X + vector19.Y) / 50f;
    //            if (num48 > 25f)
    //            {
    //                num48 = 25f;
    //            }
    //            else if (num48 < 6f)
    //            {
    //                num48 = 6f;
    //            }
    //            if (projectile.ai[1] == 0f)
    //            {
    //                if (projectile.velocity.Y == 0f && (vector19.Y < -100f || Math.Abs(vector19.X) > 5f && projectile.velocity.X == 0f))
    //                {
    //                    projectile.velocity.Y = -12f;
    //                }
    //                if (projectile.velocity.Y < 10f)
    //                {
    //                    projectile.velocity.Y += 0.4f;
    //                }
    //                if (vector19.Y < -150f || Math.Abs(vector19.X) > 500f)
    //                {
    //                    projectile.ai[1] = 1f;
    //                }
    //                if (vector19.Y > projectile.height / 2)
    //                {
    //                    projectile.tileCollide = false;
    //                }
    //                vector19 = vector19.PerfectNormalize();
    //                projectile.velocity.X = vector19.X * num47;
    //                if (projectile.velocity.X > 0f)
    //                {
    //                    projectile.spriteDirection = 0;
    //                }
    //                else
    //                {
    //                    projectile.spriteDirection = -1;
    //                }
    //            }
    //            else
    //            {
    //                if (num48 < 18f)
    //                {
    //                    num48 = 18f;
    //                }
    //                projectile.velocity = (projectile.velocity * 20f + vector19.PerfectNormalize() * num48) / 21f;
    //                projectile.rotation = projectile.velocity.X * 0.05f;
    //                if (Math.Abs(vector19.Y) < 5f || Math.Abs(vector19.X) < 5f)
    //                {
    //                    for (int num49 = 0; num49 < 7; num49++)
    //                    {
    //                        if (Main.tile[(int)npc.Center.X / 16, (int)((projectile.Center.Y + projectile.height) / 16f) + num49].HasTile)
    //                        {
    //                            projectile.ai[1] = 0f;
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //        else
    //        {
    //            ReboundSpeed = 0f;
    //            int num50 = 0;
    //            for (int num51 = 0; num51 < 1000; num51++)
    //            {
    //                if (Main.projectile[num51].active && Main.projectile[num51].type == projectile.type && num51 <= projectile.whoAmI && projectile.owner == Main.myPlayer)
    //                {
    //                    num50++;
    //                }
    //            }
    //            Vector2 vector20 = player.Center - projectile.Center;
    //            vector20.X -= (10 + 80 * num50) * player.direction;
    //            float num52 = Math.Abs(vector20.X) / 50f;
    //            if (num52 > 12f)
    //            {
    //                num52 = 12f;
    //            }
    //            else if (num52 < 5f)
    //            {
    //                num52 = 5f;
    //            }
    //            if (projectile.velocity.X > 0f)
    //            {
    //                projectile.spriteDirection = 0;
    //            }
    //            else
    //            {
    //                projectile.spriteDirection = -1;
    //            }
    //            if (projectile.ai[1] == 0f)
    //            {
    //                if (projectile.velocity.Y == 0f && (vector20.Y < -100f || Math.Abs(vector20.X) > 5f && projectile.velocity.X == 0f))
    //                {
    //                    projectile.velocity.Y = -12f;
    //                }
    //                if (projectile.velocity.Y < 10f)
    //                {
    //                    projectile.velocity.Y += 0.4f;
    //                }
    //                if (vector20.Y < -150f || Math.Abs(vector20.X) > 500f)
    //                {
    //                    projectile.ai[1] = 1f;
    //                }
    //                if (vector20.Length() <= 8f)
    //                {
    //                    projectile.frame = 0;
    //                }
    //                vector20 = vector20.PerfectNormalize();
    //                projectile.velocity.X = vector20.X * num52;
    //                if (projectile.velocity.Length() < 2f)
    //                {
    //                    if (player.direction == 1)
    //                    {
    //                        projectile.spriteDirection = 0;
    //                    }
    //                    else
    //                    {
    //                        projectile.spriteDirection = -1;
    //                    }
    //                }
    //                if (vector20.Y > projectile.height / 2)
    //                {
    //                    projectile.tileCollide = false;
    //                }
    //            }
    //            else
    //            {
    //                if (projectile.velocity.Length() < 2f)
    //                {
    //                    if (player.direction == 1)
    //                    {
    //                        projectile.spriteDirection = 0;
    //                    }
    //                    else
    //                    {
    //                        projectile.spriteDirection = -1;
    //                    }
    //                }
    //                projectile.velocity = (projectile.velocity * 20f + vector20.PerfectNormalize() * 12f) / 21f;
    //                projectile.rotation = projectile.velocity.X * 0.05f;
    //                if (player.velocity.Y == 0f && (Math.Abs(vector20.Y) < 5f || Math.Abs(vector20.X) < 5f))
    //                {
    //                    for (int num53 = 0; num53 < 7; num53++)
    //                    {
    //                        if (Main.tile[(int)((player.Center.X - (10 + 80 * num50) * player.direction) / 16f), (int)((projectile.Center.Y + projectile.height) / 16f) + num53].HasTile)
    //                        {
    //                            projectile.ai[1] = 0f;
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //        return false;
    //    }
    //    if (projectile.type >= 191 && projectile.type <= 194)
    //    {
    //        if (Main.player[projectile.owner].dead)
    //        {
    //            Main.player[projectile.owner].pygmy = false;
    //        }
    //        if (Main.player[projectile.owner].pygmy)
    //        {
    //            projectile.timeLeft = 2;
    //        }
    //        projectile.friendly = target;
    //        projectile.rotation = 0f;
    //        if (projectile.ai[1] == 1f)
    //        {
    //            if (!target && projectile.frame < 12)
    //            {
    //                projectile.frame = Main.rand.Next(12, 18);
    //            }
    //            if (target && projectile.ai[0] > 0f)
    //            {
    //                if (projectile.ai[0] < 5f)
    //                {
    //                    projectile.frame = 1;
    //                }
    //                else if (projectile.ai[0] < 10f)
    //                {
    //                    projectile.frame = 2;
    //                }
    //                else
    //                {
    //                    projectile.frame = 3;
    //                }
    //            }
    //            else if (projectile.frame < 12)
    //            {
    //                projectile.frame = 0;
    //            }
    //        }
    //        else
    //        {
    //            projectile.DPoroj().Times[1] += Math.Abs(projectile.velocity.X);
    //            if (projectile.DPoroj().Times[1] > 8f)
    //            {
    //                projectile.DPoroj().Times[1] -= 8f;
    //                projectile.frame++;
    //            }
    //            if (!target || projectile.ai[0] == 0f)
    //            {
    //                if (projectile.frame > 11)
    //                {
    //                    projectile.frame = 5;
    //                }
    //                if (projectile.frame < 5)
    //                {
    //                    projectile.frame = 5;
    //                }
    //                if (projectile.velocity.Y != 0f)
    //                {
    //                    projectile.frame = 4;
    //                }
    //                else if (Math.Abs(projectile.velocity.X) <= 0.01f)
    //                {
    //                    projectile.frame = 0;
    //                }
    //            }
    //            else if (projectile.ai[0] < 10f)
    //            {
    //                projectile.frame = 1;
    //            }
    //            else if (projectile.ai[0] < 20f)
    //            {
    //                projectile.frame = 2;
    //            }
    //            else
    //            {
    //                projectile.frame = 3;
    //            }
    //        }
    //        projectile.tileCollide = projectile.ai[1] == 0f;
    //        if (target)
    //        {
    //            ReboundSpeed = 0.5f;
    //            Vector2 vector21 = npc.Center - projectile.Center;
    //            if (vector21.X > 0f)
    //            {
    //                projectile.spriteDirection = -1;
    //            }
    //            else
    //            {
    //                projectile.spriteDirection = 0;
    //            }
    //            float num54 = Math.Abs(vector21.X) / 20f;
    //            if (num54 > 10f)
    //            {
    //                num54 = 10f;
    //            }
    //            else if (num54 < 2f)
    //            {
    //                num54 = 2f;
    //            }
    //            if (projectile.ai[1] == 0f)
    //            {
    //                if (vector21.Length() > 300f)
    //                {
    //                    projectile.ai[0] = 0f;
    //                    vector21 = vector21.PerfectNormalize();
    //                    projectile.velocity.X = (projectile.velocity.X * 20f + vector21.X * num54) / 21f;
    //                }
    //                else if (vector21.Length() < 30f)
    //                {
    //                    projectile.ai[0] = 0f;
    //                    vector21 = vector21.PerfectNormalize();
    //                    projectile.velocity.X = (projectile.velocity.X * 20f + (0f - vector21.X) * num54) / 21f;
    //                }
    //                else
    //                {
    //                    projectile.ai[0] += 1f;
    //                    if (projectile.ai[0] == 20f)
    //                    {
    //                        int num55 = Projectile.NewProjectile(projectile.GetSource_FromAI(), projectile.Center - new Vector2(0f, 10f), vector21.PerfectNormalize() * 15f, 195, projectile.damage, projectile.knockBack, projectile.owner, 0f, 1f);
    //                        Main.projectile[num55].tileCollide = false;
    //                        projectile.netUpdate = true;
    //                    }
    //                    if (projectile.ai[0] >= 30f)
    //                    {
    //                        projectile.ai[0] = 1f;
    //                        projectile.netUpdate = true;
    //                    }
    //                    projectile.velocity *= 0.9f;
    //                }
    //                if (projectile.velocity.Y < 10f)
    //                {
    //                    projectile.velocity.Y += 0.4f;
    //                }
    //                if (!Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height))
    //                {
    //                    projectile.ai[1] = 1f;
    //                }
    //            }
    //            else
    //            {
    //                if (vector21.Length() < 500f)
    //                {
    //                    projectile.ai[0] += 1f;
    //                    if (projectile.ai[0] == 20f)
    //                    {
    //                        int num56 = Projectile.NewProjectile(projectile.GetSource_FromAI(), projectile.Center - new Vector2(0f, 10f), vector21.PerfectNormalize() * 15f, 195, projectile.damage, projectile.knockBack, projectile.owner, 0f, 1f);
    //                        Main.projectile[num56].tileCollide = false;
    //                        projectile.netUpdate = true;
    //                    }
    //                    if (projectile.ai[0] >= 30f)
    //                    {
    //                        projectile.ai[0] = 1f;
    //                        projectile.netUpdate = true;
    //                    }
    //                    for (int num57 = 0; num57 < 7; num57++)
    //                    {
    //                        if (Main.tile[(int)npc.Center.X / 16, (int)((npc.Center.Y + npc.height) / 16f) + num57].HasTile && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height))
    //                        {
    //                            projectile.ai[1] = 0f;
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    projectile.ai[0] = 0f;
    //                }
    //                if (vector21.Length() > 300f)
    //                {
    //                    vector21 = vector21.PerfectNormalize();
    //                    projectile.velocity = (projectile.velocity * 20f + vector21 * num54) / 21f;
    //                }
    //                else
    //                {
    //                    vector21 = vector21.PerfectNormalize();
    //                    projectile.velocity = (projectile.velocity * 20f + -vector21 * num54) / 21f;
    //                }
    //            }
    //        }
    //        else
    //        {
    //            int num58 = 0;
    //            for (int num59 = 0; num59 < 1000; num59++)
    //            {
    //                if (Main.projectile[num59].active && Main.projectile[num59].type >= 191 && Main.projectile[num59].type <= 194 && num59 <= projectile.whoAmI && projectile.owner == Main.myPlayer)
    //                {
    //                    num58++;
    //                }
    //            }
    //            Vector2 vector22 = player.Center - projectile.Center;
    //            float num60 = Math.Abs(vector22.X) / 50f;
    //            if (num60 > 12f)
    //            {
    //                num60 = 12f;
    //            }
    //            else if (num60 < 5f)
    //            {
    //                num60 = 5f;
    //            }
    //            if (projectile.velocity.X > 0f)
    //            {
    //                projectile.spriteDirection = -1;
    //            }
    //            else
    //            {
    //                projectile.spriteDirection = 0;
    //            }
    //            if (projectile.ai[1] == 0f)
    //            {
    //                ReboundSpeed = 0f;
    //                vector22.X -= (10 + 80 * num58) * player.direction;
    //                if (projectile.velocity.Y == 0f && (vector22.Y < -100f || Math.Abs(vector22.X) > 5f && projectile.velocity.X == 0f))
    //                {
    //                    projectile.velocity.Y = -12f;
    //                }
    //                if (projectile.velocity.Y < 10f)
    //                {
    //                    projectile.velocity.Y += 0.4f;
    //                }
    //                if (vector22.Y < -150f || Math.Abs(vector22.X) > 500f)
    //                {
    //                    projectile.ai[1] = 1f;
    //                }
    //                vector22 = vector22.PerfectNormalize();
    //                projectile.velocity.X = vector22.X * num60;
    //                if (projectile.velocity.Length() < 2f)
    //                {
    //                    if (player.direction == 1)
    //                    {
    //                        projectile.spriteDirection = -1;
    //                    }
    //                    else
    //                    {
    //                        projectile.spriteDirection = 0;
    //                    }
    //                }
    //                if (vector22.Y > projectile.height / 2)
    //                {
    //                    projectile.tileCollide = false;
    //                }
    //            }
    //            else
    //            {
    //                projectile.rotation = projectile.velocity.X * 0.03f;
    //                ReboundSpeed = 0.5f;
    //                float num61 = (player.Center - projectile.Center).Length();
    //                if (player.velocity.Y == 0f)
    //                {
    //                    vector22 = player.Center - projectile.Center;
    //                    vector22.X -= (10 + 80 * num58) * player.direction;
    //                    if (projectile.velocity.Length() < 2f)
    //                    {
    //                        if (player.direction == 1)
    //                        {
    //                            projectile.spriteDirection = -1;
    //                        }
    //                        else
    //                        {
    //                            projectile.spriteDirection = 0;
    //                        }
    //                    }
    //                    projectile.velocity = (projectile.velocity * 20f + vector22.PerfectNormalize() * 12f) / 21f;
    //                    projectile.rotation = projectile.velocity.X * 0.05f;
    //                    if (player.velocity.Y == 0f && (Math.Abs(vector22.Y) < 5f || Math.Abs(vector22.X) < 5f))
    //                    {
    //                        for (int num62 = 0; num62 < 7; num62++)
    //                        {
    //                            if (Main.tile[(int)((player.Center.X - (10 + 80 * num58) * player.direction) / 16f), (int)((projectile.Center.Y + projectile.height) / 16f) + num62].HasTile)
    //                            {
    //                                projectile.ai[1] = 0f;
    //                            }
    //                        }
    //                    }
    //                }
    //                else if (player.velocity.Length() > 20f || vector22.Length() > 300f)
    //                {
    //                    vector22 = player.Center - new Vector2(0f, 100f) - projectile.Center;
    //                    float num63 = num60 + player.velocity.Length();
    //                    projectile.netUpdate = true;
    //                    if (num61 > 3000f)
    //                    {
    //                        projectile.Center = player.Center;
    //                    }
    //                    vector22 = vector22.PerfectNormalize();
    //                    vector22 *= num63;
    //                    float num64 = 60f;
    //                    projectile.velocity = (projectile.velocity * num64 + vector22) / (num64 + 1f);
    //                }
    //                else
    //                {
    //                    float num65 = 2f;
    //                    projectile.netUpdate = true;
    //                    vector22.Y -= 350f;
    //                    if (num61 > 3000f)
    //                    {
    //                        projectile.Center = player.Center;
    //                    }
    //                    if (projectile.velocity.Length() <= 5f)
    //                    {
    //                        projectile.velocity.X *= 1.02f;
    //                    }
    //                    if (vector22.Length() > 300f)
    //                    {
    //                        vector22.Normalize();
    //                        vector22 *= num65;
    //                        float num66 = 10f;
    //                        projectile.velocity = (projectile.velocity * num66 + vector22) / (num66 + 1f);
    //                    }
    //                }
    //            }
    //        }
    //        return false;
    //    }
    //    return base.PreAI(projectile);
    //}

    //public override bool MinionContactDamage(Projectile projectile)
    //{
    //    if (projectile.type == 388)
    //    {
    //        return true;
    //    }
    //    if (projectile.type == 533)
    //    {
    //        return true;
    //    }
    //    return base.MinionContactDamage(projectile);
    //}

    //public override bool? CanDamage(Projectile projectile)
    //{
    //    return base.CanDamage(projectile);
    //}

    //public override bool TileCollideStyle(Projectile projectile, ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
    //{
    //    return base.TileCollideStyle(projectile, ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
    //}

    //public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
    //{
    //    _ = Main.player[projectile.owner];
    //    if (projectile.type == 266 && projectile.ai[1] == 0f)
    //    {
    //        damage = (int)(damage * 1.75f);
    //    }
    //    if (projectile.type == 951)
    //    {
    //        projectile.ai[1] = 0f;
    //    }
    //    if (projectile.type == 533)
    //    {
    //        if (projectile.ai[1] <= 5f)
    //        {
    //            damage = (int)(damage * 2.5f);
    //        }
    //        if (projectile.ai[1] > 6f)
    //        {
    //            damage = (int)(damage * 1.75f);
    //            target.AddBuff(ModContent.BuffType<Charged>(), 300);
    //        }
    //    }
    //}

    //public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
    //{
    //    _ = Main.player[projectile.owner];
    //}

    //public override void Kill(Projectile projectile, int timeLeft)
    //{
    //    if (projectile.type == 389)
    //    {
    //        for (int i = 0; i < Main.rand.Next(3, 7); i++)
    //        {
    //            Dust obj = Main.dust[Dust.NewDust(projectile.Center - projectile.velocity / 2f, 0, 0, 182, 0f, 0f, 100, default, 1.6f)];
    //            obj.velocity *= 1.5f;
    //            obj.noGravity = true;
    //        }
    //    }
    //}

    //public override bool PreDraw(Projectile projectile, ref Color lightColor)
    //{
    //    Texture2D value = DDTextures.VoidStar.Value;
    //    if (projectile.type == 375)
    //    {
    //        SpriteEffects effects = SpriteEffects.None;
    //        if (projectile.spriteDirection == -1)
    //        {
    //            effects = SpriteEffects.FlipHorizontally;
    //        }
    //        Texture2D texture2D = (Texture2D)TextureAssets.Projectile[projectile.type];
    //        Main.spriteBatch.Draw(texture2D, projectile.Center - Main.screenPosition, new Rectangle(0, texture2D.Height / Main.projFrames[projectile.type] * projectile.frame, texture2D.Width, texture2D.Height / Main.projFrames[projectile.type]), lightColor, projectile.rotation, new Vector2(texture2D.Width / 2, texture2D.Height / Main.projFrames[projectile.type] / 2), projectile.scale, effects, 0f);
    //        for (int i = 0; i < 2; i++)
    //        {
    //            if (target)
    //            {
    //                Vector2 vector = npc.Center - projectile.Center;
    //                Color color = new Color(255, 120, 0, 0);
    //                Vector2 position = projectile.Center - Main.screenPosition + vector.PerfectNormalize() * 30f;
    //                Main.spriteBatch.Draw(value, position, null, color * 0.25f, vector.ToRotation(), value.Size() / 2f, new Vector2(projectile.ai[0] / 100f, projectile.ai[0] / 50f), SpriteEffects.None, 0f);
    //                texture2D = DDTextures.Circle[4].Value;
    //                DDHelper.Compression(texture2D, color, vector.ToRotation(), projectile.Opacity, new Vector2(1.5f + projectile.ai[0] / 60f, 1f), 1f, projectile.DPoroj().Times[0], BlendState.Additive);
    //                DDHelper.Compression(texture2D, color, vector.ToRotation(), projectile.Opacity, new Vector2(1.5f + projectile.ai[0] / 60f, 1f), 1f, projectile.DPoroj().Times[0], BlendState.Additive);
    //                Main.spriteBatch.Draw(texture2D, position, new Rectangle(0, 0, texture2D.Width, texture2D.Height), color, 0f, texture2D.Size() / 2f, projectile.ai[0] / 60f, SpriteEffects.None, 0f);
    //                Main.spriteBatch.End();
    //                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
    //            }
    //        }
    //        return false;
    //    }
    //    if (projectile.type == 387)
    //    {
    //        Texture2D texture2D2 = (Texture2D)TextureAssets.Projectile[projectile.type];
    //        Main.spriteBatch.Draw(texture2D2, projectile.Center - Main.screenPosition, new Rectangle(0, texture2D2.Height / Main.projFrames[projectile.type] * projectile.frame, texture2D2.Width, texture2D2.Height / Main.projFrames[projectile.type]), lightColor, projectile.rotation, new Vector2(texture2D2.Width / 2, texture2D2.Height / Main.projFrames[projectile.type] / 2), projectile.scale, SpriteEffects.FlipHorizontally, 0f);
    //        Main.spriteBatch.Draw(TextureAssets.EyeLaserSmall.Value, projectile.Center - Main.screenPosition, new Rectangle(0, texture2D2.Height / Main.projFrames[projectile.type] * projectile.frame, texture2D2.Width, texture2D2.Height / Main.projFrames[projectile.type]), Color.White, projectile.rotation, new Vector2(texture2D2.Width / 2, texture2D2.Height / Main.projFrames[projectile.type] / 2), projectile.scale, SpriteEffects.FlipHorizontally, 0f);
    //        texture2D2 = DDTextures.Starlight.Value;
    //        if (projectile.ai[0] >= 80f && projectile.ai[0] % 10f >= 8f)
    //        {
    //            Main.spriteBatch.Draw(texture2D2, projectile.Center - Main.screenPosition + projectile.rotation.ToRotationVector2().PerfectNormalize() * 30f, null, new Color(255, 0, 0, 0), projectile.rotation, new Vector2(texture2D2.Width / 2, texture2D2.Height / 2), new Vector2(projectile.scale, projectile.scale / 2f), SpriteEffects.None, 0f);
    //            Main.spriteBatch.Draw(texture2D2, projectile.Center - Main.screenPosition + projectile.rotation.ToRotationVector2().PerfectNormalize() * 30f, null, new Color(0, 255, 255, 0), projectile.rotation, new Vector2(texture2D2.Width / 2, texture2D2.Height / 2), new Vector2(projectile.scale, projectile.scale / 2f) / 2f, SpriteEffects.None, 0f);
    //            Main.spriteBatch.Draw(value, projectile.Center - Main.screenPosition + projectile.rotation.ToRotationVector2().PerfectNormalize() * 30f, null, new Color(255, 0, 0, 0) * 0.5f, projectile.rotation, new Vector2(value.Width / 2, value.Height / 2), new Vector2(projectile.scale, projectile.scale / 2f), SpriteEffects.None, 0f);
    //        }
    //        return false;
    //    }
    //    if (projectile.type >= 191 && projectile.type <= 194)
    //    {
    //        SpriteEffects effects2 = SpriteEffects.None;
    //        if (projectile.spriteDirection == -1)
    //        {
    //            effects2 = SpriteEffects.FlipHorizontally;
    //        }
    //        Texture2D texture2D3 = (Texture2D)TextureAssets.Projectile[projectile.type];
    //        Main.spriteBatch.Draw(texture2D3, projectile.Center - Main.screenPosition - new Vector2(0f, 10f), new Rectangle(0, texture2D3.Height / Main.projFrames[projectile.type] * projectile.frame, texture2D3.Width, texture2D3.Height / Main.projFrames[projectile.type]), lightColor, projectile.rotation, new Vector2(texture2D3.Width / 2, texture2D3.Height / Main.projFrames[projectile.type] / 2), projectile.scale, effects2, 0f);
    //        return false;
    //    }
    //    if (projectile.type == 389)
    //    {
    //        Texture2D texture2D4 = (Texture2D)TextureAssets.Projectile[projectile.type];
    //        Main.spriteBatch.Draw(texture2D4, projectile.position + new Vector2(texture2D4.Width / 2, 0f) - Main.screenPosition, null, projectile.GetAlpha(Color.White), projectile.rotation, new Vector2(texture2D4.Width / 2, 0f), projectile.scale, SpriteEffects.None, 0f);
    //        return false;
    //    }
    //    if (projectile.type == 408)
    //    {
    //        float num = projectile.rotation;
    //        if (projectile.spriteDirection == 1)
    //        {
    //            num += (float)Math.PI;
    //        }
    //        Texture2D texture2D5 = (Texture2D)TextureAssets.Projectile[projectile.type];
    //        for (int j = 0; j < projectile.oldPos.Length / 2; j++)
    //        {
    //            Vector2 position2 = projectile.oldPos[j] + new Vector2(projectile.width, projectile.height) / 2f - Main.screenPosition;
    //            Color color2 = new Color(0, 55, 255, 255) * ((projectile.oldPos.Length - j) / (float)projectile.oldPos.Length / 4f);
    //            Main.spriteBatch.Draw(texture2D5, position2, new Rectangle(0, TextureAssets.Projectile[projectile.type].Height() / Main.projFrames[projectile.type] * projectile.frame, TextureAssets.Projectile[projectile.type].Width(), TextureAssets.Projectile[projectile.type].Height() / 2), color2, num, new Vector2(texture2D5.Width, texture2D5.Height / 2) / 2f, projectile.scale, (SpriteEffects)projectile.spriteDirection, 0f);
    //        }
    //        Main.spriteBatch.Draw(texture2D5, projectile.Center - Main.screenPosition, new Rectangle(0, TextureAssets.Projectile[projectile.type].Height() / Main.projFrames[projectile.type] * projectile.frame, TextureAssets.Projectile[projectile.type].Width(), TextureAssets.Projectile[projectile.type].Height() / 2), Color.White, num, new Vector2(texture2D5.Width, texture2D5.Height / 2) / 2f, projectile.scale, (SpriteEffects)projectile.spriteDirection, 0f);
    //        return false;
    //    }
    //    return base.PreDraw(projectile, ref lightColor);
    //}

    //public override void PostDraw(Projectile projectile, Color lightColor)
    //{
    //    Texture2D value = DDTextures.VoidStar.Value;
    //    if (projectile.type == 951 && projectile.ai[1] == 1f)
    //    {
    //        Texture2D value2 = ModContent.Request<Texture2D>("DoxxarsRainbowMagic/Image/云").Value;
    //        Vector2 position = projectile.Center - Main.screenPosition;
    //        position.Y += projectile.height / 1.5f;
    //        Main.spriteBatch.Draw(value2, position, new Rectangle(0, value2.Height / 3 * projectile.frame, value2.Width, value2.Height / 3), lightColor, projectile.rotation, new Vector2(value2.Width / 2, value2.Height / 6), projectile.scale * 2f, SpriteEffects.FlipHorizontally, 0f);
    //    }
    //    if (projectile.type >= 191 && projectile.type <= 194)
    //    {
    //        SpriteEffects effects = SpriteEffects.None;
    //        if (projectile.spriteDirection == -1)
    //        {
    //            effects = SpriteEffects.FlipHorizontally;
    //        }
    //        if (projectile.ai[1] == 1f && target && projectile.ai[0] > 0f)
    //        {
    //            Texture2D value3 = ModContent.Request<Texture2D>("DoxxarsRainbowMagic/Image/长矛").Value;
    //            Vector2 position2 = projectile.Center - Main.screenPosition;
    //            position2.Y += projectile.height * 0.75f;
    //            Main.spriteBatch.Draw(value3, position2, new Rectangle(0, 0, value3.Width, value3.Height), lightColor, projectile.rotation, new Vector2(value3.Width / 2, value3.Height / 2), projectile.scale, effects, 0f);
    //        }
    //    }
    //    if (projectile.type == 533 && projectile.localAI[0] < 12f)
    //    {
    //        Color color = projectile.ai[1] <= 5f ? new Color(255, 0, 0, 0) : !(projectile.ai[1] <= 6f) ? new Color(100, 150, 255, 0) : new Color(155, 55, 55, 0);
    //        Main.spriteBatch.Draw(value, projectile.Center - Main.screenPosition, null, color * 0.5f, projectile.rotation, new Vector2(value.Width / 2, value.Height / 2), new Vector2(projectile.scale, projectile.scale) * projectile.localAI[0] / 4f, SpriteEffects.None, 0f);
    //        Main.spriteBatch.Draw(value, projectile.Center - Main.screenPosition, null, color * 0.5f, projectile.rotation, new Vector2(value.Width / 2, value.Height / 2), new Vector2(projectile.scale, projectile.scale) * projectile.localAI[0] / 8f, SpriteEffects.None, 0f);
    //    }
    //}
}
