exec("./projectile.cs");

datablock AudioProfile(blockhead_prime_introSound)
{
	filename = "./blockhead_prime_intro.wav";
	description = AudioClosest3d;
	preload = false;
};

datablock AudioProfile(blockhead_prime_die_1Sound : blockhead_prime_introSound)
{
	filename = "./blockhead_prime_die_1.wav";
};

datablock AudioProfile(blockhead_prime_die_2Sound : blockhead_prime_introSound)
{
	filename = "./blockhead_prime_die_2.wav";
};

datablock AudioProfile(blockhead_prime_prepare_thyself_1Sound : blockhead_prime_introSound)
{
	filename = "./blockhead_prime_prepare_thyself_1.wav";
};

datablock AudioProfile(blockhead_prime_prepare_thyself_2Sound : blockhead_prime_introSound)
{
	filename = "./blockhead_prime_prepare_thyself_2.wav";
};

datablock AudioProfile(blockhead_prime_judgement_1Sound : blockhead_prime_introSound)
{
	filename = "./blockhead_prime_judgement_1.wav";
};

datablock AudioProfile(blockhead_prime_judgement_2Sound : blockhead_prime_introSound)
{
	filename = "./blockhead_prime_judgement_2.wav";
};

datablock AudioProfile(blockhead_prime_thy_end_is_now_1Sound : blockhead_prime_introSound)
{
	filename = "./blockhead_prime_thy_end_is_now_1.wav";
};

datablock AudioProfile(blockhead_prime_thy_end_is_now_2Sound : blockhead_prime_introSound)
{
	filename = "./blockhead_prime_thy_end_is_now_1.wav";
};

datablock AudioProfile(blockhead_prime_crushSound : blockhead_prime_introSound)
{
	filename = "./blockhead_prime_crush.wav";
};

datablock AudioProfile(blockhead_prime_uselessSound : blockhead_prime_introSound)
{
	filename = "./blockhead_prime_useless.wav";
};

datablock AudioProfile(blockhead_prime_weakSound : blockhead_prime_introSound)
{
	filename = "./blockhead_prime_weak.wav";
};

datablock AudioProfile(blockhead_prime_grunt_1Sound : blockhead_prime_introSound)
{
	filename = "./blockhead_prime_grunt_1.wav";
};

datablock AudioProfile(blockhead_prime_grunt_2Sound : blockhead_prime_introSound)
{
	filename = "./blockhead_prime_grunt_2.wav";
};

datablock AudioProfile(blockhead_prime_punchSound : blockhead_prime_introSound)
{
	filename = "./blockhead_prime_punch.wav";
};

datablock AudioProfile(blockhead_prime_projectileSound : blockhead_prime_introSound)
{
	filename = "./blockhead_prime_projectile.wav";
};

datablock AudioProfile(blockhead_prime_throw1Sound : blockhead_prime_introSound)
{
	filename = "./blockhead_prime_throw1.wav";
};

datablock AudioProfile(blockhead_prime_throw2Sound : blockhead_prime_introSound)
{
	filename = "./blockhead_prime_throw2.wav";
};

datablock AudioProfile(blockhead_prime_explosion1Sound : blockhead_prime_introSound)
{
	filename = "./blockhead_prime_explosion1.wav";
};

datablock AudioProfile(blockhead_prime_explosion2Sound : blockhead_prime_introSound)
{
	filename = "./blockhead_prime_explosion2.wav";
};

datablock AudioProfile(blockhead_prime_explosion3Sound : blockhead_prime_introSound)
{
	filename = "./blockhead_prime_explosion3.wav";
};

