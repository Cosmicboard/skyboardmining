$advancedCrafts = 0;
$advancedSmelts = 0;
$advancedExtracts = 0;
$advancedExtractions = 0;
$advancedFuels = 0;
$advancedDrillFuels = 0;
$advancedItems = 0;
function assignAdvancedCraft(%name, %ingredients, %amount, %type)
{
    $advancedCraft[$advancedCrafts] = %name TAB %ingredients TAB %amount TAB %type;
    $advancedCrafts++;
}
function assignAdvancedSmelting(%name, %ingredients, %amount, %type)
{
    $advancedSmelting[$advancedSmelts] = %name TAB %ingredients TAB %amount TAB %type;
    $advancedSmelts++;
}
function assignExtract(%name, %type)
{
    $advancedExtract[$advancedExtracts] = %name TAB %type;
    $advancedExtracts++;
}
function assignExtracted(%name, %ore, %chance)
{
    $advancedExtracted[$advancedExtractions] = %name TAB %ore TAB %chance;
    $advancedExtractions++;
}
function assignFuel(%name, %fuel)
{
    $advancedFuel[$advancedFuels] = %name TAB %fuel;
    $advancedFuels++;
}
function assignDrillFuel(%name, %fuel, %color)
{
    $advancedDrillFuel[$advancedDrillFuels] = %name TAB %fuel TAB %color;
    $advancedDrillFuels++;
}
function assignAdvancedItem(%name, %value, %color)
{
    $advancedItem[$advancedItems] = %name TAB %value TAB %color;
    $advancedItems++;
}

assignAdvancedCraft("Grand Design", "15 Iron\n8 Quartz\n5 Bronze", 1, "TOOL");
assignAdvancedCraft("Cobalt Cannon", "2 Cobalt Base Parts\n1 Titanium Lens\n2 Uranium", 1, "TOOL");
assignAdvancedCraft("Cobalt Base Parts", "6 Cobalt\n3 Steel", 1, "MATERIAL");
assignAdvancedCraft("Titanium Lens", "5 Titanium\n5 Quartz", 1, "MATERIAL");
assignAdvancedCraft("Luminite Cannon", "2 Luminite Base Parts\n1 Moonstone Lens\n3 Solarium", 1, "TOOL");
assignAdvancedCraft("Luminite Base Parts", "6 Lunar Alloy\n3 Neodymium", 1, "MATERIAL");
assignAdvancedCraft("Moonstone Lens", "5 Aurium\n3 Moonstone", 1, "MATERIAL");
assignAdvancedCraft("Olympium Cannon", "2 Olympium Base Parts\n1 Void Gem Lens\n3 Acceleratium", 1, "TOOL");
assignAdvancedCraft("Olympium Base Parts", "10 Olympium\n4 Eximite", 1, "MATERIAL");
assignAdvancedCraft("Void Gem Lens", "5 Illuminyx\n3 Void Gem", 1, "MATERIAL");

assignAdvancedSmelting("Biofuel", "10 Grass\n1 Heat", 1, "MATERIAL");
assignAdvancedSmelting("Steel", "3 Iron\n1 Coal\n2 Heat", 4, "MATERIAL");
assignAdvancedSmelting("Bronze", "3 Copper\n1 Tin\n2 Heat", 4, "MATERIAL");
assignAdvancedSmelting("Electrum", "2 Gold\n2 Silver\n2 Heat", 4, "MATERIAL");
assignAdvancedSmelting("Lava", "5 Stone\n2 Heat", 1, "MATERIAL");
assignAdvancedSmelting("Diamond", "69 Coal\n50 Heat", 1, "MATERIAL");
assignAdvancedSmelting("Lunar Alloy", "3 Luminite\n1 Astral Silver\n100 Heat", 4, "MATERIAL");
assignAdvancedSmelting("Olympium", "3 Elementium\n1 Aegistone\n500 Heat", 4, "MATERIAL");

assignExtract("Grass", "EXTRACTINATOR");
assignExtracted("Grass", "Coal", "1\n400");
assignExtracted("Grass", "Tin", "1\n450");
assignExtracted("Grass", "Iron", "1\n500");
assignExtracted("Grass", "Copper", "1\n550");
assignExtracted("Grass", "Gold", "1\n600");

assignExtract("Dirt", "EXTRACTINATOR");
assignExtracted("Dirt", "Coal", "1\n450");
assignExtracted("Dirt", "Tin", "1\n475");
assignExtracted("Dirt", "Iron", "1\n550");
assignExtracted("Dirt", "Copper", "1\n650");
assignExtracted("Dirt", "Gold", "1\n650");
assignExtracted("Dirt", "Silver", "1\n700");
assignExtracted("Dirt", "Aluminium", "1\n750");
assignExtracted("Dirt", "Zinc", "1\n800");
assignExtracted("Dirt", "Quartz", "1\n900");

