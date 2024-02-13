if(!isobject(diggerbossgroup))
    new simgroup(diggerbossgroup){};
if(!isobject(brickgroup_777777))
    new simgroup(brickgroup_777777){};

package diggerboss
{
    function gameConnection::onDeath(%client,%source,%killer,%type,%location)
    {
        cancel(%client.diggerMusic);
        %totalpeople = 0;
        for(%i = 0; %i < clientgroup.getcount(); %i++)
        {
            %clients = clientgroup.getobject(%i);
            if(%clients.player.fightingdigger && %clients.player.getstate() !$= "dead")
                %totalpeople++;
        }
        if(%totalpeople <= 0)
            schedule(5000, 0, removediggerbossgroup);
        parent::onDeath(%client,%source,%killer,%type,%location);
    }
    function aiplayer::ondeath(%this)
    {
        if(%this.mininghelmet)
        {
            %this.mininghelmet.schedule($corpsetimeoutvalue, delete);
            %this.mininghelmet.light.schedule($corpsetimeoutvalue, delete);
            for(%i = 0; %i < clientgroup.getcount(); %i++)
            {
                %client = clientgroup.getobject(%i);
                %level = %client.level;
                if(%level > 50)
                {
                    %level = %level - mpow(msqrt(%level),1.33)*1.1; 
                    %above50 = 1;
                }
                if(%above50 && %level < 50)
                    %level = 50*1.2;
                if(%client.player.fightingdigger)
                {
                    %level = %client.level;
                    %percent = %level/$totallevels;
                    %exp = mfloatlength($totallevels*getrandom(100,150),0);
                    %money = mfloatlength($totallevels * getrandom(75,125),0);
                    %exp = mfloatlength(mpow(%exp*(%level/10),1+%level/(30+%level/1.3)),0);
                    %money = mfloatlength(mpow(%money*(%level/22.5),1+%level/(50+%level/1.35)),0);
                    %client.unlockachievement("all your ores are belong to us");
                    if($totalpeople == 1)
                    {
                        %exp *= 1.5;
                        %money *= 1.5;
                    }
                    else if($totalpeople == 2)
                    {
                        %exp *= 1.33;
                        %money *= 1.33;
                    }
                    else if($totalpeople == 3)
                    {
                        %exp *= 1.125;
                        %money *= 1.125;
                    }
                    if($challengeDigger)
                    {
                        %client.setmusic("musicdata_unmaykr_obliteration_end", 1);
                        %exp *= 4;
                        %money *= 4;
                        %client.unlockachievement("digger's invocation");
                    }
                    if(!$rewardsDisabled)
                    {
                        %client.addexp(%exp, 1);
                        %client.addmoney(%money);
                    }
                    %client.chatmessage("\c6You have received\c2" SPC mfloatlength(%money,0) @ "$ \c6and\c4" SPC mfloatlength(%exp,0) SPC "EXP\c6! You will be teleported back shortly.");
                    schedule(10000, 0, teleportfromdiggerboss);
                }
                if(%client.dieddigger)
                {
                    %client.dieddigger = 0;
                    %exp = mfloatlength($totallevels*getrandom(100,150),0);
                    %money = mfloatlength($totallevels * getrandom(75,125),0);
                    %exp = mfloatlength(mpow(%exp*(%level/10),1+%level/(30+%level/1.3)),0);
                    %money = mfloatlength(mpow(%money*(%level/22.5),1+%level/(50+%level/1.35)),0);
                    if($totalpeople == 1)
                    {
                        %exp *= 1.5;
                        %money *= 1.5;
                    }
                    else if($totalpeople == 2)
                    {
                        %exp *= 1.33;
                        %money *= 1.33;
                    }
                    else if($totalpeople == 3)
                    {
                        %exp *= 1.125;
                        %money *= 1.125;
                    }
                    if($challengeDigger)
                    {
                        %exp *= 4;
                        %money *= 4;
                    }
                    if(!$rewardsDisabled)
                    {
                        %client.addexp(%exp*0.2, 1);
                        %client.addmoney(%money*0.2);
                    }
                    %client.chatmessage("\c6You have received\c2" SPC mfloatlength(%money*0.2,0) @ "$ \c6and\c4" SPC mfloatlength(%exp*0.2,0) SPC "EXP\c6 for participating in the boss fight!");
                }
            }
        }
        parent::ondeath(%this);
    }
    function armor::onreachdestination(%this, %obj)
    {
        if(%obj.getdatablock().getname() $= "playerdiggerboss")
        {
            %obj.mountimage(diggerpickaxe2image, 0);
            %obj.setimagetrigger(0,1);
            %obj.schedule(1250, setimagetrigger, 0,0);
            if(!$challengeDigger)
                %obj.schedule(1250, mountimage, diggerpickaxephase2image, 0);
            else
                %obj.schedule(1250, mountimage, diggerpickaxechallengephase2image, 0);
            %obj.schedule(1000, phase2bricks);
            %obj.phase2 = 1;
            %obj.phase2time = $sim::time;
        }
        parent::onreachdestination(%this, %Obj);
    }
    function Armor::onNewDataBlock (%this, %player)
    {
        parent::onNewDataBlock (%this, %player);
        if(%player.getclassname() $= "player" && isobject(%player.mininghelmet))
            %player.mountobject(%player.mininghelmet, 5);
        if(%player.getclassname() $= "player" && isobject(%player.cryogenumtank))
            %player.mountobject(%player.cryogenumtank, 7);
    }
    function serverDirectSaveFileLoad (%filename, %colorMethod, %dirName, %ownership, %silent)
    {
        $loadingsavefile = %filename;
        parent::serverDirectSaveFileLoad (%filename, %colorMethod, %dirName, %ownership, %silent);
    }
    function ServerLoadSaveFile_End ()
    {
        $loadingsavefile = "";
        parent::ServerLoadSaveFile_End();
    }
};
activatepackage(Diggerboss);

