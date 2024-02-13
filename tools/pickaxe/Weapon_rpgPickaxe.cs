datablock AudioProfile(dig_0)
{
   filename    = "./dig_0.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(dig_1)
{
   filename    = "./dig_1.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(dig_2)
{
   filename    = "./dig_2.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(tink_0)
{
   filename    = "./tink_0.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(tink_1)
{
   filename    = "./tink_1.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(tink_2)
{
   filename    = "./tink_2.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock ExplosionData(rpgPickaxeExplosion)
{
   lifeTimeMS = 400;

   particleEmitter = swordExplosionEmitter;
   particleDensity = 8;
   particleRadius = 0.15;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = false;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "0.5 0.5 0.5";
   camShakeDuration = 0.25;
   camShakeRadius = 5.0;

   lightStartRadius = 1.5;
   lightEndRadius = 0;
   lightStartColor = "00.0 0.2 0.6";
   lightEndColor = "0 0 0";
};


datablock ItemData(rpgPickaxeItem : swordItem)
{
	miningpower = 10;
	shapeFile = "./Lil_Pickaxe.dts";
	uiName = "Stone Pickaxe";
	doColorShift = true;
	colorShiftColor = "0.825 0.825 0.825 1.000";

	image = rpgPickaxeImage;
	canDrop = false;
	iconName = "./icon_rpgPickaxe";
};

AddDamageType("rpgPickaxe",   '<bitmap:add-ons/Tool_RPG/CI_rpgPickaxe> %1',    '%2 <bitmap:add-ons/Tool_RPG/CI_rpgPickaxe> %1',0.75,1);

datablock ProjectileData(rpgPickaxeProjectile)
{
   directDamage        = 25;
   directDamageType  = $DamageType::rpgPickaxe;
   radiusDamageType  = $DamageType::rpgPickaxe;
   explosion           = rpgPickaxeExplosion;

   muzzleVelocity      = 75;
   velInheritFactor    = 1;

   armingDelay         = 0;
   lifetime            = 100;
   fadeDelay           = 70;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.0;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";

   uiName = "RPG Pickaxe Hit";
};

datablock ProjectileData(blankProjectile)
{
   directDamage        = 0;
   directDamageType  = $DamageType::rpgPickaxe;
   radiusDamageType  = $DamageType::rpgPickaxe;
   explosion           = "";

   muzzleVelocity      = 75;
   velInheritFactor    = 1;

   armingDelay         = 0;
   lifetime            = 100;
   fadeDelay           = 70;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.0;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";

   uiName = "";
};



datablock ShapeBaseImageData(rpgPickaxeImage)
{
   shapeFile = "./Lil_Pickaxe.dts";
   emap = true;

   mountPoint = 0;
   offset = "0 0 0";

   correctMuzzleVector = false;

   className = "WeaponImage";

   item = rpgPickaxeItem;
   ammo = " ";
   projectiletype = projectile;
   projectile = blankprojectile;


   melee = true;
   doRetraction = false;

   armReady = true;


   doColorShift = true;
   colorShiftColor = "0.825 0.825 0.825 1.000";

	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.5;
	stateTransitionOnTimeout[0]      = "Ready";
	stateSound[0]                    = swordDrawSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "PreFire";
	stateAllowImageChange[1]         = true;

	stateName[2]			= "PreFire";
	stateScript[2]                  = "onPreFire";
	stateAllowImageChange[2]        = false;
	stateTimeoutValue[2]            = 0.1;
	stateTransitionOnTimeout[2]     = "Fire";

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "CheckFire";
	stateTimeoutValue[3]            = 0.2;
	stateFire[3]                    = true;
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateScript[3]                  = "onFire";
	stateWaitForTimeout[3]		= true;

	stateName[4]			= "CheckFire";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";

	
	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.2;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	stateSequence[5]                = "StopFire";
	stateScript[5]                  = "onStopFire";


};

function rpgPickaxeImage::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armattack);
}

function rpgPickaxeImage::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}

function rpgPickaxeImage::onFire(%this,%obj,%slot)
{
	swingPickaxe(%obj, %this.getname());
}



datablock ItemData(rpgPickaxe2Item : rpgPickaxeItem)
{
	miningpower = 15;
	uiName = "Iron Pickaxe";
	colorShiftColor = "0.5 0.5 0.6 1.000";
	image = rpgPickaxe2Image;
};

datablock ShapeBaseImageData(rpgPickaxe2Image : rpgPickaxeImage)
{
   item = rpgPickaxe2Item;
   colorShiftColor = "0.5 0.5 0.6 1.000";
};

function rpgPickaxe2Image::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armattack);
}

function rpgPickaxe2Image::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}

