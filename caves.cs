function loadcave(%save, %offset, %amount, %line)
{
    if(!$currentlygenerating)
        $currentlygenerating = 0;
    $currentlygenerating++;
    %save = "saves/" @ %save @ ".bls";
    %cavegenerator = new scriptobject(CaveGenerator @ $currentlygenerating);
    %cavegenerator.amount = 0;
    %offset = vectoradd(%offset, "0.5 0 0.5");
    %fw = new fileobject();
    %fw.openforread(%save);
    while(!%fw.iseof())
    {
        %line = %fw.readline();
        %quotePos = strstr(%line, "\"");
        if(%quotepos < 0)
            continue;
		%uiName = getSubStr(%line, 0, %quotePos);
        %db = $uiNameTable[%uiName];
        %line = getSubStr (%line, %quotePos + 2, 9999);
		%pos = getWords (%line, 0, 2);
		%angId = getWord (%line, 3);
		%isBaseplate = getWord (%line, 4);
		%colorId = getWord(%line, 5);
		%printName = getWord (%line, 6);
        if(%colorid == 38)
        {
            %trans = vectoradd(%pos, %offset);
            %pos1 = mceil(getword(%trans,0));
            %pos2 = mceil(getword(%trans,1));
            %pos3 = getword(%trans,2);
            $diggedPosition[%pos1 SPC %pos2 SPC %pos3] = 1 SPC $iterations;
        }
        else
        {
            %depth = 5000.2 - getword(vectoradd(%pos, %offset),2);
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
            else if(%depth >= 5000 && getrandom(1,100) >= 95)
                %generate = "Black Hole";
            %trans = vectoradd(%pos, %offset);
            %pos1 = mceil(getword(%trans,0));
            %pos2 = mceil(getword(%trans,1));
            %pos3 = getword(%trans,2);
            %cavegenerator.position[%cavegenerator.amount] = %pos1 SPC %pos2 SPC %pos3 TAB oreIDfromName(%generate);
            %cavegenerator.amount++;
        }
    }
    %fw.close();
	%fw.delete();
    cavegeneratortick(0, $currentlygenerating);
}

function cavegeneratortick(%line, %cave)
{
    %cavegenerator = "CaveGenerator" @ %cave;
    if(%cavegenerator.amount > %line)
    {
        while(%amount < 50)
        {
            if(getfield(%cavegenerator.position[%line],1) $= "block")
            {
                %b = new fxDTSBrick ("")
                {
                    dataBlock = brick2xcubeprintdata;
                    colorID = 38;
                    isPlanted = 1;
                    stackBL_ID = 888888;
                };
                $lastspawnedbrick = %b;
                brickgroup_888888.add(%b);
                %b.setTransform(getfield(%cavegenerator.position[%line],0));
                %b.trustCheckFinished();
                %err = %b.plant();
                %b.setrendering(0);
                %b.setcolliding(0);
                %b.setraycasting(0);
                if (%err == 1 || %err == 3 || %err == 5)
                {
                    $lastspawnedbrick = 0;
                    %b.delete();
                }
            }
            else
            {
                %pos = getfield(%cavegenerator.position[%line],0);
                %pos1 = mceil(getword(%pos,0));
                %pos2 = mceil(getword(%pos,1));
                %finalpos = %pos1 SPC %pos2 SPC getword(%pos,2);
                %orename = getfield(%cavegenerator.position[%line],1);
                if(%orename != -1)
                    addbrick(%finalpos, getfield(%cavegenerator.position[%line],1));
                
            }
            %line++;
            %amount++;
        }
        if(%amount >= 50)
        {
            %amount = 0;
            %cavegenerator.repeat = schedule(350, 0, cavegeneratortick, %line, %cave);
        }
    }
    else
    {
        $currentlygenerating--;
        cancel(%cavegenerator.repeat);
        %cavegenerator.delete();
        if($sim::time < $startertime + 10)
        {
            initcontainerradiussearch("0 0 0", 1, $typemasks::fxbrickobjecttype);
            while(%search=containersearchnext())
            {
                %search.delete();
            }
        }
    }
}