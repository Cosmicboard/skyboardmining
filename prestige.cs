function servercmdprestige(%client)
{
    if(%client.level < 20)
        return;
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
    %client.promptclient(1, "<font:Verdana Bold:18><color:fff000>All of your progress will be reset with the exception of your prestige skill tree." NL "<font:Verdana Bold:15><color:FF00FF>You will receive" SPC %points SPC "prestige points.", "0", %client, 4);
}

function servercmdprestigeshop(%client, %type)
{
    if(%client.player.ineventgamemode || %client.player.fightingdigger)
        return;
    %client.player.inbasement = 0;
    if(%type == 0)
    {
        %client.player.settransform(_prestigeshop.getposition());
        %client.playsound(printfiresound);
        %client.player.setwhiteout(0.4);
    }
    else if(%type == 1)
    {
        %client.player.settransform(_prestigeshopexit.getposition());
        %client.playsound(printfiresound);
        %client.player.setwhiteout(0.4);
    }
}

function gameconnection::enterPrestigeRoom(%client, %type)
{
    servercmdprestigeshop(%client, %type);
}

registeroutputevent("gameconnection", "enterPrestigeRoom", "LIST yes 0 no 1");

function gameconnection::respecprestigetoggle(%client, %type)
{
    if(!%client.respecprestige)
    {
        %client.chatmessage("\c6all of your upgrades will be converted into prestige points on next prestige (automatically disables after prestiging)");
        %client.respecprestige = 1;
        %client.playsound(beep_key_sound);
    }
    else
    {
        %client.chatmessage("\c6all of your upgrades will remain in place");
        %client.respecprestige = 0;
        %client.playsound(beep_key_sound);
    }
}

registeroutputevent("gameconnection", "respecPrestigeToggle");

function fixSaveFiles()
{
    %fw = new FileObject();
    for(%file = findfirstfile("config/server/mining/*/prestige.txt"); isfile(%file); %file = findnextfile("config/server/mining/*/prestige.txt"))
    {
        if(strchr(%file, "BACKUP 1") !$= "" || strchr(%file, "BACKUP 2") !$= "" || strchr(%file, "BACKUP 3") !$= ""  || strchr(%file, "dev") !$= "")
            continue;
        %fw.openForRead(%file);
        %points = 0;
        %prestige = %fw.readline();
        %prestigePoints = %fw.readline();
        %mpu = %fw.readline()/5;
        %mmu = mceil(%fw.readline()/0.05);
        %eu = %fw.readline()/0.1;
        %cu = %fw.readline()/0.1;
        %cdu = %fw.readline()/0.1;
        %slu = %fw.readline();
        %mmmu = %fw.readline()/0.1;
        %pu = %fw.readline()/0.15;
        %tu = mceil(%fw.readline()/0.45);
        %fw.readline();
        %tcd = mceil(%fw.readline()/2.5);
        %sod = %fw.readline();
        for(%i = 1; %i <= %mpu; %i++)
        {
            %points += %i;
        }
        for(%i = 1; %i <= %mmu; %i++)
        {
            %points += 1 + ((%i-1)*5) - (2 * (%i-1));
        }
        for(%i = 1; %i <= %eu; %i++)
        {
            %points += 1 + ((%i-1)*10/5);
        }
        for(%i = 1; %i <= %cu; %i++)
        {
            %points += 1 + ((%i-1)*10/5);
        }
        for(%i = 1; %i <= %cdu; %i++)
        {
            %points += mfloatlength(2 * (mpow((1+(%i-1)/10)*100,2.2))/2500,0)-10;
        }
        for(%i = 1; %i <= %slu; %i++)
        {
            %points += mpow(5,%i);
        }
        for(%i = 1; %i <= %mmmu; %i++)
        {
            %points += 1 + mfloatlength((mpow((%i-1)*10,1.15)/1.5),0);
        }
        for(%i = 1; %i <= %pu; %i++)
        {
            if(%i == 1)
                %points += 15;
            else
                %points += mfloatlength(15 + (%i-1)*0.15*35,0);
        }
        for(%i = 1; %i <= %tu; %i++)
        {
            %points += mfloatlength(15 + mpow((%i-1)*45,0.6)*1.5,0);
        }
        for(%i = 1; %i <= %tcd; %i++)
        {
            %points += mfloatlength(10 + mpow((%i-1)*2.5,0.8)*1.25,0);
        }
        for(%i = 1; %i <= %sod; %i++)
        {
            %points += 14 + mpow(4,1+%i);
        }
        if(%points > 0)
        {
            %fw.openForWrite(%file);
            %fw.writeline(%prestige);
            %fw.writeline(%points);
        }
    }
    %fw.close();
    %fw.delete();
}

