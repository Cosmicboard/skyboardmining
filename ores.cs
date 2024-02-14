$oreCount = 0;
$craftCount = 0;
$depthPurchaseCount = 0;
$inventoryPurchaseCount = 0;

function registerInventoryPurchase(%name, %gold, %mats)
{
    $inventoryPurchase[$inventoryPurchaseCount] = %name TAB %gold TAB %mats;
    $inventoryPurchaseCount++;
}

function registerDepthPurchase(%name, %gold, %mats)
{
    $depthPurchase[$depthPurchaseCount] = %name TAB %gold TAB %mats;
    $depthPurchaseCount++;
}

function registerCraft(%name, %level, %gold, %mats, %item, %cosmetictype)
{
    $craft[$craftCount] = %name TAB %level TAB %gold TAB %mats TAB %item TAB %cosmetictype;
    $craftCount++;
}

function registerOre(%name, %health, %money, %exp, %levelreq, %color, %colorfx, %shapefx, %printid, %rarity, %minspawn, %maxspawn, %special, %indestructible)
{
    if(%name $= "")
        return;
    $ore[$oreCount] = %name TAB %health TAB %money TAB %exp TAB %levelreq TAB %color TAB %colorfx TAB %shapefx TAB %printid TAB %rarity TAB %minspawn TAB %maxspawn TAB %special TAB %indestructible; 
    $oreCount++;
}

$maximumDepth = 6250;

