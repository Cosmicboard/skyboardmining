function servercmdspawn(%client)
{
    if(%client.player.ineventgamemode || %client.player.fightingdigger || %client.cdplayer)
        return;
    %client.player.inbasement = 0;
    %client.player.settransform("9.5 -40 5005");
    %client.playsound(printfiresound);
    %client.player.setwhiteout(0.4);
}

function player::teleportTo(%player, %tp)
{
    if(%tp == 5)
    {
        %player.inbasement = 1;
        %player.settransform(_basement.position);
        %player.client.playsound(printfiresound);
        %player.setwhiteout(0.6);
        return;
    }
    if($sim::time < $startertime + 10)
    {
        %player.client.centerprint("<font:arial:25>\c0hold on the spawns are still generating",3);
        %player.setvelocity("0 -15 10");
        %player.client.playsound(floatingplanterrorsound);
		return;
    }
    if(%tp == 0 && %player.client.past500)
    {
        %player.settransform("9.5 9.5 4492");
        %player.client.playsound(printfiresound);
        %player.setwhiteout(0.6);
        return;
    }
    else if(%tp == 0 && !%player.client.past500)
    {
        %player.client.centerprint("<font:arial:25>\c0you did not reach the 1000m spawn to be able to enter this teledoor",3);
        %player.setvelocity("0 -15 10");
        %player.client.playsound(floatingplanterrorsound);
		return;
    }

    if(%tp == 1 && %player.client.past1000)
    {
        %player.settransform("9.5 9.5 3992");
        %player.client.playsound(printfiresound);
        %player.setwhiteout(0.6);
        return;
    }
    else if(%tp == 1 && !%player.client.past100)
    {
        %player.client.centerprint("<font:arial:25>\c0you did not reach the 1500m spawn to be able to enter this teledoor",3);
        %player.setvelocity("0 -15 10");
		%player.client.playsound(floatingplanterrorsound);
		return;
    }

    if(%tp == 2 && %player.client.past1500)
    {
        %player.settransform("9.5 9.5 3492");
        %player.client.playsound(printfiresound);
        %player.setwhiteout(0.6);
        return;
    }
    else if(%tp == 2 && !%player.client.past1500)
    {
        %player.client.centerprint("<font:arial:25>\c0you did not reach the 2250m spawn to be able to enter this teledoor",3);
        %player.setvelocity("0 -15 10");
		%player.client.playsound(floatingplanterrorsound);
		return;
    }

    if(%tp == 3 && %player.client.past2250)
    {
        %player.settransform("9.5 9.5 2742");
        %player.client.playsound(printfiresound);
        %player.setwhiteout(0.6);
        return;
    }
    else if(%tp == 3 && !%player.client.past2250)
    {
        %player.client.centerprint("<font:arial:25>\c0you did not reach the 3000m spawn to be able to enter this teledoor",3);
        %player.setvelocity("0 -15 10");
		%player.client.playsound(floatingplanterrorsound);
		return;
    }

    if(%tp == 4 && %player.client.past3000)
    {
        %player.settransform("9.5 9.5 1992");
        %player.client.playsound(printfiresound);
        %player.setwhiteout(0.6);
        return;
    }
    else if(%tp == 4 && !%player.client.past3000)
    {
        %player.client.centerprint("<font:arial:25>\c0you did not reach the 4000m spawn to be able to enter this teledoor",3);
        %player.setvelocity("0 -15 10");
		%player.client.playsound(floatingplanterrorsound);
		return;
    }

    if(%tp == 6 && %player.client.past4000)
    {
        %player.settransform("9.5 9.5 992");
        %player.client.playsound(printfiresound);
        %player.setwhiteout(0.6);
        return;
    }
    else if(%tp == 6 && !%player.client.past4000)
    {
        %player.client.centerprint("<font:arial:25>\c0you did not reach the 5000m spawn to be able to enter this teledoor",3);
        %player.setvelocity("0 -15 10");
		%player.client.playsound(floatingplanterrorsound);
		return;
    }

    if(%tp == 7 && %player.client.past5000)
    {
        %player.settransform("9.5 9.5 -8");
        %player.client.playsound(printfiresound);
        %player.setwhiteout(0.6);
        return;
    }
    else if(%tp == 7 && !%player.client.past5000)
    {
        %player.client.centerprint("<font:arial:25>\c0you did not reach the 6250m spawn to be able to enter this teledoor",3);
        %player.setvelocity("0 -15 10");
		%player.client.playsound(floatingplanterrorsound);
		return;
    }
}

registeroutputevent("Player", "teleportTo", "LIST 500m 0 1000m 1 1500m 2 2250m 3 3000m 4 room 5 4000m 6 5000m 7");

function eulerToAxis(%euler)
{
	%euler = VectorScale(%euler,$pi / 180);
	%matrix = MatrixCreateFromEuler(%euler);
	return getWords(%matrix,3,6);
}

function oreIDfromName(%name)
{
    for(%i = 0; %i < $orecount; %i++)
    {
        if(getfield($ore[%i],0) $= %name)
        {
            return %i;
        }
    }
    return -1;
}

