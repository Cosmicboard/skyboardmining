$totalDrillCount = 0;
function assignDrill(%name, %resources)
{
    $drill[$totaldrillcount] = %name TAB %resources;
    $totaldrillcount++;
}
assignDrill("Stationary Drill", "12 Steel\n6 Bronze\n5 Electrum\n2 Quartz");
//assignDrill("Stationary Drill", "12 Steel\n6 Bronze\n5 Electrum\n2 Quartz");

function gameconnection::reciteCrafts(%client, %drillName)
{
    for(%i = 0; %i < $totalDrillCount; %i++)
    {
        %drill = $drill[%i];
        if(getfield(%drill,0) $= %drillname)
            break;
    }
    %mats = getfields(%drill,1,getfieldcount(%drill)-1);
    %matscount = getfieldcount(%drill)-1;
    for(%i = 0; %i < %matscount; %i++)
    {
        if(%client.inventory[restwords(getfield(%mats,%i))] >= firstword(getfield(%drill,%i+1)))
        {
            %materials = %materials SPC "\c2" @ mfloor(%client.inventory[restwords(getfield(%mats,%i))]) @ "/" @ firstword(getfield(%drill,%i+1)) SPC restwords(getfield(%mats,%i));
        }
        else
        {
            %materials = %materials SPC "\c0" @ mfloor(%client.inventory[restwords(getfield(%mats,%i))]) @ "/" @ firstword(getfield(%drill,%i+1)) SPC restwords(getfield(%mats,%i));
        }
    }
    return %materials;
}

