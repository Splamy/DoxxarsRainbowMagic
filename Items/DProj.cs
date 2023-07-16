using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoxxarsRainbowMagic.Items;

public static class DProj
{
    public static int NewProjectileChange(IEntitySource spawnSource, Vector2 position, Vector2 velocity, int Type, int Damage, float KnockBack, int Owner = 255, float ai0 = 0f, float ai1 = 0f, float Magnification = 1f)
    {
        int num = Projectile.NewProjectile(spawnSource, position, velocity, Type, Damage, KnockBack, Owner, ai0, ai1);
        Main.projectile[num].DPoroj().Magnification = Magnification;
        return num;
    }

    public static float SolidTileDistanceDetection(this Projectile projectile, float Detectiondistance, float Minusdistance)
    {
        float num = 0f;
        float[] array = new float[160];
        Collision.LaserScan(Main.player[projectile.owner].Center, projectile.velocity, 16f, Detectiondistance, array);
        for (int i = 0; i < array.Length; i++)
        {
            num += array[i];
        }
        num /= array.Length;
        return num - Minusdistance;
    }

    public static DDGlobalProjectile DPoroj(this Projectile projectile)
    {
        return projectile.GetGlobalProjectile<DDGlobalProjectile>();
    }

    public static MeleeProjectile MeleePoroj(this Projectile projectile)
    {
        return projectile.GetGlobalProjectile<MeleeProjectile>();
    }

    public static int OriginalWidth(this Projectile proj)
    {
        if (!ContentSamples.ProjectilesByType.TryGetValue(proj.type, out var value))
        {
            return 0;
        }
        return value.width;
    }

    public static int OriginalHeight(this Projectile proj)
    {
        if (!ContentSamples.ProjectilesByType.TryGetValue(proj.type, out var value))
        {
            return 0;
        }
        return value.height;
    }

    public static float OriginalScale(this Projectile proj)
    {
        if (!ContentSamples.ProjectilesByType.TryGetValue(proj.type, out var value))
        {
            return 0f;
        }
        return value.scale;
    }

    public static Vector2 OriginalSize(this Projectile projectile)
    {
        return new Vector2(projectile.OriginalWidth(), projectile.OriginalHeight());
    }

    public static void ProjScale(this Projectile projectile, float ExtraScale = 0f)
    {
        Player player = projectile.Player();
        projectile.scale = player.ActiveItem().scale + ExtraScale;
        projectile.Resize((int)(projectile.OriginalWidth() * projectile.scale), (int)(projectile.OriginalHeight() * projectile.scale));
        projectile.netUpdate = true;
    }

    public static void ProjScaleChange(this Projectile projectile, float ExtraScale = 0f)
    {
        projectile.Resize((int)(projectile.OriginalWidth() * (projectile.scale + ExtraScale)), (int)(projectile.OriginalHeight() * (projectile.scale + ExtraScale)));
        projectile.netUpdate = true;
    }

    public static Player Player(this Projectile projectile)
    {
        return Main.player[projectile.owner];
    }

    public static void RotationSpeed(this Projectile projectile, float Rotation, float Speed)
    {
        if (Rotation < 0f)
        {
            Rotation += (float)Math.PI * 2f;
        }
        else if ((double)Rotation > 6.283)
        {
            Rotation -= (float)Math.PI * 2f;
        }
        if (projectile.rotation < Rotation)
        {
            if (Rotation - projectile.rotation > (float)Math.PI)
            {
                projectile.rotation -= Speed;
            }
            else
            {
                projectile.rotation += Speed;
            }
        }
        else if (projectile.rotation > Rotation)
        {
            if (projectile.rotation - Rotation > (float)Math.PI)
            {
                projectile.rotation += Speed;
            }
            else
            {
                projectile.rotation -= Speed;
            }
        }
        if (projectile.rotation < 0f)
        {
            projectile.rotation += (float)Math.PI * 2f;
        }
        else if (projectile.rotation > 6.283)
        {
            projectile.rotation -= (float)Math.PI * 2f;
        }
        if (projectile.rotation > Rotation - Speed && projectile.rotation < Rotation + Speed)
        {
            projectile.rotation = Rotation;
        }
    }

