datablock ItemData(diggerPickaxeChallengeItem : diggerPickaxeImage)
{
	image = diggerPickaxeChallengeImage;
};

datablock ShapeBaseImageData(diggerPickaxeChallengeImage : diggerPickaxeImage)
{
   item = diggerPickaxeChallengeItem;
   
    stateName[4]						= "Wait";
    stateTransitionOnTimeout[4]			= "Fire";
	stateTimeoutValue[4]		= 0.15;
	stateWaitForTimeout[4]			= true;
	stateAllowImageChange[4]			= false;
};

function diggerpickaxechallengeimage::onmount(%this, %obj, %slot)
{
    if(%obj.getclassname() !$= "AIPlayer")
        %obj.playthread(2, armreadyboth);
    %obj.hidenode("rhand");
    %obj.hidenode("lhand");
}

function diggerpickaxechallengeimage::onunmount(%this, %obj, %slot)
{
    if(%obj.getclassname() !$= "AIPlayer")
        %obj.playthread(2, root);
    %obj.unhidenode("rhand");
    %obj.unhidenode("lhand");
}

function diggerpickaxechallengeimage::onprepare(%this, %obj, %slot)
{
    serverplay3d(metal_click, %obj.getmuzzlepoint(%slot));
}

function diggerpickaxechallengeimage::onfire(%this, %obj, %slot)
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

datablock ItemData(diggerPickaxe2ChallengeItem : diggerPickaxe2Item)
{
	image = diggerPickaxe2challengeImage;
};

datablock ShapeBaseImageData(diggerPickaxe2ChallengeImage : diggerPickaxe2Image)
{
   item = diggerPickaxe2Item;

    stateName[4]						= "Wait";
    stateTransitionOnTimeout[4]			= "Fire";
	stateTimeoutValue[4]		= 0.20;
	stateWaitForTimeout[4]			= true;
    stateSequence[4]						= "prepare2";
	stateAllowImageChange[4]			= false;
};

function diggerpickaxe2challengeimage::onmount(%this, %obj, %slot)
{
    if(%obj.getclassname() !$= "AIPlayer")
        %obj.playthread(2, armreadyboth);
    %obj.hidenode("rhand");
    %obj.hidenode("lhand");
}

function diggerpickaxe2challengeimage::onunmount(%this, %obj, %slot)
{
    if(%obj.getclassname() !$= "AIPlayer")
        %obj.playthread(2, root);
    %obj.unhidenode("rhand");
    %obj.unhidenode("lhand");
}

function diggerpickaxe2challengeimage::onprepare(%this, %obj, %slot)
{
    serverplay3d(metal_click, %obj.getmuzzlepoint(%slot));
}

function diggerpickaxe2challengeimage::onfire(%this, %obj, %slot)
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
    schedule(140, 0, pickaxeimpact2, %obj, %obj.getmuzzlepoint(%slot));
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

function pickaxeimpact2(%obj, %pos)
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
        if(%obj.getmountedimage(0) == nametoid(diggerpickaxe2challengeimage))
            %r = getrandom(9,13);
        else
            %r = getrandom(13,18);
        for(%i = 0; %i <= %r; %i++)
        {
            %scale = getrandom(45,85)/100 SPC getrandom(45,85)/100 SPC getrandom(45,85)/100;
            %vel = getwords(vectorscale(%obj.getforwardvector(), getrandom(15,40)),0,1) SPC getrandom(3,8);
            %rndm = getrandom(0,1);
            if(%rndm == 0)
                %rndm--;
            %newvel = vectorsub(%vel, vectorscale(vectorcross(%obj.getforwardvector(), "0 0" SPC %rndm),%rndm*getrandom(-125,125)/100));
            %position = vectoradd(getwords(%ray,1,3), getrandom(-5, 5)/10 SPC getrandom(-5, 5)/10 SPC 0);
            %proj = new projectile()
            {
                initialvelocity = %newvel;
                initialposition = %position;
                datablock = diggerrock2projectile;
                sourceobject = %obj;
                sourceclient = %obj.client;
                scale = %scale;
            };
        }
    }
    if(%obj.getmountedimage(0).getname() $= "diggerpickaxe2challengeimage")
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
        }
    }
    else if(%obj.getmountedimage(0).getname() $= "diggerpickaxe2challengephase2image")
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

datablock ItemData(diggerPickaxe3ChallengeItem : diggerPickaxe3Item)
{
	image = diggerPickaxe3ChallengeImage;
};

datablock ShapeBaseImageData(diggerPickaxe3ChallengeImage : diggerPickaxe3Image)
{
   item = diggerPickaxe3ChallengeItem;
};

function diggerpickaxe3Challengeimage::onmount(%this, %obj, %slot)
{
    if(%obj.getclassname() !$= "AIPlayer")
        %obj.playthread(2, armreadyboth);
    %obj.hidenode("rhand");
    %obj.hidenode("lhand");
}

function diggerpickaxe3Challengeimage::onunmount(%this, %obj, %slot)
{
    if(%obj.getclassname() !$= "AIPlayer")
        %obj.playthread(2, root);
    %obj.unhidenode("rhand");
    %obj.unhidenode("lhand");
}

