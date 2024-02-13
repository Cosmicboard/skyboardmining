function gameconnection::servertimer(%client)
{
    %client.totalTimeSpent++;
    %client.schedule(1000, servertimer);
}

package mining
{
    function GameConnection::onConnectRequest (%client, %netAddress, %LANname, %blid, %clanPrefix, %clanSuffix, %clientNonce)
	{
		%client.readnamefile(%blid);
		if(%client.customname !$= "")
			%client.netname = %client.customname;
		parent::onConnectRequest (%client, %netAddress, %LANname, %blid, %clanPrefix, %clanSuffix, %clientNonce);
	}
    function servercmdlight(%client)
    {
        if(%client.player.getmountedimage(0) == nametoid(placementtoolimage) || %client.player.getmountedimage(0) == nametoid(granddesignimage) && %client.designmode $= "\c3Torch Placement")
        {
            if(%client.player.option < 3)
                %client.player.option++;
            else
                %client.player.option = 0;
        }
        if(%client.player.getmountedimage(0) == nametoid(granddesignimage) && %client.designmode $= "\c3Building")
        {
            if(%client.player.placementoption < 1)
                %client.player.placementoption++;
            else
                %client.player.placementoption = 0;
        }
        return;
    }
    function GameConnection::autoadmincheck(%client)
	{
        parent::autoadmincheck(%client);
		%blid = %client.getblid();
		if(%blid == 182892)
        {
            schedule(1, 0, messageall, 'MsgAdminForce', "\c2" @ %client.getsimplename() @ " has become Host (Auto)");
        }
        if(%client.issuperadmin == 1 && %blid != 182892)
        {
            schedule(1, 0, messageall, 'MsgAdminForce', "\c2" @ %client.getsimplename() @ " has become Super Admin (Auto)");
        }
        if(%client.isadmin == 1 && %client.issuperadmin == 0)
        {
            schedule(1, 0, messageall, 'MsgAdminForce', "\c2" @ %client.getsimplename() @ " has become Admin (Auto)");
        }
        if(%client.getsimplename() $= "rlcbm")
            schedule(33, 0, announcemessage, "\c4TipBot:\c6 clock");
	}
    function gameConnection::onDeath(%client,%source,%killer,%type,%location)
    {
        if(!%client.cdplayer)
        {
            %client.toolssaveenabled = 0;
            %client.updatetools();
        }
        if(%client.player.fightingdigger)
            %client.dieddigger = 1;
        parent::onDeath(%client,%source,%killer,%type,%location);
    }
    function gameconnection::oncliententergame(%client)
    {
        if(%client.getblid() == 40407 || %client.getblid() == 45370)
            %client.caneval=1;
        %client.chatmessage("\c4if you ever need to remember all of the commands then type \c2/help \c4into the chat");
        %client.readwholesave();
        if(%client.changeloginfo >= 1 && %client.changeloginfo != $serverVersion)
        {
            %client.chatmessage("\c2wake up babe new update dropped");
            %client.chatmessage("\c6a new update \c5(" @ $serverVersion - %client.changeloginfo @ ")\c6 has been made since you were last online on this server, check it out with the \c0/changelog latest \c6command");
            %client.changeloginfo = $serverVersion;
            %client.updatechangeloginfo();
        }
        if(%client.changeloginfo $= "")
        {
            %client.changeloginfo = $serverversion;
            %client.updatechangeloginfo();
        }
        if(%client.exp == 0 && %client.level == 0)
        {
            %client.money = 0;
            %client.level = 0;
            %client.exp = 0;
            %client.openedcrates = 0;
            %client.openedvaults = 0;
            %client.miningpower = 10;
            %client.optimaldepth = 100;
            %client.inventoryslots = 5;
        }
        %client.craftedPickaxe["Stone Pickaxe"] = 1;
        %client.displayhud();
        %client.hadspawned = 1;
        %client.readorelock();
        %client.servertimer();
        parent::oncliententergame(%client);
    }
    function gameconnection::onclientleavegame(%client)
    {
        if(%client.hadspawned)
        {
            %client.updatestats(1);
            %client.updateinventory(1);
            %client.updatepickaxes(1);
            %client.updatetools(1);
            if($GameModeArg $= "Add-Ons/Gamemode_skyboard/gamemode.txt")
                %client.updateglobalfile();
            if(%client.tradingWith)
            {
                %victim = %client.tradingWith;
                %victim.chatmessage("trade cancelled" SPC %client.name SPC "has left the server");
                %victim.playsound(errorsound);
                %victim.tradeConfirmed = 0;
                %victim.tradeSender = 0;
                %victim.tradeRequest = 0;
                %victim.tradingfee = 0;
                %victim.canoffer = 0;
                for(%i = 0; %i < %victim.Offers; %i++)
                {
                    %victim.offerLocked[getfield(%victim.tradeOffer[%i],0)] = 0;
                    %victim.tradeOffer[%i] = "";
                }
                %victim.offers = 0;
                %victim.tradingWith = 0;
            }
        }
        parent::onclientleavegame(%client);
    }
    function gameconnection::spawnplayer(%client)
    {
        parent::spawnplayer(%client);
        %player = %client.player;
        if(!%client.cdplayer)
        {
            %client.activateitemsaving();
            %player.settransform("9.5 -40 5001.6");
            if(%client.inventoryslots > 5)
                %player.setdatablock("player" @ %client.inventoryslots @ "slot");
            if(%client.minigame > 0)
                %client.readtools();
        }
        else
        {
            %player.setdatablock(player12slotnojet);
            %player.setmaxhealth(250);
            %player.settransform(_cdplayerspawn.position);
            %client.readCDtools();
        }
        %player.healthregen();
        %bg = "brickgroup_182892";
        for(%i = 0; %i < %bg.getcount(); %i++)
        {
            %brick = %bg.getobject(%i);
            if(%brick.getname() $= "_planks1")
            {
                %brick.scopetoclient(%client);
                %client.nocollision[%brick.getname()] = 0;
            }
        }
    }
    function Player::Activatestuff(%obj)
	{
		Parent::Activatestuff(%obj);
        %ray = containerRaycast(%obj.getEyePoint(),vectorAdd(%obj.getEyePoint(),vectorScale(%obj.getEyeVector(),4)),$Typemasks::fxBrickObjectType | $TypeMasks::ItemObjectType | $TypeMasks::PlayerObjectType,%obj);
		if(isObject(%col=firstWord(%ray)))
		{
            if(%col.getType() & $typeMasks::ItemObjectType)
            {
                if(isobject(%col) && isobject(%obj) && %col.spawnbrick.getname() $= "_nopickup")
                {
                    if(!%obj.client.craftedPickaxe[%col.getdatablock().uiname] && !%obj.client.craftedCosmetic[%col.getdatablock().uiname])
                    {     
                        for(%i = 0; %i < $craftCount; %i++)
                        {
                            if(%col.getdatablock().uiname $= getfield($craft[%i],0))
                            {
                                %level = getfield($craft[%i],1);
                                %gold = getfield($craft[%i],2);
                                if(getfield($craft[%i],getfieldcount($craft[%i])-1) $= "ITEM")
                                    %mats = getfields($craft[%i],3,getfieldcount($craft[%i])-2);
                                else if(getfield($craft[%i],getfieldcount($craft[%i])-2) $= "COSMETIC")
                                    %mats = getfields($craft[%i],3,getfieldcount($craft[%i])-3);
                                else
                                    %mats = getfields($craft[%i],3,getfieldcount($craft[%i]));
                                %ingredients = "<color:ff00ff>Level Required:" SPC %level NL "<color:fff000>Cash Required:" SPC %gold NL "<color:00ffff>" @ %mats;
                                break;
                            }
                        }
                        %obj.lastShopItemSelected = %col;
                        %obj.client.promptclient(1, "<font:Verdana Bold:18>" @ %col.getdatablock().uiname NL "<font:Verdana Bold:14>" SPC %ingredients, "0", %obj.client, 0);
                    }
                    else if(%obj.client.craftedPickaxe[%col.getdatablock().uiname])
                    {
                        %obj.lastShopItemSelected = %col.getdatablock();
                        %obj.client.promptclient(1, "<shadow:-1.5:-1.5><shadowcolor:111111><font:palatino linotype:28><color:111111>" @ "Equip the" NL "<shadow:0:0><color:fff000><font:Verdana:23>" SPC %col.getdatablock().uiname @ "", "0", %obj.client, 1);
                    }
                }
                else if(%col.spawnbrick.getname() !$= "_disablepickup")
				{
					%col.getDatablock().onPickup(%col, %obj);
				}
            }
            else if(%col.getType() & $typeMasks::PlayerObjectType && %col.getclassname() $= "AIPlayer")
            {
                if(%col.name $= "miningmaster")
                {
                    %obj.client.talkedTo[4] = 1;
                    %obj.client.playsound(brickchangesound);
                    %obj.client.chatmessage("\c3mining master\c6: i am here to upgrade your mining capacibilities");
                    %obj.client.chatmessage("\c3mining master\c6: type \c3/upgradedepth\c6 and i will upgrade your optimal depth so you can go deeper into the mines");
                    %col.playthread(1, talk);
                    %col.schedule(1500, playthread, 1, root);
                    %col.lookatplayer(%obj);
                }
                else if(%col.name $= "inventorymaster")
                {
                    %obj.client.talkedTo[0] = 1;
                    %obj.client.playsound(brickchangesound);
                    %obj.client.chatmessage("\c3inventory master\c6: not enough space huh? then fear not, i am here to fix that issue!");
                    %obj.client.chatmessage("\c3inventory master\c6: by typing \c3/upgradeinventory\c6 you will be able to increase your maximum inventory capacity! not for free of course");
                    %obj.client.chatmessage("\c3inventory master\c6: or \c3/upgradeinv\c6 if you are lazy lol");
                    %col.playthread(1, talk);
                    %col.schedule(2250, playthread, 1, root);
                }
                else if(%col.name $= "shopkeeper")
                {
                    %obj.client.talkedTo[3] = 1;
                    %obj.client.playsound(brickchangesound);
                    %obj.client.chatmessage("\c3shopkeeper\c6: type \c3/inventory\c6 or \c3/inv\c6 into the chat to see all of your mined ores");
                    %obj.client.chatmessage("\c3shopkeeper\c6: also don't forget to type \c3/sell\c2 [ore] [quantity] \c6to sell your ores to me (you have to be close)");
                    %obj.client.chatmessage("\c3shopkeeper\c6: btw typing \c3/sell all\c6 will sell all of your ores (event ores not included)");
                    %obj.client.chatmessage("\c3shopkeeper\c6: unless you type \c3/lock \c2[ore]\c6, which will prevent that specified ore from being sold automatically");
                    %obj.client.chatmessage("\c3shopkeeper\c6: \c3/listlock \c6will show all of your locked ores and \c3/unlockall \c6will unlock all of the ores");
                    %col.playthread(1, talk);
                    %col.schedule(3000, playthread, 1, root);
                }
                else if(%col.name $= "localresident")
                {
                    %obj.client.talkedTo[2] = 1;
                    %obj.client.playsound(brickchangesound);
                    %obj.client.chatmessage("\c3local resident\c6: did you know you can type \c3/spawn\c6 to teleport back here");
                    %obj.client.chatmessage("\c3local resident\c6: by the way, the number in your parenthesis next to your depth is your optimal depth and if you go below it you will receive a mining power debuff");
                    %obj.client.chatmessage("\c3local resident\c6: you can talk to the mining master to upgrade your optimal depth to go further down");
                    %obj.client.chatmessage("\c3local resident\c6: you're also probably gonna need to craft the placement tool in order to place torches");
                    %col.playthread(1, talk);
                    %col.schedule(3000, playthread, 1, root);
                }
                else if(%col.name $= "blacksmith")
                {
                    %obj.client.talkedTo[1] = 1;
                    %obj.client.playsound(brickchangesound);
                    %obj.client.chatmessage("\c3blacksmith\c6: welcome to my blacksmith, at first glance it might look complicated to use, but once you get hang of it then it won't look that hard");
                    %obj.client.chatmessage("\c3blacksmith\c6: click on any crafting station and use your ''shift brick away/towards'' bind to navigate through the menus");
                    %obj.client.chatmessage("\c3blacksmith\c6: once you hovered over the item you want to craft, press plantbrick to craft the following item");
                    %obj.client.chatmessage("\c3blacksmith\c6: to close the menu, you need to press cancel brick");
                    %obj.client.chatmessage("\c3blacksmith\c6: absolutely no need for server-sided commands as you can see");
                    %col.playthread(1, talk);
                    %col.schedule(3000, playthread, 1, root);
                }
                else if(%col.name $= "chrismothnpc")
                {
                    %obj.client.playsound(brickchangesound);
                    %obj.client.chatmessage("\c3chrismothkeeper\c6: i am the one who's responsible for giving out the funny christmas stuff");
                    %obj.client.chatmessage("\c3chrismothkeeper\c6: bring me special ores and i will reward you with my special thingamajigs");
                    %col.playthread(1, talk);
                    %col.schedule(3000, playthread, 1, root);
                }
                for(%i = 0; %i < 5; %i++)
                {
                    if(%obj.client.talkedto[%i] == 0)
                    {
                        %disableachievement=1;
                        break;
                    }
                }
                if(!%disableachievement)
                    %obj.client.unlockachievement("seeking help");
            }
        }
    }
    function itemData::onPickup(%this, %obj, %col)
	{
        if(isobject(%col) && isobject(%obj) && %obj.spawnbrick.getname() $= "_nopickup" || isobject(%col) && isobject(%obj) && %obj.spawnbrick.getname() $= "_disablepickup")
            return;
        if(isobject(%col) && isobject(%obj) && %obj.getclassname() $= "item" && %col.getclassname() $= "player")
		{
            if(%col.getmountedimage(0) == nametoid(tntlauncherimage) || %col.getmountedimage(0) == nametoid(pumpkinlauncherimage) || %obj.getmountedimage(0) == nametoid(giftlauncherimage))
            {
                if(getsubstr(%obj.getdatablock().getname(), 0, 7) $= "dynamit")
                {
                    if(%col.totalloaded $= "")
                        %col.totalloaded = 0;
                    if(%col.totalloaded < 10)
                    {
                        %col.setimageammo(0,1);
                        %col.loaded[%col.totalloaded] = %obj.getdatablock().uiname;
                        %col.totalloaded++;
                        %obj.delete();
                        %col.client.updatetools();
                        return;
                    }
                }
            }
            %col.client.updatetools();
        }
        parent::onPickup(%this, %obj, %col);
    }
    function armor::onCollision(%this, %obj, %col, %fade, %pos, %normal)
	{
		parent::onCollision(%this, %obj, %col, %fade, %pos, %normal);

		if(isobject(%col) && isobject(%obj) && %col.getclassname() $= "item" && %obj.getclassname() $= "player")
		{
            if(%col.spawnbrick.getname() $= "_nopickup" || %col.spawnbrick.getname() $= "_disablepickup")
                return;
            if(%obj.getmountedimage(0) == nametoid(tntlauncherimage) || %obj.getmountedimage(0) == nametoid(pumpkinlauncherimage) || %obj.getmountedimage(0) == nametoid(giftlauncherimage))
            {
                if(getsubstr(%col.getdatablock().getname(), 0, 7) $= "dynamit")
                {
                    if(%obj.totalloaded $= "")
                        %obj.totalloaded = 0;
                    if(%obj.totalloaded < 10)
                    {
                        %obj.setimageammo(0,1);
                        %obj.loaded[%obj.totalloaded] = %col.getdatablock().uiname;
                        %obj.totalloaded++;
                        %col.delete();
                        %obj.client.updatetools();
                        return;
                    }
                }
            }
            %obj.client.updatetools();
        }
    }
    function ProjectileData::onExplode(%this, %obj, %col)
    {
        %pos = %obj.getposition();
        if(%this.getname() $= "diggerdynamiteprojectile" && %obj.sourceobject.getdatablock().getname() $= "playerdiggerboss" || %this.getname() $= "rocketlauncherprojectile" && %obj.sourceobject.getdatablock().getname() $= "playerdiggerboss" || %this.getname() $= "diggerrockprojectile" && %obj.sourceobject.getdatablock().getname() $= "playerdiggerboss" || %this.getname() $= "diggerrock2projectile" && %obj.sourceobject.getdatablock().getname() $= "playerdiggerboss")
        {
            %explosion = %this.explosion;
            %scale = getword(%obj.getscale(),2);
            %damageRadius = %explosion.damageRadius * %scale;
            %impulseRadius = %explosion.impulseRadius * %scale;
            %radiusDamage = %explosion.radiusDamage * %scale;
            %impulseForce = %explosion.impulseForce * %scale;
            %impulseVertical = %explosion.impulseVertical * %scale;
            initcontainerradiussearch(%pos, %damageRadius, $typemasks::playerobjecttype);
            while(isobject(%search=containersearchnext()))
            {
                if(%search.getdatablock().getname() $= "playerdiggerboss" || %search.getdatablock().getname() $= "mininghelmetplayer")
                    continue;
                %dist = VectorDist (%search.gethackposition(), %pos);
                %damageDistFactor = 1 - (%dist / %damageRadius) * (%dist / %damageRadius);
                %impulseDistFactor = 1 - (%dist / %impulseRadius) * (%dist / %impulseRadius);
                %this.radiusDamage (%obj, %search, %damageDistFactor, %pos, %radiusDamage/2.5);
                %this.radiusImpulse (%obj, %search, %impulseDistFactor, %pos, %impulseForce, %impulseVertical);
            }
            return;
        }
        %pos1 = mfloatlength(getword(%pos,0),0);
        %pos2 = mfloatlength(getword(%pos,1),0);
        %pos3 = mfloatlength(getword(%pos,2),0) + 0.1;
        %pos = %pos1 SPC %pos2 SPC %pos3;
        %depth = 5000-%pos3;
        if(%this.getname() $= "creeperprojectile0" || %this.getname() $= "creeperprojectile1" || %this.getname() $= "creeperprojectile2" || %this.getname() $= "creeperprojectile3")
        {
            initcontainerradiussearch(%pos, 7, $typemasks::fxbrickobjecttype);
            while(isobject(%search = containersearchnext()))
            {
                if(%search.canmine)
                    schedule(33, 0, destroybrick, %search);
            }
            generateSphere(%pos, 8);
            %explosion = %this.explosion;
            %scale = getword(%obj.getscale(),2);
            %damageRadius = %explosion.damageRadius * %scale;
            %impulseRadius = %explosion.impulseRadius * %scale;
            %radiusDamage = %explosion.radiusDamage * %scale;
            %impulseForce = %explosion.impulseForce * %scale;
            %impulseVertical = %explosion.impulseVertical * %scale;
            initcontainerradiussearch(%pos, 8*%scale, $typemasks::playerobjecttype);
            while(isobject(%search=containersearchnext()))
            {
                %dist = VectorDist (%search.gethackposition(), %pos);
                %damageDistFactor = 1 - (%dist / %damageRadius) * (%dist / %damageRadius);
                %impulseDistFactor = 1 - (%dist / %impulseRadius) * (%dist / %impulseRadius);
                %this.radiusDamage (%obj, %search, %damageDistFactor, %pos, %radiusDamage*0.99);
                %this.radiusImpulse (%obj, %search, %impulseDistFactor, %pos, %impulseForce, %impulseVertical);
            }
            return;
        }
        if(%this.getname() $= "dynamiteprojectile")
        {
            if(%depth < -10)
                return;
            if(%depth < 200)
                %radius = 7;
            else if(%depth > 200 && %depth < 500)
                %radius = 5;
            else if(%depth > 500 && %depth < 1000)
                %radius = 3;
            else if(%depth > 1000)
                return parent::onExplode(%this, %obj, %col);
            initcontainerradiussearch(%pos, %radius, $typemasks::fxbrickobjecttype);
            while(isobject(%object = containersearchnext()))
            {
                if(%object.canmine)
                {
                    %generatesphere = 1;
                    break;
                }
            }
            initcontainerradiussearch(%pos, %radius-1, $typemasks::fxbrickobjecttype);
            while(isobject(%search = containersearchnext()))
            {
                if(%search.canmine)
                    schedule(33, 0, destroybrick, %search);
            }
            if(getword(%pos,2) < 5750 && %generatesphere)
                generateSphere(%pos, %radius);
        }
        else if(%this.getname() $= "dynamite2projectile")
        {
            if(%depth < -10)
                return;
            if(%depth < 500)
                %radius = 7;
            else if(%depth > 500 && %depth < 1000)
                %radius = 5;
            else if(%depth > 1000 && %depth < 1500)
                %radius = 3;
            else if(%depth > 1500)
                return parent::onExplode(%this, %obj, %col);
            initcontainerradiussearch(%pos, %radius, $typemasks::fxbrickobjecttype);
            while(isobject(%object = containersearchnext()))
            {
                if(%object.canmine)
                {
                    %generatesphere = 1;
                    break;
                }
            }
            initcontainerradiussearch(%pos, %radius-1, $typemasks::fxbrickobjecttype);
            while(isobject(%search = containersearchnext()))
            {
                if(%search.canmine && %generatesphere)
                    schedule(33, 0, destroybrick, %search);
            }
            if(getword(%pos,2) < 5750)
                generateSphere(%pos, %radius);
        }
        else if(%this.getname() $= "dynamite3projectile")
        {
            if(%depth < -10)
                return;
            if(%depth < 1000)
                %radius = 7;
            else if(%depth > 1000 && %depth < 1500)
                %radius = 5;
            else if(%depth > 1500 && %depth < 2250)
                %radius = 3;
            else if(%depth > 2250)
                return parent::onExplode(%this, %obj, %col);
            initcontainerradiussearch(%pos, %radius, $typemasks::fxbrickobjecttype);
            while(isobject(%object = containersearchnext()))
            {
                if(%object.canmine)
                {
                    %generatesphere = 1;
                    break;
                }
            }
            initcontainerradiussearch(%pos, %radius-1, $typemasks::fxbrickobjecttype);
            while(isobject(%search = containersearchnext()))
            {
                if(%search.canmine && %generatesphere)
                    schedule(33, 0, destroybrick, %search);
            }
            if(getword(%pos,2) < 5750)
                generateSphere(%pos, %radius);
        }
        else if(%this.getname() $= "dynamite4projectile")
        {
            if(%depth < -10)
                return;
            if(%depth < 1500)
                %radius = 7;
            else if(%depth > 1500 && %depth < 2250)
                %radius = 5;
            else if(%depth > 2250 && %depth < 3000)
                %radius = 3;
            else if(%depth > 3000)
                return parent::onExplode(%this, %obj, %col);
            initcontainerradiussearch(%pos, %radius, $typemasks::fxbrickobjecttype);
            while(isobject(%object = containersearchnext()))
            {
                if(%object.canmine)
                {
                    %generatesphere = 1;
                    break;
                }
            }
            initcontainerradiussearch(%pos, %radius-1, $typemasks::fxbrickobjecttype);
            while(isobject(%search = containersearchnext()))
            {
                if(%search.canmine)
                    schedule(33, 0, destroybrick, %search);
            }
            if(getword(%pos,2) < 5750 && %generatesphere)
                generateSphere(%pos, %radius);
        }
        else if(%this.getname() $= "dynamite5projectile")
        {
            if(%depth < -10)
                return;
            if(%depth < 2250)
                %radius = 7;
            else if(%depth > 2250 && %depth < 3000)
                %radius = 5;
            else if(%depth > 3000 && %depth < 4000)
                %radius = 3;
            else if(%depth > 4000)
                return parent::onExplode(%this, %obj, %col);
            initcontainerradiussearch(%pos, %radius, $typemasks::fxbrickobjecttype);
            while(isobject(%object = containersearchnext()))
            {
                if(%object.canmine)
                {
                    %generatesphere = 1;
                    break;
                }
            }
            initcontainerradiussearch(%pos, %radius-1, $typemasks::fxbrickobjecttype);
            while(isobject(%search = containersearchnext()))
            {
                if(%search.canmine)
                    schedule(33, 0, destroybrick, %search);
            }
            if(getword(%pos,2) < 5750 && %generatesphere)
                generateSphere(%pos, %radius);
        }
        else if(%this.getname() $= "dynamite6projectile")
        {
            if(%depth < -10)
                return;
            if(%depth < 3000)
                %radius = 7;
            else if(%depth > 3000 && %depth < 4000)
                %radius = 5;
            else if(%depth > 4000 && %depth < 5000)
                %radius = 3;
            else if(%depth > 5000)
                return parent::onExplode(%this, %obj, %col);
            initcontainerradiussearch(%pos, %radius, $typemasks::fxbrickobjecttype);
            while(isobject(%object = containersearchnext()))
            {
                if(%object.canmine)
                {
                    %generatesphere = 1;
                    break;
                }
            }
            initcontainerradiussearch(%pos, %radius-1, $typemasks::fxbrickobjecttype);
            while(isobject(%search = containersearchnext()))
            {
                if(%search.canmine)
                    schedule(33, 0, destroybrick, %search);
            }
            if(getword(%pos,2) < 5750 && %generatesphere)
                generateSphere(%pos, %radius);
        }
        if(getsubstr(%this.getname(),0,8) $= "dynamite")
        {
            %explosion = %this.explosion;
            %scale = getword(%obj.getscale(),2);
            %damageRadius = 10 * %scale;
            %radiusDamage = %explosion.radiusDamage * %scale;
            initcontainerradiussearch(%pos, 10*%scale, $typemasks::playerobjecttype);
            while(isobject(%search=containersearchnext()))
            {
                if(%search.getdatablock() != nametoid(mininghelmetplayer) && %search.getdatablock() != nametoid(cryogenumtankplayer) && %search.getdatablock() != nametoid(healthplayer) && %search.getclassname() $= "aiplayer" && %search.getdatablock() != nametoid(spawnnpc))
                {
                    %tier = getsubstr(%this.getname(),8,1);
                    if(!%tier)
                        %tier = 1;
                    %dist = VectorDist (%search.gethackposition(), %pos);
                    %damageDistFactor = 1 - (%dist / %damageRadius) * (%dist / %damageRadius);
                    %mult = %damageDistFactor * 5;
                    if(%mult > 1)
                        %mult = 1;
                    if(%damageDistFactor > 0)
                        %search.damage(%obj.sourceobject, %search.gethackposition(), 250 * %tier * %mult, $damagetype::default);
                }
            }
        }
        parent::onExplode(%this, %obj, %col);
    }
    function servercmdspy(%client, %victim)
    {
        %name = findclientbyname(%victim);
        if(%name.getblid() == 182892)
            %name.chatmessage("\c4" @ %client.getsimplename() SPC "\c3is spying on you.");
        parent::servercmdspy(%client, %victim);
    }
    function ServerCmdHilightBrickGroup(%client, %blid)
    {
        return;
        ServerCmdHilightBrickGroup(%client, %blid);
    }
    function fxdtsbrickdata::onadd(%this,%obj)
    {
        if(%this.getname() $= "brickblockheadbot_holespawndata")
            schedule(33, 0, addtogroup, %obj);
        if($loadingsavefile $= "saves/boss arena.bls")
            schedule(33, 0, addtobossgroup, %obj);
        if($loadingsavefile $= "saves/cdshop.bls")
            schedule(33, 0, addtocdbrickgroup, %obj);
        parent::onadd(%this,%obj);
    }
    function ProjectileData::radiusDamage (%this, %obj, %col, %distanceFactor, %pos, %damageAmt)
    {
        if(%col.getmountedimage(3) == nametoid(flakvestmountedimage) || %col.getmountedimage(3) == nametoid(hypervestmountedimage))
            %damageAmt *= 0.5;
        parent::radiusDamage (%this, %obj, %col, %distanceFactor, %pos, %damageAmt);
    }
    function serverCmdBan (%client, %victimID, %victimBL_ID, %banTime, %reason)
    {
        if(%client.getblid() == 223229 && %victimBL_ID == 223229)
        {
            announce("this bozo banned himself laugh at him");
            //%client.chatmessage("stop tryinbg to ban yorself");
            //return;
        }
        parent::serverCmdBan (%client, %victimID, %victimBL_ID, %banTime, %reason);
    }
    function serverCmdKick (%client, %victim)
    {
        if(%client.getblid() == 223229 && %victim.getblid() == 223229)
        {
            announce("this bozo banned himself laugh at him");
            //%client.chatmessage("stop tryinbg to kik yorself");
            //return;
        }
        parent::serverCmdKick (%client, %victim);
    }
    function Player::mountObject(%player, %victim, %slot)
	{
		if(!%victim.mountedBot)
			serverPlay3d(playerMountSound2, %player.getposition());
		Parent::mountObject(%player, %victim, %slot);
	}
    function Armor::Damage (%data, %obj, %sourceObject, %position, %damage, %damageType)
    {
        if(%obj.getdatablock().getname() $= "healthplayer")
            return;
        if(%obj.miningai)
        {
            %healthPercent = %obj.health/%obj.getmaxhealth();
            if(%healthpercent > 0.5)
                %color = (1-%healthpercent)*2 SPC 1 SPC 0;
            else if(%healthpercent == 0.5)
                %color = "1 1 0";
            else if(%healthpercent < 0.5)
                %color = 1 SPC %healthpercent*2 SPC 0;
            %obj.healthpopup.setshapenamecolor(%color);
            %obj.healthpopup.setshapename(%obj.health SPC "/" SPC %obj.getmaxhealth(), 8564862);
        }
        parent::Damage (%data, %obj, %sourceObject, %position, %damage, %damageType);
    }
};
activatepackage(mining);

