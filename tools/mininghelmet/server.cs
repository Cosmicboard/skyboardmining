//Mining helmet player type.
datablock PlayerData(MiningHelmetPlayer : PlayerStandardArmor) {
	shapeFile = "./MiningHelmet.dts";

	uiName = "";

	boundingBox			= vectorScale("1 1 1", 4);
	crouchBoundingBox	= vectorScale("1 1 1", 4);

	maxForwardSpeed = 0;
	maxBackwardSpeed = 0;
	maxSideSpeed = 0;

	maxForwardCrouchSpeed = 0;
	maxBackwardCrouchSpeed = 0;
	maxSideCrouchSpeed = 0;

	maxForwardProneSpeed = 0;
	maxBackwardProneSpeed = 0;
	maxSideProneSpeed = 0;

	maxForwardWalkSpeed = 0;
	maxBackwardWalkSpeed = 0;
	maxSideWalkSpeed = 0;

	maxUnderwaterForwardSpeed = 0;
	maxUnderwaterBackwardSpeed = 0;
	maxUnderwaterSideSpeed = 0;
	
	jumpForce = 0 * 140; //8.3 * 90;
	canJet = 0;
	
	useCustomPainEffects = true;
   PainHighImage = "";
   PainMidImage  = "";
   PainLowImage  = "";
   painSound     = "";
   deathSound    = "";
	
};

//The lantern lightFX.
datablock fxLightData(MiningHelmetBrightLight)
{
	uiName = "Mining Helmet Bright Light";

	LightOn = true;
	radius = 12;
	brightness = 4;
	color = (255 / 255) SPC (230 / 255) SPC (155 / 255) SPC "1";

	flareOn = false;
	flarebitmap = "base/lighting/corona";
	NearSize	= 0.3;
	FarSize = 0.1;
};

datablock fxLightData(MiningHelmetLight : MiningHelmetBrightLight)
{
	uiName = "Mining Helmet Light";
	radius = 9;
	brightness = 3;
};

datablock fxLightData(MiningHelmetDimLight : MiningHelmetBrightLight)
{
	uiName = "Mining Helmet Dim Light";
	radius = 6;
	brightness = 2;
};


//Mining helmet equip item.
datablock ItemData(MiningHelmetItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./MiningHelmetItem.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Mining Helmet";
	iconName = "./icon_MiningHelmet";
	doColorShift = true;
	colorShiftColor = "1 1 1 1";

	 // Dynamic properties defined by the scripts
	image = MiningHelmetImage;
	canDrop = true;
};

datablock ShapeBaseImageData(MiningHelmetImage)
{
   // Basic Item properties
   shapeFile = "./MiningHelmetItem.dts";
   emap = true;
   undroppable = 1;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = false;

   eyeOffset = "0.0 0.0 0.0";

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = MiningHelmetItem;
   ammo = "";
   projectile = ""; //We are not firing anything so...
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = false;
   doRetraction = false;
   //raise your arm up or not
   armReady = true;

   //casing = " ";
   doColorShift = true;
   colorShiftColor = MiningHelmetItem.colorShiftColor;

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.0;
	stateTransitionOnTimeout[0]      = "Ready";
	stateSound[0]					= weaponSwitchSound;
	
	stateName[1]	= "Ready";
	stateTransitionOnTriggerDown[1]	= "Equip";
	
	stateName[2]	= "Equip";
	stateScript[2]	= "onEquip";
	stateTransitionOnTriggerUp[2]	= "Ready";
	
};

function MiningHelmetImage::onEquip(%this,%obj,%slot)
{
	
	//Equip the mining helmet bot, (so it has functional light.)
	if(isObject(%miningHelmet = %obj.MiningHelmet)){
		
		if(isObject(%light = %miningHelmet.light))
			%light.delete();
		%miningHelmet.delete();
		
		//Reset their appearance.
		%obj.unHideNode("ALL");
		if(isObject(%obj.client))
		{
			%obj.client.applyBodyParts();
			%obj.client.applyBodyColors();
		}
		else
			applyDefaultCharacterPrefs(%obj);
		
	}else{
		
		//Mount the mining helmet bot.
		%obj.MiningHelmet = new AIPlayer(){
			dataBlock = MiningHelmetPlayer;
			Wearer = %obj; //Store who is wearing this mining helmet.
		};
		%obj.MiningHelmet.setPlayerScale(%obj.getScale());
		%obj.MiningHelmet.kill();

		//Now mount a light emitting image to slot 1, so the lantern emits light.
		%light = new fxLight(){
			dataBlock = MiningHelmetBrightLight; //Assume maximum brightness.
			enable = true;
			iconSize = 1;
		};
		%light.attachToObject(%obj.MiningHelmet);
		%obj.MiningHelmet.light = %light;
		
		%obj.MiningHelmet.SKY_MiningLight(); //Start a loop that will change brightness of mining helmet depending on depth.
		
		%obj.mountObject(%obj.MiningHelmet,5);
		
		//Hide all hats and shit.
		for(%i=0; $hat[%i] !$= ""; %i++)
			%obj.hideNode($hat[%i]);

		for(%i=0; $accent[%i] !$= ""; %i++)
			%obj.hideNode($accent[%i]);
		
	}
	
}

