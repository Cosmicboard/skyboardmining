function GameConnection::promptClient(%this,%type,%msg,%nums,%cl,%type)
{
	if(%this.pendingprompt)
		return;
	if(!isObject(%cl.promptGroup))
		%cl.promptGroup = new SimGroup();
	
	%obj = new SimObject()
	{
		cmd  = (%type) ? 'MessageBoxYesNo' : 'MessageBoxOKCancel';
		msg  = %msg;
		obj  = %this;
		nums = %nums;
	};
	
	%cl.promptGroup.add(%obj);
	%cl.triggerPrompt(%type);
}

function GameConnection::triggerPrompt(%this,%type)
{
	%group = %this.promptGroup;
	
	if(%this.pendingPrompt || !isObject(%group) || !%group.getCount())
		return;
	
	%obj = %group.getObject(0);
	if(%type == 0)
	{
		%this.type = 0;
		commandToClient(%this,%obj.cmd,"Craft this Item",%obj.msg,'MessageBoxAccept');
	}
    else if(%type == 1)
	{
		%this.type = 1;
		commandToClient(%this,%obj.cmd,"Switch Pickaxe",%obj.msg,'MessageBoxAccept');
	}
	else if(%type == 2)
	{
		%this.type = 2;
		commandToClient(%this,%obj.cmd,"Upgrade Depth",%obj.msg,'MessageBoxAccept');
	}
	else if(%type == 3)
	{
		%this.type = 3;
		commandToClient(%this,%obj.cmd,"Upgrade Inventory",%obj.msg,'MessageBoxAccept');
	}
	else if(%type == 4)
	{
		%this.type = 4;
		commandToClient(%this,%obj.cmd,"Prestige Now?",%obj.msg,'MessageBoxAccept');
	}
	else if(%type == 5)
	{
		%this.type = 5;
		commandToClient(%this,%obj.cmd,"Craft this Item",%obj.msg,'MessageBoxAccept');
	}
	%this.pendingPrompt = 1;
}