function gameconnection::respecPrestigePoints(%client, %count)
{
    %mpu = %client.prestigeminingpower/5;
    %mmu = mfloatlength(%client.prestigeminingmultiplier/0.05,0);
    %eu= %client.prestigeexpbonus/0.05;
    %cu = %client.prestigecashbonus/0.05;
    %cdu = %client.prestigecratedrops/0.1;
    %slu = %client.prestigestartlevel;
    %mmmu = %client.prestigemaxmining/0.1;
    %pu = %client.prestigepointsbuff/0.15;
    %tu = mfloatlength(%client.tunnelerlavares/0.45,0);
    %tcd = mfloatlength(%client.tunnelercooldown/2.5,0);
    %sod = %client.prestigestartdepth;

    for(%i = 1; %i <= %mpu; %i++)
    {
        if(!%count)
        {
            %client.prestigeminingpower -= 5;
            if(%client.prestigeminingpower < 0)
                %client.prestigeminingpower = 0;
            %client.prestigepoints += 1 + mfloatlength((%client.prestigeminingpower/3),0);
        }
        else
            %points += 1 + mfloatlength((%i-1)*5/3,0);
    }
    for(%i = 1; %i <= %mmu; %i++)
    {
        if(!%count)
        {
            %client.prestigeminingmultiplier -= 0.05;
            if(%client.prestigeminingmultiplier < 0)
                %client.prestigeminingmultiplier = 0;
            %client.prestigepoints += 5 + mfloatlength(mpow((%client.prestigeminingmultiplier*450),1.05),0);
        }
        else
            %points += 5 + mfloatlength(mpow(((%i-1)*45),1.05),0);
    }
    for(%i = 1; %i <= %eu; %i++)
    {
        if(!%count)
        {
            %client.prestigeexpbonus -= 0.05;
            if(%client.prestigeexpbonus < 0)
                %client.prestigeexpbonus = 0;
            %client.prestigepoints += 1 + mfloatlength(mpow((%client.prestigeexpbonus*200/4),1.08),0);
        }
        else
            %points += 1 + mfloatlength(mpow(((%i-1)*20/4),1.08),0);
    }
    for(%i = 1; %i <= %cu; %i++)
    {
        if(!%count)
        {
            %client.prestigecashbonus -= 0.05;
            if(%client.prestigecashbonus < 0)
                %client.prestigecashbonus = 0;
            %client.prestigepoints += 1 + mfloatlength(mpow((%client.prestigecashbonus*200/4),1.08),0);
        }
        else
            %points += 1 + mfloatlength(mpow(((%i-1)*20/4),1.08),0);
    }
    for(%i = 1; %i <= %cdu; %i++)
    {
        if(!%count)
        {
            %client.prestigecratedrops -= 0.1;
            if(%client.prestigecratedrops < 0)
                %client.prestigecratedrops = 0;
            %client.prestigepoints += mfloatlength(2 * (mpow((1+%client.prestigecratedrops)*100,2.2))/2500,0)-10;
        }
        else
            %points += mfloatlength(2 * (mpow((1+(%i-1)/10)*100,2.2))/2500,0)-10;
    }
    for(%i = 1; %i <= %slu; %i++)
    {
        if(!%count)
        {
            %client.prestigestartlevel -= 1;
            if(%client.prestigestartlevel < 0)
                %client.prestigestartlevel = 0;
            %client.prestigepoints += mpow(5,1+%client.prestigestartlevel);
        }
        else
            %points += mpow(5,%i);
    }
    for(%i = 1; %i <= %mmmu; %i++)
    {
        if(!%count)
        {
            %client.prestigemaxmining -= 0.1;
            if(%client.prestigemaxmining < 0)
                %client.prestigemaxmining = 0;
            %client.prestigepoints += 1 + mfloatlength((mpow(%client.prestigemaxmining*175,1.15)/1.5),0);
        }
        else
            %points += 3 + mfloatlength((mpow((%i-1)*17.5,1.15)/1.5),0);
    }
    for(%i = 1; %i <= %pu; %i++)
    {
        if(!%count)
        {
            %client.prestigepointsbuff -= 0.15;
            if(%client.prestigepointsbuff < 0)
                %client.prestigepointsbuff = 0;
            %price = mfloatlength(mpow(15 + %client.prestigepointsbuff*35,1.08),0);
            if(%i == %pu)
                %price = 15;
            %client.prestigepoints += %price;
        }
        else
        {
            if(%i == 1)
                %points += 15;
            else
                %points += mfloatlength(mpow(15 + (%i-1)*0.15*35,1.08),0);
        }
    }
    for(%i = 1; %i <= %tu; %i++)
    {
        if(!%count)
        {
            %client.tunnelerlavares -= 0.45;
            if(%client.tunnelerlavares < 0)
                %client.tunnelerlavares = 0;
            %client.prestigepoints += mfloatlength(15 + mpow(%client.tunnelerlavares*100,0.6)*1.5,0);
        }
        else
            %points += mfloatlength(15 + mpow((%i-1)*45,0.6)*1.5,0);
    }
    for(%i = 1; %i <= %tcd; %i++)
    {
        if(!%count)
        {
            %client.tunnelercooldown -= 2.5;
            if(%client.tunnelercooldown < 0)
                %client.tunnelercooldown = 0;
            %client.prestigepoints += mfloatlength(10 + mpow(%client.tunnelercooldown,0.8)*1.25,0);
        }
        else
            %points += mfloatlength(10 + mpow((%i-1)*2.5,0.8)*1.25,0);
    }
    for(%i = 1; %i <= %sod; %i++)
    {
        if(!%count)
        {
            %client.prestigestartdepth -= 1;
            if(%client.prestigestartdepth < 0)
                %client.prestigestartdepth = 0;
            %client.prestigepoints += 14 + mpow(4,2+%client.prestigestartdepth);
        }
        else
            %points += 14 + mpow(4,1+%i);
    }
    if(%count)
        return %points;
}

