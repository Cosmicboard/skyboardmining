exec("./drills.cs");
exec("./blockplacement.cs");

function e(){exec("add-ons/server_mining2/tools/granddesign/server.cs");}

datablock ItemData(granddesignItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./granddesign.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Grand Design";
	iconName = "./icon_granddesign";
	doColorShift = false;
	colorShiftColor = "0.471 0.471 0.471 1.000";

	 // Dynamic properties defined by the scripts
	image = granddesignImage;
	canDrop = true;
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(granddesignImage)
{
   // Basic Item properties
   shapeFile = "./granddesign.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "-0.2 0.31 0.3";
   rotation = eulertomatrix("0 0 -15");

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = false;

   eyeOffset = "1.2 1.5 -0.55";

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = granddesignItem;
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

function player::grandtorchhud(%player)
{
    if(%player.option == 0)
    {
        if(%player.client.inventory["Coal"] < 1)
            %color = "\c0";
        else
            %color = "\c2";
        return "<just:right>\c6Selected: \c3Torch\c7 (LIGHT TO SWITCH)" NL "\c6Requires:" SPC %color @ "1x Coal\c6" SPC "\c7(" @ mfloor(%player.client.inventory["Coal"]) @ "x Coal)";
    }
    else if(%player.option == 1)
    {
        if(%player.client.inventory["Magnesium"] < 1)
            %color = "\c0";
        else
            %color = "\c2";
        return "<just:right>\c6Selected: \c3Magnesium Torch\c7 (LIGHT TO SWITCH)" NL "\c6Requires:" SPC %color @ "1x Magnesium\c6" SPC "\c7(" @ mfloor(%player.client.inventory["Magnesium"]) @ "x Magnesium)";
    }
    else if(%player.option == 2)
    {
        if(%player.client.inventory["Cryogenum"] < 1)
            %color = "\c0";
        else
            %color = "\c2";
        return "<just:right>\c6Selected: \c3Cryogenum Torch\c7 (LIGHT TO SWITCH)" NL "\c6Requires:" SPC %color @ "1x Cryogenum\c6" SPC "\c7(" @ mfloor(%player.client.inventory["Cryogenum"]) @ "x Cryogenum)";
    }
    else if(%player.option == 3)
    {
        if(%player.client.inventory["Lightstone"] < 1)
            %color = "\c0";
        else
            %color = "\c2";
        return "<just:right>\c6Selected: \c3Lightstone Torch\c7 (LIGHT TO SWITCH)" NL "\c6Requires:" SPC %color @ "1x Lightstone\c6" SPC "\c7(" @ mfloor(%player.client.inventory["Lightstone"]) @ "x Lightstone)";
    }
}

function player::grandplacementhud(%player)
{
    if(%player.placementoption == 0)
    {
        if(%player.client.inventory["Dirt"] < 1)
            %color = "\c0";
        else
            %color = "\c2";
        return "<just:right>\c6Selected: \c3Dirt\c7 (LIGHT TO SWITCH)" NL "\c6You have:" SPC %color @ mfloor(%player.client.inventory["Dirt"]) @ "x Dirt";
    }
    if(%player.placementoption == 1)
    {
        if(%player.client.inventory["Steel"] < 1)
            %color = "\c0";
        else
            %color = "\c2";
        return "<just:right>\c6Selected: \c3Steel\c7 (LIGHT TO SWITCH)" NL "\c6You have:" SPC %color @ mfloor(%player.client.inventory["Steel"]) @ "x Steel";
    }
}

function player::grandschematichud(%player)
{
    if(!%player.schematicoption)
        %player.schematicoption = 0;
    if(%player.schematicoption == 0)
    {
        return "<just:right>\c6Selected: \c3Stationary Drill\c7 (LIGHT TO SWITCH)" NL "\c6Items Required:" NL %player.client.reciteCrafts("Stationary Drill");
    }
}

function player::showgranddesign(%player)
{
    %client = %player.client;
    if(%client.designmode $= "")
        %client.designmode = "\c3Torch Placement";
    if(%client.designmode $= "\c3Torch Placement")
    {
        %client.centerprint("<just:right>\c6Mode:" SPC %client.designmode SPC "\c7(2x JET TO SWITCH)" NL %player.grandtorchhud());
        if(iseventpending(%player.blockplacement))
            cancel(%player.blockplacement);
        if(iseventpending(%player.schematicplacement))
            cancel(%player.schematicplacement);
        if(!iseventpending(%player.placement))
            %player.startplacement();
    }
    else if(%client.designmode $= "\c3Building")
    {
        %client.centerprint("<just:right>\c6Mode:" SPC %client.designmode SPC "\c7(2x JET TO SWITCH)" NL %player.grandplacementhud());
        if(iseventpending(%player.placement))
            cancel(%player.placement);
        if(iseventpending(%player.schematicplacement))
            cancel(%player.schematicplacement);
        if(!iseventpending(%player.blockplacement))
            %player.startblockplacement();
    }
    else if(%client.designmode $= "\c3Schematic")
    {
        %client.centerprint("<just:right>\c6Mode:" SPC %client.designmode SPC "\c7(2x JET TO SWITCH)" NL %player.grandschematichud());
        if(iseventpending(%player.placement))
            cancel(%player.placement);
        if(!iseventpending(%player.schematicplacement))
            %player.startschematicplacement();
        if(iseventpending(%player.blockplacement))
            cancel(%player.blockplacement);
    }
    %player.granddesign = %player.schedule(33, showgranddesign);
}

function granddesignimage::onmount(%this, %obj, %slot)
{
    %obj.showgranddesign();
}

function granddesignimage::onunmount(%this, %obj, %slot)
{
    cancel(%obj.granddesign);
    cancel(%obj.placement);
    cancel(%obj.blockplacement);
    cancel(%obj.schematicplacement);
    %obj.client.centerprint("");
}

function granddesignimage::onfire(%this, %obj, %slot)
{
    if(%obj.client.designmode $= "\c3Torch Placement")
    {
        if(%obj.client.craftedpickaxe["Placement Tool"])
            placeTorch(%obj);
        else
            {%obj.client.chatmessage("you don't have the placement tool crafted lol");%obj.client.playsound(errorsound);}
    }
    if(%obj.client.designmode $= "\c3Building")
    {
        placeBlock(%obj);
    }
    if(%obj.client.designmode $= "\c3Schematic")
    {
        placeDrill(%obj, %type);
    }
}

package grandDesign{
    function Player::Activatestuff(%obj)
	{
		Parent::Activatestuff(%obj);
        if(%obj.client.usingdrillfuelchamber)
            return;
        initcontainerradiussearch(vectoradd(%obj.geteyepoint(), vectorscale(%obj.geteyevector(),3)), 1, $typemasks::staticshapeobjecttype);
        while(isobject(%search = containersearchnext()))
        {
            if(%search.drill)
            {
                if(%obj.chosendrill != %search)
                {
                    %obj.chosenDrill = %search;
                    if(iseventpending(%obj.drillhud))
                        cancel(%obj.drillhud);
                    %obj.selectdrill();
                    %foundDrill = 1;
                    return;
                }
                else if(%obj.chosendrill == %search && %search.owner == %obj.client)
                {
                    if(!%obj.selectingDrill)
                    {%obj.selectDrillingPosition();%obj.selectingdrill = 1;}
                    %foundDrill = 1;
                    return;
                }
            }
        }
        if(!%foundDrill && !%Obj.selectingdrill)
        {
            if(iseventpending(%obj.drillhud))
                cancel(%obj.drillhud);
            %obj.chosendrill = "";
            %obj.client.centerprint();
        }
        else if(%obj.selectingdrill)
        {
            if(%obj.disabledrill)
            {
                %obj.client.chatmessage("too far away from driller");
                %obj.client.playsound(errorsound);
                return;
            }
            if(!%obj.client.chosendrillposition)
            {
                if(iseventpending(%obj.selectingposition))
                    cancel(%obj.selectingposition);
                %obj.selectingdrill = 0;
                return;
            }
            if(iseventpending(%obj.selectingposition))
                cancel(%obj.selectingposition);
            %obj.selectingdrill = 0;
            %obj.chosendrill.drillPosition = %obj.client.chosendrillposition;
            %obj.chosendrill.startdrill();
        }
    }
    function armor::ontrigger(%this, %player, %trigger, %val)
    {
        if(%trigger == 4 && %val)
        {
            if(%player.chosendrill && !%player.client.usingdrillfuelchamber)
            {
                initcontainerradiussearch(vectoradd(%player.geteyepoint(), vectorscale(%player.geteyevector(),3)), 1, $typemasks::staticshapeobjecttype);
                while(isobject(%search = containersearchnext()))
                {
                    if(%search.drill && %search.owner == %player.client)
                    {
                        %player.selectingdrill = 0;
                        if(iseventpending(%player.selectingPosition))
                            cancel(%player.selectingPosition);
                        %player.client.showDrillFuelChamberMenu();
                        return;
                    }
                }
            }
            if(%player.lastJetTime + 0.25 > $sim::time)
            {
                if(%player.client.designmode $= "\c3Torch Placement")
                {
                    %player.client.designmode = "\c3Building";
                }
                else if(%player.client.designmode $= "\c3Building")
                {
                    %player.client.designmode = "\c3Schematic";
                }
                else if(%player.client.designmode $= "\c3Schematic")
                {
                    %player.client.designmode = "\c3Torch Placement";
                }
            }
            %player.lastJetTime = $sim::time;
        }
        parent::ontrigger(%this, %player, %trigger, %val);
    }
};
activatepackage(grandDesign);