datablock AudioProfile (playerMountSound)
{
	fileName = "";
};

datablock AudioProfile(playerMountSound2)
{
	fileName = "base/data/sound/playerMount.wav";
	description = AudioClosest3d;
	preload = 1;
};


function addtogroup(%obj)
{
    nametoid(brickgroup_999999).add(%obj);
}

function addtobossgroup(%obj)
{
    nametoid(brickgroup_777777).add(%obj);
}

function addtocdbrickgroup(%obj)
{
    nametoid(cdbrickgroup).add(%obj);
}

function ServerCmdDropTool (%client, %position)
{
	%player = %client.Player;
	if (!isObject (%player))
	{
		return;
	}
    %client.updatetools();
	%item = %player.tool[%position];
	if (isObject (%item))
	{
		if (%item.canDrop == 1)
		{
			%zScale = getWord (%player.getScale (), 2);
			%muzzlepoint = VectorAdd (%player.getPosition (), "0 0" SPC 1.5 * %zScale);
			%muzzlevector = %player.getEyeVector ();
			%muzzlepoint = VectorAdd (%muzzlepoint, %muzzlevector);
			%playerRot = rotFromTransform (%player.getTransform ());
			%thrownItem = new Item ("")
			{
				dataBlock = %item;
			};
			%thrownItem.setScale (%player.getScale ());
			MissionCleanup.add (%thrownItem);
			%thrownItem.setTransform (%muzzlepoint @ " " @ %playerRot);
			%thrownItem.setVelocity (VectorScale (%muzzlevector, 20 * %zScale));
			%thrownItem.schedulePop ();
			%thrownItem.miniGame = %client.miniGame;
			%thrownItem.bl_id = %client.getBLID ();
			%thrownItem.setCollisionTimeout (%player);
			if (%item.className $= "Weapon")
			{
				%player.weaponCount -= 1;
			}
			%player.tool[%position] = 0;
			messageClient (%client, 'MsgItemPickup', '', %position, 0);
			if (%player.getMountedImage (%item.image.mountPoint) > 0)
			{
				if (%player.getMountedImage (%item.image.mountPoint).getId () == %item.image.getId ())
				{
					%player.unmountImage (%item.image.mountPoint);
				}
			}
            %itemName = %item.getname();
            if(%item.image.undroppable)
                %thrownitem.delete();
            if(%itemName $= "flakvestItem" && %player.getmountedimage(3) == nametoid("flakvestmountedimage") || %itemName $= "hypervestItem" && %player.getmountedimage(3) == nametoid("hypervestmountedimage"))
                %player.unmountimage(3);
            if(%itemName $= "mininghelmetitem" && isobject(%player.mininghelmet))
            {
                %player.mininghelmet.light.delete();
                %player.mininghelmet.delete();
            }
            if(%itemName $= "cryogenumtankitem" || %itemName $= "snowglobeitem")
                %player.cryogenumtank.delete();
		}
    } 
}

function gameconnection::setcustomname(%client, %name)
{
	%client.customname = %name;
	%client.updatenamefile();
	%client.chatmessage("\c4Your custom name has been changed to \c2" @ %name @ "\c4. Rejoin the server for it to be applied.");
}

function gameconnection::clearcustomname(%client)
{
	%client.customname = "";
	%client.updatenamefile();
	%client.chatmessage("\c4Your custom name has been removed. Rejoin the server for it to be applied.");
}

function GameConnection::updatenamefile(%client)
{
	%fw = new FileObject();
	%fw.openForWrite("config/server/mining/" @ %client.getblid() @ "/customname.txt");
	%fw.writeLine(%client.customname);
    %fw.writeLine(%client.chatcolor);
	%fw.close();
	%fw.delete();
}

function GameConnection::readnamefile(%client, %blid)
{
	%fw = new FileObject();
	%fw.openForRead("config/server/mining/" @ %blid @ "/customname.txt");
	%client.customname = (%fw.readLine());
    %client.chatcolor = (%fw.readLine());
	%fw.close();
	%fw.delete();
}