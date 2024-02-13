if(!isobject(cdgroup))
    new simgroup(cdgroup){};
if(!isobject(cdbrickgroup))
    new simgroup(cdbrickgroup){};

function startCrystalDefense()
{
    $crystaldefensestopped = 0;
    eraseCDtools();
    cdgroup.deleteall();
    cancel($crystaldefense);
    $currentwave = 1;
    $waveprogress = 0;
    $crystalHP = 1000;
    for(%i = 0; %i < clientgroup.getcount(); %i++)
    {
        %client = clientgroup.getobject(%i);
        if(%client.player.ineventbasement)
        {
            %client.points = 0;
            %client.player.cleartools();
            %client.player.ineventbasement = 0;
            %client.setmusic("");
            cancel(%client.hud);
            %client.schedule(33, displayCDhud);
            %client.CDplayer = 1;
            %client.player.settransform(_cdplayerspawn.position);
            %client.schedule(15000, setmusic, "musicdata_jingle_bells", 1);
            %client.spawnplayer();
        }
    }
    schedule(15000, 0, CDwave);
}

function CDwave()
{
    cancel($crystaldefense);
    if(!$currentwave)
        $currentwave = 1;
    if($currentwave == 1)
    {
        if(cdgroup.getcount() < 40)
            generateCDenemy(1);
        $crystalDefense = schedule(1500, 0, CDwave);
    }
    else if($currentwave == 2)
    {
        if(cdgroup.getcount() < 40)
        {
            generateCDenemy(1);
            if(getrandom(1,100) <= 20)
                generateCDenemy(2);
        }
        $crystalDefense = schedule(getrandom(1250,2000), 0, CDwave);
    }
    else if($currentwave == 3)
    {
        if(cdgroup.getcount() < 40)
        {
            %enemy = getrandom(1,100);
            if(%enemy <= 60)
                generateCDenemy(1);
            else
                generateCDenemy(4);
            if(getrandom(1,100) <= 10)
                schedule(333, 0, generateCDenemy, 2);
        }
        $crystalDefense = schedule(getrandom(1250,2000), 0, CDwave);
    }
    else if($currentwave == 4)
    {
        if(cdgroup.getcount() < 40)
        {
            %enemy = getrandom(1,100);
            if(%enemy <= 40)
                generateCDenemy(1);
            else if(%enemy <= 80)
                generateCDenemy(4);
            else
                generateCDenemy(3);
            if(getrandom(1,100) <= 20)
                schedule(100, 0, generateCDenemy, 5);
            if(getrandom(1,100) <= 10)
                schedule(333, 0, generateCDenemy, 2);
        }
        $crystalDefense = schedule(getrandom(1250,2250), 0, CDwave);
    }
    else if($currentwave == 5)
    {
        if(cdgroup.getcount() < 35)
        {
            %enemy = getrandom(1,100);
            if(%enemy <= 30)
                generateCDenemy(1);
            else if(%enemy <= 70)
                generateCDenemy(4);
            else
                generateCDenemy(3);
            if(getrandom(1,100) <= 25)
                schedule(100, 0, generateCDenemy, 5);
            if(getrandom(1,100) <= 11)
                schedule(333, 0, generateCDenemy, 2);
        }
        $crystalDefense = schedule(getrandom(2000,4000), 0, CDwave);
    }
    else if($currentwave == 6)
    {
        if(cdgroup.getcount() < 40)
        {
            %enemy = getrandom(1,100);
            if(%enemy <= 20)
                generateCDenemy("Present");
            else if(%enemy <= 40)
                generateCDenemy("Frozen Blockhead");
            else if(%enemy <= 60)
                generateCDenemy("Gunner Snowman");
            else if(%enemy <= 80)
                generateCDenemy("Festive Blockhead");
            else
                generateCDenemy("Candy");
            if(getrandom(1,100) <= 25)
                schedule(100, 0, generateCDenemy, "Snowman");
            if(getrandom(1,100) <= 13)
                schedule(333, 0, generateCDenemy, "Giant Snowman");
        }
        $crystalDefense = schedule(getrandom(750,1500), 0, CDwave);
    }
    else if($currentwave == 7)
    {
        if(cdgroup.getcount() < 40)
        {
            %enemy = getrandom(1,100);
            if(%enemy <= 20)
                generateCDenemy("Present");
            else if(%enemy <= 40)
                generateCDenemy("Frozen Blockhead");
            else if(%enemy <= 60)
                generateCDenemy("Gunner Snowman");
            if(getrandom(1,10) == 1)
            {
                %enemy2 = getrandom(1,100);
                if(%enemy2 <= 35)
                    schedule(getrandom(100,600), 0, generateCDenemy, "Festive Blockhead");
                else if(%enemy2 <= 70)
                    schedule(getrandom(100,600), 0, generateCDenemy, "Candy");
                else
                    schedule(getrandom(100,600), 0, generateCDenemy, "Spirit");
            }
            if(getrandom(1,100) <= 25)
                schedule(100, 0, generateCDenemy, "Snowman");
            if(getrandom(1,100) <= 13)
                schedule(333, 0, generateCDenemy, "Giant Snowman");
        }
        $crystalDefense = schedule(getrandom(750,1500), 0, CDwave);
    }
    else if($currentwave == 8)
    {
        if(cdgroup.getcount() < 40)
        {
            %enemy = getrandom(1,100);
            if(%enemy <= 20)
                generateCDenemy("Present");
            else if(%enemy <= 40)
                generateCDenemy("Frozen Blockhead");
            else if(%enemy <= 60)
                generateCDenemy("Gunner Snowman");
            else if(%enemy <= 80)
                generateCDenemy("Frost");
            if(getrandom(1,10) == 1)
            {
                %enemy2 = getrandom(1,100);
                if(%enemy2 <= 35)
                    schedule(getrandom(100,600), 0, generateCDenemy, "Festive Blockhead");
                else if(%enemy2 <= 70)
                    schedule(getrandom(100,600), 0, generateCDenemy, "Candy");
                else
                    schedule(getrandom(100,600), 0, generateCDenemy, "Spirit");
            }
            if(getrandom(1,100) <= 20)
                schedule(100, 0, generateCDenemy, "Snowman");
            if(getrandom(1,100) <= 8)
                schedule(333, 0, generateCDenemy, "Giant Snowman");
        }
        $crystalDefense = schedule(getrandom(750,1500), 0, CDwave);
    }
    else if($currentwave == 9)
    {
        if(cdgroup.getcount() < 40)
        {
            %enemy = getrandom(1,100);
            if(%enemy <= 15)
                generateCDenemy("Frost Invader");
            else if(%enemy <= 60)
                generateCDenemy("Frozen Blockhead");
            else if(%enemy <= 80)
                generateCDenemy("Frost");
            if(getrandom(1,10) == 1)
            {
                %enemy2 = getrandom(1,100);
                if(%enemy2 <= 35)
                    schedule(getrandom(100,600), 0, generateCDenemy, "Festive Blockhead");
                else if(%enemy2 <= 70)
                    schedule(getrandom(100,600), 0, generateCDenemy, "Candy");
                else
                    schedule(getrandom(100,600), 0, generateCDenemy, "Spirit");
            }
            %enemy3 = getrandom(1,100);
            if(getrandom(1,100) <= 20)
            {
                if(%enemy3 <= 20)
                    schedule(100, 0, generateCDenemy, "Snowman");
                else if(%enemy3 <= 50)
                    schedule(100, 0, generateCDenemy, "Gunner Snowman");
                else if(%enemy <= 80)
                    schedule(333, 0, generateCDenemy, "Giant Snowman");
                else
                    schedule(333, 0, generateCDenemy, "Present");
            }
        }
        $crystalDefense = schedule(getrandom(1000,2000), 0, CDwave);
    }
    else if($currentwave == 10)
    {
        if(cdgroup.getcount() < 30)
        {
            if(getrandom(1,2) == 1)
                schedule(333, 0, generateCDenemy, "Present");
            %enemy = getrandom(1,100);
            if(%enemy <= 15)
                generateCDenemy("Frost Invader");
            else if(%enemy <= 60)
                generateCDenemy("Frozen Blockhead");
            else if(%enemy <= 80)
                generateCDenemy("Frost");
            if(getrandom(1,9) == 1)
            {
                %enemy2 = getrandom(1,100);
                if(%enemy2 <= 35)
                    schedule(getrandom(100,600), 0, generateCDenemy, "Festive Blockhead");
                else if(%enemy2 <= 70)
                    schedule(getrandom(100,600), 0, generateCDenemy, "Candy");
                else
                    schedule(getrandom(100,600), 0, generateCDenemy, "Spirit");
            }
            %enemy3 = getrandom(1,100);
            if(getrandom(1,100) <= 20)
            {
                if(%enemy3 <= 25)
                    schedule(100, 0, generateCDenemy, "Snowman");
                else if(%enemy3 <= 60)
                    schedule(100, 0, generateCDenemy, "Gunner Snowman");
                else
                    schedule(333, 0, generateCDenemy, "Giant Snowman");
            }
        }
        $crystalDefense = schedule(getrandom(2000,4000), 0, CDwave);
    }
    else if($currentwave == 11)
    {
        if(cdgroup.getcount() < 40)
        {
            if(getrandom(1,6) == 1)
                schedule(1250, 0, generateCDenemy, "Frozen Blockhead");
            if(getrandom(1,13) == 1)
                schedule(1250, 0, generateCDenemy, "Snow Golem");
            %enemy = getrandom(1,100);
            if(%enemy <= 25)
                generateCDenemy("Frost Invader");
            else if(%enemy <= 60)
                generateCDenemy("Frost");
            else
                generateCDenemy("Elf");
            if(getrandom(1,8) == 1)
            {
                %enemy2 = getrandom(1,100);
                if(%enemy2 <= 35)
                    schedule(getrandom(100,600), 0, generateCDenemy, "Festive Blockhead");
                else if(%enemy2 <= 70)
                    schedule(getrandom(100,600), 0, generateCDenemy, "Candy");
                else
                    schedule(getrandom(100,600), 0, generateCDenemy, "Spirit");
            }
            %enemy3 = getrandom(1,100);
            if(getrandom(1,100) <= 25)
            {
                if(%enemy3 <= 20)
                    schedule(100, 0, generateCDenemy, "Snowman");
                else if(%enemy3 <= 50)
                    schedule(100, 0, generateCDenemy, "Gunner Snowman");
                else if(%enemy <= 80)
                    schedule(333, 0, generateCDenemy, "Giant Snowman");
                else
                    schedule(250, 0, generateCDenemy, "Present");
            }
        }
        $crystalDefense = schedule(getrandom(1500,3000), 0, CDwave);
    }
    else if($currentwave == 12)
    {
        if(cdgroup.getcount() < 40)
        {
            if(getrandom(1,12) == 1)
                schedule(1250, 0, generateCDenemy, "Snow Golem");
            %enemy = getrandom(1,100);
            if(%enemy <= 20)
                generateCDenemy("Frost Ravager");
            else if(%enemy <= 60)
                generateCDenemy("Frost Invader");
            else
                generateCDenemy("Elf");
            if(getrandom(1,7) == 1)
            {
                %enemy2 = getrandom(1,100);
                if(%enemy2 <= 25)
                    schedule(getrandom(100,600), 0, generateCDenemy, "Festive Blockhead");
                else if(%enemy2 <= 40)
                    schedule(getrandom(100,600), 0, generateCDenemy, "Candy");
                else if(%enemy2 <= 70)
                    generateCDenemy("Frost");
                else
                    schedule(getrandom(100,600), 0, generateCDenemy, "Spirit");
                
            }
            %enemy3 = getrandom(1,100);
            if(getrandom(1,100) <= 30)
            {
                if(%enemy3 <= 20)
                    schedule(100, 0, generateCDenemy, "Snowman");
                else if(%enemy3 <= 40)
                    schedule(100, 0, generateCDenemy, "Gunner Snowman");
                else if(%enemy <= 60)
                    schedule(333, 0, generateCDenemy, "Giant Snowman");
                else if(%enemy <= 80)
                    schedule(250, 0, generateCDenemy, "Present");
                else
                    schedule(1250, 0, generateCDenemy, "Frozen Blockhead");
            }
        }
        $crystalDefense = schedule(getrandom(1500,3000), 0, CDwave);
    }
    else if($currentwave == 13)
    {
        if(cdgroup.getcount() < 40)
        {
            if(getrandom(1,10) == 1)
                schedule(1750, 0, generateCDenemy, "Snow Monster");
            if(getrandom(1,8) == 1)
                schedule(1250, 0, generateCDenemy, "Snow Golem");
            %enemy = getrandom(1,100);
            if(%enemy <= 20)
                generateCDenemy("Frost Ravager");
            else if(%enemy <= 40)
                generateCDenemy("Frost Invader");
            else if(%enemy <= 65)
                generateCDenemy("Giant Elf");
            else
                generateCDenemy("Elf");
            if(getrandom(1,7) == 1)
            {
                %enemy2 = getrandom(1,100);
                if(%enemy2 <= 25)
                    schedule(getrandom(100,600), 0, generateCDenemy, "Festive Blockhead");
                else if(%enemy2 <= 40)
                    schedule(getrandom(100,600), 0, generateCDenemy, "Candy");
                else if(%enemy2 <= 70)
                    generateCDenemy("Frost");
                else
                    schedule(getrandom(100,600), 0, generateCDenemy, "Spirit");
                
            }
            %enemy3 = getrandom(1,100);
            if(getrandom(1,100) <= 30)
            {
                if(%enemy3 <= 20)
                    schedule(100, 0, generateCDenemy, "Snowman");
                else if(%enemy3 <= 40)
                    schedule(100, 0, generateCDenemy, "Gunner Snowman");
                else if(%enemy <= 60)
                    schedule(333, 0, generateCDenemy, "Giant Snowman");
                else if(%enemy <= 80)
                    schedule(250, 0, generateCDenemy, "Present");
                else
                    schedule(1250, 0, generateCDenemy, "Frozen Blockhead");
            }
        }
        $crystalDefense = schedule(getrandom(1750,3500), 0, CDwave);
    }
    else if($currentwave == 14)
    {
        if(cdgroup.getcount() < 40)
        {
            if(getrandom(1,9) == 1)
                schedule(1750, 0, generateCDenemy, "Snow Monster");
            if(getrandom(1,7) == 1)
                schedule(1250, 0, generateCDenemy, "Snow Golem");
            %enemy = getrandom(1,100);
            if(%enemy <= 20)
                generateCDenemy("Frost Ravager");
            else if(%enemy <= 40)
                generateCDenemy("Frost Invader");
            else if(%enemy <= 65)
                generateCDenemy("Giant Elf");
            else
                generateCDenemy("Elf");
            if(getrandom(1,7) == 1)
            {
                %enemy2 = getrandom(1,100);
                if(%enemy2 <= 25)
                    schedule(getrandom(100,600), 0, generateCDenemy, "Festive Blockhead");
                else if(%enemy2 <= 40)
                    schedule(getrandom(100,600), 0, generateCDenemy, "Candy");
                else if(%enemy2 <= 70)
                    generateCDenemy("Frost");
                else
                    schedule(getrandom(100,600), 0, generateCDenemy, "Spirit");
                
            }
            %enemy3 = getrandom(1,100);
            if(getrandom(1,100) <= 35)
            {
                if(%enemy3 <= 20)
                    schedule(100, 0, generateCDenemy, "Snowman");
                else if(%enemy3 <= 40)
                    schedule(100, 0, generateCDenemy, "Gunner Snowman");
                else if(%enemy <= 60)
                    schedule(333, 0, generateCDenemy, "Giant Snowman");
                else if(%enemy <= 80)
                    schedule(250, 0, generateCDenemy, "Present");
                else
                    schedule(1250, 0, generateCDenemy, "Frozen Blockhead");
            }
        }
        $crystalDefense = schedule(getrandom(2000,4000), 0, CDwave);
    }
    else if($currentwave == 15)
    {
        if(cdgroup.getcount() < 35)
        {
            if(getrandom(1,9) == 1)
                schedule(1750, 0, generateCDenemy, "Snow Monster");
            if(getrandom(1,7) == 1)
                schedule(1250, 0, generateCDenemy, "Snow Golem");
            %enemy = getrandom(1,100);
            if(%enemy <= 20)
                generateCDenemy("Frost Ravager");
            else if(%enemy <= 40)
                generateCDenemy("Frost Invader");
            else if(%enemy <= 65)
                generateCDenemy("Giant Elf");
            else
                generateCDenemy("Elf");
            if(getrandom(1,7) == 1)
            {
                %enemy2 = getrandom(1,100);
                if(%enemy2 <= 25)
                    schedule(getrandom(100,600), 0, generateCDenemy, "Festive Blockhead");
                else if(%enemy2 <= 40)
                    schedule(getrandom(100,600), 0, generateCDenemy, "Candy");
                else if(%enemy2 <= 70)
                    generateCDenemy("Frost");
                else
                    schedule(getrandom(100,600), 0, generateCDenemy, "Spirit");
                
            }
            %enemy3 = getrandom(1,100);
            if(getrandom(1,100) <= 35)
            {
                if(%enemy3 <= 20)
                    schedule(100, 0, generateCDenemy, "Snowman");
                else if(%enemy3 <= 40)
                    schedule(100, 0, generateCDenemy, "Gunner Snowman");
                else if(%enemy <= 60)
                    schedule(333, 0, generateCDenemy, "Giant Snowman");
                else if(%enemy <= 80)
                    schedule(250, 0, generateCDenemy, "Present");
                else
                    schedule(1250, 0, generateCDenemy, "Frozen Blockhead");
            }
        }
        $crystalDefense = schedule(getrandom(2500,4000), 0, CDwave);
    }
}

