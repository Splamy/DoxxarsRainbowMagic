using System;
using System.Collections.Generic;
using System.IO;
//using DDmod.DDOn;
//using DDmod.Worlds;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DoxxarsRainbowMagic.Items;

public class DDPlayer : ModPlayer
{
    public int ForbiddenToAttack;

    public float DodgeChance;

    public float Rotate;

    public int PlayerMana;

    public bool Hide;

    public int Hide2;

    public bool WaterWings;

    public int VampireCD;

    public Vector2 SwimSpeed;

    public bool fall;

    public float fallTime;

    public int Climb;

    public Vector2 startPoint;

    public Vector2 ScreenPosition;

    public int LockTime;

    public bool Start;

    public float Times;

    public int Earthquake;

    public float Frequency;

    private float focusTransition;

    public bool TimeStops;

    /*public override void ResetEffects()
    {
        if (ForbiddenToAttack > 0)
        {
            ForbiddenToAttack--;
        }
        DodgeChance = 0f;
        PlayerMana = 0;
        WaterWings = false;
        if (Earthquake <= 0)
        {
            Frequency = 0f;
        }
        if (VampireCD > 0)
        {
            VampireCD--;
        }
        else
        {
            VampireCD = 0;
        }
    }*/

    public override void PostUpdateMiscEffects()
    {
        Player.GetDamage(DamageClass.Summon) += Player.GetTotalAttackSpeed(DamageClass.Summon) - 1f;
        Player.GetDamage(DamageClass.SummonMeleeSpeed) -= Player.GetTotalAttackSpeed(DamageClass.Summon) - 1f;
        Player.RotationSpeed(Player.fullRotation, 0f);
        if (!Collision.DrownCollision(Player.position, Player.width, Player.height, Player.gravDir))
        {
            if (Player.controlJump && Player.wingTime > 0f)
            {
                Player.RotationSpeed(0f, 0.05f);
            }
            if (Player.fullRotation > 0.5f && Player.fullRotation < 5.78 && Player.wingTime > 0f)
            {
                Player.controlJump = false;
            }
        }
        if (Collision.DrownCollision(Player.position, Player.width, Player.height, Player.gravDir) && !Player.mount.Active && !Player.mount.Cart && !WaterWings && !ModContent.GetInstance<DDConfigServer>().Swim && Player.velocity.Length() > 1f)
        {
            Player.controlJump = false;
        }
        Player.statManaMax2 += PlayerMana;
    }

    public override void HideDrawLayers(PlayerDrawSet drawInfo)
    {
        if (Player == null || Hide2 <= 0)
        {
            return;
        }
        foreach (PlayerDrawLayer layer in PlayerDrawLayerLoader.Layers)
        {
            if (layer != PlayerDrawLayers.HeldItem)
            {
                layer.Hide();
            }
        }
        Hide2--;
    }

    public override bool? CanHitNPCWithProj(Projectile proj, NPC target)
    {
        return null;
    }

