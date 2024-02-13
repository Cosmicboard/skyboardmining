datablock ItemData(tntlauncherItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./tntlauncher.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "TNT Launcher";
	iconName = "./icon_tntlauncher";
	doColorShift = false;
	colorShiftColor = "0.100 0.500 0.250 1.000";

	 // Dynamic properties defined by the scripts
	image = tntlauncherImage;
	canDrop = true;
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(tntlauncherImage)
{
   // Basic Item properties
   shapeFile = "./tntlauncher.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = 0; //"0.7 1.2 -0.5";
   rotation = eulerToMatrix( "0 0 0" );
   undroppable = 1;

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = BowItem;
   ammo = " ";
   projectile = "";
   projectileType = Projectile;

	//casing = tntlauncherShellDebris;
	shellExitDir        = "1.0 -1.3 1.0";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 15.0;	
	shellVelocity       = 7.0;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;
   minShotTime = 700;   //minimum time allowed between shots (needed to prevent equip/dequip exploit)

   doColorShift = false;
   colorShiftColor = tntlauncherItem.colorShiftColor;//"0.400 0.196 0 1.000";

   //casing = " ";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.1;
	stateTransitionOnTimeout[0]       = "Ready";
	stateSound[0]					= weaponSwitchSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "Fire";
	stateAllowImageChange[1]         = true;
   stateTransitionOnNoAmmo[1]       = "NoAmmo";
	stateSequence[1]	= "Ready";

	stateName[2]                    = "Fire";
	stateTransitionOnTimeout[2]     = "Smoke";
	stateTimeoutValue[2]            = 0.1;
	stateFire[2]                    = true;
	stateAllowImageChange[2]        = false;
	stateSequence[2]                = "Fire";
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]			= true;
	stateEmitter[2]					= rocketlauncherFlashEmitter;
	stateEmitterTime[2]				= 0.05;
	stateEmitterNode[2]				= tailNode;
	stateSound[2]					= rocketFireSound;
   stateSequence[2]                = "Fire";
	//stateEjectShell[2]       = true;

	stateName[3] = "Smoke";
	stateEmitter[3]					= rocketlauncherSmokeEmitter;
	stateEmitterTime[3]				= 0.05;
	stateEmitterNode[3]				= "muzzleNode";
	stateTimeoutValue[3]            = 0.1;
   stateSequence[3]                = "TrigDown";
	stateTransitionOnTimeout[3]     = "CoolDown";

   stateName[5] = "CoolDown";
   stateTimeoutValue[5]            = 0.5;
	stateTransitionOnTimeout[5]     = "Reload";
   stateSequence[5]                = "TrigDown";


	stateName[4]			= "Reload";
	stateTransitionOnTriggerUp[4]     = "Ready";
	stateSequence[4]	= "TrigDown";

   stateName[6]   = "NoAmmo";
   stateTransitionOnAmmo[6] = "Ready";

};

function tntlauncherimage::onmount(%this, %Obj, %slot)
{
    if(%obj.totalloaded > 0)
        %obj.setimageammo(0,1);
    %obj.tntlauncherhud();
}

function tntlauncherimage::onunmount(%this, %Obj, %slot)
{
    cancel(%obj.tnthud);
}

function tntlauncherimage::onfire(%this, %Obj, %slot)
{
    %player=%obj;
    if(%player.totalloaded > 0)
    {
        if(%player.loaded[%Player.totalloaded-1] $= "Tier-1 Dynamite")
            %projectile = "dynamiteprojectile";
        else if(%player.loaded[%Player.totalloaded-1] $= "Tier-2 Dynamite")
            %projectile = "dynamite2projectile";
        else if(%player.loaded[%Player.totalloaded-1] $= "Tier-3 Dynamite")
            %projectile = "dynamite3projectile";
        else if(%player.loaded[%Player.totalloaded-1] $= "Tier-4 Dynamite")
            %projectile = "dynamite4projectile";
        %p = new Projectile()
        {
            dataBlock = %projectile;
            initialVelocity = vectorscale(%obj.getmuzzlevector(0),30);
            initialPosition = %obj.getMuzzlePoint(0);
            sourceObject = %obj;
            sourceSlot = 0;
            client = %obj.client;
        };
        MissionCleanup.add(%p);
        %player.loaded[%player.totalloaded-1] = "";
        %player.totalloaded--;
    }
    if(%player.totalloaded <= 0)
        %player.setimageammo(0,0);
}

function player::tntlauncherhud(%player)
{
    %loaded = %player.loaded[%player.totalloaded-1];
    if(%loaded $= "")
        %loaded = "NONE";
    %client = %Player.client;
    %client.centerprint("<just:right>\c3Currently Loaded:" SPC %loaded NL "\c6Total Loaded Dynamite:" SPC mceil(%player.totalloaded) @ " / 10",0.25);
    %player.tnthud = %player.schedule(100, tntlauncherhud);
}