function rpgPickaxe2Image::onFire(%this,%obj,%slot)
{
	swingPickaxe(%obj, %this.getname());
}



datablock ItemData(rpgPickaxe3Item : rpgPickaxeItem)
{
	miningpower = 22;
	uiName = "Gold Pickaxe";
	colorShiftColor = "1 0.9 0.1 1.000";
	image = rpgPickaxe3Image;
};

datablock ShapeBaseImageData(rpgPickaxe3Image : rpgPickaxeImage)
{
   item = rpgPickaxe3Item;
   colorShiftColor = "1 0.9 0.1 1.000";
};

function rpgPickaxe3Image::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armattack);
}

function rpgPickaxe3Image::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}

function rpgPickaxe3Image::onFire(%this,%obj,%slot)
{
	swingPickaxe(%obj, %this.getname());
}



datablock ItemData(rpgPickaxe4Item : rpgPickaxeItem)
{
	miningpower = 35;
	uiName = "Quartz Pickaxe";
	colorShiftColor = "0.9 0.9 0.9 0.850";
	image = rpgPickaxe4Image;
};

datablock ShapeBaseImageData(rpgPickaxe4Image : rpgPickaxeImage)
{
   item = rpgPickaxe4Item;
   colorShiftColor = "0.9 0.9 0.9 0.850";
   stateTimeoutValue[3]            = 0.175;
};

function rpgPickaxe4Image::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armattack);
}

function rpgPickaxe4Image::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}

function rpgPickaxe4Image::onFire(%this,%obj,%slot)
{
	swingPickaxe(%obj, %this.getname());
}



datablock ItemData(rpgPickaxe5Item : rpgPickaxeItem)
{
	miningpower = 50;
	uiName = "Cobalt Pickaxe";
	colorShiftColor = "0 0.1 0.85 1";
	image = rpgPickaxe5Image;
};

datablock ShapeBaseImageData(rpgPickaxe5Image : rpgPickaxeImage)
{
   item = rpgPickaxe5Item;
   colorShiftColor = "0 0.1 0.85 1";
   stateTimeoutValue[3]            = 0.175;
};

function rpgPickaxe5Image::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armattack);
}

function rpgPickaxe5Image::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}

function rpgPickaxe5Image::onFire(%this,%obj,%slot)
{
	swingPickaxe(%obj, %this.getname());
}


datablock ItemData(rpgPickaxe6Item : rpgPickaxeItem)
{
	miningpower = 75;
	uiName = "Palladium Pickaxe";
	colorShiftColor = "1 0.6 0.05 1.000";
	image = rpgPickaxe6Image;
};

datablock ShapeBaseImageData(rpgPickaxe6Image : rpgPickaxeImage)
{
   item = rpgPickaxe6Item;
   colorShiftColor = "1 0.6 0.05 1.000";
   stateTimeoutValue[3]            = 0.175;
};

function rpgPickaxe6Image::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armattack);
}

function rpgPickaxe6Image::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}

function rpgPickaxe6Image::onFire(%this,%obj,%slot)
{
	swingPickaxe(%obj, %this.getname());
}



datablock ItemData(rpgPickaxe7Item : rpgPickaxeItem)
{
	miningpower = 100;
	uiName = "Emerald Pickaxe";
	colorShiftColor = "0 0.95 0.1 1.000";
	image = rpgPickaxe7Image;
};

datablock ShapeBaseImageData(rpgPickaxe7Image : rpgPickaxeImage)
{
   item = rpgPickaxe7Item;
   colorShiftColor = "0 0.95 0.1 1.000";
   stateTimeoutValue[3]            = 0.175;
};

