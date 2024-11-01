using GDWeave;
using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;
using System.Net.Security;

namespace z3roco01.fishillionaire;

public class ScratchSpotsMod : IScriptMod {
    // only runs on the player gd script
    public bool ShouldRun(string path) => path == "res://Scenes/Minigames/ScratchTicket/scratch_spots.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
        // wait for chance = 0.7
        var assignWaiter = new MultiTokenWaiter([
            t => t is IdentifierToken{Name:"chance"},
            t => t.Type is TokenType.OpAssign,
            t => t is ConstantToken{Value:RealVariant{Value:0.7}}
        ], allowPartialMatch: false);

        // wait for chance *= 0.75
        var mulAssignWaiter = new MultiTokenWaiter([
            t => t is IdentifierToken{Name:"chance"},
            t => t.Type is TokenType.OpAssignMul,
            t => t is ConstantToken{Value:RealVariant{Value:0.75}}
        ], allowPartialMatch: false);

        foreach (var token in tokens) {
            if (assignWaiter.Check(token)) {
                yield return new ConstantToken(new RealVariant(1.0));
            }
            else if (mulAssignWaiter.Check(token)) {
                yield return new ConstantToken(new RealVariant(1.0));
            }
            else {
                yield return token;
            }
        }
    }
}
