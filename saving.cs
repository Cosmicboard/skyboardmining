if($GameModeArg $= "Add-Ons/Gamemode_skyboard/gamemode.txt")
    $directory = "config/server/mining/";
else if($GameModeArg $= "Add-Ons/Gamemode_skyboard_dev/gamemode.txt")
    $directory = "config/server/mining/dev/";

function gameconnection::saveBackup(%client, %number)
{
    %client.backupStats(%number);
    %client.backupInventory(%number);
    %client.backupPickaxes(%number);
    %client.backupPrestige(%number);
}

function GameConnection::backupStats(%client, %number)
{
    %fw = new FileObject();
    %fw.openForWrite($directory @ %client.getblid() @ "/BACKUP" SPC %number @ "/save.txt");
	%fw.writeLine(%client.level);
    %fw.writeLine(%client.exp);
    %fw.writeLine(%client.money);
    %fw.writeLine(%client.miningpower);
    %fw.writeLine(%client.miningmultiplier);
    %fw.writeLine(%client.optimaldepth);
    %fw.writeLine(%client.inventoryslots);
    %fw.writeLine(%client.past500);
    %fw.writeLine(%client.past1000);
    %fw.writeLine(%client.past1500);
    %fw.writeLine(%client.past2250);
    %fw.writeLine(%client.past3000);
    %fw.writeLine(%client.past4000);
    %fw.writeLine(%client.past5000);
	%fw.close();
	%fw.delete();
}

function GameConnection::backupInventory(%client, %number)
{
    %fw = new FileObject();
	%fw.openForWrite($directory @ %client.getblid() @ "/BACKUP" SPC %number @ "/ores.txt");
	for(%i = 0; %i < $orecount; %i++)
    {
        if(%client.inventory[getfield($ore[%i],0)] > 0)
            %fw.writeLine(getfield($ore[%i],0) TAB %client.inventory[getfield($ore[%i],0)]);
    }
    for(%i = 0; %i < $advanceditems; %i++)
    {
        if(%client.inventory[getfield($advanceditem[%i],0)] > 0 && getfield($advanceditem[%i],getfieldcount($advanceditem[%i])-1) !$= "TOOL" && getfield($advanceditem[%i],getfieldcount($advanceditem[%i])-1) !$= "ITEM")
            %fw.writeLine(getfield($advanceditem[%i],0) TAB %client.inventory[getfield($advanceditem[%i],0)]);
    }
	%fw.close();
	%fw.delete();
}

function GameConnection::backupPickaxes(%client, %number)
{
    %fw = new FileObject();
	%fw.openForWrite($directory @ %client.getblid() @ "/BACKUP" SPC %number @ "/items.txt");
	for(%i = 0; %i < $craftcount; %i++)
    {
        if(%client.craftedPickaxe[getfield($craft[%i],0)] && getfield($craft[%i],getfieldcount($craft[%i]-2)) !$= "COSMETIC")
            %fw.writeLine(getfield($craft[%i],0) TAB 1);
    }
	%fw.close();
	%fw.delete();
}

function GameConnection::backupPrestige(%client, %number)
{
    %fw = new FileObject();
	%fw.openForWrite($directory @ %client.getblid() @ "/BACKUP" SPC %number @ "/prestige.txt");
	%fw.writeLine(%client.prestige);
    %fw.writeLine(%client.prestigepoints);
    %fw.writeLine(%client.prestigeminingpower);
    %fw.writeLine(%client.prestigeminingmultiplier);
    %fw.writeLine(%client.prestigeexpbonus);
    %fw.writeLine(%client.prestigecashbonus);
    %fw.writeLine(%client.prestigecratedrops);
    %fw.writeLine(%client.prestigestartlevel);
    %fw.writeLine(%client.prestigemaxmining);
    %fw.writeLine(%client.prestigepointsbuff);
    %fw.writeLine(%client.tunnelerlavares);
    %fw.writeLine(%client.respecprestige);
    %fw.writeLine(%client.tunnelercooldown);
    %fw.writeLine(%client.prestigestartdepth);
	%fw.close();
	%fw.delete();
}

function GameConnection::updatestats(%client, %backup)
{
    %i = 0;
    %j = 0;
    %fw = new FileObject();
    if(!%backup)
    {
        %fw.openForRead($directory @ %client.getblid() @ "/save.txt");
        if(%fw.readline() !$= "")
        {
            %fw.openForRead($directory @ %client.getblid() @ "/save.txt");
            while(!%fw.iseof())
            {
                %line = %fw.readline();
                %client.backup[%i] = %line;
                %i++;
            }
            %fw.openForWrite($directory @ %client.getblid() @ "/saveBACKUP.txt");
            while(%i > %j)
            {
                %fw.writeline(%client.backup[%j]);
                %j++;
            }
        }
        else
        {
            if(%fw.openForRead($directory @ %client.getblid() @ "/saveBACKUP.txt"))
            {
                //%client.chatmessage("\c0During saving your save file was corrupted but was restored by a backup file.");
                %fw.openForRead($directory @ %client.getblid() @ "/saveBACKUP.txt");
                while(!%fw.iseof())
                {
                    %line = %fw.readline();
                    %client.backupfail[%i] = %line;
                    %i++;
                }
                %fw.openForWrite($directory @ %client.getblid() @ "/save.txt");
                while(!%fw.iseof())
                {
                    %fw.writeLine(%client.backupfail[%j]);
                    %j++;
                }
            }
            else
            {
                //%client.chatmessage("\c0During saving your save file was corrupted but it seems your backup file was corrupted too. Sorry for the inconvenience.");
            }
        }
    }
	%fw.openForWrite($directory @ %client.getblid() @ "/save.txt");
	%fw.writeLine(%client.level);
    %fw.writeLine(%client.exp);
    %fw.writeLine(%client.money);
    %fw.writeLine(%client.miningpower);
    %fw.writeLine(%client.miningmultiplier);
    %fw.writeLine(%client.optimaldepth);
    %fw.writeLine(%client.inventoryslots);
    %fw.writeLine(%client.past500);
    %fw.writeLine(%client.past1000);
    %fw.writeLine(%client.past1500);
    %fw.writeLine(%client.past2250);
    %fw.writeLine(%client.past3000);
    %fw.writeLine(%client.past4000);
    %fw.writeLine(%client.past5000);
    %fw.writeLine(%client.heatpoints);
	%fw.close();
	%fw.delete();
}