    public static void HoldSword(this Projectile projectile, Player player, float AttackSpeed = -12f, int StaticNPCHitCooldown = 12, float Rotate = 2f, bool batter = true, bool Attack = true)
    {
        if (projectile.ai[1] < 2f)
        {
            projectile.ai[1] += 1f;
        }
        projectile.localNPCHitCooldown = StaticNPCHitCooldown;
        Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
        projectile.DPoroj().Times[1] += 1f;
        if (projectile.DPoroj().Times[1] < 12f)
        {
            for (int i = 0; i < projectile.oldPos.Length; i++)
            {
                projectile.oldPos[i] = Vector2.Zero;
            }
        }
        if (projectile.DPoroj().Times[0] == 0f)
        {
            projectile.DPoroj().Times[0] = player.direction;
            projectile.localAI[0] = Rotate * player.direction;
            projectile.localAI[1] = AttackSpeed;
            if (projectile.owner == Main.myPlayer)
            {
                projectile.DPoroj().vector[0] = (Main.MouseWorld - vector).PerfectNormalize();
                projectile.netUpdate = true;
            }
        }
        int num = (int)(player.HeldItem.useTime / player.GetAttackSpeed(DamageClass.Melee));
        if (player.channel && !player.noItems && !player.CCed)
        {
            _ = !player.dead;
        }
        else
            _ = 0;
        if (projectile.localAI[1] < 0f)
        {
            if (Attack)
            {
                float num2 = projectile.localAI[0] + projectile.localAI[1] / (AttackSpeed * 3f) * projectile.DPoroj().Times[0];
                if (num2 > 0f)
                {
                    num2 = 0f - num2;
                }
                projectile.localAI[1] += 1f;
                projectile.ai[0] = num2 * projectile.DPoroj().Times[0];
                if (projectile.ai[1] == 3f)
                {
                    projectile.ai[1] = 2f;
                }
            }
        }
        else
        {
            projectile.localAI[1] = 1f;
            projectile.direction = (int)projectile.DPoroj().Times[0];
            if (projectile.DPoroj().Times[2] >= num)
            {
                projectile.Kill();
            }
            if (projectile.ai[0] > Rotate)
            {
                projectile.ai[0] = Rotate;
                projectile.localAI[1] = AttackSpeed;
                projectile.DPoroj().Times[2] -= AttackSpeed;
                if (projectile.owner == Main.myPlayer && projectile.DPoroj().Times[2] < num)
                {
                    projectile.DPoroj().vector[0] = (Main.MouseWorld - vector).PerfectNormalize();
                    player.ChangeDir(projectile.DPoroj().vector[0].X > 0f ? 1 : -1);
                    projectile.netUpdate = true;
                }
                projectile.DPoroj().Times[0] = -1f;
                projectile.localAI[0] = projectile.ai[0];
                projectile.DPoroj().Times[3] += 1f;
            }
            if (projectile.ai[0] < 0f - Rotate)
            {
                projectile.ai[0] = 0f - Rotate;
                projectile.localAI[1] = AttackSpeed;
                projectile.DPoroj().Times[2] -= AttackSpeed;
                if (projectile.owner == Main.myPlayer && projectile.DPoroj().Times[2] < num)
                {
                    projectile.DPoroj().vector[0] = (Main.MouseWorld - vector).PerfectNormalize();
                    player.ChangeDir(projectile.DPoroj().vector[0].X > 0f ? 1 : -1);
                    projectile.netUpdate = true;
                }
                projectile.DPoroj().Times[0] = 1f;
                projectile.localAI[0] = projectile.ai[0];
                projectile.DPoroj().Times[3] += 1f;
            }
        }
        if (!batter)
        {
            projectile.DPoroj().Times[0] = player.direction;
        }
    }

