datablock ItemData(geolocatorItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system
	shapeFile = "./radio.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	uiName = "Geolocator";
	iconName = "./icon_geolocator";
	doColorShift =  false;
	colorShiftColor = "0.4 0.43 0.42 1.000";
	image = geolocatorImage;
	canDrop = true;
};

datablock ShapeBaseImageData(geolocatorImage)
{
	shapeFile = "./radio.dts";
   emap = true;
   mountPoint = 0;
   offset = "0.02 0.1 0.2";
   eyeOffset = 0; //"0.7 1.2 -0.5";
   rotation = eulerToMatrix( "-10 0 -10" );
   correctMuzzleVector = true;
   className = "WeaponImage";
   item = geolocatorItem;
   melee = false;
   armReady = true;
   minShotTime = 0;   //minimum time allowed between shots (needed to prevent equip/dequip exploit)
   undroppable = 1;

   doColorShift = false;
   colorShiftColor = geolocatorItem.colorShiftColor;

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

function geolocatorimage::onmount(%this, %obj, %slot)
{
	%obj.geolocatortool();
}

function geolocatorimage::onunmount(%this, %obj, %slot)
{
    %obj.foundore = "";
    %obj.client.centerprint("",0);
	cancel(%obj.usinggeolocator);
}

function geolocatorimage::onfire(%this, %obj, %slot)
{
    if(%obj.geolocatorcooldown + 0.5 > $sim::time)
        return;
    %obj.geolocatorcooldown = $sim::time;
    if(%obj.client.geolocatorfind + 10 > $sim::time)
        return;
    %obj.client.geolocatorfind = $sim::time;
    %obj.geolocatorradius(96);
}

function player::geolocatortool(%player)
{
    %cooldown = "";
    if(%player.client.geolocator !$= "" && %player.foundore !$= "none")
	{
		%orefound = "\c3Click to scan ores nearby.";
        if(%player.client.geolocatorfind + 10 >= $sim::time)
            %cooldown = "\c0Cooldown:" SPC mfloatlength(%player.client.geolocatorfind + 10 - $sim::time,1) @ "s";
    }
    else
    {
        %orefound = "\c0Not looking for any ores!" NL "\c0/setOre [ore name] to start looking for them!";
    }
    if(%player.foundore !$= "none" && %player.foundore !$= "")
    {
        %ore = $ore[%player.foundore.oreid];
        %color = convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield(%ore,5)),0) * 255));
        %color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield(%ore,5)),1) * 255));
        %color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield(%ore,5)),2) * 255));
        %color = "<color:" @ %color @ ">";
        %orefound = %color @ getfield(%ore,0) @ "\c6 -" SPC vectordist(vectoradd(%player.foundore.getposition(),"0 0 0.5"), %player.getmuzzlepoint(0)) SPC "\c6studs away";
    }
    else if(%player.foundore $= "none")
    {
        %orefound = "\c0No ores nearby found.";
        %cooldown = "\c0Cooldown:" SPC mfloatlength(%player.client.geolocatorfind + 10 - $sim::time,1) @ "s";
        if(%player.client.geolocatorfind + 10 < $sim::time)
            %player.foundore = "";
    }
    %player.client.centerprint("<just:right><color:ffffff>Locating ores:" NL %orefound NL %cooldown,0.5);
    %player.usinggeolocator = %player.schedule(100, geolocatortool);
}

function player::geolocatorradius(%player, %radius)
{
    initContainerRadiusSearch(%player.getposition(), %radius, $typemasks::fxbrickobjecttype);
    while(isobject(%search = containersearchnext()))
    {
        if(!%search.brickdestroyed && %search.canmine && %player.client.geolocator $= getfield($ore[%search.oreid],0))
        {
            %foundore = %search;
            break;
        }
    }
    if(%foundore)
    {
        %player.foundore = %foundore;
        %player.client.playsound(beep_checkout_sound);
    }
    else
    {
        %player.foundore = "none";
        %player.client.playsound(errorsound);
    }
}

function servercmdsetore(%client, %ore, %ore2, %ore3)
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
	%client.geolocator = %name;
}

exec("./mk2/server.cs");