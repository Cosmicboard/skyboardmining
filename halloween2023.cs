function gameconnection::startEventGamemode(%client)
{
    %client.player.mininghelmet.light.delete();
    %client.player.mininghelmet.delete();
    %client.player.setdatablock(disabledplayer);
    %client.player.hasvalvea = 0;
    %client.player.hasvalveb = 0;
    %client.player.hasvalvec = 0;
    %client.player.haskeya = 0;
    %client.player.placedvalvea = 0;
    %client.player.placedvalveb = 0;
    %client.player.placedvalvec = 0;
    %client.player.placedkeya = 0;
    %client.player.settransform(_thetp.position);
    %client.player.ineventgamemode = 1;
    %bg = "brickgroup_182892";
    for(%i = 0; %i < %bg.getcount(); %i++)
    {
        %brick = %bg.getobject(%i);
        if(%brick.getname() $= "_valveDoorA" || %brick.getname() $= "_valveDoorB" || %brick.getname() $= "_valveDoorC")
        {
            %brick.setnetflag(6,1);
            %brick.clearscopetoclient(%client);
            %client.nocollision[%brick.getname()] = 1;
        }
        if(%brick.getname() $= "_valveA" || %brick.getname() $= "_valveB" || %brick.getname() $= "_valveC" || %brick.getname() $= "_gateA" || %brick.getname() $= "_gateB" || %brick.getname() $= "_doorA" || %brick.getname() $= "_keyA" || %brick.getname() $= "_openDoor" || %brick.getname() $= "_valveDoorAplace" || %brick.getname() $= "_valveDoorBplace" || %brick.getname() $= "_valveDoorCplace")
        {
            %brick.setnetflag(6,0);
            %brick.scopetoclient(%client);
            %client.nocollision[%brick.getname()] = 0;
        }
    }
}

registeroutputevent("gameconnection", "starteventgamemode");