    public static void HoldSword2(this Projectile projectile, Player player, float AttackSpeed = -12f, int StaticNPCHitCooldown = 12, float Rotate = 2f, bool batter = true, bool Attack = true)
    {
        if (projectile.ai[1] < 2f)
        {
            projectile.ai[1] += 1f;
        }
        projectile.localNPCHitCooldown = StaticNPCHitCooldown;
        if (StaticNPCHitCooldown == -1)
        {
            projectile.localNPCHitCooldown = 1145141919;
        }
        Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
        projectile.DPoroj().Times[1] += 1f;
        if (projectile.DPoroj().Times[1] < 12f)
        {
            for (int i = 0; i < projectile.oldPos.Length; i++)
            {
                projectile.oldPos[i] = Vector2.Zero;
            }
        }
        if (projectile.DPoroj().Times[0] == 0f)
        {
            projectile.DPoroj().Times[0] = player.direction;
            projectile.localAI[0] = Rotate * player.direction;
            projectile.localAI[1] = AttackSpeed;
            if (projectile.owner == Main.myPlayer)
            {
                projectile.DPoroj().vector[0] = (Main.MouseWorld - vector).PerfectNormalize();
                projectile.netUpdate = true;
            }
        }
        int num = (int)(player.HeldItem.useTime * projectile.extraUpdates / player.GetAttackSpeed(DamageClass.Melee));
        if (player.channel && !player.noItems && !player.CCed)
        {
            _ = !player.dead;
        }
        else
            _ = 0;
        if (projectile.localAI[1] < 0f)
        {
            if (Attack)
            {
                float num2 = projectile.localAI[0] + projectile.localAI[1] / (AttackSpeed * 3f) * projectile.DPoroj().Times[0];
                if (num2 > 0f)
                {
                    num2 = 0f - num2;
                }
                projectile.localAI[1] += 1f;
                projectile.ai[0] = num2 * projectile.DPoroj().Times[0];
                if (projectile.ai[1] == 3f)
                {
                    projectile.ai[1] = 2f;
                }
            }
            for (int j = 0; j < projectile.MeleePoroj().oldVels.Length; j++)
            {
                projectile.MeleePoroj().oldVels[j] = Vector2.Zero;
            }
            projectile.MeleePoroj().ClearInvincibility = false;
        }
        else
        {
            if (!projectile.MeleePoroj().ClearInvincibility && StaticNPCHitCooldown == -1)
            {
                for (int k = 0; k < 200; k++)
                {
                    projectile.localNPCImmunity[k] = 0;
                }
                projectile.MeleePoroj().ClearInvincibility = true;
            }
            if (projectile.localAI[1] == 0f)
            {
                projectile.localAI[1] = 1f;
            }
            projectile.direction = (int)projectile.DPoroj().Times[0];
            if (projectile.ai[0] > Rotate)
            {
                projectile.ai[0] = Rotate;
                projectile.localAI[1] = AttackSpeed;
                projectile.DPoroj().Times[2] -= AttackSpeed;
                if (projectile.owner == Main.myPlayer && projectile.DPoroj().Times[2] < num)
                {
                    projectile.DPoroj().vector[0] = (Main.MouseWorld - vector).PerfectNormalize();
                    player.ChangeDir(projectile.DPoroj().vector[0].X > 0f ? 1 : -1);
                    projectile.netUpdate = true;
                }
                projectile.DPoroj().Times[0] = -1f;
                projectile.localAI[0] = projectile.ai[0];
                projectile.DPoroj().Times[3] += 1f;
            }
            if (projectile.ai[0] < 0f - Rotate)
            {
                projectile.ai[0] = 0f - Rotate;
                projectile.localAI[1] = AttackSpeed;
                projectile.DPoroj().Times[2] -= AttackSpeed;
                if (projectile.owner == Main.myPlayer && projectile.DPoroj().Times[2] < num)
                {
                    projectile.DPoroj().vector[0] = (Main.MouseWorld - vector).PerfectNormalize();
                    player.ChangeDir(projectile.DPoroj().vector[0].X > 0f ? 1 : -1);
                    projectile.netUpdate = true;
                }
                projectile.DPoroj().Times[0] = 1f;
                projectile.localAI[0] = projectile.ai[0];
                projectile.DPoroj().Times[3] += 1f;
            }
            if (projectile.DPoroj().Times[2] >= num)
            {
                projectile.MeleePoroj().oldPlayer = player.Center;
                projectile.MeleePoroj().DelayedKill = projectile.oldPos.Length;
                projectile.DPoroj().Times[0] = 0f - projectile.DPoroj().Times[0];
                return;
            }
        }
        if (!batter || projectile.DPoroj().Times[0] == 0f)
        {
            projectile.DPoroj().Times[0] = player.direction;
        }
    }