assignExtract("Dense Dirt", "EXTRACTINATOR");
assignExtracted("Dense Dirt", "Coal", "1\n325");
assignExtracted("Dense Dirt", "Tin", "1\n400");
assignExtracted("Dense Dirt", "Iron", "1\n425");
assignExtracted("Dense Dirt", "Copper", "1\n450");
assignExtracted("Dense Dirt", "Gold", "1\n500");
assignExtracted("Dense Dirt", "Silver", "1\n580");
assignExtracted("Dense Dirt", "Aluminium", "1\n600");
assignExtracted("Dense Dirt", "Zinc", "1\n725");
assignExtracted("Dense Dirt", "Quartz", "1\n850");
assignExtracted("Dense Dirt", "Amber", "1\n925");
assignExtracted("Dense Dirt", "Graphite", "1\n925");
assignExtracted("Dense Dirt", "Lithium", "1\n950");
assignExtracted("Dense Dirt", "Cobalt", "1\n1000");

assignExtract("Stone", "EXTRACTINATOR");
assignExtracted("Stone", "Coal", "1\n300");
assignExtracted("Stone", "Tin", "1\n375");
assignExtracted("Stone", "Iron", "1\n400");
assignExtracted("Stone", "Copper", "1\n425");
assignExtracted("Stone", "Gold", "1\n450");
assignExtracted("Stone", "Silver", "1\n525");
assignExtracted("Stone", "Aluminium", "1\n550");
assignExtracted("Stone", "Cobalt", "1\n850");
assignExtracted("Stone", "Ruby", "1\n900");
assignExtracted("Stone", "Sapphire", "1\n900");
assignExtracted("Stone", "Nickel", "1\n950");
assignExtracted("Stone", "Emerald", "1\n950");
assignExtracted("Stone", "Platinum", "1\n1000");
assignExtracted("Stone", "Limestone", "1\n1100");
assignExtracted("Stone", "Amethyst", "1\n2000");

assignFuel("Grass", "0.01");
assignFuel("Biofuel", "0.2");
assignFuel("Coal", "2");
assignFuel("Lava", "5");

assignDrillFuel("Biofuel", "10", "<color:228B22>");
assignDrillFuel("yor moter", "999999");

assignAdvancedItem("Steel", "5", "<color:808080>");
assignAdvancedItem("Bronze", "6", "<color:ff7f19>");
assignAdvancedItem("Cobalt Base Parts", "100", "<color:191970>");
assignAdvancedItem("Titanium Lens", "1750", "<color:FFF0F5>");
assignAdvancedItem("Biofuel", "0.05", "<color:228B22>");

function player::tooFarAwayFrom(%player)
{
    if(%player.client.usingworkbench && vectordist("-16 -11 5003.7", %player.position) > 10)
        %player.client.closeCraftingMenu();
    else if(%player.client.usingextractinator && vectordist("-10.5 -16 5003.1", %player.position) > 10)
        %player.client.closeCraftingMenu();
    else if(%player.client.usingfuelchamber && vectordist("-16 -0.75 5004.4", %player.position) > 10)
        %player.client.closeCraftingMenu();
    else if(%player.client.usingfurnace && vectordist("-16 -0.75 5004.4", %player.position) > 10)
        %player.client.closeCraftingMenu();
    else if(%player.client.usingdrillfuelchamber && vectordist(%player.chosendrill.position, %player.position) > 10)
        %player.client.closeCraftingMenu();
    %player.toofaraway = %player.schedule(33, tooFarAwayFrom);
}

