using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.GameContent;
using Terraria.UI.Chat;

namespace DoxxarsRainbowMagic.Projectiles
{
    public class RainbowGemProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // TODO
            // DisplayName.SetDefault("Rainbow Gem"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
        }

        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Magic;
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 600;
            Projectile.light = 0.3f;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            Projectile.velocity *= 2f;
        }

        public override void AI()
        {

            int dust = Dust.NewDust(Projectile.Center, 1, 1, DustID.GemRuby, 0f, 0f, 0, default(Color), 1f);
            Main.dust[dust].velocity *= 0.4f;
            Main.dust[dust].scale = (float)Main.rand.Next(100, 120) * 0.013f;
            Main.dust[dust].noGravity = true;

            int dust2 = Dust.NewDust(Projectile.Center, 1, 1, DustID.GemAmber, 0f, 0f, 0, default(Color), 1f);
            Main.dust[dust2].velocity *= 0.4f;
            Main.dust[dust2].scale = (float)Main.rand.Next(100, 120) * 0.013f;
            Main.dust[dust2].noGravity = true;

            int dust3 = Dust.NewDust(Projectile.Center, 1, 1, DustID.GemTopaz, 0f, 0f, 0, default(Color), 1f);
            Main.dust[dust3].velocity *= 0.4f;
            Main.dust[dust3].scale = (float)Main.rand.Next(100, 120) * 0.013f;
            Main.dust[dust3].noGravity = true;

            int dust4 = Dust.NewDust(Projectile.Center, 1, 1, DustID.GemEmerald, 0f, 0f, 0, default(Color), 1f);
            Main.dust[dust4].velocity *= 0.4f;
            Main.dust[dust4].scale = (float)Main.rand.Next(100, 120) * 0.013f;
            Main.dust[dust4].noGravity = true;

            int dust5 = Dust.NewDust(Projectile.Center, 1, 1, DustID.GemSapphire, 0f, 0f, 0, default(Color), 1f);
            Main.dust[dust5].velocity *= 0.4f;
            Main.dust[dust5].scale = (float)Main.rand.Next(100, 120) * 0.013f;
            Main.dust[dust5].noGravity = true;

            int dust6 = Dust.NewDust(Projectile.Center, 1, 1, DustID.GemAmethyst, 0f, 0f, 0, default(Color), 1f);
            Main.dust[dust6].velocity *= 0.4f;
            Main.dust[dust6].scale = (float)Main.rand.Next(100, 120) * 0.013f;
            Main.dust[dust6].noGravity = true;

            Projectile.ai[0] += 1f;


            float maxDetectRadius = 70f; // The maximum radius at which a projectile can detect a target
            float projSpeed = 6f; // The speed at which the projectile moves towards the target

            // Trying to find NPC closest to the projectile
            NPC closestNPC = FindClosestNPC(maxDetectRadius);
            if (closestNPC == null)
                return;

            // If found, change the velocity of the projectile and turn it in the direction of the target
            // Use the SafeNormalize extension method to avoid NaNs returned by Vector2.Normalize when the vector is zero
            if (Projectile.ai[0] >= 20f)
                Projectile.velocity = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
                Projectile.rotation = Projectile.velocity.ToRotation() * 0.001f;
        }

        // Finding the closest NPC to attack within maxDetectDistance range
        // If not found then returns null
        public NPC FindClosestNPC(float maxDetectDistance)
        {
            NPC closestNPC = null;

            // Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

            // Loop through all NPCs(max always 200)
            for (int k = 0; k < Main.maxNPCs; k++)
            {
                NPC target = Main.npc[k];
                // Check if NPC able to be targeted. It means that NPC is
                // 1. active (alive)
                // 2. chaseable (e.g. not a cultist archer)
                // 3. max life bigger than 5 (e.g. not a critter)
                // 4. can take damage (e.g. moonlord core after all it's parts are downed)
                // 5. hostile (!friendly)
                // 6. not immortal (e.g. not a target dummy)
                if (target.CanBeChasedBy())
                {
                    // The DistanceSquared function returns a squared distance between 2 points, skipping relatively expensive square root calculations
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);

                    // Check if it is within the radius
                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        sqrMaxDetectDistance = sqrDistanceToTarget;
                        closestNPC = target;
                    }
                }
            }

            return closestNPC;



        }
        // Many projectiles fade in so that when they spawn they don't overlap the gun muzzle they appear from
        public void FadeInAndOut()
        {
            // If last less than 50 ticks — fade in, than more — fade out
            if (Projectile.ai[0] <= 50f)
            {
                // Fade in
                Projectile.alpha -= 25;
                // Cap alpha before timer reaches 50 ticks
                if (Projectile.alpha < 100)
                    Projectile.alpha = 100;

                return;
            }

            // Fade out
            Projectile.alpha += 25;
            // Cal alpha to the maximum 255(complete transparent)
            if (Projectile.alpha > 255)
                Projectile.alpha = 255;


        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
            }
            else
            {
                Projectile.ai[0] += 0.1f;
                if (Projectile.velocity.X != oldVelocity.X)
                {
                    Projectile.velocity.X = -oldVelocity.X;
                }
                if (Projectile.velocity.Y != oldVelocity.Y)
                {
                    Projectile.velocity.Y = -oldVelocity.Y;
                }
                Projectile.velocity *= 0.75f;
                SoundEngine.PlaySound(SoundID.Item25, Projectile.position);
            }
            return false;
        }

        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.GemRuby, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.GemAmber, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.GemTopaz, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.GemEmerald, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.GemSapphire, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.GemAmethyst, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
            SoundEngine.PlaySound(SoundID.Item25, Projectile.position);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.ai[0] += 0.1f;
            Projectile.velocity *= 0.75f;
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.GemRuby, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.GemAmber, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.GemTopaz, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.GemEmerald, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.GemSapphire, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.GemAmethyst, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);

            SoundEngine.PlaySound(SoundID.Item25, Projectile.position);

            base.OnHitNPC(target, hit, damageDone);
        }
    }

}