    public static void HoldFlyingKnife(this Projectile projectile, Player player, float AttackSpeed = -12f, float Rotate = 2f)
    {
        if (projectile.ai[1] < 2f)
        {
            projectile.ai[1] += 1f;
        }
        Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
        projectile.DPoroj().Times[1] += 1f;
        if (projectile.DPoroj().Times[1] < 12f)
        {
            for (int i = 0; i < projectile.oldPos.Length; i++)
            {
                projectile.oldPos[i] = Vector2.Zero;
            }
        }
        if (projectile.DPoroj().Times[0] == 0f)
        {
            projectile.DPoroj().Times[0] = player.direction;
            projectile.localAI[0] = Rotate * player.direction;
            projectile.localAI[1] = AttackSpeed;
            if (projectile.owner == Main.myPlayer)
            {
                projectile.DPoroj().vector[0] = (Main.MouseWorld - vector).PerfectNormalize();
                projectile.netUpdate = true;
            }
        }
        if (projectile.localAI[1] < 0f)
        {
            float num = projectile.localAI[0] + projectile.localAI[1] / (AttackSpeed * 3f) * projectile.DPoroj().Times[0];
            if (num > 0f)
            {
                num = 0f - num;
            }
            projectile.localAI[1] += 1f;
            projectile.ai[0] = num * projectile.DPoroj().Times[0];
            if (projectile.ai[1] == 3f)
            {
                projectile.ai[1] = 2f;
            }
            if (projectile.owner == Main.myPlayer)
            {
                projectile.DPoroj().vector[0] = (Main.MouseWorld - vector).PerfectNormalize();
                projectile.netUpdate = true;
            }
        }
        else
        {
            if (projectile.ai[0] > Rotate)
            {
                projectile.ai[0] = Rotate;
                projectile.DPoroj().Times[3] += 1f;
            }
            if (projectile.ai[0] < 0f - Rotate)
            {
                projectile.ai[0] = 0f - Rotate;
                projectile.DPoroj().Times[3] += 1f;
            }
        }
    }