function addbrick(%position, %type, %extra, %orevein)
{
    if(getword(%position,2) > 5500)
    {
        %position = getwords(%position,0,1) SPC getword(%position,2)-1;
        %extra = "IGNORE";
    }
    if(%extra !$= "IGNORE")
    {
        if(getword(%position,1) < 40 && getword(%position,1) > -53 && getword(%position,0) > -21 && getword(%position,0) < 41 && getword(%position,2) > 5000 && getword(%position,2) < 5020)
            return;
        if(getword($diggedPosition[%position],0) == 1)
            return;
    }
    if(%type == oreidfromname("Quantum Disruption"))
        %mult = (5000-getword(%position,2)-3500)/750;
    else if(%type == oreidfromname("Atomic Disruption"))
        %mult = (5000-getword(%position,2)-5000)/2500;
    else if(%type == oreidfromname("Forbidden Key"))
        %mult = (5000-getword(%position,2)-1000)/175;
    else if(%type == oreidfromname("Challenger's Key"))
        %mult = (5000-getword(%position,2)-1000)/125;
    else if(%type == oreidfromname("Lovely Crate"))
        %mult = (5000-getword(%position,2)-2500)/200;
    if(%orevein)
    {
        for(%i = 0; %i < 5; %i++)
        {
            if(%i==0)
                %end="0 0 -1";
            else if(%i==1)
                %end="0 0 1";
            else if(%i==2)
                %end="0 1 0";
            else if(%i==3)
                %end="0 -1 0";
            else if(%i==4)
                %end="1 0 0";
            else if(%i==5)
                %end="-1 0 0";
            %ray = containerraycast(%position, vectoradd(%position, %end), $typemasks::fxbrickalwaysobjecttype, 0);
            if(getfield($ore[%ray.oreid],9) > 10)
                %maxvein = 12;
            else if(getfield($ore[%ray.oreid],9) > 6)
                %maxvein = 8;
            else if(getfield($ore[%ray.oreid],9) > 3)
                %maxvein = 7;
            else if(getfield($ore[%ray.oreid],9) > 2)
                %maxvein = 4;
            if(getfield($ore[%ray.oreid],12) !$= "ignore" && getfield($ore[%ray.oreid],9) > 2 && %ray.vein < %maxvein)
            {
                if(getfield($ore[%ray.oreid],9) > 10 && getrandom(1,7+%ray.vein) == 1)
                {%type = %ray.oreid;%vein = %ray.vein+1;break;}
                else if(getfield($ore[%ray.oreid],9) > 6 && getrandom(1,9+%ray.vein) == 1)
                {%type = %ray.oreid;%vein = %ray.vein+1;break;}
                else if(getfield($ore[%ray.oreid],9) > 3 && getrandom(1,12+%ray.vein*2) == 1)
                {%type = %ray.oreid;%vein = %ray.vein+1;break;}
                else if(getfield($ore[%ray.oreid],9) > 2 && getrandom(1,15+%ray.vein*3) == 1)
                {%type = %ray.oreid;%vein = %ray.vein+1;break;}
            }
        }
    }
    %brick = new fxdtsbrick()
    {
        vein = %vein;
        datablock = brick2xcubeprintdata;
        position = %position;
        colorID = getfield($ore[%type],5);
		angleID = 0;
		colorfxID = getfield($ore[%type],6);
		shapefxID = getfield($ore[%type],7);
		isPlanted = 1;
        stackBL_ID = 999999;
        printid = $printnametable[getfield($ore[%type],8)];
        health = getfield($ore[%type],1) * (1 + %mult);
        canMine = 1;
        oreID = oreIDfromName(getfield($ore[%type],0));
        indestructible = getfield($ore[%type],13);
    }; 
    if(5000-getword(%position,2) > $maximumDepth)
    {
        %mult += mabs((1250+getword(%position,2)))/888;
        //%brick.customValue = getfield($ore[%type],2) * (1 + %mult);
        %brick.customEXP = mfloatlength(getfield($ore[%type],3) * (1 + %mult*1.05),0);
    }
    if(%mult > 0)
    {
        %brick.maxhealth = getfield($ore[%type],1) * (1 + %mult); 
        %brick.health = %brick.maxhealth;
    }
    nameToID("brickGroup_888888").add(%brick);
    %brick.setTrusted(1);
    $diggedposition[%position] = 1;
    if(getfield($ore[%type],12) $= "COMPRESSEDORE")
    {
        %brick.compressedOre = 1;
        if(5000.2-getword(%position,2) > 1250)
            %random = getrandom(1,7);
        else if(5000.2-getword(%position,2) > 1000)
            %random = getrandom(1,6);
        else
            %random = getrandom(1,4);
        if(%random == 1)
        {
            %ore = getrandom(10,20);
            %brick.setcolor(16);
            %brick.compressedOreType = "Coal";
            %brick.compressedOreAmount = %ore;
            %brick.compressedOreEXP = %ore * 8;
            %brick.compressedOreValue = %ore * 5;
            %brick.compressedOreHealth = %ore * 215;
        }
        else if(%random == 2)
        {
            %ore = getrandom(9,18);
            %brick.setcolor(11);
            %brick.compressedOreType = "Tin";
            %brick.compressedOreAmount = %ore;
            %brick.compressedOreEXP = %ore * 12;
            %brick.compressedOreValue = %ore * 6;
            %brick.compressedOreHealth = %ore * 245;
        }
        else if(%random == 3)
        {
            %ore = getrandom(8,14);
            %brick.setcolor(12);
            %brick.compressedOreType = "Iron";
            %brick.compressedOreAmount = %ore;
            %brick.compressedOreEXP = %ore * 15;
            %brick.compressedOreValue = %ore * 8;
            %brick.compressedOreHealth = %ore * 305;
        }
        else if(%random == 4)
        {
            %ore = getrandom(6,12);
            %brick.setcolor(15);
            %brick.setcolorfx(1);
            %brick.compressedOreType = "Copper";
            %brick.compressedOreAmount = %ore;
            %brick.compressedOreEXP = %ore * 20;
            %brick.compressedOreValue = %ore * 10;
            %brick.compressedOreHealth = %ore * 365;
        }
        else if(%random == 5)
        {
            %ore = getrandom(6,12);
            %brick.setcolor(13);
            %brick.setcolorfx(1);
            %brick.compressedOreType = "Silver";
            %brick.compressedOreAmount = %ore;
            %brick.compressedOreEXP = %ore * 35;
            %brick.compressedOreValue = %ore * 14;
            %brick.compressedOreHealth = %ore * 425;
        }
        else if(%random == 6)
        {
            %ore = getrandom(6,12);
            %brick.setcolor(18);
            %brick.compressedOreType = "Gold";
            %brick.compressedOreAmount = %ore;
            %brick.compressedOreEXP = %ore * 45;
            %brick.compressedOreValue = %ore * 20;
            %brick.compressedOreHealth = %ore * 645;
        }
        else if(%random == 7)
        {
            %ore = getrandom(6,12);
            %brick.setcolor(22);
            %brick.compressedOreType = "Aluminium";
            %brick.compressedOreAmount = %ore;
            %brick.compressedOreEXP = %ore * 85;
            %brick.compressedOreValue = %ore * 40;
            %brick.compressedOreHealth = %ore * 1485;
        }
        %brick.health = %brick.compressedOreHealth;
    }
	%brick.plant();
    //if(%err == 1 || %err == 3)
    //{
        //%brick.delete();
        //return;
    //}
    if(brickgroup_888888.getcount() >= 690000 && !$resettingmines)
    {
        $resettingmines = 1;
        announcemessage("\c5resetting the mines lol");
        schedule(1000, 0, starterplatform);
        for(%i = 0; %i < clientgroup.getcount(); %i++)
        {
            %client = clientgroup.getobject(%i);
                schedule(1000, 0, servercmdspawn, %client);
        }
        return;
    }
    $totalbricks++;
    if($totalbricks >= 10000)
    {
        announcemessage("\c6Mines status update:\c0" SPC 690000-brickgroup_888888.getcount() SPC "(" @ 100-mfloatlength(brickgroup_888888.getcount()/690000,4)*100 @ "%%)" SPC "blocks\c6 are remaining.");
        $totalbricks = 0;
    }
    return %brick;
}

function placebricksaround(%position, %depth) //deprecated STOP FUCKING TRYING TO USE IT HOLY SHIT I CAN'T REMEMBER
{
    %x = getword(%position,0);
    %y = getword(%position,1);
    %z = getword(%position,2);
    %pos[0] = %x+1 SPC %y SPC %z;
    %pos[1] = %x-1 SPC %y SPC %z;
    %pos[2] = %x SPC %y+1 SPC %z;
    %pos[3] = %x SPC %y-1 SPC %z;
    %pos[4] = %x SPC %y SPC %z+1;
    %pos[5] = %x SPC %y SPC %z-1;

    
    for(%i = 0; %i < 6; %i++)
    {
        if(getword(%pos[%i],0) > -21 && getword(%pos[%i],0) < 40 && getword(%pos[%i],2) > 5001.2)
            continue;
        if(getword(%pos[%i],1) > 39 && getword(%pos[%i],1) < -52 && getword(%pos[%i],2) > 5001.2)
            continue;
        if($diggedPosition[%pos[%i]] == 0)
        {
            if(%depth > 2250)
                %success = 93;
            else if(%depth > 1000)
                %success = 94;
            else
                %success = 95;
            if(getrandom(1,100) >= %success) 
            {
                %generate = rollOre(%depth);
                if(%generate $= "0")
                    %generate = "Dirt";
            }
            else if(%depth < -100)
                %generate = "Grass";
            else if(%depth < 200 && %depth > -100)
                %generate = "Dirt";
            else if(%depth < 500 && %depth > 200)
                %generate = "Dense Dirt";
            else if(%depth < 1000 && %depth > 500)
                %generate = "Stone";
            else if(%depth < 1500 && %depth > 1000)
                %generate = "Dense Stone";
            else if(%depth < 2250 && %depth > 1500)
                %generate = "Bedrock";
            else if(%depth < 3000 && %depth > 2250)
                %generate = "Mantle";
            else if(%depth < 4000 && %depth > 3000)
                %generate = "Core";
            else if(%depth > 4000 && %depth < 5000)
                %generate = "Netherrack";
            else if(%depth > 5000)
                %generate = "Voidstone";

            if(%depth >= 500 && %depth < 5000 && getrandom(1,100) >= 97)
                %generate = "Lava";
            //else if(%depth >= 1000 && %depth < 1500 && getrandom(1,100) >= 96)
                //%generate = "Lava";
            //else if(%depth >= 1500 && %depth < 5000 && getrandom(1,100) >= 95)
                //%generate = "Lava";
            else if(%depth >= 5000 && getrandom(1,100) >= 97)
                %generate = "Black Hole";

            if(%depth >= 6250 && getrandom(1,1500) == 1)
                %generate = "Void Gem";
            if(%depth <= 5000 && getrandom(1,25000) == 1)
                %generate = "Lovely Crate";
            if(%depth >= 1000 && getrandom(1,75000) == 1)
                %generate = "Forbidden Key";
            if(%depth >= 2250 && getrandom(1,250000) == 1)
                %generate = "Challenger's Key";
            addbrick(%pos[%i], oreIDfromName(%generate));
        }
    }
}

