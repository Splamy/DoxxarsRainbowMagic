//using DoxxarsRainbowMagic.Items.DDmod.Helper;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DoxxarsRainbowMagic.Items;

//public class WatchSlot : ModAccessorySlot
//{
//    public override Vector2? CustomLocation => new Vector2(564f, 20f);

//    public override string FunctionalTexture => "Terraria/Images/Item_" + 15;

//    public override string FunctionalBackgroundTexture => "DDmod/UI/InterfaceUI/表UI";

//    public override bool DrawVanitySlot => false;

//    public override bool DrawDyeSlot => false;

//  public override void OnMouseHover(AccessorySlotType context)
//    {
//        if (context == AccessorySlotType.FunctionalSlot)
//        {
//            Main.hoverItemName = DDSystem.English ? "Watch" : "表";
//        }
//    }

//    public override bool CanAcceptItem(Item checkItem, AccessorySlotType context)
//    {
//        if (AccessorySystem.WatchSlots != Type)
//        {
//            AccessorySystem.WatchSlots = Type;
//        }
//        if (checkItem.AccessoryItem().Watch)
//        {
//            return true;
//        }
//        return false;
//    }

//    public override void Load()
//    {
//        AccessorySystem.WatchSlots = Type;
//    }
//}
