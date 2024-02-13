function servercmdtrade(%client, %name)
{
    %victim = findclientbyname(%name);
    if(!isobject(%victim))
    {
        %client.chatmessage("player with this name does not exist");
        %client.playsound(errorsound);
        return;
    }
    else if(isobject(%victim) && %victim == %client)
    {
        %client.chatmessage("you can't send a trade request to yourself lol");
        %client.playsound(errorsound);
        return;
    }
    else
    {
        if(%client.tradingWith)
        {
            %client.chatmessage("you can't send another trade request while you are trading with another person");
            %client.playsound(errorsound);
            return;
        }
        else if(%victim.tradingWith)
        {
            %client.chatmessage("that person is already trading with someone");
            %client.playsound(errorsound);
            return;
        }
        %client.playsound(beep_key_sound);
        %victim.playsound(beep_popup_sound);
        %client.timeoutTrade = %client.schedule(30000, tradeTimeout, %victim);
        %client.tradeSender = 1;
        %victim.tradeRequest = 1;
        %client.tradingWith = %victim;
        %victim.tradingWith = %client;
        %client.chatmessage("\c6Sent\c4" SPC %victim.name SPC "\c6a trade request! Waiting for response...");
        %victim.chatmessage("\c4" @ %client.name SPC "\c6has sent you a trade request! \c2/acceptTrade\c6 or \c2/declineTrade");
    }
}

function servercmdAcceptTrade(%client)
{
    if(%client.tradeRequest)
    {
        %client.offers = 0;
        %client.tradingWith.offers = 0;
        %client.tradingWith.canOffer = 1;
        %client.canOffer = 1;
        %client.tradingWith.chatmessage("\c4" @ %client.name SPC "\c6had accepted your trade request!");
        %client.tradingWith.chatmessage("\c2/offer [ore] [amount]\c6 to send ores you'd be willing to trade and \c2/confirmTrade \c6to confirm the trade");
        %client.tradingWith.playsound(beep_popup_sound);
        cancel(%client.tradingWith.timeoutTrade);
        %client.chatmessage("\c6Accepted\c4" SPC %client.tradingwith.name @ "'s\c6 trade request!");
        %client.chatmessage("\c2/offer [ore] [amount]\c6 to send ores you'd be willing to trade and \c2/confirmTrade \c6to confirm the trade");
        %client.playsound(beep_key_sound);
    }
}

function servercmdDeclineTrade(%client)
{
    if(%client.tradeRequest || %client.canoffer)
    {
        %victim = %client.tradingWith;
        if(%client.canoffer && %client.tradeRequest)
        {
            %client.chatmessage("\c6Declined\c4" SPC %client.tradingwith.name @ "'s \c6trade request.");
            %client.tradingwith.chatmessage("\c4" @ %client.name SPC "\c6has declined your trade request.");
            if(iseventpending(%client.timeouttrade))
                cancel(%client.timeouttrade);
            if(iseventpending(%victim.timeouttrade))
                cancel(%victim.timeouttrade);
        }
        else if(%client.canoffer && !%client.tradeRequest)
        {
            %client.chatmessage("\c6Declined your trade request.");
            %client.tradingwith.chatmessage("\c4" @ %client.name SPC "\c6has declined their trade request.");
            if(iseventpending(%client.timeouttrade))
                cancel(%client.timeouttrade);
            if(iseventpending(%victim.timeouttrade))
                cancel(%victim.timeouttrade);
        }
        else if(!%client.canoffer && %client.tradeRequest)
        {
            %client.chatmessage("\c6Declined\c4" SPC %client.tradingwith.name @ "'s \c6trade request.");
            %client.tradingwith.chatmessage("\c4" @ %client.name SPC "\c6has declined your trade request.");
            if(iseventpending(%client.timeouttrade))
                cancel(%client.timeouttrade);
            if(iseventpending(%victim.timeouttrade))
                cancel(%victim.timeouttrade);
        }
        else if(!%client.canoffer && !%client.tradeRequest)
        {
            %client.chatmessage("\c6Declined your trade request.");
            %client.tradingwith.chatmessage("\c4" @ %client.name SPC "\c6has declined their trade request.");
            if(iseventpending(%client.timeouttrade))
                cancel(%client.timeouttrade);
            if(iseventpending(%victim.timeouttrade))
                cancel(%victim.timeouttrade);
        }
        %client.playsound(beep_key_sound);
        %client.tradingwith.playsound(errorsound);
        %client.tradeConfirmed = 0;
        %victim.tradeConfirmed = 0;
        %client.tradeSender = 0;
        %client.tradeRequest = 0;
        %victim.tradeSender = 0;
        %victim.tradeRequest = 0;
        %client.tradingfee = 0;
        %victim.tradingfee = 0;
        %client.canoffer = 0;
        %victim.canoffer = 0;
        for(%i = 0; %i < %client.Offers; %i++)
        {
            %client.offerLocked[getfield(%client.tradeOffer[%i],0)] = 0;
            %client.tradeOffer[%i] = "";
        }
        for(%i = 0; %i < %victim.Offers; %i++)
        {
            %victim.offerLocked[getfield(%victim.tradeOffer[%i],0)] = 0;
            %victim.tradeOffer[%i] = "";
        }
        %client.offers = 0;
        %victim.offers = 0;
        %client.tradingWith = 0;
        %victim.tradingWith = 0;
    }
}