function starterplatform(%ignore)
{
    for(%i = 0; %i < clientgroup.getcount(); %i++)
    {
        %client = clientgroup.getobject(%i);
        if(%client.minigame)
            servercmdspawn(%client);
    }
    $totalbricks = 0;
    $startertime = $sim::time;
    generateperlin();
    brickgroup_888888.deleteall();
    deletevariables("$diggedposition*");
    schedule(10000, 0, resettingminesoff);
    %starterposition = "0 0 5000";
    for(%i = 0; %i < 400; %i++)
    {
        %inc++;
        if(%inc < 20)
        {
            addbrick(vectoradd(%starterposition, %offsetX SPC %offsetY SPC 0), oreIDfromName("Dirt"));
            %offsetX += 1;
        }
        else
        {
            %inc = 0;
            addbrick(vectoradd(%starterposition, %offsetX SPC %offsetY SPC 0), oreIDfromName("Dirt"));
            %offsetX = 0;
            %offsetY += 1;
        }
    }
    if(!%ignore)
    {
        loadcave("starterplatform", "9 9.5 4490.1");
        loadcave("starterplatform", "9 9.5 3990.1");
        loadcave("starterplatform", "9 9.5 3490.1");
        loadcave("starterplatform", "9 9.5 2740.1");
        loadcave("starterplatform", "9 9.5 2740.1");
        loadcave("starterplatform", "9 9.5 1990.1");
        loadcave("starterplatform", "9 9.5 990.1");
        loadcave("starterplatform", "9 9.5 -9.9");
        loadcave("starterplatform", "9 9.5 -1259.9");
    }
    for(%i = 0; %i < cavegroup.getcount(); %i++)
    {
        %object = cavegroup.getobject(%i);
        if(%object.torch)
            deletetorch(%object);
    }
}

function resettingminesoff()
{
    $resettingmines = 0;
}

function gameconnection::addmoney(%client, %amount)
{
    %client.money+=%amount;
    %client.totalmoney+=%amount;
    %total = %client.totalmoney;
    if(%total >= 100)
        %client.unlockachievement("we're rich");
    if(%total >= 500)
        %client.unlockachievement("middle class");
    if(%total >= 2500)
        %client.unlockachievement("money hungry diggers");
    if(%total >= 10000)
        %client.unlockachievement("it grows in dirt");
    if(%total >= 100000)
        %client.unlockachievement("wealthy digger");
    if(%total >= 1000000)
        %client.unlockachievement("millionaire");
    if(%total >= 250000000)
        %client.unlockachievement("digging inflation");
    if(%total >= 1000000000)
        %client.unlockachievement("jeff digos");
    if(%total >= "1e+12")
        %client.unlockachievement("i own the universe");
    %client.totalmoneythisprestige+=%amount;
}

function gameconnection::addexp(%client, %amount, %ignore)
{
    %client.totalexp+=%amount;
    %client.totalexpthisprestige+=%amount;
    %level = %client.level - %client.prestigestartlevel;
    if(%level == 0)
        %req = 100;
    else if(%level <= 5)
        %req = mfloatlength(mpow(%level * 25,2),0);
    else if(%level <= 10)
        %req = mfloatlength(mpow(%level * 30,2),0);
    else if(%level <= 15)
        %req = mfloatlength(mpow(%level * 37.5,2),0);
    else if(%level <= 20)
        %req = mfloatlength(mpow(%level * 45,2),0);
    else if(%level <= 25)
        %req = mfloatlength(mpow(%level * 52.5,2.1),0);
    else if(%level <= 30)
        %req = mfloatlength(mpow(%level * 58.5,2.2),0);
    else if(%level <= 35)
        %req = mfloatlength(mpow(%level * 61.5,2.25),0);
    else if(%level <= 40)
        %req = mfloatlength(mpow(%level * 75,2.325),0);
    else if(%level <= 45)
        %req = mfloatlength(mpow(%level * 77.5,2.34),0);
    else if(%level <= 50)
        %req = mfloatlength(mpow(%level * 80,2.36),0);
    else
        %req = mfloatlength(mpow(%level * 82.5,2.375),0);
    if(%ignore)
        %client.exp+=%amount;
    else
        %client.exp+=%amount*(1+%client.prestigeexpbonus)*(1+%client.achievementexpbonus);
    while(%client.exp >= %req)
    {
        if(%level == 0)
            %req = 100;
        else if(%level <= 5)
            %req = mfloatlength(mpow(%level * 25,2),0);
        else if(%level <= 10)
            %req = mfloatlength(mpow(%level * 30,2),0);
        else if(%level <= 15)
            %req = mfloatlength(mpow(%level * 37.5,2),0);
        else if(%level <= 20)
            %req = mfloatlength(mpow(%level * 45,2),0);
        else if(%level <= 25)
            %req = mfloatlength(mpow(%level * 52.5,2.1),0);
        else if(%level <= 30)
            %req = mfloatlength(mpow(%level * 58.5,2.2),0);
        else if(%level <= 35)
            %req = mfloatlength(mpow(%level * 61.5,2.25),0);
        else if(%level <= 40)
            %req = mfloatlength(mpow(%level * 75,2.325),0);
        else if(%level <= 45)
            %req = mfloatlength(mpow(%level * 77.5,2.34),0);
        else if(%level <= 50)
            %req = mfloatlength(mpow(%level * 80,2.36),0);
        else
            %req = mfloatlength(mpow(%level * 82.5,2.375),0);
        if(%client.exp < %req)
            return;
        %client.exp-=%req;
        %client.level++;
        %total = %client.level;
        if(%total >= 1)
            %client.unlockachievement("LEVEL milestone - 1");
        if(%total >= 3)
            %client.unlockachievement("LEVEL milestone - 3");
        if(%total >= 5)
            %client.unlockachievement("LEVEL milestone - 5");
        if(%total >= 10)
            %client.unlockachievement("LEVEL milestone - 10");
        if(%total >= 15)
            %client.unlockachievement("LEVEL milestone - 15");
        if(%total >= 20)
            %client.unlockachievement("LEVEL milestone - 20");
        if(%total >= 25)
            %client.unlockachievement("LEVEL milestone - 25");
        if(%total >= 30)
            %client.unlockachievement("LEVEL milestone - 30");
        if(%total >= 35)
            %client.unlockachievement("LEVEL milestone - 35");
        if(%total >= 40)
            %client.unlockachievement("LEVEL milestone - 40");
        if(%total >= 45)
            %client.unlockachievement("LEVEL milestone - 45");
        if(%total >= 50)
            %client.unlockachievement("LEVEL milestone - 50");
        %level = %client.level - %client.prestigestartlevel;
        %miningmult = %client.level*0.1;
        if(%miningmult > 2 + %client.prestigemaxmining)
            %miningmult = 2 + %client.prestigemaxmining;
        %client.miningmultiplier += %miningmult;
        %client.chatmessage("\c3Your level has been increased to [\c4" @ %client.level @ "\c3]! \c2+" @ %miningmult*100 @ "%% \c3mining power.");
        serverplay3d(rewardsound, %client.player.getposition());
        if(%client.level == 20)
            %client.chatmessage("\c3You are now eligible to prestige by typing \c4/prestige\c3!");
    }
}