    public static void HoldProj(this Projectile projectile, Player player, float Distance, float ProjRotation, Vector2 velocity, float Rotation = 0f, float Initial = 0.25f, bool Kill = false, float ValueSpeed = 1f, bool direction = true, bool AttackSpeed = true)
    {
        if (!projectile.DPoroj().Bool[2])
        {
            player.statMana += player.ItemMana();
            projectile.DPoroj().Bool[2] = true;
            projectile.netUpdate = true;
        }
        projectile.ownerHitCheck = true;
        player.heldProj = projectile.whoAmI;
        if (projectile.ai[1] <= 0f)
        {
            projectile.ai[1] = Initial;
        }
        Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: false, addGfxOffY: false);
        if (player.channel && !player.noItems && !player.CCed && !player.dead && player.Dplayer().ForbiddenToAttack == 0 || Kill)
        {
            if (AttackSpeed)
            {
                projectile.ai[0] += ValueSpeed * player.GetTotalAttackSpeed(projectile.DamageType);
            }
            else
            {
                projectile.ai[0] += ValueSpeed;
            }
            if (velocity == Vector2.Zero)
            {
                if (Main.myPlayer == projectile.owner)
                {
                    projectile.DPoroj().vector[0] = (Main.MouseWorld - vector).PerfectNormalize();
                    projectile.netUpdate = true;
                    Vector2 vector2 = projectile.velocity = projectile.DPoroj().vector[0].PerfectNormalize().RotatedBy(ProjRotation);
                }
            }
            else
            {
                Vector2 vector3 = projectile.velocity = velocity.PerfectNormalize().RotatedBy(ProjRotation);
                projectile.netUpdate = true;
            }
        }
        else if (!Kill)
        {
            projectile.Kill();
        }
        Vector2 vector4 = Vector2.Normalize(projectile.velocity).RotatedBy(0.0);
        if (player.mount.Active)
        {
            projectile.Center = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: false, addGfxOffY: false) + vector4 * Distance;
        }
        else
        {
            projectile.Center = player.MountedCenter + vector4 * Distance;
        }
        projectile.rotation = projectile.velocity.ToRotation() + Rotation;
        projectile.timeLeft = 5;
        if (direction)
        {
            player.ChangeDir(projectile.direction);
        }
        player.itemTime = 5;
        player.itemAnimation = 5;
        float num = 0f;
        if (player.direction == -1)
        {
            num = 3.14f;
        }
        player.itemRotation = projectile.velocity.ToRotation() + num - player.fullRotation;
    }

    public static void HoldBook(this Projectile projectile, int mana, float Distance, float reduceDistance, float floating, float floatingDistance)
    {
        Player player = projectile.Player();
        bool flag = player.channel && !player.noItems && !player.CCed && !player.dead && player.Dplayer().ForbiddenToAttack == 0;
        if (projectile.DPoroj().Times[1] < projectile.SolidTileDistanceDetection(Distance, reduceDistance) && flag)
        {
            projectile.DPoroj().Times[1] += 1f;
        }
        else
        {
            projectile.DPoroj().Times[1] -= 1f;
        }
        if (projectile.DPoroj().Times[2] != player.direction)
        {
            projectile.DPoroj().Times[1] *= -1f;
            projectile.DPoroj().Times[2] = player.direction;
        }
        DDHelper.BackAndForth(floating, 0f - floating, floatingDistance, ref projectile.DPoroj().Times[3], ref projectile.DPoroj().Bool[0]);
        projectile.position.Y -= projectile.DPoroj().Times[3];
        if (flag)
        {
            if (player.statMana >= mana)
            {
                if (projectile.DPoroj().Times[4] <= 1f)
                {
                    projectile.DPoroj().Times[4] += 0.03f;
                }
                else
                {
                    projectile.frameCounter++;
                    if (projectile.frameCounter >= player.HeldItem.useAnimation / 5)
                    {
                        projectile.frameCounter = 0;
                        projectile.frame++;
                    }
                    if (projectile.frame > 6)
                    {
                        projectile.frame = 2;
                    }
                    if (projectile.frame >= 2 && projectile.frame <= 6)
                    {
                        projectile.DPoroj().Bool[1] = true;
                    }
                }
            }
            else
            {
                projectile.DPoroj().Bool[1] = false;
                projectile.netUpdate = true;
                projectile.frameCounter++;
                if (projectile.frameCounter <= 5)
                {
                    projectile.frame = 7;
                }
                else if (projectile.frameCounter <= 10)
                {
                    projectile.frame = 0;
                }
            }
        }
        else
        {
            projectile.DPoroj().Bool[1] = false;
            projectile.frameCounter++;
            projectile.netUpdate = true;
            if (projectile.frameCounter <= 5)
            {
                projectile.frame = 7;
            }
            else if (projectile.frameCounter <= 10)
            {
                projectile.frame = 0;
            }
            if (projectile.DPoroj().Times[1] <= 0f)
            {
                projectile.Kill();
            }
            if (projectile.DPoroj().Times[4] > 0.2f)
            {
                projectile.DPoroj().Times[4] -= 0.03f;
            }
        }
        int num = projectile.damage = player.GetWeaponDamage(player.HeldItem);
    }
}
