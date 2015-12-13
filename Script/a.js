

for (var i = allyShips.length - 1; i >= 0; i--) {
	// allyShips[i].shoot()
	ship = allyShips[i]
	ship.shoot()
	ship.setSpeed(5)

	p = polarFrom(ship,{x:0,y:0});
	p2 = polarFrom({x:0,y:0},ship);
	// if(ship.angle != null){
	// 	log("ship.angle : "+ship.angle)
	// }
	// else{
	// 	log("ship.angle not exist")
	// }
	// p = polar(ship)
	if(i == 0){
		log(ship.angle)
		log(p.angle)
	}
	ship.setAngleSpeed(-p.angle)
	// if(p.angle > 90){
	// 	ship.setAngleSpeed(-5)
	// }
	// else if(p.angle < 90){
	// 	ship.setAngleSpeed(5)
	// }
	// log(JSON.stringify(allyShips[i]))
	// allyShips[i].name = "a"
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