function gameconnection::displayhud(%client)
{
    if(%client.player)
    {
        if(%client.prestigestartlevel >= 1)
            %client.unlockachievement("cheat the system");
        if(!%client.achievementunlocked["professional geologist"])
        {
            if(%client.inventory["Dirt"] && %client.inventory["Dense Dirt"] && %client.inventory["Stone"] && %client.inventory["Dense Stone"] && %client.inventory["Grass"]
             && %client.inventory["Bedrock"] && %client.inventory["Mantle"] && %client.inventory["Core"] && %client.inventory["Netherrack"]
              && %client.inventory["Voidstone"]){%client.unlockachievement("professional geologist");}
        }
        for(%i = 0; %i < %client.player.getdatablock().maxtools; %i++)
        {
            if(%client.player.tool[%i])
            {
                %item = %client.player.tool[%i].getname();
                %item = getsubstr(%item,0,10);
            }
            if(%client.player.tool[%i] && %item $= "rpgpickaxe")
            {
                %client.miningpower = %client.player.tool[%i].miningpower;
                break;
            }
            else
                %client.miningpower = 10;
        }
        %pos = 5000.2-getword(%client.player.getposition(),2);
        if(%pos < -100 && %pos > -600)
        {
            %client.unlockachievement("touch grass");
            %depth = "<color:32CD32>[" @ mfloatlength(%pos,0) @ "m]";
        }
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
        %depth2 = mfloatlength(5000.2-getword(%client.player.getposition(),2),0);
        if(!%client.ignoremusic)
        {
            if(%client.player.fightingdigger)
            {
                if(!$challengeDigger && %client.musicemitter.profile !$= "musicdata_youaposre_mine")
                    %client.setmusic("musicdata_youaposre_mine", 1);
                else if($challengeDigger && !%client.player.challengeDiggerMusic)
                {
                    %client.player.challengeDiggerMusic = 1;
                    %client.setmusic("musicdata_unmaykr_obliteration_intro", 1);
                    %client.diggerMusic = %client.schedule(20776, setmusic, "musicdata_unmaykr_obliteration_climax", 1);
                }
            }
            if(%client.player.fightingblockhead)
            {
                
            }
            else if(%client.player.ineventgamemode && !%client.player.fightingdigger || getword(%client.player.position,2) > 5750 && !%client.player.fightingdigger)
                %client.setmusic("", 1);
            else if(!%client.player.fightingdigger)
            {
                cancel(%client.diggerMusic);
                %client.player.challengeDiggerMusic = 0;
                if(%depth2 >= 5000 && %client.musicemitter.profile !$= "musicdata_terraria_calamity_mod_dash_rlyeh")
                {
                    %client.setmusic("musicdata_terraria_calamity_mod_dash_rlyeh", 1);
                }
                if(%depth2 >= 4000 && %depth2 < 5000 && %client.musicemitter.profile !$= "musicdata_terraria_calamity_mod_dash_the_step_below_hell")
                {
                    %client.setmusic("musicdata_terraria_calamity_mod_dash_the_step_below_hell", 1);
                }
                else if(%depth2 >= 3000 && %depth2 < 4000 && %client.musicemitter.profile !$= "musicdata_terraria_dash_underworld")
                {
                    %client.setmusic("musicdata_terraria_dash_underworld", 1);
                }
                else if(%depth2 >= 2250 && %depth2 < 3000 && %client.musicemitter.profile !$= "musicdata_terraria_calamity_mod_dash_the_abyss")
                {
                    %client.setmusic("musicdata_terraria_calamity_mod_dash_the_abyss", 1);
                }
                else if(%depth2 >= 1000 && %depth2 < 2250 && %client.musicemitter.profile !$= "musicdata_terraria_dash_alternate_underground")
                {
                    %client.setmusic("musicdata_terraria_dash_alternate_underground", 1);
                }
                else if(%depth2 >= 500 && %depth2 < 1000 && %client.musicemitter.profile !$= "musicdata_terraria_dash_underground" && %client.musicemitter.profile !$= "musicdata_terraria_dash_underground_but_wrong")
                {
                    if(getrandom(1,100) == 1)
                        %client.setmusic("musicdata_terraria_dash_underground_but_wrong", 1);
                    else
                        %client.setmusic("musicdata_terraria_dash_underground", 1);
                }
                else if(%depth2 >= 200 && %depth2 < 500 && %client.musicemitter.profile !$= "musicdata_terraria_dash_overworld_night")
                {
                    %client.setmusic("musicdata_terraria_dash_overworld_night", 1);
                }
                else if(%depth2 < 200 && %client.musicemitter.profile !$= "musicdata_terraria_dash_overworld_day")
                //halloween - else if(%depth2 < 500 && %client.musicemitter.profile !$= "musicdata_solar_eclipse_lobby")
                //christmas - else if(%depth2 < 500 && %client.musicemitter.profile !$= "musicdata_frost_invasion_lobby")
                {
                    %client.setmusic("musicdata_terraria_dash_overworld_day", 1);
                    //halloween -%client.setmusic("musicdata_solar_eclipse_lobby", 1);
                    //christmas - %client.setmusic("musicdata_frost_invasion_lobby", 1);
                }
            }
        }
        if(%depth2 > %client.optimaldepth)
        {
            if(%depth2 < 6250 && %client.optimaldepth < 6500)
            {
                if(!%client.reachedoptimaldepth)
                {
                    %client.chatmessage("You have reached your optimal depth. You can feel your pickaxe becoming weaker.");
                    %client.reachedoptimaldepth = 1;
                }
                %reachedoptimaldepth = "\c0";
                %powerloss = 0.15 + ((%depth2/%client.optimaldepth)*0.275);
                if(%powerloss > 0.85)
                    %powerloss = 0.85;
                %client.miningpowerloss = %powerloss;
            }
        }
        else if(%depth2 <= %client.optimaldepth)
        {
            %client.reachedoptimaldepth = 0;
            %reachedoptimaldepth = "\c6";
            %client.miningpowerloss = 0;
        }
        if(%depth2 < 500)
        {
            %client.player.mood -= 0.33;
            if(%client.player.mood < 0)
                %client.player.mood = 0;
            %nolight = "";
            %client.player.torchLoss = 0;
        }
        else if(%depth2 >= 500 && %depth2 < 3500 && !%client.player.inlight || %depth2 >= 5000 && !%client.player.inlight)
        {
            if(%client.player.severelight || isobject(%client.player.mininghelmet))
                %client.player.mood += 0.25;
            else
                %client.player.mood++;
            if(%client.player.mood >= getrandom(350,700))
            {
                %sound = getrandom(1,13);
                %client.playsound(cave @ %sound);
                %client.playsound(cave @ %sound);
                %client.player.mood = 0;
            }
            if(%depth2 >= 5000)
                %client.player.torchLoss = 0.85;
            else if(%depth2 >= 3000)
                %client.player.torchLoss = 0.75;
            else if(%depth2 >= 2250)
                %client.player.torchLoss = 0.66;
            else if(%depth2 >= 1500)
                %client.player.torchLoss = 0.50;
            else if(%depth2 >= 1000)
                %client.player.torchLoss = 0.33;
            else
                %client.player.torchLoss = 0.25;
            if(%client.player.severelight || isobject(%client.player.mininghelmet))
            {
                if(%depth2 < 5000)
                    %client.player.torchloss /= 3;
                else
                    %client.player.torchloss /= 2;
                %nolight = " \c2(HELMET LIGHT)";
            }
            else
                %nolight = " \c0(NOT IN LIGHT)";
        }
        else if(%depth2 >= 500 && %depth2 < 3500 && %client.player.inlight || %depth2 >= 5000 && %client.player.inlight)
        {
            %client.player.mood -= 0.05;
            if(%client.player.mood < 0)
                %client.player.mood = 0;
            %client.player.torchLoss = 0;
            %nolight = "";
        }
        else if(%depth2 >= 3500 && %depth2 < 5000 && !%client.player.incold)
        {
            %client.player.mood++;
            if(%client.player.mood >= getrandom(350,700))
            {
                %sound = getrandom(1,13);
                %client.playsound(cave @ %sound);
                %client.playsound(cave @ %sound);
                %client.player.mood = 0;
            }
            if(%depth2 >= 3500)
                %client.player.torchLoss = 0.8;
            if(isobject(%client.player.cryogenumtank))
            {
                %client.player.torchloss /= 5;
                %nolight = " \c4(COOLING)";
            }
            else
                %nolight = " \c0(OVERHEATING)";
        }
        else if(%depth2 >= 3500 && %client.player.incold)
        {
            %client.player.mood -= 0.05;
            if(%client.player.mood < 0)
                %client.player.mood = 0;
            %client.player.torchLoss = 0;
            %nolight = "";
        }
    }
    %client.save++;
    if(%client.save >= 100 && !%client.disablesave)
    {
        %client.backup1save++;
        %client.backup2save++;
        %client.backup3save++;
        %client.backup4save++;
        %client.backup5save++;
        if(%client.backup1save >= 150)
        {
            %client.saveBackup(1);
            %client.backup1save = 0;
        }
        if(%client.backup2save >= 300)
        {
            %client.saveBackup(2);
            %client.backup2save = 0;
        }
        if(%client.backup3save >= 600)
        {
            %client.saveBackup(3);
            %client.backup3save = 0;
        }
        if(%client.backup4save >= 900)
        {
            %client.saveBackup(4);
            %client.backup4save = 0;
        }
        if(%client.backup5save >= 1500)
        {
            %client.saveBackup(5);
            %client.backup5save = 0;
        }
        %client.save=0;
        %client.updatestats();
        %client.updateinventory();
        %client.updatepickaxes();
		%client.updatetools();
        //%client.updatemisctools();
        %client.updatecosmetictools();
        %client.updateprestige();
        %client.updatetotalstats();
        if($GameModeArg $= "Add-Ons/Gamemode_skyboard/gamemode.txt")
            %client.updateglobalfile();
    }
    %level = %client.level - %client.prestigestartlevel;
    if(%level == 0)
        %req = 100;
    else if(%level <= 5)
        %req = mfloatlength(mpow(%level * 25,2),0);
    else if(%level <= 10)
        %req = mfloatlength(mpow(%level * 30,2),0);
    else if(%level <= 15)
        %req = mfloatlength(mpow(%level * 37.5,2),0);
    else if(%level <= 20)
        %req = mfloatlength(mpow(%level * 45,2),0);
    else if(%level <= 25)
        %req = mfloatlength(mpow(%level * 52.5,2.1),0);
    else if(%level <= 30)
        %req = mfloatlength(mpow(%level * 58.5,2.2),0);
    else if(%level <= 35)
        %req = mfloatlength(mpow(%level * 61.5,2.25),0);
    else if(%level <= 40)
        %req = mfloatlength(mpow(%level * 75,2.325),0);
    else if(%level <= 45)
        %req = mfloatlength(mpow(%level * 77.5,2.34),0);
    else if(%level <= 50)
        %req = mfloatlength(mpow(%level * 80,2.36),0);
    else
        %req = mfloatlength(mpow(%level * 82.5,2.375),0);
    %client.expREQ = %req+0;
    if(%client.exp < 1000000)
        %exp = %client.exp;
    else
        %exp = mfloor(%client.exp);
    %prestige = %client.prestige;
    if(%prestige > 0)
        %prestigecount = " \c6[\c5" @ numbersToLatin(%client.prestige) @ "\c6]";
    else
        %prestigecount = "";
    if(isobject(%client.player) && %client.player.getstate() !$= "Dead" && %client.player.gethealth() < %client.player.getmaxhealth())
        %health = "\c0Health:" SPC %client.player.gethealth() @ "/" @ %client.player.getmaxhealth();
    if(%client.player.fightingdigger)
        %depth = "\c6[0m]";
    %client.bottomprint("<just:center>\c2Cash:" SPC mfloatlength(%client.money,1) @ "$" NL "\c4Level:" SPC %client.level @ %prestigecount SPC "\c5(" @ %exp @ "/" @ %req @ ")" NL "\c3Mining Power:" SPC mfloatlength((%client.miningpower+%client.prestigeminingpower+%client.achievementminingpower)*(1+%client.miningmultiplier)*(1+%client.prestigeminingmultiplier)*(1+%client.achievementminingmultiplier)*(1-%client.miningpowerloss)*(1-%client.player.torchLoss),0) @ %nolight NL "\c6Depth:" SPC %depth SPC %reachedoptimaldepth @ "(" @ %client.optimaldepth @ "m)" NL %health,1,1);
    %client.hud = %client.schedule(100, displayhud);
    %client.score = %client.level;
    if(%client.player)
    {
        %depth = mfloatlength(5000.2-getword(%client.player.getposition(),2),0);
        if(!%client.ignorezones)
        {
            if(%depth < 50)
            {
                for(%i = 0; %i < environmentzonegroup.getcount(); %i++)
                {
                    %zone = environmentzonegroup.getobject(%i);
                    if(%client.player.zonename && %client.player.zoneid == %zone.environment)
                    {
                        %client.popEnvironment(%zone.environment);
                        break;
                    }
                }
            }
            else if(%depth > 50 && %depth < 200 && %client.player.zonename !$= "50")
            {
                for(%i = 0; %i < environmentzonegroup.getcount(); %i++)
                {
                    %zone = environmentzonegroup.getobject(%i);
                    if(%zone.zonename $= %client.player.zonename)
                    {
                        %client.popEnvironment(%zone.environment);
                        break;
                    }
                }
                for(%i = 0; %i < environmentzonegroup.getcount(); %i++)
                {
                    %zone = environmentzonegroup.getobject(%i);
                    if(%zone.zonename $= "50")
                    {
                        %client.pushenvironment(%zone.environment);
                        break;
                    }
                }
            }
            else if(%depth > 200 && %depth < 500 && %client.player.zonename !$= "200")
            {
                for(%i = 0; %i < environmentzonegroup.getcount(); %i++)
                {
                    %zone = environmentzonegroup.getobject(%i);
                    if(%zone.zonename $= %client.player.zonename)
                    {
                        %client.popEnvironment(%zone.environment);
                        break;
                    }
                }
                for(%i = 0; %i < environmentzonegroup.getcount(); %i++)
                {
                    %zone = environmentzonegroup.getobject(%i);
                    if(%zone.zonename $= "200")
                    {
                        %client.pushenvironment(%zone.environment);
                        break;
                    }
                }
            }
            else if(%depth > 500 && %depth < 1000 && %client.player.zonename !$= "500")
            {
                for(%i = 0; %i < environmentzonegroup.getcount(); %i++)
                {
                    %zone = environmentzonegroup.getobject(%i);
                    if(%zone.zonename $= %client.player.zonename)
                    {
                        %client.popEnvironment(%zone.environment);
                        break;
                    }
                }
                for(%i = 0; %i < environmentzonegroup.getcount(); %i++)
                {
                    %zone = environmentzonegroup.getobject(%i);
                    if(%zone.zonename $= "500")
                    {
                        %client.pushenvironment(%zone.environment);
                        break;
                    }
                }
            }
            else if(%depth > 1000 && %depth < 1500 && %client.player.zonename !$= "1000")
            {
                %client.past500 = 1;
                for(%i = 0; %i < environmentzonegroup.getcount(); %i++)
                {
                    %zone = environmentzonegroup.getobject(%i);
                    if(%zone.zonename $= %client.player.zonename)
                    {
                        %client.popEnvironment(%zone.environment);
                        break;
                    }
                }
                for(%i = 0; %i < environmentzonegroup.getcount(); %i++)
                {
                    %zone = environmentzonegroup.getobject(%i);
                    if(%zone.zonename $= "1000")
                    {
                        %client.pushenvironment(%zone.environment);
                        break;
                    }
                }
            }
            else if(%depth > 1500 && %depth < 2250 && %client.player.zonename !$= "1500")
            {
                %client.past1000 = 1;
                for(%i = 0; %i < environmentzonegroup.getcount(); %i++)
                {
                    %zone = environmentzonegroup.getobject(%i);
                    if(%zone.zonename $= %client.player.zonename)
                    {
                        %client.popEnvironment(%zone.environment);
                        break;
                    }
                }
                for(%i = 0; %i < environmentzonegroup.getcount(); %i++)
                {
                    %zone = environmentzonegroup.getobject(%i);
                    if(%zone.zonename $= "1500")
                    {
                        %client.pushenvironment(%zone.environment);
                        break;
                    }
                }
            }
            else if(%depth > 2250 && %depth < 3000 && %client.player.zonename !$= "2250")
            {
                %client.past1500 = 1;
                for(%i = 0; %i < environmentzonegroup.getcount(); %i++)
                {
                    %zone = environmentzonegroup.getobject(%i);
                    if(%zone.zonename $= %client.player.zonename)
                    {
                        %client.popEnvironment(%zone.environment);
                        break;
                    }
                }
                for(%i = 0; %i < environmentzonegroup.getcount(); %i++)
                {
                    %zone = environmentzonegroup.getobject(%i);
                    if(%zone.zonename $= "2250")
                    {
                        %client.pushenvironment(%zone.environment);
                        break;
                    }
                }
            }
            else if(%depth > 3000 && %depth < 3250 && %client.player.zonename !$= "3000")
            {
                %client.past2250 = 1;
                for(%i = 0; %i < environmentzonegroup.getcount(); %i++)
                {
                    %zone = environmentzonegroup.getobject(%i);
                    if(%zone.zonename $= %client.player.zonename)
                    {
                        %client.popEnvironment(%zone.environment);
                        break;
                    }
                }
                for(%i = 0; %i < environmentzonegroup.getcount(); %i++)
                {
                    %zone = environmentzonegroup.getobject(%i);
                    if(%zone.zonename $= "3000")
                    {
                        %client.pushenvironment(%zone.environment);
                        break;
                    }
                }
            }
            else if(%depth > 3250 && %depth < 3500 && %client.player.zonename !$= "3250")
            {
                for(%i = 0; %i < environmentzonegroup.getcount(); %i++)
                {
                    %zone = environmentzonegroup.getobject(%i);
                    if(%zone.zonename $= %client.player.zonename)
                    {
                        %client.popEnvironment(%zone.environment);
                        break;
                    }
                }
                for(%i = 0; %i < environmentzonegroup.getcount(); %i++)
                {
                    %zone = environmentzonegroup.getobject(%i);
                    if(%zone.zonename $= "3250")
                    {
                        %client.pushenvironment(%zone.environment);
                        break;
                    }
                }
            }
            else if(%depth > 3500 && %depth < 3700 && %client.player.zonename !$= "3500")
            {
                for(%i = 0; %i < environmentzonegroup.getcount(); %i++)
                {
                    %zone = environmentzonegroup.getobject(%i);
                    if(%zone.zonename $= %client.player.zonename)
                    {
                        %client.popEnvironment(%zone.environment);
                        break;
                    }
                }
                for(%i = 0; %i < environmentzonegroup.getcount(); %i++)
                {
                    %zone = environmentzonegroup.getobject(%i);
                    if(%zone.zonename $= "3500")
                    {
                        %client.pushenvironment(%zone.environment);
                        break;
                    }
                }
            }
            else if(%depth > 3700 && %depth < 3850 && %client.player.zonename !$= "3700")
            {
                for(%i = 0; %i < environmentzonegroup.getcount(); %i++)
                {
                    %zone = environmentzonegroup.getobject(%i);
                    if(%zone.zonename $= %client.player.zonename)
                    {
                        %client.popEnvironment(%zone.environment);
                        break;
                    }
                }
                for(%i = 0; %i < environmentzonegroup.getcount(); %i++)
                {
                    %zone = environmentzonegroup.getobject(%i);
                    if(%zone.zonename $= "3700")
                    {
                        %client.pushenvironment(%zone.environment);
                        break;
                    }
                }
            }
            else if(%depth > 3850 && %depth < 4000 && %client.player.zonename !$= "3850")
            {
                for(%i = 0; %i < environmentzonegroup.getcount(); %i++)
                {
                    %zone = environmentzonegroup.getobject(%i);
                    if(%zone.zonename $= %client.player.zonename)
                    {
                        %client.popEnvironment(%zone.environment);
                        break;
                    }
                }
                for(%i = 0; %i < environmentzonegroup.getcount(); %i++)
                {
                    %zone = environmentzonegroup.getobject(%i);
                    if(%zone.zonename $= "3850")
                    {
                        %client.pushenvironment(%zone.environment);
                        break;
                    }
                }
            }
            else if(%depth > 4000 && %depth < 5000 && %client.player.zonename !$= "4000")
            {
                %client.past3000 = 1;
                for(%i = 0; %i < environmentzonegroup.getcount(); %i++)
                {
                    %zone = environmentzonegroup.getobject(%i);
                    if(%zone.zonename $= %client.player.zonename)
                    {
                        %client.popEnvironment(%zone.environment);
                        break;
                    }
                }
                for(%i = 0; %i < environmentzonegroup.getcount(); %i++)
                {
                    %zone = environmentzonegroup.getobject(%i);
                    if(%zone.zonename $= "4000")
                    {
                        %client.pushenvironment(%zone.environment);
                        break;
                    }
                }
            }
            else if(%depth > 5000 && %client.player.zonename !$= "5000")
            {
                %client.past4000 = 1;
                for(%i = 0; %i < environmentzonegroup.getcount(); %i++)
                {
                    %zone = environmentzonegroup.getobject(%i);
                    if(%zone.zonename $= %client.player.zonename)
                    {
                        %client.popEnvironment(%zone.environment);
                        break;
                    }
                }
                for(%i = 0; %i < environmentzonegroup.getcount(); %i++)
                {
                    %zone = environmentzonegroup.getobject(%i);
                    if(%zone.zonename $= "5000")
                    {
                        %client.pushenvironment(%zone.environment);
                        break;
                    }
                }
            }
            if(%depth > 6250)
                %client.past5000 = 1;
        }
    }
}