function gameconnection::showCraftingMenu(%client)
{
    if(!iseventpending(%client.player.toofaraway))
        %client.player.tooFarAwayFrom();
    %client.usingworkbench = 1;
    %client.usingfurnace = 0;
    %client.usingextractinator = 0;
    %client.cursor = 0;
    %client.cursorOffset = 0;
    %client.updateCraftingMenu(0);
}
registeroutputevent("GameConnection", "showCraftingMenu");
function gameconnection::closeCraftingMenu(%client)
{
    if(iseventpending(%client.player.toofaraway))
        cancel(%client.player.toofaraway);
    %client.usingfurnace = 0;
    %client.usingworkbench = 0;
    %client.usingextractinator = 0;
    %client.usingfuelchamber = 0;
    %client.usingdrillfuelchamber = 0;
    %client.cursor = 0;
    %client.cursorOffset = 0;
    %client.chosenitem = "";
    %client.centerprint("");
}
function gameconnection::showSmeltingMenu(%client)
{
    if(!iseventpending(%client.player.toofaraway))
        %client.player.tooFarAwayFrom();
    %client.usingworkbench = 0;
    %client.usingextractinator = 0;
    %client.usingfurnace = 1;
    %client.usingfuelchamber = 0;
    %client.cursor = 0;
    %client.cursorOffset = 0;
    if(%client.heatpoints $= "")
        %client.heatpoints = 0;
    %client.updateCraftingMenu(1);
}
registeroutputevent("GameConnection", "showSmeltingMenu");
function gameconnection::showExtractinatorMenu(%client)
{
    if(!iseventpending(%client.player.toofaraway))
        %client.player.tooFarAwayFrom();
    %client.inputAmount = 1;
    %client.usingworkbench = 0;
    %client.usingextractinator = 1;
    %client.usingfuelchamber = 0;
    %client.usingfurnace = 0;
    %client.cursor = 0;
    %client.cursorOffset = 0;
    %client.updateExtractionMenu();
}
registeroutputevent("GameConnection", "showExtractinatorMenu");
function gameconnection::showFuelChamberMenu(%client)
{
    if(!iseventpending(%client.player.toofaraway))
        %client.player.tooFarAwayFrom();
    %client.inputAmount = 1;
    %client.usingworkbench = 0;
    %client.usingextractinator = 0;
    %client.usingfuelchamber = 1;
    %client.usingfurnace = 0;
    %client.cursor = 0;
    %client.cursorOffset = 0;
    %client.updateFuelChamberMenu();
}
registeroutputevent("GameConnection", "showFuelChamberMenu");

function gameconnection::showDrillFuelChamberMenu(%client)
{
    if(!iseventpending(%client.player.toofaraway))
        %client.player.tooFarAwayFrom();
    %client.inputAmount = 1;
    %client.usingdrillfuelchamber = 1;
    %client.cursor = 0;
    %client.cursorOffset = 0;
    %client.updateDrillChamberMenu();
}

function gameconnection::updateCraftingMenu(%client, %type)
{
    if(%type == 0)
        %maxCount = $advancedCrafts;
    else if(%type == 1)
        %maxCount = $advancedSmelts;
    for(%i = 0+%client.cursorOffset; %i < 5+%client.cursorOffset; %i++)
    {
        if(%type == 0)
            %itemType = $advancedCraft[%i];
        else if(%type == 1)
            %itemType = $advancedSmelting[%i];
        if(%client.cursor == %i)
        {
            %cursor[%i] = "<color:fff000>";
            %client.chosenItem = getfield(%itemType,0);
            %recipe = "" SPC listCrafts(getfields(%itemType,1,getfieldcount(%itemType)-3));
            if(getfield(%itemType,getfieldcount(%itemType)-2) > 1 || %maxCount == $advancedSmelts)
                %amount = getfield(%itemType,getfieldcount(%itemType)-2) @ "x ";
        }
        else
            {%cursor[%i] = "<color:555555>";%recipe = "";%amount="";}
        %options = %options NL %cursor[%i] @ %amount @ getfield(%itemType,0) @ %recipe;
    }
    if(%type == 0)
        %client.centerprint("<font:georgia:30>\c2crafting menu<font:verdana:24>" @ %options, -1);
    else if(%type == 1)
        %client.centerprint("<font:georgia:30>\c2smelting menu<font:arial bold:16> \c0heat:" SPC %client.heatpoints SPC "<font:verdana:24>" @ %options, -1);
}

function gameconnection::updateExtractionMenu(%client)
{
    for(%i = 0; %i < $advancedExtracts; %i++)
    {
        %itemType = $advancedExtract[%i];
        if(%client.cursor == %i)
        {
            %amount = %client.inputAmount @ "x ";
            %cursor[%i] = "<color:fff000>";
            %client.chosenItem = getfield(%itemType,0);
        }
        else
            {%cursor[%i] = "<color:555555>";%amount="";}
        %options = %options NL %cursor[%i] @ %amount @ getfield(%itemType,0);
    }
    %client.centerprint("<font:georgia:30>\c2extraction menu <font:georgia:15>\c2(shift brick left/right to increase amount)<font:verdana:24>" @ %options, -1);
}

function gameconnection::updateFuelChamberMenu(%client)
{
    for(%i = 0; %i < $advancedFuels; %i++)
    {
        %itemType = $advancedFuel[%i];
        if(%client.cursor == %i)
        {
            %amount = %client.inputAmount @ "x ";
            %cursor[%i] = "<color:fff000>";
            %client.chosenItem = getfield(%itemType,0);
        }
        else
            {%cursor[%i] = "<color:555555>";%amount="";}
        %options = %options NL %cursor[%i] @ %amount @ getfield(%itemType,0) SPC "(Heat:" SPC getfield(%itemType,1)*%client.inputAmount @ ")";
    }
    %client.centerprint("<font:georgia:30>\c2fuel chamber <font:georgia:15>\c2(shift brick left/right to increase amount)<font:verdana:24>" @ %options, -1);
}

