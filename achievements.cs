$achievementcount = 0;
function servercmdachievements(%client)
{
    if(!%client.achievementsunlocked)
        %client.achievementsunlocked = 0;
    for(%i = 0; %i < $achievementcount; %i++)
    {
        %name = getfield($achievement[%i],0);
        %description = getfield($achievement[%i],2);
        %rarity = getfield($achievement[%i],3);
        %rewards = getfields($achievement[%i],4,getfieldcount($achievement[%i])-1);
        if(%rarity == 0)
            %color = "<color:AFEEEE>";
        else if(%rarity == 1)
            %color = "<color:1E90FF>";
        else if(%rarity == 2)
            %color = "<color:FFD700>";
        else if(%rarity == 3)
            %color = "<color:FF4500>";
        else if(%rarity == 4)
            %color = "<color:FF0000>";
        else if(%rarity == 5)
            %color = "<color:600000>";
        else if(%rarity == 6)
            %color = "<color:FF00FF>";
        if(%client.achievementunlocked[%name])
        {
            %achievementlist = %color @ %name SPC "\c6-\c7" SPC %description SPC "\c6-\c4" SPC showRewards(%rewards);
            %client.chatmessage(%achievementlist);
        }
    }
    %client.chatmessage("\c6You have unlocked" SPC %client.achievementsunlocked @ "/" @ $achievementcount SPC "achievements.");
}

function servercmdachievement(%client)
{
    servercmdachievements(%client);
}

function addachievement(%achievement, %progress, %description, %rarity, %rewards)
{
    $achievement[$achievementcount] = %achievement TAB %progress TAB %description TAB %rarity TAB %rewards;
    $achievementcount++;
}

function gameconnection::unlockachievement(%client, %achievement)
{
    for(%i = 0; %i < $achievementcount; %i++)
    {
        if(getfield($achievement[%i],0) $= %achievement)
        {
            %name = getfield($achievement[%i],0);
            %description = getfield($achievement[%i],2);
            %rarity = getfield($achievement[%i],3);
            %rewards = getfields($achievement[%i],4,getfieldcount($achievement[%i])-1);
            break;
        }
    }

    if(%name $= "" || %client.achievementunlocked[%name])
        return;

    if(%rarity == 0)
        %color = "<color:AFEEEE>";
    else if(%rarity == 1)
        %color = "<color:1E90FF>";
    else if(%rarity == 2)
        %color = "<color:FFD700>";
    else if(%rarity == 3)
        %color = "<color:FF4500>";
    else if(%rarity == 4)
        %color = "<color:FF0000>";
    else if(%rarity == 5)
        %color = "<color:600000>";
    else if(%rarity == 6)
        %color = "<color:FF00FF>";

    for(%i = 0; %i < clientgroup.getcount(); %i++)
    {
        %clients = clientgroup.getobject(%i);
        if(%clients == %client)
        {
            %client.achievementsunlocked++;
            %client.achievementunlocked[%name] = 1;
            %client.chatmessage(%color @ %name SPC "\c6-\c7" SPC %description @ ". \c4Completed!");
            %client.chatmessage("\c6You have received:\c4" @ showRewards(%rewards));
            %client.receiveRewards(%rewards);
            %client.updateachievementsfile();
            %client.updateachievementstatsfile();
            if(%rarity == 0 || %rarity == 6)
                %client.playsound(achievement1);
        }
        else
        {
            if(%rarity != 0 && %rarity != 6)
            {
                %clients.chatmessage("\c4" @ %client.getplayername() SPC "\c6has completed the achievement" @ %color SPC %name @ "\c6!");
                if(%rarity == 1)
                    %sound = "achievement1";
                else if(%rarity >= 2 && %rarity < 4)
                    %sound = "achievement2";
                else if(%rarity <= 5)
                    %sound = "achievement3";
                %clients.playsound(%sound);
            }
        }
    }
    %client.playsound(%sound);
}

