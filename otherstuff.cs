function serverCmdMessageSent (%client, %text)
{
	if(!%client)
		return;
	%trimText = trim (%text);
	//if (%client.lastChatText $= %trimText)
	//{
		//%chatDelta = (getSimTime () - %client.lastChatTime) / getTimeScale ();
		//if (%chatDelta < 15000)
		//{
			//%client.spamMessageCount = $SPAM_MESSAGE_THRESHOLD;
			//messageClient (%client, '', '\c5Do not repeat yourself.');
		//}
	//}
	%client.lastChatTime = getSimTime ();
	%client.lastChatText = %trimText;
	%player = %client.Player;
	if (isObject (%player))
	{
		%player.playThread (3, talk);
		%player.schedule (strlen (%text) * 50, playThread, 3, root);
	}
	%text = chatWhiteListFilter (%text);
	%text = StripMLControlChars (%text);
	%text = trim (%text);
	if (strlen (%text) <= 0)
	{
		return;
	}
	if ($Pref::Server::MaxChatLen > 0)
	{
		if (strlen (%text) >= $Pref::Server::MaxChatLen)
		{
			%text = getSubStr (%text, 0, $Pref::Server::MaxChatLen);
		}
	}
	%protocol = "http://";
	%protocolLen = strlen (%protocol);
	%urlStart = strpos (%text, %protocol);
	if (%urlStart == -1)
	{
		%protocol = "https://";
		%protocolLen = strlen (%protocol);
		%urlStart = strpos (%text, %protocol);
	}
	if (%urlStart == -1)
	{
		%protocol = "ftp://";
		%protocolLen = strlen (%protocol);
		%urlStart = strpos (%text, %protocol);
	}
	if (%urlStart != -1)
	{
		%urlEnd = strpos (%text, " ", %urlStart + 1);
		%skipProtocol = 0;
		if (%protocol $= "http://")
		{
			%skipProtocol = 1;
		}
		if (%urlEnd == -1)
		{
			%fullUrl = getSubStr (%text, %urlStart, strlen (%text) - %urlStart);
			%url = getSubStr (%text, %urlStart + %protocolLen, (strlen (%text) - %urlStart) - %protocolLen);
		}
		else 
		{
			%fullUrl = getSubStr (%text, %urlStart, %urlEnd - %urlStart);
			%url = getSubStr (%text, %urlStart + %protocolLen, (%urlEnd - %urlStart) - %protocolLen);
		}
		if (strlen (%url) > 0)
		{
			%url = strreplace (%url, "<", "");
			%url = strreplace (%url, ">", "");
			if (%skipProtocol)
			{
				%newText = strreplace (%text, %fullUrl, "<a:" @ %url @ ">" @ %url @ "</a>\c6");
			}
			else 
			{
				%newText = strreplace (%text, %fullUrl, "<a:" @ %protocol @ %url @ ">" @ %url @ "</a>\c6");
			}
			echo (%newText);
			%text = %newText;
		}
	}
	if ($Pref::Server::ETardFilter)
	{
		if (!chatFilter (%client, %text, $Pref::Server::ETardList, '\c5This is a civilized game.  Please use full words.'))
		{
			return 0;
		}
	}
	//%color = %client.chatcolor;
	//if(%client.isadmin && !%client.ishost && %color $= "")
		//%color = "<color:ff265e>";
	//else if(%client.ishost && %color $= "")
		//%color = "<color:7B68EE>";

	if(%color $= "")
		%color = "\c3";

	%level = %client.level;
	if(%level $= "")
		%level = 0;
    %prestige = %client.prestige;
    if(%prestige > 0)
        %prestigecount = "\c6[\c5" @ numbersToLatin(%client.prestige) @ "\c6] ";
    else
        %prestigecount = "";
    if(%client.player)
    {
        %pos = 5000.2-getword(%client.player.getposition(),2);
		if(%pos < -100)
            %depth = "<color:32CD32>[" @ mfloatlength(%pos,0) @ "m]";
        else if(%pos > -100 && %pos < -20)
            %depth = "\c6[" @ mfloatlength(%pos,0) @ "m]";
        else if(%pos > -20 && %pos < 0)
            %depth = "\c6[0m]";
        else if(%pos > 0 && %pos < 500)
            %depth = "\c6[" @ mfloatlength(%pos,0) @ "m]";
        else if(%pos > 500 && %pos < 1000)
            %depth = "<color:888888>[" @ mfloatlength(%pos,0) @ "m]";
        else if(%pos > 1000 && %pos < 1500)
            %depth = "<color:666666>[" @ mfloatlength(%pos,0) @ "m]";
        else if(%pos > 1500 && %pos < 2250)
            %depth = "<color:444444>[" @ mfloatlength(%pos,0) @ "m]";
        else if(%pos > 2250 && %pos < 3000)
            %depth = "<color:333333>[" @ mfloatlength(%pos,0) @ "m]";
        else if(%pos > 3000 && %pos < 4000)
            %depth = "<color:3B0000>[" @ mfloatlength(%pos,0) @ "m]";
        else if(%pos > 4000 && %pos < 5000)
            %depth = "<color:6e0000>[" @ mfloatlength(%pos,0) @ "m]";
		else if(%pos > 5000 && %pos < 6250)
            %depth = "<color:101010>[" @ mfloatlength(%pos,0) @ "m]";
		else if(%pos > 6250)
            %depth = "<color:000000>[" @ mfloatlength(%pos,0) @ "m]";
    }
    else
        %depth = "\c6[0m]";
	//for(%i = 0; %i < clientgroup.getcount(); %i++)
	//{
		//%clients = clientgroup.getobject(%i);
		//%clients.chatmessage("\c2" @ %depth SPC %prestigecount @ "\c3(\c4" @ %level @ "\c3)\c3" SPC "\c7" @ %client.clanPrefix @ "\c3" @ %client.getsimplename() @ "\c7" @ %client.clanSuffix @ "\c6:" SPC %text);
	//}
	if(%client.rlcbmmute)
	{
		%client.chatmessage("\c2" @ %depth SPC %prestigecount @ "\c3(\c4" @ %level @ "\c3)\c3" SPC "\c7" @ %client.clanPrefix @ "\c3" @ %client.getsimplename() @ "\c7" @ %client.clanSuffix @ "\c6:" SPC %text);
		echo (%client.getSimpleName (), ": ", %text);
		return;
	}
	if(!%client.cdplayer && !%client.player.fightingdigger)
		chatMessageAll (%client, '\c7%1\c3%2\c7%3\c6: %4', %depth SPC %prestigecount @ "\c3(\c4" @ %level @ "\c3)\c7" SPC %client.clanPrefix, %color @ %client.getPlayerName (), %client.clanSuffix, %text);
	else
		chatMessageAll (%client, '\c7%1\c3%2\c7%3\c6: %4', %prestigecount @ "\c3(\c4" @ %level @ "\c3)\c7" SPC %client.clanPrefix, %color @ %client.getPlayerName (), %client.clanSuffix, %text);
	echo (%client.getSimpleName (), ": ", %text);
}

