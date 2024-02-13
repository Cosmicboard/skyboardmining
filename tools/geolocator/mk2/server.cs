datablock ItemData(geolocator2Item)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system
	shapeFile = "./geolocator2.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	uiName = "Geolocator MkII";
	iconName = "./icon_geolocator2";
	doColorShift =  false;
	colorShiftColor = "0.4 0.43 0.42 1.000";
	image = geolocator2Image;
	canDrop = true;
};

datablock ShapeBaseImageData(geolocator2Image)
{
	shapeFile = "./geolocator2.dts";
   emap = true;
   mountPoint = 0;
   offset = "-0.59 0.05 0.2";
   eyeOffset = 0; //"0.7 1.2 -0.5";
   //rotation = eulerToMatrix( "-10 0 -10" );
   correctMuzzleVector = true;
   className = "WeaponImage";
   item = geolocator2Item;
   melee = false;
   armReady = true;
   minShotTime = 0;   //minimum time allowed between shots (needed to prevent equip/dequip exploit)
   undroppable = 1;

   doColorShift = false;
   colorShiftColor = geolocator2Item.colorShiftColor;

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
	stateTimeoutValue[2]            = 0.5;
	stateAllowImageChange[2]        = false;
	stateScript[2]                  = "onFire";
	stateWaitForTimeout[2]		  = true;
};

function geolocator2image::onmount(%this, %obj, %slot)
{
	%obj.geolocator2tool();
}

function geolocator2image::onunmount(%this, %obj, %slot)
{
    %obj.foundore = "";
    %obj.foundore2 = "";
    %obj.client.centerprint("",0);
	cancel(%obj.usinggeolocator2);
}

function geolocator2image::onfire(%this, %obj, %slot)
{
    if(%obj.geolocator2cooldown + 0.5 > $sim::time)
        return;
    %obj.geolocator2cooldown = $sim::time;
    if(%obj.client.geolocator2find + 5 > $sim::time)
        return;
    %obj.client.geolocator2find = $sim::time;
    %obj.geolocator2radius(96);
}