    /*public override void PreUpdate()
    {
        Rotate += 1f;
        Roll();
        if (Main.worldName == "巨石噩梦")
        {
            if (Main.rand.NextBool(100))
            {
                Projectile.NewProjectile(Player.GetSource_FromAI(), new Vector2(Player.Center.X + Main.rand.NextFloat(-1000f, 1000f), Player.Center.Y - 1200f), Vector2.Zero, 99, 100, 1f);
            }
            if (Main.rand.NextBool(100))
            {
                Projectile.NewProjectile(Player.GetSource_FromAI(), new Vector2(Player.Center.X + Main.rand.NextFloat(-1000f, 1000f), Player.Center.Y - 1200f), Vector2.Zero, 727, 100, 1f);
            }
            if (Main.rand.NextBool(100))
            {
                Projectile.NewProjectile(Player.GetSource_FromAI(), new Vector2(Player.Center.X + Main.rand.NextFloat(-1000f, 1000f), Player.Center.Y - 1200f), Vector2.Zero, 655, 100, 1f);
            }
        }
    }

    public void Roll()
    {
        if (!Player.noFallDmg && (Player.velocity.Y > 0f || fallTime != 0f))
        {
            fallTime += Player.velocity.Y;
        }
        if (Player.noFallDmg && Player.velocity.Y == 0f)
        {
            fallTime = 0f;
        }
        if (fall)
        {
            fallTime = 0f;
        }
        if (Player.fullRotation > 1f)
        {
            _ = (double)Player.fullRotation;
            _ = 5.28;
        }
        if (!Collision.DrownCollision(Player.position, Player.width, Player.height, Player.gravDir))
        {
            SwimSpeed = Player.velocity;
            if (Player.velocity.Y != 0f && Player.controlUp && !ModContent.GetInstance<DDConfigServer>().Somersault)
            {
                float num = Player.velocity.X * 0.03f;
                if (num > 0.12f)
                {
                    num = 0.12f;
                }
                if (num < -0.12f)
                {
                    num = -0.12f;
                }
                Player.fullRotation += num;
                fall = true;
            }
            if (Player.adjWater)
            {
                fall = false;
            }
            if (Player.velocity.Y == 0f && !Player.sleeping.isSleeping)
            {
                Player.RotationSpeed(0f, 0.2f);
                if (Player.fullRotation > 1f && Player.fullRotation < 5.28 && fall)
                {
                    Player.statDefense = 0;
                    int num2 = 50 + (int)fallTime / 12;
                    if (Player.fullRotation > 2f && Player.fullRotation < 4.28 && fall)
                    {
                        num2 *= 2;
                    }
                    if (Player.whoAmI == Main.myPlayer)
                    {
                        Player.AddBuff(160, num2 * 3);
                        Player.Hurt(PlayerDeathReason.ByCustomReason("我是个傻逼"), num2, 0);
                    }
                    fall = false;
                }
            }
            if (Climb > 0)
            {
                Climb = 0;
            }
        }
        else if (Player.velocity.Length() > 0.1f && !Player.mount.Active && !Player.mount.Cart)
        {
            if (!ModContent.GetInstance<DDConfigServer>().Swim)
            {
                Player.wingTime = Player.wingTimeMax;
                fall = false;
                Player.RotationSpeed(Player.velocity.ToRotation() + (float)Math.PI / 2f, 0.1f);
                float num3 = 5 + (Player.accFlipper ? 5 : 0);
                if (Player.accMerman)
                {
                    num3 = 12f;
                }
                float num4 = 0.1f + (Player.accFlipper ? 0.1f : 0f) + (Player.accMerman ? 0.1f : 0f);
                if (Player.controlUp && SwimSpeed.Y > (0f - num3) / 2f)
                {
                    SwimSpeed.Y -= num4 * (!Player.accFlipper ? 1 : 2);
                }
                if (Player.controlDown && SwimSpeed.Y < num3 / 2f)
                {
                    SwimSpeed.Y += num4;
                }
                if (Player.controlLeft && SwimSpeed.X > 0f - num3)
                {
                    SwimSpeed.X -= num4;
                }
                if (Player.controlRight && SwimSpeed.X < num3)
                {
                    SwimSpeed.X += num4;
                }
                if (Player.swimTime < 10)
                {
                    Player.swimTime = 30;
                }
                Player.velocity = SwimSpeed;
                if (Player.controlUp && WorldGen.SolidTile((int)(Player.Center.X / 16f + Player.direction), (int)Player.Center.Y / 16))
                {
                    Climb = 16;
                }
                if (Climb > 0)
                {
                    Player.RotationSpeed(Player.direction, 0.02f);
                    Climb--;
                    if (Climb < 8)
                    {
                        Player.velocity.X = Player.direction;
                    }
                    Player.velocity.Y = -7f;
                }
                if (Player.controlJump && SwimSpeed.Length() < 8f)
                {
                    SwimSpeed *= 1.02f;
                }
                if (Player.canFloatInWater && !Player.controlDown)
                {
                    SwimSpeed.Y -= 0.1f;
                    Player.RotationSpeed(0f, 0.2f);
                }
            }
        }
        else if (!ModContent.GetInstance<DDConfigServer>().Swim)
        {
            Player.wingTime = Player.wingTimeMax;
            SwimSpeed = Player.velocity;
            if (Player.controlUp && !Player.mount.Active && !Player.mount.Cart)
            {
                Player.velocity.Y -= 0.3f * (!Player.accFlipper ? 1 : 4);
            }
        }
        if (Player.fullRotation != 0f && !Player.mount.Active && !Player.mount.Cart)
        {
            Player.fullRotationOrigin = Player.Size / 2f;
        }
        if (Player.mount.Active || Player.mount.Cart)
        {
            Player.fullRotation = 0f;
        }
        _ = Player.itemTime;
        _ = 0;
    }

    public override bool CanUseItem(Item item)
    {
        if (ForbiddenToAttack > 0)
        {
            return false;
        }
        if (!ModContent.GetInstance<DDConfigServer>().ForceMechanism)
        {
            if (item.type == 29)
            {
                if (NPCDowned.downedLifeGuard && Player.statLifeMax < 200)
                {
                    return true;
                }
                if (NPCDowned.downedLifeGuard2 && Player.statLifeMax < 300)
                {
                    return true;
                }
                if (NPCDowned.downedLifeGuard3)
                {
                    return true;
                }
                return false;
            }
            if (item.type == 109)
            {
                if (NPCDowned.downedStarGuard && Player.statManaMax < 100)
                {
                    return true;
                }
                if (NPCDowned.downedStarGuard2 && Player.statManaMax < 200)
                {
                    return true;
                }
                if (NPCDowned.downedStarGuard3)
                {
                    if (Player.statManaMax >= 200 && Player.statManaMax < 300)
                    {
                        Player.statManaMax += 20;
                        Player.ManaEffect(20);
                        item.consumable = true;
                    }
                    else if (Player.statManaMax >= 300)
                    {
                        item.consumable = false;
                    }
                    return true;
                }
                return false;
            }
        }
        return true;
    }

    public override void SaveData(TagCompound tag)
    {
        tag.Add("statManaMax", Player.statManaMax);
    }

    public override void LoadData(TagCompound tag)
    {
        Player.statManaMax = tag.Get<int>("statManaMax");
    }

    public void Bossperspective(Vector2 position, int Time, bool TimeStop, float Ti = 0.05f)
    {
        if (Player.Distance(position) < 10000f)
        {
            ScreenPosition = position - new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
            focusTransition = 0f;
            startPoint = Main.screenPosition;
            LockTime = Time;
            Times = Ti;
            Start = true;
            TimeStops = TimeStop;
        }
    }

    public override void ModifyScreenPosition()
    {
        if (WorldGen.IsGeneratingHardMode)
        {
            PlayerShake(5, 25f);
        }
        if (TimeStops)
        {
            DDmodOn.Start = true;
        }
        TimeStops = Start;
        if (Start)
        {
            if (LockTime > 0f)
            {
                if (focusTransition <= 1f)
                {
                    Main.screenPosition = Vector2.SmoothStep(startPoint, ScreenPosition, focusTransition += Times);
                }
                else
                {
                    Main.screenPosition = ScreenPosition;
                }
                if (Frequency != 0f)
                {
                    Main.screenPosition = ScreenPosition + new Vector2(Main.rand.NextFloat(0f - Frequency, Frequency), Main.rand.NextFloat(0f - Frequency, Frequency));
                }
                LockTime--;
            }
            else if (focusTransition >= 0f)
            {
                Main.screenPosition = Vector2.SmoothStep(Player.Center - new Vector2(Main.screenWidth / 2, Main.screenHeight / 2), ScreenPosition, focusTransition -= Times);
            }
            else
            {
                Start = false;
            }
        }
        else
        {
            if (Frequency != 0f)
            {
                Main.screenPosition = Player.Center + new Vector2(Main.rand.NextFloat(0f - Frequency, Frequency), Main.rand.NextFloat(0f - Frequency, Frequency)) - new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
            }
            if (Player.heldProj >= 0 && Main.projectile[Player.heldProj].active && Main.projectile[Player.heldProj].DPoroj().Detect)
            {
                Projectile projectile = Main.projectile[Player.heldProj];
                if (Frequency != 0f)
                {
                    Main.screenPosition = projectile.Center + new Vector2(Main.rand.NextFloat(0f - Frequency, Frequency), Main.rand.NextFloat(0f - Frequency, Frequency)) - new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
                }
                else
                {
                    Main.screenPosition = projectile.Center - new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
                }
            }
        }
        if (Earthquake > 0)
        {
            Earthquake--;
        }
    }

    public void PlayerShake(int VibrationTime = 0, float VibrationFrequency = 0f)
    {
        Earthquake = VibrationTime;
        Frequency = VibrationFrequency;
    }

    public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
    {
        return base.AddStartingItems(mediumCoreDeath);
    }

    public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource, ref int cooldownCounter)
    {
        if (Main.rand.Next(100) < DodgeChance)
        {
            Player.immune = true;
            Player.immuneTime = 60;
            CombatText.NewText(new Rectangle((int)Player.Center.X, (int)Player.Center.Y, 1, 1), new Color(0, 162, 255, 0) * 0.8f, DDSystem.English ? "dodge!" : "闪避!");
            return false;
        }
        return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource, ref cooldownCounter);
    }*/