function ServerCmdPlantBrick (%client)
{
	if ($Game::MissionCleaningUp)
	{
		return 0;
	}
	%player = %client.Player;
	%tempBrick = %player.tempBrick;
	if (!isObject (%player))
	{
		return;
	}
	%player.playThread (3, plant);
	%mg = %client.miniGame;
	if (isObject (%mg))
	{
		if (!%mg.EnableBuilding)
		{
			return 0;
		}
	}
	//if (getBrickCount () >= getBrickLimit ())
	//{
		//messageClient (%client, 'MsgPlantError_Limit');
		//return 0;
	//}
	if (!%client.isAdmin && !%client.isSuperAdmin)
	{
		if ($Server::MaxBricksPerSecond > 0)
		{
			%currTime = getSimTime ();
			if (%client.bpsTime + 1000 < %currTime)
			{
				%client.bpsCount = 0;
				%client.bpsTime = %currTime;
			}
			if (%client.bpsCount >= $Server::MaxBricksPerSecond)
			{
				return 0;
			}
		}
	}
	if (!isObject (%tempBrick))
	{
		return 0;
	}
	%tempBrickTrans = %tempBrick.getTransform ();
	%tempBrickPos = getWords (%tempBrickTrans, 0, 2);
	%brickData = %tempBrick.getDataBlock ();
	if (%brickData.brickSizeX > %brickData.brickSizeY)
	{
		%brickRadius = %brickData.brickSizeX;
	}
	else 
	{
		%brickRadius = %brickData.brickSizeY;
	}
	%brickRadius = (%brickRadius * 0.5) / 2;
	if ($Pref::Server::TooFarDistance == 0 || $Pref::Server::TooFarDistance $= "")
	{
		$Pref::Server::TooFarDistance = 50;
	}
	$Pref::Server::TooFarDistance = mClampF ($Pref::Server::TooFarDistance, 20, 99999);
	if (VectorDist (%tempBrickPos, %client.Player.getPosition ()) > $Pref::Server::TooFarDistance + %brickRadius)
	{
		messageClient (%client, 'MsgPlantError_TooFar');
		return 0;
	}
	%plantBrick = new fxDTSBrick ("")
	{
		dataBlock = %tempBrick.getDataBlock ();
		position = %tempBrickTrans;
		isPlanted = 1;
	};
	%client.brickGroup.add (%plantBrick);
	%plantBrick.setTransform (%tempBrickTrans);
	%plantBrick.setColor (%tempBrick.getColorID ());
	%plantBrick.setPrint (%tempBrick.getPrintID ());
	%plantBrick.client = %client;
	%plantErrorCode = %plantBrick.plant ();
	if (!%plantBrick.isColliding ())
	{
		%plantBrick.dontCollideAfterTrust = 1;
	}
	%plantBrick.setColliding (0);
	if (%plantErrorCode == 0)
	{
		if (!$Server::LAN)
		{
			if (%plantBrick.getNumDownBricks ())
			{
				%plantBrick.stackBL_ID = %plantBrick.getDownBrick (0).stackBL_ID;
			}
			else if (%plantBrick.getNumUpBricks ())
			{
				%plantBrick.stackBL_ID = %plantBrick.getUpBrick (0).stackBL_ID;
			}
			else 
			{
				%plantBrick.stackBL_ID = %client.getBLID ();
			}
			if (%plantBrick.stackBL_ID <= 0)
			{
				%plant.stackBL_ID = %client.getBLID ();
			}
		}
		%client.undoStack.push (%plantBrick TAB "PLANT");
		if ($Server::LAN)
		{
			%plantBrick.trustCheckFinished ();
		}
		else 
		{
			%plantBrick.PlantedTrustCheck ();
		}
		ServerPlay3D (brickPlantSound, %plantBrick.getTransform ());
		if ($Pref::Server::RandomBrickColor == 1)
		{
			%randColor = getRandom (5);
			if (%randColor == 0)
			{
				%player.tempBrick.setColor (0);
			}
			else if (%randColor == 1)
			{
				%player.tempBrick.setColor (1);
			}
			else if (%randColor == 2)
			{
				%player.tempBrick.setColor (3);
			}
			else if (%randColor == 3)
			{
				%player.tempBrick.setColor (4);
			}
			else if (%randColor == 4)
			{
				%player.tempBrick.setColor (5);
			}
			else if (%randColor == 5)
			{
				%player.tempBrick.setColor (7);
			}
		}
		else 
		{
			%player.tempBrick.setColor (%client.currentColor);
		}
		%client.bpsCount += 1;
	}
	else if (%plantErrorCode == 1)
	{
		%plantBrick.delete ();
		messageClient (%client, 'MsgPlantError_Overlap');
	}
	else if (%plantErrorCode == 2)
	{
		%plantBrick.delete ();
		messageClient (%client, 'MsgPlantError_Float');
	}
	else if (%plantErrorCode == 3)
	{
		%plantBrick.delete ();
		messageClient (%client, 'MsgPlantError_Stuck');
	}
	else if (%plantErrorCode == 4)
	{
		%plantBrick.delete ();
		messageClient (%client, 'MsgPlantError_Unstable');
	}
	else if (%plantErrorCode == 5)
	{
		%plantBrick.delete ();
		messageClient (%client, 'MsgPlantError_Buried');
	}
	else 
	{
		%plantBrick.delete ();
		messageClient (%client, 'MsgPlantError_Forbidden');
	}
	if (getBrickCount () <= 100 && getRayTracerProgress () <= -1 && getRayTracerProgress () < 0 && $Server::LAN == 0 && doesAllowConnections ())
	{
		startRaytracer ();
	}
	return %plantBrick;
}