function GameConnection::processPrompt(%this,%mode)
{
	%group = %this.promptGroup;
	
	if(!%this.pendingPrompt || !isObject(%group) || !%group.getCount())
		return;
	
	%obj = %group.getObject(0);
	%group.remove(%obj);
	
	%brick = %obj.obj;
	%max   = %brick.numEvents;
	%list  = " " @ parseList(%obj.nums,0,%max - 1) @ " ";
	
	for(%i = 0; %i < %max; %i++)
	{
		%input   = %brick.eventInput[%i];
		%old[%i] = %brick.eventEnabled[%i];
		
		if((%input $= "onPromptAccept" || %input $= "onPromptDecline") && strPos(%list," " @ %i @ " ") != -1)
			%brick.eventEnabled[%i] = 1;
		else
			%brick.eventEnabled[%i] = 0;
	}

    if(%mode == 1)
	{
		%selected = %this.player.lastShopItemSelected;
		for(%i = 0; %i < $craftCount; %i++)
		{
			if(%selected.getdatablock().uiname $= getfield($craft[%i],0))
			{
				%item = $craft[%i];
				break;
			}
		}
        if(%this.level < getfield(%item,1))
		{
            %this.chatmessage("you do not meet the level requirement of" SPC getfield(%item,1) SPC "to craft this item");
            %this.playsound(errorsound);
            return;
        }
		if(%this.money < getfield(%item,2))
		{
            %this.chatmessage("you do not meet the cash requirement of" SPC getfield(%item,2) @ "$ to craft this item");
            %this.playsound(errorsound);
            return;
        }
        %mats = getfields(%item,3,getfieldcount(%item));
		%matscount = getfieldcount(%item)-4;
        %materialcount = 0;
        for(%i = 0; %i < %matscount; %i++)
        {
            if(%this.inventory[restwords(getfield(%mats,%i))] >= firstword(getfield(%item,%i+3)))
            {
                %material[%i] = restwords(getfield(%mats,%i));
				%materialamount[%i] = firstword(getfield(%mats,%i));
            }
            else
            {
                %fail = true;
                %nomaterial[%materialcount] = firstword(getfield(%item,%i+3)) @ "x" SPC restwords(getfield(%mats,%i));
                %materialcount++;
            }
        }
        if(!%fail)
        {
            if(getsubstr(getfield(%item,0),0,1) $= "a" || getsubstr(getfield(%item,0),0,1) $= "e" || getsubstr(getfield(%item,0),0,1) $= "u" || getsubstr(getfield(%item,0),0,1) $= "i" || getsubstr(getfield(%item,0),0,1) $= "o")
                %craftedItem = "an" SPC getfield(%item,0);
            else
                %craftedItem = "a" SPC getfield(%item,0);
			if(getfield(%item,getfieldcount(%item)-1) !$= "ITEM" && getfield(%item,getfieldcount(%item)-2) !$= "COSMETIC")
			{
				%this.playsound(beep_key_sound);
				%this.craftedPickaxe[getfield(%item,0)] = 1;
				%this.chatmessage("\c2Crafted" SPC %crafteditem @ "! You can now switch to this tool whenever you want by clicking on it in the shop.");
				if(%this.craftedPickaxe["Reality Pickaxe"] == 1)
					%this.unlockachievement("get real");
				for(%i = 0; %i < $craftcount; %i++)
				{
					%craft = $craft[%i];
					if(getfield(%craft,getfieldcount(%craft)-1) !$= "ITEM" && getfield(%craft,getfieldcount(%craft)-2) !$= "COSMETIC")
					{
						if(%this.craftedpickaxe[getfield(%craft,0)] == 0)
						{
							%disableAchievement = 1;
							break;
						}
					}
				}
				if(!%disableAchievement)
					%this.unlockachievement("kitted out");
			}
			else if(getfield(%item,getfieldcount(%item)-1) $= "ITEM")
			{
				%invspace = 0;
				for(%i = 0; %i < %this.player.getdatablock().maxtools; %i++)
				{
					if(%this.player.tool[%i] == 0)
					{
						%invspace = 1;
						%tool = %i;
						break;
					}
				}
				if(%invspace)
				{
					%this.playsound(beep_key_sound);
					%this.player.tool[%tool] = uinametoid(getfield(%item,0));
					messageClient(%this,'MsgItemPickup',"",%tool,uinametoid(getfield(%item,0)));
					%this.chatmessage("\c2Crafted" SPC %crafteditem @ "!");
				}
				else
				{
					%this.chatmessage("your inventory is full");
            		%this.playsound(errorsound);
					return;
				}
			}
			else if(getfield(%item,getfieldcount(%item)-2) $= "COSMETIC")
			{
				%this.playsound(beep_key_sound);
				%this.craftedCosmetic[getfield(%item,0)] = 1;
				%this.chatmessage("\c2Crafted" SPC %crafteditem @ "! You can now switch to this cosmetic by typing /cosmetic equip" SPC getfield(%item,0) @ ".");
			}
			for(%i = 0; %i < %matscount; %i++)
			{
				%this.inventory[%material[%i]]-=%materialamount[%i];
			}
			%this.money-=getfield(%item,2);
        }
        else if(%fail)
        {
            %this.chatmessage("you do not have the following items to perform this craft");
            %this.playsound(errorsound);
            for(%i = 0; %i < %materialcount; %i++)
            {
				%has = %this.inventory[restwords(%nomaterial[%i])];
                %this.chatmessage("Requires:" SPC %nomaterial[%i] SPC "(you have:" SPC mfloor(%has) @ "x)");
            }
            return;
        }
    }
    else if(%mode == 2)
	{
		if(getfield($craft[craftidfromname(%this.player.lastShopItemSelected.getname())], getfieldcount($craft[craftidfromname(%this.player.lastShopItemSelected.getname())])-2) $= "COSMETIC")
			return;
		if(!%this.player.lastShopItemSelected.image.undroppable && %this.chosenitem $= "")
		{
			if(%this.minigame)
			{
				%this.player.tool[0] = nametoid(%this.player.lastShopItemSelected);
				messageClient(%this,'MsgItemPickup',"",0,nametoid(%this.player.lastShopItemSelected));
			}
			else
			{
				%this.player.tool[3] = nametoid(%this.player.lastShopItemSelected);
				messageClient(%this,'MsgItemPickup',"",3,nametoid(%this.player.lastShopItemSelected));
			}
		}
		else
		{
			%this.updatetools();
			%invspace = 0;
			for(%i = 0; %i < %this.player.getdatablock().maxtools; %i++)
			{
				if(%this.player.tool[%i] == 0)
				{
					%invspace = 1;
					%tool = %i;
					break;
				}
			}
			if(%invspace)
			{
				for(%i = 0; %i < %this.player.getdatablock().maxtools; %i++)
				{
					if(%this.chosenItem $= "")
					{
						%originaltool = nametoid(%this.player.lastShopItemSelected);
						%equiptool = nametoid(%this.player.lastShopItemSelected);
						for(%g = 0; %g < $craftcount; %g++)
						{
							%craft = $craft[%g];
							if(getfield(%craft,getfieldcount(%craft)-1) $= %equiptool.uiname && %this.equippedcosmetic[getfield(%craft,0)])
							{
								%cosmetic = %craft;
								break;
							}
						}
						if(%equiptool.uiname $= uinametoid(getfield(%cosmetic,getfieldcount(%cosmetic)-1)).uiname && %this.equippedcosmetic[uinametoid(getfield(%cosmetic,0)).uiname])
							%equiptool = uinametoid(getfield(%cosmetic,0));
					}
					else
					{
						for(%g = 0; %g < $advancedCrafts; %g++)
						{
							if(getfield($advancedCraft[%g],0) $= %this.chosenItem)
							{
								%equiptool = uinametoid(getfield($advancedCraft[%g],0));
								break;
							}
						}
					}
					if(%this.player.tool[%g] == %equiptool)
					{
						%this.chatmessage("you already have this equipped");
            			%this.playsound(errorsound);
						return;
					}
				}
				%this.playsound(beep_key_sound);
				if(%this.chosenItem $= "")
				{
					%equiptool = nametoid(%this.player.lastShopItemSelected);
					for(%i = 0; %i < $craftcount; %i++)
					{
						%craft = $craft[%i];
						if(getfield(%craft,getfieldcount(%craft)-1) $= %equiptool.uiname && %this.equippedcosmetic[getfield(%craft,0)])
						{
							%cosmetic = %craft;
							break;
						}
					}
					if(%equiptool.uiname $= uinametoid(getfield(%cosmetic,getfieldcount(%cosmetic)-1)).uiname && %this.equippedcosmetic[uinametoid(getfield(%cosmetic,0)).uiname])
						%equiptool = uinametoid(getfield(%cosmetic,0));
				}
				%this.player.tool[%tool] = %equiptool;
				messageClient(%this,'MsgItemPickup',"",%tool,%equiptool);
			}
			else
			{
				%this.chatmessage("your inventory is full");
            	%this.playsound(errorsound);
				return;
			}
		}
		if(%this.chosenItem $= "")
        	%this.chatmessage("\c2Equipped" SPC %this.player.lastShopItemSelected.uiname @ "!");
		else
			%this.chatmessage("\c2Equipped" SPC %this.chosenItem @ "!");
    }
	else if(%mode == 3)
	{
		if(%this.optimaldepth == 100)
			%item = $depthpurchase[0];
		else if(%this.optimaldepth == 200)
			%item = $depthpurchase[1];
		else if(%this.optimaldepth == 300)
			%item = $depthpurchase[2];
		else if(%this.optimaldepth == 400)
			%item = $depthpurchase[3];
		else if(%this.optimaldepth == 600)
			%item = $depthpurchase[4];
		else if(%this.optimaldepth == 750)
			%item = $depthpurchase[5];
		else if(%this.optimaldepth == 1000)
			%item = $depthpurchase[6];
		else if(%this.optimaldepth == 1250)
			%item = $depthpurchase[7];
		else if(%this.optimaldepth == 1500)
			%item = $depthpurchase[8];
		else if(%this.optimaldepth == 2000)
			%item = $depthpurchase[9];
		else if(%this.optimaldepth == 2500)
			%item = $depthpurchase[10];
		else if(%this.optimaldepth == 3000)
			%item = $depthpurchase[11];
		else if(%this.optimaldepth == 3500)
			%item = $depthpurchase[12];
		else if(%this.optimaldepth == 4000)
			%item = $depthpurchase[13];
		else if(%this.optimaldepth == 4500)
			%item = $depthpurchase[14];
		else if(%this.optimaldepth == 5000)
			%item = $depthpurchase[15];
		else if(%this.optimaldepth == 5500)
			%item = $depthpurchase[16];
		else if(%this.optimaldepth == 6000)
			%item = $depthpurchase[17];
		if(%this.money < getfield(%item,1))
		{
            %this.chatmessage("you do not meet the cash requirement of" SPC getfield(%item,1) @ "$ to purchase this");
            %this.playsound(errorsound);
            return;
        }
        %mats = getfields(%item,2,getfieldcount(%item));
        %matscount = getfieldcount(%item)-2;
        %materialcount = 0;
        for(%i = 0; %i < %matscount; %i++)
        {
            if(%this.inventory[restwords(getfield(%mats,%i))] >= firstword(getfield(%item,%i+2)))
            {
                %material[%i] = restwords(getfield(%mats,%i));
				%materialamount[%i] = firstword(getfield(%mats,%i));
            }
            else
            {
                %fail = true;
                %nomaterial[%materialcount] = firstword(getfield(%item,%i+2)) @ "x" SPC restwords(getfield(%mats,%i));
                %materialcount++;
            }
        }
        if(!%fail)
        {
			%this.optimaldepth = getfield(%item,0);
            %this.chatmessage("\c2Increased maximum optimal depth to" SPC %this.optimaldepth @ "m! You can now go deeper into the mines.");
			%this.playsound(beep_key_sound);
			for(%i = 0; %i < %matscount; %i++)
			{
				%this.inventory[%material[%i]]-=%materialamount[%i];
			}
			%this.money-=getfield(%item,1);
        }
        else if(%fail)
        {
            %this.chatmessage("you do not have the following items to perform this purchase");
            %this.playsound(errorsound);
            for(%i = 0; %i < %materialcount; %i++)
            {
				%has = %this.inventory[restwords(%nomaterial[%i])];
                %this.chatmessage("Requires:" SPC %nomaterial[%i] SPC "(you have:" SPC mfloor(%has) @ "x)");
            }
            return;
        }
	}
	else if(%mode == 4)
	{
		%purchase = %this.inventoryslots-5;
		if(%purchase $= "")
			%purchase = 0;
		%item = $inventorypurchase[%purchase];
		if(%this.money < getfield(%item,1))
		{
            %this.chatmessage("you do not meet the cash requirement of" SPC getfield(%item,1) @ "$ to purchase this");
            %this.playsound(errorsound);
            return;
        }
        %mats = getfields(%item,2,getfieldcount(%item));
        %matscount = getfieldcount(%item)-2;
        %materialcount = 0;
        for(%i = 0; %i < %matscount; %i++)
        {
            if(%this.inventory[restwords(getfield(%mats,%i))] >= firstword(getfield(%item,%i+2)))
            {
                %material[%i] = restwords(getfield(%mats,%i));
				%materialamount[%i] = firstword(getfield(%mats,%i));
            }
            else
            {
                %fail = true;
                %nomaterial[%materialcount] = firstword(getfield(%item,%i+2)) @ "x" SPC restwords(getfield(%mats,%i));
                %materialcount++;
            }
        }
        if(!%fail)
        {
			%this.inventoryslots = getfield(%item,0);
            %this.chatmessage("\c2Increased maximum inventory capacity to" SPC %this.inventoryslots @ "!");
			%this.playsound(beep_key_sound);
            %this.player.setdatablock("player" @ %this.inventoryslots @ "slot");
			for(%i = 0; %i < %matscount; %i++)
			{
				%this.inventory[%material[%i]]-=%materialamount[%i];
			}
			%this.money-=getfield(%item,1);
        }
        else if(%fail)
        {
            %this.chatmessage("you do not have the following items to perform this purchase");
            %this.playsound(errorsound);
            for(%i = 0; %i < %materialcount; %i++)
            {
				%has = %this.inventory[restwords(%nomaterial[%i])];
                %this.chatmessage("Requires:" SPC %nomaterial[%i] SPC "(you have:" SPC mfloor(%has) @ "x)");
            }
            return;
        }
	}
	else if(%mode == 5)
	{
		%client = %this;
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
		%this.chatmessage("\c5Successfully prestiged! You have received\c2" SPC %points SPC "\c5prestige points.");
		%this.chatmessage("\c4You can now type \c3/prestigeshop\c4 to go and spend your prestige points.");
		%this.erasesave(1);
	}
	else if(%mode == 6)
	{
		%selected = %this.chosenItem;
        for(%i = 0; %i < $advancedCrafts; %i++)
        {
            if(%selected $= getfield($advancedCraft[%i],0))
            {
                %item = $advancedCraft[%i];
                break;
            }
        }
        %mats = getfields(%item,1,getfieldcount(%item));
		%matscount = getfieldcount(%item)-1;
        %materialcount = 0;
        for(%i = 0; %i < %matscount; %i++)
        {
            if(%this.inventory[restwords(getfield(%mats,%i))] >= firstword(getfield(%item,%i+1)))
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
            if(getsubstr(getfield(%item,0),0,1) $= "a" || getsubstr(getfield(%item,0),0,1) $= "e" || getsubstr(getfield(%item,0),0,1) $= "u" || getsubstr(getfield(%item,0),0,1) $= "i" || getsubstr(getfield(%item,0),0,1) $= "o")
                %craftedItem = "an" SPC getfield(%item,0);
            else
                %craftedItem = "a" SPC getfield(%item,0);
			if(getfield(%item,getfieldcount(%item)-1) !$= "ITEM" && getfield(%item,getfieldcount(%item)-1) !$= "MATERIAL")
			{
				%this.playsound(beep_key_sound);
				%this.craftedPickaxe[getfield(%item,0)] = 1;
				%this.chatmessage("\c2Crafted" SPC %crafteditem @ "! You can now equip this tool whenever you want by selecting it in the workbench.");
			}
			else if(getfield(%item,getfieldcount(%item)-1) $= "ITEM")
			{
				%invspace = 0;
				for(%i = 0; %i <= %this.player.getdatablock().maxtools; %i++)
				{
					if(%this.player.tool[%i] == 0)
					{
						%invspace = 1;
						%tool = %i;
						break;
					}
				}
				if(%invspace)
				{
					%this.playsound(beep_key_sound);
					%this.player.tool[%tool] = uinametoid(getfield(%item,0));
					messageClient(%this,'MsgItemPickup',"",%tool,uinametoid(getfield(%item,0)));
					%this.chatmessage("\c2Crafted" SPC %crafteditem @ "!");
				}
				else
				{
					%this.chatmessage("your inventory is full");
            		%this.playsound(errorsound);
					return;
				}
			}
			else if(getfield(%item,getfieldcount(%item)-1) $= "MATERIAL")
			{
				%this.playsound(beep_key_sound);
				%this.inventory[getfield(%item,0)] = 1;
				%this.chatmessage("\c2Crafted" SPC %crafteditem @ "!");
			}
			for(%i = 0; %i < %matscount; %i++)
			{
				%this.inventory[%material[%i]]-=%materialamount[%i];
			}
			%this.money-=getfield(%item,2);
        }
        else if(%fail)
        {
            %this.chatmessage("you do not have the following items to perform this craft");
            %this.playsound(errorsound);
            for(%i = 0; %i < %materialcount; %i++)
            {
				%has = %this.inventory[restwords(%nomaterial[%i])];
                %this.chatmessage("Requires:" SPC %nomaterial[%i] SPC "(you have:" SPC mfloor(%has) @ "x)");
            }
            return;
        }
	}
    for(%i = 0; %i < %max; %i++)
		%brick.eventEnabled[%i] = %old[%i];
	
	%this.pendingPrompt = 0;
	%this.triggerPrompt();
}

