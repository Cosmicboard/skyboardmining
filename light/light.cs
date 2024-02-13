datablock AudioProfile(cave1)
{
   filename    = "./cave1.wav";
   description = AudioClosest3d;
   preload = true;
};
datablock AudioProfile(cave2 : cave1)
{
   filename    = "./cave2.wav";
};
datablock AudioProfile(cave3 : cave1)
{
   filename    = "./cave3.wav";
};
datablock AudioProfile(cave4 : cave1)
{
   filename    = "./cave4.wav";
};
datablock AudioProfile(cave5 : cave1)
{
   filename    = "./cave5.wav";
};
datablock AudioProfile(cave6 : cave1)
{
   filename    = "./cave6.wav";
};
datablock AudioProfile(cave7 : cave1)
{
   filename    = "./cave7.wav";
};
datablock AudioProfile(cave8 : cave1)
{
   filename    = "./cave8.wav";
};
datablock AudioProfile(cave9 : cave1)
{
   filename    = "./cave9.wav";
};
datablock AudioProfile(cave10 : cave1)
{
   filename    = "./cave10.wav";
};
datablock AudioProfile(cave11 : cave1)
{
   filename    = "./cave11.wav";
};
datablock AudioProfile(cave12 : cave1)
{
   filename    = "./cave12.wav";
};
datablock AudioProfile(cave13 : cave1)
{
   filename    = "./cave13.wav";
};

datablock StaticShapeData(cubebox)
{
    shapeFile = "./box.dts";
};

datablock StaticShapeData(laserbeam)
{
    shapeFile = "./beam.dts";
};

datablock StaticShapeData(torch)
{
    shapeFile = "./torchmodel.dts";
};

datablock StaticShapeData(lantern)
{
    shapeFile = "./lantern/lanternmodel.dts";
};

datablock StaticShapeData(soullantern)
{
    shapeFile = "./lantern/soullanternmodel.dts";
};

datablock StaticShapeData(candle)
{
    shapeFile = "./candle/candle.dts";
};


function player::startplacement(%player)
{
    %startpoint = %player.geteyepoint();
    %ray = containerraycast(%startpoint, vectoradd(%startpoint,vectorscale(%player.geteyevector(),10)), $typemasks::fxbrickobjecttype, %player);
    if(firstword(%ray))
    {
        %offset = vectorSub(getwords(%ray,1,3), %player.getmuzzlepoint(0));
        %normal = vectorNormalize(%offset);
        %xyz = vectorNormalize(vectorCross("1 0 0", %normal));
        %pow = mRadToDeg(mACos(vectorDot("1 0 0", %normal))) * -1;
        %obj = new staticShape()
        {
            datablock = laserbeam;
            scale = vectorScale(vectorLen(%offset) SPC "0.1 0.1", 0.5);
            position = vectorScale(vectorAdd(getwords(%ray,1,3), %player.getmuzzlepoint(0)), 0.5);
            rotation = %xyz SPC %pow;
        };
        %obj.setnetflag(6, 1);
        %obj.scopetoclient(%player.client);
        %obj.setNodeColor("ALL", "0 1 0 1");
        %obj.schedule(100, delete);
        %obj.towerselection = 1;
        %obj2 = new staticShape()
        {
            datablock = cubebox;
            scale = "0.25 0.25 0.25";
            position = getwords(%ray,1,3);
        };
        %obj2.setnetflag(6, 1);
        %obj2.scopetoclient(%player.client);
        %obj2.setNodeColor("ALL", "0 1 0 1");
        %obj2.schedule(100, delete);
        %obj2.towerselection = 1;
        %player.blocked = 0;
        %player.placementposition = %obj2.getposition();
        initContainerradiusSearch(%obj2.getposition(), 15, $typemasks::staticshapeobjecttype);
        while(isobject(%search = containersearchnext()))
        {
            if(%search.regulartorch && vectordist(getwords(%ray,1,3), %search.position) < 5)
            {
                %player.blocked = 1;
                %obj.setNodeColor("ALL", "1 0 0 1");
                %obj2.setNodeColor("ALL", "1 0 0 1");
            }
            else if(%search.magnesiumtorch && vectordist(getwords(%ray,1,3), %search.position) < 12)
            {
                %player.blocked = 1;
                %obj.setNodeColor("ALL", "1 0 0 1");
                %obj2.setNodeColor("ALL", "1 0 0 1");
            }
            else if(%search.cryogenumtorch && vectordist(getwords(%ray,1,3), %search.position) < 8)
            {
                %player.blocked = 1;
                %obj.setNodeColor("ALL", "1 0 0 1");
                %obj2.setNodeColor("ALL", "1 0 0 1");
            }
            else if(%search.lightstonetorch && vectordist(getwords(%ray,1,3), %search.position) < 12)
            {
                %player.blocked = 1;
                %obj.setNodeColor("ALL", "1 0 0 1");
                %obj2.setNodeColor("ALL", "1 0 0 1");
            }
        }
        if(!%ray.canmine)
        {
            %player.blocked = 1;
            %obj.setNodeColor("ALL", "1 0 0 1");
            %obj2.setNodeColor("ALL", "1 0 0 1");
        }
    }
    if(isobject(%player))
        %player.placement = %player.schedule(100, startplacement);
    %player.recentlychanged = 0;
}