function servercmdac(%client, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13, %a14, %a15, %a16, %a17, %a18, %a19, %a20)
{
	if(!%client.isadmin && %client !$= "host")
		return;
	%text = %a1 SPC %a2 SPC %a3 SPC %a4 SPC %a5 SPC %a6 SPC %a7 SPC %a8 SPC %a9 SPC %a10 SPC %a11 SPC %a12 SPC %a13 SPC %a14 SPC %a15 SPC %a16 SPC %a17 SPC %a18 SPC %a19 SPC %a20;
	for(%i = 0; %i < clientgroup.getcount(); %i++)
	{
		%clients = clientgroup.getobject(%i);
		if(%clients.isadmin || %clients.issuperadmin)
		{
			if(%client $= "host")
				%clients.chatmessage("\c2admin chat: \c5" @ "CONSOLE" @ "\c3:" SPC %text);
			else
				%clients.chatmessage("\c2admin chat: \c5" @ %client.getsimplename() @ "\c3:" SPC %text);
		}
	}
	if(%client $= "host")
		echo ("ADMIN CHAT:" SPC "CONSOLE", ": ", %text);
	else
		echo ("ADMIN CHAT:" SPC %client.getSimpleName (), ": ", %text);
}

function ac(%text)
{
	servercmdac(host, %text);
}