function rpgPickaxe7Image::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armattack);
}

function rpgPickaxe7Image::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}

function rpgPickaxe7Image::onFire(%this,%obj,%slot)
{
	swingPickaxe(%obj, %this.getname());
}



datablock ItemData(rpgPickaxe8Item : rpgPickaxeItem)
{
	miningpower = 130;
	uiName = "Diamond Pickaxe";
	colorShiftColor = "0 0.9 1 1.000";
	image = rpgPickaxe8Image;
};

datablock ShapeBaseImageData(rpgPickaxe8Image : rpgPickaxeImage)
{
   item = rpgPickaxe8Item;
   colorShiftColor = "0 0.9 1 1.000";
   stateTimeoutValue[3]            = 0.15;
};

function rpgPickaxe8Image::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armattack);
}

function rpgPickaxe8Image::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}

function rpgPickaxe8Image::onFire(%this,%obj,%slot)
{
	swingPickaxe(%obj, %this.getname());
}



datablock ItemData(rpgPickaxe9Item : rpgPickaxeItem)
{
	shapeFile = "./Lil_Pickaxe2.dts";
	miningpower = 175;
	uiName = "Titanium Pickaxe";
	colorShiftColor = "0.95 0.95 0.95 1.000";
	image = rpgPickaxe9Image;
};

datablock ShapeBaseImageData(rpgPickaxe9Image : rpgPickaxeImage)
{
	shapeFile = "./Lil_Pickaxe2.dts";
   item = rpgPickaxe9Item;
   colorShiftColor = "0.95 0.95 0.95 1.000";
   stateTimeoutValue[3]            = 0.15;
};

function rpgPickaxe9Image::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armattack);
}

function rpgPickaxe9Image::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}

function rpgPickaxe9Image::onFire(%this,%obj,%slot)
{
	swingPickaxe(%obj, %this.getname());
}


datablock ItemData(rpgPickaxe10Item : rpgPickaxeItem)
{
	shapeFile = "./Lil_Pickaxe2.dts";
	miningpower = 225;
	uiName = "Uranium Pickaxe";
	colorShiftColor = "0 1 0 1.000";
	image = rpgPickaxe10Image;
};

datablock ShapeBaseImageData(rpgPickaxe10Image : rpgPickaxeImage)
{
	shapeFile = "./Lil_Pickaxe2.dts";
   item = rpgPickaxe10Item;
   colorShiftColor = "0 1 0 1.000";
   stateTimeoutValue[3]            = 0.15;
};

function rpgPickaxe10Image::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armattack);
}

function rpgPickaxe10Image::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}

function rpgPickaxe10Image::onFire(%this,%obj,%slot)
{
	swingPickaxe(%obj, %this.getname());
}



datablock ItemData(rpgPickaxe11Item : rpgPickaxeItem)
{
	shapeFile = "./Lil_Pickaxe2.dts";
	miningpower = 275;
	uiName = "Chromodium Pickaxe";
	colorShiftColor = "0.45 0.85 0.6 1.000";
	image = rpgPickaxe11Image;
};

datablock ShapeBaseImageData(rpgPickaxe11Image : rpgPickaxeImage)
{
	shapeFile = "./Lil_Pickaxe2.dts";
   item = rpgPickaxe11Item;
   colorShiftColor = "0.45 0.85 0.6 1.000";
   stateTimeoutValue[3]            = 0.15;
};

function rpgPickaxe11Image::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armattack);
}

function rpgPickaxe11Image::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}

function rpgPickaxe11Image::onFire(%this,%obj,%slot)
{
	swingPickaxe(%obj, %this.getname());
}



datablock ItemData(rpgPickaxe12Item : rpgPickaxeItem)
{
	shapeFile = "./Lil_Pickaxe2.dts";
	miningpower = 335;
	uiName = "Luminite Pickaxe";
	colorShiftColor = "0 0.95 0.65 1.000";
	image = rpgPickaxe12Image;
};

