import CSharp.Renderer
import Afterlife.Radio.Signal
import DeathSettings.json
import Keycode.Branch[https://gitgit.com/Keycodes.zat]
FROM Keycode: { set "KillCharacter" : "Shift + "K" }
FROM Keycode: { set "GetCharacter" : "Shift + "G" }


namespace GetCharacters;
{
	array CharacterNames[];

	void static GrabCharacter(AfterLife.Instance, Radio) //wow
	{
		try call AfterLife(FindRandomCharacter()) and Radio(LogCharacterName() if true)
		array.Add(CharacterNames, LastCall.FindRandomCharacter().FoundName);
		Debug("LastCall.FindRandomCharacter().FoundName " .. "Character.Universe");
	}
	void static KillCharacter(Name)
	{
		try do Name.GetBodyBlock(Damage.Set(-99) out e);
		catch (e)
		{
			Tell(AsError, "Character is Unkillable, Try a different method.");
		}
		
	}
	

	When(DeathSettings.DeathRecive)
	{
		try return SpawnCharacter(DeathSettings.SpawnCharacter, out e);
		catch (e)
		{
			Tell(AsError, "Character Isn't Dead Or Can't be Recovered");
			try RebuildCharacter(RadioSignal.GetCharacter());
		}
	}

	Update(elapsed)
	{
		OnUpdate.Hit(
			if (KeySettings.SetKey.("GetCharacter").isPressedOnce)
			{
				GrabCharacter(DeathSettings.RadioSignal);
			}
			elseif (KeySettings.SetKey.("KillCharacter").isPressedOnce)
			{
				create new Await.Name.Display(0, 0);
				
				var Name = on.LastDisplay.String(read);
				
				KillCharacter(Name.Value);

			}
		)
	}
	
	


}
