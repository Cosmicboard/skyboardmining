datablock ItemData(diggerPickaxechallengephase2Item : diggerPickaxephase2Item)
{
	image = diggerPickaxechallengephase2Image;
};

datablock ShapeBaseImageData(diggerPickaxechallengephase2Image : diggerPickaxephase2Image)
{
   item = diggerPickaxechallengephase2Item;
};

function diggerpickaxechallengephase2image::onmount(%this, %obj, %slot)
{
    if(%obj.getclassname() !$= "AIPlayer")
        %obj.playthread(2, armreadyboth);
    %obj.hidenode("rhand");
    %obj.hidenode("lhand");
}

function diggerpickaxechallengephase2image::onunmount(%this, %obj, %slot)
{
    if(%obj.getclassname() !$= "AIPlayer")
        %obj.playthread(2, root);
    %obj.unhidenode("rhand");
    %obj.unhidenode("lhand");
}

function diggerpickaxechallengephase2image::onprepare(%this, %obj, %slot)
{
    serverplay3d(metal_click, %obj.getmuzzlepoint(%slot));
}

function diggerpickaxechallengephase2image::onfire(%this, %obj, %slot)
{
    serverplay3d(pickaxe_swing, %obj.getmuzzlepoint(%slot));
    %trail = new staticshape()
    {
        datablock = diggerPickaxephase2Trail;
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
        %search.damage(%search, %obj, 20, %ray.gethackposition(), $damagetype::directgeneric);
        %search.burnplayer(1);
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

datablock ItemData(diggerPickaxe2ChallengeItem : diggerPickaxe2challengephase2Item)
{
	image = diggerPickaxe2challengephase2Image;
};

datablock ShapeBaseImageData(diggerPickaxe2Challengephase2Image : diggerPickaxe2phase2Image)
{
   item = diggerPickaxe2Item;

    stateName[4]						= "Wait";
    stateTransitionOnTimeout[4]			= "Fire";
	stateTimeoutValue[4]		= 0.20;
	stateWaitForTimeout[4]			= true;
    stateSequence[4]						= "prepare2";
	stateAllowImageChange[4]			= false;
};

function diggerpickaxe2challengephase2image::onmount(%this, %obj, %slot)
{
    if(%obj.getclassname() !$= "AIPlayer")
        %obj.playthread(2, armreadyboth);
    %obj.hidenode("rhand");
    %obj.hidenode("lhand");
}

function diggerpickaxe2challengephase2image::onunmount(%this, %obj, %slot)
{
    if(%obj.getclassname() !$= "AIPlayer")
        %obj.playthread(2, root);
    %obj.unhidenode("rhand");
    %obj.unhidenode("lhand");
}

function diggerpickaxe2challengephase2image::onprepare(%this, %obj, %slot)
{
    serverplay3d(metal_click, %obj.getmuzzlepoint(%slot));
}

function diggerpickaxe2challengephase2image::onfire(%this, %obj, %slot)
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
            %ray.damage(%ray, %Obj, 25, %ray.gethackposition(), $damagetype::directgeneric);
            %ray.burnplayer(1);
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

datablock ItemData(diggerPickaxe3Challengephase2Item : diggerPickaxe3phase2Item)
{
	image = diggerPickaxe3Challengephase2Image;
};

datablock ShapeBaseImageData(diggerPickaxe3Challengephase2Image : diggerPickaxe3phase2Image)
{
   item = diggerPickaxe3Challengephase2Item;
};

function diggerpickaxe3Challengephase2image::onmount(%this, %obj, %slot)
{
    if(%obj.getclassname() !$= "AIPlayer")
        %obj.playthread(2, armreadyboth);
    %obj.hidenode("rhand");
    %obj.hidenode("lhand");
}

function diggerpickaxe3Challengephase2image::onunmount(%this, %obj, %slot)
{
    if(%obj.getclassname() !$= "AIPlayer")
        %obj.playthread(2, root);
    %obj.unhidenode("rhand");
    %obj.unhidenode("lhand");
}

function diggerpickaxe3Challengephase2image::onfire(%this, %obj, %slot)
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
    for(%i = 0; %i < 2; %i++)
    {
        %rndm = getrandom(0,1);
        if(%rndm == 0)
            %rndm--;
        %newvel = vectorsub(%vel, vectorscale(vectorcross(vectornormalize(vectorsub(%obj.target.gethackposition(), %obj.getmuzzlepoint(%slot))), "0 0" SPC %rndm),%rndm*getrandom(-300,300)/100));
        %proj = new projectile()
        {
            datablock = diggerdynamiteprojectile;
            initialposition = %Obj.getmuzzlepoint(%slot);
            initialvelocity = %newvel;
            rotation = %obj.rotation;
            scale = "1.15 1.15 1.15";
            sourceobject = %obj;
            sourceclient = %obj.client;
        };
    }
}

datablock ItemData(diggerPickaxe4Challengephase2Item : diggerPickaxe4phase2Item)
{
	image = diggerPickaxe4challengeImage;
};

datablock ShapeBaseImageData(diggerPickaxe4Challengephase2Image : diggerPickaxe4phase2Image)
{
   item = diggerPickaxe4ChallengeItem;

    stateName[4]						= "Wait";
    stateTransitionOnTimeout[4]			= "Fire";
	stateTimeoutValue[4]		= 0.2;
	stateWaitForTimeout[4]			= true;
    stateSequence[4]						= "prepare2";
	stateAllowImageChange[4]			= false;
};

function diggerpickaxe4Challengephase2image::onmount(%this, %obj, %slot)
{
    if(%obj.getclassname() !$= "AIPlayer")
        %obj.playthread(2, armreadyboth);
    %obj.hidenode("rhand");
    %obj.hidenode("lhand");
}

function diggerpickaxe4Challengephase2image::onunmount(%this, %obj, %slot)
{
    if(%obj.getclassname() !$= "AIPlayer")
        %obj.playthread(2, root);
    %obj.unhidenode("rhand");
    %obj.unhidenode("lhand");
}

function diggerpickaxe4Challengephase2image::onprepare(%this, %obj, %slot)
{
    serverplay3d(metal_click, %obj.getmuzzlepoint(%slot));
}

function diggerpickaxe4Challengephase2image::onfire(%this, %obj, %slot)
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
        %search.damage(%search, %obj, 20, %search.gethackposition(), $damagetype::directgeneric);
        %search.burnplayer(1);
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