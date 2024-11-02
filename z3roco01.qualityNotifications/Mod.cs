using GDWeave;

namespace z3roco01.qualityNotifications;

public class Mod : IMod {
    public static IModInterface ModInterface;

    public Mod(IModInterface modInterface) {
        ModInterface = modInterface;
        modInterface.RegisterScriptMod(new PlayerDataMod());
        modInterface.Logger.Information("now you have quality notifications !");
    }

    public void Dispose() {
    }
}