datablock ShapeBaseImageData(rpgPickaxe12Image : rpgPickaxeImage)
{
	shapeFile = "./Lil_Pickaxe2.dts";
   item = rpgPickaxe12Item;
   colorShiftColor = "0 0.95 0.65 1.000";
   stateTimeoutValue[3]            = 0.14;
};

function rpgPickaxe12Image::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armattack);
}

function rpgPickaxe12Image::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}

function rpgPickaxe12Image::onFire(%this,%obj,%slot)
{
	swingPickaxe(%obj, %this.getname());
}



datablock ItemData(rpgPickaxe13Item : rpgPickaxeItem)
{
	shapeFile = "./Lil_Pickaxe2.dts";
	miningpower = 400;
	uiName = "Aurium Pickaxe";
	colorShiftColor = "1 0.95 0 1.000";
	image = rpgPickaxe13Image;
};

datablock ShapeBaseImageData(rpgPickaxe13Image : rpgPickaxeImage)
{
	shapeFile = "./Lil_Pickaxe2.dts";
   item = rpgPickaxe12Item;
   colorShiftColor = "1 0.95 0 1.000";
   stateTimeoutValue[3]            = 0.14;
};

function rpgPickaxe13Image::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armattack);
}

function rpgPickaxe13Image::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}

function rpgPickaxe13Image::onFire(%this,%obj,%slot)
{
	swingPickaxe(%obj, %this.getname());
}



datablock ItemData(rpgPickaxe14Item : rpgPickaxeItem)
{
	shapeFile = "./Lil_Pickaxe.dts";
	miningpower = 485;
	uiName = "Dragonstone Pickaxe";
	colorShiftColor = "1 0.2 0.05 1.000";
	image = rpgPickaxe14Image;
};

datablock ShapeBaseImageData(rpgPickaxe14Image : rpgPickaxeImage)
{
	shapeFile = "./Lil_Pickaxe.dts";
   item = rpgPickaxe14Item;
   colorShiftColor = "1 0.2 0.05 1.000";
   stateTimeoutValue[3]            = 0.14;
};

function rpgPickaxe14Image::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armattack);
}

function rpgPickaxe14Image::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}

function rpgPickaxe14Image::onFire(%this,%obj,%slot)
{
	swingPickaxe(%obj, %this.getname());
}



datablock ItemData(rpgPickaxe15Item : rpgPickaxeItem)
{
	shapeFile = "./Lil_Pickaxe3.dts";
	miningpower = 575;
	uiName = "Shadowlight Pickaxe";
	colorShiftColor = "1 1 1 1.000";
	image = rpgPickaxe15Image;
};

datablock ShapeBaseImageData(rpgPickaxe15Image : rpgPickaxeImage)
{
	shapeFile = "./Lil_Pickaxe3.dts";
   item = rpgPickaxe15Item;
   colorShiftColor = "1 1 1 1.000";
   stateTimeoutValue[3]            = 0.14;
};

function rpgPickaxe15Image::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armattack);
}

function rpgPickaxe15Image::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}

function rpgPickaxe15Image::onFire(%this,%obj,%slot)
{
	swingPickaxe(%obj, %this.getname());
}



datablock ItemData(rpgPickaxe16Item : rpgPickaxeItem)
{
	shapeFile = "./Lil_Pickaxe.dts";
	miningpower = 700;
	uiName = "Uelibloom Pickaxe";
	colorShiftColor = "0.35 0.85 0.15 1.000";
	image = rpgPickaxe16Image;
};

datablock ShapeBaseImageData(rpgPickaxe16Image : rpgPickaxeImage)
{
	shapeFile = "./Lil_Pickaxe.dts";
   item = rpgPickaxe16Item;
   colorShiftColor = "0.35 0.85 0.15 1.000";
   stateTimeoutValue[3]            = 0.13;
};

function rpgPickaxe16Image::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armattack);
}

function rpgPickaxe16Image::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}

function rpgPickaxe16Image::onFire(%this,%obj,%slot)
{
	swingPickaxe(%obj, %this.getname());
}



datablock ItemData(rpgPickaxe17Item : rpgPickaxeItem)
{
	shapeFile = "./Lil_Pickaxe2.dts";
	miningpower = 875;
	uiName = "Brimstone Pickaxe";
	colorShiftColor = "1 0 0 1.000";
	image = rpgPickaxe17Image;
};