function blockhead_primeHoleBot::onBotLoop(%this, %obj)
{
	if(%obj.attacking == 1)
	{
		continue;
	}
	else
	{
		%obj.tick++;
	}
	if(%obj.tick >= 4)
	{
		if(%obj.stamina > 0)
		{
			%attack = getrandom(0, 3);
			if(%attack == %obj.ignore)
			{
				%attack = getrandom(0, 3);
				return;
			}
			if(%attack == 0)
			{
				%obj.ignore = 0;
				%obj.attacking = 1;
				%obj.stamina = %obj.stamina - 2;
				%obj.tick = 0;
				%obj.setmaxforwardspeed(0);
				initContainerRadiusSearch(%obj.getposition(), 200, $typemasks::PlayerObjectType);
   				while(%object = containerSearchNext())
   				{
					%sound = getrandom(1,2);
					if(%sound == 1)
					{
						%object.client.playsound(blockhead_prime_judgement_1Sound);
						%object.client.playsound(blockhead_prime_judgement_1Sound);
						%object.client.playsound(blockhead_prime_judgement_1Sound);
					}
					if(%sound == 2)
					{
						%object.client.playsound(blockhead_prime_judgement_2Sound);
						%object.client.playsound(blockhead_prime_judgement_2Sound);
						%object.client.playsound(blockhead_prime_judgement_2Sound);
					}
					if(%object.client.getsimplename() $= %obj.target)
					{
						%obj.schedule(2001, judgementteleport);
						%obj.schedule(2300, judgementattack);
					}
					%object.client.schedule(2300, playsound, blockhead_prime_explosion2sound);
					%object.client.schedule(2300, playsound, blockhead_prime_explosion2sound);
					%object.client.schedule(2300, playsound, blockhead_prime_explosion2sound);
					%object.client.schedule(2300, playsound, blockhead_prime_explosion2sound);
				}
			}
			if(%attack == 1)
			{
				%obj.ignore = 1;
				%obj.attacking = 1;
				%obj.setmaxforwardspeed(0);
				%obj.stamina = %obj.stamina - 3;
				%obj.tick = 0;
				initContainerRadiusSearch(%obj.getposition(), 200, $typemasks::PlayerObjectType);
   				while(%object = containerSearchNext())
   				{
					%sound = getrandom(1,2);
					if(%sound == 1)
					{
						%object.client.playsound(blockhead_prime_prepare_thyself_1Sound);
						%object.client.playsound(blockhead_prime_prepare_thyself_1Sound);
						%object.client.playsound(blockhead_prime_prepare_thyself_1Sound);
					}
					if(%sound == 2)
					{
						%object.client.playsound(blockhead_prime_prepare_thyself_2Sound);
						%object.client.playsound(blockhead_prime_prepare_thyself_2Sound);
						%object.client.playsound(blockhead_prime_prepare_thyself_2Sound);
					}
					if(%object.client.getsimplename() $= %obj.target)
					{
						%obj.schedule(2001, thyendisnowteleport);
						%obj.schedule(2751, thyendisnowteleport);
						%obj.schedule(3501, thyendisnowteleport);
						%obj.schedule(4250, preparethyselfattack);
						%obj.schedule(5000, restore);
					}
				}
			}
			if(%attack == 2)
			{
				%obj.ignore = 2;
				%obj.attacking = 1;
				%obj.setmaxforwardspeed(0);
				%obj.stamina = %obj.stamina - 2;
				%obj.tick = 0;
				initContainerRadiusSearch(%obj.getposition(), 200, $typemasks::PlayerObjectType);
   				while(%object = containerSearchNext())
   				{
					%sound = getrandom(1,2);
					if(%sound == 1)
					{
						%object.client.playsound(blockhead_prime_thy_end_is_now_1Sound);
						%object.client.playsound(blockhead_prime_thy_end_is_now_1Sound);
						%object.client.playsound(blockhead_prime_thy_end_is_now_1Sound);
					}
					if(%sound == 2)
					{
						%object.client.playsound(blockhead_prime_thy_end_is_now_2Sound);
						%object.client.playsound(blockhead_prime_thy_end_is_now_2Sound);
						%object.client.playsound(blockhead_prime_thy_end_is_now_2Sound);
					}
					if(%object.client.getsimplename() $= %obj.target)
					{
						%obj.schedule(2001, thyendisnowteleport);
						%obj.schedule(2751, thyendisnowteleport);
						%obj.schedule(3501, thyendisnowteleport);
						%obj.schedule(4200, targetairborne);
						%obj.schedule(4250, thyendisnowteleport);
						%obj.schedule(5000, restore);
					}
				}
			}
			if(%attack == 3)
			{
				%obj.ignore = 3;
				%obj.attacking = 1;
				%obj.setmaxforwardspeed(0);
				%obj.stamina = %obj.stamina - 2;
				%obj.tick = 0;
				initContainerRadiusSearch(%obj.getposition(), 200, $typemasks::PlayerObjectType);
   				while(%object = containerSearchNext())
   				{
					%sound = getrandom(1,2);
					if(%sound == 1)
					{
						%object.client.playsound(blockhead_prime_punchsound);
						%object.client.playsound(blockhead_prime_punchsound);
						%object.client.playsound(blockhead_prime_punchsound);
						%object.client.schedule(1000, playsound, blockhead_prime_die_1Sound);
						%object.client.schedule(1000, playsound, blockhead_prime_die_1Sound);
						%object.client.schedule(1000, playsound, blockhead_prime_die_1Sound);
					}
					if(%sound == 2)
					{
						%object.client.playsound(blockhead_prime_punchsound);
						%object.client.playsound(blockhead_prime_punchsound);
						%object.client.playsound(blockhead_prime_punchsound);
						%object.client.schedule(1000, playsound, blockhead_prime_die_2Sound);
						%object.client.schedule(1000, playsound, blockhead_prime_die_2Sound);
						%object.client.schedule(1000, playsound, blockhead_prime_die_2Sound);
					}
					if(%object.client.getsimplename() $= %obj.target)
					{
						%obj.dieprepare();
						%obj.schedule(2250, dieattack);
					}
				}
			}
		}	
	}
	if(%obj.stamina < 1)
	{
		if(%obj.tick >= 2)
		{
			%obj.tick = 0;
			%obj.stamina = 10;
		}
	}
}