function progressCDwave(%amount)
{
    $waveProgress += %amount;
    if($waveProgress >= 100)
    {
        for(%i = 0; %i < clientgroup.getcount(); %i++)
        {
            %client = clientgroup.getobject(%i);
            if(%client.cdplayer)
            {
                %client.playsound(beep_checkout_sound);
                %client.chatmessage("\c4Wave" SPC $currentwave+1 SPC "has begun!");
                %client.points += $currentwave+1 * 75;
            }
        }
        $waveProgress = 0;
        $currentWave++;
        if($currentWave == 5)
            generateCDenemy("Christmas Spirit");
        if($currentWave == 10)
            generateCDenemy("Present Giver");
        if($currentWave == 14)
        {
            generateCDenemy("Frost Hero");
            generateCDenemy("Frost Necromancer");
        }
        if($currentWave == 15)
        {
            for(%i = 0; %i < clientgroup.getcount(); %i++)
            {
                %client = clientgroup.getobject(%i);
                if(%client.cdplayer)
                    %client.setmusic("musicdata_the_chrismoth_spirit", 1);
            }
            generateCDenemy("Mecha-Clause");
        }
    }
}

function gameconnection::displayCDhud(%client)
{
    %player = %client.player;
    if(%player)
        %client.bottomprint("<just:center><font:arial bold:16>\c2Health:" SPC %player.gethealth() SPC "/" SPC %player.getmaxhealth() NL "\c3Score:" SPC mfloor(%client.points) NL "\c0Crystal's Health:" SPC mfloor($crystalHP) SPC "/ 1000" NL "\c4Wave Progress:" SPC mfloor($currentWave) SPC "/ 15 (" @ mfloor($waveProgress) @ "%)", 0.25, 1);
    %client.cdhud = %client.schedule(33, displayCDhud);
}

