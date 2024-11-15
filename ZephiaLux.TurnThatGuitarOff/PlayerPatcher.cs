using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace ZephiaLux.TurnThatGuitarOff;

internal class PlayerPatcher : IScriptMod {
	public bool ShouldRun(string path) => path == "res://Scenes/Entities/Player/guitar_string_sound.gdc";

	public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
	{
		// func _play_at_point
		var func_waiter = new MultiTokenWaiter([
		    t => t.Type is TokenType.PrFunction,
		    t => t is IdentifierToken { Name: "_play_at_point" },
		]);
		// var index = 0
		var index_waiter = new MultiTokenWaiter([
		    t => t.Type is TokenType.PrVar,
		    t => t is IdentifierToken { Name: "index" },
		    t => t.Type is TokenType.OpAssign,
		    t => t is ConstantToken { Value: IntVariant { Value: 0 } }
		]);


		foreach (var token in tokens) {
			yield return token;
			if (index_waiter.Matched) {
				continue;
			}
			if (func_waiter.Matched) {
				if (index_waiter.Check(token)) {
					yield return new Token(TokenType.Newline, 1);
					yield return new Token(TokenType.CfReturn);
				}
			} else {
				func_waiter.Check(token);
			}
		}
	}
}