function servercmdrlcbmMute(%client, %target)
{
	if(!%client.isadmin)
		return;
	%muted = findclientbyname(%target).rlcbmmute;
	if(!%muted)
	{
		findclientbyname(%target).rlcbmMute = 1;
		%client.chatmessage(findclientbyname(%target).name SPC "has been rlcbm muted");
	}
	else if(%muted)
	{
		findclientbyname(%target).rlcbmMute = 0;
		%client.chatmessage(findclientbyname(%target).name SPC "has been rlcbm unmuted");
	}
}

function talk (%text)
{
	if (%text !$= "")
	{
		MessageAll ('', "\c5[CONSOLE] \c4Skyboard: \c6" @ %text);
	}
}

function player::conveyorCheck(%player, %vector)
{
	%ray = containerraycast(%player.gethackposition(), vectoradd(%player.gethackposition(), "0 0 -2"), $typemasks::FxBrickAlwaysObjectType, %player);
	if(firstword(%ray) && %ray.colorid == 16)
	{
		if(%ray.angleid == 1 || %ray.angleid == 3)
			%player.settransform(getword(%ray.position,0) SPC restwords(%player.position));
		else if(%ray.angleid == 0 || %ray.angleid == 2)
			%player.settransform(getword(%player.position,0) SPC getword(%ray.position,1) SPC getword(%player.position,2));
		%player.setvelocity(%vector);
		%player.schedule(100, setvelocity, %vector);
	}
}

registerOutputEvent ("Player", "conveyorCheck", "vector 200");