function gameconnection::updateDrillChamberMenu(%client)
{
    for(%i = 0; %i < $advancedDrillFuels; %i++)
    {
        %itemType = $advancedDrillFuel[%i];
        if(%client.cursor == %i)
        {
            %amount = %client.inputAmount @ "x ";
            %cursor[%i] = "<color:fff000>";
            %client.chosenItem = getfield(%itemType,0);
        }
        else
            {%cursor[%i] = "<color:555555>";%amount="";}
        %options = %options NL %cursor[%i] @ %amount @ getfield(%itemType,0) SPC "(Fuel:" SPC getfield(%itemType,1)*%client.inputAmount @ ")";
    }
    %client.centerprint("<font:georgia:30>\c2drill fuel chamber <font:georgia:15>\c2(shift brick left/right to increase amount) \c0fuel:" SPC mfloor(%client.player.chosendrill.fuel) SPC "<font:verdana:24>" @ %options, -1);
}

function listCrafts(%recipe)
{
    %count = getfieldcount(%recipe);
    for(%i = 0; %i < %count; %i++)
    {
        %craft = %craft SPC firstword(getfield(%recipe,%i)) @ "x" SPC restwords(getfield(%recipe,%i));
    }
    return "(" @ ltrim(%craft) @ ")";
}

function gameconnection::craftItem(%this, %type)
{
    %selected = %this.chosenItem;
    if(%type == 0)
        %count = $advancedCrafts;
    else if(%type == 1)
        %count = $advancedSmelts;
    for(%i = 0; %i < %count; %i++)
    {
        if(%type == 0)
            %itemType = $advancedCraft[%i];
        else if(%type == 1)
            %itemType = $advancedSmelting[%i];
        if(%selected $= getfield(%itemType,0))
        {
            %item = %itemType;
            break;
        }
    }
    %mats = getfields(%item,1,getfieldcount(%item)-3);
    %matscount = getfieldcount(%item)-3;
    %materialcount = 0;
    for(%i = 0; %i < %matscount; %i++)
    {
        if(restwords(getfield(%mats,%i)) $= "heat")
        {
            %heatReq = firstword(getfield(%mats,%i));
            continue;
        }
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
    if(%heatreq > 0 && %this.heatpoints < %heatreq)
        %fail = true;
    if(!%fail)
    {
        if(%type == 0)
        {
            if(getsubstr(getfield(%item,0),0,1) $= "a" || getsubstr(getfield(%item,0),0,1) $= "e" || getsubstr(getfield(%item,0),0,1) $= "u" || getsubstr(getfield(%item,0),0,1) $= "i" || getsubstr(getfield(%item,0),0,1) $= "o")
                %craftedItem = "an" SPC getfield(%item,0);
            else
                %craftedItem = "a" SPC getfield(%item,0);
        }
        else if(%type == 1)
            %craftedItem = getfield(%item,0);
		if(getfield(%item,getfieldcount(%item)-1) $= "TOOL")
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
			%this.inventory[getfield(%item,0)] += getfield(%item,getfieldcount(%item)-2);
			%this.chatmessage("\c2Crafted" SPC getfield(%item,getfieldcount(%item)-2) @ "x" SPC %crafteditem @ "!");
		}
		for(%i = 0; %i < %matscount; %i++)
		{
			%this.inventory[%material[%i]]-=%materialamount[%i];
		}
		if(%heatreq > 0)
            %this.heatpoints -= %heatreq;
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
        if(%heatreq > 0 && %this.heatpoints < %heatreq)
            %this.chatmessage("The furnace requires:" SPC %heatreq @ "x Heat" SPC "(you have:" SPC mfloor(%this.heatpoints) @ "x)");
        return;
    }
}

