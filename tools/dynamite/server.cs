datablock ProjectileData(dynamiteProjectile)
{
    projectileShapeName = "./tnt.dts";
   directDamage        = 0;
   explosion           = rocketExplosion;
   particleEmitter       = arrowTrailEmitter;

   muzzleVelocity      = 12.5;
   velInheritFactor    = 1;

   armingDelay         = 5000;
   lifetime            = 5000;
   fadeDelay           = 70;
   bounceAngle         = 50; //stick almost all the time
   minStickVelocity    = 10;
   bounceElasticity    = 0.2;
   bounceFriction      = 0.01;   
   isBallistic         = true;
   gravityMod = 0.6;
   explodeondeath = 1;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";

   uiName = "";
};

datablock ItemData(dynamiteItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./tnt.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Tier-1 Dynamite";
	iconName = "./icon_tnt";
	doColorShift = false;
	colorShiftColor = "0.471 0.471 0.471 1.000";

	 // Dynamic properties defined by the scripts
	image = dynamiteImage;
	canDrop = true;
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(dynamiteImage)
{
   // Basic Item properties
   shapeFile = "./tnt.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0.2";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = false;

   eyeOffset = "0.7 1.2 -0.25";

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = dynamiteItem;
   ammo = " ";
   projectile = dynamiteProjectile;
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = true;
   doRetraction = false;
   //raise your arm up or not
   armReady = true;

   //casing = " ";
   doColorShift = false;
   colorShiftColor = "0.471 0.471 0.471 1.000";


   // Initial start up state
	stateName[0]			= "Activate";
	stateTimeoutValue[0]		= 0.1;
	stateTransitionOnTimeout[0]	= "Ready";
	stateSequence[0]		= "ready";
	stateSound[0]					= weaponSwitchSound;

	stateName[1]			= "Ready";
	stateTransitionOnTriggerDown[1]	= "Fire";
	stateAllowImageChange[1]	= true;

	stateName[2]			= "Fire";
	stateTransitionOnTimeout[2]	= "Ready";
	stateTimeoutValue[2]		= 0.5;
	stateFire[2]			= true;
	stateSequence[2]		= "fire";
	stateScript[2]			= "onFire";
	stateWaitForTimeout[2]		= true;
	stateAllowImageChange[2]	= false;
};

function dynamiteImage::onFire(%this, %obj, %slot)
{
    %obj.tool[%obj.currtool] = 0;
    messageclient(%obj.client, 'MsgItemPickup', "", %obj.currtool, 0);
    %obj.unmountimage(%slot);
    parent::onFire(%this, %obj, %slot);
}



datablock ProjectileData(dynamite2Projectile : dynamiteProjectile)
{
    projectileShapeName = "./tnt.dts";
};

datablock ItemData(dynamite2Item : dynamiteItem)
{
	uiName = "Tier-2 Dynamite";
	image = dynamite2Image;
};

datablock ShapeBaseImageData(dynamite2Image : dynamiteImage)
{
   item = dynamite2Item;
   projectile = dynamite2projectile;
};

function dynamite2Image::onFire(%this, %obj, %slot)
{
    %obj.tool[%obj.currtool] = 0;
    messageclient(%obj.client, 'MsgItemPickup', "", %obj.currtool, 0);
    %obj.unmountimage(%slot);
    parent::onFire(%this, %obj, %slot);
}



datablock ProjectileData(dynamite3Projectile : dynamiteProjectile)
{
    projectileShapeName = "./tnt.dts";
};

datablock ItemData(dynamite3Item : dynamiteItem)
{
	uiName = "Tier-3 Dynamite";
	image = dynamite3Image;
};

datablock ShapeBaseImageData(dynamite3Image : dynamiteImage)
{
   item = dynamite3Item;
   projectile = dynamite3projectile;
};

function dynamite3Image::onFire(%this, %obj, %slot)
{
    %obj.tool[%obj.currtool] = 0;
    messageclient(%obj.client, 'MsgItemPickup', "", %obj.currtool, 0);
    %obj.unmountimage(%slot);
    parent::onFire(%this, %obj, %slot);
}



datablock ProjectileData(dynamite4Projectile : dynamiteProjectile)
{
    projectileShapeName = "./tnt.dts";
};

datablock ItemData(dynamite4Item : dynamiteItem)
{
	uiName = "Tier-4 Dynamite";
	image = dynamite4Image;
};

datablock ShapeBaseImageData(dynamite4Image : dynamiteImage)
{
   item = dynamite4Item;
   projectile = dynamite4projectile;
};

function dynamite4Image::onFire(%this, %obj, %slot)
{
    %obj.tool[%obj.currtool] = 0;
    messageclient(%obj.client, 'MsgItemPickup', "", %obj.currtool, 0);
    %obj.unmountimage(%slot);
    parent::onFire(%this, %obj, %slot);
}



datablock ProjectileData(dynamite5Projectile : dynamiteProjectile)
{
    projectileShapeName = "./tnt.dts";
};

datablock ItemData(dynamite5Item : dynamiteItem)
{
	uiName = "Tier-5 Dynamite";
	image = dynamite4Image;
};

datablock ShapeBaseImageData(dynamite5Image : dynamiteImage)
{
   item = dynamite5Item;
   projectile = dynamite5projectile;
};

function dynamite5Image::onFire(%this, %obj, %slot)
{
    %obj.tool[%obj.currtool] = 0;
    messageclient(%obj.client, 'MsgItemPickup', "", %obj.currtool, 0);
    %obj.unmountimage(%slot);
    parent::onFire(%this, %obj, %slot);
}



datablock ProjectileData(dynamite6Projectile : dynamiteProjectile)
{
    projectileShapeName = "./tnt.dts";
};

datablock ItemData(dynamite6Item : dynamiteItem)
{
	uiName = "Tier-6 Dynamite";
	image = dynamite4Image;
};

datablock ShapeBaseImageData(dynamite6Image : dynamiteImage)
{
   item = dynamite6Item;
   projectile = dynamite6projectile;
};

function dynamite6Image::onFire(%this, %obj, %slot)
{
    %obj.tool[%obj.currtool] = 0;
    messageclient(%obj.client, 'MsgItemPickup', "", %obj.currtool, 0);
    %obj.unmountimage(%slot);
    parent::onFire(%this, %obj, %slot);
}