function GameConnection::readsavefile(%client)
{
	%fw = new FileObject();
	%fw.openForRead($directory @ %client.getblid() @ "/save.txt");
    if(%fw.readline() $= "")
    {
        if(%fw.openForRead($directory @ %client.getblid() @ "/saveBACKUP.txt"))
        {
            //%client.chatmessage("\c0Your save file was corrupted but was restored by a backup file.");
        }
        else
        {
            //%client.chatmessage("\c0Your save file was corrupted but it seems your backup file was corrupted too. Sorry for the inconvenience.");
        }
    }
    else
	    %fw.openForRead($directory @ %client.getblid() @ "/save.txt");
	%client.level = (%fw.readLine());
	%client.exp = (%fw.readLine());
    %client.money = (%fw.readLine());
	%client.miningpower = (%fw.readLine());
    %client.miningmultiplier = (%fw.readLine());
    %client.optimaldepth = (%fw.readLine());
    %client.inventoryslots = (%fw.readLine());
    %client.past500 = (%fw.readLine());
    %client.past1000 = (%fw.readLine());
    %client.past1500 = (%fw.readLine());
    %client.past2250 = (%fw.readLine());
    %client.past3000 = (%fw.readLine());
    %client.past4000 = (%fw.readLine());
    %client.past5000 = (%fw.readLine());
    %client.heatpoints = (%fw.readLine());
	%fw.close();
	%fw.delete();
}

function GameConnection::updateinventory(%client, %backup)
{
    %success = 0;
    %i = 0;
    %j = 0;
    %fw = new FileObject();
    if(!%backup)
    {
        %fw.openForRead($directory @ %client.getblid() @ "/ores.txt");
        while(!%fw.iseof())
        {
            if(%fw.readline() !$= "")
            {
                %success = true;
                break;
            }
        }
        if(%success)
        {
            %fw.openForRead($directory @ %client.getblid() @ "/ores.txt");
            while(!%fw.iseof())
            {
                %line = %fw.readline();
                %client.itembackup[%i] = %line;
                %i++;
            }
            %fw.openForWrite($directory @ %client.getblid() @ "/oresBACKUP.txt");
            while(%i > %j)
            {
                %fw.writeline(%client.itembackup[%j]);
                %j++;
            }
        }
        else
        {
            if(%fw.openForRead($directory @ %client.getblid() @ "/oresBACKUP.txt"))
            {
                //%client.chatmessage("\c0During saving your save file was corrupted but was restored by a backup file.");
                %fw.openForRead($directory @ %client.getblid() @ "/oresBACKUP.txt");
                while(!%fw.iseof())
                {
                    %line = %fw.readline();
                    %client.itembackupfail[%i] = %line;
                    %i++;
                }
                %fw.openForWrite($directory @ %client.getblid() @ "/ores.txt");
                while(!%fw.iseof())
                {
                    %fw.writeLine(%client.itembackupfail[%j]);
                    %j++;
                }
            }
            else
            {
                //%client.chatmessage("\c0During saving your save file was corrupted but it seems your backup file was corrupted too. Sorry for the inconvenience.");
            }
        }
    }
	%fw.openForWrite($directory @ %client.getblid() @ "/ores.txt");
	for(%i = 0; %i < $orecount; %i++)
    {
        if(%client.inventory[getfield($ore[%i],0)] > 0)
            %fw.writeLine(getfield($ore[%i],0) TAB %client.inventory[getfield($ore[%i],0)]);
    }
    for(%i = 0; %i < $advanceditems; %i++)
    {
        if(%client.inventory[getfield($advanceditem[%i],0)] > 0 && getfield($advanceditem[%i],getfieldcount($advanceditem[%i])-1) !$= "TOOL" && getfield($advanceditem[%i],getfieldcount($advanceditem[%i])-1) !$= "ITEM")
            %fw.writeLine(getfield($advanceditem[%i],0) TAB %client.inventory[getfield($advanceditem[%i],0)]);
    }
	%fw.close();
	%fw.delete();
}