//-----  0   -----    1   -----   2   -----  3  -----     4     -----   5   -----    6    -----    7    -----    8    -----   9    -----     10    -----    11     -----   12    -----       13       -----
//----- name ----- health ----- money ----- exp ----- level req ----- color ----- colorfx ----- shapefx ----- printid ----- rarity ----- min spawn ----- max spawn ----- special ----- indestructible -----
registerOre("Grass", "15", "0", "0", "0", "27", "0", "0", "modter/grass", "0", "-100", "0", "IGNORE");
registerOre("Dirt", "30", "0", "1", "0", "1", "0", "0", "modter/sand03", "0", "-100", "200", "IGNORE");
registerOre("Dense Dirt", "125", "0", "2", "0", "2", "0", "0", "modter/sand03", "0", "200", "500", "IGNORE");
registerOre("Stone", "750", "0", "4", "3", "4", "0", "0", "modter/rock", "0", "500", "1000", "IGNORE");
registerOre("Dense Stone", "2500", "0", "6", "7", "5", "0", "0", "modter/rock", "0", "1000", "1500", "IGNORE");
registerOre("Bedrock", "7500", "0", "10", "8", "16", "0", "0", "modter/old_stone_road", "0", "1500", "2250", "IGNORE");
registerOre("Mantle", "20000", "0", "15", "13", "7", "0", "0", "modter/ground", "0", "2250", "3000", "IGNORE");
registerOre("Core", "50000", "0", "25", "18", "8", "0", "0", "modter/ground", "0", "3000", "4000", "IGNORE");
registerOre("Netherrack", "100000", "0", "40", "24", "10", "0", "0", "modter/ground", "0", "4000", "5000", "IGNORE");
registerOre("Voidstone", "250000", "0", "100", "35", "63", "0", "0", "modter/brickramp", "0", "5000", "6250", "IGNORE");
registerOre("Coal", "150", "5", "8", "0", "16", "0", "0", "modter/sand03", "30", "-100", "500");
registerOre("Ionized Coal", "2500", "75", "120", "0", "16", "1", "0", "modter/sand03", "0", "-100", "500", "IGNORE");
registerOre("Tin", "200", "6", "12", "0", "11", "0", "0", "modter/sand03", "27", "-75", "500");
registerOre("Ionized Tin", "4000", "150", "225", "0", "11", "1", "0", "modter/sand03", "0", "-75", "500", "IGNORE");
registerOre("Iron", "240", "8", "15", "0", "12", "0", "0", "modter/sand03", "25", "-50", "500");
registerOre("Ionized Iron", "6250", "275", "425", "0", "12", "1", "0", "modter/sand03", "0", "-50", "500", "IGNORE");
registerOre("Copper", "325", "10", "20", "0", "15", "1", "0", "modter/sand03", "20", "0", "500");
registerOre("Ionized Copper", "9250", "400", "600", "0", "15", "2", "0", "modter/sand03", "0", "0", "500", "IGNORE");
registerOre("Silver", "350", "14", "35", "0", "13", "1", "0", "modter/rockface", "17", "35", "1000");
registerOre("Ionized Silver", "10250", "425", "665", "0", "13", "2", "0", "modter/rockface", "0", "35", "1000", "IGNORE");
registerOre("Gold", "575", "20", "45", "0", "18", "0", "0", "modter/rockface", "17", "80", "1000");
registerOre("Ionized Gold", "14500", "620", "755", "0", "18", "1", "0", "modter/rockface", "0", "80", "1000", "IGNORE");
registerOre("Zinc", "300", "15", "30", "0", "20", "0", "0", "modter/sand03", "13", "130", "500");
registerOre("Ionized Zinc", "8500", "375", "520", "0", "20", "1", "0", "modter/sand03", "0", "130", "500", "IGNORE");
registerOre("Aluminium", "1200", "40", "85", "2", "22", "0", "0", "modter/rockface", "10", "130", "1250");
registerOre("Ionized Aluminium", "20000", "650", "1250", "2", "22", "1", "0", "modter/rockface", "0", "130", "1250", "IGNORE");
registerOre("Antimony", "650", "25", "50", "2", "30", "0", "0", "modter/gravel_bed", "16", "165", "750");
registerOre("Quartz", "700", "28", "56", "3", "21", "0", "0", "modter/chiseled_ice", "16", "250", "500");
registerOre("Ionized Quartz", "16250", "700", "1500", "3", "21", "1", "0", "modter/chiseled_ice", "0", "250", "500", "IGNORE");
registerOre("Amber", "1500", "70", "165", "3", "48", "0", "0", "modter/chiseled_ice", "11", "250", "750");
registerOre("Graphite", "750", "32", "64", "3", "17", "0", "0", "modter/chiseled_ice", "14", "325", "500");
registerOre("Lithium", "1000", "42", "90", "3", "22", "0", "0", "modter/ground", "12", "325", "500");
registerOre("Ionized Lithium", "25000", "700", "1425", "3", "22", "1", "0", "modter/ground", "0", "325", "500", "IGNORE");
registerOre("Cobalt", "1250", "50", "125", "4", "25", "0", "0", "modter/rock", "9", "415", "1000");
registerOre("Ionized Cobalt", "32500", "825", "1800", "4", "25", "1", "0", "modter/rock", "0", "415", "1000", "IGNORE");
registerOre("Compressed Ore Deposit", "0", "0", "0", "5", "0", "0", "0", "modter/rockface", "16", "500", "2250", "COMPRESSEDORE");
registerOre("Lava", "5", "0", "0", "0", "38", "3", "0", "modter/lava5", "0", "500", "5000", "LAVA");
registerOre("Amethyst", "40000", "750", "1500", "5", "61", "0", "0", "modter/chiseled_ice", "2", "510", "2250");
registerOre("Ruby", "3000", "90", "180", "5", "23", "1", "0", "modter/rockface", "10", "510", "1250");
registerOre("Nickel", "3500", "100", "195", "5", "37", "1", "0", "modter/snow", "11", "525", "1250");
registerOre("Gallium", "3500", "100", "195", "5", "12", "0", "0", "modter/port_of_taganrog", "11", "550", "1000");
registerOre("Sapphire", "3750", "105", "225", "5", "46", "0", "0", "modter/rockface", "13", "600", "1250");
registerOre("Palladium", "5000", "130", "275", "6", "32", "0", "0", "modter/rockface", "11", "625", "1500");
registerOre("Platinum", "4500", "115", "250", "6", "13", "1", "0", "modter/rockface", "12", "650", "1500");
registerOre("Topaz", "7500", "185", "400", "6", "19", "3", "0", "modter/port_of_taganrog", "6", "650", "1750");
registerOre("Opal", "8500", "190", "425", "6", "60", "1", "0", "modter/port_of_taganrog", "7", "700", "1250");
registerOre("Emerald", "9500", "200", "450", "7", "42", "0", "0", "modter/rockface", "8", "750", "2000");
registerOre("Chromium", "25000", "500", "1000", "7", "43", "0", "0", "modter/ttdirt01", "3", "800", "2250");
registerOre("Lapis Lazuli", "8000", "225", "500", "7", "45", "0", "0", "modter/lava5", "8", "825", "1750");
registerOre("Limestone", "8000", "210", "475", "7", "30", "1", "0", "modter/old_stone_road", "7", "900", "1750");
registerOre("Jade", "9000", "215", "485", "7", "42", "1", "0", "modter/chiseled_ice", "7", "950", "1750");
registerOre("Lead", "10000", "225", "515", "8", "36", "0", "0", "modter/rockface", "11", "1000", "1500");
registerOre("Orichalcum", "11000", "235", "575", "8", "40", "0", "0", "modter/rockface", "11", "1050", "2250");
registerOre("Manganese", "11500", "245", "595", "8", "12", "0", "0", "modter/chiseled_ice", "9", "1100", "1750");
registerOre("Mithril", "12500", "265", "625", "8", "27", "0", "0", "modter/rockface", "11", "1150", "2250");
registerOre("Diamond", "12500", "365", "925", "8", "53", "3", "0", "modter/chiseled_ice", "7", "1200", "2250");
registerOre("schmungus", "25000", "625", "1750", "8", "20", "3", "0", "modter/schmungus", "1", "1250", "2250");
registerOre("Tungsten", "15000", "265", "625", "9", "22", "0", "0", "modter/ground", "11", "1300", "2250");
registerOre("Adamantite", "20000", "340", "750", "10", "29", "0", "0", "modter/rockface", "9", "1350", "2250");
registerOre("Titanium", "21250", "350", "775", "10", "5", "5", "0", "modter/rockface", "9", "1400", "2250");
registerOre("Beamonemol", "22500", "375", "825", "10", "48", "0", "0", "modter/chiseled_ice", "9", "1450", "2250");
registerOre("Lonsdaleite", "25000", "400", "950", "11", "55", "3", "0", "modter/chiseled_ice", "5", "1500", "2250");
registerOre("Bismuth", "40000", "725", "1600", "11", "47", "0", "0", "modter/rockface", "7", "1600", "2500");
registerOre("Osmium", "43000", "765", "1685", "11", "37", "0", "0", "modter/sand03", "9", "1650", "2500");
registerOre("Magnesium", "42500", "350", "800", "11", "22", "3", "0", "modter/port_of_taganrog", "12", "1700", "2750");
registerOre("Vanadium", "43000", "765", "1685", "11", "39", "5", "0", "modter/rockface", "9", "1700", "2750");
registerOre("Iridium", "45000", "780", "1725", "11", "30", "3", "0", "modter/water3", "7", "1750", "2750");
registerOre("Uranium", "47500", "800", "1950", "11", "42", "3", "0", "modter/water3", "6", "1800", "2750");
registerOre("Rhenium", "55000", "885", "2050", "12", "41", "2", "0", "modter/ground", "7", "1850", "3000");
registerOre("Thorium", "60000", "885", "2050", "13", "17", "0", "0", "modter/sand2", "4", "1950", "3000");
registerOre("Plutonium", "70000", "970", "2160", "13", "26", "0", "0", "modter/sand2", "4", "2050", "3000");
registerOre("Skyboardium", "99999", "1750", "3750", "13", "40", "1", "1", "modter/sand03", "2", "2100", "3000");
registerOre("Cernoburith", "112500", "1200", "2750", "14", "45", "5", "0", "modter/sand03", "4", "2150", "3000");
registerOre("LEVEL", "300000", "2500", "7000", "16", "18", "0", "0", "modter/level", "1", "2275", "3275");
registerOre("Nanite", "125000", "1300", "3000", "16", "45", "0", "0", "modter/pixelated", "9", "2300", "3750");
registerOre("Rainbonite", "150000", "1450", "3500", "16", "52", "6", "0", "modter/sand03", "9", "2300", "3750");
registerOre("Beryllium", "175000", "1600", "3850", "16", "5", "0", "0", "modter/rockface", "9", "2400", "3750");
registerOre("Luminite", "220000", "1900", "4600", "17", "27", "2", "0", "modter/grass", "8", "2450", "4000");
registerOre("Crystallium", "225000", "1500", "4750", "17", "45", "1", "0", "modter/sand03", "6", "2500", "4000");
registerOre("Astral Silver", "230000", "2000", "4900", "17", "25", "3", "0", "modter/rockface", "6", "2550", "4000");
registerOre("Neodymium", "240000", "1850", "4500", "17", "14", "0", "0", "modter/snow", "8", "2500", "4000");
registerOre("Molybdenum", "250000", "1950", "4750", "17", "23", "0", "0", "modter/rockface", "7", "2600", "4000");
registerOre("Niobium", "200000", "1750", "4250", "17", "3", "0", "0", "modter/rock", "8", "2650", "4000");
registerOre("Baryte", "275000", "2300", "5250", "17", "54", "3", "0", "modter/snow", "6", "2650", "4000");
registerOre("G_", "500000", "3500", "9000", "18", "46", "0", "0", "modter/g_", "1", "2700", "4000");
registerOre("Moonstone", "5000000", "22500", "67500", "18", "59", "3", "0", "modter/chiseled_ice", "1", "2750", "4500");
registerOre("Aurium", "300000", "2400", "5500", "19", "41", "3", "0", "modter/chiseled_ice", "6", "2800", "4250");
registerOre("Lanite", "325000", "2600", "5700", "19", "44", "0", "0", "modter/rockface", "6", "2900", "4250");
registerOre("Serdenitium", "350000", "2850", "6000", "19", "34", "0", "0", "modter/rockface", "6", "2950", "4250");
registerOre("Cryogenum", "400000", "2250", "9000", "20", "53", "3", "0", "modter/snow4", "12", "3050", "5000");
registerOre("Dragonstone", "680000", "5500", "14000", "21", "29", "0", "0", "modter/rockface", "8", "3100", "5250");
registerOre("Firecrystal", "765000", "5750", "14750", "21", "48", "1", "0", "modter/chiseled_ice", "8", "3150", "5250");
registerOre("Corium", "828750", "6000", "15000", "21", "15", "0", "0", "modter/dirt2", "8", "3200", "5250");
registerOre("SVINTUS PRIDET", "1275000", "10000", "25000", "21", "40", "0", "0", "modter/cvintus", "2", "3250", "5250");
registerOre("Astralium", "935000", "6200", "17000", "21", "47", "5", "0", "modter/snow", "7", "3300", "5250");
registerOre("Crystallite", "977500", "6300", "18250", "21", "54", "1", "0", "modter/chiseled_ice", "7", "3350", "5250");
registerOre("Quranitium", "1062500", "6500", "18500", "22", "26", "2", "0", "modter/chiseled_ice", "7", "3350", "5250");
registerOre("Shadowstone", "1126250", "6750", "19000", "22", "33", "0", "0", "modter/rockface", "7", "3400", "5250");
registerOre("Solarium", "1168750", "7100", "19750", "23", "41", "2", "0", "modter/port_of_taganrog", "7", "3450", "5250");
registerOre("Symmetrium", "1211250", "7250", "20000", "23", "44", "0", "0", "modter/ground", "7", "3525", "5250");
registerOre("Trinomium", "1360000", "7500", "20500", "23", "35", "1", "0", "modter/rockface", "7", "3600", "5250");
registerOre("Bloodstone", "1402500", "7750", "21000", "24", "38", "0", "0", "modter/old_stone_road", "7", "3650", "5250");
registerOre("Painite", "1466250", "8000", "21750", "24", "29", "5", "0", "modter/sand03", "6", "3700", "5250");
registerOre("Sphalerite", "1487500", "8250", "22000", "24", "22", "0", "0", "modter/ttdirt01", "6", "3750", "5250");
registerOre("Uelibloom", "1530000", "8500", "23500", "25", "30", "1", "0", "modter/rockface", "6", "3800", "5250");
registerOre("Dedicatum", "1572500", "9000", "24000", "25", "28", "1", "0", "modter/sand03", "6", "3850", "5250");
registerOre("Promethium", "1636250", "9750", "24500", "25", "27", "0", "0", "modter/rockface", "6", "3900", "5250");
registerOre("Xeoron", "1657500", "10500", "25500", "25", "61", "0", "0", "modter/sand03", "6", "3950", "5250");
registerOre("Naquadah", "3400000", "19500", "49500", "26", "25", "0", "0", "modter/snow", "8", "4050", "6000");
registerOre("Copernicium", "3525000", "20500", "50250", "26", "50", "1", "0", "modter/chiseled_ice", "8", "4100", "6000");
registerOre("Yunium", "3650000", "21750", "51750", "26", "49", "3", "0", "modter/water3", "8", "4150", "6000");
registerOre("Frightstone", "3725000", "22100", "52500", "26", "17", "3", "0", "modter/rockface", "8", "4200", "6000");
registerOre("Brimstone", "3800000", "22500", "53250", "27", "38", "4", "0", "modter/ground", "7", "4250", "6000");
registerOre("Netherite", "3875000", "23000", "54000", "27", "8", "1", "0", "modter/dirt2", "7", "4300", "6000");
registerOre("Sunstone", "10000000", "75000", "175000", "27", "10", "4", "0", "modter/rockface", "1", "4350", "6000");
registerOre("Dumortierite", "4000000", "25000", "55000", "27", "45", "3", "0", "modter/port_of_taganrog", "7", "4400", "6000");
registerOre("Cosmilite", "4125000", "25750", "56750", "28", "31", "5", "0", "modter/port_of_taganrog", "7", "4450", "6000");
registerOre("Hellite", "4200000", "26200", "57250", "28", "29", "3", "0", "modter/pixelated", "7", "4500", "6250");
registerOre("Lightstone", "2500000", "17500", "37500", "28", "41", "5", "0", "modter/ground", "11", "4500", "6000");
registerOre("KRATOS MESSI", "20000000", "125000", "350000", "28", "23", "0", "0", "modter/kratosmessi", "1", "4550", "6250");
registerOre("Occultatum", "4285000", "26500", "58000", "28", "14", "1", "0", "modter/rockface", "6", "4600", "6250");
registerOre("Astatine", "4385000", "27250", "60750", "28", "47", "1", "0", "modter/ground", "6", "4650", "6250");
registerOre("Constellatium", "4500000", "28000", "61500", "28", "25", "3", "0", "modter/sand03", "6", "4700", "6250");
registerOre("Circinite", "4625000", "28250", "62750", "29", "57", "3", "0", "modter/chiseled_ice", "6", "4750", "6250");
registerOre("Auric Tesla", "4800000", "28500", "63500", "29", "19", "0", "0", "modter/ground", "5", "4800", "6250");
registerOre("Hexaferrum", "4925000", "29250", "64250", "29", "22", "1", "0", "modter/rockface", "5", "4850", "6250");
registerOre("Cretorium", "5000000", "30250", "65000", "29", "34", "0", "0", "modter/rock", "5", "4900", "6250");
registerOre("Quadromium", "5125000", "31000", "72500", "30", "40", "2", "0", "modter/snow", "5", "4950", "6250");
registerOre("Black Hole", "5", "0", "0", "0", "63", "3", "0", "modter/lava5", "0", "5000", "6250", "LAVA");
registerOre("Onyx", "12500000", "75000", "175000", "35", "47", "0", "0", "modter/port_of_taganrog", "8", "5050", "7500");
registerOre("Praxium", "13250000", "77350", "179500", "35", "35", "1", "0", "modter/snow", "8", "5100", "7500");
registerOre("Stellarite", "13700000", "79200", "182500", "36", "46", "3", "0", "modter/rockface", "8", "5150", "7500");
registerOre("Illuminite", "14150000", "82500", "187850", "36", "43", "3", "0", "modter/ttdirt01", "7", "5200", "7500");
registerOre("Ignatius", "14650000", "85650", "192500", "36", "38", "0", "0", "modter/rock", "7", "5250", "7500");
registerOre("Eximite", "14650000", "88500", "197000", "36", "25", "0", "0", "modter/ground", "7", "5300", "7500");
registerOre("Meultor", "14650000", "91250", "205000", "37", "46", "0", "0", "modter/chiseled_ice", "7", "5350", "7500");
registerOre("Verisite", "14650000", "94000", "210000", "37", "42", "0", "0", "modter/gravel_bed", "7", "5400", "7500");
registerOre("Antimatter", "100000000", "350000", "1000000", "37", "26", "3", "0", "letters/-space", "1", "5450", "7500");
registerOre("Sylvanite", "16500000", "98500", "216000", "38", "12", "3", "0", "modter/water3", "7", "5500", "7500");
registerOre("Ulexite", "16850000", "101250", "222500", "28", "59", "0", "0", "modter/port_of_taganrog", "6", "5550", "7500");
registerOre("Cryolite", "17237500", "103500", "227750", "38", "53", "3", "0", "modter/chiseled_ice", "6", "5600", "7500");
registerOre("Linarite", "17750000", "106000", "231500", "39", "45", "1", "0", "modter/ground", "6", "5650", "7500");
registerOre("Aegistone", "18250000", "109250", "245000", "39", "19", "0", "0", "modter/chiseled_ice", "6", "5700", "7500");
registerOre("Cosmicboardium", "25000000", "150000", "227750", "40", "41", "1", "1", "modter/sand03", "2", "5750", "7500");
registerOre("Elementium", "18250000", "109250", "245000", "40", "23", "6", "0", "modter/old_stone_road", "5", "5800", "7500");
registerOre("Acceleratium", "18725000", "112500", "247500", "40", "40", "5", "0", "modter/grass", "5", "5850", "7500");
registerOre("Darkmatter", "150000000", "600000", "1750000", "40", "63", "3", "0", "modter/brickramp", "1", "5900", "7500");
registerOre("Gemstone Of Reality", "19500000", "119500", "254250", "41", "59", "0", "1", "modter/snow", "5", "5950", "7500");
registerOre("Quintollium", "20000000", "122500", "257000", "41", "6", "0", "0", "modter/rockface", "5", "6000", "7500");
registerOre("Scandellium", "20400000", "125750", "260500", "41", "24", "0", "0", "modter/whitesand", "4", "6050", "7500");
registerOre("Durescalite", "20850000", "128000", "265250", "42", "33", "0", "0", "modter/ground", "4", "6050", "7500");
registerOre("Spristium", "21250000", "130000", "268500", "42", "54", "0", "0", "modter/chiseled_ice", "4", "6100", "7500");
registerOre("Illuminyx", "21750000", "133000", "271500", "42", "60", "0", "0", "modter/water3", "4", "6150", "7500");
registerOre("Cerlustrium", "22250000", "135000", "275000", "42", "62", "0", "0", "modter/ground", "4", "6200", "7500");
registerOre("Void Gem", "1000000000", "3333333", "10000000", "45", "56", "3", "1", "letters/-space", "EXOTIC", "6250", "UNLIMITED");
registerOre("Tier-1 EXPium", "1000", "0", "150", "1", "41", "2", "0", "modter/sand03", "3", "0", "500", "NOORE");
registerOre("Tier-2 EXPium", "10000", "0", "1500", "4", "42", "2", "0", "modter/rockface", "3", "500", "1000", "NOORE");
registerOre("Tier-3 EXPium", "35000", "0", "5000", "8", "43", "2", "0", "modter/rockface", "4", "1000", "1500", "NOORE");
registerOre("Tier-4 EXPium", "100000", "0", "10000", "8", "44", "2", "0", "modter/rockface", "3", "1500", "2250", "NOORE");
registerOre("Tier-5 EXPium", "250000", "0", "22500", "12", "45", "2", "0", "modter/rockface", "3", "2250", "3000", "NOORE");
registerOre("Tier-6 EXPium", "1500000", "0", "82500", "21", "46", "2", "0", "modter/rockface", "3", "3000", "4000", "NOORE");
registerOre("Tier-7 EXPium", "8250000", "0", "325000", "26", "47", "2", "0", "modter/rockface", "3", "4000", "5000", "NOORE");
registerOre("Tier-8 EXPium", "62500000", "0", "2250000", "36", "48", "2", "0", "modter/rockface", "3", "5000", "6250", "NOORE");
registerOre("Tier-1 Treasure Chest", "1250", "125", "0", "1", "1", "0", "0", "modter/chest", "2", "0", "500", "TREASURECHEST");
registerOre("Tier-2 Treasure Chest", "12500", "1250", "0", "4", "1", "0", "0", "modter/chest", "2", "500", "1000", "TREASURECHEST");
registerOre("Tier-3 Treasure Chest", "50000", "3750", "0", "8", "1", "0", "0", "modter/chest", "3", "1000", "1500", "TREASURECHEST");
registerOre("Tier-4 Treasure Chest", "200000", "7500", "0", "8", "1", "0", "0", "modter/chest", "2", "2000", "2250", "TREASURECHEST");
registerOre("Tier-5 Treasure Chest", "500000", "13750", "0", "12", "1", "0", "0", "modter/chest", "2", "2250", "3000", "TREASURECHEST");
registerOre("Tier-6 Treasure Chest", "3000000", "75000", "0", "21", "1", "0", "0", "modter/chest", "2", "3000", "4000", "TREASURECHEST");
registerOre("Tier-7 Treasure Chest", "15000000", "375000", "0", "26", "1", "0", "0", "modter/chest", "2", "4000", "5000", "TREASURECHEST");
registerOre("Tier-8 Treasure Chest", "75000000", "1500000", "0", "36", "1", "0", "0", "modter/chest", "2", "5000", "6250", "TREASURECHEST");
registerOre("Tier-1 Crate", "2500", "0", "100", "0", "2", "0", "0", "modter/crate", "1", "0", "500", "CRATE");
registerOre("Tier-2 Crate", "25000", "0", "500", "4", "2", "0", "0", "modter/crate", "1", "500", "1000", "CRATE");
registerOre("Tier-3 Crate", "105000", "0", "1500", "8", "2", "0", "0", "modter/crate", "2", "1000", "1500", "CRATE");
registerOre("Tier-4 Crate", "300000", "0", "2750", "10", "2", "0", "0", "modter/crate", "1", "1500", "2250", "CRATE");
registerOre("Tier-5 Crate", "750000", "0", "5000", "12", "2", "0", "0", "modter/crate", "1", "2250", "3000", "CRATE");
registerOre("Tier-6 Crate", "4500000", "0", "12500", "21", "2", "0", "0", "modter/crate", "1", "3000", "4000", "CRATE");
registerOre("Tier-7 Crate", "22500000", "0", "50000", "26", "2", "0", "0", "modter/crate", "1", "4000", "5000", "CRATE");
registerOre("Tier-8 Crate", "125000000", "0", "225000", "36", "2", "0", "0", "modter/crate", "1", "5000", "6250", "CRATE");
registerOre("Tier-1 Crate Vault", "1250000", "0", "7500", "15", "12", "0", "0", "modter/vault", "1", "2250", "3000", "CRATEVAULT");
registerOre("Tier-2 Crate Vault", "7500000", "0", "20000", "21", "12", "0", "0", "modter/vault", "1", "3000", "4000", "CRATEVAULT");
registerOre("Tier-3 Crate Vault", "37500000", "0", "75000", "26", "12", "0", "0", "modter/vault", "1", "4000", "5000", "CRATEVAULT");
registerOre("Tier-4 Crate Vault", "400000000", "0", "375000", "36", "12", "0", "0", "modter/vault", "1", "5000", "6250", "CRATEVAULT");
registerOre("Quantum Disruption", "20000000", "0", "100000", "24", "22", "4", "1", "modter/rockface", "1", "3500", "5000", "QUANTUMDISRUPTION");
registerOre("Atomic Disruption", "200000000", "0", "1000000", "36", "42", "4", "1", "modter/rockface", "1", "5000", "6250", "QUANTUMDISRUPTION");
registerOre("Candy Corn", "2500", "125", "275", "2", "19", "0", "0", "modter/gravel_bed", "15", "0", "0", "EVENT");// "0", "500", "EVENT");
registerOre("Pumpkinium", "10000", "350", "850", "5", "14", "0", "0", "modter/grass", "7", "0", "0", "EVENT");// "500", "1250", "EVENT");
registerOre("Bone Block", "17500", "425", "975", "6", "22", "0", "0", "modter/ground", "6", "0", "0", "EVENT");// "750", "1500", "EVENT");
registerOre("BERRIED DELIGHT", "25000", "750", "2750", "8", "40", "0", "0", "modter/ground", "5", "0", "0", "EVENT");// "1000", "1750", "EVENT");
registerOre("Cold Lava", "75000", "1125", "3500", "9", "19", "3", "0", "modter/lava5", "5", "0", "0", "EVENT");// "1250", "2250", "EVENT");
registerOre("Enchanted Amethyst", "200000", "6000", "14250", "10", "36", "3", "0", "modter/chiseled_ice", "2", "0", "0", "EVENT");//" 1500", "2500", "EVENT");
registerOre(":whqskeleton:", "125000", "2500", "6666", "11", "22", "0", "0", "modter/whqskeleton", "3", "0", "0", "EVENT");// "1750", "2750", "EVENT");
registerOre("Soulstone", "200000", "4000", "10000", "13", "59", "3", "1", "modter/chiseled_ice", "3", "0", "0", "EVENT");// "2000", "3250", "EVENT");
registerOre("Bloodsteel", "350000", "6000", "15000", "17", "38", "0", "0", "modter/port_of_taganrog", "2", "0", "0", "EVENT");//"2250", "3250", "EVENT");
registerOre("Galaxite", "650000", "10000", "27500", "18", "23", "6", "0", "modter/ttdirt01", "2", "0", "0", "EVENT");// "2500", "3500", "EVENT");
registerOre("Cursed Pumpkinium", "750000", "8250", "22500", "19", "35", "3", "0", "modter/grass", "5", "0", "0", "EVENT");// "2750", "4000", "EVENT");
registerOre("Ectoplasm", "2250000", "17500", "45000", "20", "53", "0", "0", "modter/ground", "1", "0", "0", "EVENT");// "3000", "4000", "EVENT");
registerOre("Probemonium", "3000000", "20000", "50000", "21", "42", "0", "0", "modter/rockface", "1", "0", "0", "EVENT");// "3250", "4500", "EVENT");
registerOre("Cake", "3250", "150", "350", "3", "40", "0", "0", "modter/cake", "5", "0", "0", "EVENT");// "0", "500", "EVENT");
registerOre("Forbidden Key", "1000000", "0", "0", "15", "15", "0", "0", "modter/key", "LEGENDARY", "1000", "UNLIMITED", "KEY");
registerOre("Challenger's Key", "10000000", "0", "0", "25", "15", "0", "0", "modter/redkey", "MYTHIC", "2250", "UNLIMITED", "KEY");
registerOre("Key Fragment", "0", "0", "0", "0", "46", "0", "0", "letters/-space", "0", "0", "0", "");
registerOre("Present", "10000", "75", "150", "2", "45", "0", "0", "modter/bricktop", "6", "0", "0", "PRESENT");// "0", "1000", "PRESENT");
registerOre("Candy Cane", "2250", "100", "225", "2", "38", "0", "0", "modter/gravel_bed", "15", "0", "0", "EVENT");// "0", "500", "EVENT");
registerOre("Gingerbread", "8250", "300", "725", "5", "1", "0", "0", "modter/rock", "7", "0", "0", "EVENT");// "500", "1250", "EVENT");
registerOre("Blue Topaz", "15000", "400", "950", "6", "46", "3", "0", "modter/port_of_taganrog", "6", "0", "0", "EVENT");// "750", "1500", "EVENT");
registerOre("Solidified Snow", "32500", "750", "2000", "8", "26", "0", "0", "modter/sand03", "6", "0", "0", "EVENT");// "1000", "1750", "EVENT");
registerOre("Frostarium", "82500", "1250", "3750", "9", "54", "0", "0", "modter/chiseled_ice", "5", "0", "0", "EVENT");// "1250", "2250", "EVENT");
registerOre("Glacial Amethyst", "200000", "6000", "14750", "10", "57", "1", "0", "modter/chiseled_ice", "2", "0", "0", "EVENT");// "1500", "2500", "EVENT");
registerOre("Coldfirium", "112500", "2250", "5750", "11", "25", "1", "0", "modter/ground", "5", "0", "0", "EVENT");// "1750", "2750", "EVENT");
registerOre("VSTAVAY BALBES", "250000", "4500", "11500", "13", "7", "0", "0", "modter/balbes", "4", "0", "0", "EVENT");// "2000", "3000", "EVENT");
registerOre("Bluesteel", "350000", "6000", "15000", "17", "45", "0", "0", "modter/rockface", "4", "0", "0", "EVENT");// "2250", "3250", "EVENT");
registerOre("Hyperstone", "725000", "11250", "25000", "18", "12", "4", "0", "modter/rock", "3", "0", "0", "EVENT");// "2500", "3500", "EVENT");
registerOre("Snowglobe", "1750000", "16250", "42500", "19", "59", "5", "0", "letters/-space", "3", "0", "0", "EVENT");// "2750", "4000", "EVENT");
registerOre("Frexarite", "2500000", "18250", "48250", "20", "38", "1", "0", "modter/rockface", "3", "0", "0", "EVENT");// "3000", "4250", "EVENT");
registerOre("Delectium", "3250000", "21250", "52500", "21", "60", "0", "0", "modter/sand03", "2", "0", "0", "EVENT");// "3250", "4500", "EVENT");
registerOre("Elexinite", "4500000", "30000", "75000", "23", "47", "0", "0", "modter/port_of_taganrog", "2", "0", "0", "EVENT");// "3500", "4750", "EVENT");
registerOre("CHRISMOTH SPIRIT", "10000000", "100000", "300000", "27", "26", "4", "2", "letters/-space", "1", "0", "0", "EVENT");// "4000", "5500", "EVENT");
registerOre("REAL BEDROCK", "1", "0", "0", "0", "17", "0", "0", "modter/bricktop", "0", "0", "0", "IGNORE", "2");
registerOre("Steel", "2500", "0", "0", "0", "6", "0", "0", "modter/rockface", "0", "0", "0", "MATERIAL");
//registerOre("Lovely Crate", "10000", "0", "0", "0", "0", "0", "0", "modter/lovelycrate", "INSANE", "0", "5000", "LOVELYCRATE");

// enderium shadowspec

function exportOres()
{
    %fw = new FileObject();
    %fw.openForWrite("config/server/oreList.txt");
    for(%i = 0; %i < $orecount; %i++)
    {
        %fw.writeline(getfield($ore[%i],0));
    }
    %fw.close();
    %fw.delete();
}