function generateCDenemy(%enemy)
{
    if(%enemy $= "Snowman")
        %enemy = 1;
    else if(%enemy $= "Giant Snowman")
        %enemy = 2;
    else if(%enemy $= "Gunner Snowman")
        %enemy = 3;
    else if(%enemy $= "Frozen Blockhead")
        %enemy = 4;
    else if(%enemy $= "Present")
        %enemy = 5;
    else if(%enemy $= "Elf")
        %enemy = 6;
    else if(%enemy $= "Christmas Spirit")
        %enemy = 7;
    else if(%enemy $= "Festive Blockhead")
        %enemy = 8;
    else if(%enemy $= "Spirit")
        %enemy = 9;
    else if(%enemy $= "Candy")
        %enemy = 10;
    else if(%enemy $= "Frost")
        %enemy = 11;
    else if(%enemy $= "Frost Invader")
        %enemy = 12;
    else if(%enemy $= "Frost Ravager")
        %enemy = 13;
    else if(%enemy $= "Snow Monster")
        %enemy = 14;
    else if(%enemy $= "Giant Elf")
        %enemy = 15;
    else if(%enemy $= "Snow Golem")
        %enemy = 16;
    else if(%enemy $= "Present Giver")
        %enemy = 17;
    else if(%enemy $= "Frost Hero")
        %enemy = 18;
    else if(%enemy $= "Frost Necromancer")
        %enemy = 19;
    else if(%enemy $= "Mecha-Clause")
        %enemy = 20;
    %spawn = _cdbotspawn @ getrandom(1,40);
    %bot = new AiPlayer()
	{
        minigame = $miningminigame;
        CDenemy = 1;
        position = %spawn.position;
		datablock = PlayerNoJet;
        rotation = "0 0 1 90";
	};
    %bot.client = %bot;
    %bot.setmoveslowdown(0);
    %bot.setplayerscale("1.01");
    %bot.size = 1;
    if(!%enemy)
    {
        %bot.setnodecolor("lhand", "1 0.878 0.611 1");
        %bot.setnodecolor("rhand", "1 0.878 0.611 1");
        %bot.setnodecolor("headskin", "1 0.878 0.611 1");
        %bot.setnodecolor("larm", "0.9 0 0 1");
        %bot.setnodecolor("rarm", "0.9 0 0 1");
        %bot.setnodecolor("lshoe", "0 0 1 1");
        %bot.setnodecolor("rshoe", "0 0 1 1");
        %bot.setnodecolor("pants", "0 0 1 1");
        %bot.setnodecolor("chest", "0.9 0.9 0.9 1");
        %bot.name = "Blockhead";
    }
    else if(%enemy == 1)
    {
        %bot.setnodecolor("ALL", "1 1 1 1");
        %bot.sethat("snowman");
        %bot.name = "Snowman";
        %bot.setmaxhealth(250);
        %bot.attackdamage = 10;
    }
    else if(%enemy == 2)
    {
        %bot.setnodecolor("ALL", "1 1 1 1");
        %bot.sethat("snowman");
        %bot.name = "Giant Snowman";
        %bot.setmaxhealth(1250);
        %bot.attackdamage = 40;
        %bot.size = 1.6;
        %bot.setrunspeed(0.6);
    }
    else if(%enemy == 3)
    {
        %bot.setnodecolor("ALL", "1 1 1 1");
        %bot.sethat("snowman");
        %bot.name = "Gunner Snowman";
        %bot.setmaxhealth(300);
        %bot.attackdamage = 5;
        %bot.setrunspeed(0.8);
        %bot.mountimage(candygunimage, 0);
        %bot.playthread(0, armreadyright);
    }
    else if(%enemy == 4)
    {
        %bot.setnodecolor("ALL", "0 0.6 0.9 0.7");
        %bot.name = "Frozen Blockhead";
        %bot.setmaxhealth(500);
        %bot.attackdamage = 20;
        %bot.setmaxforwardspeed(12);
    }
    else if(%enemy == 5)
    {
        %bot.setnodecolor("ALL", "1 0 0 1");
        %bot.setnodecolor("chest", "1 1 1 1");
        %bot.setnodecolor("headskin", "1 1 1 1");
        %bot.setnodecolor("pants", "1 1 1 1");
        %bot.name = "Present";
        %bot.sethat("present");
        %bot.setmaxhealth(500);
        %bot.attackdamage = 15;
        %bot.playthread(0, armreadyright);
        if(getrandom(1,2) == 1)
            %bot.mountimage(candycaneimage, 0);
        else
            %bot.mountimage(candygunimage, 0);
    }
    else if(%enemy == 6)
    {
        %bot.setnodecolor("lhand", "1 0.878 0.611 1");
        %bot.setnodecolor("rhand", "1 0.878 0.611 1");
        %bot.setnodecolor("headskin", "1 0.878 0.611 1");
        %bot.setnodecolor("larm", "0 0.5 0.25 1");
        %bot.setnodecolor("rarm", "0 0.5 0.25 1");
        %bot.setnodecolor("lshoe", "0.2 0.2 0.2 1");
        %bot.setnodecolor("rshoe", "0.2 0.2 0.2 1");
        %bot.setnodecolor("pants", "0.2 0.2 0.2 1");
        %bot.setnodecolor("chest", "0 0.5 0.25 1");
        %bot.sethat("festive");
        %bot.setmaxhealth(1250);
        %bot.name = "Elf";
        %bot.mountimage(presentgunimage, 0);
        %bot.playthread(0, armreadyright);
    }
    else if(%enemy == 7)
    {
        %bot.setdatablock(playerstandardarmor);
        %bot.setnodecolor("ALL", "0.5 0.6 0.7 0.6");
        %bot.sethat("tiara");
        %bot.setmaxhealth(8000);
        %bot.attackdamage = 50;
        %bot.size = 1.8;
        %bot.name = "Christmas Spirit";
        %bot.boss = 1;
    }
    else if(%enemy == 8)
    {
        %bot.setnodecolor("lhand", "1 0.878 0.611 1");
        %bot.setnodecolor("rhand", "1 0.878 0.611 1");
        %bot.setnodecolor("headskin", "1 0.878 0.611 1");
        %bot.setnodecolor("larm", "0.9 0 0 1");
        %bot.setnodecolor("rarm", "0.9 0 0 1");
        %bot.setnodecolor("lshoe", "0 0 1 1");
        %bot.setnodecolor("rshoe", "0 0 1 1");
        %bot.setnodecolor("pants", "0 0 1 1");
        %bot.setnodecolor("chest", "0.9 0.9 0.9 1");
        %bot.sethat("festive");
        %bot.name = "Festive Blockhead";
        %bot.setmaxhealth(500);
    }
    else if(%enemy == 9)
    {
        %bot.setnodecolor("ALL", "0.5 0.6 0.7 0.6");
        %bot.setmaxhealth(750);
        %bot.attackdamage = 20;
        %bot.name = "Spirit";
    }
    else if(%enemy == 10)
    {
        %bot.setnodecolor("ALL", "1 0 0 1");
        %bot.name = "Candy";
        %bot.setmaxhealth(750);
        %bot.playthread(0, armreadyright);
        %bot.mountimage(candycaneimage, 0);
    }
    else if(%enemy == 11)
    {
        %bot.setnodecolor("ALL", "0 0 0.9 0.6");
        %bot.setmaxhealth(1000);
        %bot.attackdamage = 20;
        %bot.setmaxforwardspeed(12);
        %bot.name = "Frost";
        %bot.mountimage(zb1image, 0);
    }
    else if(%enemy == 12)
    {
        %bot.setnodecolor("lhand", "0.56 0.929 0.96 1");
        %bot.setnodecolor("rhand", "0.56 0.929 0.96 1");
        %bot.setnodecolor("headskin", "0.56 0.929 0.96 1");
        %bot.setnodecolor("larm", "0.56 0.929 0.96 1");
        %bot.setnodecolor("rarm", "0.56 0.929 0.96 1");
        %bot.setnodecolor("lshoe", "0.2 0.2 0.2 1");
        %bot.setnodecolor("rshoe", "0.2 0.2 0.2 1");
        %bot.setnodecolor("pants", "0.2 0.2 0.2 1");
        %bot.setnodecolor("chest", "0.2 0.2 0.2 1");
        %bot.setmaxhealth(2500);
        %bot.attackdamage = 25;
        %bot.setmaxforwardspeed(10);
        %bot.name = "Frost Invader";
    }
    else if(%enemy == 13)
    {
        %bot.setnodecolor("lhand", "0.56 0.929 0.96 1");
        %bot.setnodecolor("rhand", "0.56 0.929 0.96 1");
        %bot.setnodecolor("headskin", "0.56 0.929 0.96 1");
        %bot.setnodecolor("larm", "0.2 0.2 0.2 1");
        %bot.setnodecolor("rarm", "0.2 0.2 0.2 1");
        %bot.setnodecolor("lshoe", "0.2 0.2 0.2 1");
        %bot.setnodecolor("rshoe", "0.2 0.2 0.2 1");
        %bot.setnodecolor("pants", "0.2 0.2 0.2 1");
        %bot.setnodecolor("chest", "0.2 0.2 0.2 1");
        %bot.setnodecolor("helmet", "0.5 0.5 0.5 1");
        %bot.setnodecolor("armor", "0.5 0.5 0.5 1");
        %bot.setnodecolor("visor", "0.11 0.59 0.74 1");
        %bot.unhidenode("helmet");
        %bot.unhidenode("visor");
        %bot.unhidenode("armor");
        %bot.setmaxhealth(7000);
        %bot.attackdamage = 100;
        %bot.setmaxforwardspeed(3);
        %bot.name = "Frost Ravager";
        %bot.size = 1.25;
    }
    else if(%enemy == 14)
    {
        %bot.setnodecolor("lhand", "1 1 1 1");
        %bot.setnodecolor("rhand", "1 1 1  1");
        %bot.setnodecolor("headskin", "1 1 1  1");
        %bot.setnodecolor("larm", "1 1 1  1");
        %bot.setnodecolor("rarm", "1 1 1  1");
        %bot.setnodecolor("lshoe", "0.2 0.2 0.2 1");
        %bot.setnodecolor("rshoe", "0.2 0.2 0.2 1");
        %bot.setnodecolor("pants", "0.2 0.2 0.2 1");
        %bot.setnodecolor("chest", "0.2 0.2 0.2 1");
        %bot.setnodecolor("armor", "0.5 0.5 0.5 1");
        %bot.unhidenode("armor");
        %bot.setmaxhealth(12500);
        %bot.attackdamage = 100;
        %bot.setmaxforwardspeed(1);
        %bot.name = "Snow Monster";
        %bot.size = 1.75;
    }
    else if(%enemy == 15)
    {
        %bot.setnodecolor("lhand", "1 0.878 0.611 1");
        %bot.setnodecolor("rhand", "1 0.878 0.611 1");
        %bot.setnodecolor("headskin", "1 0.878 0.611 1");
        %bot.setnodecolor("larm", "0 0.5 0.25 1");
        %bot.setnodecolor("rarm", "0 0.5 0.25 1");
        %bot.setnodecolor("lshoe", "0.2 0.2 0.2 1");
        %bot.setnodecolor("rshoe", "0.2 0.2 0.2 1");
        %bot.setnodecolor("pants", "0.2 0.2 0.2 1");
        %bot.setnodecolor("chest", "0 0.5 0.25 1");
        %bot.sethat("festive");
        %bot.setmaxhealth(2500);
        %bot.name = "Giant Elf";
        %bot.mountimage(presentgunimage, 0);
        %bot.playthread(0, armreadyright);
        %bot.size = 1.5;
    }
    else if(%enemy == 16)
    {
        %bot.setnodecolor("ALL", "1 1 1 1");
        %bot.sethat("snowman");
        %bot.name = "Snow Golem";
        %bot.setmaxhealth(4250);
        %bot.attackdamage = 100;
        %bot.size = 2;
        %bot.setrunspeed(0.15);
    }
    else if(%enemy == 17)
    {
        %bot.setnodecolor("lhand", "1 0.878 0.611 1");
        %bot.setnodecolor("rhand", "1 0.878 0.611 1");
        %bot.setnodecolor("headskin", "1 0.878 0.611 1");
        %bot.setnodecolor("larm", "1 0 0 1");
        %bot.setnodecolor("rarm", "1 0 0 1");
        %bot.setnodecolor("lshoe", "1 1 1 1");
        %bot.setnodecolor("rshoe", "1 1 1 1");
        %bot.setnodecolor("pants", "1 1 1 1");
        %bot.setnodecolor("chest", "1 0 0 1");
        %bot.sethat("festive");
        %bot.setmaxhealth(32500);
        %bot.attackdamage = 10;
        %bot.size = 1.1;
        %bot.name = "Present Giver";
        %bot.boss = 1;
        %bot.mountimage(presentgunimage, 0);
        %bot.playthread(0, armreadyright);
    }
    else if(%enemy == 18)
    {
        %bot.setnodecolor("lhand", "0.181 0.635 0.789 1");
        %bot.setnodecolor("rhand", "0.181 0.635 0.789 1");
        %bot.setnodecolor("headskin", "0.2 0.2 0.2 1");
        %bot.setnodecolor("larm", "0.2 0.2 0.2 1");
        %bot.setnodecolor("rarm", "0.2 0.2 0.2 1");
        %bot.setnodecolor("lshoe", "0.2 0.2 0.2 1");
        %bot.setnodecolor("rshoe", "0.2 0.2 0.2 1");
        %bot.setnodecolor("pants", "0.2 0.2 0.2 1");
        %bot.setnodecolor("chest", "0.05 0.4 0.58 1");
        %bot.setnodecolor("epaulets", "0.181 0.4 0.58 1");
        %bot.setnodecolor("helmet", "0.2 0.2 0.2 1");
        %bot.setnodecolor("armor", "0.2 0.2 0.2 1");
        %bot.setnodecolor("visor", "0.05 0.4 0.58 1");
        %bot.setnodecolor("cape", "0.2 0.2 0.2 1");
        %bot.unhidenode("epaulets");
        %bot.unhidenode("helmet");
        %bot.unhidenode("visor");
        %bot.unhidenode("armor");
        %bot.unhidenode("cape");
        %bot.setmaxhealth(50000);
        %bot.attackdamage = 250;
        %bot.setmaxforwardspeed(2);
        %bot.name = "Frost Hero";
        %bot.size = 1.4;
        %bot.mountimage(swordimage, 0);
    }
    else if(%enemy == 19)
    {
        %bot.setnodecolor("lhand", "0.181 0.635 0.789 1");
        %bot.setnodecolor("rhand", "0.181 0.635 0.789 1");
        %bot.setnodecolor("headskin", "0 0 0 1");
        %bot.setnodecolor("larm", "0.2 0.2 0.2 1");
        %bot.setnodecolor("rarm", "0.2 0.2 0.2 1");
        %bot.setnodecolor("lshoe", "0.2 0.2 0.2 1");
        %bot.setnodecolor("rshoe", "0.2 0.2 0.2 1");
        %bot.setnodecolor("pants", "0.2 0.2 0.2 1");
        %bot.setnodecolor("chest", "0.05 0.4 0.58 1");
        %bot.setnodecolor("epaulets", "0.181 0.4 0.58 1");
        %bot.setnodecolor("helmet", "0.2 0.2 0.2 1");
        %bot.setnodecolor("armor", "0.2 0.2 0.2 1");
        %bot.setnodecolor("cape", "0.2 0.2 0.2 1");
        %bot.unhidenode("epaulets");
        %bot.unhidenode("helmet");
        %bot.unhidenode("armor");
        %bot.unhidenode("cape");
        %bot.setmaxhealth(22500);
        %bot.attackdamage = 250;
        %bot.setmaxforwardspeed(2.5);
        %bot.name = "Frost Necromancer";
        %bot.size = 1.3;
        %bot.mountimage(frozenspearimage, 0);
        %bot.playthread(0, armreadyright);
    }
    else if(%enemy == 20)
    {
        %bot.setnodecolor("lhand", "0.4 0.4 0.4 1");
        %bot.setnodecolor("rhand", "0.4 0.4 0.4 1");
        %bot.setnodecolor("headskin", "0.4 0.4 0.4 1");
        %bot.setnodecolor("larm", "1 0 0 1");
        %bot.setnodecolor("rarm", "1 0 0  1");
        %bot.setnodecolor("lshoe", "1 0 0  1");
        %bot.setnodecolor("rshoe", "1 0 0  1");
        %bot.setnodecolor("pants", "1 0 0  1");
        %bot.setnodecolor("chest", "1 0 0 1");
        %bot.setnodecolor("shoulderpads", "1 1 1 1");
        %bot.setnodecolor("helmet", "1 0 0 1");
        %bot.setnodecolor("armor", "1 1 1 1");
        %bot.setnodecolor("visor", "1 1 1 0.5");
        %bot.unhidenode("shoulderpads");
        %bot.unhidenode("helmet");
        %bot.unhidenode("armor");
        %bot.unhidenode("visor");
        %bot.sethat("festive");
        %bot.setmaxhealth(200000);
        %bot.setmaxforwardspeed(1.5);
        %bot.attackdamage = 999;
        %bot.name = "Mecha-Clause";
        %bot.boss = 1;
        %bot.mountimage(repeaterimage, 0);
        %bot.playthread(0, armreadyright);
        %bot.size = 2;
    }
    %bot.schedule(33, setplayerscale, %bot.size);
    %bot.setshapename(%bot.name, 8564862);
    %health = new AiPlayer()
	{
        position = %bot.position;
		datablock = HealthPlayer;
		mountedbot = 1;
	};
    %health.setmaxhealth(9999999);
    %health.owner = %bot;
	%bot.mountobject(%health, 7);
    %bot.healthpopup = %health;
    %health.healthpopupschedule();
    %health.setplayerscale("0.1");
	%health.setshapenamecolor("0 1 0");
	%health.setshapename(%bot.health SPC "/" SPC %bot.maxhealth, 8564862);
    if(!%bot.boss)
    {
        %bot.setshapenamecolor("1 1 1");
        %bot.setshapenamedistance("50");
    }
    else
    {
        %bot.setshapenamecolor("1 0 0");
        %bot.setshapenamedistance("100");
    }
    %bot.setmovedestination(_botcore.position);
    cdgroup.add(%bot);
    %bot.AI = schedule(33, 0, botthinkloopcd, %bot);
    %bot.reducecrystalhp();
    return %bot;
}