function GameConnection::readinventory(%client)
{
    %success = 0;
	%fw = new FileObject();
	%fw.openForRead($directory @ %client.getblid() @ "/ores.txt");
    while(!%fw.iseof())
    {
        if(%fw.readline() !$= "")
        {
            %success = true;
            break;
        }
    }
    if(!%success)
    {
        if(%fw.openForRead($directory @ %client.getblid() @ "/oresBACKUP.txt"))
        {
            //%client.chatmessage("\c0Your save file was corrupted but was restored by a backup file.");
        }
        else
        {
            //%client.chatmessage("\c0Your save file was corrupted but it seems your backup file was corrupted too. Sorry for the inconvenience.");
        }
    }
    else
	    %fw.openForRead($directory @ %client.getblid() @ "/ores.txt");
    if(%client.checksaveversion() $= "NEW")
    {
        for(%i = 0; %i < $orecount; %i++)
        {
            %line = %fw.readline();
            %client.inventory[getfield(%line,0)] = getfield(%line,1);
        }
    }
    else
    {
        for(%i = 0; %i < $orecount; %i++)
            %client.inventory[getfield($ore[%i],0)] = (%fw.readLine());
    }
    %fw.close();
	%fw.delete();
}

function gameconnection::checkSaveVersion(%client)
{
    %fw = new FileObject();
    %fw.openForRead($directory @ %client.getblid() @ "/ores.txt");
    if(getfield(%fw.readline(),1) $= "")
        return "OLD";
    else
        return "NEW";
    %fw.close();
	%fw.delete();
}

function gameconnection::checkPickaxesSaveVersion(%client)
{
    %fw = new FileObject();
    %fw.openForRead($directory @ %client.getblid() @ "/itemsBACKUP.txt");
    if(getfield(%fw.readline(),1) $= "")
        return "OLD";
    else
        return "NEW";
    %fw.close();
	%fw.delete();
}

function GameConnection::updatepickaxes(%client, %backup)
{
    %i = 0;
    %j = 0;
    %fw = new FileObject();
    if(!%backup)
    {
        %fw.openForRead($directory @ %client.getblid() @ "/items.txt");
        if(%fw.readline() !$= "")
        {
            %fw.openForRead($directory @ %client.getblid() @ "/items.txt");
            while(!%fw.iseof())
            {
                %line = %fw.readline();
                %client.pickaxebackup[%i] = %line;
                %i++;
            }
            %fw.openForWrite($directory @ %client.getblid() @ "/itemsBACKUP.txt");
            while(%i > %j)
            {
                %fw.writeline(%client.pickaxebackup[%j]);
                %j++;
            }
        }
        else
        {
            if(%fw.openForRead($directory @ %client.getblid() @ "/itemsBACKUP.txt"))
            {
                //%client.chatmessage("\c0During saving your save file was corrupted but was restored by a backup file.");
                %fw.openForRead($directory @ %client.getblid() @ "/itemsBACKUP.txt");
                while(!%fw.iseof())
                {
                    %line = %fw.readline();
                    %client.backupfail[%i] = %line;
                    %i++;
                }
                %fw.openForWrite($directory @ %client.getblid() @ "/items.txt");
                while(!%fw.iseof())
                {
                    %fw.writeLine(%client.backupfail[%j]);
                    %j++;
                }
            }
            else
            {
                //%client.chatmessage("\c0During saving your save file was corrupted but it seems your backup file was corrupted too. Sorry for the inconvenience.");
            }
        }
    }
	%fw.openForWrite($directory @ %client.getblid() @ "/items.txt");
	for(%i = 0; %i < $craftcount; %i++)
    {
        if(%client.craftedPickaxe[getfield($craft[%i],0)] && getfield($craft[%i],getfieldcount($craft[%i])-2) !$= "COSMETIC")
            %fw.writeLine(getfield($craft[%i],0) TAB %client.craftedPickaxe[getfield($craft[%i],0)]);
    }
    for(%i = 0; %i < $advancedcrafts; %i++)
    {
        if(%client.craftedPickaxe[getfield($advancedcraft[%i],0)] && getfield($advancedcraft[%i],getfieldcount($advancedcraft[%i])-1) !$= "ITEM" && getfield($advancedcraft[%i],getfieldcount($advancedcraft[%i])-1) !$= "MATERIAL")
            %fw.writeLine(getfield($advancedcraft[%i],0) TAB %client.craftedPickaxe[getfield($advancedcraft[%i],0)]);
    }
	%fw.close();
	%fw.delete();
}

function GameConnection::readpickaxes(%client)
{
	%fw = new FileObject();
	%fw.openForRead($directory @ %client.getblid() @ "/items.txt");
    while(!%fw.iseof())
    {
        if(%fw.readline() !$= "")
        {
            %success = true;
            break;
        }
    }
    if(!%success)
    {
        if(%fw.openForRead($directory @ %client.getblid() @ "/itemsBACKUP.txt"))
        {
            //%client.chatmessage("\c0Your save file was corrupted but was restored by a backup file.");
        }
        else
        {
            //%client.chatmessage("\c0Your save file was corrupted but it seems your backup file was corrupted too. Sorry for the inconvenience.");
        }
    }
    else
	    %fw.openForRead($directory @ %client.getblid() @ "/items.txt");
    if(%client.checkPickaxesSaveVersion() $= "NEW")
    {
        while(!%fw.iseof())
        {
            %line = %fw.readline();
            %client.craftedPickaxe[getfield(%line,0)] = getfield(%line,1);
        }
    }
    else
    {
        %client.craftedPickaxe["Stone Pickaxe"] = (%fw.readLine());
        for(%i = 0; %i < $oldcraftamount; %i++)
        {
            %client.craftedPickaxe[$oldcraft[%i]] = %fw.readLine();
        }
    }
    %fw.close();
	%fw.delete();
}