function gameconnection::extractItem(%client)
{
    %client.found = "";
    %client.totalarrays=0;
    for(%i = 0; %i < $advancedExtractions; %i++)
    {
        %extract = $advancedExtracted[%i];
        %oreName = getfield(%extract,1);
        %client.totalFound[%orename] = 0;
    }
    %selected = %client.chosenItem;
    if(%client.inputAmount <= %client.inventory[%selected] || %client.inputAmount > %client.inventory[%selected] && %client.inventory[%selected] > 0)
    {
        %inputAmount = %client.inputAmount;
        if(%client.inputAmount > %client.inventory[%selected] && %client.inventory[%selected] > 0)
            %inputAmount = %client.inventory[%selected];
        %client.inventory[%selected] -= %inputAmount;
		%client.playsound(beep_key_sound);
        %color = convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[oreidfromname(%selected)],5)),0) * 255));
		%color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[oreidfromname(%selected)],5)),1) * 255));
		%color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[oreidfromname(%selected)],5)),2) * 255));
		%color = "<color:" @ %color @ ">";
		%client.chatmessage("\c6The extractinator has proccessed" SPC %inputAmount @ "x" SPC %color @ %selected @ "\c6 and has extracted:");
        for(%i = 0; %i < %inputAmount; %i++)
        {
            %totalArrays = 0;
            for(%g = 0; %g < $advancedExtractions; %g++)
            {
                %extract = $advancedExtracted[%g];
                %oreName = getfield(%extract,1);
                if(%selected $= getfield(%extract,0))
                {
                    if(getrandom(1, getfield(%extract,3)) <= getfield(%extract,2))
                    {
                        %client.inventory[%oreName]++;
                        if(!%client.totalfound[%oreName])
                            %client.totalfound[%oreName] = 1;
                        else
                            %client.totalFound[%oreName]++;
                        if(stripos(%client.found, %oreName, 0) <= 0)
                        {
                            if(!%client.totalArrays)
                                %client.totalArrays = 1;
                            else
                                %client.totalArrays++;
                            %client.found = %client.totalfound[%oreName] @ "x" SPC %oreName TAB %client.found;
                        }
                        else
                        {
                            for(%e = 0; %e < %client.totalArrays; %e++)
                            {
                                %field = getfield(%client.found,%e);
                                if(restwords(%field) $= %oreName)
                                {
                                    %client.found = strreplace(%client.found, %client.totalfound[restwords(%field)]-1 @ "x" SPC restwords(%field), %client.totalfound[restwords(%field)] @ "x" SPC restwords(%field));
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
        if(%client.found !$= "")
        {
            seperateExtractinatorDrops(%client, %client.found, %client.totalarrays);
            totalCashExtractinatorDrops(%client, %client.found, %client.totalarrays);
        }
        else
            %client.chatmessage("nothing lol");
    }
    else
    {
        %client.chatmessage("you only have" SPC mfloor(%client.inventory[%selected]) @ "x" SPC %selected);
        %client.playsound(errorsound);
        return;
    }
}

function gameconnection::inputFuel(%client)
{
    %selected = %client.chosenItem;
    if(%client.inputAmount <= %client.inventory[%selected] || %client.inputAmount > %client.inventory[%selected] && %client.inventory[%selected] > 0)
    {
        %inputAmount = %client.inputAmount;
        if(%client.inputAmount > %client.inventory[%selected] && %client.inventory[%selected] > 0)
            %inputAmount = %client.inventory[%selected];
        %client.inventory[%selected] -= %inputAmount;
		%client.playsound(beep_key_sound);
        %color = convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[oreidfromname(%selected)],5)),0) * 255));
		%color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[oreidfromname(%selected)],5)),1) * 255));
		%color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[oreidfromname(%selected)],5)),2) * 255));
		%color = "<color:" @ %color @ ">";
        for(%i = 0; %i < $advancedFuels; %i++)
        {
            if(getfield($advancedFuel[%i],0) $= %selected)
                {%heatPoints = getfield($advancedFuel[%i],1);break;}
        }
		%client.chatmessage("\c6Successfully put" SPC %inputAmount @ "x" SPC %color @ %selected @ "\c6 and gained\c0" SPC %heatPoints*%inputAmount SPC "heat\c6!");
        %client.heatpoints += %heatpoints * %inputAmount;
    }
    else
    {
        %client.chatmessage("you only have" SPC mfloor(%client.inventory[%selected]) @ "x" SPC %selected);
        %client.playsound(errorsound);
        return;
    }
}

function gameconnection::inputDrillFuel(%client)
{
    %selected = %client.chosenItem;
    if(%client.inputAmount <= %client.inventory[%selected] || %client.inputAmount > %client.inventory[%selected] && %client.inventory[%selected] > 0)
    {
        %inputAmount = %client.inputAmount;
        if(%client.inputAmount > %client.inventory[%selected] && %client.inventory[%selected] > 0)
            %inputAmount = %client.inventory[%selected];
        %client.inventory[%selected] -= %inputAmount;
		%client.playsound(beep_key_sound);
        for(%i = 0; %i < $advancedFuels; %i++)
        {
            if(getfield($advancedDrillFuel[%i],0) $= %selected)
                {%color = getfield($advancedDrillFuel[%i],2);%heatPoints = getfield($advancedDrillFuel[%i],1);break;}
        }
		%client.chatmessage("\c6Successfully put" SPC %inputAmount @ "x" SPC %color @ %selected @ "\c6 for\c0" SPC %heatPoints*%inputAmount SPC "fuel\c6!");
        %client.player.chosendrill.addfuel(%heatpoints * %inputAmount);
        %client.updateDrillChamberMenu();
    }
    else
    {
        %client.chatmessage("you only have" SPC mfloor(%client.inventory[%selected]) @ "x" SPC %selected);
        %client.playsound(errorsound);
        return;
    }
}

