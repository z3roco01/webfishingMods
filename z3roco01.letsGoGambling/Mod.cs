using GDWeave;

namespace z3roco01.letsGoGambling;

public class Mod : IMod {
    public static IModInterface ModInterface;

    public Mod(IModInterface modInterface) {
        ModInterface = modInterface;
        modInterface.Logger.Information("LETS GO GAMBLING !!!");
    }

    public void Dispose() {
    }
}