function gameconnection::activateitemsaving(%client)
{
    %client.toolssaveenabled = 1;
}

function GameConnection::updatetools(%client, %backup)
{
    if(%client.minigame < 1 || !%client.toolssaveenabled || %client.cdplayer)
        return; 
    %i = 0;
    %j = 0;
    %g = 0;
    %fw = new FileObject();
    if(!%backup)
    {
        %fw.openForRead($directory @ %client.getblid() @ "/tools.txt");
        if(%fw.readline() !$= "")
        {
            %fw.openForRead($directory @ %client.getblid() @ "/tools.txt");
            while(!%fw.iseof())
            {
                %line = %fw.readline();
                %client.toolsbackup[%i] = %line;
                %i++;
            }
            %fw.openForWrite($directory @ %client.getblid() @ "/toolsBACKUP.txt");
            while(%i > %j)
            {
                %fw.writeline(%client.toolsbackup[%j]);
                %j++;
            }
        }
        else
        {
            if(%fw.openForRead($directory @ %client.getblid() @ "/toolsBACKUP.txt"))
            {
                //%client.chatmessage("\c0During the process of saving your items, the save file was corrupted but was restored by a backup file.");
                %fw.openForRead($directory @ %client.getblid() @ "/toolsBACKUP.txt");
                while(!%fw.iseof())
                {
                    %line = %fw.readline();
                    %client.toolsbackupfail[%i] = %line;
                    %j++;
                }
                %fw.openForWrite($directory @ %client.getblid() @ "/tools.txt");
                while(!%fw.iseof())
                {
                    %fw.writeLine(%client.toolsbackupfail[%j]);
                    %j++;
                }
            }
            else
            {
                //%client.chatmessage("\c0During the process of saving your items, the save file was corrupted but it seems your backup file was corrupted too. Sorry for the inconvenience.");
            }
        }
    }
    %fw.openForWrite($directory @ %client.getblid() @ "/tools.txt");
    while(%client.player.getdatablock().maxtools > %g)
    {
        if(isobject(%client.player.tool[%g]))
        {
            %fw.writeline(%client.player.tool[%g].getname());
            %g++;
        }
        else
            %g++;
    }
    %fw.close();
    %fw.delete();
}

function GameConnection::readtools(%client)
{
    if(isobject(%client.player))
        %client.player.cleartools();
    %fw = new FileObject();
    %fw.openForRead($directory @ %client.getblid() @ "/tools.txt");
    if(%fw.readline() $= "")
    {
        if(%fw.openForRead($directory @ %client.getblid() @ "/toolsBACKUP.txt"))
        {
            //%client.chatmessage("\c0Your items save file was corrupted but was restored by a backup file.");
        }
        else
        {
            //%client.chatmessage("\c0Your items save file was corrupted but it seems your backup file was corrupted too. Sorry for the inconvenience.");
        }
    }
    else
        %fw.openForRead($directory @ %client.getblid() @ "/tools.txt");
    if(isobject(%client.player))
    {
        for(%i=0;%i<%client.player.getdatablock().maxtools;%i++)
        {
            %line = %fw.readline();
            if(%line !$= "" && restwords(%line) !$= "dynamite")
            {
                %client.player.tool[%i] = nameToID(%line);
                messageClient(%client,'MsgItemPickup','',%i,nameToID(%line));
            }
        }
    }
    %fw.close();
    %fw.delete();
    if(!%client.player.tool[0])
    {
        %client.player.tool[0] = nametoid(rpgpickaxeitem);
        messageClient(%client,'MsgItemPickup','',0,nameToID(rpgpickaxeitem));
    }
}

function gameconnection::deletesave(%client)
{
    %fw = new FileObject();
    for(%file = findfirstfile($directory @ %client.getblid() @ "/*.txt"); isfile(%file); %file = findnextfile($directory @ %client.getblid() @ "/*.txt"))
    {
        %fw.openforwrite(%file);
    }
    %fw.close();
    %fw.delete();
    %client.readwholesave();
    %client.exp = 0;
    %client.level = 0;
    %client.money = 0;
    %client.optimaldepth = 100;
    %client.inventoryslots = 5;
    %client.craftedpickaxe["Stone Pickaxe"] = 1;
    %client.spawnplayer();
}