registerCraft("Iron Pickaxe", "2", "250", "12 Iron\n10 Coal");
registerCraft("Gold Pickaxe", "3", "750", "15 Gold\n20 Copper\n25 Coal");
registerCraft("Quartz Pickaxe", "4", "2250", "12 Quartz\n8 Aluminium");
registerCraft("Cobalt Pickaxe", "5", "4500", "12 Cobalt\n10 Graphite");
registerCraft("Palladium Pickaxe", "6", "7500", "15 Palladium\n5 Sapphire\n5 Ruby");
registerCraft("Emerald Pickaxe", "7", "12500", "15 Emerald\n25 Coal");
registerCraft("Diamond Pickaxe", "8", "25000", "15 Diamond\n40 Coal\n5 Quartz");
registerCraft("Titanium Pickaxe", "9", "37500", "15 Titanium\n5 Diamond\n3 Amethyst");
registerCraft("Uranium Pickaxe", "11", "62500", "12 Uranium\n6 Iridium\n4 Lonsdaleite");
registerCraft("Chromodium Pickaxe", "13", "100000", "15 Vanadium\n8 Chromium\n7 Thorium\n4 Plutonium");
registerCraft("Luminite Pickaxe", "17", "275000", "12 Luminite\n6 Nanite\n5 Astral Silver\n4 Chromium");
registerCraft("Aurium Pickaxe", "19", "450000", "12 Aurium\n8 Baryte\n6 Lanite");
registerCraft("Dragonstone Pickaxe", "21", "625000", "12 Dragonstone\n10 Serdenitium\n6 Firecrystal");
registerCraft("Shadowlight Pickaxe", "23", "1000000", "12 Solarium\n12 Shadowstone\n6 Symmetrium");
registerCraft("Uelibloom Pickaxe", "25", "1500000", "12 Uelibloom\n10 Painite\n5 Sphalerite");
registerCraft("Brimstone Pickaxe", "27", "5000000", "12 Brimstone\n8 Netherite\n5 Frightstone");
registerCraft("Auric Tesla Pickaxe", "29", "10000000", "12 Auric Tesla\n8 Constellatium\n6 Circinite\n4 Astatine");
registerCraft("Stellarite Pickaxe", "36", "25000000", "12 Stellarite\n8 Praxium\n5 Onyx");
registerCraft("Cryolite Pickaxe", "39", "75000000", "10 Cryolite\n8 Meultor\n6 Linarite");
registerCraft("Reality Pickaxe", "41", "250000000", "16 Gemstone Of Reality\n8 Elementium");
registerCraft("Placement Tool", "5", "5000", "25 Iron\n30 Copper\n10 Lithium");
registerCraft("Tunneler", "10", "50000", "30 Lead\n15 Titanium\n10 Diamond");
registerCraft("Mining Helmet", "7", "12500", "25 Gold\n15 Palladium\n10 Quartz");
registerCraft("TNT Launcher", "11", "150000", "15 Osmium\n12 Bismuth\n8 Lonsdaleite\n5 Diamond");
registerCraft("Laser Drill", "17", "325000", "20 Beryllium\n12 Crystallium\n10 Nanite");
registerCraft("Cryogenum Tank", "20", "500000", "40 Aluminium\n20 Cryogenum\n15 Molybdenum\n15 Titanium");
registerCraft("Geolocator", "5", "5000", "20 Iron\n15 Aluminium\n10 Gold\n5 Lithium");
registerCraft("Geolocator MkII", "25", "1250000", "12 Quranitium\n8 Trinomium\n5 Crystallite\n4 Xeoron");
registerCraft("Flak Vest", "10", "50000", "15 Manganese\n10 Nickel\n4 Chromium");
registerCraft("Pumpkin Launcher", "0", "0", "50 Candy Corn\n30 Pumpkinium\n10 BERRIED DELIGHT", "COSMETIC", "TNT LAUNCHER");
registerCraft("Thermal Drill", "0", "0", "100 Candy Corn\n25 :whqskeleton:\n10 Bloodsteel", "COSMETIC", "LASER DRILL");
registerCraft("Ghosthammer", "0", "0", "20 Cursed Pumpkinium\n15 Soulstone\n10 Ectoplasm", "COSMETIC", "TUNNELER");
registerCraft("Halloweenium Tank", "0", "0", "50 Cold Lava\n25 Pumpkinium\n10 Enchanted Amethyst\n5 Probemonium", "COSMETIC", "CRYOGENUM TANK");
registerCraft("Lantern", "0", "0", "", "COSMETIC", "TORCH");
registerCraft("Icebreaker", "0", "0", "35 Frostarium\n20 Coldfirium\n10 Glacial Amethyst", "COSMETIC", "TUNNELER");
registerCraft("Festive Tunneler", "0", "0", "100 Candy Cane\n50 Gingerbread\n20 Frexarite", "COSMETIC", "TUNNELER");
registerCraft("Gift Launcher", "0", "0", "75 Candy Cane\n40 Gingerbread\n10 VSTAVAY BALBES", "COSMETIC", "TNT LAUNCHER");
registerCraft("Candy Drill", "0", "0", "150 Candy Cane\n50 Solidified Snow\n20 Coldfirium", "COSMETIC", "LASER DRILL");
registerCraft("Hypervest", "0", "0", "40 Blue Topaz\n20 Hyperstone\n15 Bluesteel", "COSMETIC", "FLAK VEST");
registerCraft("Snowglobe", "0", "0", "25 Snowglobe\n10 Delectium", "COSMETIC", "CRYOGENUM TANK");
registerCraft("Candle", "0", "0", "", "COSMETIC", "TORCH");

registerCraft("Tier-1 Dynamite", "1", "125", "4 Iron\n4 Tin", "ITEM");
registerCraft("Tier-2 Dynamite", "3", "250", "4 Zinc\n2 Antimony", "ITEM");
registerCraft("Tier-3 Dynamite", "5", "425", "4 Nickel\n2 Gallium", "ITEM");
registerCraft("Tier-4 Dynamite", "8", "750", "3 Lead\n4 Gold", "ITEM");
registerCraft("Tier-5 Dynamite", "10", "1250", "4 Osmium\n2 Uranium", "ITEM");
registerCraft("Tier-6 Dynamite", "14", "2500", "4 Plutonium\n2 Nanite", "ITEM");
registerCraft("Key Blue", "0", "0", "10 Key Fragment", "ITEM");

registerDepthPurchase("200", "250", "12 Coal\n10 Tin\n6 Iron");
registerDepthPurchase("300", "500", "12 Silver\n8 Gold\n6 Antimony");
registerDepthPurchase("400", "750", "12 Aluminium\n6 Quartz\n5 Amber");
registerDepthPurchase("600", "1000", "14 Graphite\n7 Lithium");
registerDepthPurchase("750", "2000", "10 Nickel\n3 Ruby\n40 Coal");
registerDepthPurchase("1000", "5000", "8 Platinum\n5 Topaz\n30 Copper");
registerDepthPurchase("1250", "10000", "4 Emerald\n3 Amethyst\n2 Chromium");
registerDepthPurchase("1500", "15000", "6 Lead\n5 Orichalcum\n4 Mithril\n1 Diamond");
registerDepthPurchase("2000", "25000", "8 Tungsten\n6 Adamantite\n4 Titanium");
registerDepthPurchase("2500", "50000", "10 Osmium\n6 Vanadium\n4 Rhenium\n1 Thorium");
registerDepthPurchase("3000", "100000", "8 Nanite\n4 Rainbonite\n3 Skyboardium");
registerDepthPurchase("3500", "250000", "10 Neodymium\n8 Baryte\n6 Molybdenum\n5 Aurium\n1 Moonstone");
registerDepthPurchase("4000", "750000", "10 Corium\n8 Astralium\n6 Crystallite\n1 SVINTUS PRIDET");
registerDepthPurchase("4500", "1250000", "10 Bloodstone\n6 Dedicatum\n5 Trinomium\n4 Promethium");
registerDepthPurchase("5000", "5000000", "12 Naquadah\n8 Frightstone\n4 Dumortierite\n1 Sunstone");
registerDepthPurchase("5500", "12500000", "10 Occultatum\n6 Constellatium\n5 Hexaferrum\n2 Quadromium\n1 KRATOS MESSI");
registerDepthPurchase("6000", "72500000", "8 Ignatius\n7 Eximite\n6 Verisite\n1 Antimatter");
registerDepthPurchase("6500", "250000000", "10 Aegistone\n8 Sylvanite\n6 Acceleratium\n3 Cosmicboardium\n1 Darkmatter");

registerInventoryPurchase("6", "500", "15 Aluminium\n20 Silver\n6 Antimony");
registerInventoryPurchase("7", "1250", "8 Cobalt\n15 Graphite\n6 Amber");
registerInventoryPurchase("8", "7500", "20 Sapphire\n20 Ruby\n10 Platinum\n40 Gold");
registerInventoryPurchase("9", "20000", "12 Orichalcum\n12 Mithril\n10 Opal\n8 Lapis Lazuli");
registerInventoryPurchase("10", "50000", "15 Adamantite\n15 Bismuth\n10 Iridium");
registerInventoryPurchase("11", "125000", "12 Rhenium\n12 Plutonium\n12 Rainbonite");
registerInventoryPurchase("12", "250000", "15 Lanite\n12 Niobium\n2 G_");
registerInventoryPurchase("13", "750000", "10 Serdenitium\n6 Corium\n2 SVINTUS PRIDET");
registerInventoryPurchase("14", "1500000", "8 Quranitium\n8 Symmetrium\n5 Promethium");
registerInventoryPurchase("15", "3750000", "10 Xeoron\n8 Copernicium\n5 Yunium");
registerInventoryPurchase("16", "10000000", "12 Cosmilite\n12 Hellite");
registerInventoryPurchase("17", "50000000", "10 Cretorium\n6 Hexaferrum\n4 Circinite");
registerInventoryPurchase("18", "500000000", "10 Onyx\n8 Praxium\n6 Illuminite");
registerInventoryPurchase("19", "500000000", "15 Ulexite\n10 Linarite");
registerInventoryPurchase("20", "1500000000", "5 Antimatter\n3 Darkmatter");

function uiNameToID(%uiName)
{
    for(%i = 0; %i < DataBlockGroup.getCount(); %i++)
    {
        %obj = DataBlockGroup.getObject(%i);

        if(%obj.getClassName() $= "ItemData" && strStr(strlwr(%obj.uiName), strlwr(%uiName)) == 0)
        {
            %item = %obj;
            break;
        }
    }

    if(!isObject(%item)) //item not found
        return;
    else
        return %item;
}

function convertRGBToHex(%dec)
{
	%str = "0123456789ABCDEF";

	while(%dec != 0)
	{
		%hexn = %dec % 16;
		%dec = mFloor(%dec / 16);
		%hex = getSubStr(%str,%hexn,1) @ %hex;    
	}

	if(strLen(%hex) == 1)
		%hex = "0" @ %hex;
	if(!strLen(%hex))
		%hex = "00";

	return %hex;
}

function rollOre(%depth)
{
    if(%depth > 6250)
        %depth = 6250;
    %j=0;
    for(%i = 0; %i < $orecount; %i++)
    {
        %ore = $ore[%i];
        if(%depth > getfield(%ore,10) && %depth <= getfield(%ore,11) && getfield(%ore,12) !$= "IGNORE" && getfield(%ore,12) !$= "LAVA")
        {
            %allowedOre[%j] = %ore;
            if(%depth > 5000 && getfield(%ore,10) < 5000)
                %totalweight += mceil(getfield(%ore,9)*1.5);
            else if(%depth > 4000 && getfield(%ore,10) < 4000)
                %totalweight += mceil(getfield(%ore,9)*1.5);
            else if(%depth > 3000 && getfield(%ore,10) < 3000)
                %totalweight += mceil(getfield(%ore,9)*1.5);
            else if(%depth > 2250 && getfield(%ore,10) < 2250)
                %totalweight += mceil(getfield(%ore,9)*1.5);
            else
                %totalweight += getfield(%ore,9);
            %j++;
        }
    }
    %random = getrandom(0, %totalweight);
    for(%i = 0; %i < %j; %i++)
    {
        if(%depth > 5000 && getfield(%allowedOre[%i],10) < 5000)
            %random -= mceil(getfield(%allowedOre[%i],9)*1.5);
        else if(%depth > 4000 && getfield(%allowedOre[%i],10) < 4000)
            %random -= mceil(getfield(%allowedOre[%i],9)*1.5);
        else if(%depth > 3000 && getfield(%allowedOre[%i],10) < 3000)
            %random -= mceil(getfield(%allowedOre[%i],9)*1.5);
        else if(%depth > 2250 && getfield(%allowedOre[%i],10) < 2250)
            %random -= mceil(getfield(%allowedOre[%i],9)*1.5);
        else
            %random -= getfield(%allowedOre[%i],9);
        if(%random <= 0)
            return getfield(%allowedOre[%i],0);
    }
}

function destroyLava(%col)
{
    if(!isobject(%col))
        return;
    if(!%col.wasfakekilled)
    {
        placebricksaround(%col.getposition(), 5000-getword(%col.getposition(),2));
        $diggedPosition[%col.position] = 1;
        serverplay3d(brickbreaksound, %col.getposition());
        if(getword(%col.position,2) < 0)
            %col.disappear(-1);
        else
            %col.fakekillbrick("0 0 10", 0);
        %col.wasfakekilled = 1;
        %col.schedule(1000, delete);
    }
}

function destroyBrick(%col, %ignore)
{
    if(%col.indestructible > 0)
        return;
    if(!%col.brickdestroyed)
    {
        if(getfield($ore[%col.oreid],12) !$= "ignore" && getfield($ore[%col.oreid],9) > 2)
            %orevein = 1;
        if(!%ignore)
            placebricksaround(%col.getposition(), 5000-getword(%col.getposition(),2), %orevein);
        %col.brickdestroyed = 1;
        if(getword(%col.position,2) < 0)
            %col.disappear(-1);
        else
            %col.fakekillbrick("0 0 10", 0);
        %col.deletion = %col.schedule(1000, delete);
        $diggedPosition[%col.position] = 1;
    }
}

function generateSphere(%pos, %radius, %extra)
{
    for(%i = 0; %i < %radius * 2 - 1; %i++)
    {
        if(%remainingradius < %radius && !%switched) 
        {
            %totalup++;
            %remainingradius++;
            %upoffset = 1 + %remainingradius;
        }
        else
        {
            %totalup++;
            %switched = 1;
            %remainingradius--;
        }
        %upoffset = %totalup - %radius;
        %resetcounter = 0;
        %generated = 0;
        %addpos = 0;
        for(%j = 0; %j < 4*%remainingradius; %j++)
        {
            if(%resetcounter >= 1*%remainingradius)
            {
                %resetcounter = 0;
                %addpos = 0;
            }
            if(%generated < 1*%remainingradius)
                %offset = %addpos SPC %addpos-%remainingradius SPC %upoffset;
            else if(%generated < 2*%remainingradius)
                %offset = -%addpos+%remainingradius SPC %addpos SPC %upoffset;
            else if(%generated < 3*%remainingradius)
                %offset = -%addpos SPC -%addpos+%remainingradius SPC %upoffset;
            else if(%generated < 4*%remainingradius)
                %offset = %addpos-%remainingradius SPC -%addpos SPC %upoffset;
            addbrick(vectoradd(%pos,%offset), oreIDfromName(sphereRoll(5000-getword(vectoradd(%pos,%offset),2))), "SPHERE");
            %resetcounter++;
            %generated++;
            %addpos++;
        }
    }
    addbrick(vectoradd(%pos,"0 0" SPC %radius), oreIDfromName(sphereRoll(5000-getword(%pos,2))), "SPHERE");
    addbrick(vectoradd(%pos,"0 0" SPC -%radius), oreIDfromName(sphereRoll(5000-getword(%pos,2))), "SPHERE");
    if(!%extra)
    {
        for(%i = 0; %i < %radius; %i++)
        {
            generateSpherePrefs(%pos, %i);
        }
    }
}

function sphereRoll(%depth)
{
    if(getrandom(1,100) >= 95) 
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
    else if(%depth < 5000 && %depth > 4000)
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
    //if(%depth <= 5000 && getrandom(1,25000) == 1)
        //%generate = "Lovely Crate";
    if(%depth >= 1000 && getrandom(1,75000) == 1)
        %generate = "Forbidden Key";
    if(%depth >= 2250 && getrandom(1,250000) == 1)
        %generate = "Challenger's Key";

    return %generate;
}

function generateSpherePrefs(%pos, %radius)
{
    for(%i = 0; %i < %radius * 2 - 1; %i++)
    {
        if(%remainingradius < %radius && !%switched) 
        {
            %totalup++;
            %remainingradius++;
            %upoffset = 1 + %remainingradius;
        }
        else
        {
            %totalup++;
            %switched = 1;
            %remainingradius--;
        }
        %upoffset = %totalup - %radius;
        %resetcounter = 0;
        %generated = 0;
        %addpos = 0;
        for(%j = 0; %j < 4*%remainingradius; %j++)
        {
            if(%resetcounter >= 1*%remainingradius)
            {
                %resetcounter = 0;
                %addpos = 0;
            }
            if(%generated < 1*%remainingradius)
                %offset = %addpos SPC %addpos-%remainingradius SPC %upoffset;
            else if(%generated < 2*%remainingradius)
                %offset = -%addpos+%remainingradius SPC %addpos SPC %upoffset;
            else if(%generated < 3*%remainingradius)
                %offset = -%addpos SPC -%addpos+%remainingradius SPC %upoffset;
            else if(%generated < 4*%remainingradius)
                %offset = %addpos-%remainingradius SPC -%addpos SPC %upoffset;
            $diggedposition[vectoradd(%pos,%offset)] = 1;
            %resetcounter++;
            %generated++;
            %addpos++;
        }
    }
    $diggedposition[vectoradd(%pos,"0 0" SPC %radius)] = 1;
    $diggedposition[vectoradd(%pos,"0 0" SPC -%radius)] = 1;
}

