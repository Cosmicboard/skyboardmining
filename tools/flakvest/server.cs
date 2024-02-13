datablock ItemData(flakvestItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system
	shapeFile = "./flakvest.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	uiName = "Flak Vest";
	iconName = "./icon_flakvest";
	doColorShift =  false;
	colorShiftColor = "0.4 0.43 0.42 1.000";
	image = flakvestImage;
	canDrop = true;
};

datablock ShapeBaseImageData(flakvestImage)
{
	shapeFile = "./flakvest.dts";
   emap = true;
   mountPoint = 0;
   offset = "0.02 0.55 -0.05";
   rotation = eulertomatrix("0 0 90");
   eyeOffset = "0 0 0"; //"0.7 1.2 -0.5";
   correctMuzzleVector = true;
   className = "WeaponImage";
   item = flakvestItem;
   melee = false;
   armReady = true;
   minShotTime = 0;   //minimum time allowed between shots (needed to prevent equip/dequip exploit)
   undroppable = 1;

   doColorShift = false;

	stateName[0]                    = "Activate";
	stateTimeoutValue[0]            = 0;
	stateTransitionOnTimeout[0]     = "Ready";
	stateSound[0]			  = weaponSwitchSound;

	stateName[1]                    = "Ready";
	stateTransitionOnTriggerDown[1] = "Fire";
	stateAllowImageChange[1]        = true;

	stateName[2]                    = "Fire";
	stateSequence[2]          = "fire";
	stateTransitionOnTimeout[2]     = "Ready";
	stateTimeoutValue[2]            = 0.25;
	stateAllowImageChange[2]        = false;
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]		  = true;
};

function flakvestimage::onfire(%this, %obj, %slot)
{
    if(!%obj.getmountedimage(3))
    {
        %obj.mountimage(flakvestmountedimage, 3);
        %obj.client.chatmessage("\c6You are now wearing the Flak Vest. \c2-50%% Explosion Damage");
    }
    else
        %obj.unmountimage(3);
}

package mountimages
{
    function player::mountimage(%player, %image, %slot)
    {
        if(%slot == 3 && %player.getmountedimage(3) == nametoid(flakvestmountedimage) || %slot == 3 && %player.getmountedimage(3) == nametoid(hypervestmountedimage) )
            return;
        parent::mountimage(%player, %image, %slot);
    }
};
activatepackage(mountimages);

datablock ShapeBaseImageData(flakvestMountedImage)
{
	shapeFile = "./flakvest.dts";
   emap = true;
   mountPoint = 2;
   offset = "0 0.035 -0.6";
   rotation = eulertomatrix("0 0 0");
   eyeOffset = "0 0 -1000"; //"0.7 1.2 -0.5";
   correctMuzzleVector = true;
   className = "WeaponImage";
   item = flakvestItem;
   melee = false;
   armReady = true;
   minShotTime = 0;   //minimum time allowed between shots (needed to prevent equip/dequip exploit)

   doColorShift = false;

	stateName[0]                    = "Activate";
	stateTimeoutValue[0]            = 0.2;
	stateTransitionOnTimeout[0]     = "Ready";
	stateSound[0]			  = weaponSwitchSound;

	stateName[1]                    = "Ready";
	stateTransitionOnTriggerDown[1] = "Fire";
	stateAllowImageChange[1]        = true;

	stateName[2]                    = "Fire";
	stateSequence[2]          = "fire";
	stateTransitionOnTimeout[2]     = "Ready";
	stateTimeoutValue[2]            = 0.25;
	stateAllowImageChange[2]        = false;
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]		  = true;
};

datablock ItemData(hypervestItem : flakvestItem)
{
	shapeFile = "./hypervest/hypervest.dts";
	uiName = "Hypervest";
	image = hypervestImage;
};

datablock ShapeBaseImageData(hypervestImage : flakvestImage)
{
	shapeFile = "./hypervest/hypervest.dts";
   item = hypervestItem;
};

function hypervestimage::onfire(%this, %obj, %slot)
{
    if(!%obj.getmountedimage(3))
    {
        %obj.mountimage(hypervestmountedimage, 3);
        %obj.client.chatmessage("\c6You are now wearing the Flak Vest. \c2-50%% Explosion Damage");
    }
    else
        %obj.unmountimage(3);
}

datablock ShapeBaseImageData(hypervestMountedImage : flakvestMountedImage)
{
	shapeFile = "./hypervest/hypervest.dts";
   item = hypervestItem;
};