function serverCmdMessageBoxAccept(%cl)
{
	if(%cl.type == 0)
		%cl.processPrompt(1);
    if(%cl.type == 1)
		%cl.processPrompt(2);
	if(%cl.type == 2)
		%cl.processPrompt(3);
	if(%cl.type == 3)
		%cl.processPrompt(4);
	if(%cl.type == 4)
		%cl.processPrompt(5);
	if(%cl.type == 5)
		%cl.processPrompt(6);
    %cl.pendingPrompt = 0;
	%cl.triggerPrompt();
}

package PromptEvents
{
	function serverCmdMessageBoxCancel(%cl)
	{
		%cl.processPrompt(0);
	}
	
	function serverCmdMessageBoxNo(%cl)
	{
		%cl.processPrompt(0);
	}

};
activatePackage(PromptEvents);

function parseList(%str,%min,%max)
{
	%str = %str @ ",";
	%len = strLen(%str);
	
	for(%i = 0; %i < %len; %i++)
	{
		%chr = getSubStr(%str,%i,1);
		
		if(strPos(",-",%chr) == -1)
		{
			%num = %num @ ((strPos("0123456789",%chr) != -1) ? %chr : "");
			continue;
		}
		
		if(%num $= "")
		{
			%old = "";
			continue;
		}
		
		%a = %num;
		
		if(%old !$= "")
		{
			if(%old > %num)
			{
				%num = %old;
				%old = %a;
			}
			
			for(%j = %old; %j <= %num; %j++)
			{
				if(strPos(" " @ %list @ " "," " @ %j @ " ") == -1)
					%list = %list SPC %j;
			}
		}
		else
		{
			if(strPos(" " @ %list @ " "," " @ %num @ " ") == -1)
				%list = %list SPC %num;
		}
		
		%num = "";
		%old = ((%chr $= "-") ? %a : "");
	}
	
	%cnt = getWordCount(%list);
	
	for(%i = 0; %i < %cnt; %i++)
	{
		%num = getWord(%list,%i);
		
		if(%num >= %min && %num <= %max)
			%new = %new SPC %num;
	}
	
	return trim(%new);
}