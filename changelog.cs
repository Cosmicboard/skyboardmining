$serverVersion = 15;

function servercmdchangelog(%client, %ver)
{
    if(%ver $= "")
    {
        %client.chatmessage("\c4list of all updates sorted by dates");
        %client.chatmessage("\c3minor update 1 - v1.0 - 24.09.2023");
        %client.chatmessage("\c3update 2 - v1.01 - 25.09.2023");
        %client.chatmessage("\c3the cool update 3 - v1.02 - 26.09.2023");
        %client.chatmessage("\c3the halloween update 4 - v1.03 -  01.10.2023");
        %client.chatmessage("\c3update 5 - v1.035 - 05.10.2023");
        %client.chatmessage("\c3update 6 - v1.04 - 21.10.2023");
        %client.chatmessage("\c3boss update 7 - v1.05 - 01.11.2023");
        %client.chatmessage("\c3minor update 8 - v1.051 - 13.11.2023");
        %client.chatmessage("\c3big update 9 - v1.06 - 18.11.2023");
        %client.chatmessage("\c3minor update 10 - v1.061 - 22.11.2023");
        %client.chatmessage("\c3the christmoth update 11 - v1.07 - 01.12.2023");
        %client.chatmessage("\c3minor update 12 - v1.071 - 07.12.2023");
        %client.chatmessage("\c3rebalances 13 - v1.072 - 08.12.2023");
        %client.chatmessage("\c3minor update 14 - v1.073 - 23.01.2024");
        %client.chatmessage("\c3the updatening part I (vID: 15) - v1.08 - 14.02.2024");
        %client.chatmessage("\c5type \c0/changelog [update number] \c5or \c0/changelog latest \c5to view changes that are specific to that update");
    }
    else if(%ver == 1)
    {
        %client.chatmessage("\c524.09.2023 - minor update");
        %client.chatmessage("\c3--- new additions ---");
        %client.chatmessage("\c6added the /changelog command to keep track of updates");
        %client.chatmessage("\c3--- changes ---");
        %client.chatmessage("\c6crafted tools are now deleted upon being dropped to prevent from people picking them up");
        %client.chatmessage("\c3--- bug fixes ---");
        %client.chatmessage("\c6fixed /sell and /inv commands not supporting all types of ores");
    }
    else if(%ver == 2)
    {
        %client.chatmessage("\c525.09.2023 - update");
        %client.chatmessage("\c3--- new additions ---");
        %client.chatmessage("\c6added value to dirt and the rest dirt-like ores");
        %client.chatmessage("\c6zombies will despawn if not provoked 150 seconds after being spawned");
        %client.chatmessage("\c3--- changes ---");
        %client.chatmessage("\c6improved /inventory and /sell commands to support all types of ores");
        %client.chatmessage("\c3--- bug fixes ---");
        %client.chatmessage("\c6fixed people being able to dig tunnels straight into the prestige shop");
        %client.chatmessage("\c6fixed (currently known) all ways of escaping the map");
        %client.chatmessage("\c6zombies spawn with a 1 second delay when a cave is being generated to prevent them from falling through the cave mid generation");
    }
    else if(%ver == 3)
    {
        %client.chatmessage("\c526.09.2023 - the cool update");
        %client.chatmessage("\c3--- new additions ---");
        %client.chatmessage("\c1--- QUANTUM DISRUPTIONS ---");
        %client.chatmessage("\c2new type of rng ore that starts to generate below 3500m");
        %client.chatmessage("\c2upon being destroyed, it will turn all nearby blocks into a random ore with an exception being this ore itself");
        %client.chatmessage("\c6filled up the abdundance of new tier crates at the 1500m layer");
        %client.chatmessage("\c6trying to mine an ore that's above the level requirement will still show up the ore info");
        %client.chatmessage("\c3--- changes ---");
        %client.chatmessage("\c6made the changelogs less cramped and seperated them into different versions");
        %client.chatmessage("\c3--- bug fixes ---");
        %client.chatmessage("\c6fixed the geolocator not having an info brick next to it");
    }
    else if(%ver == 4)
    {
        %client.chatmessage("\c501.10.2023 - the halloween update");
        %client.chatmessage("<color:FF4500>--- HALLOWEEN EVENT ---");
        %client.chatmessage("<color:FFA500>featuring halloween spawn and new song for overworld");
        %client.chatmessage("<color:FFA500>added exclusive ores that will last for the entiriety of this event (they do not reset on prestiging)");
        %client.chatmessage("<color:FFA500>added exclusive cosmetics (they do not reset on prestiging)");
        %client.chatmessage("<color:FFA500>added a new npc that will give out the halloween stuff");
        %client.chatmessage("<color:FFA500>added renderman enemies to the caves");
        %client.chatmessage("<color:FFA500>added him");
        %client.chatmessage("\c3--- new additions ---");
        %client.chatmessage("\c1--- LASER DRILL ---");
        %client.chatmessage("\c2a new tool that acts like the tunneler but is very different from its premise");
        %client.chatmessage("\c2it will start to ramp up mining power and speed depending on for how long you were drilling");
        %client.chatmessage("\c1--- TNT LAUNCHER ---");
        %client.chatmessage("\c2allows you to carry up to 10 dynamites in this bad boy");
        %client.chatmessage("\c2acts exactly the same as normal dynamite but you just stuff it into this cannon");
        %client.chatmessage("\c6added a /buyexp command that allows you to buy exp (5 cash to 1 exp ratio)");
        %client.chatmessage("\c6added a craft recipes support to the /info command");
        %client.chatmessage("\c6added creeper enemies");
        %client.chatmessage("\c6added total stats tracking for a few things that will be done in the future");
        %client.chatmessage("\c6added a button that allows you to respec prestige points");
        %client.chatmessage("\c6added 2 more dynamite tiers");
        %client.chatmessage("\c3--- changes ---");
        %client.chatmessage("\c6changed the prestige shop design");
        %client.chatmessage("\c6increased the bricklimit to 690k bricks");
        %client.chatmessage("\c6changelog announcement has been changed into a chatmessage rather a messagebox upon joining the server for the first time after an update dropped");
        %client.chatmessage("\c6capped max zombie damage at 10 per hit");
        %client.chatmessage("\c6nerfed the throw range of dynamite");
        %client.chatmessage("\c3--- bug fixes ---");
        %client.chatmessage("\c6fixed quantum disruptions being able to spawn ores from any depth");
        %client.chatmessage("\c6fixed being able to escape the map using the ceiling");
        %client.chatmessage("\c6fixed being able to escape the map using the floor (don't question me how does this work i have no idea myself)");
        %client.chatmessage("\c6fixed incorrect dynamite's explosion behaviour");
        %client.chatmessage("\c6fixed incorrect number scaling on the tunneler prestige upgrade");
    }
    else if(%ver == 5)
    {
        %client.chatmessage("\c505.10.2023 - update");
        %client.chatmessage("\c3--- new additions ---");
        %client.chatmessage("\c6added a few more ''useful'' tips to the tipbot");
        %client.chatmessage("\c3--- changes ---");
        %client.chatmessage("\c6changed gold to exp ratio to 3/1 for the /buyexp command");
        %client.chatmessage("\c6changed the way how distance is calculated for the laser drill");
        %client.chatmessage("\c6changed exp and money values for event ores for balance purposes");
        %client.chatmessage("\c6cryogenum lanterns are now replaced with cryogenum soul lanterns to match their light color");
        %client.chatmessage("\c6increased mining power loss when going below optimal depth");
        %client.chatmessage("\c6increased debuff resistance for the tunneler to 33%%");
        %client.chatmessage("\c6nerfed creepers explosion radius");
        %client.chatmessage("\c3--- bug fixes ---");
        %client.chatmessage("\c6fixed pickaxes being able to deal damage through walls");
        %client.chatmessage("\c6fixed tunneler's cooldown starting only when you unequip it");
        %client.chatmessage("\c6fixed lanterns being incorrectly positioned towards the ground causing their bottom part to be inside the bricks");
    }
    else if(%ver == 6)
    {
        %client.chatmessage("\c521.10.2023 - update");
        %client.chatmessage("\c3--- new additions ---");
        %client.chatmessage("\c6added 2 more halloween skins");
        %client.chatmessage("\c6added 8 new halloween ores");
        %client.chatmessage("\c6added 4 new ores");
        %client.chatmessage("\c6added 2 new prestige upgrades");
        %client.chatmessage("\c3--- changes ---");
        %client.chatmessage("\c6allowed tier-1 crates to drop ores from 0-500m depths");
        %client.chatmessage("\c6allowed crates to drop event ores");
        %client.chatmessage("\c6changed gold to exp ratio to 1/1 for the /buyexp command");
        %client.chatmessage("\c6increased prestige point gain on higher levels");
        %client.chatmessage("\c6rewrote the way how ore inventory saving is handled");
        %client.chatmessage("\c3--- bug fixes ---");
        %client.chatmessage("\c6fixed creepers exploding if you are 100 feet above them");
        %client.chatmessage("\c6fixed being able to upgrade decreased tunneler lava damage beyond 4.5%%");
    }
    else if(%ver == 7)
    {
        %client.chatmessage("\c501.11.2023 - boss update");
        %client.chatmessage("\c3--- new additions ---");
        %client.chatmessage("\c1--- DIGGER BOSS ---");
        %client.chatmessage("\c2very first boss to be introduced to this game");
        %client.chatmessage("\c2a new type of ore will start to spawn below 1000m very rarely and upon mining will give you out a key");
        %client.chatmessage("\c2said key can be used at a magic door that materialized out of nowhere somewhere on spawn");
        %client.chatmessage("\c2upon activating it, a 30 second countdown will start before teleporting everyone nearby, starting the boss fight");
        %client.chatmessage("\c6added a /help command");
        %client.chatmessage("\c6added /buyexp [all] support");
        %client.chatmessage("\c6added a few more ''useful'' tips to the tipbot");
        %client.chatmessage("\c6added 1 new EVENT ore");
        %client.chatmessage("\c6implemented item saving so you don't have to requip items when dying or leaving the server");
        %client.chatmessage("\c3--- changes ---");
        %client.chatmessage("\c6changed tier-6 treasure chest price: 50000 -> 75000");
        %client.chatmessage("\c6changed tier-7 treasure chest price: 250000 -> 375000");
        %client.chatmessage("\c6decreased health values for almost all 3000m-4000m ores by 15%%");
        %client.chatmessage("\c6decreased health values for almost all 4000m-5000m ores by 20%%");
        %client.chatmessage("\c6decreased enemy hit range on pickaxes");
        %client.chatmessage("\c6updates are now seperated by their number and version");
        %client.chatmessage("\c6/sell [all] no longer sells EVENT ores");
        %client.chatmessage("\c3--- bug fixes ---");
        %client.chatmessage("\c6fixed tunneler cooldown behaving incorrectly");
        %client.chatmessage("\c6fixed /stats command not showing the new upgrades");
        %client.chatmessage("\c6fixed mining helmet and cryogenum tank unequipping themselves when changing datablocks");
    }
    else if(%ver == 8)
    {
        %client.chatmessage("\c513.11.2023 - minor update");
        %client.chatmessage("\c3--- new additions ---");
        %client.chatmessage("\c6added pickaxe sounds to the digger boss");
        %client.chatmessage("\c3--- changes ---");
        %client.chatmessage("\c6halloween event is gone");
        %client.chatmessage("\c6increased exp and gold drop scaling for higher levels after defeating the digger boss");
        %client.chatmessage("\c6made the digger boss slightly more aggressive");
        %client.chatmessage("\c3--- bug fixes ---");
        %client.chatmessage("\c6fixed animations not showing on the fire pickaxe for digger boss");
        %client.chatmessage("\c6fixed inventory save file corruption if you were leave the server while loading into the server");
    }
    else if(%ver == 9)
    {
        %client.chatmessage("\c518.11.2023 - big update");
        %client.chatmessage("\c3--- new additions ---");
        %client.chatmessage("\c1--- 6250M + INFINITE ---");
        %client.chatmessage("\c2mining threshold has been increased to 6250m with 30 new ores");
        %client.chatmessage("\c2you can now dig BELOW the mining threshold, but you will not find anything new");
        %client.chatmessage("\c2however, enemies will remain intact and receive their depth scalings, meaning they will drop more stuff and also have more health");
        %client.chatmessage("\c2optimal depth does not affect your character by entering the infinite area");
        %client.chatmessage("\c1--- FLAK VEST ---");
        %client.chatmessage("\c2featuring a new armor that can be crafted in the shop");
        %client.chatmessage("\c2provides a 40%% explosion damage resistance from any explosions");
        %client.chatmessage("\c6added 4 new ores");
        %client.chatmessage("\c6added 1 new torch");
        %client.chatmessage("\c6added a /leaderboard to this game");
        %client.chatmessage("\c6added a /lock [ore] command to blacklist ores from being sold by typing /sell all");
        %client.chatmessage("\c6added icons to mining helmet and cryogenum tank");
        %client.chatmessage("\c6added new bugs");
        %client.chatmessage("\c3--- changes ---");
        %client.chatmessage("\c6changed tier-7 expium health: 7500000 > 8250000");
        %client.chatmessage("\c6changed tier-7 exp value: 350000 > 325000");
        %client.chatmessage("\c6changed the way how spawn npcs work");
        %client.chatmessage("\c6decreased exp scaling and added exp curves to higher levels");
        %client.chatmessage("\c6increased exp and money drop scaling from enemies by a lot");
        %client.chatmessage("\c6rewrote the way how pickaxes saving is handled");
        %client.chatmessage("\c6quantum disruption and forbidden key's health value scales from depth on top of base health");
        %client.chatmessage("\c3--- bug fixes ---");
        %client.chatmessage("\c6fixed lava blocks breaking multiplie times");
        %client.chatmessage("\c6fixed equipped cosmetics saving incorrectly");
        %client.chatmessage("\c6fixed digger boss' fire pickaxe not glowing sometimes");
    }
    else if(%ver == 10)
    {
        %client.chatmessage("\c522.11.2023 - minor update");
        %client.chatmessage("\c3--- changes ---");
        %client.chatmessage("\c6made the laser drill less effective if you're below your optimal depth");
        %client.chatmessage("\c3--- bug fixes ---");
        %client.chatmessage("\c6fixed bricks being broken twice when you mine them");
        %client.chatmessage("\c6fixed being able to dig infinitely up");
        %client.chatmessage("\c6fixed digger boss arena");
        %client.chatmessage("\c6fixed one type of ore never generating");
    }
    else if(%ver == 11)
    {
        %client.chatmessage("\c501.12.2023 - the chrismoth update");
        %client.chatmessage("\c4--- CHRISTMAS EVENT ---");
        %client.chatmessage("\c4featuring christmas spawn and new song for overworld (feels like i've heard this before)");
        %client.chatmessage("\c4added new event ores");
        %client.chatmessage("\c4added new event cosmetics");
        %client.chatmessage("\c4added a new npc that will give out the christmas stuff");
        %client.chatmessage("\c4added a cool minigame!!!");
        %client.chatmessage("\c3--- new additions ---");
        %client.chatmessage("\c1--- ORE TRADING ---");
        %client.chatmessage("\c2/trade [name] allows you to trade ores with people");
        %client.chatmessage("\c2the receiver must meet the ore's level requirement to get it");
        %client.chatmessage("\c2base tax fee will be 25%% HOWEVER if the receiver has bonus cash prestige upgrades then that amount will be increased");
        %client.chatmessage("\c1--- CHALLENGER'S KEY ---");
        %client.chatmessage("\c2a new type of forbidden key that spawns VERY rare below 2250m");
        %client.chatmessage("\c2functions exactly the same as the forbidden key but spawns a harder version of the digger boss with increased rewards");
        %client.chatmessage("\c1--- GEOLOCATOR MKII ---");
        %client.chatmessage("\c2a new variant of the geolocator with more drip");
        %client.chatmessage("\c2allows to look for 2 ores at once and have their exact position printed out");
        %client.chatmessage("\c6added /listlock and /unlockall commands");
        %client.chatmessage("\c6added extra save backups in case your both saves get corrupted somehow");
        %client.chatmessage("\c6added rewards (25%%) for those people who died in a boss fight but the boss was defeated");
        %client.chatmessage("\c6added a few more ''useful'' tips to the tipbot");
        %client.chatmessage("\c3--- changes ---");
        %client.chatmessage("\c6changed flak vest's explosion resistance: 40%% > 50%%");
        %client.chatmessage("\c6changed reality pickaxe's swingrate: 0.125s > 0.12s");
        %client.chatmessage("\c6changed optimal depth to be less punishing when you go below only 1m but scale way more");
        %client.chatmessage("\c6dynamite now deals [250 * tier] damage to enemies");
        %client.chatmessage("\c6event ores no longer receive crate bonus upgrade bonuses if rolled from a crate");
        %client.chatmessage("\c6now to sell ores, you need to meet the ore's level requirement");
        %client.chatmessage("\c3--- bug fixes ---");
        %client.chatmessage("\c6fixed being able to escape the map from the basement");
        %client.chatmessage("\c6fixed creepers not exploding blocks below 5000m");
        %client.chatmessage("\c6fixed digger boss taking damage from lava");
        %client.chatmessage("\c6fixed digger boss not becoming invincible during his phase 1 to phase 2 transition");
        %client.chatmessage("\c6fixed digger boss being able to break lower tiles");
        %client.chatmessage("\c6fixed mining helmet and cryogenum tank not being dismounted from players if the item is dropped");
        %client.chatmessage("\c6fixed starter depth prestige upgrade upgrading base mining power instead");
        %client.chatmessage("\c6fixed the new 3 pickaxes not being erased from your inventory upon prestiging");
        %client.chatmessage("\c6fixed the placement tool not prompting error messages if you do not have enough materials to place a torch");
        %client.chatmessage("\c6fixed incorrect prestige points calculation in the /leaderboard");
    }
    else if(%ver == 12)
    {
        %client.chatmessage("\c507.12.2023 - minor update");
        %client.chatmessage("\c3--- new additions ---");
        %client.chatmessage("\c6added support for 20 player slots");
        %client.chatmessage("\c3--- changes ---");
        %client.chatmessage("\c6reworked health system for enemies");
        %client.chatmessage("\c3--- bug fixes ---");
        %client.chatmessage("\c6fixed laser drill playing playermount sound upon equipping");
        %client.chatmessage("\c6fixed presents not having their drops scaled from depth");
        %client.chatmessage("\c6fixed quantum disruptions being able to generate ore inside of bricks");
        %client.chatmessage("\c6fixed quantum disruptions being able to spawn key fragments");
        %client.chatmessage("\c6fixed zombies being able to drop any type of ores");
    }
    else if(%ver == 13)
    {
        %client.chatmessage("\c508.12.2023 - rebalances");
        %client.chatmessage("\c3--- changes ---");
        %client.chatmessage("\c6capped prestige points multiplier at 1000%%");
        %client.chatmessage("\c6participating in a digger boss fight will now award you with only 20%% rewards");
        %client.chatmessage("\c6reduced scaling for rare ore drops from crates");
        %client.chatmessage("\c6reduced ore drops from crates if you have bonus cash upgrades");
        %client.chatmessage("\c6reduced drops from digger boss for higher levels (50+)");
        %client.chatmessage("\c6reduced health scaling for digger boss from people with higher levels (50+)");
        %client.chatmessage("\c6reduced the time it takes enemies to spawn during boss waves in the event gamemode");
        %client.chatmessage("\c6slightly reduced ore drop scaling for tier-6 crates and above");
        %client.chatmessage("\c6greatly reduced prestige points scaling for higher levels (35+)");
        %client.chatmessage("\c6void gems can now spawn below 6250m");
        %client.chatmessage("\c3--- bug fixes ---");
        %client.chatmessage("\c6fixed save backups not saving items");
        %client.chatmessage("\c6fixed your character not being respawned when entering the event");
    }
    else if(%ver == 14)
    {
        %client.chatmessage("\c523.01.2024 - minor update");
        %client.chatmessage("\c3--- new additions ---");
        %client.chatmessage("\c6added 1 new ore");
        %client.chatmessage("\c3--- changes ---");
        %client.chatmessage("\c6you immediately unlock the new skin if you open the event door because my lazy ass did not host this for like a month");
        %client.chatmessage("\c6depth indicator can now go into negative values");
        %client.chatmessage("\c3--- bug fixes ---");
        %client.chatmessage("\c6fixed health bars sometimes not showing");
    }
    else if(%ver == 15 || %ver $= "latest")
    {
        %client.chatmessage("\c514.02.2024 - the updatening part I");
        %client.chatmessage("\c3--- new additions ---");
        %client.chatmessage("\c0reverted everyone's prestige upgrades back into prestige points because all of the upgrades had their prices changed");
        %client.chatmessage("\c0the leaderboard was also reset because it doesn't update in real time :3 (nice coding)");
        %client.chatmessage("\c1--- ACHIEVEMENTS ---");
        %client.chatmessage("\c2achievements now exist to encourage more brain activity");
        %client.chatmessage("\c2they will grant permament buffs depending on how rare the achievement is");
        %client.chatmessage("\c6added more tips");
        %client.chatmessage("\c6ores can now sometimes generate in veins");
        %client.chatmessage("\c3--- changes ---");
        %client.chatmessage("\c6winter event is gone");
        %client.chatmessage("\c6changed voidstone's texture so you can actually see a damn thing in the void layer");
        %client.chatmessage("\c6capped max starting level prestige upgrade at 5");
        %client.chatmessage("\c6reduced health scaling for challenged digger boss");
        %client.chatmessage("\c6rebalanced tunneler to be more viable at lower levels");
        %client.chatmessage("\c6increased ore requirements for lower tier pickaxes");
        %client.chatmessage("\c6improved pickaxe shop house model");
        %client.chatmessage("\c6/buyexp price now increases with the amount of prestige cash bonuses");
        %client.chatmessage("\c6caves will generate slower at higher player counts to reduce server lag");
        %client.chatmessage("\c6reworked torch placement code to be more stable");
        %client.chatmessage("\c6removed value from dirt type ores");
        %client.chatmessage("\c6remodelled snowglobe");
        %client.chatmessage("\c3--- bug fixes ---");
        %client.chatmessage("\c6fixed incorrect leaderboard prestige points scaling (for real this time holy shit)");
        %client.chatmessage("\c6fixed slightly incorrect upgrades to prestige points conversion");
        %client.chatmessage("\c6fixed shopkeeper's dialogue");
        %client.chatmessage("\c6fixed server crashing if someone has too many levels when fighting the digger boss");
        %client.chatmessage("\c6fixed challenged digger boss not spawning with his pickaxe");
        %client.chatmessage("\c6fixed being able to trade decimal amount of ores");
        %client.chatmessage("\c6fixed /declinetrade not prompting you with a message");
        %client.chatmessage("\c6fixed trades being timeout'd even after declining them");
        %client.chatmessage("\c6fixed equipped cosmetics not saving");
        %client.chatmessage("\c6fixed real bedrock being unbreakable");
    }
    else if(%ver $= "secret")
    {
        %client.chatmessage("\c520.02.2024 - major update");
        %client.chatmessage("\c1--- THE BLACKSMITH ---");
        %client.chatmessage("\c2introducing a whole new building to the spawn location called the blacksmith");
        %client.chatmessage("\c2adds a WHOLE new variety to the crafting options and new crafted materials make use of the inventory");
        %client.chatmessage("\c2including a furnace, allowing to smelt ores into alloys for more advanced crafting recipes");
        %client.chatmessage("\c2comes with an extractinator that allows you to put dirt type ores into it and receive ores from it");
        //%client.chatmessage("\c2and don't forget ore transmutation, which allows you to transform lower tier ores into higher");
        %client.chatmessage("\c1--- COBALT CANNON ---");
        %client.chatmessage("\c2OMG FIRST GUN IN SKYBOARDMINING IT SHOOTS LASERS!!!! (comes with 2 more variants)");
        %client.chatmessage("\c2as mentioned above, it works as a ranged weapon, but has another purpose for ionizing lower tier ores");
        %client.chatmessage("\c2ionizing ores will make them worth more for the price of having more health (probably will be also used for crafting)");
        %client.chatmessage("\c1--- GRAND DESIGN ---");
        %client.chatmessage("\c2the long awaited building tool in skyboardmining that allows you to place blocks");
        %client.chatmessage("\c2you can switch between specific blocks you want to place (i'm not adding dirt upgrades to make them stronger)");
        %client.chatmessage("\c1--- DRILLS ---");
        %client.chatmessage("\c2the grand design allows you to construct bigger drills to do the job for you!!!");
        %client.chatmessage("\c2differences being, they use up fuel in order to keep running so they can drill by themselves");
    }
} 