function player::placementhud(%player)
{
    if(%player.option == 0)
    {
        if(%player.client.inventory["Coal"] < 1)
            %color = "\c0";
        else
            %color = "\c2";
        %player.client.centerprint("<just:right>\c6Selected: \c3Torch\c7 (LIGHT TO SWITCH)" NL "\c6Requires:" SPC %color @ "1x Coal\c6" SPC "\c7(" @ mfloor(%player.client.inventory["Coal"]) @ "x Coal)",0.25);
    }
    else if(%player.option == 1)
    {
        if(%player.client.inventory["Magnesium"] < 1)
            %color = "\c0";
        else
            %color = "\c2";
        %player.client.centerprint("<just:right>\c6Selected: \c3Magnesium Torch\c7 (LIGHT TO SWITCH)" NL "\c6Requires:" SPC %color @ "1x Magnesium\c6" SPC "\c7(" @ mfloor(%player.client.inventory["Magnesium"]) @ "x Magnesium)",0.25);
    }
    else if(%player.option == 2)
    {
        if(%player.client.inventory["Cryogenum"] < 1)
            %color = "\c0";
        else
            %color = "\c2";
        %player.client.centerprint("<just:right>\c6Selected: \c3Cryogenum Torch\c7 (LIGHT TO SWITCH)" NL "\c6Requires:" SPC %color @ "1x Cryogenum\c6" SPC "\c7(" @ mfloor(%player.client.inventory["Cryogenum"]) @ "x Cryogenum)",0.25);
    }
    else if(%player.option == 3)
    {
        if(%player.client.inventory["Lightstone"] < 1)
            %color = "\c0";
        else
            %color = "\c2";
        %player.client.centerprint("<just:right>\c6Selected: \c3Lightstone Torch\c7 (LIGHT TO SWITCH)" NL "\c6Requires:" SPC %color @ "1x Lightstone\c6" SPC "\c7(" @ mfloor(%player.client.inventory["Lightstone"]) @ "x Lightstone)",0.25);
    }
    if(isobject(%player))
        %player.placementhud = %player.schedule(100, placementhud);
}

function placementtoolimage::onmount(%this, %obj, %slot)
{
    %obj.placementhud();
    %obj.startplacement();
}

function placementtoolimage::onunmount(%this, %obj, %slot)
{
    cancel(%obj.placement);
    cancel(%obj.placementhud);
}

function placementtoolimage::onfire(%this, %obj, %slot)
{
    placeTorch(%obj);
}