function player::healthregen(%player)
{
    %player.addhealth(1);
    %player.schedule(1000, healthregen);
}

function numbersToLatin(%amount, %result, %tries)
{
    if(%amount > 1000)
        return %amount;
    %latin[0] = "M" SPC 1000;
    %latin[1] = "CM" SPC 900;
    %latin[2] = "D" SPC 500;
    %latin[3] = "CD" SPC 400;
    %latin[4] = "C" SPC 100;
    %latin[5] = "XC" SPC 90;
    %latin[6] = "L" SPC 50;
    %latin[7] = "XL" SPC 40;
    %latin[8] = "X" SPC 10;
    %latin[9] = "IX" SPC 9;
    %latin[10] = "V" SPC 5;
    %latin[11] = "IV" SPC 4;
    %latin[12] = "I" SPC 1;
    for(%i = 0; %i <= 12; %i++)
    {
        if(%amount >= getword(%latin[%i],1))
        {
            if(%tries >= 50)
                return "stop crashing the server";
            %result = %result @ getword(%latin[%i],0);
            %amount -= getword(%latin[%i],1);
            numbersToLatin(%amount, %result, %tries);
            return;
        }
    }
    if(%result $= "")
        %result = 0;
    return %result;
}

function tipbotannounce(%msg)
{
    %day = getsubstr(getdatetime(), 3, 2);
	%month = getsubstr(getdatetime(), 0, 2);
	if(getsubstr(getdatetime(), 6, 2) > 23)
		%month = %month + (getsubstr(getdatetime(), 6, 2) - 23) * 12;
	%daycount = getdayofyear(%month, %day);
    cancel($tipbot);
    if(!%msg)
        %msg = getrandom(0,67);
    if(%msg == 0)
        announcemessage("\c4TipBot:\c6 did you know you can type \c2/spawn\c6 to teleport back home");
    else if(%msg == 1)
        announcemessage("\c4TipBot:\c6 did you know you can type \c2/bestiary [ore name here]\c6 or \c2/info [ore name here]\c6 to view its info (works on all craft recipes too)");
    else if(%msg == 2)
        announcemessage("\c4TipBot:\c6 going below your optimal depth will cause you to have less mining power");
    else if(%msg == 3)
        announcemessage("\c4TipBot:\c6 compressed ore deposit");
    else if(%msg == 4)
        announcemessage("\c4TipBot:\c6 you can extinguish lava by hitting it once and waiting 5 seconds afterwards");
    else if(%msg == 5)
        announcemessage("\c4TipBot:\c6 did you know the brick limit is capped at 690000 bricks");
    else if(%msg == 6)
        announcemessage("\c4TipBot:\c6 tier-3 crate is not the most valuable out of all crates here");
    else if(%msg == 7)
        announcemessage("\c4TipBot:\c6 do you guys love playing minecraft rpg");
    else if(%msg == 8)
        announcemessage("\c4TipBot:\c6 you should craft the placement tool when going below 500m");
    else if(%msg == 9)
        announcemessage("\c4TipBot:\c6 tunneler is the best thing to ever exist");
    else if(%msg == 10)
        announcemessage("\c4TipBot:\c6 did you know that dynamite exists");
    else if(%msg == 11)
        announcemessage("\c4TipBot:\c6 i love miners");
    else if(%msg == 12)
        announcemessage("\c4TipBot:\c6 did you know prestiges exist in this game and you can prestige at level 20");
    else if(%msg == 13)
        announcemessage("\c4TipBot:\c6 did you know your mining helmet stops working once you go below 3500m");
    else if(%msg == 14)
        announcemessage("\c4TipBot:\c6 did you know i say something every 2 minutes");
    else if(%msg == 15)
        announcemessage("\c4TipBot:\c6 did you know if your tunneler digs lava, it will lose 5%% of its maximum health");
    else if(%msg == 16)
        announcemessage("\c4TipBot:\c6 did you know you will start losing more mining power in deeper layers if you are not in light");
    else if(%msg == 17)
        announcemessage("\c4TipBot:\c6 did you know regular torches will start losing their power in deeper layers, this is why magnesium torches exist");
    else if(%msg == 18)
        announcemessage("\c4TipBot:\c6 magnesium torches are twice as strong than regular torches");
    else if(%msg == 19)
    {
        %time = restwords(getdatetime());
        %time1 = getsubstr(%time,0,2);
        if(%time1 > 12)
            %time = %time1 - 12 @ ":" @ getsubstr(%time,3,2) @ "pm";
        else if(%time == 12)
            %time = %time1 @ ":" @ getsubstr(%time,3,2) @ "pm";
        else if(%time < 12)
            %time = %time1 @ ":" @ getsubstr(%time,3,2) @ "am";
        else if(%time == 0)
            %time = "12:" @ getsubstr(%time,3,2) @ "am";
        announcemessage("\c4TipBot:\c6 it's" SPC %time);
    }
    else if(%msg == 20)
        announcemessage("\c4TipBot:\c6 did you know the tunneler has a 33%% debuff resistance to everything while drilling");
    else if(%msg == 21)
        announcemessage("\c4TipBot:\c6 upside down torches exist");
    else if(%msg == 22)
        announcemessage("\c4TipBot:\c6 the diggers do get a bit quirky in the mines");
    else if(%msg == 23)
        announcemessage("\c4TipBot:\c6 did you know my balls itch");
    else if(%msg == 24)
        announcemessage("\c4TipBot:\c6 you can type \c2/changelog\c6 to view all of the changes");
    else if(%msg == 25)
        announcemessage("\c4TipBot:\c6 did you know zombies can drop ores with a 25%% chance");
    else if(%msg == 26)
        announcemessage("\c4TipBot:\c6 did you know you can type \c2/buyexp [amount]\c6 to buy exp for cash (1 cash to 1 exp ratio)");
    else if(%msg == 27)
        announcemessage("\c4TipBot:\c6 did you know that the laser drill, compared to its lil brother - the tunneler, does not get a 33%% debuff resistance");
    else if(%msg == 28)
        announcemessage("\c4TipBot:\c6 did you know that the event cosmetics and event ores are not erased upon prestiging and always stay in your save file");
    else if(%msg == 29)
        announcemessage("\c4TipBot:\c6 did you know you cannot sell ores that have a level requirement above your level");
    else if(%msg == 30)
        announcemessage("\c4TipBot:\c6 we do not condone child labour");
    else if(%msg == 31)
        announcemessage("\c4TipBot:\c6 the old part of skyboard mines were cut off from the newer mines due to potential safety risks");
    else if(%msg == 32)
        announcemessage("\c4TipBot:\c6 did you know that the laser drill has infinite drilling power potential, because the power keeps rising without any limits");
    else if(%msg == 33)
        announcemessage("\c4TipBot:\c6 diggers lives matter!!!");
    else if(%msg == 34)
        announcemessage("\c4TipBot:\c6 letter q");
    else if(%msg == 35)
        announcemessage("\c4TipBot:\c6 did you know if you type \c2/sell all\c6 it won't sell EVENT ores");
    else if(%msg == 36)
        announcemessage("\c4TipBot:\c6 did you know dynamite does not save when you die or leave the server");
    else if(%msg == 37)
        announcemessage("\c4TipBot:\c6 it's been" SPC %daycount - 219 SPC "days since this gamemode was made");
    else if(%msg == 38)
        announcemessage("\c4TipBot:\c6 every copy of skyboardmining is personalized");
    else if(%msg == 39)
    {
        if(getrandom(1,5) == 1)
            announcemessage("\c4TipBot:\c6 you left your over on");
        else
            announcemessage("\c4TipBot:\c6 you left your oven on");
    }
    else if(%msg == 40)
        announcemessage("\c4TipBot:\c6 did you know you can type \c2/leaderboard\c6 to view it");
    else if(%msg == 41)
        announcemessage("\c4TipBot:\c6 did you know you can trade people by typing \c2/trade [name]");
    else if(%msg == 42)
        announcemessage("\c4TipBot:\c6 did you know this video is sponsored by raid shadow legends");
    else if(%msg == 43)
        announcemessage("\c4TipBot:\c6 did you know this game is basically unlimited and you can dig even below 100000m if you wanted to");
    else if(%msg == 44)
        announcemessage("\c4TipBot:\c6 i farted");
    else if(%msg == 45)
        announcemessage("\c4TipBot:\c6 have you tried not dying");
    else if(%msg == 46)
        announcemessage("\c4TipBot:\c6 morbius");
    else if(%msg == 47)
        announcemessage("\c4TipBot:\c6 this gamemode has sold over 1 morbillion copies around the world");
    else if(%msg == 48)
        announcemessage("\c4TipBot:\c6 so guys we did it, we reached a quarter of a million subscribers");
    else if(%msg == 49)
        announcemessage("\c4TipBot:\c6 did you know this game takes inspiration for 3 different games at once");
    else if(%msg == 50)
        announcemessage("\c4TipBot:\c6 whoever read this will die");
    else if(%msg == 51)
        announcemessage("\c4TipBot:\c6 did you know you can type \c2/help \c6to view all commands in this game");
    else if(%msg == 52)
        announcemessage("\c4TipBot:\c6 did you know this game was made in less than a week");
    else if(%msg == 53)
        announcemessage("\c4TipBot:\c6 did you know if you press alt+f4 you will get free 1000 levels");
    else if(%msg == 54)
        announcemessage("\c4TipBot:\c6 did you know there's a total of" SPC $orecount SPC "ores in this game");
    if(%msg == 55)
        announcemessage("\c4TipBot:\c6 i am going to trans your gender");
    else if(%msg == 56)
        announcemessage("\c4TipBot:\c6 legends speak of someone, who has been guarding their cave for over 100 years... who could it be??? of course someone!!!");
    else if(%msg == 57)
        announcemessage("\c4TipBot:\c6 did you know your mining helmet runs on divine energy that we have obtained by digging to the sky");
    else if(%msg == 58)
        announcemessage("\c4TipBot:\c6 smash that like button and subscribe to my channel for more skyboardmining news");
    else if(%msg == 59)
        announcemessage("\c4TipBot:\c6 real bedrock is real");
    else if(%msg == 60)
        announcemessage("\c4TipBot:\c6 did you know the tunneler has the most cosmetics out of any other item, and mining helmet with geolocators still don't have any");
    else if(%msg == 61)
        announcemessage("\c4TipBot:\c6 the diggers are coming");
    else if(%msg == 62)
        announcemessage("\c4TipBot:\c6 do you know how does a clean shirt look like");
    else if(%msg == 63)
        announcemessage("\c4TipBot:\c6 if you have nothing else to do in your life, skyboardmining is the place for you to waste your time");
    else if(%msg == 64)
        announcemessage("\c4TipBot:\c6 are you a smart fella or fart smella");
    else if(%msg == 65)
        announcemessage("\c4TipBot:\c6 did you know the longest time this game has went without big updates is slightly over 2 months");
    else if(%msg == 66)
        announcemessage("\c4TipBot:\c6 what's the opposite of the opposite");
    else if(%msg == 67)
        announcemessage("\c4TipBot:\c6 did you know you can type \c2/achievements \c6to view your completed achievements");
    $tipbot = schedule(120000, 0, tipbotannounce);
}