function mineOre(%obj, %col, %drilling, %damage)
{
    if(%col.canmine)
	{
		%client = %obj.client;
		if(getfield($ore[%col.oreid],4) > %client.level)
		{
            %color = convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[%col.oreid],5)),0) * 255));
			%color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[%col.oreid],5)),1) * 255));
			%color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[%col.oreid],5)),2) * 255));
			%color = "<color:" @ %color @ ">";
            %oreName = getfield($ore[%col.oreid],0);
            if(%oreName $= "voidstone")
                %color = "<color:3520d1>";
            if(%col.maxhealth > 0 || %col.indestructible > 0)
            {
                %currentHealth = mfloatlength(%col.health,0);
                %maxHealth = mfloatlength(%col.maxhealth,0);
                if(%col.customValue != 0)
                    %value = %col.customValue;
                else
                    %value = getfield($ore[%col.oreid],2);
                if(%col.customEXP != 0)
                    %exp = %col.customEXP;
                else
                    %exp = getfield($ore[%col.oreid],3);
                if(%client.prestigecashbonus)
                    %value = mfloatlength(%value*(1+%client.prestigecashbonus)*(1+%client.achievementcashbonus),1);
                if(%client.prestigeexpbonus)
                    %exp = mfloatlength(%exp*(1+%client.prestigeexpbonus)*(1+%client.achievementexpbonus),1);
                if(%col.indestructible > 0)
                {
                    %currentHealth = "invinicle";
                    %maxhealth = "ten gorillion trillion infinit";
                }
		        %client.centerprint("<font:verdana:35>" @ %color @ %oreName NL "<font:verdana:20>\c0Level Requirement:" SPC getfield($ore[%col.oreid],4) NL "<font:arial:20>\c3Value:" SPC %value @ "$" NL "\c4EXP:" SPC %exp NL "\c2Health:" SPC %currenthealth @ "/" @ %maxhealth NL "<font:arial:15>\c6Currently Held:" SPC mceil(%client.inventory[getfield($ore[%col.oreid],0)]) @ "x",1);
            }
            else
		        %client.centerprint("<font:verdana:35>" @ %color @ %oreName NL "<font:verdana:20>\c0Level Requirement:" SPC getfield($ore[%col.oreid],4) NL "<font:arial:20>\c3Value:" SPC getfield($ore[%col.oreid],2)*(1+%client.prestigecashbonus)*(1+%client.achievementcashbonus) @ "$" NL "\c4EXP:" SPC getfield($ore[%col.oreid],3)*(1+%client.prestigeexpbonus)*(1+%client.achievementexpbonus) NL "\c2Health:" SPC mfloatlength(%col.health,0) @ "/" @ getfield($ore[%col.oreid],1) NL "<font:arial:15>\c6Currently Held:" SPC mceil(%client.inventory[getfield($ore[%col.oreid],0)]) @ "x",1);
			//%client.centerprint("<font:arial:25>\c0You do not meet the level requirement of" SPC getfield($ore[%col.oreid],4) SPC "to mine this ore!",3);
			serverplay3d(floatingplanterrorsound, %col.getposition());
			return;
		}
        if(!%drilling)
            %miningpower = mfloatlength((%obj.client.miningpower+%obj.client.prestigeminingpower+%obj.client.achievementminingpower)*(1+%obj.client.miningmultiplier)*(1+%obj.client.prestigeminingmultiplier)*(1+%obj.client.achievementminingmultiplier)*(1-%client.miningpowerloss)*(1-%client.player.torchLoss),0);
        else if(%drilling == 1)
            %miningpower = mfloatlength((%obj.client.miningpower+%obj.client.prestigeminingpower+%obj.client.achievementminingpower)*(1+%obj.client.miningmultiplier)*(1+%obj.client.prestigeminingmultiplier)*(1+%obj.client.achievementminingmultiplier)*(1-(%client.miningpowerloss*0.67))*(1-(%client.player.torchLoss*0.67)),0);
        else if(%drilling == 2)
            %miningpower = mfloatlength((%obj.client.miningpower+%obj.client.prestigeminingpower+%obj.client.achievementminingpower)*(1+%obj.client.miningmultiplier)*(1+%obj.client.prestigeminingmultiplier)*(1+%obj.client.achievementminingmultiplier)*(1+%obj.laserdrilloverheat)*(1-%client.miningpowerloss)*(1-%client.player.torchLoss),0);
        if(getfield($ore[%col.oreid],12) !$= "LAVA" || getfield($ore[%col.oreid],12) $= "LAVA" && %drilling)
        {
            if(!%col.indestructible)
            {
                %oldHealth = %col.health;
                %col.health-=%miningpower;
                if(%col.health < 0)
                    %newHealth = 0;
                else
                    %newHealth = %col.health;
                %damageDiff = %oldhealth - %newhealth;
                %obj.client.totalbrickdamage+=%damageDiff;
                %total = %obj.client.totalbrickdamage;
                if(%total >= 1000)
                    %obj.client.unlockachievement("first steps");
                if(%total >= 10000)
                    %obj.client.unlockachievement("a lot of damage");
                if(%total >= 100000)
                    %obj.client.unlockachievement("even more damage");
                if(%total >= 1000000)
                    %obj.client.unlockachievement("excavator");
                if(%total >= 50000000)
                    %obj.client.unlockachievement("caves decimator");
                if(%total >= 250000000)
                    %obj.client.unlockachievement("ores for breakfast");
                if(%total >= 1000000000)
                    %obj.client.unlockachievement("ultimate destruction");
                if(%total >= "1e+12")
                    %obj.client.unlockachievement("you can either be big digger or dig bigger");
            }
        }
        else if(getfield($ore[%col.oreid],12) $= "LAVA" && !%drilling)
        {
            %oreName = getfield($ore[%col.oreid],0);
            if(%oreName $= "black hole")
            {
                %col.schedule(0, setcolor, 17);
                %col.schedule(0, setcolorfx, 3);
                %col.schedule(4500, setcolor, 16);
                %col.schedule(4500, setcolorfx, 0);
                //%col.schedule(5000, fakekillbrick, "0 0 10", 0);
                //%col.schedule(6000, delete);
            }
            else if(%oreName $= "lava")
            {
                %col.schedule(0, setcolor, 29);
                %col.schedule(4500, setcolorfx, 0);
                %col.schedule(4500, setcolor, 5);
                //%col.schedule(5000, fakekillbrick, "0 0 10", 0);
                //%col.schedule(6000, delete);
            }
            if(%col.destroyschedule != 1)
            {
                %col.destroyschedule = 1;
                %col.placebrick = schedule(5000, 0, destroylava, %col);
            }
            %col.health-=1;
        }
		if(%col.health<0)
			%col.health=0;
        if(!%col.compressedOre)
        {
            %color = convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[%col.oreid],5)),0) * 255));
			%color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[%col.oreid],5)),1) * 255));
			%color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[%col.oreid],5)),2) * 255));
			%color = "<color:" @ %color @ ">";
            %oreName = getfield($ore[%col.oreid],0);
            if(%oreName $= "voidstone")
                %color = "<color:3520d1>";
            if(%col.maxhealth > 0 || %col.indestructible > 0)
            {
                %currentHealth = mfloatlength(%col.health,0);
                %maxHealth = mfloatlength(%col.maxhealth,0);
                if(%col.customValue != 0)
                    %value = %col.customValue;
                else
                    %value = getfield($ore[%col.oreid],2);
                if(%col.customEXP != 0)
                    %exp = %col.customEXP;
                else
                    %exp = getfield($ore[%col.oreid],3);
                if(%client.prestigecashbonus)
                    %value = mfloatlength(%value*(1+%client.prestigecashbonus)*(1+%client.achievementcashbonus),1);
                if(%client.prestigeexpbonus)
                    %exp = mfloatlength(%exp*(1+%client.prestigeexpbonus)*(1+%client.achievementexpbonus),1);
                if(%col.indestructible > 0)
                {
                    %currentHealth = "invinicle";
                    %maxhealth = "ten gorillion trillion infinit";
                }
		        %client.centerprint("<font:verdana:35>" @ %color @ %oreName NL "<font:arial:20>\c3Value:" SPC %value @ "$" NL "\c4EXP:" SPC %exp NL "\c2Health:" SPC %currenthealth @ "/" @ %maxhealth NL "<font:arial:15>\c6Currently Held:" SPC mceil(%client.inventory[getfield($ore[%col.oreid],0)]) @ "x",1);
            }
            else if(%col.placedBrick)
                %client.centerprint("<font:verdana:35>" @ %color @ %oreName NL "<font:verdana:18>\c5(Owner:" SPC %col.placedby @ ")" NL "<font:arial:20>\c3Value:" SPC 0 @ "$" NL "\c4EXP:" SPC 0 NL "\c2Health:" SPC mfloatlength(%col.health,0) @ "/" @ getfield($ore[%col.oreid],1) NL "<font:arial:15>\c6Currently Held:" SPC mceil(%client.inventory[getfield($ore[%col.oreid],0)]) @ "x",1);
            else
                %client.centerprint("<font:verdana:35>" @ %color @ getfield($ore[%col.oreid],0) NL "<font:arial:20>\c3Value:" SPC getfield($ore[%col.oreid],2)*(1+%client.prestigecashbonus)*(1+%client.achievementcashbonus) @ "$" NL "\c4EXP:" SPC getfield($ore[%col.oreid],3)*(1+%client.prestigeexpbonus)*(1+%client.achievementexpbonus) NL "\c2Health:" SPC mfloatlength(%col.health,0) @ "/" @ getfield($ore[%col.oreid],1) NL "<font:arial:15>\c6Currently Held:" SPC mceil(%client.inventory[getfield($ore[%col.oreid],0)]) @ "x",1);
        }
        else if(%col.compressedOre)
        {
            %color = convertRGBtoHex(mFloor(getWord(getColorIdTable(%col.colorid),0) * 255));
			%color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(%col.colorid),1) * 255));
			%color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(%col.colorid),2) * 255));
            %color = "<color:" @ %color @ ">";
		    %client.centerprint("<font:verdana:35>" @ %color @ getfield($ore[%col.oreid],0) NL "<font:verdana:24>" SPC %col.compressedOreAmount @ "x" SPC %col.compressedOreType NL "<font:arial:20>\c3Value:" SPC %col.compressedOreValue*(1+%client.prestigecashbonus)*(1+%client.achievementcashbonus) @ "$" NL "\c4EXP:" SPC %col.compressedOreEXP*(1+%client.prestigeexpbonus)*(1+%client.achievementexpbonus) NL "\c2Health:" SPC %col.health @ "/" @ %col.compressedOreHealth NL "<font:arial:15>\c6Currently Held:" SPC mceil(%client.inventory[%col.compressedOreType]) @ "x",1);
        }
		if(%col.health<=0)
		{
            if(%drilling)
                %obj.drillbrick++;
            if(getfield($ore[%col.oreid],12) !$= "LAVA")
            {
                %col.brickdigged = 1;
                %obj.client.totalbricks++;
                %total = %obj.client.totalbricks;
                if(%total >= 100)
                    %obj.client.unlockachievement("humble digger");
                if(%total >= 1000)
                    %obj.client.unlockachievement("beginner digger");
                if(%total >= 2500)
                    %obj.client.unlockachievement("certified digger");
                if(%total >= 10000)
                    %obj.client.unlockachievement("renowned digger");
                if(%total >= 50000)
                    %obj.client.unlockachievement("digception");
                if(%total >= 100000)
                    %obj.client.unlockachievement("digger boss");
                if(%total >= 250000)
                    %obj.client.unlockachievement("earth dismantler");
                if(%total >= 1000000)
                    %obj.client.unlockachievement("destroyer of worlds");
                %pos = mfloatlength(5000.2-getword(%col.getposition(),2),0);
                if(%pos > 500)
                    %obj.client.unlockachievement("new sights");
                if(%pos > 1000)
                    %obj.client.unlockachievement("going deeper");
                if(%pos > 1500)
                    %obj.client.unlockachievement("rock and stone");
                if(%pos > 2250)
                    %obj.client.unlockachievement("going even deeper");
                if(%pos > 3000)
                    %obj.client.unlockachievement("it's getting hotter");
                if(%pos > 4000)
                    %obj.client.unlockachievement("literally minecraft");
                if(%pos > 5000)
                    %obj.client.unlockachievement("the deep is calling");
            }
            //%cave = getrandom(1,200);
            //if(%cave == 1 && !$cavegenerating)
            //{
                //for(%i = 0; %i < 10; %i++)
                //{
                    //%randompos = getrandom(-20,20) SPC getrandom(-20,20) SPC getrandom(-30,5);
                    //initcontainerboxsearch(vectoradd(vectoradd(%col.position, %randompos),"0 0 6"), "26 26 13", $typemasks::fxbrickobjecttype);
                    //if(isobject(%search = containersearchnext()))
                    //{
                        //%success = 0;
                    //}
                    //else
                        //%success = 1;
                //}
                //if(%success)
                //{
                    //loadcave("cavepreset" @ getrandom(1,3), vectoradd(%col.position, %randompos));
                //}
            //}
            if(!%drilling)
            {
                if(getfield($ore[%col.oreid],12) $= "LAVA")
                {
                    placebricksaround(%col.getposition(), 5000-getword(%col.getposition(),2));
                    $diggedPosition[%col.position] = 1;
                    %col.fakekillbrick("0 0 10", 0);
                    serverplay3d(brickbreaksound, %col.getposition());
                    %col.schedule(1000, delete);
                    %obj.addhealth(-33);
                    cancel(%col.placebrick);
                    return;
                }
            }
			%col.destroy = schedule(0, 0, destroyBrick, %col);
            if(getfield($ore[%col.oreid],12) $= "" || getfield($ore[%col.oreid],12) $= "EVENT" || getfield($ore[%col.oreid],12) $= "IGNORE")
            {
			    %obj.client.inventory[getfield($ore[%col.oreid],0)]+=1;
                if(!%col.placedbrick)
                {
                    if(!%col.customEXP)
                        %obj.client.addexp(getfield($ore[%col.oreid],3));
                    else
                        %obj.client.addexp(%col.customEXP);
                }
            }
            else if((getfield($ore[%col.oreid],12) $= "NOORE"))
                %obj.client.addexp(getfield($ore[%col.oreid],3));
            else if(getfield($ore[%col.oreid],12) $= "CRATE")
                openCrate(%obj, %col);
            else if(getfield($ore[%col.oreid],12) $= "LOVELYCRATE")
                openEventCrate(%obj, %col);
            else if(getfield($ore[%col.oreid],12) $= "TREASURECHEST")
                %obj.client.addmoney(mfloatlength(getfield($ore[%col.oreid],2)*(1+%obj.client.prestigecashbonus+%obj.client.achievementcashbonus),0));
            else if(getfield($ore[%col.oreid],12) $= "KEY")
            {
                if(getfield($ore[%col.oreid],0) $= "Forbidden Key")
                    %data = yellowkeyitem;
                else if(getfield($ore[%col.oreid],0) $= "Challenger's Key")
                    %data = redkeyitem;
                %Item = new Item()
                {
                    dataBlock = %data;
                    position = %col.position;
                };
                %Item.BL_ID = 777;
                %Item.minigame = $miningMinigame;
                %Item.spawnBrick = -1;
            }
            else if(getfield($ore[%col.oreid],12) $= "CRATEVAULT")
            {
                %obj.client.openedvaults++;
                %total = %obj.client.openedvaults;
                if(%total >= 1)
                    %obj.client.unlockachievement("breaking bank");
                if(%total >= 10)
                    %obj.client.unlockachievement("successful heist");
                if(%total >= 50)
                    %obj.client.unlockachievement("the day of payment");
                if(%total >= 200)
                    %obj.client.unlockachievement("the greatest risk is not taking one");
                if(%total >= 500)
                    %obj.client.unlockachievement("pole vaulting at its finest");
                if(getfield($ore[%col.oreid],0) $= "Tier-1 Crate Vault")
                {
                    %crates = getrandom(1,100);
                    if(%crates <= 40)
                        %amount = 2;
                    else if(%crates <= 75)
                        %amount = 3;
                    else if(%crates <= 88)
                        %amount = 4;
                    else if(%crates <= 94)
                        %amount = 5;
                    else if(%crates <= 97)
                        %amount = 6;
                    else if(%crates <= 99)
                        %amount = 7;
                    if(%crates == 100)
                        %amount = 8;
                    if(%amount == 8)
                        %obj.client.unlockachievement("lucky digger");
                    for(%i = 0; %i < %amount; %i++)
                    {
                        %tier = getrandom(1,100);
                        if(%tier <= 15)
                            %crate = 1;
                        else if(%tier <= 35)
                            %crate = 2;
                        else if(%tier <= 60)
                            %crate = 3;
                        else if(%tier <= 80)
                            %crate = 4;
                        else if(%tier <= 100)
                            %crate = 5;
                        %reward[%i] = openCrate(%obj, %col, %crate);
                    }
                    for(%i = 0; %i < %amount; %i++)
                    {
                        if(%i == %amount-1)
                            %rewardLine = %rewardLine SPC %reward[%i];
                        else
                            %rewardLine = %rewardLine SPC %reward[%i] @ ",";
                    }
                    announcemessage("\c2" @ %obj.client.name SPC "\c3has cracked open a\c5 Tier-1 Crate Vault\c3 and received\c5" @ %rewardline @ "!");
                }
                else if(getfield($ore[%col.oreid],0) $= "Tier-2 Crate Vault")
                {
                    %crates = getrandom(1,100);
                    if(%crates <= 30)
                        %amount = 3;
                    else if(%crates <= 55)
                        %amount = 4;
                    else if(%crates <= 75)
                        %amount = 5;
                    else if(%crates <= 90)
                        %amount = 6;
                    else if(%crates <= 97)
                        %amount = 7;
                    else if(%crates <= 99)
                        %amount = 8;
                    if(%crates == 100)
                        %amount = 9;
                    if(%amount == 9)
                        %obj.client.unlockachievement("lucky digger");
                    for(%i = 0; %i < %amount; %i++)
                    {
                        %tier = getrandom(1,100);
                        if(%tier <= 7)
                            %crate = 1;
                        else if(%tier <= 18)
                            %crate = 2;
                        else if(%tier <= 32)
                            %crate = 3;
                        else if(%tier <= 53)
                            %crate = 4;
                        else if(%tier <= 77)
                            %crate = 5;
                        else if(%tier <= 100)
                            %crate = 6;
                        %reward[%i] = openCrate(%obj, %col, %crate);
                    }
                    for(%i = 0; %i < %amount; %i++)
                    {
                        if(%i == %amount-1)
                            %rewardLine = %rewardLine SPC %reward[%i];
                        else
                            %rewardLine = %rewardLine SPC %reward[%i] @ ",";
                    }
                    announcemessage("\c2" @ %obj.client.name SPC "\c3has cracked open a<color:FF4500> Tier-2 Crate Vault\c3 and received\c5" @ %rewardline @ "!");
                }
                else if(getfield($ore[%col.oreid],0) $= "Tier-3 Crate Vault")
                {
                    %crates = getrandom(1,100);
                    if(%crates <= 15)
                        %amount = 4;
                    else if(%crates <= 28)
                        %amount = 5;
                    else if(%crates <= 46)
                        %amount = 6;
                    else if(%crates <= 71)
                        %amount = 7;
                    else if(%crates <= 95)
                        %amount = 8;
                    else if(%crates <= 99)
                        %amount = 9;
                    if(%crates == 100)
                        %amount = 10;
                    if(%amount == 10)
                        %obj.client.unlockachievement("lucky digger");
                    for(%i = 0; %i < %amount; %i++)
                    {
                        %tier = getrandom(1,100);
                        if(%tier <= 2)
                            %crate = 1;
                        else if(%tier <= 6)
                            %crate = 2;
                        else if(%tier <= 13)
                            %crate = 3;
                        else if(%tier <= 30)
                            %crate = 4;
                        else if(%tier <= 56)
                            %crate = 5;
                        else if(%tier <= 75)
                            %crate = 6;
                        else if(%tier <= 100)
                            %crate = 7;
                        %reward[%i] = openCrate(%obj, %col, %crate);
                    }
                    for(%i = 0; %i < %amount; %i++)
                    {
                        if(%i == %amount-1)
                            %rewardLine = %rewardLine SPC %reward[%i];
                        else
                            %rewardLine = %rewardLine SPC %reward[%i] @ ",";
                    }
                    announcemessage("\c2" @ %obj.client.name SPC "\c3has cracked open a\c0 Tier-3 Crate Vault\c3 and received\c5" @ %rewardline @ "!");
                }
                else if(getfield($ore[%col.oreid],0) $= "Tier-4 Crate Vault")
                {
                    %crates = getrandom(1,100);
                    if(%crates <= 13)
                        %amount = 5;
                    else if(%crates <= 26)
                        %amount = 6;
                    else if(%crates <= 43)
                        %amount = 7;
                    else if(%crates <= 69)
                        %amount = 8;
                    else if(%crates <= 93)
                        %amount = 9;
                    else if(%crates <= 98)
                        %amount = 10;
                    if(%crates == 100)
                        %amount = 11;
                    if(%amount == 11)
                        %obj.client.unlockachievement("lucky digger");
                    for(%i = 0; %i < %amount; %i++)
                    {
                        %tier = getrandom(1,100);
                        if(%tier <= 1)
                            %crate = 1;
                        else if(%tier <= 3)
                            %crate = 2;
                        else if(%tier <= 6)
                            %crate = 3;
                        else if(%tier <= 11)
                            %crate = 4;
                        else if(%tier <= 27)
                            %crate = 5;
                        else if(%tier <= 54)
                            %crate = 6;
                        else if(%tier <= 74)
                            %crate = 7;
                        else if(%tier <= 100)
                            %crate = 8;
                        %reward[%i] = openCrate(%obj, %col, %crate);
                    }
                    for(%i = 0; %i < %amount; %i++)
                    {
                        if(strlen(%rewardLine SPC %reward[%i]) < 180)
                        {
                            if(%i == %amount-1)
                                %rewardLine = %rewardLine SPC %reward[%i];
                            else
                                %rewardLine = %rewardLine SPC %reward[%i] @ ",";
                        }
                        else
                        {
                            %seperate = 1;
                            if(%rewardline2 $= "")
                                %rewardline2 = %reward[%i] @ ",";
                            if(%i == %amount-1)
                                %rewardLine2 = %rewardLine2 SPC %reward[%i];
                            else
                                %rewardLine2 = %rewardLine2 SPC %reward[%i] @ ",";
                        }
                    }
                    if(%seperate)
                    {
                        announcemessage("\c2" @ %obj.client.name SPC "\c3has cracked open a<color:4B0082> Tier-4 Crate Vault\c3 and received\c5" @ %rewardline);
                        announcemessage(%rewardline2 @ "!");
                    }
                    else
                        announcemessage("\c2" @ %obj.client.name SPC "\c3has cracked open a<color:4B0082> Tier-4 Crate Vault\c3 and received\c5" @ %rewardline @ "!");
                }
            }
            else if(getfield($ore[%col.oreid],12) $= "COMPRESSEDORE")
            {
                %obj.client.inventory[%col.compressedOreType]+=%col.compressedOreAmount;
                %obj.client.addexp(%col.compressedOreEXP);
            }
            else if(getfield($ore[%col.oreid],12) $= "QUANTUMDISRUPTION")
            {
                if(!%col.customEXP)
                    %obj.client.addexp(getfield($ore[%col.oreid],3));
                else
                    %obj.client.addexp(%col.customEXP);
                %ore = rollQuantumOre(5000 - getword(%col.getposition(),2));
                if(strchr(getfield($ore[%ore],0), "Crate") $= "Crate")
                    %obj.client.unlockachievement("time manipulated");
                if(strchr(getfield($ore[%ore],0), "Crate Vault") $= "Crate Vault")
                    %obj.client.unlockachievement("is this even legal");
                %x = getword(%col.position,0);
                %y = getword(%col.position,1);
                %z = getword(%col.position,2);
                %pos[0] = %x+1 SPC %y SPC %z-1;
                %pos[1] = %x SPC %y SPC %z-1;
                %pos[2] = %x-1 SPC %y SPC %z-1;
                %pos[3] = %x+1 SPC %y-1 SPC %z-1;
                %pos[4] = %x SPC %y-1 SPC %z-1;
                %pos[5] = %x-1 SPC %y-1 SPC %z-1;
                %pos[6] = %x+1 SPC %y+1 SPC %z-1;
                %pos[7] = %x SPC %y+1 SPC %z-1;
                %pos[8] = %x-1 SPC %y+1 SPC %z-1;
                %pos[9] = %x+1 SPC %y SPC %z;
                %pos[10] = %x SPC %y SPC %z;
                %pos[11] = %x-1 SPC %y SPC %z;
                %pos[12] = %x+1 SPC %y-1 SPC %z;
                %pos[13] = %x SPC %y-1 SPC %z;
                %pos[14] = %x-1 SPC %y-1 SPC %z;
                %pos[15] = %x+1 SPC %y+1 SPC %z;
                %pos[16] = %x SPC %y+1 SPC %z;
                %pos[17] = %x-1 SPC %y+1 SPC %z;
                %pos[18] = %x+1 SPC %y SPC %z+1;
                %pos[19] = %x SPC %y SPC %z+1;
                %pos[20] = %x-1 SPC %y SPC %z+1;
                %pos[21] = %x+1 SPC %y-1 SPC %z+1;
                %pos[22] = %x SPC %y-1 SPC %z+1;
                %pos[23] = %x-1 SPC %y-1 SPC %z+1;
                %pos[24] = %x+1 SPC %y+1 SPC %z+1;
                %pos[25] = %x SPC %y+1 SPC %z+1;
                %pos[26] = %x-1 SPC %y+1 SPC %z+1;
                for(%i = 0; %i < 27; %i++)
                {
                    %allowPlant = 1;
                    initContainerRadiusSearch(%pos[%i], 2, $typemasks::fxbrickobjecttype);
                    while(%search = containersearchnext())
                    {
                        if(%search.position $= %pos[%i])
                            %allowPlant = 0;
                    }
                    if(%allowPlant)
                        addbrick(%pos[%i], %ore, "ignore");
                }
                initcontainerboxsearch(%col.position, "1 1 1", $typemasks::fxbrickobjecttype);
                while(isobject(%search = containersearchnext()))
                {
                    if(%search == %col)
                        continue;
                    %search.turnOre(%ore);
                }
            }
            else if(getfield($ore[%col.oreid],12) $= "PRESENT")
            {
                serverplay3d(synth_08_sound, %obj.position);
                %depth = 5000-getword(%col.position,2);
                %obj.client.addexp(getfield($ore[%col.oreid],3));
                %obj.client.addmoney(mfloatlength(getfield($ore[%col.oreid],2)*(1+%obj.client.prestigecashbonus),0));
                %drop = getrandom(1,3);
                if(%drop == 1)
                {
                    %exp = mfloatlength(getrandom(100,200) * (1+%depth/50),0);
                    %obj.client.addexp(%exp, 1);
                    %obj.client.chatmessage("\c6The present had given you\c4" SPC %exp SPC "EXP\c6!");
                }
                else if(%drop == 2)
                {
                    %cash = mfloatlength(getrandom(75,150) * (1+%depth/75),0);
                    %obj.client.addmoney(%cash);
                    %obj.client.chatmessage("\c6The present had given you\c2" SPC %cash @ "$\c6!");
                }
                else if(%drop == 3)
                {
                    %ore = rollore(%depth);
                    if(%ore $= "present")
                        %ore = rollore(%depth);
                    else if(%ore $= "present")
                        %ore = rollore(%depth);
                    %color = convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[oreidfromname(%ore)],5)),0) * 255));
                    %color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[oreidfromname(%ore)],5)),1) * 255));
                    %color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[oreidfromname(%ore)],5)),2) * 255));
                    %color = "<color:" @ %color @ ">";
                    if(getrandom(1,5) == 1)
                    {
                        %ore = "Key Fragment";
                        %color = convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[oreidfromname(%ore)],5)),0) * 255));
                        %color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[oreidfromname(%ore)],5)),1) * 255));
                        %color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[oreidfromname(%ore)],5)),2) * 255));
                        %color = "<color:" @ %color @ ">";
                        serverplay3d(alarmsound, %obj.position);
                        %obj.client.chatmessage("\c1RARE DROP! \c6The present had given you a" @ %color SPC %ore @ "\c6!");
                        %amount = 1;
                    }
                    else
                    {
                        %mult = 1+(%depth-getfield($ore[oreidfromname(%ore)],10))/100;
                        %amount = mfloatlength(getrandom(1,5) * %mult,0);
                        %obj.client.chatmessage("\c6The present had given you" @ %color SPC %amount @ "x" SPC %ore @ "\c6!");
                    }
                    %obj.client.inventory[%ore] += %amount;
                }
            }
            minedirt(%col);
		}
	}
}

