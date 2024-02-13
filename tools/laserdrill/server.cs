package KeepWhenDead {
	function Armor::onDisabled(%this, %obj, %state) {
		if (%this.keepWhenDead) {
			return;
		}

		if (isObject(%obj.swordBot)) {
			%obj.swordBot.delete();
		}

		if (isObject(%obj.emptyBot)) {
			%obj.emptyBot.delete();
		}
		return parent::onDisabled(%this, %obj, %state);
	}
	function Armor::onRemove(%this, %obj) {
		if (isObject(%obj.swordBot)) {
			%obj.swordBot.delete();
		}
		if (isObject(%obj.emptyBot)) {
			%obj.emptyBot.delete();
		}

		return parent::onRemove(%this, %obj);
	}
};
activatePackage(KeepWhenDead);

function equipSword(%pl, %type) {
    if(%type $= "")
	    %type = %pl.getmountedimage(0).weapontype;
	if (!isObject(%pl.swordBot)) {
		%pl.swordBot = new AIPlayer(Sword) {
			datablock = %type;
			player = %pl;
		};
		%pl.swordBot.setScale(%pl.getScale());
		%pl.swordBot.kill();
		%pl.swordBot.mountedbot = 1;
	}
	if (!isObject(%pl.emptyBot)) {
		%pl.emptyBot = new AIPlayer(Sword) {
			datablock = %type;
			owner = %pl;
		};
		%pl.emptyBot.hideNode("ALL");
		%pl.swordBot.setScale(%pl.getScale());
		%pl.emptyBot.kill();
		%pl.emptyBot.mountedbot = 1;
	}
	if(isobject(%pl.swordbot) && isobject(%pl.emptybot))
	{
		%pl.mountObject(%pl.emptyBot, 0);
		%pl.emptyBot.mountObject(%pl.swordBot, 4);
		%pl.playThread(1, armReadyRight);

		%pl.swordbot.setnodecolor("ALL", "1 1 1 1");

		%pl.swordbot.setnodecolor("righthand", %pl.client.rhandcolor);
		%pl.swordbot.setnodecolor("lefthand", %pl.client.rhandcolor);
	}
}

datablock PlayerData(LaserDrill : PlayerStandardArmor) {
	shapeFile = "./laserdrill.dts";

	uiName = "";

	boundingBox = vectorScale("20 20 20", 4);
	crouchBoundingBox = vectorScale("20 20 20", 4);
	
	keepWhenDead = 1;
};

datablock PlayerData(ThermalDrill : PlayerStandardArmor) {
	shapeFile = "./thermaldrill/thermaldrill.dts";

	uiName = "";

	boundingBox = vectorScale("20 20 20", 4);
	crouchBoundingBox = vectorScale("20 20 20", 4);
	
	keepWhenDead = 1;
};

datablock PlayerData(CandyDrill : PlayerStandardArmor) {
	shapeFile = "./candydrill/candydrill.dts";

	uiName = "";

	boundingBox = vectorScale("20 20 20", 4);
	crouchBoundingBox = vectorScale("20 20 20", 4);
	
	keepWhenDead = 1;
};

datablock PlayerData(ValentineDrill : PlayerStandardArmor) {
	shapeFile = "./valentinedrill/valentinedrill.dts";

	uiName = "";

	boundingBox = vectorScale("20 20 20", 4);
	crouchBoundingBox = vectorScale("20 20 20", 4);
	
	keepWhenDead = 1;
};