package CrystalDefense
{
    function armor::onreachdestination(%this, %obj)
    {
        if(%obj.cdenemy)
            %obj.setmovedestination(_botcore.position);
        parent::onreachdestination(%this, %Obj);
    }
    function armor::onCollision(%this, %obj, %col, %fade, %pos, %normal)
	{
		parent::onCollision(%this, %obj, %col, %fade, %pos, %normal);
		
		if(!isObject(%col) || !isObject(%obj))
			return;

		if(%col.getclassname() !$= "item" && %col.getstate() $= "dead")
			return;

        if(%col.getclassname() !$= "item" && %col.getstate() $= "dead" || %col.getclassname() !$= "item" && %obj.getstate() $= "dead")
            return;

        if(%obj.attackdamage == 0)
			return;

        if(%obj.CDenemy && %obj.getclassname() $= "aiplayer" && %col.getclassname() !$= "aiplayer")
        {
            %col.damage(%obj, %col.gethackposition(), %obj.attackdamage, 0);
            %col.addvelocity(vectorscale(%obj.getforwardvector(),1.5));
            %obj.playthread(2, shiftup);
        }
    }
    function Armor::onDisabled (%this, %obj, %state)
	{
        if(%obj.CDenemy)
        {
            if(%obj.name $= "Present")
            {
                %p = new projectile(){
                    datablock = cannonballprojectile;
                    initialposition = %obj.gethackposition();
                    initialvelocity = "";
                    client = %obj.client;
                    sourceobject = %obj;
                };
                %p.setscale("0.4 0.4 0.4");
                %p.explode();
            }
            %obj.setshapename("", 8564862);
            if($currentWave == 1 && %obj.name $= "Snowman")
                progressCDwave(4);
            else if($currentWave == 2)
            {
                if(%obj.name $= "Snowman")
                    progressCDwave(4);
                else if(%obj.name $= "Giant Snowman")
                    progressCDwave(7);
            }
            else if($currentWave == 3)
            {
                if(%obj.name $= "Snowman")
                    progressCDwave(3);
                else if(%obj.name $= "Frozen Blockhead")
                    progressCDwave(4);
                else if(%obj.name $= "Giant Snowman")
                    progressCDwave(7);
            }
            else if($currentWave == 4)
            {
                if(%obj.name $= "Snowman")
                    progressCDwave(3);
                else if(%obj.name $= "Frozen Blockhead")
                    progressCDwave(3);
                else if(%obj.name $= "Gunner Blockhead")
                    progressCDwave(3);
                else if(%obj.name $= "Present")
                    progressCDwave(4);
                else if(%obj.name $= "Giant Snowman")
                    progressCDwave(6);
            }
            else if($currentWave == 5)
            {
                if(%obj.name $= "Snowman")
                    progressCDwave(0.2);
                else if(%obj.name $= "Frozen Blockhead")
                    progressCDwave(0.5);
                else if(%obj.name $= "Gunner Blockhead")
                    progressCDwave(1);
                else if(%obj.name $= "Present")
                    progressCDwave(1);
                else if(%obj.name $= "Giant Snowman")
                    progressCDwave(2);
                else if(%obj.name $= "Christmas Spirit")
                    progressCDwave(100);
            }
            else if($currentWave == 6)
            {
                if(%obj.name $= "Snowman")
                    progressCDwave(3);
                else if(%obj.name $= "Frozen Blockhead")
                    progressCDwave(3);
                else if(%obj.name $= "Gunner Blockhead")
                    progressCDwave(3);
                else if(%obj.name $= "Present")
                    progressCDwave(3);
                else if(%obj.name $= "Giant Snowman")
                    progressCDwave(6);
                else if(%obj.name $= "Festive Blockhead")
                    progressCDwave(4);
                else if(%obj.name $= "Candy")
                    progressCDwave(4);
            }
            else if($currentWave == 7)
            {
                if(%obj.name $= "Snowman")
                    progressCDwave(2);
                else if(%obj.name $= "Frozen Blockhead")
                    progressCDwave(3);
                else if(%obj.name $= "Gunner Blockhead")
                    progressCDwave(3);
                else if(%obj.name $= "Present")
                    progressCDwave(3);
                else if(%obj.name $= "Giant Snowman")
                    progressCDwave(6);
                else if(%obj.name $= "Festive Blockhead")
                    progressCDwave(3);
                else if(%obj.name $= "Candy")
                    progressCDwave(3);
                else if(%obj.name $= "Spirit")
                    progressCDwave(3);
            }
            else if($currentWave == 8)
            {
                if(%obj.name $= "Snowman")
                    progressCDwave(3);
                else if(%obj.name $= "Frozen Blockhead")
                    progressCDwave(3);
                else if(%obj.name $= "Gunner Blockhead")
                    progressCDwave(3);
                else if(%obj.name $= "Present")
                    progressCDwave(3);
                else if(%obj.name $= "Giant Snowman")
                    progressCDwave(5);
                else if(%obj.name $= "Festive Blockhead")
                    progressCDwave(2);
                else if(%obj.name $= "Candy")
                    progressCDwave(2);
                else if(%obj.name $= "Spirit")
                    progressCDwave(3);
                else if(%obj.name $= "Frost")
                    progressCDwave(4);
            }
            else if($currentWave == 9)
            {
                if(%obj.name $= "Snowman")
                    progressCDwave(3);
                else if(%obj.name $= "Frozen Blockhead")
                    progressCDwave(3);
                else if(%obj.name $= "Gunner Blockhead")
                    progressCDwave(3);
                else if(%obj.name $= "Present")
                    progressCDwave(3);
                else if(%obj.name $= "Giant Snowman")
                    progressCDwave(2);
                else if(%obj.name $= "Festive Blockhead")
                    progressCDwave(2);
                else if(%obj.name $= "Candy")
                    progressCDwave(2);
                else if(%obj.name $= "Spirit")
                    progressCDwave(2);
                else if(%obj.name $= "Frost")
                    progressCDwave(3);
                else if(%obj.name $= "Frost Invader")
                    progressCDwave(4);
            }
            else if($currentWave == 10)
            {
                if(%obj.name $= "Giant Snowman")
                    progressCDwave(0.5);
                else if(%obj.name $= "Festive Blockhead")
                    progressCDwave(0.5);
                else if(%obj.name $= "Candy")
                    progressCDwave(0.5);
                else if(%obj.name $= "Spirit")
                    progressCDwave(1);
                else if(%obj.name $= "Frost")
                    progressCDwave(1);
                else if(%obj.name $= "Frost Invader")
                    progressCDwave(2);
            }
            else if($currentWave == 11)
            {
                if(%obj.name $= "Snowman")
                    progressCDwave(2);
                else if(%obj.name $= "Frozen Blockhead")
                    progressCDwave(2);
                else if(%obj.name $= "Gunner Blockhead")
                    progressCDwave(2);
                else if(%obj.name $= "Present")
                    progressCDwave(3);
                else if(%obj.name $= "Giant Snowman")
                    progressCDwave(5);
                else if(%obj.name $= "Festive Blockhead")
                    progressCDwave(3);
                else if(%obj.name $= "Candy")
                    progressCDwave(3);
                else if(%obj.name $= "Spirit")
                    progressCDwave(3);
                else if(%obj.name $= "Frost")
                    progressCDwave(3);
                else if(%obj.name $= "Frost Invader")
                    progressCDwave(6);
                else if(%obj.name $= "Elf")
                    progressCDwave(4);
                else if(%obj.name $= "Snow Golem")
                    progressCDwave(8);
            }
            else if($currentWave == 12)
            {
                if(%obj.name $= "Snowman")
                    progressCDwave(1);
                else if(%obj.name $= "Present")
                    progressCDwave(1);
                else if(%obj.name $= "Giant Snowman")
                    progressCDwave(4);
                else if(%obj.name $= "Festive Blockhead")
                    progressCDwave(2);
                else if(%obj.name $= "Candy")
                    progressCDwave(2);
                else if(%obj.name $= "Spirit")
                    progressCDwave(3);
                else if(%obj.name $= "Frost")
                    progressCDwave(3);
                else if(%obj.name $= "Frost Invader")
                    progressCDwave(6);
                else if(%obj.name $= "Elf")
                    progressCDwave(4);
                else if(%obj.name $= "Snow Golem")
                    progressCDwave(8);
                else if(%obj.name $= "Frost Ravager")
                    progressCDwave(10);
            }
            else if($currentWave == 13)
            {
                if(%obj.name $= "Snowman")
                    progressCDwave(1);
                else if(%obj.name $= "Giant Snowman")
                    progressCDwave(3);
                else if(%obj.name $= "Festive Blockhead")
                    progressCDwave(2);
                else if(%obj.name $= "Candy")
                    progressCDwave(2);
                else if(%obj.name $= "Spirit")
                    progressCDwave(2);
                else if(%obj.name $= "Frost")
                    progressCDwave(2);
                else if(%obj.name $= "Frost Invader")
                    progressCDwave(4);
                else if(%obj.name $= "Elf")
                    progressCDwave(2);
                else if(%obj.name $= "Giant Elf")
                    progressCDwave(4);
                else if(%obj.name $= "Snow Golem")
                    progressCDwave(6);
                else if(%obj.name $= "Frost Ravager")
                    progressCDwave(8);
                else if(%obj.name $= "Snow Monster")
                    progressCDwave(12);
            }
            else if($currentWave == 14)
            {
                if(%obj.name $= "Spirit")
                    progressCDwave(0.5);
                else if(%obj.name $= "Frost")
                    progressCDwave(0.5);
                else if(%obj.name $= "Frost Invader")
                    progressCDwave(0.5);
                else if(%obj.name $= "Elf")
                    progressCDwave(0.5);
                else if(%obj.name $= "Giant Elf")
                    progressCDwave(1);
                else if(%obj.name $= "Snow Golem")
                    progressCDwave(1);
                else if(%obj.name $= "Frost Ravager")
                    progressCDwave(1);
                else if(%obj.name $= "Snow Monster")
                    progressCDwave(1);
                else if(%obj.name $= "Frost Hero")
                    progressCDwave(50);
                else if(%obj.name $= "Frost Necromancer")
                    progressCDwave(50);
            }
            else if($currentWave == 15)
            {
                if(%obj.name $= "Mecha-Clause")
                {
                    $waveprogress = 100;
                    $crystaldefensestopped = 1;
                    cancel($crystaldefense);
                    for(%i = 0; %i < clientgroup.getcount(); %i++)
                    {
                        %client = clientgroup.getobject(%i);
                        if(%client.cdplayer)
                        {
                            %exp = mfloatlength(100*getrandom(100,150),0);
                            %money = mfloatlength(150 * getrandom(50,100),0);
                            %exp = mfloatlength(mpow(%exp*(%client.level/10),1+%client.level/(30+%client.level/1.35)),0);
                            %money = mfloatlength(mpow(%money*(%client.level/25),1+%client.level/(50+%client.level/1.35)),0);
                            %client.addexp(%exp, 1);
                            %client.addmoney(%money);
                            %client.setmusic("musicdata_christmas_peace");
                            %client.chatmessage("\c6Mecha-Clause has been defeated! You have unlocked the \c4Candle\c6 cosmetic! You will be teleported back soon.");
                            %client.chatmessage("\c6You have received\c2" SPC mfloatlength(%money,0) @ "$ \c6and\c4" SPC mfloatlength(%exp,0) SPC "EXP\c6!");
                            %client.playsound(beep_key_sound);
                            %client.craftedcosmetic["Candle"] = 1;
                        }
                    }
                    schedule(54000, 0, endcrystaldefense);
                }
            }
        }
        parent::onDisabled(%this, %obj, %state);
    }
    function Armor::Damage (%data, %obj, %sourceObject, %position, %damage, %damageType)
    {
        if(%obj.CDenemy)
        {
            %healthPercent = %obj.health/%obj.getmaxhealth();
            if(%healthpercent > 0.5)
                %color = (1-%healthpercent)*2 SPC 1 SPC 0;
            else if(%healthpercent < 0.5)
                %color = 1 SPC %healthpercent*2 SPC 0;
            %obj.healthpopup.setshapenamecolor(%color);
            %obj.healthpopup.setshapename(%obj.health SPC "/" SPC %obj.getmaxhealth(), 8564862);
        }
        parent::Damage(%data, %obj, %sourceObject, %position, %damage, %damageType);
        if(%obj.CDenemy && %sourceobject.sourceobject.getclassname() $= "player")
            %sourceobject.sourceobject.client.points += mfloatlength(%obj.damagedealt/25,0);
    }
};
activatepackage(crystaldefense);