function fxdtsbrick::turnOre(%brick, %oreid)
{
    %brick.oreid = oreidfromname(getfield($ore[%oreid],0));
    %brick.health = getfield($ore[%oreid],1);
    %brick.setcolor(getfield($ore[%oreid],5));
    %brick.setcolorfx(getfield($ore[%oreid],6));
    %brick.setshapefx(getfield($ore[%oreid],7));
    %brick.setprint($printnametable[getfield($ore[%oreid],8)]);
}

function rollCrateOre(%maxdepth)
{
    %ore = getrandom(0, $orecount-1);
    %oretag = getfield($ore[%ore],12);
    %eventMinDepth = getfield($ore[%ore],10);
    %eventMinDepth = getfield($ore[%ore],11);
    %name = getfield($ore[%ore],0);
    if(getfield($ore[%ore],10) > %maxdepth)
    {
        rollCrateOre(%maxdepth);
        return;
    }
    else if(%oretag $= "IGNORE" || %oretag $= "TREASURECHEST" || %oretag $= "NOORE" || %oretag $= "CRATE" || %oretag $= "CRATEVAULT" || %oretag $= "QUANTUMDISRUPTION" || %oretag $= "KEY" || %oretag $= "PRESENT" && %oretag $= "LOVELYCRATE" && %oretag $= "MATERIAL" || %oretag $= "EVENT" && %eventMinDepth == 0 && %eventMaxDepth == 0 || %name $= "Key Fragment")
    {
        rollCrateOre(%maxdepth);
        return;
    }
    else
        return %ore;
}

function rollQuantumOre(%currentdepth, %maxdepth)
{
    %ore = getrandom(0, $orecount-1);
    %oretag = getfield($ore[%ore],12);
    %eventMinDepth = getfield($ore[%ore],10);
    %eventMinDepth = getfield($ore[%ore],11);
    %name = getfield($ore[%ore],0);
    if(%currentdepth < getfield($ore[%ore],10))
    {
        rollQuantumOre(%currentDepth);
        return;
    }
    else if(%oretag $= "QUANTUMDISRUPTION" || %oretag $= "KEY" || %oretag $= "EVENT" && %eventMinDepth == 0 && %eventMaxDepth == 0 || %name $= "Key Fragment")
    {
        rollQuantumOre(%currentDepth);
        return;
    }
    else
        return %ore;
}