function gameconnection::erasesave(%client, %test)
{
    %fw = new FileObject();
    %fw.openForWrite($directory @ %client.getblid() @ "/save.txt");
    %fw.close();
    %fw.delete();
    for(%i = 0; %i < %client.eventores; %i++)
    {
        %client.eventore[%i] = "";
    }
    %client.eventores = 0;
    %j=0;
    for(%i = 0; %i < $orecount; %i++)
    {
        %ore = $ore[%i];
        if(getfield(%ore,12) $= "EVENT" && %client.inventory[getfield(%ore,0)] > 0)
        {
            %client.eventores++;
            %client.eventOre[%j] = getfield(%ore,0) TAB %client.inventory[getfield(%ore,0)];
            %j++;
        }
    }
    %fw2 = new FileObject();
    %fw2.openForWrite($directory @ %client.getblid() @ "/ores.txt");
    %fw2.close();
    %fw2.delete();
    %fw3 = new FileObject();
    %fw3.openForWrite($directory @ %client.getblid() @ "/items.txt");
    %fw3.close();
    %fw3.delete();
    %fw4 = new FileObject();
    %fw4.openForWrite($directory @ %client.getblid() @ "/tools.txt");
    %fw4.close();
    %fw4.delete();
    %fw5 = new FileObject();
    %fw5.openForWrite($directory @ %client.getblid() @ "/savebackup.txt");
    %fw5.close();
    %fw5.delete();
    %fw6 = new FileObject();
    %fw6.openForWrite($directory @ %client.getblid() @ "/oresbackup.txt");
    %fw6.close();
    %fw6.delete();
    %fw7 = new FileObject();
    %fw7.openForWrite($directory @ %client.getblid() @ "/itemsbackup.txt");
    %fw7.close();
    %fw7.delete();
    %fw8 = new FileObject();
    %fw8.openForWrite($directory @ %client.getblid() @ "/toolsbackup.txt");
    %fw8.close();
    %fw8.delete();
    %fw9 = new FileObject();
    %fw9.openForWrite($directory @ %client.getblid() @ "/misctools.txt");
    %fw9.close();
    %fw9.delete();
    for(%i = 0; %i < $craftcount; %i++)
    {
        if(getfield($craft[%i],4) !$= "COSMETIC")
            %client.craftedpickaxe[getfield($craft[%i],0)] = "";
    }
    if(%test)
    {
        if(%client.level <= 25)
        	%points = mfloatlength(%client.level/1.5 * (mpow(%client.level,1.21)/20),0);
		else if(%client.level <= 30)
			%points = mfloatlength(%client.level/1.35 * (mpow(%client.level,1.26)/19),0);
		else if(%client.level <= 35)
			%points = mfloatlength(%client.level/1.25 * (mpow(%client.level,1.31)/18),0);
		else if(%client.level <= 40)
			%points = mfloatlength(%client.level/1.15 * (mpow(%client.level,1.35)/17),0);
		else if(%client.level <= 45)
			%points = mfloatlength(%client.level/1.125 * (mpow(%client.level,1.39)/16),0);
		else
			%points = mfloatlength(%client.level/1.1 * (mpow(%client.level,1.45)/15),0);
        %points = mfloatlength(%points * (1+%client.prestigepointsbuff),0);
        %client.prestige++;
        %total = %client.prestige;
        if(%total >= 1)
            %client.unlockachievement("all over again");
        if(%total >= 2)
            %client.unlockachievement("demand more content");
        if(%total >= 3)
            %client.unlockachievement("never is enough");
        if(%total >= 5)
            %client.unlockachievement("persistence is key");
        if(%total >= 10)
            %client.unlockachievement("elite digger");
        if(%total >= 25)
            %client.unlockachievement("master digger");
        if(%total >= 50)
            %client.unlockachievement("unstoppable digger");
        if(%total >= 100)
            %client.unlockachievement("holy digger");
		%client.prestigepoints += %points;
        if(%client.respecprestige)
        {
            %client.respecprestige = 0;
            %client.respecprestigepoints();
        }
        %client.updateprestige();
        while(%client.level < %client.prestigestartlevel)
        {
            if(%client.level == 0)
                %req = 100;
            else if(%client.level <= 5)
                %req = mfloatlength(mpow(%client.level * 25,2),0);
            %client.exp = 0;
            %client.addexp(%req,1);
        }
		for(%i = 0; %i < clientgroup.getcount(); %i++)
		{
			%clients = clientgroup.getobject(%i);
            %clients.playsound(beep_checkout_sound);
			if(%clients != %client)
			{
				%clients.chatmessage("\c4" @ %client.getsimplename() SPC "\c2has reached Prestige\c5" SPC numbersToLatin(%client.prestige) @ "\c2!");
			}
		}
    }
    %client.readwholesave();
    %client.exp = 0;
    %client.level = 0;
    %client.money = 0;
    %client.optimaldepth = 100;
    %client.inventoryslots = 5;
    %client.craftedpickaxe["Stone Pickaxe"] = 1;
    %client.spawnplayer();
    if(%client.prestigestartdepth == 1)
        %client.optimaldepth = 200;
    else if(%client.prestigestartdepth == 2)
        %client.optimaldepth = 300;
    else if(%client.prestigestartdepth == 3)
        %client.optimaldepth = 400;
    else if(%client.prestigestartdepth == 4)
        %client.optimaldepth = 600;
    while(%client.level < %client.prestigestartlevel)
    {
        %client.level++;
        %miningmult = %client.level*0.1;
        if(%miningmult > 2 + %client.prestigemaxmining)
            %miningmult = 2 + %client.prestigemaxmining;
        %client.miningmultiplier += %miningmult;
    }
    for(%i = 0; %i < %client.eventores; %i++)
    {
        %client.inventory[getfield(%client.eventore[%i],0)] = getfield(%client.eventore[%i],1);
    }
}

function remainingcolors()
{
    for(%i = 0; %i < $orecount; %i++)
    {
        //$coloroccupied[%i] = 0;
    }
    for(%i = 0; %i < $orecount; %i++)
    {
        $coloroccupied[getfield($ore[%i],5)] = 1;
    }
    for(%i = 0; %i < 63; %i++)
    {
        if(!$coloroccupied[%i])
            announce(%i);
    }
}

function gameconnection::readwholesave(%client)
{
    %client.readsavefile();
    %client.readinventory();
    %client.readpickaxes();
    %client.readtools();
    //%client.readmisctools();
    %client.readcosmetictools();
    %client.readequippedcosmetictools();
    %client.readprestige();
    %client.readchangeloginfo();
    %client.readtotalstats();
    %client.readachievementsfile();
    %client.readachievementstatsfile();
}