function BotThinkLoopCD(%bot)
{
	if(isObject(%bot.target))
	{
		if(botWallCheck(%bot.target.getHackPosition(), %bot))
		{
			%bot.target = "";
			%bot.setMoveObject(0);
			%bot.stop();
			%bot.setAimObject(0);
			%bot.thinkSchedule = schedule(250, %bot, BotThinkLoopCD, %bot);
			return;
		}
        else if(%bot.target.getstate() $= "dead")
        {
            %bot.target = "";
			%bot.setMoveObject(0);
			%bot.stop();
			%bot.setAimObject(0);
			%bot.thinkSchedule = schedule(250, %bot, BotThinkLoopCD, %bot);
			return;
        }
		else
		{	
            if(%bot.name $= "Spirit" && %bot.target)
            {
                if(getword(%bot.target.position,2) > getword(%bot.position,2) + 1)
                {
                    %bot.settransform(vectoradd(%bot.position, "0 0 0.2"));
                    %bot.setvelocity(getwords(%bot.getvelocity(),0,1) SPC "0.1");
                }
                else if(getword(%bot.target.position,2) < getword(%bot.position,2) - 1)
                {
                    %bot.settransform(vectoradd(%bot.position, "0 0 -0.2"));
                    %bot.setvelocity(getwords(%bot.getvelocity(),0,1) SPC "-0.1");
                }
            }
            if(%bot.getmountedimage(0) && %bot.getstate() !$= "dead")
            {
                if(vectordist(%bot.position, %bot.target.position) < 6*getword(%bot.getscale(),2) && %bot.getmountedimage(0).melee)
                    %bot.setimagetrigger(0,1);
                else if(vectordist(%bot.position, %bot.target.position) > 6*getword(%bot.getscale(),2) && %bot.getmountedimage(0).melee)
                    %bot.setimagetrigger(0,0);
                else if(vectordist(%bot.position, %bot.target.position) < 60*getword(%bot.getscale(),2) && !%bot.getmountedimage(0).melee)
                {
                    %bot.setimagetrigger(0,1);
                    %bot.schedule(200, setimagetrigger, 0, 0);
                }
                else if(vectordist(%bot.position, %bot.target.position) > 60*getword(%bot.getscale(),2) && !%bot.getmountedimage(0).melee)
                    %bot.setimagetrigger(0,0);	
            }
            if(%bot.getdatablock().canjet && %bot.getstate() !$= "dead")		
            {	
                if(getword(%bot.target.getposition(),2) - getword(%bot.getposition(),2) > 2 && vectordist(%bot.getposition(), %bot.target.getposition()) < 25)
                {
                    %bot.setjumping(1);
                    %bot.setJetting(1);
                }
                else if(getword(%bot.target.getposition(),2) - getword(%bot.getposition(),2) < 2 && vectordist(%bot.getposition(), %bot.target.getposition()) < 25)
                {
                    %bot.setjumping(0);
                    %bot.setJetting(0);
                }
            }
            if(mabs(getword(%bot.getvelocity(),0)) + mabs(getword(%bot.getvelocity(),1)) < 2 && vectordist(%bot.getposition(), _botcore.position) > 5 && vectordist(%bot.getposition(), _botcore.position) < 25 && %bot.target $= "")
                %bot.setjumping(1);
            else
                %bot.setjumping(0);

            if(%bot.target && getword(%bot.target.getposition(),2) - getword(%bot.getposition(),2) > 4 && vectordist(%bot.getposition(), %bot.target.getposition()) < 15)
                %bot.setjumping(1);
            else
                %bot.setjumping(0);
            if(%bot.name !$= "Spirit")
			    %bot.thinkSchedule = schedule(250, %bot, BotThinkLoopCD, %bot);
            else
                %bot.thinkSchedule = schedule(50, %bot, BotThinkLoopCD, %bot);
			return;
		}
	}

	else
	{
		initContainerRadiusSearch(%bot.position, 125, $TypeMasks::PlayerObjectType);
		while(isobject(%search = containerSearchNext()))
		{
			if(%search.getclassname() $= "aiplayer")
                continue;
			if(botWallCheck(%search.getHackPosition(), %bot))
				continue;	
			if(isObject(%search) && vectordist(%search.position, %bot.position) < 80 && getrandom(1,20) == 1)
				%bot.target = %search;
            else if(isObject(%search) && vectordist(%search.position, %bot.position) < 60 && getrandom(1,15) == 1)
				%bot.target = %search;
            else if(isObject(%search) && vectordist(%search.position, %bot.position) < 40 && getrandom(1,10) == 1)
				%bot.target = %search;
            else if(isObject(%search) && vectordist(%search.position, %bot.position) < 30)
				%bot.target = %search;
			break;
		}
	}

	%bot.thinkSchedule = schedule(1000, %bot, BotThinkLoopCD, %bot);

	if(!isObject(%bot.target))
	{
		%bot.setmovedestination(_botcore.position);
		return;
	}

	%bot.setMoveObject(%bot.target);
	%bot.setAimObject(%bot.target);
}