function gameconnection::removeachievement(%client, %achievement)
{
    for(%i = 0; %i < $achievementcount; %i++)
    {
        if(getfield($achievement[%i],0) $= %achievement)
        {
            %name = getfield($achievement[%i],0);
            %description = getfield($achievement[%i],2);
            %rarity = getfield($achievement[%i],3);
            %rewards = getfields($achievement[%i],4,getfieldcount($achievement[%i])-1);
            break;
        }
    }

    if(%name $= "" || !%client.achievementunlocked[%name])
        return;

    if(%rarity == 0)
        %color = "<color:AFEEEE>";
    else if(%rarity == 1)
        %color = "<color:1E90FF>";
    else if(%rarity == 2)
        %color = "<color:FFD700>";
    else if(%rarity == 3)
        %color = "<color:FF4500>";
    else if(%rarity == 4)
        %color = "<color:FF0000>";
    else if(%rarity == 5)
        %color = "<color:600000>";
    else if(%rarity == 6)
        %color = "<color:FF00FF>";

    %client.achievementsunlocked--;
    %client.achievementunlocked[%name] = 0;
    %client.updateachievementsfile();
    %client.chatmessage(%color @ %name SPC "\c6-\c7" SPC %description @ ". \c4Successfully removed!");
    %client.receiveRewards(%rewards, 1);
}

function servercmdviewachievements(%client)
{
    if(%client.isadmin)
    {
        for(%i = 0; %i < $achievementcount; %i++)
        {
            %rarity = getfield($achievement[%i],3);
            if(%rarity == 0)
                %color = "<color:AFEEEE>";
            else if(%rarity == 1)
                %color = "<color:1E90FF>";
            else if(%rarity == 2)
                %color = "<color:FFD700>";
            else if(%rarity == 3)
                %color = "<color:FF4500>";
            else if(%rarity == 4)
                %color = "<color:FF0000>";
            else if(%rarity == 5)
                %color = "<color:600000>";
            else if(%rarity == 6)
                %color = "<color:FF00FF>";
            %client.chatmessage(%color @ getfield($achievement[%i],0) SPC "\c6-\c7" SPC getfield($achievement[%i],2) SPC "\c6-\c4" @ showRewards(getfields($achievement[%i],4,getfieldcount($achievement[%i])-1)));
        }
        %client.chatmessage("\c4list of all achievements in here (theres" SPC $achievementcount SPC "of them)");
    }
    else
    {
        %client.chatmessage("admin only only :)");
    }
}

function showRewards(%reward)
{
    if(%reward $= "")
        return " no rewards";
    %fields = getfieldcount(%reward);
    for(%i = 0; %i < %fields; %i++)
    {
        if(firstword(getfield(%reward,%i)) $= "miningpower")
            %rewards = %rewards SPC "MiningPower +" @ restwords(getfield(%reward,%i));
        else if(firstword(getfield(%reward,%i)) $= "miningmultiplier")
            %rewards = %rewards SPC "MiningMultiplier +" @ restwords(getfield(%reward,%i)) @ "%%";
        else if(firstword(getfield(%reward,%i)) $= "expbonus")
            %rewards = %rewards SPC "ExpBonus +" @ restwords(getfield(%reward,%i)) @ "%%";
        else if(firstword(getfield(%reward,%i)) $= "cashbonus")
            %rewards = %rewards SPC "CashBonus +" @ restwords(getfield(%reward,%i)) @ "%%";
        else if(firstword(getfield(%reward,%i)) $= "cratedrops")
            %rewards = %rewards SPC "CrateDrops +" @ restwords(getfield(%reward,%i)) @ "%%";
        else
            %rewards = %rewards SPC "no rewards";
    }
    return %rewards;
}

function gameconnection::receiveRewards(%client, %reward, %reverse)
{
    if(%reward $= "")
        return;
    if(%reverse)
        %reverse = "-1";
    else
        %reverse = "1";
    %fields = getfieldcount(%reward);
    for(%i = 0; %i < %fields; %i++)
    {
        if(firstword(getfield(%reward,%i)) $= "miningpower")
            %client.achievementMiningPower += restwords(getfield(%reward,%i)) * %reverse;
        else if(firstword(getfield(%reward,%i)) $= "miningmultiplier")
            %client.achievementMiningMultiplier += restwords(getfield(%reward,%i))/100 * %reverse;
        else if(firstword(getfield(%reward,%i)) $= "expbonus")
            %client.achievementExpBonus += restwords(getfield(%reward,%i))/100 * %reverse;
        else if(firstword(getfield(%reward,%i)) $= "cashbonus")
            %client.achievementCashBonus += restwords(getfield(%reward,%i))/100 * %reverse;
        else if(firstword(getfield(%reward,%i)) $= "cratedrops")
            %client.achievementCrateDrops += restwords(getfield(%reward,%i))/100 * %reverse;
    }
}