datablock ItemData(pumpkinlauncherItem : tntlauncheritem)
{
	shapeFile = "./pumpkinlauncher/pumpkinlauncher.dts";
	uiName = "Pumpkin Launcher";
	image = pumpkinlauncherImage;
};

datablock ShapeBaseImageData(pumpkinlauncherImage : tntlauncherImage)
{
   shapeFile = "./pumpkinlauncher/pumpkinlauncher.dts";
};

function pumpkinlauncherimage::onmount(%this, %Obj, %slot)
{
    if(%obj.totalloaded > 0)
        %obj.setimageammo(0,1);
    %obj.tntlauncherhud();
}

function pumpkinlauncherimage::onunmount(%this, %Obj, %slot)
{
    cancel(%obj.tnthud);
}

function pumpkinlauncherimage::onfire(%this, %Obj, %slot)
{
    %player=%obj;
    if(%player.totalloaded > 0)
    {
        if(%player.loaded[%Player.totalloaded-1] $= "Tier-1 Dynamite")
            %projectile = "dynamiteprojectile";
        else if(%player.loaded[%Player.totalloaded-1] $= "Tier-2 Dynamite")
            %projectile = "dynamite2projectile";
        else if(%player.loaded[%Player.totalloaded-1] $= "Tier-3 Dynamite")
            %projectile = "dynamite3projectile";
        else if(%player.loaded[%Player.totalloaded-1] $= "Tier-4 Dynamite")
            %projectile = "dynamite4projectile";
        else if(%player.loaded[%Player.totalloaded-1] $= "Tier-5 Dynamite")
            %projectile = "dynamite5projectile";
        else if(%player.loaded[%Player.totalloaded-1] $= "Tier-6 Dynamite")
            %projectile = "dynamite6projectile";
        %p = new Projectile()
        {
            dataBlock = %projectile;
            initialVelocity = vectorscale(%obj.getmuzzlevector(0),30);
            initialPosition = %obj.getMuzzlePoint(0);
            sourceObject = %obj;
            sourceSlot = 0;
            client = %obj.client;
        };
        MissionCleanup.add(%p);
        %player.loaded[%player.totalloaded-1] = "";
        %player.totalloaded--;
    }
    if(%player.totalloaded <= 0)
        %player.setimageammo(0,0);
}

datablock ItemData(giftlauncherItem : tntlauncheritem)
{
	shapeFile = "./giftlauncher/giftlauncher.dts";
	uiName = "Gift Launcher";
	image = giftlauncherImage;
};

datablock ShapeBaseImageData(giftlauncherImage : tntlauncherImage)
{
   shapeFile = "./giftlauncher/giftlauncher.dts";
};

function giftlauncherimage::onmount(%this, %Obj, %slot)
{
    if(%obj.totalloaded > 0)
        %obj.setimageammo(0,1);
    %obj.tntlauncherhud();
}

function giftlauncherimage::onunmount(%this, %Obj, %slot)
{
    cancel(%obj.tnthud);
}

function giftlauncherimage::onfire(%this, %Obj, %slot)
{
    %player=%obj;
    if(%player.totalloaded > 0)
    {
        if(%player.loaded[%Player.totalloaded-1] $= "Tier-1 Dynamite")
            %projectile = "dynamiteprojectile";
        else if(%player.loaded[%Player.totalloaded-1] $= "Tier-2 Dynamite")
            %projectile = "dynamite2projectile";
        else if(%player.loaded[%Player.totalloaded-1] $= "Tier-3 Dynamite")
            %projectile = "dynamite3projectile";
        else if(%player.loaded[%Player.totalloaded-1] $= "Tier-4 Dynamite")
            %projectile = "dynamite4projectile";
        else if(%player.loaded[%Player.totalloaded-1] $= "Tier-5 Dynamite")
            %projectile = "dynamite5projectile";
        else if(%player.loaded[%Player.totalloaded-1] $= "Tier-6 Dynamite")
            %projectile = "dynamite6projectile";
        %p = new Projectile()
        {
            dataBlock = %projectile;
            initialVelocity = vectorscale(%obj.getmuzzlevector(0),30);
            initialPosition = %obj.getMuzzlePoint(0);
            sourceObject = %obj;
            sourceSlot = 0;
            client = %obj.client;
        };
        MissionCleanup.add(%p);
        %player.loaded[%player.totalloaded-1] = "";
        %player.totalloaded--;
    }
    if(%player.totalloaded <= 0)
        %player.setimageammo(0,0);
}