function gameconnection::showPrestigeStats(%client, %option)
{
    if(%option == 0)
    {
        %price = 1 + mfloatlength((%client.prestigeminingpower/3),0);
        %client.centerprint("<font:arial bold:26>\c6+5 Bonus Mining Power" NL "\c3Price:" SPC %price NL "<font:arial bold:20>\c5You currently have:" SPC 10+mfloor(%client.prestigeminingpower) NL "\c2Prestige Points:" SPC %client.prestigepoints,1);
    }
    else if(%option == 1)
    {
        %price = 5 + mfloatlength(mpow((%client.prestigeminingmultiplier*450),1.05),0);
        %client.centerprint("<font:arial bold:26>\c6+5% Bonus Mining Multiplier" NL "\c3Price:" SPC %price NL "<font:arial bold:20>\c5You currently have:" SPC mfloor(%client.prestigeminingmultiplier*100) @ "%" NL "\c2Prestige Points:" SPC %client.prestigepoints,1);
    }
    else if(%option == 2)
    {
        %price = 1 + mfloatlength(mpow((%client.prestigeexpbonus*200/4),1.08),0);
        %client.centerprint("<font:arial bold:26>\c6+5% Bonus Exp Multiplier" NL "\c3Price:" SPC %price NL "<font:arial bold:20>\c5You currently have:" SPC mfloor(%client.prestigeexpbonus*100) @ "%" NL "\c2Prestige Points:" SPC %client.prestigepoints,1);
    }
    else if(%option == 3)
    {
        %price = 1 + mfloatlength(mpow((%client.prestigecashbonus*200/4),1.08),0);
        %client.centerprint("<font:arial bold:26>\c6+5% Bonus Cash Multiplier" NL "\c3Price:" SPC %price NL "<font:arial bold:20>\c5You currently have:" SPC mfloor(%client.prestigecashbonus*100) @ "%" NL "\c2Prestige Points:" SPC %client.prestigepoints,1);
    }
    else if(%option == 4)
    {
        %price = mfloatlength(2 * (mpow((1+%client.prestigecratedrops)*100,2.2))/2500,0)-10;
        %client.centerprint("<font:arial bold:26>\c6+10% Bonus Crate Drops" NL "\c3Price:" SPC %price NL "<font:arial bold:20>\c5You currently have:" SPC mfloor(%client.prestigecratedrops*100) @ "%" NL "\c2Prestige Points:" SPC %client.prestigepoints,1);
    }
    else if(%option == 5)
    {
        %price = mpow(5,1+%client.prestigestartlevel);
        %client.centerprint("<font:arial bold:26>\c6+1 Starting Base Level" NL "\c3Price:" SPC %price NL "<font:arial bold:20>\c5You currently have:" SPC mfloor(%client.prestigestartlevel) NL "\c2Prestige Points:" SPC %client.prestigepoints NL "\c0(MAX UP TO 5)",1);
    }
    else if(%option == 6)
    {
        %price = 3 + mfloatlength((mpow(%client.prestigemaxmining*175,1.15)/1.5),0);
        %client.centerprint("<font:arial bold:26>\c6+10% Max Mining Multiplier" NL "\c3Price:" SPC %price NL "<font:arial bold:20>\c5You currently have:" SPC 200 + mfloor(%client.prestigemaxmining*100) @ "%" NL "\c2Prestige Points:" SPC %client.prestigepoints,1);
    }
    else if(%option == 7)
    {
        %price = mfloatlength(mpow(15 + %client.prestigepointsbuff*35,1.08),0);
        if(%price == 19)
            %price = 15;
        %client.centerprint("<font:arial bold:26>\c6+15% Prestige Points from Prestiges" NL "\c3Price:" SPC %price NL "<font:arial bold:20>\c5You currently have:" SPC 100 + mfloor(%client.prestigepointsbuff*100) @ "%" NL "\c2Prestige Points:" SPC %client.prestigepoints NL "\c0(MAX UP TO 1000%)",1);
    }
    else if(%option == 8)
    {
        %price = mfloatlength(15 + mpow(%client.tunnelerlavares*100,0.6)*1.5,0);
        %client.centerprint("<font:arial bold:26>\c6-0.45% Decreased Tunneler Lava Damage" NL "\c3Price:" SPC %price NL "<font:arial bold:20>\c5You currently have:" SPC %client.tunnelerlavares @ "%" NL "\c2Prestige Points:" SPC %client.prestigepoints NL "\c0(MAX UP TO 4.5%)",1);
    }
    else if(%option == 9)
    {
        %price = mfloatlength(10 + mpow(%client.tunnelercooldown,0.8)*1.25,0);
        %client.centerprint("<font:arial bold:26>\c6-2.5s Decreased Tunneler Cooldown" NL "\c3Price:" SPC %price NL "<font:arial bold:20>\c5You currently have:" SPC 150 - %client.tunnelercooldown @ "s" NL "\c2Prestige Points:" SPC %client.prestigepoints NL "\c0(MAX UP TO 60s)",1);
    }
    else if(%option == 10)
    {
        %price = 14 + mpow(4,2+%client.prestigestartdepth);
        if(%client.prestigestartdepth == 0)
            %startdepth = 100;
        else if(%client.prestigestartdepth == 1)
            %startdepth = 200;
        else if(%client.prestigestartdepth == 2)
            %startdepth = 300;
        else if(%client.prestigestartdepth == 3)
            %startdepth = 400;
        else if(%client.prestigestartdepth >= 4)
            %startdepth = 600;
        %client.centerprint("<font:arial bold:26>\c6Increased Starting Optimal Depth" NL "\c3Price:" SPC %price NL "<font:arial bold:20>\c5You currently have:" SPC %startdepth @ "m" NL "\c2Prestige Points:" SPC %client.prestigepoints NL "\c0(MAX UP TO 600m)",1);
    }
}

