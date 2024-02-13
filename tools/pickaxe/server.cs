%error = ForceRequiredAddOn("Weapon_Sword");

if(%error == $Error::AddOn_Disabled)
{
	swordItem.uiName = "";
}

if(%error == $Error::AddOn_NotFound)
{
	error("ERROR: Tool_RPG - required add-on Weapon_Sword not found");
}
else
{
	exec("./Weapon_rpgAxe.cs");
	exec("./Weapon_rpgPickaxe.cs");
	registerInputEvent(fxDTSBrick,onPickaxeHit,"Self fxDTSBrick" TAB "Player Player" TAB "Client GameConnection" TAB "Minigame Minigame");
	registerInputEvent(fxDTSBrick,onAxeHit,"Self fxDTSBrick" TAB "Player Player" TAB "Client GameConnection" TAB "Minigame Minigame");
}