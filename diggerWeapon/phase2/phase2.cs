datablock StaticShapeData(diggerPickaxephase2Trail)
{
  shapeFile = "./trail.dts";
};

datablock StaticShapeData(diggerPickaxephase2UpperTrail)
{
  shapeFile = "./uppertrail.dts";
};

datablock StaticShapeData(diggerPickaxephase2DiagTrail)
{
  shapeFile = "./diagtrail.dts";
};


datablock ItemData(diggerPickaxephase2Item : diggerPickaxeItem)
{
	shapeFile = "base/data/shapes/empty.dts";
	image = diggerPickaxephase2Image;
};

datablock ShapeBaseImageData(diggerPickaxephase2Image : diggerPickaxeImage)
{
   shapeFile = "./diggerpickaxe.dts";
   item = diggerPickaxephase2Item;
};

function diggerpickaxephase2image::onmount(%this, %obj, %slot)
{
    if(%obj.getclassname() !$= "AIPlayer")
        %obj.playthread(2, armreadyboth);
    %obj.hidenode("rhand");
    %obj.hidenode("lhand");
}

function diggerpickaxephase2image::onunmount(%this, %obj, %slot)
{
    if(%obj.getclassname() !$= "AIPlayer")
        %obj.playthread(2, root);
    %obj.unhidenode("rhand");
    %obj.unhidenode("lhand");
}

function diggerpickaxephase2image::onprepare(%this, %obj, %slot)
{
    serverplay3d(metal_click, %obj.getmuzzlepoint(%slot));
}

function diggerpickaxephase2image::onfire(%this, %obj, %slot)
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

datablock ItemData(diggerPickaxe2phase2Item : diggerPickaxe2Item)
{
	image = diggerPickaxe2phaseImage;
};

datablock ShapeBaseImageData(diggerPickaxe2phase2Image : diggerPickaxe2Image)
{
   shapeFile = "./diggerpickaxeupperswing.dts";
   item = diggerPickaxe2Item;
};

function diggerpickaxe2phase2image::onmount(%this, %obj, %slot)
{
    if(%obj.getclassname() !$= "AIPlayer")
        %obj.playthread(2, armreadyboth);
    %obj.hidenode("rhand");
    %obj.hidenode("lhand");
}

function diggerpickaxe2phase2image::onunmount(%this, %obj, %slot)
{
    if(%obj.getclassname() !$= "AIPlayer")
        %obj.playthread(2, root);
    %obj.unhidenode("rhand");
    %obj.unhidenode("lhand");
}

function diggerpickaxe2phase2image::onprepare(%this, %obj, %slot)
{
    serverplay3d(metal_click, %obj.getmuzzlepoint(%slot));
}

function diggerpickaxe2phase2image::onfire(%this, %obj, %slot)
{
    serverplay3d(pickaxe_swing, %obj.getmuzzlepoint(%slot));
    %trail = new staticshape()
    {
        datablock = diggerPickaxephase2UpperTrail;
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

datablock ItemData(diggerPickaxe3phase2Item : diggerPickaxe3Item)
{
	image = diggerPickaxe3phase2Image;
};

datablock ShapeBaseImageData(diggerPickaxe3phase2Image : diggerPickaxe3Image)
{
   shapeFile = "./diggerpickaxedynamite.dts";
   item = diggerPickaxe3phase2Item;
};

function diggerpickaxe3phase2image::onmount(%this, %obj, %slot)
{
    if(%obj.getclassname() !$= "AIPlayer")
        %obj.playthread(2, armreadyboth);
    %obj.hidenode("rhand");
    %obj.hidenode("lhand");
}

function diggerpickaxe3phase2image::onunmount(%this, %obj, %slot)
{
    if(%obj.getclassname() !$= "AIPlayer")
        %obj.playthread(2, root);
    %obj.unhidenode("rhand");
    %obj.unhidenode("lhand");
}

function diggerpickaxe3phase2image::onfire(%this, %obj, %slot)
{
    if(%obj.getclassname() !$= "aiplayer")
        %vel = vectorscale(%obj.geteyevector(), 30);
    else if(%obj.getclassname() $= "aiplayer")
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
        scale = "1.1 1.1 1.1";
        sourceobject = %obj;
        sourceclient = %obj.client;
    };
}

datablock ItemData(diggerPickaxe4phase2Item : diggerPickaxe4Item)
{
	image = diggerPickaxe4Image;
};

datablock ShapeBaseImageData(diggerPickaxe4phase2Image : diggerPickaxe4Image)
{
   shapeFile = "./diggerpickaxediag.dts";
   item = diggerPickaxe4Item;
};

function diggerpickaxe4phase2image::onmount(%this, %obj, %slot)
{
    if(%obj.getclassname() !$= "AIPlayer")
        %obj.playthread(2, armreadyboth);
    %obj.hidenode("rhand");
    %obj.hidenode("lhand");
}

function diggerpickaxe4phase2image::onunmount(%this, %obj, %slot)
{
    if(%obj.getclassname() !$= "AIPlayer")
        %obj.playthread(2, root);
    %obj.unhidenode("rhand");
    %obj.unhidenode("lhand");
}

function diggerpickaxe4phase2image::onprepare(%this, %obj, %slot)
{
    serverplay3d(metal_click, %obj.getmuzzlepoint(%slot));
}

function diggerpickaxe4phase2image::onfire(%this, %obj, %slot)
{
    serverplay3d(pickaxe_swing, %obj.getmuzzlepoint(%slot));
    %trail = new staticshape()
    {
        datablock = diggerPickaxephase2DiagTrail;
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
    pickaxerockthrow(%obj, %rockpos);
}