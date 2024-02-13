function botWallCheck(%pos,%player)
{
	%position = %player.geteyepoint();
    %object = ContainerRayCast(%pos, %position, $TypeMasks::FxBrickObjectType, %player);
 	%obstr = getWord(%object,0);
	%position2 = %player.gethackposition();
    %object2 = ContainerRayCast(%pos, %position2, $TypeMasks::FxBrickObjectType, %player);
 	%obstr2 = getWord(%object2,0);
	%position3 = vectoradd(%player.gethackposition(), vectorscale(vectorcross(%player.getforwardvector(), "0 0 1"),2));
    %object3 = ContainerRayCast(%pos, %position3, $TypeMasks::FxBrickObjectType, %player);
 	%obstr3 = getWord(%object3,0);
	%position4 = vectoradd(%player.gethackposition(), vectorscale(vectorcross(%player.getforwardvector(), "0 0 -1"),2));
    %object4 = ContainerRayCast(%pos, %position4, $TypeMasks::FxBrickObjectType, %player);
 	%obstr4 = getWord(%object4,0);
    if(isObject(%obstr) && isObject(%obstr2) && isObject(%obstr3) && isObject(%obstr4))
       return 1;
    else
       return 0;
}

function generateEnemy(%position, %type)
{
	%depth = 5000-getword(%position,2);
	%bot = new AiPlayer(MiningMonster)
	{
        position = %position;
		datablock = PlayerStandardArmor;
		miningAI = true;
        minigame = $miningMinigame;
	};

    %bot.setmoveslowdown(0);
	%bot.client = %bot;
	if(%rot == 0)
	{
		%bot.rotation = "0 0 1" SPC getrandom(0,200);
		%bot.setplayerscale(getword(%bot.getscale(),0) + 0.1);
		%bot.setplayerscale(getword(%bot.getscale(),0) - 0.1);
	}
	if(%rot == 1)
	{
		%bot.rotation = "0 0 -1" SPC getrandom(0,100);
		%bot.setplayerscale(getword(%bot.getscale(),0) + 0.1);
		%bot.setplayerscale(getword(%bot.getscale(),0) - 0.1);
	}
    switch(%type)
	{
		case 1:
			%bot.attackDamage = 3 + mfloatlength(%depth/250,0);
			if(%bot.attackdamage > 10)
				%bot.attackdamage = 10;
			%bot.health = mfloatlength(%depth/125,0) * getRandom(40, 60);
			%bot.name = "Zombie";
            %bot.exp = mfloatlength(%depth/45 * getRandom(25,75) * %depth/666,0);
			%bot.money = mfloatlength(%depth/40 * getRandom(15,45) * %depth/888,0);
            %bot.startAi = schedule(500, 0, botthinkloopzombie, %bot);
            %bot.playthread(0, armreadyboth);
            %bot.setdatablock(playerzombie);
            %bot.setnodecolor("rshoe", "0 0.141 0.333 1");
            %bot.setnodecolor("lshoe", "0 0.141 0.333 1");
            %bot.setnodecolor("pants", "0 0.141 0.333 1");
            %bot.setnodecolor("chest", "0.75 0.75 0.75 1");
            %bot.setnodecolor("rarm", "0.593 0 0 1");
            %bot.setnodecolor("larm", "0.593 0 0 1");
            %bot.setnodecolor("rhand", "0.626 0.71 0.453 1");
            %bot.setnodecolor("lhand", "0.626 0.71 0.453 1");
            %bot.setnodecolor("headskin", "0.626 0.71 0.453 1");
            %bot.setfacename("asciiterror");
			%bot.setmaxforwardspeed(8.5);

		case 2:
			%bot.health = mfloatlength(%depth/125,0) * getRandom(30, 50);
			%bot.name = "Creeper";
            %bot.exp = mfloatlength(%depth/35 * getRandom(30,80) * %depth/555,0);
			%bot.money = mfloatlength(%depth/30 * getRandom(20,50) * %depth/777,0);
            %bot.startAi = schedule(500, 0, botthinkloopcreeper, %bot);
            %bot.setdatablock(playermccreeper);
			%bot.setmaxforwardspeed(7);

		case 3:
			%bot.attackDamage = 3 + mfloatlength(%depth/250,0);
			if(%bot.attackdamage > 10)
				%bot.attackdamage = 10;
			%bot.health = mfloatlength(%depth/125,0) * getRandom(40, 60);
			%bot.name = "renderman";
            %bot.exp = mfloatlength(%depth/45 * getRandom(25,75) * %depth/666,0);
			%bot.money = mfloatlength(%depth/40 * getRandom(15,45) * %depth/888,0);
            %bot.startAi = schedule(500, 0, botthinkloopzombie, %bot);
            %bot.playthread(0, armreadyboth);
            %bot.setdatablock(playerzombie);
            %bot.setnodecolor("ALL", "0 0 0 1");
			%bot.setscale(getrandom(5,20)/10 SPC getrandom(5,20)/10 SPC getrandom(5,20)/10);
			%face = getrandom(0,2);
			if(%face == 0) 
            	%bot.setfacename("asciiterror");
			else if(%face == 1) 
            	%bot.setfacename("memegrinman");
			else if(%face == 2) 
            	%bot.setfacename("memeyaranika");
			%bot.setmaxforwardspeed(8.5);
	}
	%health = new AiPlayer()
	{
        position = %bot.position;
		datablock = PlayerHealth;
		mountedbot = 1;
	};
	%health.isinvincible = 1;
	%bot.mountobject(%health, 7);
	%health.setplayerscale("0.1");
    %health.hidenode("ALL");
	%health.owner = %bot;
	%bot.healthpopup = %health;
	%health.healthpopupschedule();
    %bot.setshapename(%bot.name, 8564862);
    %bot.setshapenamecolor("1 0 0");
    %bot.setshapenamedistance("32");
    nameToID(CaveGroup).add(%bot);
	%bot.exp = mfloatlength(%bot.exp,0);
	%bot.setmaxhealth(%bot.health);
	%health.setplayerscale("0.3");
	%health.setshapenamecolor("0 1 0");
	%health.setshapename(%bot.health SPC "/" SPC %bot.maxhealth, 8564862);
    return %bot;
}