function openCrate(%obj, %col, %crate)
{
    if(!%crate)
    {
        %obj.client.openedcrates++;
        %total = %obj.client.openedcrates;
        if(%total >= 1)
            %obj.client.unlockachievement("first impressions");
        if(%total >= 10)
            %obj.client.unlockachievement("get scammed or get rich");
        if(%total >= 50)
            %obj.client.unlockachievement("i love rng");
        if(%total >= 100)
            %obj.client.unlockachievement("name me the god of diggers");
        if(%total >= 250)
            %obj.client.unlockachievement("crate infestation");
        if(%total >= 500)
            %obj.client.unlockachievement("unsuccessful entropy");
    }
    %obj.client.addexp(getfield($ore[%col.oreid],3));
    %drop = getrandom(1,3);
    if(getfield($ore[%col.oreid],0) $= "Tier-1 Crate" || %crate == 1)
    {
        if(%drop == 1)
        {
            %money = mfloatlength(getrandom(75,375)*(1+%obj.client.prestigecratedrops+%obj.client.achievementcratedrops),0);
            %obj.client.addmoney(%money);
            if(!%crate)
                announcemessage("\c2" @ %obj.client.name SPC "\c3has opened a\c7 Tier-1 Crate\c3 and received\c5" SPC %money @ "$!");
            if(%crate)
                return "\c7" @ %money @ "$";
        }
        else if(%drop == 2)
        {
            %exp = mfloatlength(getrandom(100,500)*(1+%obj.client.prestigecratedrops+%obj.client.achievementcratedrops),0);
            %obj.client.addexp(%exp);
            if(!%crate)
                announcemessage("\c2" @ %obj.client.name SPC "\c3has opened a\c7 Tier-1 Crate\c3 and received\c5" SPC %exp SPC "EXP!");
            if(%crate)
                return "\c7" @ %exp SPC "EXP";
        }
        else if(%drop == 3)
        {
            %dropsMult = 1+%obj.client.prestigecratedrops+%obj.client.achievementcratedrops-%obj.client.prestigecashbonus-%obj.client.achievementcashbonus;
            if(%dropsMult < 1)
                %dropsMult = 1;
            %ore = rollCrateOre(500);
            if(getfield($ore[%ore],12) $= "EVENT")
                %oreamount = getrandom(2,10);
            else
                %oreamount = mfloatlength(getrandom(2,10)*%dropsMult,0);
            if(!%crate)
                announcemessage("\c2" @ %obj.client.name SPC "\c3has opened a\c7 Tier-1 Crate\c3 and received\c5" SPC %oreamount @ "x" SPC getfield($ore[%ore],0) @ "!");
            %obj.client.inventory[getfield($ore[%ore],0)]+=%oreamount;
            if(%crate)
                return "\c7" @ %oreamount @ "x" SPC getfield($ore[%ore],0);
        }
    }
    else if(getfield($ore[%col.oreid],0) $= "Tier-2 Crate" || %crate == 2)
    {
        if(%drop == 1)
        {
            %money = mfloatlength(getrandom(375,1500)*(1+%obj.client.prestigecratedrops+%obj.client.achievementcratedrops),0);
            %obj.client.addmoney(%money);
            if(!%crate)
                announcemessage("\c2" @ %obj.client.name SPC "\c3has opened a\c6 Tier-2 Crate\c3 and received\c5" SPC %money @ "$!");
            if(%crate)
                return "\c6" @ %money @ "$";
        }
        else if(%drop == 2)
        {
            %exp = mfloatlength(getrandom(500,2000)*(1+%obj.client.prestigecratedrops+%obj.client.achievementcratedrops),0);
            %obj.client.addexp(%exp);
            if(!%crate)
                announcemessage("\c2" @ %obj.client.name SPC "\c3has opened a\c6 Tier-2 Crate\c3 and received\c5" SPC %exp SPC "EXP!");
            if(%crate)
                return "\c6" @ %exp SPC "EXP";
        }
        else if(%drop == 3)
        {
            %dropsMult = 1+%obj.client.prestigecratedrops+%obj.client.achievementcratedrops-%obj.client.prestigecashbonus-%obj.client.achievementcashbonus;
            if(%dropsMult < 1)
                %dropsMult = 1;
            %ore = rollCrateOre(1000);
            if(getfield($ore[%ore],12) $= "EVENT")
                %oreamount = getrandom(4,18);
            else
                %oreamount = mfloatlength(getrandom(4,18)*%dropsMult,0);
            if(getfield($ore[%ore],10) < 500 && getfield($ore[%ore],12) !$= "EVENT")
                %oreamount = %oreamount*6;
            if(!%crate)
                announcemessage("\c2" @ %obj.client.name SPC "\c3has opened a\c6 Tier-2 Crate\c3 and received\c5" SPC %oreamount @ "x" SPC getfield($ore[%ore],0) @ "!");
            %obj.client.inventory[getfield($ore[%ore],0)]+=%oreamount;
            if(%crate)
                return "\c6" @ %oreamount @ "x" SPC getfield($ore[%ore],0);
        }
    }
    else if(getfield($ore[%col.oreid],0) $= "Tier-3 Crate" || %crate == 3)
    {
        if(%drop == 1)
        {
            %money = mfloatlength(getrandom(5000,13750)*(1+%obj.client.prestigecratedrops+%obj.client.achievementcratedrops),0);
            %obj.client.addmoney(%money);
            if(!%crate)
                announcemessage("\c2" @ %obj.client.name SPC "\c3has opened a\c2 Tier-3 Crate\c3 and received\c5" SPC %money @ "$!");
            if(%crate)
                return "\c2" @ %money @ "$";
        }
        else if(%drop == 2)
        {
            %exp = mfloatlength(getrandom(7500,20000)*(1+%obj.client.prestigecratedrops+%obj.client.achievementcratedrops),0);
            %obj.client.addexp(%exp,1);
            if(!%crate)
                announcemessage("\c2" @ %obj.client.name SPC "\c3has opened a\c2 Tier-3 Crate\c3 and received\c5" SPC %exp SPC "EXP!");
            if(%crate)
                return "\c2" @ %exp SPC "EXP";
        }
        else if(%drop == 3)
        {
            %dropsMult = 1+%obj.client.prestigecratedrops+%obj.client.achievementcratedrops-%obj.client.prestigecashbonus-%obj.client.achievementcashbonus;
            if(%dropsMult < 1)
                %dropsMult = 1;
            %ore = rollCrateOre(1500);
            if(getfield($ore[%ore],12) $= "EVENT")
                %oreamount = getrandom(6,20);
            else
                %oreamount = mfloatlength(getrandom(6,20)*%dropsMult,0);
            if(getfield($ore[%ore],10) < 500 && getfield($ore[%ore],12) !$= "EVENT")
                %oreamount = %oreamount*17;
            else if(getfield($ore[%ore],10) < 1000 && getfield($ore[%ore],12) !$= "EVENT")
                %oreamount = %oreamount*6;
            if(!%crate)
                announcemessage("\c2" @ %obj.client.name SPC "\c3has opened a\c2 Tier-3 Crate\c3 and received\c5" SPC %oreamount @ "x" SPC getfield($ore[%ore],0) @ "!");
            %obj.client.inventory[getfield($ore[%ore],0)]+=%oreamount;
            if(%crate)
                return "\c2" @ %oreamount @ "x" SPC getfield($ore[%ore],0);
        }
    }
    else if(getfield($ore[%col.oreid],0) $= "Tier-4 Crate" || %crate == 4)
    {
        if(%drop == 1)
        {
            %money = mfloatlength(getrandom(8250,22500)*(1+%obj.client.prestigecratedrops+%obj.client.achievementcratedrops),0);
            %obj.client.addmoney(%money);
            if(!%crate)
                announcemessage("\c2" @ %obj.client.name SPC "\c3has opened a\c1 Tier-4 Crate\c3 and received\c5" SPC %money @ "$!");
            if(%crate)
                return "\c1" @ %money @ "$";
        }
        else if(%drop == 2)
        {
            %exp = mfloatlength(getrandom(12500,47500)*(1+%obj.client.prestigecratedrops+%obj.client.achievementcratedrops),0);
            %obj.client.addexp(%exp,1);
            if(!%crate)
                announcemessage("\c2" @ %obj.client.name SPC "\c3has opened a\c1 Tier-4 Crate\c3 and received\c5" SPC %exp SPC "EXP!");
            if(%crate)
                return "\c1" @ %exp SPC "EXP";
        }
        else if(%drop == 3)
        {
            %dropsMult = 1+%obj.client.prestigecratedrops+%obj.client.achievementcratedrops-%obj.client.prestigecashbonus-%obj.client.achievementcashbonus;
            if(%dropsMult < 1)
                %dropsMult = 1;
            %ore = rollCrateOre(2250);
            if(getfield($ore[%ore],12) $= "EVENT")
                %oreamount = getrandom(7,21);
            else
                %oreamount = mfloatlength(getrandom(7,21)*%dropsMult,0);
            if(getfield($ore[%ore],10) < 500 && getfield($ore[%ore],12) !$= "EVENT")
                %oreamount = %oreamount*52;
            else if(getfield($ore[%ore],10) < 1000 && getfield($ore[%ore],12) !$= "EVENT")
                %oreamount = %oreamount*21;
            else if(getfield($ore[%ore],10) < 1500 && getfield($ore[%ore],12) !$= "EVENT")
                %oreamount = %oreamount*7;
            if(!%crate)
                announcemessage("\c2" @ %obj.client.name SPC "\c3has opened a\c1 Tier-4 Crate\c3 and received\c5" SPC %oreamount @ "x" SPC getfield($ore[%ore],0) @ "!");
            %obj.client.inventory[getfield($ore[%ore],0)]+=%oreamount;
            if(%crate)
                return "\c1" @ %oreamount @ "x" SPC getfield($ore[%ore],0);
        }
    }
    else if(getfield($ore[%col.oreid],0) $= "Tier-5 Crate" || %crate == 5)
    {
        if(%drop == 1)
        {
            %money = mfloatlength(getrandom(15000,42500)*(1+%obj.client.prestigecratedrops+%obj.client.achievementcratedrops),0);
            %obj.client.addmoney(%money);
            if(!%crate)
                announcemessage("\c2" @ %obj.client.name SPC "\c3has opened a\c5 Tier-5 Crate\c3 and received\c5" SPC %money @ "$!");
            if(%crate)
                return "\c5" @ %money @ "$";
        }
        else if(%drop == 2)
        {
            %exp = mfloatlength(getrandom(22500,67500)*(1+%obj.client.prestigecratedrops+%obj.client.achievementcratedrops),0);
            %obj.client.addexp(%exp,1);
            if(!%crate)
                announcemessage("\c2" @ %obj.client.name SPC "\c3has opened a\c5 Tier-5 Crate\c3 and received\c5" SPC %exp SPC "EXP!");
            if(%crate)
                return "\c5" @ %exp SPC "EXP";
        }
        else if(%drop == 3)
        {
            %dropsMult = 1+%obj.client.prestigecratedrops+%obj.client.achievementcratedrops-%obj.client.prestigecashbonus-%obj.client.achievementcashbonus;
            if(%dropsMult < 1)
                %dropsMult = 1;
            %ore = rollCrateOre(3000);
            if(getfield($ore[%ore],12) $= "EVENT")
                %oreamount = getrandom(7,22);
            else
                %oreamount = mfloatlength(getrandom(7,22)*%dropsMult,0);
            if(getfield($ore[%ore],10) < 500 && getfield($ore[%ore],12) !$= "EVENT")
                %oreamount = %oreamount*75;
            else if(getfield($ore[%ore],10) < 1000 && getfield($ore[%ore],12) !$= "EVENT")
                %oreamount = %oreamount*32;
            else if(getfield($ore[%ore],10) < 1500 && getfield($ore[%ore],12) !$= "EVENT")
                %oreamount = %oreamount*14;
            else if(getfield($ore[%ore],10) < 2250 && getfield($ore[%ore],12) !$= "EVENT")
                %oreamount = %oreamount*7;
            if(!%crate)
                announcemessage("\c2" @ %obj.client.name SPC "\c3has opened a\c5 Tier-5 Crate\c3 and received\c5" SPC %oreamount @ "x" SPC getfield($ore[%ore],0) @ "!");
            %obj.client.inventory[getfield($ore[%ore],0)]+=%oreamount;
            if(%crate)
                return "\c5" @ %oreamount @ "x" SPC getfield($ore[%ore],0);
        }
    }
    else if(getfield($ore[%col.oreid],0) $= "Tier-6 Crate" || %crate == 6)
    {
        if(%drop == 1)
        {
            %money = mfloatlength(getrandom(35000,125000)*(1+%obj.client.prestigecratedrops+%obj.client.achievementcratedrops),0);
            %obj.client.addmoney(%money);
            if(!%crate)
                announcemessage("\c2" @ %obj.client.name SPC "\c3has opened a<color:FF4500> Tier-6 Crate\c3 and received\c5" SPC %money @ "$!");
            if(%crate)
                return "<color:FF4500>" @ %money @ "$";
        }
        else if(%drop == 2)
        {
            %exp = mfloatlength(getrandom(75000,250000)*(1+%obj.client.prestigecratedrops+%obj.client.achievementcratedrops),0);
            %obj.client.addexp(%exp,1);
            if(!%crate)
                announcemessage("\c2" @ %obj.client.name SPC "\c3has opened a<color:FF4500> Tier-6 Crate\c3 and received\c5" SPC %exp SPC "EXP!");
            if(%crate)
                return "<color:FF4500>" @ %exp SPC "EXP";
        }
        else if(%drop == 3)
        {
            %dropsMult = 1+%obj.client.prestigecratedrops+%obj.client.achievementcratedrops-%obj.client.prestigecashbonus-%obj.client.achievementcashbonus;
            if(%dropsMult < 1)
                %dropsMult = 1;
            %ore = rollCrateOre(4000);
            if(getfield($ore[%ore],12) $= "EVENT")
                %oreamount = getrandom(8,22);
            else
                %oreamount = mfloatlength(getrandom(8,22)*%dropsMult,0);
            if(getfield($ore[%ore],10) < 500 && getfield($ore[%ore],12) !$= "EVENT")
                %oreamount = %oreamount*155;
            else if(getfield($ore[%ore],10) < 1000 && getfield($ore[%ore],12) !$= "EVENT")
                %oreamount = %oreamount*52;
            else if(getfield($ore[%ore],10) < 1500 && getfield($ore[%ore],12) !$= "EVENT")
                %oreamount = %oreamount*18;
            else if(getfield($ore[%ore],10) < 2250 && getfield($ore[%ore],12) !$= "EVENT")
                %oreamount = %oreamount*12;
            else if(getfield($ore[%ore],10) < 3000 && getfield($ore[%ore],12) !$= "EVENT")
                %oreamount = %oreamount*7;
            if(%ore $= "Moonstone")
                %oreamount /= 7;
            if(!%crate)
                announcemessage("\c2" @ %obj.client.name SPC "\c3has opened a<color:FF4500> Tier-6 Crate\c3 and received\c5" SPC %oreamount @ "x" SPC getfield($ore[%ore],0) @ "!");
            %obj.client.inventory[getfield($ore[%ore],0)]+=%oreamount;
            if(%crate)
                return "<color:FF4500>" @ %oreamount @ "x" SPC getfield($ore[%ore],0);
        }
    }
    else if(getfield($ore[%col.oreid],0) $= "Tier-7 Crate" || %crate == 7)
    {
        if(%drop == 1)
        {
            %money = mfloatlength(getrandom(200000,600000)*(1+%obj.client.prestigecratedrops+%obj.client.achievementcratedrops),0);
            %obj.client.addmoney(%money);
            if(!%crate)
                announcemessage("\c2" @ %obj.client.name SPC "\c3has opened a\c0 Tier-7 Crate\c3 and received\c5" SPC %money @ "$!");
            if(%crate)
                return "\c0" @ %money @ "$";
        }
        else if(%drop == 2)
        {
            %exp = mfloatlength(getrandom(325000,925000)*(1+%obj.client.prestigecratedrops+%obj.client.achievementcratedrops),0);
            %obj.client.addexp(%exp,1);
            if(!%crate)
                announcemessage("\c2" @ %obj.client.name SPC "\c3has opened a\c0 Tier-7 Crate\c3 and received\c5" SPC %exp SPC "EXP!");
            if(%crate)
                return "\c0" @ %exp SPC "EXP";
        }
        else if(%drop == 3)
        {
            %dropsMult = 1+%obj.client.prestigecratedrops+%obj.client.achievementcratedrops-%obj.client.prestigecashbonus-%obj.client.achievementcashbonus;
            if(%dropsMult < 1)
                %dropsMult = 1;
            %ore = rollCrateOre(5000);
            if(getfield($ore[%ore],12) $= "EVENT")
                %oreamount = getrandom(9,23);
            else
                %oreamount = mfloatlength(getrandom(9,23)*%dropsMult,0);
            if(getfield($ore[%ore],10) < 500 && getfield($ore[%ore],12) !$= "EVENT")
                %oreamount = %oreamount*422;
            else if(getfield($ore[%ore],10) < 1000 && getfield($ore[%ore],12) !$= "EVENT")
                %oreamount = %oreamount*175;
            else if(getfield($ore[%ore],10) < 1500 && getfield($ore[%ore],12) !$= "EVENT")
                %oreamount = %oreamount*60;
            else if(getfield($ore[%ore],10) < 2250 && getfield($ore[%ore],12) !$= "EVENT")
                %oreamount = %oreamount*36;
            else if(getfield($ore[%ore],10) < 3000 && getfield($ore[%ore],12) !$= "EVENT")
                %oreamount = %oreamount*14;
            else if(getfield($ore[%ore],10) < 4000 && getfield($ore[%ore],12) !$= "EVENT")
                %oreamount = %oreamount*4;
            if(%ore $= "Moonstone")
                %oreamount = mfloatlength(%oreamount/7,0);
            else if(%ore $= "Sunstone")
                %oreamount = mfloatlength(%oreamount/8,0);
            if(!%crate)
                announcemessage("\c2" @ %obj.client.name SPC "\c3has opened a\c0 Tier-7 Crate\c3 and received\c5" SPC %oreamount @ "x" SPC getfield($ore[%ore],0) @ "!");
            %obj.client.inventory[getfield($ore[%ore],0)]+=%oreamount;
            if(%crate)
                return "\c0" @ %oreamount @ "x" SPC getfield($ore[%ore],0);
        }
    }
    else if(getfield($ore[%col.oreid],0) $= "Tier-8 Crate" || %crate == 8)
    {
        if(%drop == 1)
        {
            %money = mfloatlength(getrandom(750000,3750000)*(1+%obj.client.prestigecratedrops+%obj.client.achievementcratedrops),0);
            %obj.client.addmoney(%money);
            if(!%crate)
                announcemessage("\c2" @ %obj.client.name SPC "\c3has opened a<color:4B0082> Tier-8 Crate\c3 and received\c5" SPC %money @ "$!");
            if(%crate)
                return "<color:4B0082>" @ %money @ "$";
        }
        else if(%drop == 2)
        {
            %exp = mfloatlength(getrandom(1250000,7250000)*(1+%obj.client.prestigecratedrops+%obj.client.achievementcratedrops),0);
            %obj.client.addexp(%exp,1);
            if(!%crate)
                announcemessage("\c2" @ %obj.client.name SPC "\c3has opened a<color:4B0082> Tier-8 Crate\c3 and received\c5" SPC %exp SPC "EXP!");
            if(%crate)
                return "<color:4B0082>" @ %exp SPC "EXP";
        }
        else if(%drop == 3)
        {
            %dropsMult = 1+%obj.client.prestigecratedrops+%obj.client.achievementcratedrops-%obj.client.prestigecashbonus-%obj.client.achievementcashbonus;
            if(%dropsMult < 1)
                %dropsMult = 1;
            %ore = rollCrateOre(6250);
            if(getfield($ore[%ore],12) $= "EVENT")
                %oreamount = getrandom(10,24);
            else
                %oreamount = mfloatlength(getrandom(10,24)*%dropsMult,0);
            if(getfield($ore[%ore],10) < 500 && getfield($ore[%ore],12) !$= "EVENT")
                %oreamount = %oreamount*1126;
            else if(getfield($ore[%ore],10) < 1000 && getfield($ore[%ore],12) !$= "EVENT")
                %oreamount = %oreamount*422;
            else if(getfield($ore[%ore],10) < 1500 && getfield($ore[%ore],12) !$= "EVENT")
                %oreamount = %oreamount*175;
            else if(getfield($ore[%ore],10) < 2250 && getfield($ore[%ore],12) !$= "EVENT")
                %oreamount = %oreamount*60;
            else if(getfield($ore[%ore],10) < 3000 && getfield($ore[%ore],12) !$= "EVENT")
                %oreamount = %oreamount*36;
            else if(getfield($ore[%ore],10) < 4000 && getfield($ore[%ore],12) !$= "EVENT")
                %oreamount = %oreamount*14;
            else if(getfield($ore[%ore],10) < 5000 && getfield($ore[%ore],12) !$= "EVENT")
                %oreamount = %oreamount*4;
            if(%ore $= "Moonstone")
                %oreamount = mfloatlength(%oreamount/7,0);
            else if(%ore $= "Sunstone")
                %oreamount = mfloatlength(%oreamount/8,0);
            else if(%ore $= "Antimatter")
                %oreamount = mfloatlength(%oreamount/9,0);
            else if(%ore $= "Darkmatter")
                %oreamount = mfloatlength(%oreamount/10,0);
            else if(%ore $= "Void Gem")
                %oreamount = mfloatlength(%oreamount/10,0);
            if(!%crate)
                announcemessage("\c2" @ %obj.client.name SPC "\c3has opened a<color:4B0082> Tier-8 Crate\c3 and received\c5" SPC %oreamount @ "x" SPC getfield($ore[%ore],0) @ "!");
            %obj.client.inventory[getfield($ore[%ore],0)]+=%oreamount;
            if(%crate)
                return "<color:4B0082>" @ %oreamount @ "x" SPC getfield($ore[%ore],0);
        }
    }
}

