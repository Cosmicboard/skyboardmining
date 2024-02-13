datablock ItemData(jackhammerItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./jackhammer.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Tunneler";
	iconName = "./icon_jackhammer";
	doColorShift = false;
	colorShiftColor = "0.471 0.471 0.471 1.000";

	 // Dynamic properties defined by the scripts
	image = jackhammerImage;
	canDrop = true;
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(jackhammerImage)
{
   // Basic Item properties
   shapeFile = "./jackhammer.dts";
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
   item = jackhammerItem;
   ammo = " ";

   //melee particles shoot from eye node for consistancy
   melee = true;
   doRetraction = false;
   //raise your arm up or not
   armReady = true;
   undroppable = 1;

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

datablock ItemData(ghosthammerItem : jackhammerItem)
{
	shapeFile = "./ghosthammer.dts";
	uiName = "Ghosthammer";
	image = ghosthammerImage;
};

datablock ShapeBaseImageData(ghosthammerImage : jackhammerImage)
{
   shapeFile = "./ghosthammer.dts";
   item = ghosthammerItem;
};

datablock ItemData(frozenhammerItem : jackhammerItem)
{
	shapeFile = "./frozenhammer/frozenhammer.dts";
	uiName = "Icebreaker";
	image = frozenhammerImage;
};

datablock ShapeBaseImageData(frozenhammerImage : jackhammerImage)
{
   shapeFile = "./frozenhammer/frozenhammer.dts";
   item = frozenhammerItem;
};

datablock ItemData(festivehammerItem : jackhammerItem)
{
	shapeFile = "./festivehammer/festivehammer.dts";
	uiName = "Festive Tunneler";
	image = festivehammerImage;
};

datablock ShapeBaseImageData(festivehammerImage : jackhammerImage)
{
   shapeFile = "./festivehammer/festivehammer.dts";
   item = festivehammerItem;
};

function player::mounthammer(%obj)
{
    %obj.playthread(2, armreadyboth);
    %obj.preparedrill();
}

function player::unmounthammer(%obj)
{
    %obj.playthread(2, root);
    cancel(%obj.drill);
    cancel(%obj.drilling);
    if(%obj.drillhealth > 0)
        %obj.client.drilltime = $sim::time + (150 - %obj.client.tunnelercooldown) * ((%obj.totaldrillhealth - %obj.drillhealth) / %obj.totaldrillhealth) + 10;
    %obj.totaldrillhealth = 0;
    %obj.drillhealth = 0;
    %obj.drillposition = "";
    %obj.drillbrick = 0;
}

function jackhammerimage::onmount(%this, %obj, %slot)
{
    %obj.mounthammer();
}

function jackhammerimage::onunmount(%this, %obj, %slot)
{
    %obj.unmounthammer();
}

function ghosthammerimage::onmount(%this, %obj, %slot)
{
    %obj.mounthammer();
}

function ghosthammerimage::onunmount(%this, %obj, %slot)
{
    %obj.unmounthammer();
}

function frozenhammerimage::onmount(%this, %obj, %slot)
{
    %obj.mounthammer();
}

function frozenhammerimage::onunmount(%this, %obj, %slot)
{
    %obj.unmounthammer();
}

function festivehammerimage::onmount(%this, %obj, %slot)
{
    %obj.mounthammer();
}

function festivehammerimage::onunmount(%this, %obj, %slot)
{
    %obj.unmounthammer();
}

function player::preparedrill(%player)
{
    %ray = containerraycast(%player.geteyepoint(), vectoradd(%player.geteyepoint(),vectorscale(%player.geteyevector(),4)), $typemasks::fxbrickobjecttype, %player);
    if(firstword(%ray) && %ray.canmine)
    {
        initcontainerboxsearch(getwords(%ray,1,3), "1 1 1", $typemasks::fxbrickobjecttype);
        while(%search = containersearchnext())
        {
            if(%search.canmine)
            {
                if(getword(%search.position,2) != getword(%ray.position,2))
                    continue;
                if(!%search.originalcolor)
                    %search.originalcolor = %search.colorid;
                cancel(%search.returncolor);
                %search.setcolor(44);
                %search.returncolor = %search.schedule(100, setcolor, %search.originalcolor);
            }
        }
    }
    %player.drill = %player.schedule(100, preparedrill);
}

function player::startdrilling(%player, %candigdown)
{
    if(%Player.getstate() $= "dead")
        return;
    if(%candigdown)
        %player.drillposition = getwords(%player.drillposition,0,1) SPC getword(%player.drillposition,2)-1;
    %candigdown = 1;
    %ray = containerraycast(%player.geteyepoint(), vectoradd(%player.geteyepoint(),vectorscale(%player.geteyevector(),4)), $typemasks::fxbrickobjecttype, %player);
    if(firstword(%ray) && %ray.canmine)
    {
        if(%player.drillhealth <= 0)
        {
            %player.drillbrick = 0;
            %player.drillhealth = mfloatlength((250+%player.client.miningpower+%player.client.prestigeminingpower+%player.client.achievementminingpower) * (%player.client.miningmultiplier+1) * (%player.client.prestigeminingmultiplier+1),0) * (%player.client.achievementminingmultiplier+1) * (25 * ((1+%player.client.miningmultiplier+%player.client.prestigeminingmultiplier+%player.client.achievementminingmultiplier)/1.75));
            %player.totaldrillhealth = mfloatlength((250+%player.client.miningpower+%player.client.prestigeminingpower+%player.client.achievementminingpower) * (%player.client.miningmultiplier+1) * (%player.client.prestigeminingmultiplier+1) * (%player.client.achievementminingmultiplier+1),0) * (25 * ((1+%player.client.miningmultiplier+%player.client.prestigeminingmultiplier+%player.client.achievementminingmultiplier)/1.75));
            %drillposition = getwords(%ray,1,3);
            if(getword(%ray.position,2) <= -0.9)
                %zpos = mfloatlength(getword(%ray,3),0) @ ".9";
            else
                %zpos = mfloatlength(getword(%ray,3),0) @ ".1";
            %player.drillposition = getwords(%drillposition,0,1) SPC %zpos;
        }
    }
    if(%player.drillhealth > 0 && %player.drillbrick < 400)
    {
        initcontainerboxsearch(%player.drillposition, "1 1 1", $typemasks::fxbrickobjecttype);
        while(%search = containersearchnext())
        {
            if(%search.canmine && %player.drillhealth > 0 && %player.drillbrick < 400)
            {
                if(getword(%search.position,2) != getword(%player.drillposition,2) && %player.drillhealth > 0 && %player.drillbrick < 400)
                    continue;
                %found = 1;
                %candigdown = 0;
                %oldOreHealth = %search.health;
                mineOre(%player, %search, 1);
                %newOreHealth = %search.health;
                if(getfield($ore[%search.oreid],12) $= "LAVA")
                    %player.drillhealth -= %player.totaldrillhealth * (0.05 - (%player.client.tunnelerlavares/100));
                else
                    %player.drillhealth -= %oldOreHealth - %newOreHealth;
                if(%player.drillhealth < 0)
                    %Player.drillhealth = 0;
            }
        }
        if(%found)
            serverplay3d(tink_ @ getrandom(0,2), %player.drillposition);
        if(%player.drillhealth > 0 && %player.drillbrick < 400 && vectordist(%player.getposition(),%player.drillposition) < 4)
            %player.drilling = %player.schedule(200, startdrilling, %candigdown);
        else
        {
            %player.preparedrill();
            %player.client.drilltime = $sim::time + (150 - %player.client.tunnelercooldown) * ((%player.totaldrillhealth - %player.drillhealth) / %player.totaldrillhealth);
            %player.totaldrillhealth = 0;
            %player.drillhealth = 0;
            %player.drillposition = "";
            %player.drillbrick = 0;
        }
    }
    %player.client.centerprint("<just:right>\c3Drilling Power:" SPC mfloatlength((%player.client.miningpower+%player.client.prestigeminingpower+%player.client.achievementminingpower)*(1+%player.client.miningmultiplier)*(%player.client.prestigeminingmultiplier+1)*(%player.client.achievementminingmultiplier+1)*(1-(%player.client.miningpowerloss*0.67))*(1-(%player.torchLoss*0.67)),0) NL "\c2Drill Health: " @ %player.drillhealth NL "\c4Blocks Drilled:" SPC %player.drillbrick @ " / 400" NL "\c0Distance:" SPC vectordist(%player.getposition(),%player.drillposition) @ " / 4", 1);
}

function player::tunnelerfire(%obj)
{
    if(%obj.drillhealth > 0)
        return;
    %time = %obj.client.drilltime  - $sim::time;
    if(%obj.client.drilltime > $sim::time)
    {
        %obj.client.centerprint("your drill is on a" SPC %time @ "s cooldown",1);
        %obj.client.playsound(errorsound);
        return;
    }
    else
    {
        cancel(%obj.drill);
        %obj.startdrilling();
    }
}

function jackhammerimage::onfire(%this, %obj, %slot)
{
    %obj.tunnelerfire();
}

function ghosthammerimage::onfire(%this, %obj, %slot)
{
    %obj.tunnelerfire();
}

function frozenhammerimage::onfire(%this, %obj, %slot)
{
    %obj.tunnelerfire();
}

function festivehammerimage::onfire(%this, %obj, %slot)
{
    %obj.tunnelerfire();
}