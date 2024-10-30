using GDWeave;
using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;
using System.Net.Security;

namespace z3roco01.pitchedVocals;

public class BarkMod : IScriptMod {
    // only runs on the player gd script
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Player/player.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
        // wait for `_sync_sfx(bark_id`
        var waiter = new MultiTokenWaiter([
            t => t is IdentifierToken{Name:"_sync_sfx"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t is IdentifierToken{Name:"bark_id"},
        ], allowPartialMatch: false);

        foreach (var token in tokens) {
            // always add the token
            yield return token;

            if (waiter.Check(token)) {
                //appeneds `, null, PlayerData.voice_pitch / 1.8, 0.0` after `_sync_sfx(bark_id`
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new NilVariant());
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("voice_pitch");
                yield return new Token(TokenType.OpDiv);
                yield return new ConstantToken(new RealVariant(1.45));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new RealVariant(0.0));
            }
        }
    }
}