function announceItemPurchase(%color, %who, %item)
{
    for(%i = 0; %i < clientgroup.getcount(); %i++)
    {
        %client = clientgroup.getobject(%i);
        if(%client.cdplayer)
        {
            %client.chatmessage(%color @ %who SPC "has purchased" SPC %item @ "!");
            %client.playsound(beep_ekg_sound);
        }
    }
}

function gameconnection::buyCDWeapon(%client, %option)
{
    if(%option == 0)
    {
        %start = _sword;
        %item = "Sword";
        %item2 = "sworditem";
        %color = "<color:8B008B>";
        %points = 0;
    }
    else if(%option == 1)
    {
        %start = _bow;
        %item = "Bow";
        %item2 = "bowitem";
        %color = "<color:8B008B>";
        %points = 0;
    }
    else if(%option == 2)
    {
        %start = _toaster;
        %item = "Toaster";
        %item2 = "toasteritem";
        %color = "<color:8B008B>";
        %points = 225;
    }
    else if(%option == 3)
    {
        %start = _snowgun;
        %item = "Snowball Gun";
        %item2 = "snowgunitem";
        %color = "<color:4682B4>";
        %points = 825;
    }
    else if(%option == 4)
    {
        %start = _bfg50;
        %item = "bfg50";
        %item2 = "bfg50item";
        %color = "<color:4682B4>";
        %points = 625;
    }
    else if(%option == 5)
    {
        %start = _thing;
        %item = "thing";
        %item2 = "thingitem";
        %color = "<color:4682B4>";
        %points = 500;
    }
    else if(%option == 6)
    {
        %start = _crucible;
        %item = "Crucible";
        %item2 = "crucibleitem";
        %color = "<color:4682B4>";
        %points = 950;
    }
    else if(%option == 7)
    {
        %start = _railgun;
        %item = "railgun";
        %item2 = "railgunitem";
        %color = "<color:4682B4>";
        %points = 650;
    }
    else if(%option == 8)
    {
        %start = _book;
        %item = "book";
        %item2 = "bookitem";
        %color = "<color:00FFFF>";
        %points = 1500;
    }
    else if(%option == 9)
    {
        %start = _airpodshotty;
        %item = "Airpod Shotty";
        %item2 = "airpodshottyitem";
        %color = "<color:00FFFF>";
        %points = 1500;
    }
    else if(%option == 10)
    {
        %start = _repeater;
        %item = "Repeater G";
        %item2 = "repeateritem";
        %color = "<color:00FFFF>";
        %points = 1650;
    }
    else if(%option == 11)
    {
        %start = _shotgun;
        %item = "Real Super Shotgun";
        %item2 = "doomshotgunitem";
        %color = "<color:00FFFF>";
        %points = 1300;
    }
    else if(%option == 12)
    {
        %start = _icebook;
        %item = "ice book";
        %item2 = "icebookitem";
        %color = "<color:00FFFF>";
        %points = 1525;
    }
    else if(%option == 13)
    {
        %start = _snowballminigun;
        %item = "Snowball Minigun";
        %item2 = "snowballminigunitem";
        %color = "<color:00FFFF>";
        %points = 2050;
    }
    else if(%option == 14)
    {
        %start = _p90;
        %item = "SC Chaingun";
        %item2 = "scp90item";
        %color = "<color:FF0000>";
        %points = 5750;
    }
    else if(%option == 15)
    {
        %start = _frost;
        %item = "Frost";
        %item2 = "frostitem";
        %color = "<color:FF0000>";
        %points = 4500;
    }
    else if(%option == 16)
    {
        %start = _book2;
        %item = "book mkII";
        %item2 = "book2item";
        %color = "<color:FF0000>";
        %points = 5150;
    }
    if(%client.points >= %points)
    {
        %start.setitem(%item2);
        announceItemPurchase(%color, %client.name, %item);
        %client.points -= %points;
        %infobrick = %start @ "info";
        %infobrick.delete();
    }
    else
    {
        %client.chatmessage("not enough money");
        %client.playsound(errorsound);
        return;
    }
    %ray = containerraycast(%start.position, vectoradd(%start.position, "0 0 1"), $typemasks::fxbrickobjecttype, %start);
    if(firstword(%ray))
        %ray.delete();
}