function player::diggerLavaDamage(%player)
{
    if(%player.lastaddhealthtime + 0.05 > $sim::time)
        return;
    %player.lastaddhealthtime = $sim::time;
    if(%player.getdatablock().getname() $= "playerdiggerboss")
    {
        %player.addvelocity(getwords(vectorscale(%player.getforwardvector(), -3),0,1) SPC 40);
        return;
    }
    if($challengeDigger)
        %player.addhealth(-50);
    else
        %player.addhealth(-10);
    %player.burnplayer(3);
    %Player.addvelocity("0 0 30");
}

registeroutputevent("player", "diggerLavaDamage");

function aiplayer::phase2bricks(%this)
{
    initContainerRadiusSearch(%this.position, 64, $typemasks::fxbrickobjecttype);
    while(isobject(%search=containersearchnext()))
    {
        if(!$challengeDigger)
        {
            if(%search.getdatablock().getname() $= "brick16xcubedata" && %search.colorid == 6 && getword(%search.position,2) $= "5996.6")
            {
                %random = getrandom(1,100);
                if(%random >= 35)
                {
                    %search.setcolor(7);
                    %search.setcolorfx(2);
                    %ray = containerraycast(%search.position, vectorsub(%search.position, "0 0 18"), $typemasks::fxbrickobjecttype, %search);
                    if(firstword(%ray))
                    {
                        %ray.setcolor(7);
                        %ray.setcolorfx(2);
                    }
                }
            }
        }
        else
        {
            if(%search.getdatablock().getname() $= "brick16xcubedata" && %search.colorid == 6 && getword(%search.position,2) $= "5996.6")
            {
                %random = getrandom(1,100);
                if(%random >= 50)
                {
                    %search.setcolor(7);
                    %search.setcolorfx(2);
                    %ray = containerraycast(%search.position, vectorsub(%search.position, "0 0 18"), $typemasks::fxbrickobjecttype, %search);
                    if(firstword(%ray))
                    {
                        %ray.setcolor(7);
                        %ray.setcolorfx(2);
                    }
                }
            }
            else if(%search.getdatablock().getname() $= "brick16xcubedata" && %search.colorid == 7)
            {
                %random = getrandom(1,100);
                if(%random >= 50)
                {
                    %search.fakekillbrick("0 0 0", 1);
                    %search.schedule(1000, delete);
                    %ray2 = containerraycast(%search.position, vectorsub(%search.position, "0 0 18"), $typemasks::fxbrickobjecttype, %search);
                    if(firstword(%ray2) && %ray2.colorid == 7)
                    {
                        %ray2.fakekillbrick("0 0 0", 1);
                        %ray2.schedule(1000, delete);
                    }
                }
            }
        }
    }
}

