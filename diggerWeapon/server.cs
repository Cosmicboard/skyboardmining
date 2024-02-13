datablock AudioProfile(pickaxe_swing)
{
   filename    = "./pickaxe_swing.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(metal_click)
{
   filename    = "./metal_click.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(rockhit)
{
   filename    = "./rockhit.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(MINORS)
{
   filename    = "./MINORS.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock StaticShapeData(diggerPickaxeTrail)
{
  shapeFile = "./trail.dts";
};

datablock StaticShapeData(diggerPickaxeUpperTrail)
{
  shapeFile = "./uppertrail.dts";
};

datablock StaticShapeData(diggerPickaxeDiagTrail)
{
  shapeFile = "./diagtrail.dts";
};


datablock ItemData(diggerPickaxeItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "base/data/shapes/empty.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "";
	iconName = "";
	doColorShift = false;
	colorShiftColor = "0.471 0.471 0.471 1.000";

	 // Dynamic properties defined by the scripts
	image = diggerPickaxeImage;
	canDrop = true;
};

datablock ShapeBaseImageData(diggerPickaxeImage)
{
   // Basic Item properties
   shapeFile = "./diggerpickaxe.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = false;

   eyeOffset = "0.6 1.2 -0.45";

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = diggerPickaxeItem;
   ammo = " ";

   //melee particles shoot from eye node for consistancy
   melee = true;
   doRetraction = false;
   //raise your arm up or not
   armReady = true;

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

	stateName[1]						= "Ready";
	stateTransitionOnTriggerDown[1]		= "Prepare";
	stateScript[1]						= "onReady";
	stateAllowImageChange[1]			= true;

	stateName[3]						= "Prepare";
    stateTransitionOnTimeout[3]			= "Wait";
	stateTimeoutValue[3]		= 0.33;
	stateWaitForTimeout[3]			= true;
	stateAllowImageChange[3]			= false;
    stateSequence[3]						= "prepare";
	stateScript[3]						= "onPrepare";

    stateName[4]						= "Wait";
    stateTransitionOnTimeout[4]			= "Fire";
	stateTimeoutValue[4]		= 0.25;
	stateWaitForTimeout[4]			= true;
	stateAllowImageChange[4]			= false;

    stateName[5]						= "Fire";
    stateTransitionOnTimeout[5]			= "Wait2";
	stateTimeoutValue[5]		= 0.18;
	stateWaitForTimeout[5]			= true;
	stateAllowImageChange[5]			= false;
    stateSequence[5]						= "attack";
	stateScript[5]						= "onFire";

    stateName[6]						= "Wait2";
    stateTransitionOnTimeout[6]			= "Retract";
	stateTimeoutValue[6]		= 0.075;
	stateWaitForTimeout[6]			= true;
	stateAllowImageChange[6]			= false;

    stateName[7]						= "Retract";
    stateTransitionOnTimeout[7]			= "Wait3";
	stateTimeoutValue[7]		= 0.55;
	stateWaitForTimeout[7]			= true;
	stateAllowImageChange[7]			= false;
    stateSequence[7]						= "retract";
	stateScript[7]						= "onRetract";

    stateName[8]						= "Wait3";
    stateTransitionOnTimeout[8]			= "Ready";
	stateTimeoutValue[8]		= 0.15;
	stateWaitForTimeout[8]			= true;
	stateAllowImageChange[8]			= false;
};

function diggerpickaxeimage::onmount(%this, %obj, %slot)
{
    if(%obj.getclassname() !$= "AIPlayer")
        %obj.playthread(2, armreadyboth);
    %obj.hidenode("rhand");
    %obj.hidenode("lhand");
}

function diggerpickaxeimage::onunmount(%this, %obj, %slot)
{
    if(%obj.getclassname() !$= "AIPlayer")
        %obj.playthread(2, root);
    %obj.unhidenode("rhand");
    %obj.unhidenode("lhand");
}

function diggerpickaxeimage::onprepare(%this, %obj, %slot)
{
    serverplay3d(metal_click, %obj.getmuzzlepoint(%slot));
}

function diggerpickaxeimage::onfire(%this, %obj, %slot)
{
    serverplay3d(pickaxe_swing, %obj.getmuzzlepoint(%slot));
    %trail = new staticshape()
    {
        datablock = diggerPickaxeTrail;
        rotation = %obj.rotation;
        position = vectorsub(vectoradd(%obj.getmuzzlepoint(0), vectorscale(%obj.getmuzzlevector(%slot), -0.3)), "0 0 0.15");
        scale = vectorscale("5 5 2", getword(%obj.getscale(),2));
    };
    %trail.setnodecolor("ALL", "1 1 1 0.3");
    %trail.playthread(0, lash);
    %trail.schedule(180, delete);
    initcontainerradiussearch(%obj.getmuzzlepoint(%slot), 1, $typemasks::playerobjecttype);
    while(isobject(%search=containersearchnext()))
    {
        if(%search == %obj)
            continue;
        if(%search.getdatablock().getname() $= "mininghelmetplayer")
            continue;
        %dist = vectordist(%obj.getmuzzlepoint(%slot), %search.gethackposition());
        if(%dist > 3)
            continue;
        %search.addvelocity(vectoradd(vectorscale(%obj.getmuzzlevector(%slot), 15), "0 0 7"));
        %search.damage(%search, %obj, 15, %ray.gethackposition(), $damagetype::directgeneric);
        %emitter = new particleemitternode()
        {
            datablock = genericemitternode;
            emitter = swordexplosionemitter;
            position = %search.gethackposition();
            scale = vectorscale("0.4 0.4 0.4", getword(%search.getscale(),2));
        };
        %emitter.schedule(33, delete);
        serverplay3d(swordhitsound, %search.gethackposition());
    }
}

datablock ItemData(diggerPickaxe2Item : diggerPickaxeItem)
{
	image = diggerPickaxe2Image;
};

datablock ShapeBaseImageData(diggerPickaxe2Image : diggerPickaxeImage)
{
   shapeFile = "./diggerpickaxeupperswing.dts";
   item = diggerPickaxe2Item;

	stateName[0]						= "Activate";
	stateScript[0]						= "onActivate";
	stateTimeoutValue[0]				= 0.2;
	stateTransitionOnTimeout[0]			= "Ready";

	stateName[1]						= "Ready";
	stateTransitionOnTriggerDown[1]		= "Prepare";
	stateScript[1]						= "onReady";
	stateAllowImageChange[1]			= true;

	stateName[3]						= "Prepare";
    stateTransitionOnTimeout[3]			= "Wait";
	stateTimeoutValue[3]		= 0.33;
	stateWaitForTimeout[3]			= true;
	stateAllowImageChange[3]			= false;
    stateSequence[3]						= "prepare";
	stateScript[3]						= "onPrepare";

    stateName[4]						= "Wait";
    stateTransitionOnTimeout[4]			= "Fire";
	stateTimeoutValue[4]		= 0.33;
	stateWaitForTimeout[4]			= true;
    stateSequence[4]						= "prepare2";
	stateAllowImageChange[4]			= false;

    stateName[5]						= "Fire";
    stateTransitionOnTimeout[5]			= "Wait2";
	stateTimeoutValue[5]		= 0.15;
	stateWaitForTimeout[5]			= true;
	stateAllowImageChange[5]			= false;
    stateSequence[5]						= "attack";
	stateScript[5]						= "onFire";

    stateName[6]						= "Wait2";
    stateTransitionOnTimeout[6]			= "Retract";
	stateTimeoutValue[6]		= 0.4;
	stateWaitForTimeout[6]			= true;
	stateAllowImageChange[6]			= false;

    stateName[7]						= "Retract";
    stateTransitionOnTimeout[7]			= "Wait3";
	stateTimeoutValue[7]		= 0.55;
	stateWaitForTimeout[7]			= true;
	stateAllowImageChange[7]			= false;
    stateSequence[7]						= "retract";
	stateScript[7]						= "onRetract";

    stateName[8]						= "Wait3";
    stateTransitionOnTimeout[8]			= "Ready";
	stateTimeoutValue[8]		= 0.15;
	stateWaitForTimeout[8]			= true;
	stateAllowImageChange[8]			= false;
};

function diggerpickaxe2image::onmount(%this, %obj, %slot)
{
    if(%obj.getclassname() !$= "AIPlayer")
        %obj.playthread(2, armreadyboth);
    %obj.hidenode("rhand");
    %obj.hidenode("lhand");
}

function diggerpickaxe2image::onunmount(%this, %obj, %slot)
{
    if(%obj.getclassname() !$= "AIPlayer")
        %obj.playthread(2, root);
    %obj.unhidenode("rhand");
    %obj.unhidenode("lhand");
}

function diggerpickaxe2image::onprepare(%this, %obj, %slot)
{
    serverplay3d(metal_click, %obj.getmuzzlepoint(%slot));
}

function diggerpickaxe2image::onfire(%this, %obj, %slot)
{
    serverplay3d(pickaxe_swing, %obj.getmuzzlepoint(%slot));
    %trail = new staticshape()
    {
        datablock = diggerPickaxeUpperTrail;
        rotation = %obj.rotation;
        position = vectorsub(vectoradd(%obj.getmuzzlepoint(%slot), vectorscale(%obj.getmuzzlevector(%slot), -0.5)), "0 0 0.6");
        scale = vectorscale("1 5 5", getword(%obj.scale,2));
    };
    %trail.setnodecolor("ALL", "1 1 1 0.3");
    %trail.playthread(0, lash);
    %trail.schedule(160, delete);
    schedule(140, 0, pickaxeimpact, %obj, %obj.getmuzzlepoint(%slot));
    for(%i = -8; %i <= 8; %i++)
    {
        %vec = vectorscale("0 0 1", %i*0.125);
        %vec = getwords(%obj.getforwardvector(),0,1) SPC getword(%vec,2);
        %startpos = %obj.getmuzzlepoint(%slot);
        %endpos = vectoradd(%startpos, getwords(vectorscale(%vec, 6),0,1) SPC getword(%vec,2));
        %ray = containerraycast(%startpos, %endpos, $typemasks::playerobjecttype, %obj);
        if(firstword(%ray))
        {
            if(%ray.getdatablock().getname() $= "mininghelmetplayer")
                continue;
            %ray.addvelocity(vectoradd(vectorscale(%obj.getmuzzlevector(%slot), 4), "0 0 -15"));
            %ray.damage(%ray, %Obj, 20, %ray.gethackposition(), $damagetype::directgeneric);
            %emitter = new particleemitternode()
            {
                datablock = genericemitternode;
                emitter = swordexplosionemitter;
                position = getwords(%ray,1,3);
                scale = vectorscale("0.4 0.4 0.4", getword(%obj.scale,2));
            };
            %emitter.schedule(33, delete);
            serverplay3d(swordhitsound, getwords(%ray,1,3));
            break;
        }
    }
}

function pickaxeimpact(%obj, %pos)
{
    %ray = containerraycast(%pos, vectoradd(vectorsub(%pos, vectorscale("0 0 2.5", getword(%obj.scale,2))), getwords(vectorscale(%obj.getmuzzlevector(0), 2),0,1)), $typemasks::fxbrickobjecttype, %obj);
    if(firstword(%ray))
    {
        %proj = new projectile()
        {
            initialposition = getwords(%ray,1,3);
            datablock = rocketlauncherprojectile;
            sourceobject = %obj;
            sourceclient = %obj.client;
            scale = "1.25 1.25 1.25";
        };
        %vel = %obj.getvelocity();
        %proj.explode();
        %obj.setvelocity(%vel);
    }
    if(%obj.getmountedimage(0).getname() $= "diggerpickaxe2phase2image")
    {
        if(getword(%ray.position,2) $= "5996.6")
        {
            if(%ray.getdatablock().getname() $= "brick16xcubedata" && %ray.colorid == 6)
            {
                %ray.setcolor(7);
                %ray.setcolorfx(2);
                %ray2 = containerraycast(%ray.position, vectorsub(%ray.position, "0 0 18"), $typemasks::fxbrickobjecttype, %ray);
                if(firstword(%ray2))
                {
                    %ray2.setcolor(7);
                    %ray2.setcolorfx(2);
                }
            }
            else if(%ray.getdatablock().getname() $= "brick16xcubedata" && %ray.colorid == 7)
            {
                %ray.fakekillbrick("0 0 0", 1);
                %ray.schedule(1000, delete);
                %ray2 = containerraycast(%ray.position, vectorsub(%ray.position, "0 0 18"), $typemasks::fxbrickobjecttype, %ray);
                if(firstword(%ray2) && %ray2.colorid == 7)
                {
                    %ray2.fakekillbrick("0 0 0", 1);
                    %ray2.schedule(1000, delete);
                }
            }
        }
    }
}

datablock ParticleData(diggerdynamitetrailParticle)
{
	dragCoefficient      = 3;
	gravityCoefficient   = -0.0;
	inheritedVelFactor   = 0.15;
	constantAcceleration = 0.0;
	lifetimeMS           = 1000;
	lifetimeVarianceMS   = 0;
	textureName          = "base/data/particles/dot";
	spinSpeed		= 10.0;
	spinRandomMin		= 0.0;
	spinRandomMax		= 0.0;
	colors[0]     = "1 1 1 0.4";
	colors[1]     = "1 1 1 0.5";

	sizes[0]      = 0.1;
	sizes[1]      = 0.05;

	useInvAlpha = false;
};
datablock ParticleEmitterData(diggerdynamitetrailEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 1;
   ejectionVelocity = 0;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "diggerdynamitetrailParticle";

   uiName = "";
};

datablock ProjectileData(diggerdynamiteprojectile)
{
    explosion = tankshellexplosion;
   projectileShapeName = "./dynamiteModel.dts";
   directDamage        = 0;
   particleEmitter     = "diggerdynamitetrailemitter";
   muzzleVelocity      = 75;
   velInheritFactor    = 0;
   armingDelay         = 1750;
   lifetime            = 1750;
   fadeDelay           = 3500;
   bounceElasticity    = 0.5;
   bounceFriction      = 0.20;
   isBallistic         = true;
   gravityMod = 0.9;
   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "1 0 0";
   uiName = "";
   explodeondeath = 1;
};

datablock ItemData(diggerPickaxe3Item : diggerPickaxeItem)
{
	image = diggerPickaxe3Image;
};

datablock ShapeBaseImageData(diggerPickaxe3Image : diggerPickaxeImage)
{
   shapeFile = "./diggerpickaxedynamite.dts";
   item = diggerPickaxe2Item;

	stateName[0]						= "Activate";
	stateScript[0]						= "onActivate";
	stateTimeoutValue[0]				= 0.2;
	stateTransitionOnTimeout[0]			= "Ready";

	stateName[1]						= "Ready";
	stateTransitionOnTriggerDown[1]		= "Prepare";
	stateScript[1]						= "onReady";
	stateAllowImageChange[1]			= true;

	stateName[3]						= "Prepare";
    stateTransitionOnTimeout[3]			= "Wait";
	stateTimeoutValue[3]		= 0.4;
	stateWaitForTimeout[3]			= true;
	stateAllowImageChange[3]			= false;
    stateSequence[3]						= "prepare";
	stateScript[3]						= "onPrepare";

    stateName[4]						= "Wait";
    stateTransitionOnTimeout[4]			= "Throw";
	stateTimeoutValue[4]		= 0.15;
	stateWaitForTimeout[4]			= true;
	stateAllowImageChange[4]			= false;

    stateName[5]						= "Throw";
    stateTransitionOnTimeout[5]			= "Fire";
	stateTimeoutValue[5]		= 0.2;
	stateWaitForTimeout[5]			= true;
	stateAllowImageChange[5]			= false;
    stateSequence[5]						= "throw";
	stateScript[5]						= "onThrow";

    stateName[6]						= "Fire";
    stateTransitionOnTimeout[6]			= "Wait2";
	stateTimeoutValue[6]		= 0.5;
	stateWaitForTimeout[6]			= true;
	stateAllowImageChange[6]			= false;
    stateSequence[6]						= "attack";
	stateScript[6]						= "onFire";

    stateName[7]						= "Wait2";
    stateTransitionOnTimeout[7]			= "Ready";
	stateTimeoutValue[7]		= 0.2;
	stateWaitForTimeout[7]			= true;
	stateAllowImageChange[7]			= false;
};

function diggerpickaxe3image::onmount(%this, %obj, %slot)
{
    if(%obj.getclassname() !$= "AIPlayer")
        %obj.playthread(2, armreadyboth);
    %obj.hidenode("rhand");
    %obj.hidenode("lhand");
}

function diggerpickaxe3image::onunmount(%this, %obj, %slot)
{
    if(%obj.getclassname() !$= "AIPlayer")
        %obj.playthread(2, root);
    %obj.unhidenode("rhand");
    %obj.unhidenode("lhand");
}

function diggerpickaxe3image::onfire(%this, %obj, %slot)
{
    if(%obj.getclassname() !$= "aiplayer")
        %vel = vectorscale(%obj.geteyevector(), 30);
    else if(%obj.getclassname() $= "aiplayer" && isobject(%obj.target))
    {
        if(getword(%obj.target.getvelocity(),0) < 2.5 && getword(%obj.target.getvelocity(),0) > -2.5 && getword(%obj.target.getvelocity(),1) < 2.5 && getword(%obj.target.getvelocity(),1) > -2.5)
            %vel = vectorscale(vectornormalize(vectorsub(%obj.target.gethackposition(), %obj.getmuzzlepoint(%slot))), vectordist(%obj.gethackposition(), %obj.target.gethackposition()));
        else
        {
            %dist = vectordist(%obj.gethackposition(), %obj.target.gethackposition());
            %prediction = getwords(vectoradd(%obj.target.gethackposition(),%obj.target.getvelocity()),0,1) SPC getword(%obj.target.gethackposition(),2);
            %vel = vectorscale(vectornormalize(vectorsub(%prediction,%obj.getmuzzlepoint(%slot))), %dist);
        }
    }
    %proj = new projectile()
    {
        datablock = diggerdynamiteprojectile;
        initialposition = %Obj.getmuzzlepoint(%slot);
        initialvelocity = %vel;
        rotation = %obj.rotation;
        scale = "1 1 1";
        sourceobject = %obj;
        sourceclient = %obj.client;
    };
}

datablock ItemData(diggerPickaxe4Item : diggerPickaxeItem)
{
	image = diggerPickaxe4Image;
};

datablock ShapeBaseImageData(diggerPickaxe4Image : diggerPickaxeImage)
{
   shapeFile = "./diggerpickaxediag.dts";
   item = diggerPickaxe2Item;

	stateName[0]						= "Activate";
	stateScript[0]						= "onActivate";
	stateTimeoutValue[0]				= 0.2;
	stateTransitionOnTimeout[0]			= "Ready";

	stateName[1]						= "Ready";
	stateTransitionOnTriggerDown[1]		= "Prepare";
	stateScript[1]						= "onReady";
	stateAllowImageChange[1]			= true;

	stateName[3]						= "Prepare";
    stateTransitionOnTimeout[3]			= "Wait";
	stateTimeoutValue[3]		= 0.33;
	stateWaitForTimeout[3]			= true;
	stateAllowImageChange[3]			= false;
    stateSequence[3]						= "prepare";
	stateScript[3]						= "onPrepare";

    stateName[4]						= "Wait";
    stateTransitionOnTimeout[4]			= "Fire";
	stateTimeoutValue[4]		= 0.33;
	stateWaitForTimeout[4]			= true;
    stateSequence[4]						= "prepare2";
	stateAllowImageChange[4]			= false;

    stateName[5]						= "Fire";
    stateTransitionOnTimeout[5]			= "Wait2";
	stateTimeoutValue[5]		= 0.15;
	stateWaitForTimeout[5]			= true;
	stateAllowImageChange[5]			= false;
    stateSequence[5]						= "attack";
	stateScript[5]						= "onFire";

    stateName[6]						= "Wait2";
    stateTransitionOnTimeout[6]			= "Retract";
	stateTimeoutValue[6]		= 0.4;
	stateWaitForTimeout[6]			= true;
	stateAllowImageChange[6]			= false;

    stateName[7]						= "Retract";
    stateTransitionOnTimeout[7]			= "Wait3";
	stateTimeoutValue[7]		= 0.55;
	stateWaitForTimeout[7]			= true;
	stateAllowImageChange[7]			= false;
    stateSequence[7]						= "retract";
	stateScript[7]						= "onRetract";

    stateName[8]						= "Wait3";
    stateTransitionOnTimeout[8]			= "Ready";
	stateTimeoutValue[8]		= 0.15;
	stateWaitForTimeout[8]			= true;
	stateAllowImageChange[8]			= false;
};

function diggerpickaxe4image::onmount(%this, %obj, %slot)
{
    if(%obj.getclassname() !$= "AIPlayer")
        %obj.playthread(2, armreadyboth);
    %obj.hidenode("rhand");
    %obj.hidenode("lhand");
}

function diggerpickaxe4image::onunmount(%this, %obj, %slot)
{
    if(%obj.getclassname() !$= "AIPlayer")
        %obj.playthread(2, root);
    %obj.unhidenode("rhand");
    %obj.unhidenode("lhand");
}

function diggerpickaxe4image::onprepare(%this, %obj, %slot)
{
    serverplay3d(metal_click, %obj.getmuzzlepoint(%slot));
}

function diggerpickaxe4image::onfire(%this, %obj, %slot)
{
    serverplay3d(pickaxe_swing, %obj.getmuzzlepoint(%slot));
    %trail = new staticshape()
    {
        datablock = diggerPickaxeDiagTrail;
        rotation = %obj.rotation;
        position = vectorsub(vectoradd(%obj.getmuzzlepoint(%slot), vectorscale(%obj.getmuzzlevector(%slot), -0.5)), "0 0 0.6");
        scale = "2.5 5 5";
    };
    %trail.setnodecolor("ALL", "1 1 1 0.3");
    %trail.playthread(0, lash);
    %trail.schedule(160, delete);
    initcontainerradiussearch(%obj.getmuzzlepoint(%slot), 1, $typemasks::playerobjecttype);
    while(isobject(%search=containersearchnext()))
    {
        if(%search == %obj)
            continue;
        if(%search.getdatablock().getname() $= "mininghelmetplayer")
            continue;
        %dist = vectordist(%obj.getmuzzlepoint(%slot), %search.gethackposition());
        if(%dist > 3)
            continue;
        %search.addvelocity(vectoradd(vectorscale(%obj.getmuzzlevector(%slot), 20), "0 0 14"));
        %search.damage(%search, %obj, 15, %search.gethackposition(), $damagetype::directgeneric);
        %emitter = new particleemitternode()
        {
            datablock = genericemitternode;
            emitter = swordexplosionemitter;
            position = %search.gethackposition();
            scale = "0.4 0.4 0.4";
        };
        %emitter.schedule(33, delete);
        serverplay3d(swordhitsound, %search.gethackposition());
    }
    %rockpos = vectoradd(%obj.getmuzzlepoint(%slot), vectorcross(%obj.getmuzzlevector(%slot), "0 0 1"));
    pickaxerockthrow(%obj, %rockpos);
}

datablock ProjectileData(diggerrockprojectile)
{
    explosion = rocketexplosion;
   projectileShapeName = "./rockModel.dts";
   directDamage        = 15;
   muzzleVelocity      = 50;
   velInheritFactor    = 0;
   armingDelay         = 5000;
   lifetime            = 5000;
   fadeDelay           = 5500;
   isBallistic         = true;
   gravityMod = 0.55;
   uiName = "";
   explodeondeath = 1;
   explodeonimpact = 1;
};

function diggerrockprojectile::oncollision(%this, %obj, %col, %fade, %Pos, %norm)
{
    %obj.explode();
    parent::oncollision(%this, %obj, %col, %fade, %Pos, %norm);
}

function pickaxerockthrow(%obj, %pos)
{
    %ray = containerraycast(%pos, vectorsub(%pos, "0 0 4"), $typemasks::fxbrickobjecttype, %obj);
    if(firstword(%ray))
    {
        %allowspawn = 1;
        %pos = getwords(%ray,1,3);
    }
    if(%obj.getclassname() !$= "aiplayer")
        %vel = vectorscale(vectoradd(%obj.geteyevector(),"0 0 0.2"), 30);
    else if(%obj.getclassname() $= "aiplayer" && isobject(%obj.target))
    {
        if(getword(%obj.target.getvelocity(),0) < 2.5 && getword(%obj.target.getvelocity(),0) > -2.5 && getword(%obj.target.getvelocity(),1) < 2.5 && getword(%obj.target.getvelocity(),1) > -2.5)
            %vel = vectorscale(vectornormalize(vectorsub(%obj.target.geteyepoint(), %pos)), vectordist(%pos, %obj.target.geteyepoint()));
        else
        {
            %dist = vectordist(%obj.gethackposition(), %obj.target.gethackposition());
            %prediction = getwords(vectoradd(%obj.target.gethackposition(),%obj.target.getvelocity()),0,1) SPC getword(%obj.target.geteyepoint(),2);
            %vel = vectorscale(vectornormalize(vectorsub(%prediction,%pos)), %dist);
        }
    }
    if(%allowspawn)
    {
        serverplay3d(tink_ @ getrandom(0,2), %pos);
        %proj = new projectile()
        {
            initialposition = vectoradd(%pos, "0 0 0.5");
            initialvelocity = %vel;
            datablock = diggerrockprojectile;
            sourceobject = %obj;
            sourceclient = %obj.client;
            scale = "1.35 1.35 1.35";
        };
    }
}

exec("./phase2/phase2.cs");
exec("./challenge/server.cs");