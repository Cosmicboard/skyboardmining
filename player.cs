//Setting the support so we can see all Slots
package InventorySlotAdjustment
{
	function Armor::onNewDatablock(%data,%this)
	{
		Parent::onNewDatablock(%data,%this);
		if(isObject(%this.client) && %data.maxTools != %this.client.lastMaxTools)
		{
			%this.client.lastMaxTools = %data.maxTools;
			commandToClient(%this.client,'PlayGui_CreateToolHud',%data.maxTools);
			for(%i=0;%i<%data.maxTools;%i++)
			{
				if(isObject(%this.tool[%i]))
					messageClient(%this.client,'MsgItemPickup',"",%i,%this.tool[%i].getID(),1);
				else
					messageClient(%this.client,'MsgItemPickup',"",%i,0,1);
			}
		}
	}
	function GameConnection::setControlObject(%this,%obj)
	{
		Parent::setControlObject(%this,%obj);
		if(%obj == %this.player && %obj.getDatablock().maxTools != %this.lastMaxTools)
		{
			%this.lastMaxTools = %obj.getDatablock().maxTools;
			commandToClient(%this,'PlayGui_CreateToolHud',%obj.getDatablock().maxTools);
		}
	}
	function Player::changeDatablock(%this,%data,%client)
	{
		if(%data != %this.getDatablock() && %data.maxTools != %this.client.lastMaxTools)
		{
			%this.client.lastMaxTools = %data.maxTools;
			commandToClient(%this.client,'PlayGui_CreateToolHud',%data.maxTools);
		}
		Parent::changeDatablock(%this,%data,%client);
	}
};
activatePackage(InventorySlotAdjustment);

datablock PlayerData(PlayerZombie : PlayerStandardArmor)
{
	canJet = 0;
	maxStepHeight = 1.5;
};

datablock PlayerData(Player6Slot : PlayerStandardArmor)
{
	uiName = "6 Slot Player";
	maxTools = 6;
	maxWeapons = 6;
	maxStepHeight = 1.0;
};

datablock PlayerData(Player7Slot : PlayerStandardArmor)
{
	uiName = "7 Slot Player";
	maxTools = 7;
	maxWeapons = 7;
	maxStepHeight = 1.0;
};

datablock PlayerData(Player8Slot : PlayerStandardArmor)
{
	uiName = "8 Slot Player";
	maxTools = 8;
	maxWeapons = 8;
	maxStepHeight = 1.0;
};

datablock PlayerData(Player9Slot : PlayerStandardArmor)
{
	uiName = "9 Slot Player";
	maxTools = 9;
	maxWeapons = 9;
	maxStepHeight = 1.0;
};

datablock PlayerData(Player10Slot : PlayerStandardArmor)
{
	uiName = "10 Slot Player";
	maxTools = 10;
	maxWeapons = 10;
	maxStepHeight = 1.0;
};

datablock PlayerData(Player11Slot : PlayerStandardArmor)
{
	uiName = "11 Slot Player";
	maxTools = 11;
	maxWeapons = 1;
	maxStepHeight = 1.0;
};

datablock PlayerData(Player12Slot : PlayerStandardArmor)
{
	uiName = "12 Slot Player";
	maxTools = 12;
	maxWeapons = 12;
	maxStepHeight = 1.0;
};

datablock PlayerData(Player13Slot : PlayerStandardArmor)
{
	uiName = "13 Slot Player";
	maxTools = 13;
	maxWeapons = 13;
	maxStepHeight = 1.0;
};

datablock PlayerData(Player14Slot : PlayerStandardArmor)
{
	uiName = "14 Slot Player";
	maxTools = 14;
	maxWeapons = 14;
	maxStepHeight = 1.0;
};

datablock PlayerData(Player15Slot : PlayerStandardArmor)
{
	uiName = "15 Slot Player";
	maxTools = 15;
	maxWeapons = 15;
	maxStepHeight = 1.0;
};

datablock PlayerData(Player16Slot : PlayerStandardArmor)
{
	uiName = "16 Slot Player";
	maxTools = 16;
	maxWeapons = 16;
	maxStepHeight = 1.0;
};

datablock PlayerData(Player17Slot : PlayerStandardArmor)
{
	uiName = "17 Slot Player";
	maxTools = 17;
	maxWeapons = 17;
	maxStepHeight = 1.0;
};

datablock PlayerData(Player18Slot : PlayerStandardArmor)
{
	uiName = "18 Slot Player";
	maxTools = 18;
	maxWeapons = 18;
	maxStepHeight = 1.0;
};

datablock PlayerData(Player19Slot : PlayerStandardArmor)
{
	uiName = "19 Slot Player";
	maxTools = 19;
	maxWeapons = 19;
	maxStepHeight = 1.0;
};

datablock PlayerData(Player20Slot : PlayerStandardArmor)
{
	uiName = "20 Slot Player";
	maxTools = 20;
	maxWeapons = 20;
	maxStepHeight = 1.0;
};

datablock PlayerData(DisabledPlayer : PlayerStandardArmor)
{
	jumpDelay = 127;
	jumpForce = 0;
	uiName = "";
	maxTools = 3;
	maxWeapons = 3;
	firstPersonOnly = 1;
	canjet = 0;
	maxstepheight = 0.75;
};

datablock PlayerData(Player12SlotNoJet : PlayerStandardArmor)
{
	uiName = "";
	maxTools = 20;
	maxWeapons = 20;
	maxStepHeight = 1.0;
	canJet = 0;
};

datablock playerdata(PlayerHealth : playerstandardarmor)
{
    canjet = 0;
    useCustomPainEffects = true;
    PainHighImage = "";
    PainMidImage  = "";
    PainLowImage  = "";
    painSound     = "";
    deathSound    = "";
};