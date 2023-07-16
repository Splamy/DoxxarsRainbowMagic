//using DDmod.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DoxxarsRainbowMagic.Items;

internal static class NPCdirection
{
	public static bool Incident(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.Invasion || Main.eclipse || Main.pumpkinMoon || Main.snowMoon)
		{
			return true;
		}
		return false;
	}

	public static NPC FindClosest(Vector2 Position, float maxRange, bool checkCanHit = true, NPC NONPC = null, int NPCMaster = -1)
	{
		NPC result = null;
		float num = maxRange;
		for (int i = 0; i < 200; i++)
		{
			NPC nPC = Main.npc[i];
			if (NONPC != null && NONPC == nPC || !nPC.CanBeChasedBy() || !checkCanHit && !Collision.CanHitLine(Position, 1, 1, nPC.position, nPC.width, nPC.height))
			{
				continue;
			}
			if (NPCMaster < 0)
			{
				float num2 = (Position - nPC.Center).Length();
				if (!(num <= num2))
				{
					num = num2;
					result = nPC;
				}
			}
			else if (i == NPCMaster || nPC.realLife == Main.npc[NPCMaster].realLife || nPC.realLife == Main.npc[NPCMaster].type)
			{
				float num3 = (Position - nPC.Center).Length();
				if (!(num <= num3))
				{
					num = num3;
					result = nPC;
				}
			}
		}
		return result;
	}

	public static bool HaveGoal(this Projectile projectile, int Distance, int Time = 30, bool IgnoreTile = false, int NPCID = -1)
	{
		NPC nPC = projectile.FindTargetWithinRange(Distance, !IgnoreTile);
		if (NPCID >= 0)
		{
			nPC = Main.npc[NPCID];
			if (nPC == null)
			{
				nPC = FindClosest(projectile.Center, Distance, IgnoreTile, null, NPCID);
			}
			if ((nPC.Center - projectile.Center).Length() > Distance)
			{
				nPC = null;
			}
		}
		if (nPC != null && nPC.active && (Time < 0 || projectile.GetGlobalProjectile<DDGlobalProjectile>().track > Time))
		{
			return true;
		}
		return false;
	}

	public static bool CustomHaveGoal(Vector2 vector, int Distance, int Time = 30, bool IgnoreTile = false, int NPCID = -1)
	{
		NPC nPC = FindClosest(vector, Distance, IgnoreTile);
		if (NPCID >= 0)
		{
			nPC = Main.npc[NPCID];
			if (nPC == null)
			{
				nPC = FindClosest(vector, Distance, IgnoreTile, null, NPCID);
			}
			if ((nPC.Center - vector).Length() > Distance)
			{
				nPC = null;
			}
		}
		if (nPC != null && nPC.active)
		{
			return true;
		}
		return false;
	}

	public static void Track(this Projectile projectile, int Distance, float Inertia = 10f, float Speed = 8f, int Time = 30, bool IgnoreTile = false, int NPCID = -1)
	{
		NPC nPC = projectile.FindTargetWithinRange(Distance, !IgnoreTile);
		if (NPCID >= 0)
		{
			nPC = Main.npc[NPCID];
			if (nPC == null)
			{
				nPC = FindClosest(projectile.Center, Distance, IgnoreTile, null, NPCID);
			}
			if ((nPC.Center - projectile.Center).Length() > Distance)
			{
				nPC = null;
			}
		}
		if (nPC != null && nPC.active && projectile.GetGlobalProjectile<DDGlobalProjectile>().track > Time)
		{
			projectile.Chase(nPC, Speed, Inertia);
		}
	}

	public static void Chase(this Projectile projectile, NPC npc, float Speed, float Inertia)
	{
		if (!projectile.hostile && projectile.friendly)
		{
			Vector2 vector = (npc.Center - projectile.Center).PerfectNormalize() * Speed;
			projectile.velocity = (projectile.velocity * Inertia + vector) / (Inertia + 1f);
		}
	}
}
