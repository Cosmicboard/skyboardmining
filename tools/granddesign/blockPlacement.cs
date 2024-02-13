function player::startBlockPlacement(%player)
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
        %testRay = containerraycast(vectoradd(getwords(%ray,1,3), "0 0 0.05"), vectoradd(getwords(%ray,1,3), "0 0 -0.2"), $typemasks::fxbrickobjecttype, %obj);
        if(getsubstr(getword(%ray,3), strlen(getword(%ray,3))-1, 1) == 6 && !firstword(%testRay))
            %zoffset=0.15;
        %vec = vectorscale(%player.geteyevector(),-1);
        %offset = vectorscale(%vec, 0.01);
        %xpos = mfloatlength(getword(%ray,1)+getword(%offset,0),0);
        %ypos = mfloatlength(getword(%ray,2)+getword(%offset,1),0);
        %zpos = mfloatlength(getword(%ray,3)-%zoffset,0);
        %realPos = %xpos SPC %ypos SPC %zpos+0.1;
        %player.blockPosition = %realPos;
        %obj2 = new staticShape()
        {
            datablock = cubebox;
            scale = "0.5 0.5 0.5";
            position = %realpos;
        };
        %obj2.setnetflag(6, 1);
        %obj2.scopetoclient(%player.client);
        %obj2.setNodeColor("ALL", "0 1 0 1");
        %obj2.schedule(100, delete);
        %player.blocked = 0;
        %player.placementposition = %obj2.getposition();
        initcontainerboxsearch(%realpos, "1 1 1", $typemasks::fxbrickalwaysobjecttype);
        while(%search = containersearchnext())
        {
            if(vectordist(%search.position, %realpos) < 1)
            {
                %player.blocked = 1;
                %obj.setNodeColor("ALL", "1 0 0 1");
                %obj2.setNodeColor("ALL", "1 0 0 1");
                break;
            }
        }
        if(getword(%realpos,0) >= 0 && getword(%realpos,0) <= 19 && getword(%realpos,1) >= 0 && getword(%realpos,1) <= 19 && getword(%realpos,2) $= "5001.1" || !%ray.canmine)
        {
            %player.blocked = 1;
            %obj.setNodeColor("ALL", "1 0 0 1");
            %obj2.setNodeColor("ALL", "1 0 0 1");
        }

    }
    if(isobject(%player))
        %player.blockPlacement = %player.schedule(100, startblockplacement);
    %player.recentlychanged = 0;
}

function placeBlock(%Obj)
{
    if(%obj.blocked)
    {
        %obj.client.chatmessage("\c0cannot place");
        %obj.client.playsound(errorsound);
        return;
    }
    if(%obj.placementoption == 0)
    {
        %brick = addbrick(%obj.blockposition, oreidfromname("Dirt"), ignore);
        %obj.client.inventory["Dirt"]--;
    }
    else if(%obj.placementoption == 1)
    {
        %brick = addbrick(%obj.blockposition, oreidfromname("Steel"), ignore);
        %obj.client.inventory["Steel"]--;
    }
    %brick.placedBrick = 1;
    %brick.placedBy = %obj.client.name;
}