function player::geolocator2tool(%player)
{
    %cooldown = "";
    if(%player.client.geolocator !$= "" && %player.foundore !$= "none" || %player.client.geolocator2 !$= "" && %player.foundore2 !$= "none")
	{
        if(%player.client.geolocator !$= "")
        {
            %ore = $ore[oreidfromname(%player.client.geolocator)];
            %color = convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield(%ore,5)),0) * 255));
            %color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield(%ore,5)),1) * 255));
            %color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield(%ore,5)),2) * 255));
            %color = "<color:" @ %color @ ">";
            %orefound = "\c6Selected Ore 1: " @ %color @ getfield(%ore,0);
        }
        else
        {
            %orefound = "\c0/setOre [ore name] to start looking for ores!";
        }
        if(%player.client.geolocator2 !$= "")
        {
            %ore = $ore[oreidfromname(%player.client.geolocator2)];
            %color = convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield(%ore,5)),0) * 255));
            %color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield(%ore,5)),1) * 255));
            %color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield(%ore,5)),2) * 255));
            %color = "<color:" @ %color @ ">";
            %orefound = %orefound NL "\c6Selected Ore 2: " @ %color @ getfield(%ore,0);
        }
        else
        {
            %orefound = %orefound NL "\c0/setOre2 [ore name] to start looking for ores!";
        }
		//%orefound = %orefound NL "\c3Click to scan ores nearby.";
        if(%player.client.geolocator2find + 5 >= $sim::time)
            %cooldown = "\c0Cooldown:" SPC mfloatlength(%player.client.geolocator2find + 5 - $sim::time,1) @ "s";
        else
            %cooldown = "\c3Click to scan ores nearby.";
    }
    else
    {
        %orefound = "\c0Not looking for any ores!" NL "\c0/setOre [ore name] to start looking for them!" NL "\c0/setOre2 [ore name] to start looking for them!";
    }
    if(%player.foundore !$= "none" && %player.foundore !$= "" && %player.foundore2 !$= "none" && %player.foundore2 !$= "")
    {
        for(%i = 0; %i < 2; %i++)
        {
            if(%i == 0)
                %ore = $ore[%player.foundore.oreid];
            else
                %ore = $ore[%player.foundore2.oreid];
            %color = convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield(%ore,5)),0) * 255));
            %color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield(%ore,5)),1) * 255));
            %color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield(%ore,5)),2) * 255));
            %color = "<color:" @ %color @ ">";
            if(%i == 0)
                %found1 = %color @ getfield(%ore,0) @ "\c6 -" SPC "[" @ %player.foundore.getposition() @ "]" SPC vectordist(vectoradd(%player.foundore.getposition(),"0 0 0.5"), %player.getmuzzlepoint(0)) SPC "\c6studs away.";
            else
                %found2 = %color @ getfield(%ore,0) @ "\c6 -" SPC "[" @ %player.foundore2.getposition() @ "]" SPC vectordist(vectoradd(%player.foundore2.getposition(),"0 0 0.5"), %player.getmuzzlepoint(0)) SPC "\c6studs away.";
        }
        %orefound = %found1 NL %found2;
    }
    else if(%player.foundore !$= "none" && %player.foundore !$= "")
    {
        %ore = $ore[%player.foundore.oreid];
        %color = convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield(%ore,5)),0) * 255));
        %color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield(%ore,5)),1) * 255));
        %color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield(%ore,5)),2) * 255));
        %color = "<color:" @ %color @ ">";
        %orefound = %color @ getfield(%ore,0) @ "\c6 -" SPC "[" @ %player.foundore.getposition() @ "]" SPC vectordist(vectoradd(%player.foundore.getposition(),"0 0 0.5"), %player.getmuzzlepoint(0)) SPC "\c6studs away.";
        %orefound = %orefound NL "\c0Could not locate" SPC getfield($ore[oreidfromname(%player.client.geolocator2)],0) SPC "nearby.";
    }
    else if(%player.foundore2 !$= "none" && %player.foundore2 !$= "")
    {
        %ore = $ore[%player.foundore2.oreid];
        %color = convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield(%ore,5)),0) * 255));
        %color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield(%ore,5)),1) * 255));
        %color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield(%ore,5)),2) * 255));
        %color = "<color:" @ %color @ ">";
        %orefound = "\c0Could not locate" SPC getfield($ore[oreidfromname(%player.client.geolocator)],0) SPC "nearby.";
        %orefound = %orefound NL %color @ getfield(%ore,0) @ "\c6 -" SPC "[" @ %player.foundore2.getposition() @ "]" SPC vectordist(vectoradd(%player.foundore2.getposition(),"0 0 0.5"), %player.getmuzzlepoint(0)) SPC "\c6studs away.";
    }
    else if(%player.foundore $= "none" && %player.foundore2 $= "none")
    {
        %orefound = "\c0No ores nearby found.";
        %cooldown = "\c0Cooldown:" SPC mfloatlength(%player.client.geolocator2find + 5 - $sim::time,1) @ "s";
        if(%player.client.geolocator2find + 5 < $sim::time)
            %player.foundore = "";
    }
    %player.client.centerprint("<just:right>\c6Locating ores:" NL "\c6Position:\c5" SPC mfloatlength(getword(%player.position,0),1) SPC mfloatlength(getword(%player.position,1),1) SPC mfloatlength(getword(%player.position,2),1) SPC "" NL %orefound NL %cooldown,0.5);
    %player.usinggeolocator2 = %player.schedule(100, geolocator2tool);
}

function player::geolocator2radius(%player, %radius)
{
    initContainerRadiusSearch(%player.getposition(), %radius, $typemasks::fxbrickobjecttype);
    while(isobject(%search = containersearchnext()))
    {
        if(!%search.brickdestroyed && %search.canmine && %player.client.geolocator $= getfield($ore[%search.oreid],0) && %foundore $= "")
            %foundore = %search;
        if(!%search.brickdestroyed && %search.canmine && %player.client.geolocator2 $= getfield($ore[%search.oreid],0) && %foundore2 $= "")
            %foundore2 = %search;
        if(%foundore !$= "" && %foundore2 !$= "")
            break;
    }
    if(%foundore || %foundore2)
    {
        if(%foundore)
            %player.foundore = %foundore;
        if(%foundore2)
            %player.foundore2 = %foundore2;
        %player.client.playsound(beep_checkout_sound);
    }
    else
    {
        if(!%foundore)
            %player.foundore = "none";
        if(!%foundore2)
            %player.foundore2 = "none";
        %player.client.playsound(errorsound);
    }
}

function servercmdsetore2(%client, %ore, %ore2, %ore3)
{
	if(%ore $= "")
    {
        %client.chatmessage("no ore specified bruh");
        %client.playsound(errorsound);
        return;
    }
    if(%ore !$= "")
        %name = %ore;
    if(%ore2 !$= "")
        %name = %name SPC %ore2;
    if(%ore3 !$= "")
        %name = %name SPC %ore3;
    if(oreidfromname(%name) == -1)
    {
        %client.chatmessage("not a valid ore bruh");
        %client.playsound(errorsound);
        return;
    }
	%client.chatmessage("\c2Geolocator will now look for" SPC getfield($ore[oreidfromname(%name)],0) @ ".");
    %client.playsound(beep_key_sound);
	%client.geolocator2 = %name;
}