function player::startdiggerboss(%player)
{
    %ray = containerraycast(%player.geteyepoint(), vectoradd(%player.geteyepoint(), vectorscale(%player.geteyevector(), 4)), $typemasks::fxbrickobjecttype, %Obj);
    if(firstword(%ray))
    {
        $challengeDigger = 0;
        removediggerbossgroup();
        for(%i = 0; %i < %player.getdatablock().maxtools; %i++)
        {
            if(%player.tool[%i].getname() $= "yellowkeyitem")
            {
                %player.unmountimage(0);
                %player.tool[%i] = 0;
                messageClient(%player.client,'MsgItemPickup','',%i,0);
                break;
            }
        }
        if(%ray.getname() $= "_diggerdoor")
        {
            brickgroup_777777.deleteall();
            serverdirectsavefileload("saves/boss arena.bls", 3, "", 1, 1);
            %ray.disappear(-1);
            cancel(%ray.diggerdoorschedule);
            %ray.schedule(60000, diggerdoorreappear);
            schedule(30000, 0, teleporttodiggerboss);
            for(%i = 0; %i < clientgroup.getcount(); %i++)
            {
                %client = clientgroup.getobject(%i);
                if(%client.player.inbasement)
                    %client.chatmessage("\c4the digger door has been opened!!!! all of you will be teleported in 30 seconds (unless you leave the room)");
            }
        }
    }
}

function player::startchallengediggerboss(%player)
{
    %ray = containerraycast(%player.geteyepoint(), vectoradd(%player.geteyepoint(), vectorscale(%player.geteyevector(), 4)), $typemasks::fxbrickobjecttype, %Obj);
    if(firstword(%ray))
    {
        $challengeDigger = 1;
        removediggerbossgroup();
        for(%i = 0; %i < %player.getdatablock().maxtools; %i++)
        {
            if(%player.tool[%i].getname() $= "redkeyitem")
            {
                %player.unmountimage(0);
                %player.tool[%i] = 0;
                messageClient(%player.client,'MsgItemPickup','',%i,0);
                break;
            }
        }
        if(%ray.getname() $= "_diggerdoor")
        {
            brickgroup_777777.deleteall();
            serverdirectsavefileload("saves/boss arena.bls", 3, "", 1, 1);
            %ray.disappear(-1);
            cancel(%ray.diggerdoorschedule);
            %ray.schedule(60000, diggerdoorreappear);
            schedule(30000, 0, teleporttodiggerboss);
            for(%i = 0; %i < clientgroup.getcount(); %i++)
            {
                %client = clientgroup.getobject(%i);
                if(%client.player.inbasement)
                    %client.chatmessage("\c0i think you want to die, all of you will be teleported in 30 seconds (unless you leave the room)");
            }
        }
    }
}

function removediggerbossgroup()
{
    diggerbossgroup.deleteall();
}

function fxdtsbrick::diggerdoorreappear(%this)
{
    if(%this.doorreappear >= 60)
    {
        %this.disappear(0);
        %this.doorreappear = 0;
        removediggerbossgroup();
    }
    else
    {
        %this.doorreappear++;
        %this.diggerdoorschedule = %this.schedule(60000, diggerdoorreappear);
    }
}