datablock ShapeBaseImageData(rpgPickaxe17Image : rpgPickaxeImage)
{
	shapeFile = "./Lil_Pickaxe2.dts";
   item = rpgPickaxe17Item;
   colorShiftColor = "1 0 0 1.000";
   stateTimeoutValue[3]            = 0.13;
};

function rpgPickaxe17Image::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armattack);
}

function rpgPickaxe17Image::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}

function rpgPickaxe17Image::onFire(%this,%obj,%slot)
{
	swingPickaxe(%obj, %this.getname());
}



datablock ItemData(rpgPickaxe18Item : rpgPickaxeItem)
{
	shapeFile = "./Lil_Pickaxe2.dts";
	miningpower = 1200;
	uiName = "Auric Tesla Pickaxe";
	colorShiftColor = "1 0.65 0.05 1.000";
	image = rpgPickaxe18Image;
};

datablock ShapeBaseImageData(rpgPickaxe18Image : rpgPickaxeImage)
{
	shapeFile = "./Lil_Pickaxe2.dts";
   item = rpgPickaxe18Item;
   colorShiftColor = "1 0.65 0.05 1.000";
   stateTimeoutValue[3]            = 0.125;
};

function rpgPickaxe18Image::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armattack);
}

function rpgPickaxe18Image::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}

function rpgPickaxe18Image::onFire(%this,%obj,%slot)
{
	swingPickaxe(%obj, %this.getname());
}

datablock ItemData(rpgPickaxe19Item : rpgPickaxeItem)
{
	shapeFile = "./Lil_Pickaxe2.dts";
	miningpower = 1650;
	uiName = "Stellarite Pickaxe";
	colorShiftColor = "0 0.15 1 1.000";
	image = rpgPickaxe19Image;
};

datablock ShapeBaseImageData(rpgPickaxe19Image : rpgPickaxeImage)
{
	shapeFile = "./Lil_Pickaxe2.dts";
   item = rpgPickaxe19Item;
   colorShiftColor = "0 0.15 1 1.000";
   stateTimeoutValue[3]            = 0.125;
};

function rpgPickaxe19Image::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armattack);
}

function rpgPickaxe19Image::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}

function rpgPickaxe19Image::onFire(%this,%obj,%slot)
{
	swingPickaxe(%obj, %this.getname());
}

datablock ItemData(rpgPickaxe20Item : rpgPickaxeItem)
{
	shapeFile = "./Lil_Pickaxe2.dts";
	miningpower = 2250;
	uiName = "Cryolite Pickaxe";
	colorShiftColor = "0 0.8 0.9 1.000";
	image = rpgPickaxe20Image;
};

datablock ShapeBaseImageData(rpgPickaxe20Image : rpgPickaxeImage)
{
	shapeFile = "./Lil_Pickaxe2.dts";
   item = rpgPickaxe20Item;
   colorShiftColor = "0 0.8 0.9 1.000";
   stateTimeoutValue[3]            = 0.125;
};

function rpgPickaxe20Image::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armattack);
}

function rpgPickaxe20Image::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}

function rpgPickaxe20Image::onFire(%this,%obj,%slot)
{
	swingPickaxe(%obj, %this.getname());
}

datablock ItemData(rpgPickaxe21Item : rpgPickaxeItem)
{
	shapeFile = "./Lil_Pickaxe2.dts";
	miningpower = 3000;
	uiName = "Reality Pickaxe";
	colorShiftColor = "1 1 1 1.000";
	image = rpgPickaxe21Image;
};

datablock ShapeBaseImageData(rpgPickaxe21Image : rpgPickaxeImage)
{
	shapeFile = "./Lil_Pickaxe2.dts";
   item = rpgPickaxe20Item;
   colorShiftColor = "1 1 1 1.000";
   stateTimeoutValue[3]            = 0.12;
};

function rpgPickaxe21Image::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armattack);
}

function rpgPickaxe21Image::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}

function rpgPickaxe21Image::onFire(%this,%obj,%slot)
{
	swingPickaxe(%obj, %this.getname());
}