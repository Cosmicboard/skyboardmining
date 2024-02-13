datablock AudioProfile(cobaltcannonfire)
{
   filename    = "./cobaltcannonfire.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock ParticleData(CobaltCannonParticle)
{
	textureName          = "base/data/particles/dot";
	dragCoefficient      = 0.0;
	gravityCoefficient   = 0.0;
	inheritedVelFactor   = 0.0;
	windCoefficient      = 0;
	constantAcceleration = 0;
	lifetimeMS           = 100;
	lifetimeVarianceMS   = 0;
	spinSpeed     = 0;
	spinRandomMin = -90.0;
	spinRandomMax =  90.0;
	useInvAlpha   = false;

	colors[0]	= "0.0 1.0 0.0 1.0";
	colors[1]	= "0.0 1.0 0.0 1.0";
	colors[2]	= "0.0 1.0 0.0 0.0";

	sizes[0]	= 0.1;
	sizes[1]	= 0.1;
	sizes[2]	= 0.0;

	times[0]	= 0.0;
	times[1]	= 0.9;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(CobaltCannonEmitter)
{
   ejectionPeriodMS = 8;
   periodVarianceMS = 0;
   ejectionVelocity = 6.0;
   ejectionOffset   = 0.0;
   velocityVariance = 0.0;
   thetaMin         = 0;
   thetaMax         = 0;
   phiReferenceVel  = 0;
   phiVariance      = 0;
   overrideAdvance = false;
   particles = CobaltCannonParticle;   
};

datablock ParticleData(LuminiteCannonParticle : CobaltCannonParticle)
{
	colors[0]	= "1.0 1.0 0.0 1.0";
	colors[1]	= "1.0 1.0 0.0 1.0";
	colors[2]	= "1.0 1.0 0.0 0.0";
};

datablock ParticleEmitterData(LuminiteCannonEmitter : CobaltCannonEmitter)
{
   particles = LuminiteCannonParticle;   
};

datablock ParticleData(OlympiumCannonParticle : CobaltCannonParticle)
{
	colors[0]	= "1.0 0.0 1.0 1.0";
	colors[1]	= "1.0 0.0 1.0 1.0";
	colors[2]	= "1.0 0.0 1.0 0.0";
};

datablock ParticleEmitterData(OlympiumCannonEmitter : CobaltCannonEmitter)
{
   particles = OlympiumCannonParticle;   
};

datablock ProjectileData(CobaltCannonProjectile)
{
   projectileShapeName = "base/data/shapes/empty.dts";
   directDamage        = 0;
   directDamageType    = $DamageType::Direct;
   radiusDamageType    = $DamageType::Direct;
   particleEmitter     = "CobaltCannonEmitter";
   muzzleVelocity      = 50;
   velInheritFactor    = 1;
   armingDelay         = 500;
   lifetime            = 500;
   fadeDelay           = 500;
   bounceElasticity    = 0.5;
   bounceFriction      = 0.20;
   isBallistic         = false;
   gravityMod = 0.0;
   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "0 1 0";
};

datablock ProjectileData(LuminiteCannonProjectile : CobaltCannonProjectile)
{
   particleEmitter     = "LuminiteCannonEmitter";
   lightRadius = 3.0;
   lightColor  = "1 1 0";
};

datablock ProjectileData(OlympiumCannonProjectile : CobaltCannonProjectile)
{
   particleEmitter     = "OlympiumCannonEmitter";
   lightRadius = 3.0;
   lightColor  = "1 0 1";
};


datablock PlayerData(cobaltcannon : PlayerStandardArmor) {
	shapeFile = "./cobaltcannon.dts";

	uiName = "";

	boundingBox = vectorScale("20 20 20", 4);
	crouchBoundingBox = vectorScale("20 20 20", 4);
	
	keepWhenDead = 1;
};

datablock PlayerData(luminitecannon : PlayerStandardArmor) {
	shapeFile = "./luminite/cobaltcannon.dts";

	uiName = "";

	boundingBox = vectorScale("20 20 20", 4);
	crouchBoundingBox = vectorScale("20 20 20", 4);
	
	keepWhenDead = 1;
};

datablock PlayerData(olympiumcannon : PlayerStandardArmor) {
	shapeFile = "./olympium/cobaltcannon.dts";

	uiName = "";

	boundingBox = vectorScale("20 20 20", 4);
	crouchBoundingBox = vectorScale("20 20 20", 4);
	
	keepWhenDead = 1;
};

datablock ItemData(cobaltcannonItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./cobaltcannonidle.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Cobalt Cannon";
	iconName = "./icon_cobaltcannon";
	doColorShift = false;
	colorShiftColor = "0.471 0.471 0.471 1.000";

	 // Dynamic properties defined by the scripts
	image = cobaltcannonImage;
	canDrop = true;
};

datablock ShapeBaseImageData(cobaltcannonImage)
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
   item = cobaltcannonItem;
   ammo = " ";

   //melee particles shoot from eye node for consistancy
   melee = true;
   doRetraction = false;
   //raise your arm up or not
   armReady = true;
   undroppable = 1;

   weaponType = cobaltcannon;

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
	stateTransitionOnTriggerDown[0]		= "Ready";
	stateSound[0]                    = weaponSwitchSound;

	stateName[1]						= "FireCheck";
    stateTransitionOnTriggerUp[1]		= "Stop";
	stateTransitionOnTriggerDown[1]		= "Fire";
	stateScript[1]						= "onReady";
	stateAllowImageChange[1]			= true;

    stateName[2]						= "Ready";
	stateTransitionOnTriggerDown[2]		= "Fire";
	stateScript[2]						= "onReady";
	stateAllowImageChange[2]			= true;

	stateName[3]						= "Fire";
    stateTransitionOnTimeout[3]			= "FireCheck";
	stateTimeoutValue[3]		= 0.05;
	stateWaitForTimeout[3]			= true;
	stateAllowImageChange[3]			= false;
	stateScript[3]						= "onFire";

    stateName[4]						= "Stop";
    stateTransitionOnTimeout[4]			= "Ready";
	stateTimeoutValue[4]		= 0.35;
	stateWaitForTimeout[4]			= true;
	stateAllowImageChange[4]			= false;
	stateScript[4]						= "onStop";
};

datablock ItemData(luminitecannonItem : cobaltcannonItem)
{
	shapeFile = "./luminite/cobaltcannonidle.dts";
	uiName = "Luminite Cannon";
	iconName = "./luminite/icon_cobaltcannon";
	image = luminitecannonImage;
};
datablock ShapeBaseImageData(luminitecannonImage : cobaltcannonImage)
{
   item = luminitecannonItem;
   weaponType = luminitecannon;
};

datablock ItemData(olympiumcannonItem : cobaltcannonItem)
{
	shapeFile = "./olympium/cobaltcannonidle.dts";
	uiName = "Olympium Cannon";
	iconName = "./olympium/icon_cobaltcannon";
	image = olympiumcannonImage;
};
datablock ShapeBaseImageData(olympiumcannonImage : cobaltcannonImage)
{
   item = olympiumcannonItem;
   weaponType = olympiumcannon;
};

function player::equipcobaltcannon(%obj)
{
    equipsword(%obj);
    %obj.hidenode(rhand);
    %obj.hidenode(lhand);
    %obj.hidenode(rhook);
    %obj.hidenode(lhook);
    %obj.playthread(2, armreadyboth);
    if(isobject(%obj.cryogenumtank))
    {
        %obj.cryogenumtank.setnetflag(6,1);
        %obj.cryogenumtank.clearscopetoclient(%obj.client);
    }
}

function player::unequipcobaltcannon(%obj)
{
    %obj.emptybot.delete();
    %obj.swordbot.delete();
    %obj.unhidenode(rhand);
    %obj.unhidenode(lhand);
    %obj.playthread(2, root);
    if(isobject(%obj.cryogenumtank))
    {
        %obj.cryogenumtank.setnetflag(6,0);
        %obj.cryogenumtank.scopetoclient(%obj.client);
    }
}

function cobaltcannonimage::onmount(%this, %obj, %slot)
{
    %obj.equipcobaltcannon();
}

function cobaltcannonimage::onunmount(%this, %obj, %slot)
{
    %obj.unequipcobaltcannon();
}

function cobaltcannonimage::onstop(%this, %obj, %slot)
{
    %obj.swordbot.playthread(0, spinslower);
}

function player::cannonshoot(%obj, %type)
{
    if(%type == 0)
    {
        %datablock = "cobaltcannonprojectile";
        %ionization = 1;
        %damage = 8;
    }
    else if(%type == 1)
    {
        %datablock = "luminitecannonprojectile";
        %ionization = 80;
        %damage = 25;
    }
    else if(%type == 2)
    {
        %datablock = "olympiumcannonprojectile";
        %ionization = 325;
        %damage = 75;
    }
    %obj.swordbot.playthread(0, spin);
    %position = vectoradd(vectoradd(%obj.getmountedobject(0).position, "0 0 -0.35"), vectorscale(getwords(%obj.getforwardvector(),0,1) SPC getword(%obj.geteyevector(),2), 1.5));
    %rayPos = containerraycast(%obj.geteyepoint(), vectoradd(%obj.geteyepoint(), vectorscale(getwords(%obj.getforwardvector(),0,1) SPC getword(%obj.geteyevector(),2),30)), $typemasks::fxbrickobjecttype | $typemasks::playerobjecttype, %obj);
    if(firstword(%rayPos) && vectordist(getwords(%rayPos,1,3), %obj.geteyepoint()) > 4)
        %endPosition = getwords(%rayPos,1,3);
    else
        %endPosition = vectoradd(%obj.geteyepoint(), vectorscale(getwords(%obj.getforwardvector(),0,1) SPC getword(%obj.geteyevector(),2), 30));
    %p = new projectile()
    {
        datablock = %datablock;
        initialposition = %position;
        initialvelocity = vectorscale(vectornormalize(vectorsub(%endPosition, %position)),40);
    };
    serverplay3d(cobaltcannonfire, %position);
    %ray = containerraycast(%obj.geteyepoint(), vectoradd(%obj.geteyepoint(), vectorscale(%obj.geteyevector(), 20)), $typemasks::fxbrickobjecttype, %obj);
    if(firstword(%ray))
    {
        if(%ray.canmine)
        {
            if(oreidfromname("Ionized" SPC getfield($ore[%ray.oreid],0)) > 0)
            {
                %color = convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[%ray.oreid],5)),0) * 255));
                %color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[%ray.oreid],5)),1) * 255));
                %color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[%ray.oreid],5)),2) * 255));
                %color = "<color:" @ %color @ ">";
                %ray.ionization += %ionization;
                %req = mfloor(getfield($ore[%ray.oreid],1)/5);
                if(%ray.ionization >= %req)
                    {%ray.ionization=%req;%ray.turnOre(oreidfromname("Ionized" SPC getfield($ore[%ray.oreid],0)));}
                %obj.client.centerprint("<just:right><font:verdana:35>" @ %color @ getfield($ore[%ray.oreid],0) NL "<font:arial:20>\c2Ionization:" SPC mfloor(%ray.ionization) SPC "/" SPC %req SPC "",1);
            }
        }
    }
    %ray = containerraycast(%obj.geteyepoint(), vectoradd(%obj.geteyepoint(), vectorscale(%obj.geteyevector(), 20)), $typemasks::playerobjecttype, %obj);
    if(firstword(%ray))
    {
        if(%ray.miningAI)
        {
            %ray.damage(%obj, getwords(%ray,1,3), %damage, $damagetype::default);
        }
    }
}

function cobaltcannonimage::onfire(%this, %obj, %slot)
{
    %obj.cannonshoot(0);
}

function luminitecannonimage::onmount(%this, %obj, %slot)
{
    %obj.equipcobaltcannon();
}

function luminitecannonimage::onunmount(%this, %obj, %slot)
{
    %obj.unequipcobaltcannon();
}

function luminitecannonimage::onstop(%this, %obj, %slot)
{
    %obj.swordbot.playthread(0, spinslower);
}

function luminitecannonimage::onfire(%this, %obj, %slot)
{
    %obj.cannonshoot(1);
}

function olympiumcannonimage::onmount(%this, %obj, %slot)
{
    %obj.equipcobaltcannon();
}

function olympiumcannonimage::onunmount(%this, %obj, %slot)
{
    %obj.unequipcobaltcannon();
}

function olympiumcannonimage::onstop(%this, %obj, %slot)
{
    %obj.swordbot.playthread(0, spinslower);
}

function olympiumcannonimage::onfire(%this, %obj, %slot)
{
    %obj.cannonshoot(2);
}