function placeTorch(%obj)
{
    if(%obj.blocked)
    {
        %obj.client.chatmessage("\c0cannot place too close to other torches");
        %obj.client.playsound(errorsound);
        return;
    }
    if(%obj.option == 0)
    {
        if(%obj.client.inventory["Coal"] < 1)
        {
            %obj.client.chatmessage("\c0bro no materials");
            %obj.client.playsound(errorsound);
            return;
        }
    }
    else if(%obj.option == 1)
    {
        if(%obj.client.inventory["Magnesium"] < 1)
        {
            %obj.client.chatmessage("\c0bro no materials");
            %obj.client.playsound(errorsound);
            return;
        }
    }
    else if(%obj.option == 2)
    {
        if(%obj.client.inventory["Cryogenum"] < 1)
        {
            %obj.client.chatmessage("\c0bro no materials");
            %obj.client.playsound(errorsound);
            return;
        }
    }
    else if(%obj.option == 3)
    {
        if(%obj.client.inventory["Lightstone"] < 1)
        {
            %obj.client.chatmessage("\c0bro no materials");
            %obj.client.playsound(errorsound);
            return;
        }
    }
    %ray = containerraycast(%obj.geteyepoint(), vectoradd(%obj.geteyepoint(),vectorscale(%obj.geteyevector(),10)), $typemasks::fxbrickobjecttype, %obj);
    if(firstword(%ray))
    {
        %pos = getwords(%ray,1,3);
        %rayNORTH = containerraycast(vectoradd(%pos,"0 -0.15 0.05"), vectoradd(%pos,"0 0.5 0"), $typemasks::fxbrickobjecttype, 0);
        if(firstword(%rayNORTH) && %direction $= "")
        {
            %axis = "25 0 0";
            %offset = "0 -0.07 0";
            %direction = "NORTH";
        }
        %raySOUTH = containerraycast(vectoradd(%pos,"0 0.15 0.05"), vectoradd(%pos,"0 -0.5 0"), $typemasks::fxbrickobjecttype, 0);
        if(firstword(%raySOUTH) && %direction $= "")
        {
            %axis = "-25 0 0";
            %offset = "0 0.07 0";
            %direction = "SOUTH";
        }
        %rayEAST = containerraycast(vectoradd(%pos,"-0.15 0 0.05"), vectoradd(%pos,"0.5 0 0"), $typemasks::fxbrickobjecttype, 0);
        if(firstword(%rayEAST) && %direction $= "")
        {
            %axis = "0 -25 0";
            %offset = "-0.07 0 0";
            %direction = "EAST";
        }
        %rayWEST = containerraycast(vectoradd(%pos,"0.15 0 0.05"), vectoradd(%pos,"-0.5 0 0"), $typemasks::fxbrickobjecttype, 0);
        if(firstword(%rayWEST) && %direction $= "")
        {
            %axis = "0 25 0";
            %offset = "0.07 0 0";
            %direction = "WEST";
        }
        %rayDOWN = containerraycast(vectoradd(%pos,"0 0 0.25"), vectoradd(%pos,"0 0 -0.5"), $typemasks::fxbrickobjecttype, %obj);
        if(firstword(%rayDOWN) && %direction $= "")
        {
            %axis = "0 0 0";
            %offset = "0 0 0.1";
            %direction = "DOWN";
        }
        %rayUP = containerraycast(vectoradd(%pos,"0 0 -0.25"), vectoradd(%pos,"0 0 1"), $typemasks::fxbrickobjecttype, %obj);
        if(firstword(%rayUP) && %direction $= "")
        {
            %axis = "0 180 0";
            %offset = "0 0 -0.5";
            %direction = "UP";
        }
        if(%obj.client.equippedcosmetic["Lantern"])
        {
            if(%obj.option == 2)
                %datablock = "soullantern";
            else
                %datablock = "lantern";
            %position = vectoradd(%pos,"0 0 0.25");
        }
        else if(%obj.client.equippedcosmetic["Candle"])
        {
            %datablock = "candle";
            %position = vectoradd(%pos,"0 0 0.25");
        }
        else
        {
            %datablock = "torch";
            %position = vectoradd(%pos,"0 0 0.25");
        }
        %shape = new staticShape()
        {
            datablock = %datablock;
            scale = "1 1 1";
            position = %position;
        };
        %shape.settransform(vectoradd(%shape.position,%offset) SPC eulertoaxis(%axis));
        %depth = 5000.2-getword(%shape.position,2);
        if(%obj.option == 0)
        {
            if(%depth < 1500)
            {
                %datablock = "orangeambientlight";
                %power = "1R";
            }
            if(%depth >= 1500)
            {
                %datablock = "orangedimambientlight";
                %power = "2R";
            }
            if(%depth >= 2250)
            {
                %datablock = "orangedimmerambientlight";
                %power = "3R";
            }
            if(%depth >= 2250)
            {
                %datablock = "orangedimmestambientlight";
                %power = "4R";
            }
            %emitter = playerJetEmitter;
            %offset2 = "0 0 0.3";
            %obj.client.inventory["Coal"]--;
            %shape.regulartorch = 1;
        }
        else if(%obj.option == 1)
        {
            if(%depth < 1500)
            {
                %datablock = "tier3ambientlight";
                %power = "1M";
            }
            if(%depth >= 1500)
            {
                %datablock = "tier2ambientlight";
                %power = "2M";
            }
            if(%depth >= 2250)
            {
                %datablock = "ambientlight";
                %power = "3M";
            }
            if(%depth >= 5000)
            {
                %datablock = "tier1ambientlight";
                %power = "4M";
            }
            %emitter = whiteburnemitterb;
            %offset2 = "0 0 0.1";
            %obj.client.inventory["Magnesium"]--;
            %shape.magnesiumtorch = 1;
        }
        else if(%obj.option == 2)
        {
            if(%depth < 4000)
            {
                %datablock = "tier3cryogenumlight";
                %power = "1C";
            }
            if(%depth >= 4000)
            {
                %datablock = "tier2cryogenumlight";
                %power = "2C";
            }
            if(%depth >= 4500)
            {
                %datablock = "tier1cryogenumlight";
                %power = "3C";
            }
            if(%depth >= 5000)
            {
                %datablock = "tier0cryogenumlight";
                %power = "4C";
            }
            %emitter = cyanburnemitterb;
            %offset2 = "0 0 0.1";
            %obj.client.inventory["Cryogenum"]--;
            %shape.cryogenumtorch = 1;
        }
        else if(%obj.option == 3)
        {
            if(%depth < 5000)
            {
                %datablock = "tier2lightstone";
                %power = "1L";
            }
            if(%depth >= 5000)
            {
                %datablock = "tier1lightstone";
                %power = "2L";
            }
            %emitter = lightburnemitterb;
            %offset2 = "0 0 0.1";
            %obj.client.inventory["Lightstone"]--;
            %shape.lightstonetorch = 1;
        }
        %shape.light = new fxLight ("")
        {
            dataBlock = %datablock;
            position = vectoradd(vectoradd(%shape.position,vectorscale(%offset,2)),"0 0 0.175");
            scale = "1.5 1.5 1.5";
        };
        if(!%obj.client.equippedcosmetic["Lantern"])
        {
            %shape.emitter = new ParticleEmitterNode ("")
            {
                dataBlock = GenericEmitterNode;
                scale = "0.15 0.15 0.15";
                emitter = %emitter;
                position = vectoradd(vectoradd(%shape.position,vectorscale(%offset,2)),%offset2);
            };
        }
        nameToID(CaveGroup).add(%shape);
        %shape.torch = 1;
        checkTorch(%shape.position, %shape, %direction, %power);
    }
}