function BotThinkLoopZombie(%bot)
{
	if(isObject(%bot.target))
	{
		%bot.unactivestate = 0;
		if(botWallCheck(%bot.target.getHackPosition(), %bot))
		{
			%bot.target = "";
			%bot.setMoveObject(0);
			%bot.stop();
			%bot.setAimObject(0);
			%bot.thinkSchedule = schedule(250, %bot, BotThinkLoopZombie, %bot);
			return;
		}

		else
		{			
			if(getword(%bot.target.getposition(),2) - getword(%bot.getposition(),2) > 2.5 && vectordist(%bot.getposition(), %bot.target.getposition()) < 15)
			{
				%bot.setJumping(1);
				%bot.schedule(200, setJumping, 0);
			}
			%bot.thinkSchedule = schedule(250, %bot, BotThinkLoopZombie, %bot);
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
			if(isObject(%search))
				%bot.target = %search;
			break;
		}
	}

	%bot.thinkSchedule = schedule(1000, %bot, BotThinkLoopZombie, %bot);

	if(!isObject(%bot.target))
	{
		%bot.unactivestate++;
		if(%bot.unactivestate >= 150)
			%bot.delete();
		return;
	}

	%bot.setMoveObject(%bot.target);
	%bot.setAimObject(%bot.target);
}

function BotThinkLoopCreeper(%bot)
{
	if(isObject(%bot.target))
	{
		%bot.unactivestate = 0;
		if(botWallCheck(%bot.target.getHackPosition(), %bot))
		{
			%bot.target = "";
			%bot.setMoveObject(0);
			%bot.stop();
			%bot.setAimObject(0);
			%bot.thinkSchedule = schedule(250, %bot, BotThinkLoopCreeper, %bot);
			return;
		}

		else
		{	
			if(vectordist(getwords(%bot.target.getposition(),0,1),getwords(%bot.getposition(),0,1)) < 6 && vectordist(getword(%bot.target.getposition(),2),getword(%bot.getposition(),2)) < 9)
			{
				cancel(%bot.creepsched);
				%bot.creepTime = getSimTime();
				%bot.creepSched = %bot.schedule(5, "creeperSwell");
				%bot.playAudio(0, CreeperFuseSound);
			}
			else
			{
				%bot.setmovey(1);
				cancel(%bot.creepSched);
				%bot.creepTime = getSimTime();
				%bot.creepSched = %bot.schedule(5, "creeperShrink");
				%bot.stopAudio(0);	
			}
			if(vectordist(getwords(%bot.target.getposition(),0,1),getwords(%bot.getposition(),0,1)) < 3.5)
				%bot.setmovey(0);
			else
				%bot.setmovey(1);
			if(getword(%bot.target.getposition(),2) - getword(%bot.getposition(),2) > 2.5 && vectordist(%bot.getposition(), %bot.target.getposition()) < 15)
			{
				%bot.setJumping(1);
				%bot.schedule(200, setJumping, 0);
			}
			%bot.thinkSchedule = schedule(250, %bot, BotThinkLoopCreeper, %bot);
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
			if(isObject(%search))
				%bot.target = %search;
			break;
		}
	}

	%bot.thinkSchedule = schedule(1000, %bot, BotThinkLoopCreeper, %bot);

	if(!isObject(%bot.target))
	{
		%bot.unactivestate++;
		if(%bot.unactivestate >= 150)
			%bot.delete();
		return;
	}

	%bot.setMoveObject(%bot.target);
	%bot.setAimObject(%bot.target);
}