function GameConnection::updateprestige(%client, %backup)
{
    %i = 0;
    %j = 0;
    %fw = new FileObject();
    if(!%backup)
    {
        %fw.openForRead($directory @ %client.getblid() @ "/prestige.txt");
        if(%fw.readline() !$= "")
        {
            %fw.openForRead($directory @ %client.getblid() @ "/prestige.txt");
            while(!%fw.iseof())
            {
                %line = %fw.readline();
                %client.prestigebackup[%i] = %line;
                %i++;
            }
            %fw.openForWrite($directory @ %client.getblid() @ "/prestigeBACKUP.txt");
            while(%i > %j)
            {
                %fw.writeline(%client.prestigebackup[%j]);
                %j++;
            }
        }
        else
        {
            if(%fw.openForRead($directory @ %client.getblid() @ "/prestigeBACKUP.txt"))
            {
                //%client.chatmessage("\c0During saving your save file was corrupted but was restored by a backup file.");
                %fw.openForRead($directory @ %client.getblid() @ "/prestigeBACKUP.txt");
                while(!%fw.iseof())
                {
                    %line = %fw.readline();
                    %client.prestigebackupfail[%i] = %line;
                    %i++;
                }
                %fw.openForWrite($directory @ %client.getblid() @ "/prestige.txt");
                while(!%fw.iseof())
                {
                    %fw.writeLine(%client.prestigebackupfail[%j]);
                    %j++;
                }
            }
            else
            {
                //%client.chatmessage("\c0During saving your save file was corrupted but it seems your backup file was corrupted too. Sorry for the inconvenience.");
            }
        }
    }
	%fw.openForWrite($directory @ %client.getblid() @ "/prestige.txt");
	%fw.writeLine(%client.prestige);
    %fw.writeLine(%client.prestigepoints);
    %fw.writeLine(%client.prestigeminingpower);
    %fw.writeLine(%client.prestigeminingmultiplier);
    %fw.writeLine(%client.prestigeexpbonus);
    %fw.writeLine(%client.prestigecashbonus);
    %fw.writeLine(%client.prestigecratedrops);
    %fw.writeLine(%client.prestigestartlevel);
    %fw.writeLine(%client.prestigemaxmining);
    %fw.writeLine(%client.prestigepointsbuff);
    %fw.writeLine(%client.tunnelerlavares);
    %fw.writeLine(%client.respecprestige);
    %fw.writeLine(%client.tunnelercooldown);
    %fw.writeLine(%client.prestigestartdepth);
	%fw.close();
	%fw.delete();
}

function GameConnection::readprestige(%client)
{
	%fw = new FileObject();
	%fw.openForRead($directory @ %client.getblid() @ "/prestige.txt");
    if(%fw.readline() $= "")
    {
        if(%fw.openForRead($directory @ %client.getblid() @ "/prestigeBACKUP.txt"))
        {
            //%client.chatmessage("\c0Your save file was corrupted but was restored by a backup file.");
        }
        else
        {
            //%client.chatmessage("\c0Your save file was corrupted but it seems your backup file was corrupted too. Sorry for the inconvenience.");
        }
    }
    else
	    %fw.openForRead($directory @ %client.getblid() @ "/prestige.txt");
	%client.prestige = (%fw.readLine());
    %client.prestigepoints = (%fw.readLine());
    %client.prestigeminingpower = (%fw.readLine());
    %client.prestigeminingmultiplier = (%fw.readLine());
    %client.prestigeexpbonus = (%fw.readLine());
    %client.prestigecashbonus = (%fw.readLine());
    %client.prestigecratedrops = (%fw.readLine());
    %client.prestigestartlevel = (%fw.readLine());
    %client.prestigemaxmining = (%fw.readLine());
    %client.prestigepointsbuff = (%fw.readLine());
    %client.tunnelerlavares = (%fw.readLine());
    %client.respecprestige = (%fw.readLine());
    %client.tunnelercooldown = (%fw.readLine());
    %client.prestigestartdepth = (%fw.readLine());
	%fw.close();
	%fw.delete();
}

function GameConnection::updatemisctools(%client)
{
    %fw = new FileObject();
    %fw.openForWrite($directory @ %client.getblid() @ "/misctools.txt");
    %fw.writeLine(%client.craftedPickaxe["Placement Tool"]);
    %fw.writeLine(%client.craftedPickaxe["Tunneler"]);
    %fw.writeLine(%client.craftedPickaxe["Mining Helmet"]);
    %fw.writeLine(%client.craftedPickaxe["Cryogenum Tank"]);
    %fw.writeLine(%client.craftedPickaxe["Geolocator"]);
    %fw.writeLine(%client.craftedPickaxe["Laser Drill"]);
    %fw.writeLine(%client.craftedPickaxe["Pumpkin Launcher"]);
    %fw.writeLine(%client.craftedPickaxe["Flak Vest"]);
    %fw.close();
	%fw.delete();
}

