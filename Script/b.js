

for (var i = allyShips.length - 1; i >= 0; i--) {
	// allyShips[i].shoot()
	ship = allyShips[i]
	ship.shoot()
	ship.setSpeed(5)
	// log(JSON.stringify(allyShips[i]))
	// allyShips[i].name = "a"
};

//enemyShips : array<ship>
//allyShips : array<ship>
//bullets : array<bulley>


//ship : x,y,rotation,hp
//bullet : x,y,speed,rotation

//function
//GetPos() return {x,y}
//Shoot() <- 총알쏨
//SetShipSpeed(number) : 배의 전진속력(0~5)
//SetShipAngleSpeed(number) : 회전속도()