function blockhead_primeHoleBot::onBotCollision(%this, %obj, %col, %normal, %speed)
{

}

function blockhead_primeHoleBot::onBotFollow(%this, %obj, %targ)
{
	%obj.target = %targ.client.getsimplename();
	%targetposition = %targ.getposition();
	%forwardvector = %obj.getforwardvector();
	%forwardvectorx = -3 * getword(%forwardvector, 0);
	%forwardvectory = -3 * getword(%forwardvector, 1);
	%objvector = %forwardvectorx SPC %forwardvectory;
	%obj.targetposition = vectoradd(%targetposition,%objvector);

	%notairborne = getwords(%targ.getposition(),0,1);
	%obj.targetpositionnotairborne = %notairborne SPC "16.5";

	%targetpos2 = getwords(%targ.getposition(),0,1);
	%targetpos3 = %targetpos2 SPC "16.5";
	%obj.targetpos = vectoradd(%targetpos3,%objvector);
}

function blockhead_primeHoleBot::onBotDamage(%this, %obj, %source, %pos, %damage, %type)
{
	%damage = getrandom(1,25);
	if(%damage == 1)
	{
		initContainerRadiusSearch(%obj.getposition(), 200, $typemasks::PlayerObjectType);
   		while(%object = containerSearchNext())
   		{
			%sound = getrandom(1,2);
			if(%sound == 1)
			{
				%object.client.playsound(blockhead_prime_grunt_1Sound);
				%object.client.playsound(blockhead_prime_grunt_1Sound);
				%object.client.playsound(blockhead_prime_grunt_1Sound);
			}
			if(%sound == 2)
			{
				%object.client.playsound(blockhead_prime_grunt_2Sound);
				%object.client.playsound(blockhead_prime_grunt_2Sound);
				%object.client.playsound(blockhead_prime_grunt_2Sound);
			}
		}
	}
}

function AIPlayer::restore(%this)
{
	%this.attacking = 0;
	%this.tick = 0;
	%this.schedule(1000, setmaxforwardspeed, 5);
}

function AIPlayer::judgementteleport(%this)
{
	%this.settransform(%this.targetposition);
	if(%this.isgroundedsport() != 1)
	{
		%this.setvelocity("0 0 0");
		%this.schedule(40, setvelocity, "0 0 0");
		%this.schedule(80, setvelocity, "0 0 0");
		%this.schedule(120, setvelocity, "0 0 0");
		%this.schedule(160, setvelocity, "0 0 0");
		%this.schedule(200, setvelocity, "0 0 0");
		%this.schedule(240, setvelocity, "0 0 0");
		%this.schedule(280, setvelocity, "0 0 0");
		%this.schedule(320, setvelocity, "0 0 0");
		%this.schedule(360, setvelocity, "0 0 0");
		%this.schedule(400, setvelocity, "0 0 0");
		%this.schedule(440, setvelocity, "0 0 0");
		%this.schedule(480, setvelocity, "0 0 0");
	}
}

function AIPlayer::gainvelocity(%this)
{
	%fw = %this.getforwardvector();
	%fw1 = getword(%fw, 0);
	%fw2 = getword(%fw, 1);
	%vel1 = 17 * %fw1;
	%vel2 = 17 * %fw2;
	%this.setvelocity(%vel1 SPC %vel2 SPC "3");
}

function AIPlayer::targetairborne(%this)
{
	if(getword(%this.targetposition,2) >= 23)
	{
		%this.airattack = 1;
	}
}