function seperateExtractinatorDrops(%client, %table, %amount)
{
    for(%i = 0; %i < %amount; %i++)
    {
        %ore = restwords(getfield(%table,%i));
        %oreCount = firstword(getfield(%table,%i));
        %color = convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[oreidfromname(%ore)],5)),0) * 255));
		%color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[oreidfromname(%ore)],5)),1) * 255));
		%color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[oreidfromname(%ore)],5)),2) * 255));
		%color = "<color:" @ %color @ ">";
        if(strlen(%droptable SPC %color SPC %orecount SPC %ore) > 200)
            %dropTable2 = %color @ %orecount SPC %ore SPC "" TAB %dropTable2;
        else
            %dropTable = %color @ %orecount SPC %ore SPC "" TAB %dropTable;
    }
    %client.chatmessage(%droptable);
    %client.chatmessage(%droptable2);
}

function totalCashExtractinatorDrops(%client, %table, %amount)
{
    %oreValue = 0;
    for(%i = 0; %i < %amount; %i++)
    {
        %ore = restwords(getfield(%table,%i));
        %oreCount = firstword(getfield(%table,%i));
        %oreValue += getfield($ore[oreidfromname(%ore)],2) * %oreCount;
    }
    %client.chatmessage("\c6that all makes you\c2" SPC %oreValue*(1+%client.prestigecashbonus)*(1+%client.achievementcashbonus) @ " cash\c6 btw");
}

