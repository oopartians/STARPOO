

for (var i = allyShips.length - 1; i >= 0; i--) {
	// allyShips[i].shoot()
	ship = allyShips[i]
	ship.shoot()
	ship.setSpeed(5)

	var goodR = 20;
	var goodAngle = 90;

	p = polarFrom(ship,{x:0,y:0});

	if(p.r > goodR){
		goodAngle = 0;
	}
		log(p.angle)
	ship.setAngleSpeed((p.angle-goodAngle)*50);
};

//enemyShips : array<ship>
//allyShips : array<ship>
//bullets : array<bulley>


//ship : x,y,angle,hp
//bullet : x,y,speed,angle

//allyShip : x,y,angle,hp,

//function
//shoot() <- 총알쏨
//setSpeed(number) : 배의 전진속력(0~5)
//setAngleSpeed(number) : 회전속도()

//polarFrom(center,target) -> angle, r