addachievement("test", 0, "literally who", 6, "");
// -- ore related --
addachievement("humble digger", 100, "mine 100 blocks", 0, "miningpower 1");
addachievement("beginner digger", 1000, "mine 1000 blocks", 0, "miningpower 1");
addachievement("certified digger", 2500, "mine 2500 blocks", 1, "miningpower 2");
addachievement("renowned digger", 10000, "mine 10000 blocks", 2, "miningpower 3");
addachievement("digception", 50000, "mine 50000 blocks", 2, "miningpower 5");
addachievement("digger boss", 100000, "mine 100000 blocks", 3, "miningpower 7");
addachievement("earth dismantler", 250000, "mine 250000 blocks", 4, "miningpower 10");
addachievement("destroyer of worlds", 1000000, "mine 1000000 blocks", 5, "miningpower 15");
addachievement("first steps", 1000, "inflict 1000 damage to blocks", 0, "miningpower 1");
addachievement("a lot of damage", 10000, "inflict 10000 damage to blocks", 0, "miningpower 1");
addachievement("even more damage", 100000, "inflict 100000 damage to blocks", 1, "miningpower 2");
addachievement("excavator", 1000000, "inflict 1 million damage to blocks", 1, "miningpower 3");
addachievement("caves decimator", 50000000, "inflict 50 million damage to blocks", 2, "miningpower 5");
addachievement("ores for breakfast", 250000000, "inflict 250 million damage to blocks", 3, "miningpower 7");
addachievement("ultimate destruction", 1000000000, "inflict 1 billion damage to blocks", 4, "miningpower 10");
addachievement("you can either be big digger or dig bigger", 1000000000000, "inflict 1 trillion damage to blocks", 5, "miningpower 15");
// -- level --
addachievement("LEVEL milestone - 1", 1, "reach level 1", 0, "miningmultiplier 1\ncashbonus 1\nexpbonus 1");
addachievement("LEVEL milestone - 3", 3, "reach level 3", 0, "miningmultiplier 2\ncashbonus 2\nexpbonus 2");
addachievement("LEVEL milestone - 5", 5, "reach level 5", 1, "miningmultiplier 3\ncashbonus 3\nexpbonus 3");
addachievement("LEVEL milestone - 10", 10, "reach level 10", 1, "miningmultiplier 4\ncashbonus 4\nexpbonus 4");
addachievement("LEVEL milestone - 15", 15, "reach level 15", 1, "miningmultiplier 5\ncashbonus 5\nexpbonus 5");
addachievement("LEVEL milestone - 20", 20, "reach level 20", 2, "miningmultiplier 10\ncashbonus 10\nexpbonus 10");
addachievement("LEVEL milestone - 25", 25, "reach level 25", 2, "miningmultiplier 5\ncashbonus 5\nexpbonus 5");
addachievement("LEVEL milestone - 30", 30, "reach level 30", 3, "miningmultiplier 4\ncashbonus 4\nexpbonus 4");
addachievement("LEVEL milestone - 35", 35, "reach level 35", 3, "miningmultiplier 3\ncashbonus 3\nexpbonus 3");
addachievement("LEVEL milestone - 40", 40, "reach level 40", 4, "miningmultiplier 2\ncashbonus 2\nexpbonus 2");
addachievement("LEVEL milestone - 45", 40, "reach level 45", 4, "miningmultiplier 2\ncashbonus 2\nexpbonus 2");
addachievement("LEVEL milestone - 50", 40, "reach level 50", 5, "miningmultiplier 1\ncashbonus 1\nexpbonus 1");
// -- depth --
addachievement("touch grass", -100, "reach the grass layer", 6, "miningpower 1");
addachievement("new sights", 500, "mine your first ore at 500m", 0, "miningpower 1\ncashbonus 1\nexpbonus 1");
addachievement("going deeper", 1000, "mine your first ore at 1000m", 1, "miningpower 2\ncashbonus 2\nexpbonus 2");
addachievement("rock and stone", 1500, "mine your first ore at 1500m", 1, "miningpower 3\ncashbonus 3\nexpbonus 3");
addachievement("going even deeper", 2250, "mine your first ore at 2250m", 1, "miningpower 4\ncashbonus 5\nexpbonus 5");
addachievement("it's getting hotter", 3000, "mine your first ore at 3000m", 2, "miningpower 5\ncashbonus 8\nexpbonus 8");
addachievement("literally minecraft", 4000, "mine your first ore at 4000m", 2, "miningpower 7\ncashbonus 10\nexpbonus 10");
addachievement("the deep is calling", 5000, "mine your first ore at 5000m", 3, "miningpower 10\ncashbonus 15\nexpbonus 15");
// -- money --
addachievement("we're rich", 100, "accumulate 100 cash in total", 0, "cashbonus 1");
addachievement("middle class", 500, "accumulate 500 cash in total", 0, "cashbonus 1");
addachievement("money hungry diggers", 2500, "accumulate 2500 cash in total", 0, "cashbonus 1");
addachievement("it grows in dirt", 10000, "accumulate 10000 cash in total", 1, "cashbonus 2");
addachievement("wealthy digger", 100000, "accumulate 100000 cash in total", 1, "cashbonus 3");
addachievement("millionaire", 1000000, "accumulate 1 million cash in total", 2, "cashbonus 5");
addachievement("digging inflation", 250000000, "accumulate 250 million cash in total", 3, "cashbonus 7");
addachievement("jeff digos", 1000000000, "accumulate 1 billion cash in total", 4, "cashbonus 10");
addachievement("i own the universe", 1000000000000, "accumulate 1 trillion cash in total", 5, "cashbonus 20");
// -- prestiges --
addachievement("all over again", 1, "prestige once", 2, "");
addachievement("demand more content", 2, "prestige twice", 2, "");
addachievement("never enough", 3, "prestige thrice", 2, "");
addachievement("persistence is key", 5, "prestige 5 times", 3, "");
addachievement("elite digger", 10, "prestige 10 times", 3, "");
addachievement("master digger", 25, "prestige 25 times, are you ok", 4, "");
addachievement("unstoppable digger", 50, "prestige 50 times, you should stop", 4, "");
addachievement("holy digger", 100, "prestige 100 times, are you satisfied enough yet", 5, "");
// -- digger boss --
addachievement("all your ores are belong to us", 0, "defeat the digger boss", 2, "miningpower 5\nminingmultiplier 1");
addachievement("digger's invocation", 0, "defeat the challenged variant of the digger boss", 4, "miningpower 10\nminingmultiplier 3");
// -- crates / vaults --
addachievement("first impressions", 0, "open your first crate", 0, "cratedrops 1");
addachievement("get scammed or get rich", 0, "open a total of 10 crates", 1, "cratedrops 2");
addachievement("i love rng", 0, "open a total of 50 crates", 2, "cratedrops 3");
addachievement("name me the god of diggers", 0, "open a total of 100 crates", 2, "cratedrops 4");
addachievement("crate infestation", 250, "open a total of 250 crates", 2, "cratedrops 5");
addachievement("unsuccessful entropy", 500, "open a total of 500 crates", 3, "cratedrops 10");
addachievement("99%% of diggers quit before they hit big", 1000, "open a total of 1000 crates", 4, "cratedrops 15");
addachievement("breaking bank", 0, "crack open your first crate vault", 1, "cratedrops 2");
addachievement("successful heist", 0, "crack open a total of 10 crate vaults", 1, "cratedrops 3");
addachievement("the day of payment", 0, "crack open a total of 50 crate vaults", 2, "cratedrops 5");
addachievement("the greatest risk is not taking one", 0, "crack open a total of 200 crate vaults", 3, "cratedrops 10");
addachievement("pole vaulting at its finest", 0, "crack open a total of 500 crate vaults", 4, "cratedrops 15");
addachievement("lucky digger", 0, "roll a maximum amount of crates possible from any crate vault", 2, "cratedrops 3");
addachievement("time manipulated", 0, "have a quantum or atomic disruption spawns any tier crates", 3, "cratedrops 5");
addachievement("is this even legal", 0, "have a quantum or atomic disruption spawns any tier crate vaults", 3, "cratedrops 5");
// -- miscellaneous
addachievement("seeking help", 0, "interact with every npc", 0, "expbonus 1");
addachievement("shiny ore", 0, "encounter a very rare ore", 2, "expbonus 7");
addachievement("cheat the system", 0, "spawn with level 1", 1, "expbonus 3");
addachievement("kitted out", 0, "craft all available tools", 4, "miningmultiplier 3");
addachievement("professional geologist", 0, "have one of every dirt piece", 2, "miningpower 2\nexpbonus 3");
addachievement("get real", 0, "obtain reality pickaxe", 4, "miningpower 4");