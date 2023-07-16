using Terraria.ModLoader;

namespace DoxxarsRainbowMagic.Items;

public class AccessoryPlayer : ModPlayer
{
    public int wingslot;

    public int wingFrameCounter;

    public int wingFrame;

    public override void ResetEffects()
    {
        wingslot = 0;
    }

    public override void PreUpdate()
    {
        if (Player.wingsLogic > 0 && Player.controlJump && Player.wingTime > 0f && Player.jump == 0 && Player.velocity.Y != 0f || Player.jump > 0)
        {
            wingFrameCounter++;
            if (wingFrameCounter > 4)
            {
                wingFrame++;
                wingFrameCounter = 0;
                if (wingFrame >= 5)
                {
                    wingFrame = 0;
                }
            }
        }
        else if (!Player.controlJump || Player.velocity.Y == 0f)
        {
            wingFrame = 5;
        }
        else
        {
            wingFrame = 1;
        }
    }

    public void GrappleMovement()
    {
    }

    public override void Load()
    {
    }
}
