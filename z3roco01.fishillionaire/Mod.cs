using GDWeave;
using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace z3roco01.fishillionaire;

public class Mod : IMod {
    public static IModInterface modInterface;

    public Mod(IModInterface ModInterface) {
        modInterface = ModInterface;
        ModInterface.RegisterScriptMod(new ScratchSpotsMod());
        modInterface.Logger.Information("YOURE THE FISHILLIONAIRE !!!!");
    }

    public void Dispose() {
    }
}