function checkTorch(%pos, %shape, %direction, %power)
{
    if(!isobject(%shape))
        return;
    if(%direction $= "NORTH")
    {
        %offset = "0 -0.1 0";
        %offset2 = "0 1.05 0";
    }
    else if(%direction $= "SOUTH")
    {
        %offset = "0 0.1 0";
        %offset2 = "0 -1.05 0";
    }
    else if(%direction $= "WEST")
    {
        %offset = "0.1 0 0";
        %offset2 = "-1.05 0 0";
    }
    else if(%direction $= "EAST")
    {
        %offset = "-0.1 0 0";
        %offset2 = "1.05 0 0";
    }
    else if(%direction $= "DOWN")
    {
        %offset = "0 0 0";
        %offset2 = "0 0 -1.0";
    }
    else if(%direction $= "UP")
    {
        %offset = "0 0 -0.1";
        %offset2 = "0 0 1.05";
    }
    %ray = containerraycast(vectoradd(%pos,%offset), vectoradd(%pos,%offset2), $typemasks::fxbrickobjecttype, 0);
    if(!firstword(%ray))
    {
        deletetorch(%shape);
        return;
    }
    schedule(100, 0, playerInLight, %pos, %power);
    schedule(100, 0, checktorch, %pos, %shape, %direction, %power);
}