function AIPlayer::thyendisnowteleport(%this)
{
	initContainerRadiusSearch(%this.getposition(), 200, $typemasks::PlayerObjectType);
   	while(%object = containerSearchNext())
   	{
		%object.client.playsound(blockhead_prime_punchsound);
		%object.client.playsound(blockhead_prime_punchsound);
		%object.client.playsound(blockhead_prime_punchsound);
	}
	if(%this.airattack < 1)
	{
		%this.settransform(%this.targetpos);
		%this.schedule(100, gainvelocity);
		%this.schedule(300, thyendisnowattack);
	}
	else
	{
		%this.settransform(%this.targetposition);
		%this.setvelocity("0 0 0");
		%this.schedule(40, setvelocity, "0 0 0");
		%this.schedule(80, setvelocity, "0 0 0");
		%this.schedule(120, setvelocity, "0 0 0");
		%this.schedule(160, setvelocity, "0 0 0");
		%this.schedule(200, setvelocity, "0 0 0");
		%this.schedule(240, setvelocity, "0 0 0");
		%this.schedule(280, setvelocity, "0 0 0");
		%this.schedule(320, setvelocity, "0 0 0");
		%this.schedule(360, setvelocity, "0 0 0");
		%this.schedule(400, setvelocity, "0 0 0");
		%this.schedule(440, setvelocity, "0 0 0");
		%this.schedule(480, setvelocity, "0 0 0");
		%this.schedule(250, thyendisnowairattack);
		%this.airattack = 0;
	}
}

function AIPlayer::thyendisnowattack(%this)
{
	%forwardvector = %this.getforwardvector();
	%forwardvector1 = getword(%forwardvector,0);
	%forwardvector2 = getword(%forwardvector,1);
	%diff1 = 2 * %forwardvector1;
	%diff2 = 2 * %forwardvector2;
	%objvec = %diff1 SPC %diff2 SPC "1.5";
	%pos = vectoradd(%this.getposition(),%objvec);
	initContainerRadiusSearch(%pos, 1.5, $typemasks::PlayerObjectType);
	while(%object = containerSearchNext())
	{
		if(%object == %this)
		{
			continue;
		}
		%object.inflictdamage(0, 15);
		%object.setvelocity("0 0 30");
	}
}

function AIPlayer::thyendisnowairattack(%this)
{
	%forwardvector = %this.getforwardvector();
	%forwardvector1 = getword(%forwardvector,0);
	%forwardvector2 = getword(%forwardvector,1);
	%diff1 = 2 * %forwardvector1;
	%diff2 = 2 * %forwardvector2;
	%objvec = %diff1 SPC %diff2 SPC "1.5";
	%pos = vectoradd(%this.getposition(),%objvec);
	initContainerRadiusSearch(%pos, 3.5, $typemasks::PlayerObjectType);
	while(%object = containerSearchNext())
	{
		if(%object == %this)
		{
			continue;
		}
		%object.inflictdamage(0, 25);
		%object.setvelocity("0 0 -60");
		%object.schedule(275, spawnexplosion, cannonballprojectile, 1);
	}
}

function AIPlayer::judgementattack(%this)
{
	initContainerRadiusSearch(%this.getposition(), 200, $typemasks::PlayerObjectType);
	while(%object = containerSearchNext())
	{
		%forwardvector = %this.getforwardvector();
		%forwardvector1 = getword(%forwardvector,0);
		%forwardvector2 = getword(%forwardvector,1);
		%diff1 = 2.2 * %forwardvector1;
		%diff2 = 2.2 * %forwardvector2;
		%objvec = %diff1 SPC %diff2 SPC "0.5";
		%pos = vectoradd(%this.getposition(),%objvec);
		%p = new Projectile()
	    {
		    dataBlock = cannonballprojectile;
		    initialPosition = %pos;
		    initialVelocity = "0 0 0";
        	sourceObject = %this;
			client = %this;
			sourceSlot = 0;
	    };
		%this.isinvincible = 1;
		%p.setscale("1.6 1.6 1.6");
		%p.explode();
		%this.clearburn();
		%this.isinvincible = 0;
	}
	%this.attacking = 0;
	%this.schedule(1000, setmaxforwardspeed, 5);
}

