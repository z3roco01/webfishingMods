using GDWeave;

namespace z3roco01.pitchedVocals;

public class Mod : IMod {
    public static IModInterface ModInterface;

    public Mod(IModInterface modInterface) {
        ModInterface = modInterface;
        modInterface.RegisterScriptMod(new BarkMod());
        modInterface.Logger.Information("pitch vocals loaded !");
    }

    public void Dispose() {
    }
}