registeroutputevent("gameconnection", "buyCDweapon", "LIST sword 0 bow 1 toaster 2 snowballgun 3 bfg50 4 thing 5 crucible 6 railgun 7 book 8 airpod 9 repeater 10 shotgun 11 icebook 12 snowballmini 13 p90 14 frost 15 book2 16");

function player::eventbasement(%player, %option)
{
    if(%option == 0)
    {
        %player.ineventbasement = 1;
        %player.settransform(_basement2.position);
        %player.client.playsound(printfiresound);
        %player.setwhiteout(0.6);
        return;
    }
    if(%option == 1)
    {
        %player.ineventbasement = 0;
        %player.settransform("9.5 -40 5005");
        %player.client.playsound(printfiresound);
        %player.setwhiteout(0.4);
        return;
    }
    if(%option == 2)
    {
        %player.settransform(_cdshop.position SPC rotateplayer(%player, 270));
        %player.client.playsound(printfiresound);
        %player.setwhiteout(0.6);
        return;
    }
    if(%option == 3)
    {
        %player.settransform(_cdplayerspawn.position);
        %player.client.playsound(printfiresound);
        %player.setwhiteout(0.6);
        return;
    }
}

registeroutputevent("player", "eventbasement", "LIST yes 0 no 1 shop 2 spawn 3");