function servercmdbuyexp(%client, %amount)
{
    %amount = strreplace(%amount, strchr(%amount, "."), "");
    if(%amount $= "all")
        %amount = %client.money;
    if(%amount $= "")
    {
        %client.chatmessage("no amount specified bruh");
        %client.playsound(errorsound);
        return;
    }
    else if(%amount > %client.money)
    {
        %client.chatmessage("you do not have" SPC %amount SPC "cash to afford this");
        %client.playsound(errorsound);
        return;
    }
    else if(%amount*(1+(%client.prestigecashbonus*0.5)) <= %client.money)
    {
        if(%amount <= 0)
            %client.chatmessage("\c3Successfully did not purchase\c4" SPC %amount SPC "EXP\c3 for\c2" SPC %amount*(1+(%client.prestigecashbonus*0.5)) @ "$\c3!");
        else
            %client.chatmessage("\c3Successfully purchased\c4" SPC %amount SPC "EXP\c3 for\c2" SPC %amount*(1+(%client.prestigecashbonus*0.5)) @ "$\c3!");
        %client.addexp(%amount, 1);
        %client.money -= %amount;
        %client.playsound(beep_key_sound);
    }
}

datablock PlayerData(SpawnNPC : PlayerStandardArmor)
{
    mass = 1000;
};
function spawnNPCS()
{
    for(%i = 1; %i <= 5; %i++)
    {
        %NPC = "NPC" @ %i;
        if(isobject(%NPC))
            %NPC.delete();
    }
    %bot = new AiPlayer(NPC1)
	{
        position = _miningmaster.position;
		datablock = SpawnNPC;
        rotation = "0 0 -1 90";
	};
    %bot.setplayerscale("1.1");
    %bot.schedule(33, setplayerscale, 1.0);
    %bot.name = "miningmaster";
    %bot.isinvincible = 1;
    %bot.sethat("longhead");
    %bot.setnodecolor("lhand", "1 0.878 0.611 1");
    %bot.setnodecolor("rhand", "1 0.878 0.611 1");
    %bot.setnodecolor("headskin", "1 0.878 0.611 1");
    %bot.setnodecolor("larm", "0.9 0 0 1");
    %bot.setnodecolor("rarm", "0.9 0 0 1");
    %bot.setnodecolor("pants", "0 0 1 1");
    %bot.setnodecolor("lshoe", "0 0 1 1");
    %bot.setnodecolor("rshoe", "0 0 1 1");
    %bot.setnodecolor("pants", "0 0 1 1");
    %bot.setnodecolor("chest", "0.9 0.9 0.9 1");

    %bot = new AiPlayer(NPC2)
	{
        position = _inventorymaster.position;
		datablock = SpawnNPC;
        rotation = "0 0 -1 90";
	};
    %bot.setplayerscale("1.1");
    %bot.schedule(33, setplayerscale, 1.0);
    %bot.name = "inventorymaster";
    %bot.isinvincible = 1;
    %bot.sethat("badass_shades");
    %bot.setnodecolor("lhand", "1 0.878 0.611 1");
    %bot.setnodecolor("rhand", "1 0.878 0.611 1");
    %bot.setnodecolor("headskin", "1 0.878 0.611 1");
    %bot.setnodecolor("larm", "0.9 0 0 1");
    %bot.setnodecolor("rarm", "0.9 0 0 1");
    %bot.setnodecolor("pants", "0 0 1 1");
    %bot.setnodecolor("lshoe", "0 0 1 1");
    %bot.setnodecolor("rshoe", "0 0 1 1");
    %bot.setnodecolor("pants", "0 0 1 1");
    %bot.setnodecolor("chest", "0.9 0.9 0.9 1");

    %bot = new AiPlayer(NPC3)
	{
        position = _shopkeeper.position;
		datablock = SpawnNPC;
        rotation = "0 0 -1 90";
	};
    %bot.setplayerscale("1.1");
    %bot.schedule(33, setplayerscale, 1.0);
    %bot.name = "shopkeeper";
    %bot.isinvincible = 1;
    %bot.sethat("doomslayer");
    %bot.setnodecolor("lhand", "1 0.878 0.611 1");
    %bot.setnodecolor("rhand", "1 0.878 0.611 1");
    %bot.setnodecolor("headskin", "1 0.878 0.611 1");
    %bot.setnodecolor("larm", "0.9 0 0 1");
    %bot.setnodecolor("rarm", "0.9 0 0 1");
    %bot.setnodecolor("pants", "0 0 1 1");
    %bot.setnodecolor("lshoe", "0 0 1 1");
    %bot.setnodecolor("rshoe", "0 0 1 1");
    %bot.setnodecolor("pants", "0 0 1 1");
    %bot.setnodecolor("chest", "0.9 0.9 0.9 1");

    %bot = new AiPlayer(NPC4)
	{
        position = _localresident.position;
		datablock = SpawnNPC;
        rotation = "0 0 -1 90";
	};
    %bot.setplayerscale("1.1");
    %bot.schedule(33, setplayerscale, 1.0);
    %bot.schedule(33, addvelocity, "1 0 0");
    %bot.schedule(333, playthread, 0, sit);
    %bot.name = "localresident";
    %bot.isinvincible = 1;
    %bot.sethat("belial");
    %bot.setnodecolor("lhand", "1 0.878 0.611 1");
    %bot.setnodecolor("rhand", "1 0.878 0.611 1");
    %bot.setnodecolor("headskin", "1 0.878 0.611 1");
    %bot.setnodecolor("larm", "0.9 0 0 1");
    %bot.setnodecolor("rarm", "0.9 0 0 1");
    %bot.setnodecolor("pants", "0 0 1 1");
    %bot.setnodecolor("lshoe", "0 0 1 1");
    %bot.setnodecolor("rshoe", "0 0 1 1");
    %bot.setnodecolor("pants", "0 0 1 1");
    %bot.setnodecolor("chest", "0.9 0.9 0.9 1");

    %bot = new AiPlayer(NPC5)
	{
        position = _blacksmith.position;
		datablock = SpawnNPC;
        rotation = "0 0 1 90";
	};
    %bot.setplayerscale("1.1");
    %bot.schedule(33, setplayerscale, 1.0);
    %bot.name = "blacksmith";
    %bot.isinvincible = 1;
    %bot.sethat("thefallen");
    %bot.setnodecolor("lhand", "1 0.878 0.611 1");
    %bot.setnodecolor("rhand", "1 0.878 0.611 1");
    %bot.setnodecolor("headskin", "1 0.878 0.611 1");
    %bot.setnodecolor("larm", "0.9 0 0 1");
    %bot.setnodecolor("rarm", "0.9 0 0 1");
    %bot.setnodecolor("pants", "0 0 1 1");
    %bot.setnodecolor("lshoe", "0 0 1 1");
    %bot.setnodecolor("rshoe", "0 0 1 1");
    %bot.setnodecolor("pants", "0 0 1 1");
    %bot.setnodecolor("chest", "0.9 0.9 0.9 1");

    //%bot = new AiPlayer(NPC5)
	//{
        //position = _eventnpc.position;
		//datablock = SpawnNPC;
        //rotation = "0 0 -1 0";
	//};
    //%bot.setplayerscale("1.1");
    //%bot.schedule(33, setplayerscale, 1.0);
    //%bot.name = "chrismothnpc";
    //%bot.isinvincible = 1;
    //%bot.sethat("villager");
    //%bot.setnodecolor("lhand", "1 0.878 0.611 1");
    //%bot.setnodecolor("rhand", "1 0.878 0.611 1");
    //%bot.setnodecolor("headskin", "1 0.878 0.611 1");
    //%bot.setnodecolor("larm", "0.9 0 0 1");
    //%bot.setnodecolor("rarm", "0.9 0 0 1");
    //%bot.setnodecolor("pants", "0 0 1 1");
    //%bot.setnodecolor("lshoe", "0 0 1 1");
    //%bot.setnodecolor("rshoe", "0 0 1 1");
    //%bot.setnodecolor("pants", "0 0 1 1");
    //%bot.setnodecolor("chest", "0.9 0.9 0.9 1");
}