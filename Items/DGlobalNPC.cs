//using DDmod.Content.Buffs.PlayerBuffs;
//using DDmod.Content.Projectiles.Summon;
//using DDmod.Helper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace DoxxarsRainbowMagic.Items;

public class DGlobalNPC : GlobalNPC
{
    public bool LifeUP = true;

    public bool Deathrattle;

    public bool HeartMarker;

    public bool AcornMarker;

    public bool MeteorMarker;

    public int[] InvincibleFrame = new int[256];

    public int[] InvincibleProj = new int[1000];

    public int Penetrate;

    public float PenetrationProtection;

    public float MaxPenetrationProtection;

    public Vector2[] vector = new Vector2[3];

    public bool[] Bool = new bool[5];

    public float[] Times = new float[5];

    public int[] Lifes = new int[5];

    public int[] MaxLifes = new int[5];

    public override bool InstancePerEntity => true;

    public override void ResetEffects(NPC npc)
    {
        HeartMarker = false;
        AcornMarker = false;
        MeteorMarker = false;
        Penetrate = 0;
    }

    public override void SetDefaults(NPC npc)
    {
    }

    public override void ModifyHitNPC(NPC npc, NPC target, ref NPC.HitModifiers modifiers)
    {
        modifiers.ArmorPenetration += Penetrate;

        base.ModifyHitNPC(npc, target, ref modifiers);
    }

    public override void AI(NPC npc)
    {
    }

    public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
    {
        if (InvincibleFrame[projectile.owner] > 0 && InvincibleProj[projectile.whoAmI] == 0)
        {
            return true;
        }
        return base.CanBeHitByProjectile(npc, projectile);
    }

    public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
    {
        if (InvincibleFrame[player.whoAmI] > 0)
        {
            return false;
        }
        return base.CanBeHitByItem(npc, player, item);
    }

    public override bool PreAI(NPC npc)
    {
        for (int i = 0; i < InvincibleFrame.Length; i++)
        {
            if (InvincibleFrame[i] > 0)
            {
                InvincibleFrame[i]--;
            }
            else
            {
                InvincibleFrame[i] = 0;
            }
            if (InvincibleFrame[i] > 0)
            {
                npc.immune[i] = InvincibleFrame[i];
            }
        }
        for (int j = 0; j < InvincibleProj.Length; j++)
        {
            Projectile projectile = Main.projectile[j];
            if (InvincibleProj[j] > 0)
            {
                InvincibleProj[j] = InvincibleFrame[projectile.owner];
            }
        }
        return base.PreAI(npc);
    }

    //public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
    //{
    //    InvincibleProj[projectile.whoAmI] = InvincibleFrame[projectile.owner];
    //    if (HeartMarker && projectile.DamageType == DamageClass.Summon)
    //    {
    //        Projectile.NewProjectile(projectile.GetSource_FromAI(), npc.Center, new Vector2(0f, -5f), ModContent.ProjectileType<SummonHeart>(), 0, 0f, projectile.owner);
    //        if (npc.HasBuff(ModContent.BuffType<BlessingOfTheHeart>()))
    //        {
    //            npc.buffTime[npc.FindBuffIndex(ModContent.BuffType<BlessingOfTheHeart>())] = 0;
    //        }
    //    }
    //    if (AcornMarker && projectile.DamageType == DamageClass.Summon)
    //    {
    //        Projectile.NewProjectile(projectile.GetSource_FromAI(), npc.Center, new Vector2(Main.rand.NextFloat(-3f, 3f), -5f), ModContent.ProjectileType<SummonAcorn>(), (int)(damage * 0.8f), 0f, projectile.owner);
    //        if (npc.HasBuff(ModContent.BuffType<AcornMarker>()))
    //        {
    //            npc.buffTime[npc.FindBuffIndex(ModContent.BuffType<AcornMarker>())] = 0;
    //        }
    //    }
    //    if (MeteorMarker && projectile.DamageType == DamageClass.Summon)
    //    {
    //        damage = (int)(damage * 1.1f);
    //    }
    //    damage = (int)(damage - npc.defense / 2 * projectile.DPoroj().Magnification) + npc.defense / 2;
    //}

    public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        return base.PreDraw(npc, spriteBatch, screenPos, drawColor);
    }

    public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
    }
}
