using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DoxxarsRainbowMagic.Items;

public class DDGlobalProjectile : GlobalProjectile
{
    public int track;

    public float Magnification = 1f;

    public float PenetrationProtection = 1f;

    public bool Detect;

    public Vector2[] vector = new Vector2[3];

    public bool[] Bool = new bool[5];

    public float[] Times = new float[5];

    public Vector2 MouseWorld;

    public override bool InstancePerEntity => true;

    public override void SetDefaults(Projectile projectile)
    {
    }

    public override void ReceiveExtraAI(Projectile projectile, BitReader bitReader, BinaryReader binaryReader)
    {
        projectile.localAI[0] = binaryReader.ReadFloat();
        projectile.localAI[1] = binaryReader.ReadFloat();
        vector[0] = binaryReader.ReadVector2();
        vector[1] = binaryReader.ReadVector2();
        vector[2] = binaryReader.ReadVector2();
        MouseWorld = binaryReader.ReadVector2();
        Bool[0] = binaryReader.ReadBoolean();
        Bool[1] = binaryReader.ReadBoolean();
        Bool[2] = binaryReader.ReadBoolean();
        Bool[3] = binaryReader.ReadBoolean();
        Bool[4] = binaryReader.ReadBoolean();
        Times[0] = binaryReader.ReadFloat();
        Times[1] = binaryReader.ReadFloat();
        Times[2] = binaryReader.ReadFloat();
        Times[3] = binaryReader.ReadFloat();
        Times[4] = binaryReader.ReadFloat();
        projectile.scale = binaryReader.ReadFloat();
    }

    public override void SendExtraAI(Projectile projectile, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
        binaryWriter.Write(projectile.localAI[0]);
        binaryWriter.Write(projectile.localAI[1]);
        binaryWriter.WriteVector2(vector[0]);
        binaryWriter.WriteVector2(vector[1]);
        binaryWriter.WriteVector2(vector[2]);
        binaryWriter.WriteVector2(MouseWorld);
        binaryWriter.Write(Bool[0]);
        binaryWriter.Write(Bool[1]);
        binaryWriter.Write(Bool[2]);
        binaryWriter.Write(Bool[3]);
        binaryWriter.Write(Bool[4]);
        binaryWriter.Write(Times[0]);
        binaryWriter.Write(Times[1]);
        binaryWriter.Write(Times[2]);
        binaryWriter.Write(Times[3]);
        binaryWriter.Write(Times[4]);
        binaryWriter.Write(projectile.scale);
    }

    public override bool PreAI(Projectile projectile)
    {
        track++;
        if (PenetrationProtection < 1f && !projectile.minion)
        {
            PenetrationProtection += 0.001f;
            if (projectile.minion)
            {
                PenetrationProtection += 0.01f;
            }
        }
        else
        {
            PenetrationProtection = 1f;
        }
        return base.PreAI(projectile);
    }

    public override bool? CanDamage(Projectile projectile)
    {
        if (projectile.type == 12)
        {
            return false;
        }
        return base.CanDamage(projectile);
    }

    /*public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
    {
        _ = Main.player[projectile.owner];
        if (target.Dnpc().PenetrationProtection > 0f)
        {
            damage = (int)(damage * PenetrationProtection);
        }
        if (PenetrationProtection > target.Dnpc().MaxPenetrationProtection)
        {
            PenetrationProtection -= target.Dnpc().PenetrationProtection;
            if (PenetrationProtection < target.Dnpc().MaxPenetrationProtection)
            {
                PenetrationProtection = target.Dnpc().MaxPenetrationProtection;
            }
        }
    }*/

    public override void Kill(Projectile projectile, int timeLeft)
    {
    }

    public override bool PreDraw(Projectile projectile, ref Color lightColor)
    {
        if (projectile.type == ProjectileID.PinkLaser)
        {
            lightColor = projectile.GetAlpha(Color.White);
        }
        return base.PreDraw(projectile, ref lightColor);
    }
}