function diggerpickaxe3Challengeimage::onfire(%this, %obj, %slot)
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

datablock ItemData(diggerPickaxe4ChallengeItem : diggerPickaxe4Item)
{
	image = diggerPickaxe4challengeImage;
};

datablock ShapeBaseImageData(diggerPickaxe4ChallengeImage : diggerPickaxe4Image)
{
   item = diggerPickaxe4ChallengeItem;

    stateName[4]						= "Wait";
    stateTransitionOnTimeout[4]			= "Fire";
	stateTimeoutValue[4]		= 0.2;
	stateWaitForTimeout[4]			= true;
    stateSequence[4]						= "prepare2";
	stateAllowImageChange[4]			= false;
};

function diggerpickaxe4Challengeimage::onmount(%this, %obj, %slot)
{
    if(%obj.getclassname() !$= "AIPlayer")
        %obj.playthread(2, armreadyboth);
    %obj.hidenode("rhand");
    %obj.hidenode("lhand");
}

function diggerpickaxe4Challengeimage::onunmount(%this, %obj, %slot)
{
    if(%obj.getclassname() !$= "AIPlayer")
        %obj.playthread(2, root);
    %obj.unhidenode("rhand");
    %obj.unhidenode("lhand");
}

function diggerpickaxe4Challengeimage::onprepare(%this, %obj, %slot)
{
    serverplay3d(metal_click, %obj.getmuzzlepoint(%slot));
}

function diggerpickaxe4Challengeimage::onfire(%this, %obj, %slot)
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
    pickaxerockthrow2(%obj, %rockpos);
}

function pickaxerockthrow2(%obj, %pos)
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
        %proj = new projectile()
        {
            initialposition = vectoradd(%pos, "0 0 0.5");
            initialvelocity = %vel;
            datablock = diggerrockprojectile;
            sourceobject = %obj;
            sourceclient = %obj.client;
            scale = "1.35 1.35 1.35";
        };
        if(%obj.getmountedimage(0) == nametoid(diggerpickaxe4challengeimage))
            %r = getrandom(9,13);
        else
            %r = getrandom(13,18);
        for(%i = 0; %i <= %r; %i++)
        {
            serverplay3d(tink_ @ getrandom(0,2), %pos);
            %scale = getrandom(45,85)/100 SPC getrandom(45,85)/100 SPC getrandom(45,85)/100;
            if(%obj.target)
                %vector = vectornormalize(vectorsub(%obj.target.geteyepoint(), %pos));
            else
                %vector = %obj.getforwardvector();
            %vel = getwords(vectorscale(%vector, getrandom(10,30)),0,1) SPC getrandom(2,6);
            %rndm = getrandom(0,1);
            if(%rndm == 0)
                %rndm--;
            %newvel = vectorsub(%vel, vectorscale(vectorcross(%vector, "0 0" SPC %rndm),%rndm*getrandom(-125,125)/100));
            %position = vectoradd(getwords(%ray,1,3), getrandom(-5, 5)/10 SPC getrandom(-5, 5)/10 SPC 0);
            %proj = new projectile()
            {
                initialvelocity = %newvel;
                initialposition = %position;
                datablock = diggerrock2projectile;
                sourceobject = %obj;
                sourceclient = %obj.client;
                client = %obj.client;
                scale = %scale;
            };
        }
    }
}

exec("./phase2.cs");

datablock ParticleData(diggerrockParticle)
{
   dragCoefficient      = 2;
   gravityCoefficient   = 1.0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   spinRandomMin = -90;
   spinRandomMax = 90;
   lifetimeMS           = 250;
   lifetimeVarianceMS   = 200;
   textureName          = "base/data/particles/chunk";
   colors[0]     = "0.2 0.2 0.2 0.9";
   colors[1]     = "0.1 0.1 0.1 0.0";
   sizes[0]      = 0.5;
   sizes[1]      = 0.25;
};

datablock ParticleEmitterData(diggerrockEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   ejectionVelocity = 8;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 60;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "diggerrockParticle";

   uiName = "";
};

datablock ExplosionData(diggerrockExplosion)
{
	soundProfile = rockhit;

   lifeTimeMS = 350;

   particleEmitter = diggerrockEmitter;
   particleDensity = 10;
   particleRadius = 0.2;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
   camShakeFreq = "3.0 3.5 3.0";
   camShakeAmp = "1.0 20.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 1.0;

   // Dynamic light

   damageRadius = 5;
   radiusDamage = 30;

   impulseRadius = 8;
   impulseForce = 1000;
};

datablock ProjectileData(diggerrock2projectile : diggerrockprojectile)
{
    explosion = diggerrockexplosion;
   directDamage        = 10;
   verticalImpulse = 500;
   impactImpulse = 500;
   muzzleVelocity      = 50;
   velInheritFactor    = 0;
   armingDelay         = 5000;
   lifetime            = 5000;
   fadeDelay           = 5500;
   isBallistic         = true;
   gravityMod = 0.35;
   explodeondeath = 1;
   explodeonimpact = 1;
};

function diggerrock2projectile::oncollision(%this, %obj, %col, %fade, %Pos, %norm)
{
    %obj.explode();
    parent::oncollision(%this, %obj, %col, %fade, %Pos, %norm);
}