function servercmdcosmetic(%client, %var1, %var2, %var3, %var4)
{
	if(%var1 !$= "" && %var2 !$= "" && %var3 !$= "" && %var4 !$= "")
		%var = %var1 SPC %var2 SPC %var3 SPC %var4;
	else if(%var1 !$= "" && %var2 !$= "" && %var3 !$= "")
		%var = %var1 SPC %var2 SPC %var3;
	else if(%var1 !$= "" && %var2 !$= "")
		%var = %var1 SPC %var2;
	else if(%var1 !$= "")
		%var = %var1;
	if(%var $= "")
	{
		%client.chatmessage("<color:ffffff>cosmetics help if you forgor");
		%client.chatmessage("<color:ff4500>/cosmetic all<color:fff000> - shows a list of all cosmetics you can get");
		%client.chatmessage("<color:ff4500>/cosmetic equip [name]<color:fff000> - equips the cosmetic (works as a toggle, aka write the same to unequip it)");
	}
	else if(%var $= "all")
	{
		%client.chatmessage("\c5Halloween 2023 Cosmetics:");
		for(%i = 0; %i < $craftcount; %i++)
		{
			%color = "<color:ff4500>";
			%item = $craft[%i];
			if(getfield(%item, getfieldcount(%item)-2) $= "COSMETIC")
			{
				if(getfield(%item,0) $= "Icebreaker")
				{
					%client.chatmessage("\c5Christmas 2023 Cosmetics:");
					%craftID = %i;
				}
				if(%craftID > 0 && %i >= %craftID)
					%color = "\c4";
				if(getfield(%item,3) !$= "")
					%client.chatmessage(%color @ getfield(%item,0) SPC "<color:ffffff>- Can be Crafted");
				else
					%client.chatmessage(%color @ getfield(%item,0) SPC "<color:ffffff>- Can be Obtained");
			}
		}
	}
	else if(firstword(%var) $= "equip")
		%client.equipcosmetic(restwords(%var));
}

function gameconnection::equipcosmetic(%client, %cosmetic)
{
	for(%i = 0; %i < $craftcount; %i++)
	{
		%cosmeticName = $craft[craftIDfromName(%cosmetic)];
		if(getfield($craft[%i],0) !$= %cosmetic && getfield($craft[%i],getfieldcount($craft[%i])-1) $= getfield(%cosmeticName,getfieldcount(%cosmeticName)-1))
			%client.equippedcosmetic[getfield($craft[%i],0)] = 0;
	}
	if(craftidfromname(%cosmetic) != -1)
	{
		if(%client.craftedcosmetic[%cosmetic])
		{
			if(!%client.equippedcosmetic[%cosmetic])
			{
				%client.equippedcosmetic[%cosmetic] = 1;
				%client.chatmessage("\c2Equipped the" SPC getfield($craft[craftidfromname(%cosmetic)],0) SPC "cosmetic!");
				%client.playsound(beep_key_sound);
				%client.updateequippedcosmetictools();
			}
			else if(%client.equippedcosmetic[%cosmetic])
			{
				%client.equippedcosmetic[%cosmetic] = 0;
				%client.chatmessage("\c2Unequipped the" SPC getfield($craft[craftidfromname(%cosmetic)],0) SPC "cosmetic!");
				%client.playsound(beep_key_sound);
				%client.updateequippedcosmetictools();
			}
		}
		else
		{
			%client.chatmessage("you do not have this owned");
			%client.playsound(errorsound);
		}
	}
}

function servercmdStats(%client)
{ 
	if(%client.prestige>=0)
    {
		%startdepth = 100;
		if(%client.prestigestartdepth == 1)
			%startdepth = 200;
		else if(%client.prestigestartdepth == 2)
			%startdepth = 300;
		else if(%client.prestigestartdepth == 3)
			%startdepth = 400;
		else if(%client.prestigestartdepth == 4)
			%startdepth = 500;
		messageClient(%client, '', "\c6Bonus Flat Mining Power: +\c3" @ %client.prestigeminingpower);
		messageClient(%client, '', "\c6Bonus Mining Power Multiplier: \c3" @ 1+%client.prestigeminingmultiplier @ "\c6x");
		messageClient(%client, '', "\c6Max Mining Power On Levelup: +\c3" @ 200+%client.prestigemaxmining*100 @ "\c6%");
		messageClient(%client, '', "\c6EXP Multiplier: \c5" @ 1+%client.prestigeexpbonus @ "\c6x");
		messageClient(%client, '', "\c6Cash Multiplier: \c2" @ 1+%client.prestigecashbonus @ "\c6x");
		messageClient(%client, '', "\c6Crate Drop Multiplier: \c2" @ 1+%client.prestigecratedrops @ "\c6x");
		messageClient(%client, '', "\c6Starting Level: \c4" @ %client.prestigestartlevel);
		messageClient(%client, '', "\c6Starting Optimal Depth: \c4" @ %startdepth @ "m");
		messageClient(%client, '', "\c6Prestige Points Multiplier: \c5" @ 1+%client.prestigepointsbuff @ "\c6x");
		messageClient(%client, '', "\c6Tunneler HP Penalty From Lava: <color:ff0000>" @ 5-%client.tunnelerlavares @ "\c6%");
		messageClient(%client, '', "\c6Tunneler Cooldown: \c3" @ 150-%client.tunnelercooldown @ "\c6s");
		messageClient(%client, '', "\c6Prestige Count: \c5" @ %client.prestige);
		messageClient(%client, '', "\c6Unspent Prestige Points: \c5" @ %client.prestigepoints);
	}
    else
		messageClient(%client, '', "\c6You have not prestiged yet!");
}

