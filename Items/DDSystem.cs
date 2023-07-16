using System.Collections.Generic;
using System.IO;
using DoxxarsRainbowMagic.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace DDmod;

public class DDSystem : ModSystem
{

    public static DDSystem Instance;

    internal UserInterface MagicWeaponInterface;

    public override void Load()
    {
        //DDmodOn.Load();
        DDProjTextures.LoadProjTextures();
        DDItemTextures.LoadItemTextures();
        DDTextures.LoadTextures();
        //if (!Main.dedServ)
        //{
        //    MagicWeaponUI = new MagicWeaponStrengtheningUI();
        //    ((UIElement)MagicWeaponUI).Activate();
        //    MagicWeaponInterface = new UserInterface();
        //    MagicWeaponInterface.SetState((UIState)(object)MagicWeaponUI);
        //}
    }

    public override void Unload()
    {
        DDProjTextures.UnloadProjTextures();
        DDItemTextures.UnloadItemTextures();
        DDTextures.UnloadTextures();
    }
}