package BrickControlsMenu
{
	function serverCmdPlantBrick(%client)
	{
        if(%client.usingworkbench)
        {
            for(%i = 0; %i < $advancedCrafts; %i++)
            {
                if(%client.chosenItem $= getfield($advancedCraft[%i],0))
                {
                    if(%client.craftedpickaxe[%client.chosenItem])
                    {
                        %client.promptclient(1, "<shadow:-1.5:-1.5><shadowcolor:111111><font:palatino linotype:28><color:111111>" @ "Equip the" NL "<shadow:0:0><color:fff000><font:Verdana:23>" SPC %client.chosenItem @ "", "0", %client, 1);
                        return;
                    }
                   %client.craftItem(0);
                }
            }
            return;
        }
        else if(%client.usingfurnace)
        {
            for(%i = 0; %i < $advancedSmelts; %i++)
            {
                if(%client.chosenItem $= getfield($advancedSmelting[%i],0))
                {
                   %client.craftItem(1);
                   %client.updateCraftingMenu(1);
                }
            }
            return;
        }
        else if(%client.usingextractinator)
        {
            for(%i = 0; %i < $advancedExtracts; %i++)
            {
                if(%client.chosenItem $= getfield($advancedExtract[%i],0))
                {
                   %client.extractItem();
                }
            }
            return;
        }
        else if(%client.usingfuelchamber)
        {
            for(%i = 0; %i < $advancedFuels; %i++)
            {
                if(%client.chosenItem $= getfield($advancedFuel[%i],0))
                {
                   %client.inputFuel();
                }
            }
            return;
        }
        else if(%client.usingdrillfuelchamber)
        {
            for(%i = 0; %i < $advancedDrillFuels; %i++)
            {
                if(%client.chosenItem $= getfield($advancedDrillFuel[%i],0))
                {
                   %client.inputDrillFuel();
                }
            }
            return;
        }
		return parent::serverCmdPlantBrick(%client);
	}

	function serverCmdCancelBrick(%client)
	{
        if(%client.usingworkbench || %client.usingfurnace || %client.usingextractinator || %client.usingfuelchamber || %client.usingdrillfuelchamber)
            {%client.closeCraftingMenu();return;}
		return parent::serverCmdCancelBrick(%client);
	}

	function serverCmdShiftBrick(%client, %y, %x, %z)
	{
        if(%x == -1 && %client.usingextractinator || %x == -1 && %client.usingfuelchamber || %x == -1 && %client.usingdrillfuelchamber)
        {
            %client.inputAmount++;
            if(%client.inputAmount > 500)
                %client.inputAmount = 500;
            if(%client.usingextractinator)
                %client.updateExtractionMenu();
            else if(%client.usingfuelchamber)
                %client.updateFuelChamberMenu();
            else if(%client.usingdrillfuelchamber)
                %client.updateDrillChamberMenu();
            return;
        }
        else if(%x == 1 && %client.usingextractinator || %x == 1 && %client.usingfuelchamber || %x == 1 && %client.usingdrillfuelchamber)
        {
            %client.inputAmount--;
            if(%client.inputAmount <= 0)
                %client.inputAmount = 1;
            if(%client.usingextractinator)
                %client.updateExtractionMenu();
            else if(%client.usingfuelchamber)
                %client.updateFuelChamberMenu();
            else if(%client.usingdrillfuelchamber)
                %client.updateDrillChamberMenu();
            return;
        }
        if(%y == 1)
        {
            %client.cursor--;
            if(%client.cursor > 1 && %client.cursoroffset > 0)
                %client.cursorOffset--;
            if(%client.usingworkbench)
            {
                if(%client.cursor <= -1)
                {
                    %client.cursor = $advancedCrafts-1;
                    if($advancedCrafts > 5)
                        %client.cursorOffset = $advancedCrafts-5;
                }
                %client.updateCraftingMenu(0);
                return;
            }
            else if(%client.usingfurnace)
            {
                if(%client.cursor <= -1)
                {
                    %client.cursor = $advancedSmelts-1;
                    if($advancedSmelts > 5)
                        %client.cursorOffset = $advancedSmelts-5;
                }
                %client.updateCraftingMenu(1);
                return;
            }
            else if(%client.usingextractinator)
            {
                if(%client.cursor <= -1)
                {
                    %client.cursor = $advancedExtracts-1;
                    if($advancedExtracts > 5)
                        %client.cursorOffset = $advancedExtracts-5;
                }
                %client.updateExtractionMenu();
                return;
            }
            else if(%client.usingfuelchamber)
            {
                if(%client.cursor <= -1)
                {
                    %client.cursor = $advancedFuels-1;
                    if($advancedFuels > 5)
                        %client.cursorOffset = $advancedFuels-5;
                }
                %client.updateFuelChamberMenu();
                return;
            }
            else if(%client.usingdrillfuelchamber)
            {
                if(%client.cursor <= -1)
                {
                    %client.cursor = $advancedDrillFuels-1;
                    if($advancedFuels > 5)
                        %client.cursorOffset = $advancedDrillFuels-5;
                }
                %client.updateDrillChamberMenu();
                return;
            }
            return parent::serverCmdShiftBrick(%client, %y, %x, %z);
        }
        else if(%y == -1)
        {
            %client.cursor++;
            if(%client.usingworkbench)
            {
                if($advancedCrafts > 5)
                {
                    if(%client.cursor > 2 && %client.cursoroffset < $advancedCrafts-5)
                        %client.cursorOffset++;
                }
                if(%client.cursor >= $advancedCrafts)
                    {%client.cursor = 0;%client.cursorOffset=0;}
                %client.updateCraftingMenu(0);
                return;
            }
            else if(%client.usingfurnace)
            {
                if($advancedSmelts > 5)
                {
                    if(%client.cursor > 2 && %client.cursoroffset < $advancedSmelts-5)
                        %client.cursorOffset++;
                }
                if(%client.cursor >= $advancedSmelts)
                    {%client.cursor = 0;%client.cursorOffset=0;}
                %client.updateCraftingMenu(1);
                return;
            }
            if(%client.usingextractinator)
            {
                if($advancedExtracts > 5)
                {
                    if(%client.cursor > 2 && %client.cursoroffset < $advancedExtracts-5)
                        %client.cursorOffset++;
                }
                if(%client.cursor >= $advancedExtracts)
                    {%client.cursor = 0;%client.cursorOffset=0;}
                %client.updateExtractionMenu();
                return;
            }
            if(%client.usingfuelchamber)
            {
                if($advancedFuels > 5)
                {
                    if(%client.cursor > 2 && %client.cursor < $advancedFuels-2)
                        %client.cursorOffset++;
                }
                if(%client.cursor >= $advancedFuels)
                    {%client.cursor = 0;%client.cursorOffset=0;}
                %client.updateFuelChamberMenu();
                return;
            }
            if(%client.usingdrillfuelchamber)
            {
                if($advancedDrillFuels > 5)
                {
                    if(%client.cursor > 2 && %client.cursor < $advancedDrillFuels-2)
                        %client.cursorOffset++;
                }
                if(%client.cursor >= $advancedDrillFuels)
                    {%client.cursor = 0;%client.cursorOffset=0;}
                %client.updateDrillChamberMenu();
                return;
            }
            return parent::serverCmdShiftBrick(%client, %y, %x, %z);
        }
        parent::serverCmdShiftBrick(%client, %y, %x, %z);
	}

	function serverCmdSuperShiftBrick(%client, %y, %x, %z)
	{
        if(%x == -1 && %client.usingextractinator || %x == -1 && %client.usingfuelchamber || %x == -1 && %client.usingdrillfuelchamber)
        {
            %client.inputAmount += 10;
            if(%client.inputAmount > 500)
                %client.inputAmount = 500;
            if(%client.usingextractinator)
                %client.updateExtractionMenu();
            else if(%client.usingfuelchamber)
                %client.updateFuelChamberMenu();
            else if(%client.usingdrillfuelchamber)
                %client.updateDrillChamberMenu();
            return;
        }
        else if(%x == 1 && %client.usingextractinator || %x == 1 && %client.usingfuelchamber || %x == 1 && %client.usingdrillfuelchamber)
        {
            %client.inputAmount -= 10;
            if(%client.inputAmount <= 0)
                %client.inputAmount = 1;
            if(%client.usingextractinator)
                %client.updateExtractionMenu();
            else if(%client.usingfuelchamber)
                %client.updateFuelChamberMenu();
            else if(%client.usingdrillfuelchamber)
                %client.updateDrillChamberMenu();
            return;
        }
        if(%y == 1)
        {
            %client.cursor--;
            if(%client.cursor > 1 && %client.cursoroffset > 0)
                %client.cursorOffset--;
            if(%client.usingworkbench)
            {
                if(%client.cursor <= -1)
                {
                    %client.cursor = $advancedCrafts-1;
                    if($advancedCrafts > 5)
                        %client.cursorOffset = $advancedCrafts-5;
                }
                %client.updateCraftingMenu(0);
                return;
            }
            else if(%client.usingfurnace)
            {
                if(%client.cursor <= -1)
                {
                    %client.cursor = $advancedSmelts-1;
                    if($advancedSmelts > 5)
                        %client.cursorOffset = $advancedSmelts-5;
                }
                %client.updateCraftingMenu(1);
                return;
            }
            else if(%client.usingextractinator)
            {
                if(%client.cursor <= -1)
                {
                    %client.cursor = $advancedExtracts-1;
                    if($advancedExtracts > 5)
                        %client.cursorOffset = $advancedExtracts-5;
                }
                %client.updateExtractionMenu();
                return;
            }
            else if(%client.usingfuelchamber)
            {
                if(%client.cursor <= -1)
                {
                    %client.cursor = $advancedFuels-1;
                    if($advancedFuels > 5)
                        %client.cursorOffset = $advancedFuels-5;
                }
                %client.updateFuelChamberMenu();
                return;
            }
            else if(%client.usingdrillfuelchamber)
            {
                if(%client.cursor <= -1)
                {
                    %client.cursor = $advancedDrillFuels-1;
                    if($advancedFuels > 5)
                        %client.cursorOffset = $advancedDrillFuels-5;
                }
                %client.updateDrillChamberMenu();
                return;
            }
            return parent::serverCmdSuperShiftBrick(%client, %y, %x, %z);
        }
        else if(%y == -1)
        {
            %client.cursor++;
            if(%client.usingworkbench)
            {
                if($advancedCrafts > 5)
                {
                    if(%client.cursor > 2 && %client.cursor < $advancedCrafts-2)
                        %client.cursorOffset++;
                }
                if(%client.cursor >= $advancedCrafts)
                    {%client.cursor = 0;%client.cursorOffset=0;}
                %client.updateCraftingMenu(0);
                return;
            }
            else if(%client.usingfurnace)
            {
                if($advancedSmelts > 5)
                {
                    if(%client.cursor > 2 && %client.cursor < $advancedSmelts-2)
                        %client.cursorOffset++;
                }
                if(%client.cursor >= $advancedSmelts)
                    {%client.cursor = 0;%client.cursorOffset=0;}
                %client.updateCraftingMenu(1);
                return;
            }
            if(%client.usingextractinator)
            {
                if($advancedExtracts > 5)
                {
                    if(%client.cursor > 2 && %client.cursor < $advancedExtracts-2)
                        %client.cursorOffset++;
                }
                if(%client.cursor >= $advancedExtracts)
                    {%client.cursor = 0;%client.cursorOffset=0;}
                %client.updateExtractionMenu();
                return;
            }
            if(%client.usingfuelchamber)
            {
                if($advancedFuels > 5)
                {
                    if(%client.cursor > 2 && %client.cursor < $advancedFuels-2)
                        %client.cursorOffset++;
                }
                if(%client.cursor >= $advancedFuels)
                    {%client.cursor = 0;%client.cursorOffset=0;}
                %client.updateFuelChamberMenu();
                return;
            }
            if(%client.usingdrillfuelchamber)
            {
                if($advancedDrillFuels > 5)
                {
                    if(%client.cursor > 2 && %client.cursor < $advancedDrillFuels-2)
                        %client.cursorOffset++;
                }
                if(%client.cursor >= $advancedDrillFuels)
                    {%client.cursor = 0;%client.cursorOffset=0;}
                %client.updateDrillChamberMenu();
                return;
            }
        }
        parent::serverCmdSuperShiftBrick(%client, %y, %x, %z);
	}
};
activatePackage(BrickControlsMenu);