function swingPickaxe(%obj, %pickaxetype)
{
    %ray = containerRaycast(%obj.geteyepoint(), vectoradd(%obj.geteyepoint(),vectorscale(%obj.geteyevector(),5)), $typemasks::fxbrickobjecttype, 0);
    if(firstword(%ray))
    {
        mineore(%obj, %ray);
        //%proj = new Projectile()
        //{
            //sourceObject = %obj;
            //initialPosition = getwords(%ray,1,3);
            //initialVelocity = "0 0 0";
            //sourceSlot = 0;
            //datablock = rpgpickaxeprojectile;
            //client = %obj.client;
        //};
        //%proj.explode();
        if(%ray.canmine && getfield($ore[%ray.oreid],4) <= %obj.client.level)
		{
			if(getfield($ore[%ray.oreid],0) $= "Grass" || getfield($ore[%ray.oreid],0) $= "Dirt" || getfield($ore[%ray.oreid],0) $= "Dense Dirt" || getfield($ore[%ray.oreid],12) $= "CRATE" || getfield($ore[%ray.oreid],12) $= "TREASURECHEST" || getfield($ore[%ray.oreid],12) $= "LAVA")
                serverplay3d(dig_ @ getrandom(0,2), getwords(%ray,1,3));
            else
                serverplay3d(tink_ @ getrandom(0,2), getwords(%ray,1,3));
		}
        //else if(!%ray.canmine)
            //serverplay3d(swordhitsound, getwords(%ray,1,3));
    }
    pickaxeDamageRay(%obj.geteyepoint(), vectoradd(%obj.geteyepoint(),vectorscale(%obj.geteyevector(),3)), %obj, %pickaxetype, %obj, 0);
}

function pickaxeDamageRay(%pos, %end, %ignore, %pickaxetype, %attacker, %crash)
{
    if(%crash >= 10)
        return;
    %ray = containerRaycast(%pos, %end, $typemasks::fxbrickobjecttype, %ignore);
    if(firstword(%ray))
        return;
    %ray = containerRaycast(%pos, %end, $typemasks::playerobjecttype, %ignore);
    if(firstword(%ray))
    {
        if(getword(%ray,0).getclassname() $= "Player" || getword(%ray,0).getdatablock().getname() $= "mininghelmetplayer" || getword(%ray,0).getdatablock().getname() $= "healthplayer")
        {
            %crash+=1;
            pickaxeDamageRay(%pos, %end, %ray, %pickaxetype, %attacker, %crash);
            return;
        }
        if(%ray.miningai)
        {
            %damage = mfloatlength(stripchars(%pickaxetype, "rpgPickaxeImage") * 7.5,0);
            if(%pickaxetype $= "rpgpickaxeimage")
                %damage = 7.5;
            %ray.damage(%attacker, getwords(%ray,1,3), %damage, $damagetype::default);
        }
    }
}

function announcemessage(%msg)
{
    for(%i=0;%i<clientgroup.getcount();%i++)
    {
        clientgroup.getobject(%i).chatmessage(%msg);
    }
}

function servercmdinv(%client, %ore, %extra, %extra2)
{
    servercmdinventory(%client, %ore, %extra, %extra2);
}

function servercmdinventory(%client, %ore, %extra, %extra2)
{
    if(%extra !$= "" && %extra2 $= "")
        %ore = %ore SPC %extra;
    else if(%extra !$= "" && %extra2 !$= "")
        %ore = %ore SPC %extra SPC %extra2;
    if(%ore $= "")
    {
        for(%i = 0; %i < $orecount; %i++)
        {
            if(%client.inventory[getfield($ore[%i],0)] > 0 && getfield($ore[%i],getfieldcount($ore[%i])-1) !$= "MATERIAL")
            {
                %color = convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[%i],5)),0) * 255));
			    %color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[%i],5)),1) * 255));
			    %color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[%i],5)),2) * 255));
                %color = "<color:" @ %color @ ">";
                %client.chatmessage(%color @ getfield($ore[%i],0) SPC "\c6-" SPC %client.inventory[getfield($ore[%i],0)] @ "x" SPC "-\c2" SPC (getfield($ore[%i],2)*%client.inventory[getfield($ore[%i],0)])*(1+%client.prestigecashbonus)*(1+%client.achievementcashbonus) @ "$");
                if(getfield($ore[%i],12) $= "EVENT")
                    %totaleventmoney+=(getfield($ore[%i],2)*%client.inventory[getfield($ore[%i],0)])*(1+%client.prestigecashbonus)*(1+%client.achievementcashbonus);
                else
                    %totalmoney+=(getfield($ore[%i],2)*%client.inventory[getfield($ore[%i],0)])*(1+%client.prestigecashbonus)*(1+%client.achievementcashbonus);
            }
        }
        for(%i = 0; %i < $advancedItems; %i++)
        {
            if(%client.inventory[getfield($advancedItem[%i],0)] > 0)
            {
                %client.chatmessage(getfield($advancedItem[%i],2) @ getfield($advancedItem[%i],0) SPC "\c6-" SPC %client.inventory[getfield($advancedItem[%i],0)] @ "x" SPC "-\c2" SPC (getfield($advancedItem[%i],1)*%client.inventory[getfield($advancedItem[%i],0)])*(1+%client.prestigecashbonus)*(1+%client.achievementcashbonus) @ "$");
                %totalmaterialmoney+=(getfield($advancedItem[%i],1)*%client.inventory[getfield($advancedItem[%i],0)])*(1+%client.prestigecashbonus)*(1+%client.achievementcashbonus);
            }
        }
        if(!%totalmoney)
            %totalmoney = 0;
        if(%totalmaterialmoney > 0)
            %materialTotal = " \c3(" @ %totalmaterialmoney @ "$ from crafted materials)";
        if(%totaleventmoney > 0)
            %eventTotal = " \c4(" @ %totaleventmoney @ "$ from EVENT ores)";
        %client.chatmessage("\c6Total cash:\c2" SPC %totalmoney @ "$" @ %materialTotal @ %eventTotal);
    }
    else if(%ore !$= "")
    {
        for(%i = 0; %i < $orecount; %i++)
        {
            if(%ore $= getfield($ore[%i],0))
            {
                %amount = %client.inventory[getfield($ore[%i],0)];
                if(!%amount)
                    %amount=0;
                %success = 1;
                %color = convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[%i],5)),0) * 255));
			    %color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[%i],5)),1) * 255));
			    %color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[%i],5)),2) * 255));
                %color = "<color:" @ %color @ ">";
                %client.chatmessage(%color @ getfield($ore[%i],0) SPC "\c6-" SPC %amount @ "x" SPC "-\c2" SPC getfield($ore[%i],2)*%client.inventory[getfield($ore[%i],0)]*(1+%client.prestigecashbonus)*(1+%client.achievementcashbonus) @ "$");
            }
        }
        if(!%success)
        {
            %client.chatmessage("\c0bro the ore you are trying to look for doesn't exist");
            %client.playsound(errorsound);
            return;
        } 
    }
}

function servercmdsell(%client, %ore, %quantity, %extra, %extra2)
{
    if(%extra !$= "" && %extra2 $= "")
    {
        %ore = %ore SPC %quantity;
        %quantity = %extra;
    }
    else if(%extra !$= "" && %extra2 !$= "")
    {
        %ore = %ore SPC %quantity SPC %extra;
        %quantity = %extra2;
    }
    if(vectordist(_shopkeeper.getposition(),%client.player.getposition()) > 15)
    {
        %client.chatmessage("\c0stand closer to the shopkeeper lol");
        %client.playsound(errorsound);
        return;
    }
    if(%ore $= "")
    {
        %client.chatmessage("\c0bro what ore are you trying to sell");
        %client.playsound(errorsound);
        return;
    }
    if(%ore $= "all" && %quantity !$= "")
    {
        %client.chatmessage("\c0are you sure you are not trying to sell all ores by accident");
        %client.playsound(errorsound);
        return;
    }
    if(%ore $= "all" && %quantity $= "")
    {
        for(%i = 0; %i < $orecount; %i++)
        {
            if(getfield($ore[%i],12) $= "EVENT")
                continue;
            if(getfield(%client.oreLock[getfield($ore[%i],0)],1))
                continue;
            if(%client.inventory[getfield($ore[%i],0)] > 0)
            {
                %totalmoney+=(%client.inventory[getfield($ore[%i],0)]*getfield($ore[%i],2))*(1+%client.prestigecashbonus)*(1+%client.achievementcashbonus);
                %client.inventory[getfield($ore[%i],0)] = 0;
            }
        }
        if(!%totalmoney)
        {
            %client.chatmessage("\c0you don't have ores to sell bruh");
            %client.playsound(errorsound);
            return;
        }
        %client.addmoney(%totalmoney);
        %client.chatmessage("\c6Sold all of your ores for\c2" SPC %totalmoney @ "$");
        %client.playsound(beep_key_sound);
        return;
    }
    for(%i = 0; %i < $orecount; %i++)
    {
        if(%ore $= getfield($ore[%i],0))
        {
            if(!%quantity && %quantity !$= "all")
            {
                %client.chatmessage("\c0bro you didn't specify the amount");
                %client.playsound(errorsound);
                return; 
            }
            if(%quantity $= "all")
            {
                %quantity = %client.inventory[getfield($ore[%i],0)];
            }
            if(%quantity > %client.inventory[getfield($ore[%i],0)])
            {
                %quantity = %client.inventory[getfield($ore[%i],0)];
            }
            if(getfield($ore[%i],4) > %client.level)
            {
                %client.chatmessage("\c0cannot sell ores that have a level requirement above your level");
                %client.playsound(errorsound);
                return; 
            }
            %color = convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[%i],5)),0) * 255));
			%color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[%i],5)),1) * 255));
			%color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[%i],5)),2) * 255));
            %color = "<color:" @ %color @ ">";
            %client.chatmessage("\c6Sold" SPC %color @ %quantity @ "x" SPC getfield($ore[%i],0) SPC "\c6for\c2" SPC (%quantity*getfield($ore[%i],2))*(1+%client.prestigecashbonus)*(1+%client.achievementcashbonus) @ "$");
            %client.addmoney((%quantity*getfield($ore[%i],2))*(1+%client.prestigecashbonus)*(1+%client.achievementcashbonus));
            %client.inventory[getfield($ore[%i],0)]-=%quantity;
            %client.playsound(beep_key_sound);
            return;
        }
        else if(%ore $= getfield($advanceditem[%i],0))
        {
            if(!%quantity && %quantity !$= "all")
            {
                %client.chatmessage("\c0bro you didn't specify the amount");
                %client.playsound(errorsound);
                return; 
            }
            if(%quantity $= "all")
            {
                %quantity = %client.inventory[getfield($advanceditem[%i],0)];
            }
            if(%quantity > %client.inventory[getfield($advanceditem[%i],0)])
            {
                %quantity = %client.inventory[getfield($advanceditem[%i],0)];
            }
            %client.chatmessage("\c6Sold" SPC getfield($advanceditem[%i],2) @ %quantity @ "x" SPC getfield($advanceditem[%i],0) SPC "\c6for\c2" SPC (%quantity*getfield($advanceditem[%i],1))*(1+%client.prestigecashbonus)*(1+%client.achievementcashbonus) @ "$");
            %client.addmoney((%quantity*getfield($advanceditem[%i],1))*(1+%client.prestigecashbonus)*(1+%client.achievementcashbonus));
            %client.inventory[getfield($advanceditem[%i],0)]-=%quantity;
            %client.playsound(beep_key_sound);
            return;
        }
    }
}

function servercmdlock(%client, %ore, %ore2, %ore3)
{
    if(%ore2 !$= "" && %ore3 $= "")
        %ore = %ore SPC %ore2;
    else if(%ore2 !$= "" && %ore3 !$= "")
        %ore = %ore SPC %ore2 SPC %ore3;
    if(oreidfromname(%ore) != -1)
    {
        if(getfield(%client.oreLock[getfield($ore[oreidfromname(%ore)],0)],1) == 0)
        {
            %color = convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[oreidfromname(%ore)],5)),0) * 255));
            %color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[oreidfromname(%ore)],5)),1) * 255));
            %color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[oreidfromname(%ore)],5)),2) * 255));
            %color = "<color:" @ %color @ ">";
            %client.chatmessage("\c6Successfully locked" @ %color SPC getfield($ore[oreidfromname(%ore)],0) SPC "\c6from being sold!");
            %client.oreLock[getfield($ore[oreidfromname(%ore)],0)] = getfield($ore[oreidfromname(%ore)],0) TAB 1;
            %client.updateorelock();
            %client.playsound(beep_key_sound);
        }
        else
        {
            %color = convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[oreidfromname(%ore)],5)),0) * 255));
            %color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[oreidfromname(%ore)],5)),1) * 255));
            %color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[oreidfromname(%ore)],5)),2) * 255));
            %color = "<color:" @ %color @ ">";
            %client.chatmessage("\c6Successfully unlocked" @ %color SPC getfield($ore[oreidfromname(%ore)],0) SPC "\c6from being sold!");
            %client.oreLock[getfield($ore[oreidfromname(%ore)],0)] = "";
            %client.updateorelock();
            %client.playsound(beep_key_sound);
        }
    }
}

function servercmdlistlock(%client)
{
    for(%i = 0; %i < $orecount; %i++)
    {
        if(getfield(%client.orelock[getfield($ore[%i],0)],1))
        {
            %color = convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[%i],5)),0) * 255));
            %color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[%i],5)),1) * 255));
            %color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield($ore[%i],5)),2) * 255));
            %color = "<color:" @ %color @ ">";
            %client.chatmessage("\c6Locked: " @ %color @ getfield($ore[%i],0));
        }
    }
    %client.playsound(beep_key_sound);
}

function servercmdunlockall(%client)
{
    for(%i = 0; %i < $orecount; %i++)
    {
        if(getfield(%client.orelock[getfield($ore[%i],0)],1))
        {
            %client.orelock[getfield($ore[%i],0)] = "";
        }
    }
}

function gameconnection::updateorelock(%client)
{
    %fw = new FileObject();
    %fw.openForWrite($directory @ %client.getblid() @ "/oreLock.txt");
	for(%i = 0; %i < $orecount; %i++)
    {
        if(getfield(%client.orelock[getfield($ore[%i],0)],1) == 1)
            %fw.writeline(getfield($ore[%i],0) TAB 1);
    }
	%fw.close();
	%fw.delete();
}

function gameconnection::readorelock(%client)
{
    %fw = new FileObject();
    %fw.openForRead($directory @ %client.getblid() @ "/oreLock.txt");
	for(%i = 0; %i < $orecount; %i++)
    {
        %line = %fw.readline();
        %client.orelock[getfield(%line,0)] = getfield(%line,0) TAB getfield(%line,1);
    }
	%fw.close();
	%fw.delete();
}