function servercmdOffer(%client, %ore1, %ore2, %ore3, %amount)
{
    if(%client.canoffer)
    {
        if(%ore1 !$= "" && %ore2 !$= "" && %ore3 $= "")
        {
            %ore = %ore1;
            %amount = %ore2;
        }
        else if(%ore1 !$= "" && %ore2 !$= "" && %ore3 !$= "" && %amount $= "")
        {
            %ore = %ore1 SPC %ore2;
            %amount = %ore3;
        }
        else
            %ore = %ore1 SPC %ore2 SPC %ore3;
        if(oreidfromname(%ore) == -1)
        {
            %client.chatmessage("this ore does not exist");
            %client.playsound(errorsound);
            return;
        }
        else if(%amount <= 0)
        {
            %client.chatmessage("how are you gonna offer this amount");
            %client.playsound(errorsound);
            return;
        }
        else if(%client.inventory[%ore] < %amount)
        {
            %client.chatmessage("you don't got this amount");
            %client.playsound(errorsound);
            return;
        }
        %amount = mfloor(%amount);
        %victim = %client.tradingWith;
        if(%victim.level < getfield($ore[%ore],4))
        {
            %client.chatmessage(%victim.name SPC "does not meet the ore's level requirement to receive this ore");
            %client.playsound(errorsound);
            return;
        }
        %color = convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[oreidfromname(%ore)],5)),0) * 255));
        %color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[oreidfromname(%ore)],5)),1) * 255));
        %color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[oreidfromname(%ore)],5)),2) * 255));
        %color = "<color:" @ %color @ ">";
        %victim.tradingFee += %amount * getfield($ore[oreidfromname(%ore)],2) * 1.25 * (1 + %victim.prestigecashbonus);
        for(%i = 0; %i < %client.offers; %i++)
        {
            if(getfield(%client.tradeOffer[%i],0) $= %ore)
            {
                %client.tradeOffer[%i] = %ore TAB getfield(%client.tradeOffer[%i],1)+%amount;
                %client.offerLocked[%ore] = 1;
            }
        }
        if(!%client.offerLocked[%ore])
        {
            %client.tradeOffer[%client.offers] = %ore TAB %amount;
            %client.offers++;
        }
        %client.chatmessage("\c6You offered" @ %color SPC %amount @ "x" SPC getfield($ore[oreidfromname(%ore)],0) @ "\c6.");
        %client.chatmessage("\c6Your trading fee:" SPC "\c2" @ mfloatlength(%client.tradingFee,3) @ "$\c6 -\c4" SPC %victim.name @ "'\s\c6 trading fee:" SPC "\c2" @ mfloatlength(%victim.tradingFee,3) @ "$");
        %client.tradeConfirmed = 0;
        %victim.chatmessage("\c4" @ %client.name SPC "\c6has offered" @ %color SPC %amount @ "x" SPC getfield($ore[oreidfromname(%ore)],0) @ "\c6.");
        %victim.chatmessage("\c6Your trading fee:" SPC "\c2" @ mfloatlength(%victim.tradingFee,3) @ "$\c6 -\c4" SPC %client.name @ "'\s\c6 trading fee:" SPC "\c2" @ mfloatlength(%client.tradingFee,3) @ "$");
        %victim.tradeConfirmed = 0;
    }
}

