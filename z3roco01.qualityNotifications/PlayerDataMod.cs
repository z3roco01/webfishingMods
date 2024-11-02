using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using z3roco01.qualityNotifications;

class PlayerDataMod : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Singletons/playerdata.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
        var waiter = new MultiTokenWaiter([
            // if not journal_logs[item_id][quality].has(quality):
            t => t.Type is TokenType.CfIf,
            t => t.Type is TokenType.OpNot,
            t => t is IdentifierToken{Name:"journal_logs"},
            t => t.Type is TokenType.BracketOpen,
            t => t is IdentifierToken{Name:"category"},
            t => t.Type is TokenType.BracketClose,
            t => t.Type is TokenType.BracketOpen,
            t => t is IdentifierToken{Name:"item_id"},
            t => t.Type is TokenType.BracketClose,
            t => t.Type is TokenType.BracketOpen,
            t => t is ConstantToken{Value:StringVariant{Value:"quality"}},
            t => t.Type is TokenType.BracketClose,
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken{Name:"has"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t is IdentifierToken{Name:"quality"},
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Colon,
            
            // journal_logs[category][item_id]["quality"].append(quality)
            t => t is IdentifierToken{Name:"journal_logs"},
            t => t.Type is TokenType.BracketOpen,
            t => t is IdentifierToken{Name:"category"},
            t => t.Type is TokenType.BracketClose,
            t => t.Type is TokenType.BracketOpen,
            t => t is IdentifierToken{Name:"item_id"},
            t => t.Type is TokenType.BracketClose,
            t => t.Type is TokenType.BracketOpen,
            t => t is ConstantToken{Value:StringVariant{Value:"quality"}},
            t => t.Type is TokenType.BracketClose,
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken{Name:"append"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t is IdentifierToken{Name:"quality"},
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Newline
        ], allowPartialMatch: false);

        var ifWaiter = new MultiTokenWaiter([
            // if not journal_logs[item_id][quality].has(quality):
            t => t.Type is TokenType.CfIf,
            t => t.Type is TokenType.OpNot,
            t => t is IdentifierToken{Name:"journal_logs"},
            t => t.Type is TokenType.BracketOpen,
            t => t is IdentifierToken{Name:"category"},
            t => t.Type is TokenType.BracketClose,
            t => t.Type is TokenType.BracketOpen,
            t => t is IdentifierToken{Name:"item_id"},
            t => t.Type is TokenType.BracketClose,
            t => t.Type is TokenType.BracketOpen,
            t => t is ConstantToken{Value:StringVariant{Value:"quality"}},
            t => t.Type is TokenType.BracketClose,
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken{Name:"has"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t is IdentifierToken{Name:"quality"},
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Colon,
        ], allowPartialMatch:false);

        foreach(var token in tokens) {
            if (waiter.Check(token))
            {
                // var qualityName = ""
                yield return new Token(TokenType.Newline, 3);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("qualityName");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant(""));
                yield return new Token(TokenType.Newline, 3);

                // if(QUALITY_DATA[quality].name == ""):
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("QUALITY_DATA");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("quality");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("name");
                yield return new Token(TokenType.OpEqual);
                yield return new ConstantToken(new StringVariant(""));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);

                // qualityName = "Normal"
                yield return new IdentifierToken("qualityName");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("Normal"));
                yield return new Token(TokenType.Newline, 3);

                //else: qualityName = QUALITY_DATA[quality].name
                yield return new Token(TokenType.CfElse);
                yield return new Token(TokenType.Colon);
                yield return new IdentifierToken("qualityName");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("QUALITY_DATA");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("quality");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("name");
                yield return new Token(TokenType.Newline, 3);

                // PlayerData._send_notification("Caught the first " + qualityName + " " + Globals.item_data[item_id]["file"].item_name)
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_send_notification");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("You caught your first "));
                yield return new Token(TokenType.OpAdd);
                yield return new IdentifierToken("qualityName");
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new StringVariant(" "));
                yield return new Token(TokenType.OpAdd);
                yield return new IdentifierToken("Globals");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("item_data");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("item_id");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("file"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("item_name");
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new StringVariant(" "));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 2);

            }else { 
                yield return token;

                if (ifWaiter.Check(token)){
                    yield return new Token(TokenType.Newline, 3);
                }
            }
        }
    }
}