function GameConnection::readmisctools(%client)
{
    return;
    %fw = new FileObject();
	%fw.openForRead($directory @ %client.getblid() @ "/misctools.txt");
    %client.craftedPickaxe["Placement Tool"] = (%fw.readLine());
    %client.craftedPickaxe["Tunneler"] = (%fw.readLine());
    %client.craftedPickaxe["Mining Helmet"] = (%fw.readLine());
    %client.craftedPickaxe["Cryogenum Tank"] = (%fw.readLine());
    %client.craftedPickaxe["Geolocator"] = (%fw.readLine());
    %client.craftedPickaxe["Laser Drill"] = (%fw.readLine());
    %client.craftedPickaxe["TNT Launcher"] = (%fw.readLine());
    %client.craftedPickaxe["Flak Vest"] = (%fw.readLine());
    %fw.close();
	%fw.delete();
}

function GameConnection::updatechangeloginfo(%client)
{
    %fw = new FileObject();
    %fw.openForWrite($directory @ %client.getblid() @ "/changeloginfo.txt");
    %fw.writeLine(%client.changeloginfo);
    %fw.close();
	%fw.delete();
}

function GameConnection::readchangeloginfo(%client)
{
    %fw = new FileObject();
	%fw.openForRead($directory @ %client.getblid() @ "/changeloginfo.txt");
    %client.changeloginfo = (%fw.readLine());
    %fw.close();
	%fw.delete();
}

function GameConnection::updateglobalfile(%client)
{
    %a = 0;
    %fw = new FileObject();
    %fw.openForRead("config/server/mining/leaderboard.txt");
    while(!%fw.iseof())
    {
        %line[%a] = %fw.readline();
        if(getfield(%line[%a],1) == %client.getblid())
            %hasBLID = 1;
        %a++;
    }
    %fw.openForWrite("config/server/mining/leaderboard.txt");
    for(%i = 0; %i < %a; %i++)
    {
        if(getfield(%line[%i],1) == %client.getblid())
            %fw.writeLine(%client.name TAB %client.getblid() TAB mceil(%client.prestige) TAB %client.level TAB %client.respecprestigepoints(1)+%client.prestigepoints);
        else
            %fw.writeLine(%line[%i]);
    }
    if(!%hasBLID)
        %fw.writeLine(%client.name TAB %client.getblid() TAB mceil(%client.prestige) TAB %client.level TAB %client.respecprestigepoints(1)+%client.prestigepoints);
    %fw.close();
	%fw.delete();
}

function gameconnection::updatetotalstats(%client)
{
    %fw = new FileObject();
    %fw.openForWrite($directory @ %client.getblid() @ "/totalstats.txt");
    %fw.writeLine(%client.totalexp);
    %fw.writeLine(%client.totalexpthisprestige);
    %fw.writeLine(%client.totalmoney);
    %fw.writeLine(%client.totalmoneythisprestige);
    %fw.writeLine(%client.totalbricks);
    %fw.writeLine(%client.totalbrickdamage);
    %fw.writeLine(%client.totaltimespent);
    %fw.writeLine(%client.openedcrates);
    %fw.writeLine(%client.openedvaults);
    %fw.close();
	%fw.delete();
}

function GameConnection::readtotalstats(%client)
{
    %fw = new FileObject();
	%fw.openForRead($directory @ %client.getblid() @ "/totalstats.txt");
    %client.totalexp = (%fw.readLine());
    %client.totalexpthisprestige = (%fw.readLine());
    %client.totalmoney = (%fw.readLine());
    %client.totalmoneythisprestige = (%fw.readLine());
    %client.totalbricks = (%fw.readLine());
    %client.totalbrickdamage = (%fw.readLine());
    %client.totaltimespent = (%fw.readLine());
    %client.openedcrates = (%fw.readLine());
    %client.openedvaults = (%fw.readLine());
    %fw.close();
	%fw.delete();
}

function GameConnection::updatecosmetictools(%client)
{
    %fw = new FileObject();
    %fw.openForWrite($directory @ %client.getblid() @ "/cosmetictools.txt");
    %fw.writeLine(%client.craftedCosmetic["Thermal Drill"]);
    %fw.writeLine(%client.craftedCosmetic["Pumpkin Launcher"]);
    %fw.writeLine(%client.craftedCosmetic["Lantern"]);
    %fw.writeLine(%client.craftedCosmetic["Ghosthammer"]);
    %fw.writeLine(%client.craftedCosmetic["Halloweenium Tank"]);
    %fw.writeLine(%client.craftedCosmetic["Candy Drill"]);
    %fw.writeLine(%client.craftedCosmetic["Gift Launcher"]);
    %fw.writeLine(%client.craftedCosmetic["Candle"]);
    %fw.writeLine(%client.craftedCosmetic["Icebreaker"]);
    %fw.writeLine(%client.craftedCosmetic["Festive Tunneler"]);
    %fw.writeLine(%client.craftedCosmetic["Snowglobe"]);
    %fw.writeLine(%client.craftedCosmetic["Hypervest"]);
    %fw.close();
	%fw.delete();
}

function GameConnection::readcosmetictools(%client)
{
    %fw = new FileObject();
	%fw.openForRead($directory @ %client.getblid() @ "/cosmetictools.txt");
    %client.craftedCosmetic["Thermal Drill"] = (%fw.readLine());
    %client.craftedCosmetic["Pumpkin Launcher"] = (%fw.readLine());
    %client.craftedCosmetic["Lantern"] = (%fw.readLine());
    %client.craftedCosmetic["Ghosthammer"] = (%fw.readLine());
    %client.craftedCosmetic["Halloweenium Tank"] = (%fw.readLine());
    %client.craftedCosmetic["Candy Drill"] = (%fw.readLine());
    %client.craftedCosmetic["Gift Launcher"] = (%fw.readLine());
    %client.craftedCosmetic["Candle"] = (%fw.readLine());
    %client.craftedCosmetic["Icebreaker"] = (%fw.readLine());
    %client.craftedCosmetic["Festive Tunneler"] = (%fw.readLine());
    %client.craftedCosmetic["Snowglobe"] = (%fw.readLine());
    %client.craftedCosmetic["Hypervest"] = (%fw.readLine());
    %fw.close();
	%fw.delete();
}