function player::startchristmasgamemode(%player)
{
    %ray = containerraycast(%player.geteyepoint(), vectoradd(%player.geteyepoint(), vectorscale(%player.geteyevector(), 4)), $typemasks::fxbrickobjecttype, %Obj);
    if(firstword(%ray))
    {
        for(%i = 0; %i < %player.getdatablock().maxtools; %i++)
        {
            if(%player.tool[%i].getname() $= "bluekeyitem")
            {
                %player.unmountimage(0);
                %player.tool[%i] = 0;
                messageClient(%player.client,'MsgItemPickup','',%i,0);
                break;
            }
        }
        if(%ray.getname() $= "_eventdoor")
        {
            %player.client.chatmessage("\c6You have unlocked the \c4Candle\c6 cosmetic!");
            %player.client.playsound(beep_key_sound);
            %player.client.craftedcosmetic["Candle"] = 1;
        }
    }
}


registeroutputevent("player", "startChristmasGamemode");

function GameConnection::updateCDtools(%client)
{
    %g = 0;
    %fw = new fileobject();
    %fw.openForWrite($directory @ %client.getblid() @ "/CDtools.txt");
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

function GameConnection::readCDtools(%client)
{
    %client.player.cleartools();
    %fw = new fileobject();
    %fw.openForRead($directory @ %client.getblid() @ "/CDtools.txt");
    if(isobject(%client.player))
    {
        for(%i=0;%i<%client.player.getdatablock().maxtools;%i++)
        {
            %line = %fw.readline();
            if(%line !$= "")
            {
                %client.player.tool[%i] = nameToID(%line);
                messageClient(%client,'MsgItemPickup','',%i,nameToID(%line));
            }
        }
    }
    %fw.close();
    %fw.delete();
}

registeroutputevent("gameconnection", "updateCDtools");
registeroutputevent("gameconnection", "readCDtools");

function eraseCDtools(%client)
{
    %fw = new FileObject();
    for(%file = findfirstfile($directory @ "*/CDtools.txt"); isfile(%file); %file = findnextfile($directory @ "*/CDtools.txt"))
    {
        %fw.openforwrite(%file);
        %fw.close();
        %fw.delete();
    }
}

function aiplayer::reducecrystalhp(%this)
{
    if(%this.getstate() $= "dead")
        return;
    if(getword(%this.position,0) > 10123.5 && getword(%this.position,0) < 10127.5 && getword(%this.position,1) > 9980 && getword(%this.position,1) < 9984 && getword(%this.position,2) > 8 && getword(%this.position,2) < 12)
        $crystalHP--;
    if($crystalHP <= 0 && !$crystaldefensestopped)
    {
        $crystaldefensestopped = 1;
        cancel($crystaldefense);
        for(%i = 0; %i < clientgroup.getcount(); %i++)
        {
            %client = clientgroup.getobject(%i);
            if(%client.cdplayer)
            {
                %client.setmusic("");
                %client.chatmessage("your crystal has been destroyed!!! you will be teleported back to spawn soon");
                %client.playsound(glassexplosionsound);
            }
        }
        schedule(15000, 0, endcrystaldefense);
    }
    %this.schedule(100, reducecrystalhp);
}

function endCrystalDefense()
{
    cancel(_eventdoor.diggerdoorschedule);
    _eventdoor.schedule(60000, eventdoorreappear);
    cdgroup.deleteall();
    cdbrickgroup.deleteall();
    $crystaldefensestopped = 0;
    for(%i = 0; %i < clientgroup.getcount(); %i++)
    {
        %client = clientgroup.getobject(%i);
        if(%client.cdplayer)
        {
            cancel(%client.cdhud);
            %client.schedule(33, displayhud);
            %client.CDplayer = 0;
            %client.spawnplayer();
        }
    }
}

function fxdtsbrick::eventdoorreappear(%this)
{
    if(%this.doorreappear >= 60)
    {
        %this.disappear(0);
        %this.doorreappear = 0;
    }
    else
    {
        %this.doorreappear++;
        %this.diggerdoorschedule = %this.schedule(60000, eventdoorreappear);
    }
}

function eulerToAxis(%euler)
{
	%euler = VectorScale(%euler,$pi / 180);
	%matrix = MatrixCreateFromEuler(%euler);
	return getWords(%matrix,3,6);
}

function axisToEuler(%axis)
{
	%angleOver2 = getWord(%axis,3) * 0.5;
	%angleOver2 = -%angleOver2;
	%sinThetaOver2 = mSin(%angleOver2);
	%cosThetaOver2 = mCos(%angleOver2);
	%q0 = %cosThetaOver2;
	%q1 = getWord(%axis,0) * %sinThetaOver2;
	%q2 = getWord(%axis,1) * %sinThetaOver2;
	%q3 = getWord(%axis,2) * %sinThetaOver2;
	%q0q0 = %q0 * %q0;
	%q1q2 = %q1 * %q2;
	%q0q3 = %q0 * %q3;
	%q1q3 = %q1 * %q3;
	%q0q2 = %q0 * %q2;
	%q2q2 = %q2 * %q2;
	%q2q3 = %q2 * %q3;
	%q0q1 = %q0 * %q1;
	%q3q3 = %q3 * %q3;
	%m13 = 2.0 * (%q1q3 - %q0q2);
	%m21 = 2.0 * (%q1q2 - %q0q3);
	%m22 = 2.0 * %q0q0 - 1.0 + 2.0 * %q2q2;
	%m23 = 2.0 * (%q2q3 + %q0q1);
	%m33 = 2.0 * %q0q0 - 1.0 + 2.0 * %q3q3;
	return mRadToDeg(mAsin(%m23)) SPC mRadToDeg(mAtan(-%m13, %m33)) SPC mRadToDeg(mAtan(-%m21, %m22));
}

function rotatePlayer(%player, %rotate)
{
    %eulerRotation = axisToEuler(getWords((%transform = %player.getTransform()),3,6));
	%eulerXY = getWords(%eulerRotation,0,1);
	%eulerZ = %rotate;
    return eulerToAxis(%eulerXY SPC %eulerZ);
}