function servercmdConfirmTrade(%client)
{
    if(%client.canoffer)
    {
        if(!%client.tradeConfirmed)
        {
            %victim = %client.tradingWith;
            %client.playsound(beep_key_sound);
            %client.chatmessage("\c6You confirmed your trade.");
            %victim.playsound(beep_key_sound);
            %victim.chatmessage("\c4" @ %client.name SPC "\c6has confirmed their trade.");
            %client.tradeConfirmed = 1;
            if(%client.tradeConfirmed && %victim.tradeConfirmed)
            {
                if(%client.tradingFee > %client.money)
                {
                    %client.chatmessage("you don't have enough money to finish the trade");
                    %client.playsound(errorsound);
                    %victim.chatmessage(%client.name SPC "did not have enough money to finish the trade");
                    %victim.playsound(errorsound);
                    %client.tradeConfirmed = 0;
                    %victim.tradeConfirmed = 0;
                    %client.tradeSender = 0;
                    %client.tradeRequest = 0;
                    %victim.tradeSender = 0;
                    %victim.tradeRequest = 0;
                    %client.canoffer = 0;
                    %victim.canoffer = 0;
                    for(%i = 0; %i < %client.Offers; %i++)
                    {
                        %client.offerLocked[getfield(%client.tradeOffer[%i],0)] = 0;
                        %client.tradeOffer[%i] = "";
                    }
                    for(%i = 0; %i < %victim.Offers; %i++)
                    {
                        %victim.offerLocked[getfield(%victim.tradeOffer[%i],0)] = 0;
                        %victim.tradeOffer[%i] = "";
                    }
                    %client.offers = 0;
                    %victim.offers = 0;
                    %client.tradingfee = 0;
                    %victim.tradingfee = 0;
                    %client.tradingWith = 0;
                    %victim.tradingWith = 0;
                    return;
                }
                else if(%victim.tradingFee > %victim.money)
                {
                    %victim.chatmessage("you don't have enough money to finish the trade");
                    %victim.playsound(errorsound);
                    %client.chatmessage(%victim.name SPC "did not have enough money to finish the trade");
                    %client.playsound(errorsound);
                    %client.tradeConfirmed = 0;
                    %victim.tradeConfirmed = 0;
                    %client.tradeSender = 0;
                    %client.tradeRequest = 0;
                    %victim.tradeSender = 0;
                    %victim.tradeRequest = 0;
                    %client.canoffer = 0;
                    %victim.canoffer = 0;
                    for(%i = 0; %i < %client.Offers; %i++)
                    {
                        %client.offerLocked[getfield(%client.tradeOffer[%i],0)] = 0;
                        %client.tradeOffer[%i] = "";
                    }
                    for(%i = 0; %i < %victim.Offers; %i++)
                    {
                        %victim.offerLocked[getfield(%victim.tradeOffer[%i],0)] = 0;
                        %victim.tradeOffer[%i] = "";
                    }
                    %client.offers = 0;
                    %victim.offers = 0;
                    %client.tradingfee = 0;
                    %victim.tradingfee = 0;
                    %client.tradingWith = 0;
                    %victim.tradingWith = 0;
                    return;
                }
                else
                {
                    %client.chatmessage("\c5Trade successful!");
                    %client.playsound(beep_popup_sound);
                    %victim.chatmessage("\c5Trade successful!");
                    %victim.playsound(beep_popup_sound);
                    for(%i = 0; %i < %client.offers; %i++)
                    {
                        %client.inventory[getfield(%client.tradeoffer[%i],0)] -= getfield(%client.tradeoffer[%i],1);
                        %victim.inventory[getfield(%client.tradeoffer[%i],0)] += getfield(%client.tradeoffer[%i],1);
                    }
                    for(%i = 0; %i < %victim.offers; %i++)
                    {
                        %victim.inventory[getfield(%victim.tradeoffer[%i],0)] -= getfield(%victim.tradeoffer[%i],1);
                        %client.inventory[getfield(%victim.tradeoffer[%i],0)] += getfield(%victim.tradeoffer[%i],1);
                    }
                    %client.money -= mfloatlength(%client.tradingFee,3);
                    %victim.money -= mfloatlength(%victim.tradingFee,3);
                    %client.tradingfee = 0;
                    %victim.tradingfee = 0;
                    %client.tradeConfirmed = 0;
                    %victim.tradeConfirmed = 0;
                    %client.tradeSender = 0;
                    %client.tradeRequest = 0;
                    %victim.tradeSender = 0;
                    %victim.tradeRequest = 0;
                    %client.canoffer = 0;
                    %victim.canoffer = 0;
                    for(%i = 0; %i < %client.Offers; %i++)
                    {
                        %client.offerLocked[getfield(%client.tradeOffer[%i],0)] = 0;
                        %client.tradeOffer[%i] = "";
                    }
                    for(%i = 0; %i < %victim.Offers; %i++)
                    {
                        %victim.offerLocked[getfield(%victim.tradeOffer[%i],0)] = 0;
                        %victim.tradeOffer[%i] = "";
                    }
                    %client.offers = 0;
                    %victim.offers = 0;
                    %client.tradingWith = 0;
                    %victim.tradingWith = 0;
                }
            }
        }
    }
}

function gameconnection::tradeTimeout(%client, %victim)
{
    %client.playsound(errorsound);
    %victim.playsound(errorsound);
    %client.chatmessage("Trade request to" SPC %victim.name SPC "timed out.");
    %victim.chatmessage("Trade request from" SPC %client.name SPC "timed out.");
    %client.tradeConfirmed = 0;
    %victim.tradeConfirmed = 0;
    %client.tradeSender = 0;
    %client.tradeRequest = 0;
    %victim.tradeSender = 0;
    %victim.tradeRequest = 0;
    %client.tradingfee = 0;
    %victim.tradingfee = 0;
    for(%i = 0; %i < %client.Offers; %i++)
    {
        %client.offerLocked[getfield(%client.tradeOffer[%i],0)] = 0;
        %client.tradeOffer[%i] = "";
    }
    for(%i = 0; %i < %victim.Offers; %i++)
    {
        %victim.offerLocked[getfield(%victim.tradeOffer[%i],0)] = 0;
        %victim.tradeOffer[%i] = "";
    }
    %client.offers = 0;
    %victim.offers = 0;
    %client.tradingWith = 0;
    %victim.tradingWith = 0;
}