registeroutputevent("gameconnection", "showPrestigeStats", "LIST miningpower 0 miningmultiplier 1 exp 2 cash 3 cratedrops 4 starterlevel 5 maxmining 6 prestigepoints 7 tunnelerlavares 8 tunnelercooldown 9 starterdepth 10");

function gameconnection::purchasePrestigeUpgrade(%client, %option)
{
    if(%option == 0)
    {
        %price = 1 + mfloatlength((%client.prestigeminingpower/3),0);
        if(%client.prestigepoints < %price)
        {
            %client.chatmessage("no money no funny");
            %client.playsound(errorsound);
            return;
        }
        %client.prestigepoints -= %price;
        %client.prestigeminingpower += 5;
        %client.chatmessage("\c2Successfully upgraded Bonus Mining Power to\c4" SPC 10+%client.prestigeminingpower @ "\c2!");
        %client.playsound(beep_key_sound);
    }
    else if(%option == 1)
    {
        %price = 5 + mfloatlength(mpow((%client.prestigeminingmultiplier*450),1.05),0);
        if(%client.prestigepoints < %price)
        {
            %client.chatmessage("no money no funny");
            %client.playsound(errorsound);
            return;
        }
        %client.prestigepoints -= %price;
        %client.prestigeminingmultiplier += 0.05;
        %client.chatmessage("\c2Successfully upgraded Bonus Mining Multiplier to\c4" SPC %client.prestigeminingmultiplier*100 @ "%%\c2!");
        %client.playsound(beep_key_sound);
    }
    else if(%option == 2)
    {
        %price = 1 + mfloatlength(mpow((%client.prestigeexpbonus*200/4),1.08),0);
        if(%client.prestigepoints < %price)
        {
            %client.chatmessage("no money no funny");
            %client.playsound(errorsound);
            return;
        }
        %client.prestigepoints -= %price;
        %client.prestigeexpbonus += 0.05;
        %client.chatmessage("\c2Successfully upgraded Bonus Exp Multiplier to\c4" SPC %client.prestigeexpbonus*100 @ "%%\c2!");
        %client.playsound(beep_key_sound);
    }
    else if(%option == 3)
    {
        %price = 1 + mfloatlength(mpow((%client.prestigecashbonus*200/4),1.08),0);
        if(%client.prestigepoints < %price)
        {
            %client.chatmessage("no money no funny");
            %client.playsound(errorsound);
            return;
        }
        %client.prestigepoints -= %price;
        %client.prestigecashbonus += 0.05;
        %client.chatmessage("\c2Successfully upgraded Bonus Cash Multiplier to\c4" SPC %client.prestigecashbonus*100 @ "%%\c2!");
        %client.playsound(beep_key_sound);
    }
    else if(%option == 4)
    {
        %price = mfloatlength(2 * (mpow((1+%client.prestigecratedrops)*100,2.2))/2500,0)-10;
        if(%client.prestigepoints < %price)
        {
            %client.chatmessage("no money no funny");
            %client.playsound(errorsound);
            return;
        }
        %client.prestigepoints -= %price;
        %client.prestigecratedrops += 0.1;
        %client.chatmessage("\c2Successfully upgraded Bonus Crate Drops to\c4" SPC %client.prestigecratedrops*100 @ "%%\c2!");
        %client.playsound(beep_key_sound);
    }
    else if(%option == 5)
    {
        if(%client.prestigestartlevel >= 5)
        {
            %client.chatmessage("reached maximum limit of 5");
            %client.playsound(errorsound);
            return;
        }
        %price = mpow(5,1+%client.prestigestartlevel);
        if(%client.prestigepoints < %price)
        {
            %client.chatmessage("no money no funny");
            %client.playsound(errorsound);
            return;
        }
        %client.level++;
        %miningmult = %client.level*0.1;
        if(%miningmult > 2 + %client.prestigemaxmining)
            %miningmult = 2 + %client.prestigemaxmining;
        %client.miningmultiplier += %miningmult;
        %client.prestigepoints -= %price;
        %client.prestigestartlevel += 1;
        %client.chatmessage("\c2Successfully upgraded Starting Base Level to\c4" SPC %client.prestigestartlevel @ "\c2!");
        %client.playsound(beep_key_sound);
    }
    else if(%option == 6)
    {
        %price = 3 + mfloatlength((mpow(%client.prestigemaxmining*175,1.15)/1.5),0);
        if(%client.prestigepoints < %price)
        {
            %client.chatmessage("no money no funny");
            %client.playsound(errorsound);
            return;
        }
        %client.prestigepoints -= %price;
        %client.prestigemaxmining += 0.1;
        %client.chatmessage("\c2Successfully upgraded Max Mining Multiplier to\c4" SPC 200+%client.prestigemaxmining*100 @ "%%\c2!");
        %client.playsound(beep_key_sound);
    }
    else if(%option == 7)
    {
        if(%client.prestigepointsbuff >= 9)
        {
            %client.chatmessage("reached maximum limit of 1000%%");
            %client.playsound(errorsound);
            return;
        }
        %price = mfloatlength(mpow(15 + %client.prestigepointsbuff*35,1.08),0);
        if(%price == 19)
            %price = 15;
        if(%client.prestigepoints < %price)
        {
            %client.chatmessage("no money no funny");
            %client.playsound(errorsound);
            return;
        }
        %client.prestigepoints -= %price;
        %client.prestigepointsbuff += 0.15;
        %client.chatmessage("\c2Successfully upgraded Prestige Points to\c4" SPC 100+%client.prestigepointsbuff*100 @ "%%\c2!");
        %client.playsound(beep_key_sound);
    }
    else if(%option == 8)
    {
        if(%client.tunnelerlavares >= 4.5)
        {
            %client.chatmessage("reached maximum limit of 4.5%%");
            %client.playsound(errorsound);
            return;
        }
        %price = mfloatlength(15 + mpow(%client.tunnelerlavares*100,0.6)*1.5,0);
        if(%client.prestigepoints < %price)
        {
            %client.chatmessage("no money no funny");
            %client.playsound(errorsound);
            return;
        }
        %client.prestigepoints -= %price;
        %client.tunnelerlavares += 0.45;
        %client.chatmessage("\c2Successfully upgraded Decreased Tunneler Lava Damage to\c4" SPC %client.tunnelerlavares @ "%%\c2!");
        %client.playsound(beep_key_sound);
    }
    else if(%option == 9)
    {
        if(%client.tunnelercooldown >= 90)
        {
            %client.chatmessage("reached maximum limit of 60s");
            %client.playsound(errorsound);
            return;
        }
        %price = mfloatlength(10 + mpow(%client.tunnelercooldown,0.8)*1.25,0);
        if(%client.prestigepoints < %price)
        {
            %client.chatmessage("no money no funny");
            %client.playsound(errorsound);
            return;
        }
        %client.prestigepoints -= %price;
        %client.tunnelercooldown += 2.5;
        %client.chatmessage("\c2Successfully upgraded Decreased Tunneler Cooldown to\c4" SPC %client.tunnelercooldown @ "s\c2!");
        %client.playsound(beep_key_sound);
    }
    else if(%option == 10)
    {
        if(%client.prestigestartdepth >= 4)
        {
            %client.chatmessage("reached maximum limit of 500m");
            %client.playsound(errorsound);
            return;
        }
        %price = 14 + mpow(4,2+%client.prestigestartdepth);
        if(%client.prestigepoints < %price)
        {
            %client.chatmessage("no money no funny");
            %client.playsound(errorsound);
            return;
        }
        %client.prestigestartdepth++;
        if(%client.prestigestartdepth == 1)
        {
            %startdepth = 200;
            %client.optimaldepth = 200;
        }
        else if(%client.prestigestartdepth == 2)
        {
            %startdepth = 300;
            %client.optimaldepth = 300;
        }
        else if(%client.prestigestartdepth == 3)
        {
            %startdepth = 400;
            %client.optimaldepth = 400;
        }
        else if(%client.prestigestartdepth == 4)
        {
            %startdepth = 600;
            %client.optimaldepth = 600;
        }
        %client.prestigepoints -= %price;
        %client.chatmessage("\c2Successfully upgraded Increased Starting Optimal Depth to\c4" SPC %startdepth @ "m\c2!");
        %client.playsound(beep_key_sound);
    }
}

registeroutputevent("gameconnection", "purchasePrestigeUpgrade", "LIST miningpower 0 miningmultiplier 1 exp 2 cash 3 cratedrops 4 starterlevel 5 maxmining 6 prestigepoints 7 tunnelerlavares 8 tunnelercooldown 9 starterdepth 10");