registeroutputevent("player", "startdiggerboss");
registeroutputevent("player", "startchallengediggerboss");

function teleporttodiggerboss()
{
    for(%i = 0; %i < clientgroup.getcount(); %i++)
    {
        %client = clientgroup.getobject(%i);
        if(%client.player.inbasement)
        {
            if(%client.player.getmountedimage(0) == nametoid(blankaballimage))
                %client.player.unmountimage(0);
            %tp = _diggerbosstp @ getrandom(1,4);
            %client.player.settransform(%tp.position);
            %client.dieddigger = 0;
            %client.player.fightingdigger = 1;
            %level = %client.level;
            %realtotallevels += %level;
            if(%level > 50)
            {
                 %level = %level - mpow(msqrt(%level),1.33)*1.1; 
                %above50 = 1;
            }
            if(%above50 && %level < 50)
                %level = 50*1.2;
            %totallevels += %level;
            %client.player.setdatablock(player12slotnojet);
            %totalpeople++;
            if($challengeDigger)
                %client.player.setmaxhealth(200);
        }
    }
    $totalpeople = %totalpeople;
    if(%totallevels > 500)
        %totallevels = 500;
    $totallevels = %totallevels;
    if(!$challengeDigger)
    {
        $bosshealth = mfloatlength(15000 + mpow((%totallevels * 175 * 1.35),1.1),0);
        generatediggerboss(_diggerbossspawn.position);
    }
    else
    {
        $bosshealth = mfloatlength(32500 + mpow((%totallevels * 180 * 1.37),1.11),0);
        generatediggerboss(_diggerbossspawn.position, 1);
    }
    $totallevels = %realtotallevels;
}

function teleportfromdiggerboss()
{
    for(%i = 0; %i < clientgroup.getcount(); %i++)
    {
        %client = clientgroup.getobject(%i);
        if(%client.player.fightingdigger)
        {
            %client.spawnplayer();
        }
    }
}

datablock PlayerData(PlayerDiggerBoss : PlayerStandardArmor)
{
	canJet = 0;
	mass = 90*10;
    runforce = 4320*10;
    jumpforce = 1080*10;
};

function generateDiggerBoss(%position, %challenge)
{
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

    %bot.playthread(0, armreadyboth);
	%bot.name = "Digger Boss";
    if(!%challenge)
    {
        %bot.startAi = schedule(5000, 0, botthinkloopdiggerboss, %bot);
        %bot.mountimage(diggerpickaxeimage, 0);
        %bot.setplayerscale(1.5);
        %bot.setmaxbackwardspeed(4);
        %bot.setmaxforwardspeed(5.75);
    }
    else
    {
        %bot.startAi = schedule(5000, 0, botthinkloopdiggerboss2, %bot);
        %bot.mountimage(diggerpickaxechallengeimage, 0);
        %bot.setplayerscale(1.55);
        %bot.setmaxbackwardspeed(4.25);
        %bot.setmaxforwardspeed(6);
    }
    %bot.setdatablock(playerdiggerboss);
    %bot.setnodecolor("rshoe", "0.15 0.15 0.15 1");
    %bot.setnodecolor("lshoe", "0.15 0.15 0.15 1");
    %bot.setnodecolor("pants", "0.15 0.15 0.15 1");
    %bot.setnodecolor("chest", "0.275 0.275 0.275 1");
    %bot.unhidenode("armor");
    %bot.setnodecolor("armor", "0.2 0.2 0.2 1");
    %bot.setnodecolor("rarm", "0.35 0.35 0.35 1");
    %bot.setnodecolor("larm", "0.35 0.35 0.35 1");
    %bot.setnodecolor("headskin", "1 0.878 0.611 1");
    %bot.setshapenamecolor("1 0 0");
    %bot.setshapename(%bot.name, 8564862);
    %bot.setmaxhealth($bosshealth);
    %health = new AiPlayer()
	{
        position = %bot.position;
		datablock = PlayerHealth;
		mountedbot = 1;
	};
    %health.isinvincible = 1;
    %health.setplayerscale("0.1");
    %health.hidenode("ALL");
    %health.owner = %bot;
    %bot.healthpopup = %health;
    %health.healthpopupschedule();
    %bot.mountobject(%health, 7);
    %health.setmaxhealth(999999);
    %health.setplayerscale("0.1");
	%health.setshapenamecolor("0 1 0");
	%health.setshapename(%bot.health SPC "/" SPC %bot.maxhealth, 8564862);
    %bot.isinvincible = 1;
    %bot.schedule(5000, removeinvuln);
    nametoid(Diggerbossgroup).add(%bot);

    %MiningHelmet = new AIPlayer(){
		dataBlock = MiningHelmetPlayer;
		Wearer = %bot; //Store who is wearing this mining helmet.
        scale = %bot.getscale();
	};

    %light = new fxLight(){
		dataBlock = MiningHelmetBrightLight; //Assume maximum brightness.
		enable = true;
		iconSize = 1;
	};
    %mininghelmet.light = %light;
	%light.attachToObject(%MiningHelmet);
    %bot.mininghelmet = %mininghelmet;
    %bot.mountObject(%MiningHelmet,5);
    nametoid(diggerbossgroup).add(%health);
    nametoid(diggerbossgroup).add(%mininghelmet);
    nametoid(diggerbossgroup).add(%mininghelmet.light);
}