function servercmdupgradedepth(%client)
{
    if(vectordist(_miningmaster.getposition(),%client.player.getposition()) > 15)
    {
        %client.chatmessage("\c0stand closer to the mining master lol");
        %client.playsound(errorsound);
        return;
    }
    if(%client.optimaldepth == 100)
    {
        %gold = getfield($depthPurchase[0],1);
        %mats = getfields($depthPurchase[0],2,getfieldcount($depthPurchase[0]));
        %ingredients = "<color:fff000>Cash Required:" SPC %gold NL "<color:00ffff>" @ %mats;
        %client.promptclient(1, "<shadow:-1.5:-1.5><shadowcolor:111111><font:palatino linotype:28><color:111111>200m Depth Upgrade" NL "<shadow:0:0><font:Verdana Bold:14>" SPC %ingredients, "0", %client, 2);
    }
    else if(%client.optimaldepth == 200)
    {
        %gold = getfield($depthPurchase[1],1);
        %mats = getfields($depthPurchase[1],2,getfieldcount($depthPurchase[1]));
        %ingredients = "<color:fff000>Cash Required:" SPC %gold NL "<color:00ffff>" @ %mats;
        %client.promptclient(1, "<shadow:-1.5:-1.5><shadowcolor:111111><font:palatino linotype:28><color:111111>300m Depth Upgrade" NL "<shadow:0:0><font:Verdana Bold:14>" SPC %ingredients, "0", %client, 2);
    }
    else if(%client.optimaldepth == 300)
    {
        %gold = getfield($depthPurchase[2],1);
        %mats = getfields($depthPurchase[2],2,getfieldcount($depthPurchase[1]));
        %ingredients = "<color:fff000>Cash Required:" SPC %gold NL "<color:00ffff>" @ %mats;
        %client.promptclient(1, "<shadow:-1.5:-1.5><shadowcolor:111111><font:palatino linotype:28><color:111111>400m Depth Upgrade" NL "<shadow:0:0><font:Verdana Bold:14>" SPC %ingredients, "0", %client, 2);
    }
    else if(%client.optimaldepth == 400)
    {
        %gold = getfield($depthPurchase[3],1);
        %mats = getfields($depthPurchase[3],2,getfieldcount($depthPurchase[3]));
        %ingredients = "<color:fff000>Cash Required:" SPC %gold NL "<color:00ffff>" @ %mats;
        %client.promptclient(1, "<shadow:-1.5:-1.5><shadowcolor:111111><font:palatino linotype:28><color:111111>600m Depth Upgrade" NL "<shadow:0:0><font:Verdana Bold:14>" SPC %ingredients, "0", %client, 2);
    }
    else if(%client.optimaldepth == 600)
    {
        %gold = getfield($depthPurchase[4],1);
        %mats = getfields($depthPurchase[4],2,getfieldcount($depthPurchase[4]));
        %ingredients = "<color:fff000>Cash Required:" SPC %gold NL "<color:00ffff>" @ %mats;
        %client.promptclient(1, "<shadow:-1.5:-1.5><shadowcolor:111111><font:palatino linotype:28><color:111111>750m Depth Upgrade" NL "<shadow:0:0><font:Verdana Bold:14>" SPC %ingredients, "0", %client, 2);
    }
    else if(%client.optimaldepth == 750)
    {
        %gold = getfield($depthPurchase[5],1);
        %mats = getfields($depthPurchase[5],2,getfieldcount($depthPurchase[5]));
        %ingredients = "<color:fff000>Cash Required:" SPC %gold NL "<color:00ffff>" @ %mats;
        %client.promptclient(1, "<shadow:-1.5:-1.5><shadowcolor:111111><font:palatino linotype:28><color:111111>1000m Depth Upgrade" NL "<shadow:0:0><font:Verdana Bold:14>" SPC %ingredients, "0", %client, 2);
    }
    else if(%client.optimaldepth == 1000)
    {
        %gold = getfield($depthPurchase[6],1);
        %mats = getfields($depthPurchase[6],2,getfieldcount($depthPurchase[6]));
        %ingredients = "<color:fff000>Cash Required:" SPC %gold NL "<color:00ffff>" @ %mats;
        %client.promptclient(1, "<shadow:-1.5:-1.5><shadowcolor:111111><font:palatino linotype:28><color:111111>1250m Depth Upgrade" NL "<shadow:0:0><font:Verdana Bold:14>" SPC %ingredients, "0", %client, 2);
    }
    else if(%client.optimaldepth == 1250)
    {
        %gold = getfield($depthPurchase[7],1);
        %mats = getfields($depthPurchase[7],2,getfieldcount($depthPurchase[7]));
        %ingredients = "<color:fff000>Cash Required:" SPC %gold NL "<color:00ffff>" @ %mats;
        %client.promptclient(1, "<shadow:-1.5:-1.5><shadowcolor:111111><font:palatino linotype:28><color:111111>1500m Depth Upgrade" NL "<shadow:0:0><font:Verdana Bold:14>" SPC %ingredients, "0", %client, 2);
    }
    else if(%client.optimaldepth == 1500)
    {
        %gold = getfield($depthPurchase[8],1);
        %mats = getfields($depthPurchase[8],2,getfieldcount($depthPurchase[8]));
        %ingredients = "<color:fff000>Cash Required:" SPC %gold NL "<color:00ffff>" @ %mats;
        %client.promptclient(1, "<shadow:-1.5:-1.5><shadowcolor:111111><font:palatino linotype:28><color:111111>2000m Depth Upgrade" NL "<shadow:0:0><font:Verdana Bold:14>" SPC %ingredients, "0", %client, 2);
    }
    else if(%client.optimaldepth == 2000)
    {
        %gold = getfield($depthPurchase[9],1);
        %mats = getfields($depthPurchase[9],2,getfieldcount($depthPurchase[9]));
        %ingredients = "<color:fff000>Cash Required:" SPC %gold NL "<color:00ffff>" @ %mats;
        %client.promptclient(1, "<shadow:-1.5:-1.5><shadowcolor:111111><font:palatino linotype:28><color:111111>2500m Depth Upgrade" NL "<shadow:0:0><font:Verdana Bold:14>" SPC %ingredients, "0", %client, 2);
    }
    else if(%client.optimaldepth == 2500)
    {
        %gold = getfield($depthPurchase[10],1);
        %mats = getfields($depthPurchase[10],2,getfieldcount($depthPurchase[10]));
        %ingredients = "<color:fff000>Cash Required:" SPC %gold NL "<color:00ffff>" @ %mats;
        %client.promptclient(1, "<shadow:-1.5:-1.5><shadowcolor:111111><font:palatino linotype:28><color:111111>3000m Depth Upgrade" NL "<shadow:0:0><font:Verdana Bold:14>" SPC %ingredients, "0", %client, 2);
    }
    else if(%client.optimaldepth == 3000)
    {
        %gold = getfield($depthPurchase[11],1);
        %mats = getfields($depthPurchase[11],2,getfieldcount($depthPurchase[11]));
        %ingredients = "<color:fff000>Cash Required:" SPC %gold NL "<color:00ffff>" @ %mats;
        %client.promptclient(1, "<shadow:-1.5:-1.5><shadowcolor:111111><font:palatino linotype:28><color:111111>3500m Depth Upgrade" NL "<shadow:0:0><font:Verdana Bold:14>" SPC %ingredients, "0", %client, 2);
    }
    else if(%client.optimaldepth == 3500)
    {
        %gold = getfield($depthPurchase[12],1);
        %mats = getfields($depthPurchase[12],2,getfieldcount($depthPurchase[12]));
        %ingredients = "<color:fff000>Cash Required:" SPC %gold NL "<color:00ffff>" @ %mats;
        %client.promptclient(1, "<shadow:-1.5:-1.5><shadowcolor:111111><font:palatino linotype:28><color:111111>4000m Depth Upgrade" NL "<shadow:0:0><font:Verdana Bold:14>" SPC %ingredients, "0", %client, 2);
    }
    else if(%client.optimaldepth == 4000)
    {
        %gold = getfield($depthPurchase[13],1);
        %mats = getfields($depthPurchase[13],2,getfieldcount($depthPurchase[13]));
        %ingredients = "<color:fff000>Cash Required:" SPC %gold NL "<color:00ffff>" @ %mats;
        %client.promptclient(1, "<shadow:-1.5:-1.5><shadowcolor:111111><font:palatino linotype:28><color:111111>4500m Depth Upgrade" NL "<shadow:0:0><font:Verdana Bold:14>" SPC %ingredients, "0", %client, 2);
    }
    else if(%client.optimaldepth == 4500)
    {
        %gold = getfield($depthPurchase[14],1);
        %mats = getfields($depthPurchase[14],2,getfieldcount($depthPurchase[14]));
        %ingredients = "<color:fff000>Cash Required:" SPC %gold NL "<color:00ffff>" @ %mats;
        %client.promptclient(1, "<shadow:-1.5:-1.5><shadowcolor:111111><font:palatino linotype:28><color:111111>5000m Depth Upgrade" NL "<shadow:0:0><font:Verdana Bold:14>" SPC %ingredients, "0", %client, 2);
    }
    else if(%client.optimaldepth == 5000)
    {
        %gold = getfield($depthPurchase[15],1);
        %mats = getfields($depthPurchase[15],2,getfieldcount($depthPurchase[15]));
        %ingredients = "<color:fff000>Cash Required:" SPC %gold NL "<color:00ffff>" @ %mats;
        %client.promptclient(1, "<shadow:-1.5:-1.5><shadowcolor:111111><font:palatino linotype:28>5500m Depth Upgrade" NL "<shadow:0:0><font:Verdana Bold:14>" SPC %ingredients, "0", %client, 2);
    }
    else if(%client.optimaldepth == 5500)
    {
        %gold = getfield($depthPurchase[16],1);
        %mats = getfields($depthPurchase[16],2,getfieldcount($depthPurchase[16]));
        %ingredients = "<color:fff000>Cash Required:" SPC %gold NL "<color:00ffff>" @ %mats;
        %client.promptclient(1, "<shadow:-1.5:-1.5><shadowcolor:111111><font:palatino linotype:28>6000m Depth Upgrade" NL "<shadow:0:0><font:Verdana Bold:14>" SPC %ingredients, "0", %client, 2);
    }
    else if(%client.optimaldepth == 6000)
    {
        %gold = getfield($depthPurchase[17],1);
        %mats = getfields($depthPurchase[17],2,getfieldcount($depthPurchase[17]));
        %ingredients = "<color:fff000>Cash Required:" SPC %gold NL "<color:00ffff>" @ %mats;
        %client.promptclient(1, "<shadow:-1.5:-1.5><shadowcolor:111111><font:palatino linotype:28>6500m Depth Upgrade" NL "<shadow:0:0><font:Verdana Bold:14>" SPC %ingredients, "0", %client, 2);
    }
}

function servercmdupgradeinv(%client)
{
    servercmdupgradeinventory(%client);
}

function servercmdupgradeinventory(%client)
{
    if(vectordist(_inventorymaster.getposition(),%client.player.getposition()) > 15)
    {
        %client.chatmessage("\c0stand closer to the inventory master lol");
        %client.playsound(errorsound);
        return;
    }
    if(%client.inventoryslots == 5 || !%client.inventoryslots)
    {
        %gold = getfield($inventoryPurchase[0],1);
        %mats = getfields($inventoryPurchase[0],2,getfieldcount($inventoryPurchase[0]));
        %ingredients = "<color:fff000>Cash Required:" SPC %gold NL "<color:00ffff>" @ %mats;
        %client.promptclient(1, "<shadow:-1.5:-1.5><shadowcolor:111111><font:palatino linotype:28><color:111111>6 Slots Upgrade" NL "<shadow:0:0><font:Verdana Bold:14>" SPC %ingredients, "0", %client, 3);
    }
    else if(%client.inventoryslots > 5)
    {
        %slots = %client.inventoryslots+1;
        %purchase = $inventoryPurchase[%client.inventoryslots-5];
        %gold = getfield($inventoryPurchase[%client.inventoryslots-5],1);
        %mats = getfields(%purchase,2,getfieldcount(%purchase));
        %ingredients = "<color:fff000>Cash Required:" SPC %gold NL "<color:00ffff>" @ %mats;
        %client.promptclient(1, "<shadow:-1.5:-1.5><shadowcolor:111111><font:palatino linotype:28><color:111111>" @ %slots SPC "Slots Upgrade" NL "<shadow:0:0><font:Verdana Bold:14>" SPC %ingredients, "0", %client, 3);
    }
}

function inventoryPurchaseIDfromName(%name)
{
    for(%i = 0; %i < $inventorypurchasecount; %i++)
    {
        if(getfield($inventorypurchase[%i],0) $= strreplace(%name, strchr(%name, " S"), ""))
        {
            return %i;
        }
    }
    return -1;
}

function depthPurchaseIDfromName(%name)
{
    for(%i = 0; %i < $depthpurchasecount; %i++)
    {
        if(getfield($depthpurchase[%i],0) $= strreplace(%name, strchr(%name, "m"), ""))
        {
            return %i;
        }
    }
    return -1;
}

function craftIDfromName(%name)
{
    for(%i = 0; %i < $craftcount; %i++)
    {
        if(getfield($craft[%i],0) $= %name)
        {
            return %i;
        }
    }
    return -1;
}

function servercmdinfo(%client, %ore, %ore2, %ore3)
{
    servercmdbestiary(%client, %ore, %ore2, %Ore3);
}

function servercmdbestiary(%client, %ore, %ore2, %ore3)
{
    if(%ore $= "")
    {
        %client.chatmessage("no ore specified bruh");
        %client.playsound(errorsound);
        return;
    }
    if(%ore !$= "")
        %name = %ore;
    if(%ore2 !$= "")
        %name = %name SPC %ore2;
    if(%ore3 !$= "")
        %name = %name SPC %ore3;
    %id = oreIDfromName(%name);
    if(%id == -1)
    {
        %id = craftIDfromName(%name);
        if(%id == -1)
        {
            %id = depthPurchaseIDfromName(%name);
            if(%id == -1)
            {
                %id = inventoryPurchaseIDfromName(%name);
                if(%id == -1)
                {
                    %client.chatmessage("that does not exist bruh");
                    %client.playsound(errorsound);
                    return;
                }
                else
                {
                    %inventorypurchase = $inventorypurchase[%id];
                    %client.chatmessage("\c6Craft Recipe:" SPC %color @ getfield(%inventorypurchase,0) SPC "Slots Upgarde");
                    %client.chatmessage("\c2Cash Requirement:" SPC getfield(%inventorypurchase,1));
                    for(%i = 0; %i < getfieldcount(%inventorypurchase)-2; %i++)
                    {
                        %client.chatmessage("\c3Ingredient" SPC %i+1 @ ":\c4" SPC getfield(%inventorypurchase,%i+2));
                    }
                    return;
                }
            }
            else
            {
                %depthpurchase = $depthpurchase[%id];
                %client.chatmessage("\c6Craft Recipe:" SPC %color @ getfield(%depthpurchase,0) @ "m Depth Upgrade");
                %client.chatmessage("\c2Cash Requirement:" SPC getfield(%depthpurchase,1));
                for(%i = 0; %i < getfieldcount(%depthpurchase)-2; %i++)
                {
                    %client.chatmessage("\c3Ingredient" SPC %i+1 @ ":\c4" SPC getfield(%depthpurchase,%i+2));
                }
                return;
            }
        }
        else
        {
            %craft = $craft[%id];
            %client.chatmessage("\c6Craft Recipe:" SPC %color @ getfield(%craft,0));
            %client.chatmessage("\c5Level Requirement:" SPC getfield(%craft,1));
            %client.chatmessage("\c2Cash Requirement:" SPC getfield(%craft,2));
            for(%i = 0; %i < getfieldcount(%craft)-4; %i++)
            {
                %client.chatmessage("\c3Ingredient" SPC %i+1 @ ":\c4" SPC getfield(%craft,%i+3));
            }
            return;
        }
    }
    %ore = $ore[%id];
    %color = convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield(%ore,5)),0) * 255));
	%color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield(%ore,5)),1) * 255));
	%color = %color @ convertRGBtoHex(mFloor(getWord(getColorIdTable(getfield(%ore,5)),2) * 255));
    %color = "<color:" @ %color @ ">";
    %client.chatmessage("\c6Bestiary Entry:" SPC %color @ getfield(%ore,0));
    %client.chatmessage("\c2Health:" SPC getfield(%ore,1) SPC "\c6-\c3 Value:" SPC getfield(%ore,2) SPC "\c6- \c4EXP:" SPC getfield(%ore,3));
    %client.chatmessage("\c0Level Requirement:" SPC getfield(%ore,4));
    %client.chatmessage("\c6Minimum Spawn Depth:" SPC getfield(%ore,10) @ "m");
    %max = getfield(%ore,11);
    if(%max !$= "UNLIMITED")
        %client.chatmessage("\c6Maximum Spawn Depth:" SPC getfield(%ore,11) @ "m");
    else
        %client.chatmessage("\c6Maximum Spawn Depth:" SPC getfield(%ore,11));
    %client.chatmessage("\c6Rarity:" SPC getrarity(%ore));
    if(getfield(%ore,0) $= "Tier-1 Crate")
        %client.chatmessage("\c6Crate Drops: \c3Cash: 75 - 375 \c6- \c4EXP: 100 - 500 \c6- \c5Ore Amount: 2 - 10");
    else if(getfield(%ore,0) $= "Tier-2 Crate")
        %client.chatmessage("\c6Crate Drops: \c3Cash: 375 - 1500 \c6- \c4EXP: 500 - 2000 \c6- \c5Ore Amount: 4 - 18");
    else if(getfield(%ore,0) $= "Tier-3 Crate")
        %client.chatmessage("\c6Crate Drops: \c3Cash: 5000 - 13750 \c6- \c4EXP: 7500 - 20000 \c6- \c5Ore Amount: 6 - 20");
    else if(getfield(%ore,0) $= "Tier-4 Crate")
        %client.chatmessage("\c6Crate Drops: \c3Cash: 8250 - 22500 \c6- \c4EXP: 12500 - 42500 \c6- \c5Ore Amount: 7 - 20");
    else if(getfield(%ore,0) $= "Tier-5 Crate")
        %client.chatmessage("\c6Crate Drops: \c3Cash: 15000 - 42500 \c6- \c4EXP: 22500 - 67500 \c6- \c5Ore Amount: 7 - 21");
    else if(getfield(%ore,0) $= "Tier-6 Crate")
        %client.chatmessage("\c6Crate Drops: \c3Cash: 35000 - 125000 \c6- \c4EXP: 75000 - 250000 \c6- \c5Ore Amount: 8 - 22");
    else if(getfield(%ore,0) $= "Tier-7 Crate")
        %client.chatmessage("\c6Crate Drops: \c3Cash: 200000 - 600000 \c6- \c4EXP: 325000 - 925000 \c6- \c5Ore Amount: 9 - 23");
    else if(getfield(%ore,0) $= "Tier-8 Crate")
        %client.chatmessage("\c6Crate Drops: \c3Cash: 750000 - 2500000 \c6- \c4EXP: 1250000 - 7250000 \c6- \c5Ore Amount: 10 - 24");
    if(getfield(%ore,0) $= "Tier-1 Crate Vault")
        %client.chatmessage("\c6Crate Vault Drops: \c5Crates: 2 - 8");
    else if(getfield(%ore,0) $= "Tier-2 Crate Vault")
        %client.chatmessage("\c6Crate Vault Drops: \c5Crates: 3 - 9");
    else if(getfield(%ore,0) $= "Tier-3 Crate Vault")
        %client.chatmessage("\c6Crate Vault Drops: \c5Crates: 4 - 10");
    else if(getfield(%ore,0) $= "Tier-4 Crate Vault")
        %client.chatmessage("\c6Crate Vault Drops: \c5Crates: 5 - 11");
}

function getRarity(%ore)
{
    if(getfield(%ore,9) $= "MYTHIC")
        return "<color:FF4500>Mythic";
    if(getfield(%ore,9) $= "LEGENDARY")
        return "<color:e6e6fa>Le<color:D8BFD8>ge<color:DDA0DD>nd<color:EE82EE>ar<color:DA70D6>y";
    if(getfield(%ore,9) $= "EXOTIC")
        return "<color:FF4500>Exotic";
    if(getfield(%ore,9) $= "INSANE")
        return "<color:000080>INSANE";
    if(getfield(%ore,9) >= 25)
        return "\c7Common";
    else if(getfield(%ore,9) >= 15)
        return "\c2Uncommon";
    else if(getfield(%ore,9) >= 10)
        return "\c1Rare";
    else if(getfield(%ore,9) >= 5)
        return "\c4Very Rare";
    else if(getfield(%ore,9) >= 3)
        return "\c3Exceptionally Rare";
    else if(getfield(%ore,9) >= 2)
        return "\c0Incredibly Rare";
    else if(getfield(%ore,9) >= 1)
        return "\c5Unique";
    else if(getfield(%ore,9) >= 0)
        return "\c6NONE";
}