function servercmdhelp(%client)
{
	%client.chatmessage("\c2/spawn \c6- teleports you to spawn");
	%client.chatmessage("\c2/prestigeshop \c6- teleports you to the prestige shop");
	%client.chatmessage("\c2/info [ore name or craft recipe] \c6- shows you information about that thing");
	%client.chatmessage("\c2/inventory \c6- view inventory (/inventory [ore] allows to view only 1 type of ore)");
	%client.chatmessage("\c2/sell [ore] [amount] \c6- sells specified amount of ores (/sell [all] works too)");
	%client.chatmessage("\c2/lock [ore] \c6- blacklists an ore from being sold by typing /sell all (works as a toggle)");
	%client.chatmessage("\c2/listlock \c6- lists all locked ores");
	%client.chatmessage("\c2/unlockall \c6- unlocks all ores");
	%client.chatmessage("\c2/buyexp [amount] \c6- buys exp in exchange for gold (/buyexp [all] works too)");
	%client.chatmessage("\c2/cosmetic \c6- shows you information about cosmetics");
	%client.chatmessage("\c2/upgradedepth \c6- upgrade optimal depth");
	%client.chatmessage("\c2/upgradeinventory \c6- upgrade max inventory slots");
	%client.chatmessage("\c2/stats \c6- shows your information about prestiges");
	%client.chatmessage("\c2/leaderboard \c6- shows the leaderboard");
	%client.chatmessage("\c2/trade [name] \c6- sends a trade request to that person");
	%client.chatmessage("\c2/changelog \c6- shows information about all updates");
}

function onObjectCollisionTest(%obj, %col)
{
    if(isobject(%obj) && %obj.getType() & $TypeMasks::PlayerObjectType && isobject(%col) && %col.getType() & $TypeMasks::FxBrickAlwaysObjectType)
    {   
        if(%obj.client.nocollision[%col.getname()] == 1)
            return false;
    }
    return true;
}

$oldcraftamount = 0;
function registerOldCraft(%name)
{
    $oldcraft[$oldcraftamount] = %name;
    $oldcraftamount++;
}
registerOldCraft("Iron Pickaxe");
registerOldCraft("Gold Pickaxe");
registerOldCraft("Quartz Pickaxe");
registerOldCraft("Cobalt Pickaxe");
registerOldCraft("Palladium Pickaxe");
registerOldCraft("Emerald Pickaxe");
registerOldCraft("Diamond Pickaxe");
registerOldCraft("Titanium Pickaxe");
registerOldCraft("Uranium Pickaxe");
registerOldCraft("Chromodium Pickaxe");
registerOldCraft("Luminite Pickaxe");
registerOldCraft("Aurium Pickaxe");
registerOldCraft("Dragonstone Pickaxe");
registerOldCraft("Shadowlight Pickaxe");
registerOldCraft("Uelibloom Pickaxe");
registerOldCraft("Brimstone Pickaxe");
registerOldCraft("Auric Tesla Pickaxe");
registerOldCraft("Placement Tool");
registerOldCraft("Tunneler");
registerOldCraft("Mining Helmet");
registerOldCraft("TNT Launcher");
registerOldCraft("Laser Drill");
registerOldCraft("Cryogenum Tank");
registerOldCraft("Geolocator");