// DDmod.DDmod
using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DoxxarsRainbowMagic.Items;

public class DDmod : Mod
{
    public static global::DoxxarsRainbowMagic.Items.DDmod Instance;

    public override void HandlePacket(BinaryReader reader, int whoAmI)
    {
        try
        {
            DDType SType = (DDType)reader.ReadByte();
            switch (SType)
            {
            case DDType.PlayerCenter:
                // KingSlime.SyncPlayer(this, reader, whoAmI); // TODO ??
                break;
            case DDType.PlayerCenter2:
                SyncPlayer(this, reader, whoAmI);
                break;
            case DDType.Rightclick:
                DDPlayer.SyncProjvector(this, reader, whoAmI);
                break;
            case DDType.PlayerData:
                AttributesPlayer.PlayerData(this, reader, whoAmI);
                break;
            default:
                Logger.Error($"\ufffd\u07b7\ufffd\ufffd\ufffd\ufffd\ufffd\ufffd\ufffd\ufffd\ufffd\ufffd\ufffd\ufffd\ufffd\ufffd\ufffd\ufffdڵİ\ufffdIDΪ{SType}");
                throw new Exception("\ufffd\u07b7\ufffd\ufffd\ufffd\ufffd\ufffd\u036c\ufffd\ufffd\ufffd\ufffd\ufffdݰ\ufffd\ufffd\ufffd\ufffd\ufffdЧ\ufffd\ufffd\u036c\ufffd\ufffd\ufffd\ufffd\ufffdݰ\ufffdID.");
            }
        }
        catch (Exception e)
        {
            EndOfStreamException eose;
            if ((eose = (EndOfStreamException)(object)(e is EndOfStreamException ? e : null)) != null)
            {
                Logger.Error("\ufffd\u07b7\ufffd\ufffd\ufffd\ufffd\ufffd\u036c\ufffd\ufffd\ufffd\ufffd\ufffdݰ\ufffd,\ufffd\ufffd\ufffd\ufffd\ufffd\ufffd\ufffdݰ\ufffd\u032b\ufffd\ufffd,\ufffd\ufffd,\ufffd\ufffdʧ.", (Exception)(object)eose);
                return;
            }
            if (e is ObjectDisposedException ode)
            {
                Logger.Error("\ufffd\u07b7\ufffd\ufffd\ufffd\ufffd\ufffd\u036c\ufffd\ufffd\ufffd\ufffd\ufffdݰ\ufffd,\ufffd\ufffd\ufffd\ufffd\ufffd\ufffd\ufffdݰ\ufffd\u032b\ufffd\ufffd,\ufffd\ufffd,\ufffd\ufffdʧ.", ode);
                return;
            }
            if (!(e is IOException ioe))
            {
                throw;
            }
            Logger.Error("\ufffd\u07b7\ufffd\ufffd\ufffd\ufffd\ufffd\u036c\ufffd\ufffd\ufffd\ufffd\ufffdݰ\ufffd,\ufffd\ufffd\ufffd\ufffd\ufffd\ufffd\ufffdݰ\ufffd\u032b\ufffd\ufffd,\ufffd\ufffd,\ufffd\ufffdʧ", ioe);
        }
    }

    public override void Load()
    {
        DDShaders.LoadShaders();
        base.Load();
    }

    public static void SyncPlayer(Mod mod, BinaryReader reader, int whoAmI)
    {
        byte player = reader.ReadByte();
        float X = reader.ReadFloat();
        float Y = reader.ReadFloat();
        float VX = reader.ReadFloat();
        float VY = reader.ReadFloat();
        Main.player[player].Center = new Vector2(X, Y);
        Main.player[player].velocity = new Vector2(VX, VY);
        if (Main.netMode == 2)
        {
            ModPacket packet = mod.GetPacket();
            packet.Write((byte)1);
            packet.Write(player);
            packet.Write(X);
            packet.Write(Y);
            packet.Write(VX);
            packet.Write(VY);
            packet.Send(-1, player);
        }
    }
}

public enum DDType : byte
{
    PlayerCenter,
    PlayerCenter2,
    Rightclick,
    PlayerData
}