    public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
    {
        ModPacket packet = Mod.GetPacket();
        packet.Write((byte)2);
        packet.Write((byte)Player.whoAmI);
        packet.Write(Player.controlUseTile);
        packet.Send(toWho, fromWho);
        if (newPlayer)
        {
            for (int i = 0; i < 200; i++)
            {
                Main.npc[i].netUpdate = true;
            }
        }
    }

    public static void SyncProjvector(Mod mod, BinaryReader reader, int whoAmI)
    {
        byte b = reader.ReadByte();
        bool flag = reader.ReadBoolean();
        Main.player[b].controlUseTile = flag;
        if (Main.netMode == 2)
        {
            ModPacket packet = mod.GetPacket();
            packet.Write((byte)2);
            packet.Write(b);
            packet.Write(flag);
            packet.Send(-1, b);
        }
    }

    public override void ModifyDrawLayerOrdering(IDictionary<PlayerDrawLayer, PlayerDrawLayer.Position> positions)
    {
        base.ModifyDrawLayerOrdering(positions);
    }

    public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
    {
        if (drawInfo.shadow == 0f)
        {
            drawInfo.drawPlayer.ActiveItem();
            if (drawInfo.heldProjOverHand)
            {
                drawInfo.projectileDrawPosition = drawInfo.DrawDataCache.Count;
            }
        }
    }
}
