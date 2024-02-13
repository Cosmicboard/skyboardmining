exec("./halloweeniumtank/server.cs");
exec("./snowglobe/server.cs");

//Mining helmet player type.
datablock PlayerData(CryogenumTankPlayer : PlayerStandardArmor) {
	shapeFile = "./CryogenumTank.dts";

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


//Mining helmet equip item.
datablock ItemData(CryogenumTankItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./CryogenumTank.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Cryogenum Tank";
	iconName = "./icon_CryogenumTank";
	doColorShift = true;
	colorShiftColor = "1 1 1 1";

	 // Dynamic properties defined by the scripts
	image = CryogenumTankImage;
	canDrop = true;
};

datablock ShapeBaseImageData(CryogenumTankImage)
{
   // Basic Item properties
   shapeFile = "./CryogenumTank.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0.6 -0.6";

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
   item = CryogenumTankItem;
   ammo = "";
   projectile = ""; //We are not firing anything so...
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = false;
   doRetraction = false;
   //raise your arm up or not
   armReady = true;
   undroppable = 1;

   //casing = " ";
   doColorShift = true;
   colorShiftColor = CryogenumTankItem.colorShiftColor;

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

function CryogenumTankImage::onEquip(%this,%obj,%slot)
{
	
	//Equip the mining helmet bot, (so it has functional light.)
	if(isObject(%CryogenumTank = %obj.CryogenumTank)){
		
		%CryogenumTank.delete();
		
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
		%obj.CryogenumTank = new AIPlayer(){
			dataBlock = CryogenumTankPlayer;
			Wearer = %obj; //Store who is wearing this mining helmet.
		};
		%obj.CryogenumTank.setPlayerScale(%obj.getScale());
		%obj.CryogenumTank.kill();
		
		%obj.mountObject(%obj.CryogenumTank,7);
		
		//Hide all hats and shit.
		for(%i=0; $pack[%i] !$= ""; %i++)
			%obj.hideNode($pack[%i]);
		
	}
	
}

function HalloweeniumTankImage::onEquip(%this,%obj,%slot)
{
	
	//Equip the mining helmet bot, (so it has functional light.)
	if(isObject(%CryogenumTank = %obj.CryogenumTank)){
		
		%CryogenumTank.delete();
		
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
		%obj.CryogenumTank = new AIPlayer(){
			dataBlock = HalloweeniumTankPlayer;
			Wearer = %obj; //Store who is wearing this mining helmet.
		};
		%obj.CryogenumTank.setPlayerScale(%obj.getScale());
		%obj.CryogenumTank.kill();
		
		%obj.mountObject(%obj.CryogenumTank,7);
		
		//Hide all hats and shit.
		for(%i=0; $pack[%i] !$= ""; %i++)
			%obj.hideNode($pack[%i]);
		
	}
	
}

package cryogenumtank
{
	
	//When player or AIPlayer dies.
	//function Armor::onDisabled(%this,%obj,%a,%b,%c,%d,%e,%f){
		
	//	if(isObject(%miningHelmet = %obj.MiningHelmet)){
	//		if(isObject(%light = %miningHelmet.light))
	//			%light.delete();
	//		%miningHelmet.delete();
	//	}
		
	//	Parent::onDisabled(%this,%obj,%a,%b,%c,%d,%e,%f);
	//}

	function Player::removeBody(%this){
		
		if(%this.getDataBlock() == CryogenumTankPlayer.getId() || %this.getDataBlock() == HalloweeniumTankPlayer.getId() || %this.getDataBlock() == SnowglobePlayer.getId())
			return;
		Parent::removeBody(%this);
		
	}
	
	function Player::setPlayerScale(%this,%scale){
		
		if(isObject(%CryogenumTank = %this.CryogenumTank))
			%CryogenumTank.setPlayerScale(%scale);
		
		Parent::setPlayerScale(%this,%scale);
		
	}
	
	function Player::setScale(%this,%scale){
		
		if(isObject(%CryogenumTank = %this.CryogenumTank))
			%CryogenumTank.setScale(%scale);
		
		Parent::setScale(%this,%scale);
	}
	
	function Player::delete(%this){
		
		if(isObject(%CryogenumTank = %this.CryogenumTank)){
			%CryogenumTank.delete();
		}
		
		Parent::delete(%this);
		
	}
	
	function GameConnection::onClientLeaveGame(%this){
		
		if(isObject(%player = %this.player) && isObject(%CryogenumTank = %player.CryogenumTank)){
			%CryogenumTank.delete();
		}
		
		Parent::onClientLeaveGame(%this);
	}
	
	function GameConnection::applyBodyParts(%client) {
		%r = Parent::applyBodyParts(%client);

		if(isObject(%player = %client.player)) {
			
			if(isObject(%CryogenumTank = %player.CryogenumTank)){
				
				for(%i=0; $pack[%i] !$= ""; %i++)
					%player.hideNode($pack[%i]);
				
			}
			
		}

		return %r;
	}

};
activatePackage("cryogenumtank");