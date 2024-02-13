datablock ParticleData(blockheadprimeprojectiletrailParticle)
{
	dragCoefficient = 5;
	gravityCoefficient = 0;
	inheritedVelFactor = 0;
	constantAcceleration = 0;
	lifetimeMS = 1000;
	lifetimeVarianceMS = 100;
	textureName = "./trail.png";
	spinSpeed = 10;
	spinRandomMin = -200;
	spinRandomMax = 200;
	useInvAlpha = false;
	colors[0] = "1 1 0 0.9";
	colors[1] = "0.2 0.6 0.4 0.8";
	colors[2] = "0 0.25 0.5 0.8";
	sizes[0] = 0.3;
	sizes[1] = 0.5;
	sizes[2] = 0.6;
	times[0] = 0;
	times[1] = 0.2;
	times[2] = 0.4;
};
datablock ParticleEmitterData(blockheadprimeprojectiletrailEmitter)
{
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;
	ejectionVelocity = 0;
	velocityVariance = 0;
	ejectionOffset = 0.2;
	thetaMin = 135;
	thetaMax = 180;
	phiReferenceVel = 0;
	phiVariance = 360;
	overrideAdvance = false;
	particles = blockheadprimeprojectiletrailParticle;
};

datablock ParticleData(blockheadprimeprojectileExplosionParticle)
{
   dragCoefficient = 8;
   windCoefficient = 0;
   gravityCoefficient = -0.2;
   inheritedVelFactor = 0;
   constantAcceleration = 0;
   lifetimeMS = 1500;
   lifetimeVarianceMS = 35;
   spinSpeed = 10;
   spinRandomMin = -500;
   spinRandomMax = 500;
   useInvAlpha = false;
   textureName = "base/data/particles/cloud";
   colors[0] = "0 1 1 0.8";
   colors[1] = "1 1 1 0.8";
   sizes[0] = 5;
   sizes[1] = 0;
   times[0] = 0;
   times[1] = 1;
};
datablock ParticleEmitterData(blockheadprimeprojectileExplosionEmitter)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = 0;
   velocityVariance = 0;
   ejectionOffset = 0;
   thetaMin = 0;
   thetaMax = 90;
   phiReferenceVel = 0;
   phiVariance = 360;
   particles = blockheadprimeprojectileExplosionParticle;
   lifetimeMS = 50;
   lifetimeVarianceMS = 0;
};

datablock ExplosionData(blockheadprimeprojectileExplosion : GravityRocketExplosion)
{
    particleEmitter = blockheadprimeprojectileExplosionEmitter;
    particleDensity = 10;
    particleRadius = 0.2;

	damageRadius = 6;
	radiusDamage = 25;
	impulseRadius = 6;
	impulseForce = 2000;
};

AddDamageType("blockheadprimeprojectile", '<bitmap:base/client/ui/ci/generic> %1', '%2 <bitmap:base/client/ui/ci/generic> %1', 1, 1);

datablock ProjectileData(blockheadprimeProjectile)
{
	projectileShapeName = "./projectile.dts";
	directDamage = 0;
	directDamageType = $DamageType::blockheadprimeprojectile;
	radiusDamageType = $DamageType::blockheadprimeprojectile;

	brickExplosionRadius = 2;
	brickExplosionImpact = true;
	brickExplosionForce  = 20;
	brickExplosionMaxVolume = 20;
	brickExplosionMaxVolumeFloating = 30;

	impactImpulse = 0;
	verticalImpulse = 0;
	explosion = blockheadprimeprojectileExplosion;
	particleEmitter = blockheadprimeprojectiletrailEmitter;

	muzzleVelocity = 50;
	velInheritFactor = 0;

	armingDelay = 0;
	lifetime = 4000;
	fadeDelay = 3500;
	bounceElasticity = 0.5;
	bounceFriction = 0.20;
	isBallistic = false;
	gravityMod = 0;

	hasLight = true;
	lightRadius = 2;
	lightColor = "1 1 0";

    uiname = "blockhead prime projectile";
};

package projectilehoming
{
	function Projectile::onAdd(%obj, %a, %b)
	{
		Parent::onAdd(%obj, %a, %b);
		if(%obj.getDatablock() == blockheadprimeProjectile.getID())
		{
			if(!%obj.doneblockheadprimeDelay)
			{
				%obj.schedule(250, spawnblockheadprimeprojectile);
			}
			else
			{
				%obj.schedule(100, spawnblockheadprimeprojectile);
			}
		}
	}
};
activatePackage(projectilehoming);

function Projectile::spawnblockheadprimeprojectile(%this)
{
	if(vectorLen(%this.getVelocity()) == 0)
	{
		return;
	}	
	%client = %this.client;
	%muzzle = vectorLen(%this.getVelocity());
	if(!isObject(%this.target) || %this.target.getState() $= "Dead" || %this.target.getMountedImage(0) == adminWandImage.getID() || vectorDist(%this.getPosition(),%this.target.getHackPosition()) > 30)
	{
		%pos = %this.getPosition();
		%radius = 300;
		%searchMasks = $TypeMasks::PlayerObjectType;
		InitContainerRadiusSearch(%pos, %radius, %searchMasks);
		%minDist = 1000;
		while(%searchObj = containerSearchNext())
		{
				if(%searchObj == %this.sourceObject)
				{
					continue;
				}				
				%d = vectorDist(%pos,%searchObj.getPosition());
				if(%d < %minDist)
				{
					%minDist = %d;
					%found = %searchObj;
				}
		}		
		if(isObject(%found))
		{
			%this.target = %found;
		}
	}
	%pos = %this.getPosition();
	%start = %pos;
	if(!%notarget)
	{
		%found = %this.target;
		%end = %found.getHackPosition();
	}
	%enemypos = %end;
	%vec = vectorNormalize(vectorSub(%end,%start));
	for(%i=0;%i<5;%i++)
	{
		%t = vectorDist(%start,%end) / vectorLen(vectorScale(getWord(%vec,0) SPC getWord(%vec,1),%muzzle));
		%velaccl = vectorScale(%accl,%t);
		%x = getword(%velaccl,0);
		%y = getword(%velaccl,1);
		%z = getWord(%velaccl,2);
		%x = (%x < 0 ? 0 : %x);
		%y = (%y < 0 ? 0 : %y);
		%z = (%z < 0 ? 0 : %z);
		if(!%notarget)
		{
			%vel = vectorAdd(vectorScale(%found.getVelocity(),%t),%x SPC %y SPC %z);
		}
		%end = vectorAdd(%enemypos,%vel);
		%vec = vectorNormalize(vectorSub(%end,%start));
	}
	%addVec = vectorAdd(%this.getVelocity(),vectorScale(%vec,180/vectorDist(%pos,%end)*(%muzzle/getrandom(20,40))));
	%vec = vectorNormalize(%addVec);
	%p = new Projectile()
	{
		dataBlock = %this.dataBlock;
		initialPosition = %pos;
		initialVelocity = vectorScale(%vec,%muzzle);
		sourceObject = %this.sourceObject;
		client = %this.client;
		sourceSlot = 0;
		originPoint = %this.originPoint;
		doneblockheadprimeDelay = 1;
		target = %this.target;
		reflectTime = %this.reflectTime;
	};
	if(isObject(%p))
	{
		dungeoncleanupgroup.add(%p);
		%p.setScale(%this.getScale());
		%this.delete();
	}
}