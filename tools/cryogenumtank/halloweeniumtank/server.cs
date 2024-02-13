//Mining helmet player type.
datablock PlayerData(HalloweeniumTankPlayer : PlayerStandardArmor) {
	shapeFile = "./HalloweeniumTank.dts";

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
datablock ItemData(HalloweeniumTankItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./HalloweeniumTank.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Halloweenium Tank";
	iconName = "Add-Ons/Item_CryogenumTank/icon_CryogenumTank";
	doColorShift = true;
	colorShiftColor = "1 1 1 1";

	 // Dynamic properties defined by the scripts
	image = HalloweeniumTankImage;
	canDrop = true;
};

datablock ShapeBaseImageData(HalloweeniumTankImage)
{
   // Basic Item properties
   shapeFile = "./HalloweeniumTank.dts";
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
   item = HalloweeniumTankItem;
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
   colorShiftColor = HalloweeniumTankItem.colorShiftColor;

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