datablock ItemData(laserdrillItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./laserdrillidle.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Laser Drill";
	iconName = "./icon_laserdrill";
	doColorShift = false;
	colorShiftColor = "0.471 0.471 0.471 1.000";

	 // Dynamic properties defined by the scripts
	image = laserdrillImage;
	canDrop = true;
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(laserdrillImage)
{
   // Basic Item properties
   shapeFile = "base/data/shapes/empty.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "-0.53 0 -1.45";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = false;

   eyeOffset = "0 1 -2";

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = laserdrillItem;
   ammo = " ";

   //melee particles shoot from eye node for consistancy
   melee = true;
   doRetraction = false;
   //raise your arm up or not
   armReady = true;
   undroppable = 1;

   weaponType = laserdrill;

   //casing = " ";
   doColorShift = false;
   colorShiftColor = "0.471 0.471 0.471 1.000";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state

	stateName[0]						= "Activate";
	stateScript[0]						= "onActivate";
	stateTimeoutValue[0]				= 0.2;
	stateTransitionOnTimeout[0]			= "Ready";
	stateSound[0]                    = weaponSwitchSound;

	stateName[1]						= "Ready";
	stateTransitionOnTriggerDown[1]		= "Fire";
	stateScript[1]						= "onReady";
	stateAllowImageChange[1]			= true;

	stateName[3]						= "Fire";
    stateTransitionOnTimeout[3]			= "Ready";
	stateTimeoutValue[3]		= 0.35;
	stateWaitForTimeout[3]			= true;
	stateAllowImageChange[3]			= false;
	stateScript[3]						= "onFire";
};

datablock ItemData(thermaldrillItem : laserdrillItem)
{
    shapeFile = "./thermaldrill/thermaldrillidle.dts";
	uiName = "Thermal Drill";
    image = thermaldrillImage;
};

datablock ShapeBaseImageData(thermaldrillImage : laserdrillImage)
{
   item = thermaldrillItem;
   weaponType = thermaldrill;
};

datablock ItemData(candydrillItem : laserdrillItem)
{
    shapeFile = "./candydrill/candydrillidle.dts";
	uiName = "Candy Drill";
    image = candydrillImage;
};

datablock ShapeBaseImageData(candydrillImage : laserdrillImage)
{
   item = candydrillItem;
   weaponType = candydrill;
};

datablock ItemData(valentinedrillItem : laserdrillItem)
{
    shapeFile = "./valentinedrill/valentinedrillidle.dts";
	uiName = "Valentine's Drill";
    image = valentinedrillImage;
};

datablock ShapeBaseImageData(valentinedrillImage : laserdrillImage)
{
   item = valentinedrillItem;
   weaponType = valentinedrill;
};

function player::equiplaserdrill(%obj)
{
    equipsword(%obj, %this.weapontype);
    %obj.hidenode(rhand);
    %obj.hidenode(lhand);
    %obj.hidenode(rhook);
    %obj.hidenode(lhook);
    %obj.playthread(2, armreadyboth);
    %obj.preparelaserdrill();
    if(isobject(%obj.cryogenumtank))
    {
        %obj.cryogenumtank.setnetflag(6,1);
        %obj.cryogenumtank.clearscopetoclient(%obj.client);
    }
}

function player::unequiplaserdrill(%obj)
{
    %obj.emptybot.delete();
    %obj.swordbot.delete();
    %obj.unhidenode(rhand);
    %obj.unhidenode(lhand);
    %obj.playthread(2, root);
    cancel(%obj.laserdrill);
    cancel(%obj.laserdrilling);
    if(isobject(%obj.cryogenumtank))
    {
        %obj.cryogenumtank.setnetflag(6,0);
        %obj.cryogenumtank.scopetoclient(%obj.client);
    }
    if(%obj.laserdrilling)
        %obj.client.laserdrilltime = $sim::time;
}

function laserdrillimage::onmount(%this, %obj, %slot)
{
    %obj.equiplaserdrill();
}

function laserdrillimage::onunmount(%this, %obj, %slot)
{
    %obj.unequiplaserdrill();
}

function thermaldrillimage::onmount(%this, %obj, %slot)
{
    %obj.equiplaserdrill();
}

function thermaldrillimage::onunmount(%this, %obj, %slot)
{
    %obj.unequiplaserdrill();
}

function candydrillimage::onmount(%this, %obj, %slot)
{
    %obj.equiplaserdrill();
}

function candydrillimage::onunmount(%this, %obj, %slot)
{
    %obj.unequiplaserdrill();
}

function valentinedrillimage::onmount(%this, %obj, %slot)
{
    %obj.equiplaserdrill();
}

function valentinedrillimage::onunmount(%this, %obj, %slot)
{
    %obj.unequiplaserdrill();
}


function player::preparelaserdrill(%player)
{
    %player.laserdrilling = 0;
    %player.laserdrillposition = "";
    %player.laserdrillhealth = 0;
    %player.laserdrillblock = "";
    %player.laserdrilloverheat = -0.5;
    %player.laserdrillspeed = 125;
    %ray = containerraycast(%player.geteyepoint(), vectoradd(%player.geteyepoint(),vectorscale(%player.geteyevector(),5)), $typemasks::fxbrickobjecttype, %player);
    if(firstword(%ray) && %ray.canmine)
    {
        if(!%ray.originalcolor)
            %ray.originalcolor = %ray.colorid;
        cancel(%ray.returncolor);
        %ray.setcolor(44);
        %ray.returncolor = %ray.schedule(100, setcolor, %ray.originalcolor);
    }
    %player.laserdrill = %player.schedule(100, preparelaserdrill);
}

function player::startlaserdrilling(%player)
{
    %ray = containerraycast(%player.geteyepoint(), vectoradd(%player.geteyepoint(),vectorscale(%player.geteyevector(),5)), $typemasks::fxbrickobjecttype, %player);
    if(firstword(%ray) && %ray.canmine)
    {
        if(%player.laserdrillhealth <= 0)
        {
            %player.laserdrillhealth = mfloatlength((%player.client.miningpower+%player.client.prestigeminingpower+%player.client.achievementminingpower) * (%player.client.miningmultiplier+1) * (%player.client.prestigeminingmultiplier+1) * (%player.client.achievementminingmultiplier+1),0) * (32.5 * ((1+%player.client.miningmultiplier+%player.client.prestigeminingmultiplier+%player.client.achievementminingmultiplier)/1.625));
            %drillposition = getwords(%ray,1,3);
            %zpos = mfloatlength(getword(%ray,3),0) @ ".1";
            %player.laserdrillposition = getwords(%drillposition,0,1) SPC %zpos;
            %player.laserdrillblock = firstword(%ray);
        }
    }
    if(%player.laserdrillhealth > 0)
    {
        if(%player.client.miningpowerloss > 0)
            %debuffed = 1.5;
        else
            %debuffed = 1;
        %player.laserdrilling = 1;
        %oldOreHealth = %search.health;
        mineOre(%player, %player.laserdrillblock, 2);
        %newOreHealth = %search.health;
        %player.drillhealth -= %oldOreHealth - %newOreHealth;
        if(%player.drillhealth < 0)
            %Player.drillhealth = 0;
        if(%player.laserdrillblock.maxhealth > 0)
            %maxhealth = %player.laserdrillblock.maxhealth;
        else
            %maxhealth = getfield($ore[%player.laserdrillblock.oreid],1);
        serverplay3d(tink_ @ getrandom(0,2), %player.laserdrillposition);
        if(%player.laserdrillhealth > 0 && vectordist(vectoradd(%player.geteyepoint(),vectorscale(%player.getmuzzlevector(0),2)),%player.laserdrillposition) < 5 && !%player.laserdrillblock.brickdigged)
            %player.laserdrilling = %player.schedule(%player.laserdrillspeed*%debuffed, startlaserdrilling);
        else
        {
            %player.laserdrilling = 0;
            %player.swordbot.playthread(0, slowerspin);
            %player.preparelaserdrill();
            %player.client.laserdrilltime = $sim::time;
        }
    }
    %player.client.centerprint("<just:right>\c2Ore's Health:" SPC %player.laserdrillblock.health @ " / " @ %maxhealth NL "\c3Drilling Power:" SPC mfloatlength((%player.client.miningpower+%player.client.prestigeminingpower+%player.client.achievementminingpower)*(1+%player.client.miningmultiplier)*(%player.client.prestigeminingmultiplier+1)*(%player.client.achievementminingmultiplier+1)*(%player.laserdrilloverheat+1)*(1-%player.client.miningpowerloss)*(1-%player.torchLoss),0) NL "\c5Overheat Multiplier: " @ %player.laserdrilloverheat*100 @ "%" NL "\c4Drilling Speed:" SPC %player.laserdrillspeed*%debuffed @ "ms" NL "\c0Distance:" SPC vectordist(vectoradd(%player.geteyepoint(),vectorscale(%player.getmuzzlevector(0),2)),%player.laserdrillposition) @ " / 5", 1);
    if(%player.laserdrillhealth > 0)
    {
        %Player.swordbot.playthread(0, spin);
        %player.laserdrilltick++;
        %player.laserdrilltick2++;
        if(%player.laserdrilltick2 >= 3)
        {
            %player.laserdrilloverheat+=0.005;
            %player.laserdrilltick2 = 0;
        }
        if(%player.laserdrilltick >= 5)
        {
            %player.laserdrilltick = 0;
            if(%player.laserdrillspeed > 50)
                %player.laserdrillspeed-=1;
        }
    }
}

function player::firelaserdrill(%obj)
{
    if(%obj.drillhealth > 0)
        return;
    %time = %obj.client.laserdrilltime + 25 - $sim::time;
    if(%obj.client.laserdrilltime + 25 > $sim::time)
    {
        %obj.client.centerprint("your laser drill is on a" SPC %time @ "s cooldown",1);
        %obj.client.playsound(errorsound);
        return;
    }
    else if(!%obj.laserdrilling)
    {
        cancel(%obj.laserdrill);
        %obj.startlaserdrilling();
    }
}

function laserdrillimage::onfire(%this, %obj, %slot)
{
    %obj.firelaserdrill();
}

function thermaldrillimage::onfire(%this, %obj, %slot)
{
    %obj.firelaserdrill();
}

function candydrillimage::onfire(%this, %obj, %slot)
{
    %obj.firelaserdrill();
}

function valentinedrillimage::onfire(%this, %obj, %slot)
{
    %obj.firelaserdrill();
}