function AIPlayer::SKY_MiningLight(%this){
	
	if(isObject(%this) && isObject(%light = %this.light) && isObject(%wearer = %this.Wearer)){
		
		%lightMode = MiningHelmetBrightLight.getId(); //Brightest.
		
		//Change the light based on depth.
		%depth = 5000 - getWord(%wearer.getPosition(),2);
		if(%depth > 1500 && %depth <= 2250)
			%lightMode = MiningHelmetLight.getId(); //Dimmer.
		else if(%depth > 2250)
			%lightMode = MiningHelmetDimLight.getId(); //Dimmest.
		
		//Change the datablock of the light to the new light when it changes.
		if(%light.getDataBlock() != %lightMode)
			%light.setDataBlock(%lightMode);

		if(%lightmode == MiningHelmetBrightLight.getId())
			%radius = 8;
		else if(%lightmode == MiningHelmetLight.getId())
			%radius = 5;
		else if(%lightmode == MiningHelmetDimLight.getId())
			%radius = 2;
		initcontainerradiussearch(%this.getposition(), %radius, $typemasks::playerobjecttype);
		while(isobject(%search = containersearchnext()))
		{
			if(%search.getclassname() !$= "player" || isobject(%search.mininghelmet) && %search.mininghelmet.wearer != %search)
				return;
			cancel(%search.removesevere);
			%search.severeLight = 1;
			%search.removeSevere = %search.schedule(50, removeSevereLight);
		}
		%this.schedule(33,"SKY_MiningLight");
		
	}
	
}

function player::removeSevereLight(%player)
{
	%Player.severelight = 0;
}

package SkyMiningHelmetPackage{
	
	//When player or AIPlayer dies.
	//function Armor::onDisabled(%this,%obj,%a,%b,%c,%d,%e,%f){
		
	//	if(isObject(%miningHelmet = %obj.MiningHelmet)){
	//		if(isObject(%light = %miningHelmet.light))
	//			%light.delete();
	//		%miningHelmet.delete();
	//	}
		
	//	Parent::onDisabled(%this,%obj,%a,%b,%c,%d,%e,%f);
	//}
	
	function Player::setPlayerScale(%this,%scale){
		
		if(isObject(%miningHelmet = %this.MiningHelmet))
			%miningHelmet.setPlayerScale(%scale);
		
		Parent::setPlayerScale(%this,%scale);
		
	}
	
	function Player::setScale(%this,%scale){
		
		if(isObject(%miningHelmet = %this.MiningHelmet))
			%miningHelmet.setScale(%scale);
		
		Parent::setScale(%this,%scale);
	}
	
	function Player::delete(%this){
		
		if(isObject(%miningHelmet = %this.MiningHelmet)){
			if(isObject(%light = %miningHelmet.light))
				%light.delete();
			%miningHelmet.delete();
		}
		
		Parent::delete(%this);
		
	}
	
	function Player::removeBody(%this){
		
		if(%this.getDataBlock() == MiningHelmetPlayer.getId())
			return;
		Parent::removeBody(%this);
		
	}
	
	function GameConnection::onClientLeaveGame(%this){
		
		if(isObject(%player = %this.player) && isObject(%miningHelmet = %player.MiningHelmet)){
			if(isObject(%light = %miningHelmet.light))
				%light.delete();
			%miningHelmet.delete();
		}
		
		Parent::onClientLeaveGame(%this);
	}
	
	function GameConnection::applyBodyParts(%client) {
		%r = Parent::applyBodyParts(%client);

		if(isObject(%player = %client.player)) {
			
			if(isObject(%miningHelmet = %player.MiningHelmet)){
				
				for(%i=0; $hat[%i] !$= ""; %i++)
					%player.hideNode($hat[%i]);

				for(%i=0; $accent[%i] !$= ""; %i++)
					%player.hideNode($accent[%i]);
				
			}
			
		}

		return %r;
	}

};
activatePackage("SkyMiningHelmetPackage");