package eventgamemode
{
    function Player::Activatestuff(%obj)
	{
        Parent::Activatestuff(%obj);
        %ray = containerRaycast(%obj.getEyePoint(),vectorAdd(%obj.getEyePoint(),vectorScale(%obj.getEyeVector(),4)),$Typemasks::fxBrickObjectType | $TypeMasks::ItemObjectType,%obj);
        if(firstword(%ray))
        {
            %bg = "brickgroup_182892";
            if(firstword(%ray).getname() $= "_planksdestroyer" && !%obj.planksdestroyed)
            {
                for(%i = 0; %i < %bg.getcount(); %i++)
                {
                    %brick = %bg.getobject(%i);
                    if(%brick.getname() $= "_planks1")
                    {
                        %brick.setnetflag(6,1);
                        %brick.clearscopetoclient(%obj.client);
                        %obj.client.nocollision[%brick.getname()] = 1;
                    }
                }
                %obj.planksdestroyed = 1;
                %obj.client.playsound(brickchangesound);
            }
            if(%obj.ineventgamemode)
            {
                if(firstword(%ray).getname() $= "_valveA" && !%obj.hasValveA)
                {
                    for(%i = 0; %i < %bg.getcount(); %i++)
                    {
                        %brick = %bg.getobject(%i);
                        if(%brick.getname() $= "_valveA")
                        {
                            %brick.setnetflag(6,1);
                            %brick.clearscopetoclient(%obj.client);
                            %obj.client.nocollision[%brick.getname()] = 1;
                        }
                    }
                    %obj.hasValveA = 1;
                    %obj.client.playsound(brickchangesound);
                }
                else if(firstword(%ray).getname() $= "_valveB" && !%obj.hasValveB)
                {
                    for(%i = 0; %i < %bg.getcount(); %i++)
                    {
                        %brick = %bg.getobject(%i);
                        if(%brick.getname() $= "_valveB")
                        {
                            %brick.setnetflag(6,1);
                            %brick.clearscopetoclient(%obj.client);
                            %obj.client.nocollision[%brick.getname()] = 1;
                        }
                    }
                    %obj.hasValveB = 1;
                    %obj.client.playsound(brickchangesound);
                }
                else if(firstword(%ray).getname() $= "_valveC" && !%obj.hasValveC)
                {
                    for(%i = 0; %i < %bg.getcount(); %i++)
                    {
                        %brick = %bg.getobject(%i);
                        if(%brick.getname() $= "_valveC")
                        {
                            %brick.setnetflag(6,1);
                            %brick.clearscopetoclient(%obj.client);
                            %obj.client.nocollision[%brick.getname()] = 1;
                        }
                    }
                    %obj.hasValveC = 1;
                    %obj.client.playsound(brickchangesound);
                }
                if(firstword(%ray).getname() $= "_valveDoorAPlace" && !%obj.placedValveA && %obj.hasValveA)
                {
                    for(%i = 0; %i < %bg.getcount(); %i++)
                    {
                        %brick = %bg.getobject(%i);
                        if(%brick.getname() $= "_valveDoorA")
                        {
                            %brick.setnetflag(6,1);
                            %brick.scopetoclient(%obj.client);
                            %obj.client.nocollision[%brick.getname()] = 0;
                        }
                    }
                    for(%i = 0; %i < %bg.getcount(); %i++)
                    {
                        %brick = %bg.getobject(%i);
                        if(%brick.getname() $= "_gateA")
                        {
                            %brick.setnetflag(6,1);
                            %brick.clearscopetoclient(%obj.client);
                            %obj.client.nocollision[%brick.getname()] = 1;
                        }
                    }
                    _valveDoorAPlace.setnetflag(6,1);
                    _valveDoorAPlace.clearscopetoclient(%obj.client);
                    %obj.client.nocollision["_valvedooraplace"] = 1;
                    %obj.client.playsound(brickrotatesound);
                    %obj.placedvalvea = 1;
                }
                if(firstword(%ray).getname() $= "_valveDoorBPlace" && !%obj.placedValveB && %obj.hasValveB)
                {
                    for(%i = 0; %i < %bg.getcount(); %i++)
                    {
                        %brick = %bg.getobject(%i);
                        if(%brick.getname() $= "_valveDoorB")
                        {
                            %brick.setnetflag(6,1);
                            %brick.scopetoclient(%obj.client);
                            %obj.client.nocollision[%brick.getname()] = 0;
                        }
                    }
                    for(%i = 0; %i < %bg.getcount(); %i++)
                    {
                        %brick = %bg.getobject(%i);
                        if(%brick.getname() $= "_gateB")
                        {
                            %brick.setnetflag(6,1);
                            %brick.clearscopetoclient(%obj.client);
                            %obj.client.nocollision[%brick.getname()] = 1;
                        }
                    }
                    _valveDoorbPlace.setnetflag(6,1);
                    _valveDoorbPlace.clearscopetoclient(%obj.client);
                    %obj.client.nocollision["_valvedoorbplace"] = 1;
                    %obj.client.playsound(brickrotatesound);
                    %obj.placedvalvea = 1;
                }
                if(firstword(%ray).getname() $= "_valveDoorCPlace" && !%obj.placedValveC && %obj.hasValveC)
                {
                    for(%i = 0; %i < %bg.getcount(); %i++)
                    {
                        %brick = %bg.getobject(%i);
                        if(%brick.getname() $= "_valveDoorC")
                        {
                            %brick.setnetflag(6,1);
                            %brick.scopetoclient(%obj.client);
                            %obj.client.nocollision[%brick.getname()] = 0;
                        }
                    }
                    for(%i = 0; %i < %bg.getcount(); %i++)
                    {
                        %brick = %bg.getobject(%i);
                        if(%brick.getname() $= "_openDoor")
                        {
                            %brick.setnetflag(6,1);
                            %brick.clearscopetoclient(%obj.client);
                            %obj.client.nocollision[%brick.getname()] = 1;
                        }
                    }
                    _valveDoorcPlace.setnetflag(6,1);
                    _valveDoorcPlace.clearscopetoclient(%obj.client);
                    %obj.client.nocollision["_valvedoorcplace"] = 1;
                    %obj.client.playsound(brickrotatesound);
                    %obj.placedvalvea = 1;
                }
                if(firstword(%ray).getname() $= "_keyA" && !%obj.haskeyA)
                {
                    for(%i = 0; %i < %bg.getcount(); %i++)
                    {
                        %brick = %bg.getobject(%i);
                        if(%brick.getname() $= "_keyA")
                        {
                            %brick.setnetflag(6,1);
                            %brick.clearscopetoclient(%obj.client);
                            %obj.client.nocollision[%brick.getname()] = 1;
                        }
                    }
                    %obj.client.nocollision["_valvedoorbplace"] = 1;
                    %obj.client.playsound(brickchangesound);
                    %obj.haskeya = 1;
                }
                if(firstword(%ray).getname() $= "_doorA" && !%obj.placedkeyA && %obj.hasKeyA)
                {
                    _doora.setnetflag(6,1);
                    _doora.clearscopetoclient(%obj.client);
                    %obj.client.nocollision["_doora"] = 1;
                    %obj.client.playsound(brickrotatesound);
                    %obj.placedkeya = 1;
                }
            }
        }
    }
};
activatepackage(eventgamemode);

function gameconnection::wineventgamemode(%client)
{
    if(!%client.craftedcosmetic["Lantern"])
    {
        %client.chatmessage("<color:ffffff>You have unlocked the <color:ff4500>Lantern<color:ffffff> skin! (that's a torch skin btw)");
        %client.playsound(beep_ekg_sound);
    }
    %client.craftedcosmetic["Lantern"] = 1;
    %client.player.schedule(3000, instantrespawn);
    %client.player.ineventgamemode = 0;
    %client.updatecosmetictools();
}

registeroutputevent("gameconnection", "wineventgamemode");