function GameConnection::updateequippedcosmetictools(%client)
{
    %fw = new FileObject();
    %fw.openForWrite($directory @ %client.getblid() @ "/equippedcosmetictools.txt");
    %fw.writeLine(%client.equippedCosmetic["Thermal Drill"]);
    %fw.writeLine(%client.equippedCosmetic["Pumpkin Launcher"]);
    %fw.writeLine(%client.equippedCosmetic["Lantern"]);
    %fw.writeLine(%client.equippedCosmetic["Ghosthammer"]);
    %fw.writeLine(%client.equippedCosmetic["Halloweenium Tank"]);
    %fw.writeLine(%client.equippedCosmetic["Candy Drill"]);
    %fw.writeLine(%client.equippedCosmetic["Gift Launcher"]);
    %fw.writeLine(%client.equippedCosmetic["Candle"]);
    %fw.writeLine(%client.equippedCosmetic["Icebreaker"]);
    %fw.writeLine(%client.equippedCosmetic["Festive Tunneler"]);
    %fw.writeLine(%client.equippedCosmetic["Snowglobe"]);
    %fw.writeLine(%client.equippedCosmetic["Hypervest"]);
    %fw.close();
	%fw.delete();
}

function GameConnection::readequippedcosmetictools(%client)
{
    %fw = new FileObject();
	%fw.openForRead($directory @ %client.getblid() @ "/equippedcosmetictools.txt");
    %client.equippedCosmetic["Thermal Drill"] = (%fw.readLine());
    %client.equippedCosmetic["Pumpkin Launcher"] = (%fw.readLine());
    %client.equippedCosmetic["Lantern"] = (%fw.readLine());
    %client.equippedCosmetic["Ghosthammer"] = (%fw.readLine());
    %client.equippedCosmetic["Halloweenium Tank"] = (%fw.readLine());
    %client.equippedCosmetic["Candy Drill"] = (%fw.readLine());
    %client.equippedCosmetic["Gift Launcher"] = (%fw.readLine());
    %client.equippedCosmetic["Candle"] = (%fw.readLine());
    %client.equippedCosmetic["Icebreaker"] = (%fw.readLine());
    %client.equippedCosmetic["Festive Tunneler"] = (%fw.readLine());
    %client.equippedCosmetic["Snowglobe"] = (%fw.readLine());
    %client.equippedCosmetic["Hypervest"] = (%fw.readLine());
    %fw.close();
	%fw.delete();
}

function GameConnection::updateachievementsfile(%client)
{
	%fw = new FileObject();
	%fw.openForWrite($directory @ %client.getblid() @ "/achievements.txt");
    %fw.writeLine(%client.achievementsunlocked);
    for(%i = 0; %i < $achievementcount; %i++)
    {
        if(%client.achievementunlocked[getfield($achievement[%i],0)])
            %fw.writeLine(getfield($achievement[%i],0));
    }
	%fw.close();
	%fw.delete();
}

function GameConnection::readachievementsfile(%client)
{
	%fw = new FileObject();
	%fw.openForRead($directory @ %client.getblid() @ "/achievements.txt");
    %client.achievementsunlocked = (%fw.readLine());
    for(%i = 0; %i < $achievementcount; %i++)
    {
        %line = %fw.readLine();
        %client.achievementunlocked[getfield(%line,0)] = 1;
    }
	%fw.close();
	%fw.delete();
}

function GameConnection::updateachievementstatsfile(%client)
{
	%fw = new FileObject();
	%fw.openForWrite($directory @ %client.getblid() @ "/achievementStats.txt");
    %fw.writeLine(%client.achievementminingpower);
    %fw.writeLine(%client.achievementminingmultiplier);
    %fw.writeLine(%client.achievementexpbonus);
    %fw.writeLine(%client.achievementcashbonus);
    %fw.writeLine(%client.achievementcratedrops);
    %fw.writeLine(%client.talkedto[0]);
    %fw.writeLine(%client.talkedto[1]);
    %fw.writeLine(%client.talkedto[2]);
    %fw.writeLine(%client.talkedto[3]);
    %fw.writeLine(%client.talkedto[4]);
	%fw.close();
	%fw.delete();
}

function GameConnection::readachievementstatsfile(%client)
{
	%fw = new FileObject();
	%fw.openForRead($directory @ %client.getblid() @ "/achievementStats.txt");
    %client.achievementminingpower = (%fw.readLine());
    %client.achievementminingmultiplier = (%fw.readLine());
    %client.achievementexpbonus = (%fw.readLine());
    %client.achievementcashbonus = (%fw.readLine());
    %client.achievementcratedrops = (%fw.readLine());
    %client.talkedto[0] = (%fw.readLine());
    %client.talkedto[1] = (%fw.readLine());
    %client.talkedto[2] = (%fw.readLine());
    %client.talkedto[3] = (%fw.readLine());
    %client.talkedto[4] = (%fw.readLine());
	%fw.close();
	%fw.delete();
}