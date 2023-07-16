using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DoxxarsRainbowMagic.Items;

//public class MagicWeaponSlot : ModAccessorySlot
//{
//    public override Vector2? CustomLocation
//    {
//        get
//        {
//            int num = 122;
//            if (Main.mapStyle == 1)
//            {
//                num += Main.miniMapHeight + 16;
//            }
//            return new Vector2(Main.screenWidth - 236, num);
//        }
//    }

//    public override string FunctionalTexture => "Terraria/Images/Item_" + 0;

//    public override string FunctionalBackgroundTexture => "DDmod/UI/InterfaceUI/表UI";

//    public override bool DrawVanitySlot => false;

//    public override bool DrawDyeSlot => false;

//    public override void OnMouseHover(AccessorySlotType context)
//    {
//        if (context == AccessorySlotType.FunctionalSlot)
//        {
//            Main.hoverItemName = DDSystem.English ? "MagicWeapon" : "法宝";
//        }
//    }

//    public override bool CanAcceptItem(Item checkItem, AccessorySlotType context)
//    {
//        if (AccessorySystem.MagicWeaponSlots != Type)
//        {
//            AccessorySystem.MagicWeaponSlots = Type;
//        }
//        if (checkItem.Mitem().MagicWeapon)
//        {
//            return true;
//        }
//        return false;
//    }

//    public override void Load()
//    {
//        AccessorySystem.MagicWeaponSlots = Type;
//    }
//}