function playerInLight(%pos, %power)
{
    if(%power $= "1R")
        %radius = 10;
    else if(%power $= "2R")
        %radius = 8;
    else if(%power $= "3R")
        %radius = 6;
    else if(%power $= "4R")
        %radius = 3;

    if(%power $= "1M")
        %radius = 20;
    else if(%power $= "2M")
        %radius = 16;
    else if(%power $= "3M")
        %radius = 12;
    else if(%power $= "4M")
        %radius = 6;

    if(%power $= "1C")
        %radius = 11;
    else if(%power $= "2C")
        %radius = 10;
    else if(%power $= "3C")
        %radius = 9;
    else if(%power $= "4C")
        %radius = 4;

    if(%power $= "1L")
        %radius = 20;
    else if(%power $= "2L")
        %radius = 17;
    initContainerradiusSearch(%pos, %radius, $typemasks::playerobjecttype);
    while(isobject(%search=containersearchnext()))
    {
        if(vectordist(%pos, %search.getposition()) < %radius+1)
        {
            if(%power $= "1C" || %power $= "2C" || %power $= "3C" || %power $= "4C")
            {
                %search.inCold = 1;
                cancel(%search.nocold);
                %search.nocold = %search.schedule(100, notincold);
                %search.inLight = 1;
                cancel(%search.nolight);
                %search.nolight = %search.schedule(100, notinlight);
            }
            else
            {
                %search.inLight = 1;
                cancel(%search.nolight);
                %search.nolight = %search.schedule(100, notinlight);
            }
        }
    }
}

function player::notInLight(%player)
{
    %player.inLight = 0;
}

function player::notInCold(%player)
{
    %player.inCold = 0;
}

function deleteTorch(%obj)
{
    if(!%obj.torch)
        return;
    %obj.emitter.delete();
    %obj.light.delete();
    %obj.delete();
}

datablock ItemData(placementtoolItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "base/data/shapes/brickweapon.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Placement Tool";
	iconName = "./placementtool";
	doColorShift = false;
	colorShiftColor = "1 1 1 1.000";

	 // Dynamic properties defined by the scripts
	image = placementtoolImage;
	canDrop = true;
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(placementtoolImage)
{
   // Basic Item properties
   shapeFile = "base/data/shapes/brickweapon.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = 0; //"0.7 1.2 -0.5";
   rotation = eulerToMatrix( "0 0 0" );

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = placementtoolItem;
   ammo = " ";
   undroppable = 1;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;

   doColorShift = false;
   colorShiftColor = placementtoolItem.colorShiftColor;//"0.400 0.196 0 1.000";

   //casing = " ";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]                    = "Activate";
	stateTimeoutValue[0]            = 0.01;
	stateTransitionOnTimeout[0]     = "Ready";
	stateSound[0]					= weaponSwitchSound;
	
	stateName[1]                    = "Ready";
	stateTransitionOnTriggerDown[1] = "Fire";
	stateAllowImageChange[1]        = true;
	
	stateName[2]					= "Fire";
	stateTimeoutValue[2]			= 0.1;
	stateTransitionOnTimeout[2]		= "Cooldown";
	stateAllowImageChange[2]		= false;
	stateFire[2]					= true;
	stateScript[2]					= "onFire";
	
	stateName[3]					= "Cooldown";
	stateTimeoutValue[3]			= 0.1;
	stateScript[3]					= "onCooldown";
	stateTransitionOnTimeout[3]		= "Ready";
};