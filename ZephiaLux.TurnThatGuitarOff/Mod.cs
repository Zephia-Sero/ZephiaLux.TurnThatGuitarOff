using GDWeave;

namespace ZephiaLux.TurnThatGuitarOff;

public class Mod : IMod {

	public Mod(IModInterface modInterface)
	{
		modInterface.Logger.Information("TURN THAT GUITAR OFF");
		modInterface.RegisterScriptMod(new PlayerPatcher());
	}

	public void Dispose()
	{
	}
}