function player::startSchematicPlacement(%player)
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
        %player.schematicPosition = vectoradd(getwords(%ray,1,3), "0 0 1");
        %obj2 = new staticShape()
        {
            datablock = cubebox;
            scale = "1 1 1";
            position = %player.schematicPosition;
        };
        %obj2.setnetflag(6, 1);
        %obj2.scopetoclient(%player.client);
        %obj2.setNodeColor("ALL", "0 1 0 1");
        %obj2.schedule(100, delete);
        %player.blocked = 0;
        %player.placementposition = %obj2.getposition();
        initcontainerboxsearch(%player.schematicPosition, "1 1 1", $typemasks::fxbrickalwaysobjecttype);
        while(%search = containersearchnext())
        {
            if(vectordist(%search.position, %player.schematicPosition) < 0.5)
            {
                %player.blocked = 1;
                %obj.setNodeColor("ALL", "1 0 0 1");
                %obj2.setNodeColor("ALL", "1 0 0 1");
                break;
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
        %player.schematicPlacement = %player.schedule(100, startschematicplacement);
    %player.recentlychanged = 0;
}

function placeDrill(%obj, %type)
{
    %client = %obj.client;
    %selected = %obj.schematicoption;
    %item = $drill[%selected];
    %mats = getfields(%item,1,getfieldcount(%item)-1);
    %matscount = getfieldcount(%item)-1;
    %materialcount = 0;
    for(%i = 0; %i < %matscount; %i++)
    {
        if(%client.inventory[restwords(getfield(%mats,%i))] >= firstword(getfield(%item,%i+1)))
        {
            %material[%i] = restwords(getfield(%mats,%i));
			%materialamount[%i] = firstword(getfield(%mats,%i));
        }
        else
        {
            %fail = true;
            %nomaterial[%materialcount] = firstword(getfield(%item,%i+1)) @ "x" SPC restwords(getfield(%mats,%i));
            %materialcount++;
        }
    }
    if(!%fail)
    {
		for(%i = 0; %i < %matscount; %i++)
		{
			%client.inventory[%material[%i]]-=%materialamount[%i];
		}
    }
    else if(%fail)
    {
        %client.chatmessage("you do not have the following items to construct this");
        %client.playsound(errorsound);
        for(%i = 0; %i < %materialcount; %i++)
        {
			%has = %client.inventory[restwords(%nomaterial[%i])];
            %client.chatmessage("Requires:" SPC %nomaterial[%i] SPC "(you have:" SPC mfloor(%has) @ "x)");
        }
        return;
    }
    if(%type == 0)
    {
        %shape = new staticShape()
        {
            datablock = cubebox;
            scale = "1 1 1";
            position = %obj.schematicposition;
            owner = %obj.client;
            drill = 1;
            maxfuel = 250;
            maxdepth = 100;
            name = "Stationary Drill";
            damage = 1000;
            size = "2 2";
            speed = 750;
        };
        %shape.setnodecolor("ALL", "0.7 0.7 0.7 1");
    }
}

function staticshape::addFuel(%this, %amount)
{
    %this.fuel += %amount;
    if(%this.fuel > %this.maxfuel)
        %this.fuel = %this.maxfuel;
}

function staticshape::startDrill(%this, %candigdown)
{
    if(%this.fuel > 0 && mfloatlength(getword(%this.position,2),0)-mfloatlength(getword(%this.drillposition,2),0) < %this.maxdepth)
    {
        if(%candigdown)
        %this.drillposition = getwords(%this.drillposition,0,1) SPC getword(%this.drillposition,2)-1;
        %candigdown = 1;
        initcontainerboxsearch(%this.drillposition, %this.size SPC "1", $typemasks::fxbrickobjecttype);
        while(%search = containersearchnext())
        {
            if(%search.canmine)
            {
                if(getword(%search.position,2) != getword(%this.drillposition,2))
                    continue;
                %candigdown = 0;
                %found = 1;
                %search.health -= %this.damage;
                if(%search.health <= 0)
                    destroybrick(%search);
            }
        }
        if(%found)
            {%this.fuel--;serverplay3d(tink_ @ getrandom(0,2), %this.drillposition);}
    }
    %this.drill = %this.schedule(%this.speed, startdrill, %candigdown);
}

function player::selectDrill(%player)
{
    %shape = %player.chosendrill;
    %client = %player.client;
    if(%shape.owner == %player.client && !%client.showInstructions)
    {
        %client.chatmessage("\c6instructions on how to use the driller");
        %client.chatmessage("\c0right click \c6on your drill to open the fuel chamber menu");
        %client.chatmessage("\c2click\c6 on your drill to choose where to drill");
        %client.chatmessage("\c5this will appear only once during the session so you better not forget");
        %client.playsound(beep_key_sound);
        %client.showInstructions=1;
    }
    if(!iseventpending(%player.chosendrill.drill))
        %drillPosition = "NONE";
    else
        %drillPosition = mfloatlength(getword(%shape.position,2),0)-mfloatlength(getword(%shape.drillposition,2),0) SPC "/" SPC %shape.maxdepth;
    if(%player.selectingDrill)
        %drillPosition = "SELECTING";
    if(!%player.client.usingdrillfuelchamber)
    {
        if(%shape.owner == %client)
            %client.centerprint("<just:right>\c3" SPC %shape.owner.name @ "'s\c6" SPC %shape.name NL "\c6Depth:\c4" SPC %drillPosition NL "\c6Fuel:\c0" SPC mfloor(%shape.fuel) @ " / " @ %shape.maxfuel NL "<font:verdana:15>\c0Damage:" SPC %shape.damage SPC "\c2Speed:" SPC %shape.speed @ "ms" SPC "\c3Size:" SPC firstword(%shape.size)+1 @ "x" @ restwords(%shape.size)+1 NL "\c7click on an empty space to close");
        else
            %client.centerprint("<just:right>\c3" SPC %shape.owner.name @ "'s\c6" SPC %shape.name NL "\c6Depth:\c4" SPC %drillPosition NL "\c6Fuel:\c0" SPC mfloor(%shape.fuel) @ " / " @ %shape.maxfuel NL "<font:verdana:15>\c0Damage:" SPC %shape.damage SPC "\c2Speed:" SPC %shape.speed @ "ms" SPC "\c3Size:" SPC firstword(%shape.size)+1 @ "x" @ restwords(%shape.size)+1 NL "\c7click again to close");
    }
    %player.drillHud = %player.schedule(100, selectdrill, %shape);
}

function player::selectDrillingPosition(%player)
{
    cancel(%player.chosendrill.drill);
    %ray = containerraycast(%player.geteyepoint(), vectoradd(%player.geteyepoint(), vectorscale(%player.geteyevector(), 5)), $typemasks::fxbrickobjecttype, %player);
    if(firstword(%ray))
    {
        if(%ray.canmine)
        {
            if(getword(%ray,3) <= -0.9)
                %zpos = mfloatlength(getword(%ray,3),0) @ ".9";
            else
                %zpos = mfloatlength(getword(%ray,3),0) @ ".1";
            %position = getwords(%ray,1,2) SPC %zpos;
        }
    }
    if(%position)
    {
        initcontainerboxsearch(%position, "2 2 1", $typemasks::fxbrickobjecttype);
        while(isobject(%search=containersearchnext()))
        {
            if(%search.canmine)
            {
                if(getword(%search.position,2) != getword(%ray.position,2))
                    continue;
                if(!%search.originalcolor)
                    %search.originalcolor = %search.colorid;
                cancel(%search.returncolor);
                if(vectordist(getwords(%position,0,1), getwords(%player.chosendrill.position,0,1)) < 8)
                    {%player.disableDrill=0;%search.setcolor(44);}
                else
                    {%player.disableDrill=1;%search.setcolor(38);}
                %search.returncolor = %search.schedule(100, setcolor, %search.originalcolor);
            }
        }
    }
    %player.client.chosendrillposition = %position;
    %player.selectingPosition = %player.schedule(50, selectDrillingPosition);
}