function AIPlayer::dieprepare(%this)
{
	%this.setmaxforwardspeed(0);
	%this.setvelocity("0 0 40");
	%this.schedule(1000, setvelocity, "0 0 0");
	%this.schedule(1050, setvelocity, "0 0 0");
	%this.schedule(1100, setvelocity, "0 0 0");
	%this.schedule(1150, setvelocity, "0 0 0");
	%this.schedule(1200, setvelocity, "0 0 0");
	%this.schedule(1250, setvelocity, "0 0 0");
	%this.schedule(1300, setvelocity, "0 0 0");
	%this.schedule(1350, setvelocity, "0 0 0");
	%this.schedule(1400, setvelocity, "0 0 0");
	%this.schedule(1450, setvelocity, "0 0 0");
	%this.schedule(1500, setvelocity, "0 0 0");
	%this.schedule(1550, setvelocity, "0 0 0");
	%this.schedule(1600, setvelocity, "0 0 0");
	%this.schedule(1650, setvelocity, "0 0 0");
	%this.schedule(1700, setvelocity, "0 0 0");
	%this.schedule(1750, setvelocity, "0 0 0");
	%this.schedule(1800, setvelocity, "0 0 0");
	%this.schedule(1850, setvelocity, "0 0 0");
}

function AIPlayer::dieattack(%this)
{
	%this.schedule(500, setmaxforwardspeed, 5);
	%this.attacking = 0;

	%start = %this.geteyepoint();
	%eyevec = %this.geteyevector();
	%eyevecz = getword(%eyevec,2);
	%range = 200;
	%vec = vectorscale(%eyevec, %range);
	%end = vectoradd(%start, %vec);
	%mask = $TypeMasks::PlayerObjectType;
	%raycast = containerRayCast(%start, %end, %mask, %this);
	%hitObj = getWord (%raycast, 0);
	%hitPos = getWords (%raycast, 1, 3);
	%hitNormal = getWords (%raycast, 4, 6);
	%hitobj.inflictdamage(0, 30);
	%hitobj.addvelocity("0 0 30");
	
	if(getword(%this.targetposition,2) > 26)
	{
		%start = %this.geteyepoint();
		%eyevec = %this.geteyevector();
		%eyevecz = getword(%eyevec,2);
		%range = 200;
		%vec = vectorscale(%eyevec, %range);
		%end = vectoradd(%start, %vec);
		%mask = $TypeMasks::FxBrickAlwaysObjectType;
		%raycast = containerRayCast(%start, %end, %mask, %this);
		%hitObj = getWord (%raycast, 0);
		%hitPos = getWords (%raycast, 1, 3);
		%hitNormal = getWords (%raycast, 4, 6);
		%this.settransform(%hitpos);
	}
	else
	{
		%this.settransform(%this.targetpositionnotairborne);
	}
	%forwardvector = %this.getforwardvector();
	%forwardvector1 = getword(%forwardvector,0);
	%forwardvector2 = getword(%forwardvector,1);
	%diff1 = 2 * %forwardvector1;
	%diff2 = 2 * %forwardvector2;
	%objvec = %diff1 SPC %diff2 SPC "0.5";
	%pos = vectoradd(%this.getposition(),%objvec);

	%p = new Projectile()
	{
		    dataBlock = cannonballprojectile;
		    initialPosition = %pos;
		    initialVelocity = "0 0 0";
        	sourceObject = %this;
			client = %this;
			sourceSlot = 0;
	};
	%this.isinvincible = 1;
	%p.setscale("1.2 1.2 1.2");
	%p.explode();
	%this.clearburn();
	initContainerRadiusSearch(%this.getposition(), 150, $typemasks::PlayerObjectType);
	while(%object = containerSearchNext())
	{
		if(%object == %this)
		{
			continue;
		}
		%object.client.playsound(blockhead_prime_explosion1sound);
		%object.client.playsound(blockhead_prime_explosion1sound);
		%object.client.playsound(blockhead_prime_explosion1sound);
		%object.client.playsound(blockhead_prime_explosion1sound);
	}
	%this.isinvincible = 0;
}

function AIPlayer::preparethyselfattack(%this)
{
	initContainerRadiusSearch(%this.getposition(), 200, $typemasks::PlayerObjectType);
   	while(%object = containerSearchNext())
   	{
		%object.client.playsound(blockhead_prime_throw1sound);
		%object.client.playsound(blockhead_prime_throw1sound);
		%object.client.playsound(blockhead_prime_throw1sound);
		%object.client.playsound(blockhead_prime_throw1sound);
		%object.client.schedule(500, playsound, blockhead_prime_throw2sound);
		%object.client.schedule(500, playsound, blockhead_prime_throw2sound);
		%object.client.schedule(500, playsound, blockhead_prime_throw2sound);
		%object.client.schedule(500, playsound, blockhead_prime_throw2sound);
		%object.client.schedule(500, playsound, blockhead_prime_throw2sound);
	}
	%this.schedule(500, spawnprojectile, 45, blockheadprimeprojectile, "0 0 0", 2);
}