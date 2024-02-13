function servercmdleaderboard(%client)
{
    %fw = new FileObject();
    for(%i = 0; %i < 10; %i++)
    {
        %highestPoints = -1;
        %fw.openForRead("config/server/mining/leaderboard.txt");
        while(!%fw.iseof())
        {
            %line = %fw.readline();
            if(getfield(%line,4) > %highestPoints && !%ignoreList[getfield(%line,0)] || getfield(%line,4) <= 0 && getfield(%line,3) > %highestPoints && !%ignoreList[getfield(%line,0)])
            {
                if(getfield(%line,4) > 0)
                {
                    %lbSpot[%i] = getfield(%line,0) TAB getfield(%line,1) TAB getfield(%line,2) TAB getfield(%line,3) TAB getfield(%line,4);
                    %highestPoints = getfield(%line,4);
                    %ignorePlayer = getfield(%line,0);
                }
                else
                {
                    %lbSpot[%i] = getfield(%line,0) TAB getfield(%line,1 )TAB getfield(%line,2) TAB getfield(%line,3) TAB getfield(%line,4);
                    %highestPoints = getfield(%line,3);
                    %ignorePlayer = getfield(%line,0);
                }
            }
        }
        %ignoreList[%ignorePlayer] = 1;
    }
    %fw.close();
    %fw.delete();
    %client.chatmessage("\c6----- leaderboard -----");
    for(%i = 0; %i < 10; %i++)
    {
        if(getfield(%lbSpot[%i],0) !$= "")
        {
            %client.chatmessage("\c4#" @ %i+1 @ ".\c3" SPC getfield(%lbSpot[%i],0) SPC "(BL_ID: " @ getfield(%lbSpot[%i],1) @ ")" SPC "- \c2Prestige Points:" SPC mceil(getfield(%lbSpot[%i],4)) SPC "-\c5 Prestige:" SPC numbersToLatin(getfield(%lbSpot[%i],2)) SPC "- \c4Level:" SPC mceil(getfield(%lbSpot[%i],3)));
        }
    }
}