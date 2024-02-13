function servercmdleaderboard(%client)
{
    %filecount=0;
    %fw = new FileObject();
    for(%file = findfirstfile("config/server/mining/*/prestige.txt"); isfile(%file); %file = findnextfile("config/server/mining/*/prestige.txt"))
    {
        %blid = stripchars(%file, "dconfig/serverminingprestige.txt");
        %points = 0;
        %prestige = 0;
        %fw.openForRead(%file);
        //while(!%fw.iseof())
        //{
            //%prestige = %fw.readline();
            //%prestigePoints = %fw.readline();
            //%fw.openForRead(%file);
            //%line = %fw.readline();
            //if(getfield(%line,0) $= "TOTAL PRESTIGE POINTS:")
                //%totalPoints = getfield(%line,1) + %prestigePoints;
        //}
        %prestige = %fw.readline();
        if(%prestige >= 1)
        {
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
            for(%i = 0; %i < %mpu; %i++)
                %points += 1 + (%i*5/5);
            for(%i = 0; %i < %mmu; %i++)
                %points += 1 + (%i*0.05*100) - (2 * ((%i*0.05*100)/5));
            for(%i = 0; %i < %eu; %i++)
                %points += 1 + (%i*0.1*100/5);
            for(%i = 0; %i < %cu; %i++)
                %points += 1 + (%i*0.1*100/5);
            for(%i = 0; %i < %cdu; %i++)
                %points += mfloatlength(2 * (mpow((1+%i)*0.1*100,2.2))/2500,0)-10;
            for(%i = 0; %i < %slu; %i++)
                %points += mpow(5,1+%i);
            for(%i = 0; %i < %mmmu; %i++)
                %points += 1 + mfloatlength((mpow(%i*0.1*100,1.15)/1.5),0);
            for(%i = 0; %i < %pu; %i++)
            {
                %test = mfloatlength(15 + %i*35,0);
                if(%test == 0)
                    %points += 15;
                else
                    %points += mfloatlength(15 + %i*35,0);
            }
            for(%i = 0; %i < %tu; %i++)
                %points += mfloatlength(15 + mpow(%i*0.45*100,0.6)*1.5,0);
            for(%i = 0; %i < %tcd; %i++)
                %points += mfloatlength(10 + mpow(%i*2.5,0.8)*1.25,0);
            for(%i = 0; %i < %sod; %i++)
                %points += 14 + mpow(4,2+%i);
            %savedFile[%filecount] = %blid TAB %points+%prestigepoints TAB %prestige;
            announce(%blid SPC %points+%prestigepoints SPC %prestige);
            %filecount++;
        }
    }
    for(%i = 0; %i < %filecount; %i++)
    {
        %blid = getfield(%savedFile[%i],0);
        %totalPoints = getfield(%savedFile[%i],1);
        %prestige = getfield(%savedFile[%i],2);
        if(%totalPoints > %highestPoints && !%ignoreList[%blid])// || getfield(%line,2) < 1 && getfield(%line,3) > %highestPoints && !%ignoreList[getfield(%line,0)])
        {
            //if(getfield(%line,2) > 0)
            //{
                %highestPoints = %totalPoints;
                %ignoreUser = %blid;
                %lbSpot[%i] = %blid TAB %totalpoints TAB %prestige;// TAB getfield(%line,2);
            //}
            //else
            //{
                //%highestPoints = getfield(%line,3);
                //%ignoreUser = getfield(%line,0);
                //%lbSpot[%i] = getfield(%line,0) TAB getfield(%line,4) TAB getfield(%line,3) TAB getfield(%line,2);
            //}
        }
        %ignoreList[%blid] = 1;
    }
    %fw.close();
    %fw.delete();
    %client.chatmessage("\c6----- leaderboard -----");
    for(%i = 0; %i < 10; %i++)
    {
        if(getfield(%lbSpot[%i],0) !$= "")
        {
            %client.chatmessage("\c4#" @ %i+1 @ ".\c3" SPC getfield(%lbSpot[%i],0) SPC "- \c2Prestige Points:" SPC mceil(getfield(%lbSpot[%i],1)) SPC "-\c5 Prestige:" SPC numbersToLatin(getfield(%lbSpot[%i],2)) SPC "- \c4Level:" SPC mceil(getfield(%lbSpot[%i],3)));
        }
    }
}