function rollZombieOre(%depth)
{
	%total = 0;
	for(%i = 0; %i < $orecount; %i++)
	{
		%ore = $ore[%i];
		%name = getfield(%ore,0);
		%oretag = getfield(%ore,12);
		%MinDepth = getfield(%ore,10);
    	%MaxDepth = getfield(%ore,11);
		if(%oretag $= "IGNORE" || %oretag $= "TREASURECHEST" || %oretag $= "NOORE" || %oretag $= "CRATE" || %oretag $= "CRATEVAULT" || %oretag $= "QUANTUMDISRUPTION" || %oretag $= "KEY" || %oretag $= "PRESENT" || %oretag $= "EVENT" || %name $= "Key Fragment")
			continue;
		if(%depth > %mindepth && %depth < %maxdepth)
		{
			%ores[%total] = getfield(%ore,0);
			%total++;
		}
	}
	return %ores[getrandom(0, %total-1)];
}

function player::healthpopupschedule(%this)
{
	if(!isobject(%this.owner))
		%this.delete();
	else
		%this.schedule(250, healthpopupschedule);
}

package miningstuff2
{
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

        if(%obj.miningAI && %obj.getclassname() $= "aiplayer" && %col.getclassname() !$= "aiplayer")
        {
            %col.damage(%obj, %col.gethackposition(), %obj.attackdamage, 0);
            %col.addvelocity(vectorscale(%obj.getforwardvector(),1.5));
            %obj.playthread(2, shiftup);
        }
    }
    function aiPlayer::onDeath(%this)
	{
		if(%this.healthpopup)
			%this.healthpopup.delete();
        if(%this.miningAI)
            %this.setshapename("", 8564862);
        if(%this.name !$= "Digger Boss" && %this.miningAI && %this.lastkillerclient.getclassname() $= "gameconnection" && !%this.claimedDrops)
        {
			%this.claimedDrops = 1;
            %exp = mfloatlength(%this.exp * (1+%this.lastkillerclient.prestigeexpbonus+%this.lastkillerclient.achievementexpbonus),0);
            %money = mfloatlength(%this.money * (1+%this.lastkillerclient.prestigecashbonus+%this.lastkillerclient.achievementcashbonus),0);
            %this.lastkillerclient.addexp(%this.exp);
            %this.lastkillerclient.addmoney(%money);
            %this.lastkillerclient.chatmessage("\c6You have received\c2" SPC %money @ "$ \c6and\c4" SPC %exp SPC "EXP\c6!");
			if(getrandom(1,4) == 1)
			{
				%depth = 5000 - getword(%this.getposition(),2);
				%ore = rollzombieore(%depth);
				%oreamount = mfloatlength(getrandom(1,5) * (%depth - getfield($ore[oreidfromname(%ore)],10))/85,0);
				if(%oreamount < 1)
					%oreamount = getrandom(1,5);
				%color = convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[oreidfromname(%ore)],5)),0) * 255));
				%color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[oreidfromname(%ore)],5)),1) * 255));
				%color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[oreidfromname(%ore)],5)),2) * 255));
				%color = "<color:" @ %color @ ">";
				%this.lastkillerclient.chatmessage("\c6The" SPC %this.name SPC "had dropped" @ %color SPC %oreamount @ "x" SPC %ore @ "\c6!");
				%this.lastkillerclient.inventory[%ore] += %oreamount;
			}
        }
        return;
    }
};
activatepackage(miningstuff2);