function aiplayer::removeinvuln(%bot)
{
    %bot.isinvincible = 0;
}

function aiplayer::lookattarget(%bot)
{
    if(isobject(%bot.target))
        %bot.setaimvector(getwords(vectornormalize(vectorsub(%bot.target.gethackposition(), %bot.gethackposition())),0,1) SPC 0);
}

function BotThinkLoopDiggerBoss(%bot)
{
    if(%bot.getstate() $= "dead")
        return;
    if(%bot.gethealth() / %bot.getmaxhealth() <= 0.5 && !%bot.phase2)
    {
        if(!%bot.attacking)
        {
            %bot.isinvincible = 1;
            %bot.target = "";
            %bot.setmovey(0);
            %bot.setaimobject(0);
            %bot.stop();
            %bot.setmovedestination(_diggerbossspawn.position);
            %bot.thinkSchedule = schedule(250, %bot, BotThinkLoopDiggerBoss, %bot);
            return;
        }
    }
    if(%bot.phase2 && !%bot.phase2complete)
    {
        if(%bot.phase2 && %bot.phase2time + 1.5 > $sim::time)
        {
            %bot.thinkSchedule = schedule(250, %bot, BotThinkLoopDiggerBoss, %bot);
            return;
        }
        else if(%bot.phase2 && %bot.phase2time + 1.5 < $sim::time)
        {
            %bot.phase2complete = 1;
            %bot.isinvincible = 0;
            %bot.thinkSchedule = schedule(250, %bot, BotThinkLoopDiggerBoss, %bot);
            return;
        }
    }
	if(isObject(%bot.target))
	{
		if(botWallCheck(%bot.target.getHackPosition(), %bot))
		{
			%bot.target = "";
			%bot.setMoveObject(0);
			%bot.stop();
			%bot.setAimObject(0);
			%bot.thinkSchedule = schedule(250, %bot, BotThinkLoopDiggerBoss, %bot);
			return;
		}
		else
		{	
            %bot.targetchange++;
            if(%bot.targetchange >= 100 && !%bot.attacking)
            {
                %bot.oldtarget = %bot.target;
                %bot.targetchange = 0;
                if(getrandom(1,3) == 1)
                    %bot.oldtarget = "";
                else
                    %bot.oldtarget = %bot.target;
                initContainerRadiusSearch(%bot.position, 125, $TypeMasks::PlayerObjectType);
                while(isobject(%search = containerSearchNext()))
                {
                    if(%bot.oldtarget == %search)
                        continue;
                    if(%search.getclassname() $= "aiplayer")
                        continue;
                    if(botWallCheck(%search.getHackPosition(), %bot))
                        continue;	
                    if(isObject(%search))
                        %bot.target = %search;
                    break;
                }
            }
            if(%bot.attacking == 1 && %bot.attacktime + 1.25 < $sim::time)
                %bot.attacking = 0;
            if(%bot.attacking == 2 && %bot.attacktime + 1.75 < $sim::time)
                %bot.attacking = 0;
            if(%bot.getmountedimage(0).getname() $= "diggerpickaxe3image" || %bot.getmountedimage(0).getname() $= "diggerpickaxe3phase2image")
            {
                %bot.setaimlocation(%bot.target.geteyepoint());		
                %bot.setmovey(0.5);
            }
            if(!%bot.attacking)
            {
                %bot.setaimlocation(%bot.target.geteyepoint());		
                %bot.setmovey(1);
                if(vectordist(%bot.target.gethackposition(), %bot.gethackposition()) > 5)
                    %bot.rangedattack++;
                if(%bot.rangedattack >= 12)
                {
                    %wep = getrandom(3,4);
                    if(!%bot.phase2)
                    {
                        %bot.mountimage(diggerpickaxe @ %wep @ image, 0);
                        %bot.schedule(1350, mountimage, diggerpickaxeimage, 0);
                    }
                    else
                    {
                        %bot.mountimage(diggerpickaxe @ %wep @ phase2image, 0);
                        %bot.schedule(1350, mountimage, diggerpickaxephase2image, 0);
                    }
                    %bot.setimagetrigger(0,1);
                    %bot.schedule(1350, setimagetrigger, 0,0);
                    %bot.attacking = 2;
                    %bot.attacktime = $sim::time;
                    %bot.rangedattack = 0;
                    if(%wep == 4)
                    {
                        %bot.setmovey(0);
                        %bot.setaimvector(vectornormalize(vectorsub(%bot.target.gethackposition(), %bot.gethackposition())));
                    }
                }
                if(vectordist(%bot.target.gethackposition(), %bot.gethackposition()) < 4.5)
                {
                    if(getrandom(1,5) == 1)
                        %bot.tooclose = 10;
                    if(%bot.tooclose >= getrandom(3,5) || %bot.firstattack >= getrandom(4,6))
                    {
                        %bot.firstattack = 0;
                        %bot.tooclose = 0;
                        %bot.attacking = 2;
                        if(!%bot.phase2)
                        {
                            %bot.mountimage(diggerpickaxe2image, 0);
                            %bot.schedule(1350, mountimage, diggerpickaxeimage, 0);
                        }
                        else
                        {
                            %bot.mountimage(diggerpickaxe2phase2image, 0);
                            %bot.schedule(1350, mountimage, diggerpickaxephase2image, 0);
                        }
                    }
                    else
                        %bot.attacking = 1;
                    %bot.rangedattack -= 5;
                    if(%bot.rangedattack < 0)
                        %bot.rangedattack = 0;
                    %bot.attacktime = $sim::time;
                    %bot.setimagetrigger(0,1);
                    %bot.setmovey(0);
                    %bot.lookattarget();
                    %bot.schedule(250, lookattarget);
                    %bot.schedule(500, lookattarget);
                    %bot.schedule(750, lookattarget);
                    %bot.schedule(1000, lookattarget);
                    %bot.schedule(500, setimagetrigger, 0,0);
                    %bot.firstattack++;
                    if(vectordist(getwords(%bot.target.gethackposition(),0,1), getwords(%bot.gethackposition(),0,1)) < 2)
                    {
                        %bot.tooclose++;
                        %bot.setmovey(-1);
                        %bot.schedule(300, setmovey, 0);
                    }
                }
            }
            %bot.thinkSchedule = schedule(250, %bot, BotThinkLoopDiggerBoss, %bot);
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

	%bot.thinkSchedule = schedule(250, %bot, BotThinkLoopDiggerBoss, %bot);
}


function BotThinkLoopDiggerBoss2(%bot)
{
    if(%bot.getstate() $= "dead")
        return;
    if(%bot.gethealth() / %bot.getmaxhealth() <= 0.5 && !%bot.phase2)
    {
        if(!%bot.attacking)
        {
            %bot.setmaxforwardspeed(7.5);
            %bot.isinvincible = 1;
            %bot.target = "";
            %bot.setmovey(0);
            %bot.setaimobject(0);
            %bot.stop();
            %bot.setmovedestination(_diggerbossspawn.position);
            %bot.thinkSchedule = schedule(250, %bot, BotThinkLoopDiggerBoss2, %bot);
            return;
        }
    }
    if(%bot.phase2 && !%bot.phase2complete)
    {
        if(%bot.phase2 && %bot.phase2time + 1.5 > $sim::time)
        {
            %bot.thinkSchedule = schedule(250, %bot, BotThinkLoopDiggerBoss2, %bot);
            return;
        }
        else if(%bot.phase2 && %bot.phase2time + 1.5 < $sim::time)
        {
            %bot.phase2complete = 1;
            %bot.isinvincible = 0;
            %bot.thinkSchedule = schedule(250, %bot, BotThinkLoopDiggerBoss2, %bot);
            return;
        }
    }
	if(isObject(%bot.target))
	{
		if(botWallCheck(%bot.target.getHackPosition(), %bot))
		{
			%bot.target = "";
			%bot.setMoveObject(0);
			%bot.stop();
			%bot.setAimObject(0);
			%bot.thinkSchedule = schedule(250, %bot, BotThinkLoopDiggerBoss2, %bot);
			return;
		}
		else
		{	
            %bot.targetchange++;
            initContainerRadiusSearch(%bot.position, 2, $TypeMasks::PlayerObjectType);
            while(isobject(%search = containerSearchNext()))
            {
                if(%bot.target == %search)
                    continue;
                if(%search.getclassname() $= "aiplayer")
                    continue;
                if(botWallCheck(%search.getHackPosition(), %bot))
                    continue;	
                %bot.targetchange += 0.25;
            }
            if(mabs(getword(%bot.getvelocity(),0)) + mabs(getword(%bot.getvelocity(),1)) < 4 && !%bot.attacking)
            {
                %bot.instatargetchange++;
                if(%bot.instatargetchange >= 10)
                    %bot.targetchange+=100;
            }
            else
                %bot.instatargetchange = 0;
            if(%bot.targetchange >= 100 && !%bot.attacking)
            {
                %bot.oldtarget = %bot.target;
                %bot.targetchange = 0;
                if(getrandom(1,3) == 1)
                    %bot.oldtarget = "";
                else
                    %bot.oldtarget = %bot.target;
                initContainerRadiusSearch(%bot.position, 125, $TypeMasks::PlayerObjectType);
                while(isobject(%search = containerSearchNext()))
                {
                    if(%bot.oldtarget == %search)
                        continue;
                    if(%search.getclassname() $= "aiplayer")
                        continue;
                    if(botWallCheck(%search.getHackPosition(), %bot))
                        continue;	
                    if(isObject(%search))
                        %bot.target = %search;
                    break;
                }
            }
            if(%bot.attacking == 1 && %bot.attacktime + 1.25 < $sim::time)
                %bot.attacking = 0;
            if(%bot.attacking == 2 && %bot.attacktime + 1.75 < $sim::time)
                %bot.attacking = 0;
            if(%bot.getmountedimage(0).getname() $= "diggerpickaxe3challengeimage" || %bot.getmountedimage(0).getname() $= "diggerpickaxe3challenge3hase2image")
            {
                %bot.setaimlocation(%bot.target.geteyepoint());		
                %bot.setmovey(0.5);
            }
            if(!%bot.attacking)
            {
                %bot.setaimlocation(%bot.target.geteyepoint());		
                %bot.setmovey(1);
                if(vectordist(%bot.target.gethackposition(), %bot.gethackposition()) > 5)
                    %bot.rangedattack++;
                if(%bot.rangedattack >= 12)
                {
                    %wep = getrandom(3,4);
                    %bot.attacking = 2;
                    if(%wep == 3)
                    {
                        if(!%bot.phase2)
                        {
                            %bot.mountimage(diggerpickaxe3challengeimage, 0);
                            %bot.attacktime = $sim::time - 0.5;
                            %bot.schedule(1850, setimagetrigger, 0,0);
                            %bot.schedule(1850, mountimage, diggerpickaxechallengeimage, 0);
                        }
                        else
                        {
                            %bot.mountimage(diggerpickaxe3challengephase2image, 0);
                            %bot.attacktime = $sim::time - 2;
                            %bot.schedule(3350, setimagetrigger, 0,0);
                            %bot.schedule(3350, mountimage, diggerpickaxechallengephase2image, 0);
                        }
                    }
                    else
                    {
                        if(!%bot.phase2)
                        {
                            %bot.mountimage(diggerpickaxe4challengeimage, 0);
                            %bot.schedule(1250, mountimage, diggerpickaxechallengeimage, 0);
                        }
                        else
                        {
                            %bot.mountimage(diggerpickaxe4challengephase2image, 0);
                            %bot.schedule(1250, mountimage, diggerpickaxechallengephase2image, 0);
                        }
                        %bot.attacktime = $sim::time;
                        %bot.schedule(1250, setimagetrigger, 0,0);
                        %bot.lookattarget();
                        %bot.schedule(250, lookattarget);
                        %bot.schedule(500, lookattarget);
                        %bot.schedule(750, lookattarget);
                        %bot.schedule(1000, lookattarget);
                    }
                    %bot.setimagetrigger(0,1);
                    %bot.rangedattack = 0;
                    if(%wep == 4)
                    {
                        %bot.setmovey(0);
                        %bot.setaimvector(vectornormalize(vectorsub(%bot.target.gethackposition(), %bot.gethackposition())));
                    }
                }
                if(vectordist(%bot.target.gethackposition(), %bot.gethackposition()) < 4.5)
                {
                    if(getrandom(1,5) == 1)
                        %bot.tooclose = 10;
                    if(%bot.tooclose >= getrandom(3,5) || %bot.firstattack >= getrandom(4,6))
                    {
                        %bot.firstattack = 0;
                        %bot.tooclose = 0;
                        %bot.attacking = 2;
                        if(!%bot.phase2)
                        {
                            %bot.mountimage(diggerpickaxe2challengeimage, 0);
                            %bot.schedule(1250, mountimage, diggerpickaxechallengeimage, 0);
                        }
                        else
                        {
                            %bot.mountimage(diggerpickaxe2challengephase2image, 0);
                            %bot.schedule(1250, mountimage, diggerpickaxechallengephase2image, 0);
                        }
                    }
                    else
                        %bot.attacking = 1;
                    %bot.rangedattack -= 5;
                    if(%bot.rangedattack < 0)
                        %bot.rangedattack = 0;
                    %bot.attacktime = $sim::time;
                    %bot.setimagetrigger(0,1);
                    %bot.setmovey(0);
                    %bot.lookattarget();
                    %bot.schedule(250, lookattarget);
                    %bot.schedule(500, lookattarget);
                    %bot.schedule(750, lookattarget);
                    %bot.schedule(1000, lookattarget);
                    %bot.schedule(500, setimagetrigger, 0,0);
                    %bot.firstattack++;
                    if(vectordist(getwords(%bot.target.gethackposition(),0,1), getwords(%bot.gethackposition(),0,1)) < 2)
                    {
                        %bot.tooclose++;
                        %bot.setmovey(-1);
                        %bot.schedule(300, setmovey, 0);
                    }
                }
            }
            %bot.thinkSchedule = schedule(250, %bot, BotThinkLoopDiggerBoss2, %bot);
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

	%bot.thinkSchedule = schedule(250, %bot, BotThinkLoopDiggerBoss2, %bot);
}