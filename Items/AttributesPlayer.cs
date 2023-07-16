using System.Collections.Generic;
using System.IO;
using System.Reflection;
//using DDmod.Content.Buffs.DeBuffs;
//using DDmod.Content.Buffs.PlayerBuffs;
//using DDmod.Content.NPCs.Boss.StarGuardBulan;
//using DDmod.Content.Projectiles.Melee;
//using DDmod.Content.Projectiles.Summon;
//using DDmod.Helper;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Light;
using Terraria.ModLoader;

namespace DoxxarsRainbowMagic.Items;

public class AttributesPlayer : ModPlayer
{
    public bool Heartstrengthening;

    public bool ServantOfTheHeart;

    public bool GuardianOfTheStar;

    public int GuardianOfTheStarCD;

    public bool StarPower;

    public bool Crimson;

    public int Dark;

    public bool ShadowSet;

    public int Stand;

    public Vector3 ShadowColor;

    public float CrimsonTime;

    public float CrimsonTime2;

    /*public override void ResetEffects()
    {
        Heartstrengthening = false;
        ServantOfTheHeart = false;
        GuardianOfTheStar = false;
        StarPower = false;
        Crimson = false;
        ShadowSet = false;
        if (NPC.AnyNPCs(ModContent.NPCType<StarGuard>()) && Main.LocalPlayer.statManaMax2 < 200)
        {
            Main.LocalPlayer.statManaMax2 = 200;
        }
        if (Stand > 0)
        {
            Stand--;
        }
    }

    public override void PreUpdate()
    {
        if (Stand > 0)
        {
            Player.gravity = 0f;
            Player.velocity.Y = 0f;
        }
        Lighting lighting = new Lighting();
        ILightingEngine lightingEngine = (ILightingEngine)lighting.GetType().GetField("_activeEngine", BindingFlags.Static | BindingFlags.NonPublic)!.GetValue(lighting);
        if (Main.netMode != 2 && ShadowSet)
        {
            ShadowColor = lightingEngine.GetColor((int)(Player.Center.X / 16f), (int)(Player.Center.Y / 16f));
            if (Main.myPlayer == Player.whoAmI)
            {
                StartSync();
            }
        }
        if (ShadowColor.X + ShadowColor.Y + ShadowColor.Z <= 0.5f && ShadowSet && !Player.HasBuff(ModContent.BuffType<Exposed>()))
        {
            if (Dark < 200)
            {
                Dark += 5;
            }
            Player.noFallDmg = true;
            if (Player.velocity != Vector2.Zero)
            {
                Player.Dplayer().DodgeChance += 15f;
            }
            Player.moveSpeed += 0.35f;
            if (!Main.dayTime)
            {
                Player.moveSpeed += 0.5f;
            }
            if (!Player.controlDown)
            {
                if (Player.velocity.Y >= 0f)
                {
                    Player.gravity = 0f;
                    Player.velocity.Y = 0f;
                }
                if (Player.controlJump)
                {
                    Player.gravity = 0f;
                    Player.velocity.Y = -5f;
                }
            }
            Player.immuneAlpha = Player.Aplayer().Dark;
        }
        else
        {
            Dark = 0;
        }
        if (GuardianOfTheStar)
        {
            GuardianOfTheStarCD++;
        }
        else
        {
            GuardianOfTheStarCD = 0;
        }
        if (Player.sitting.isSitting)
        {
            Tile tile = Main.tile[(int)(Player.Center.X / 16f), (int)(Player.Center.Y / 16f)];
            if (tile.TileType == 497)
            {
                tile = Main.tile[(int)(Player.Center.X / 16f), (int)(Player.Center.Y / 16f)];
                if (tile.TileFrameY == 1560 && Main.rand.NextBool(10))
                {
                    Vector2 vector = new Vector2(0f, 1f).RotatedBy(Main.rand.NextFloat(-1f, 1f));
                    int num = Projectile.NewProjectile(Player.GetSource_FromAI(), (int)(Player.Center.X / 16f) * 16 + 10, (int)(Player.Center.Y / 16f) * 16 + 20, vector.X, vector.Y, ModContent.ProjectileType<TerraShit>(), 200, 1f, Player.whoAmI);
                    Main.projectile[num].tileCollide = false;
                    tile = Main.tile[(int)(Player.Center.X / 16f), (int)(Player.Center.Y / 16f)];
                    tile.LiquidAmount += 10;
                    WorldGen.SquareTileFrame((int)(Player.Center.X / 16f), (int)(Player.Center.Y / 16f));
                }
            }
        }
        if (Player.HasBuff(ModContent.BuffType<CrimsonFury>()))
        {
            if (CrimsonTime < 0.5f)
            {
                CrimsonTime += 0.25f;
            }
            for (int i = 0; i < 200; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.CanBeChasedBy() && Main.rand.NextBool(10) && (nPC.Center - Player.Center).Length() < 128f && CrimsonTime2 <= 0f)
                {
                    CrimsonTime2 = 60f;
                    Projectile.NewProjectile(Player.GetSource_FromAI(), nPC.Center, Vector2.Zero, ModContent.ProjectileType<MeleeSuckBloodPlayer>(), 30, 0f, Player.whoAmI, 0f, -1f);
                }
            }
        }
        else if (CrimsonTime > 0f)
        {
            CrimsonTime -= 0.1f;
        }
        if (CrimsonTime2 > 0f)
        {
            CrimsonTime2 -= 1f;
        }
        DDHelper.MaxandMinF(ref CrimsonTime2, 999f, 0f);
        DDHelper.MaxandMinF(ref CrimsonTime, 999f, 0f);
        if (!DDSystem.BossSurvival)
        {
            Player.respawnTimer -= 3;
        }
    }

    public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
    {
        if (ShadowSet)
        {
            Player.AddBuff(ModContent.BuffType<Exposed>(), 600);
        }
    }

    public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
    {
        if (ShadowSet)
        {
            Player.AddBuff(ModContent.BuffType<Exposed>(), 600);
        }
    }

    public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource, ref int cooldownCounter)
    {
        if (ShadowSet)
        {
            Player.AddBuff(ModContent.BuffType<Exposed>(), 600);
        }
        if (GuardianOfTheStarCD > 6000)
        {
            GuardianOfTheStarCD = -30;
            Player.immune = true;
            Player.immuneTime = 60;
            return false;
        }
        if (Crimson)
        {
            if (Player.HasBuff(ModContent.BuffType<CrimsonFury>()))
            {
                Player.buffTime[Player.FindBuffIndex(ModContent.BuffType<CrimsonFury>())] += damage * 5;
            }
            else
            {
                Player.AddBuff(ModContent.BuffType<CrimsonFury>(), damage * 5);
            }
        }
        return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource, ref cooldownCounter);
    }

    public override void PostUpdate()
    {
    }

    public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
    {
    }

    public override void ModifyDrawLayerOrdering(IDictionary<PlayerDrawLayer, PlayerDrawLayer.Position> positions)
    {
    }

    public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
    {
        if (Player.HasBuff(ModContent.BuffType<CrimsonFury>()))
        {
            r = 1f;
            g = 0.1f;
            b = 0.1f;
        }
        if (Player.HasBuff(137))
        {
            r = 0f;
            g = 0.6f;
            b = 1f;
        }
    }*/

    public void StartSync()
    {
        if (Main.netMode != 0 && Main.netMode == 1)
        {
            ModPacket packet = Mod.GetPacket();
            Vector3 shadowColor = ShadowColor;
            packet.Write((byte)3);
            packet.Write((byte)Player.whoAmI);
            packet.Write(shadowColor.X);
            packet.Write(shadowColor.Y);
            packet.Write(shadowColor.Z);
            packet.Send();
        }
    }

    public static void PlayerData(Mod mod, BinaryReader reader, int whoAmI)
    {
        byte b = reader.ReadByte();
        AttributesPlayer modPlayer = Main.player[b].GetModPlayer<AttributesPlayer>();
        float num = reader.ReadSingle();
        float num2 = reader.ReadSingle();
        float num3 = reader.ReadSingle();
        modPlayer.ShadowColor = new Vector3(num, num2, num3);
        if (Main.netMode == 2)
        {
            ModPacket packet = mod.GetPacket();
            packet.Write((byte)3);
            packet.Write(b);
            packet.Write(num);
            packet.Write(num2);
            packet.Write(num3);
            packet.Send(-1, b);
        }
    }
}
