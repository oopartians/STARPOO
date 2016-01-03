

for (var i = myShips.length - 1; i >= 0; i--) {
	// myShips[i].shoot()
	ship = myShips[i]
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
	// if(i == 0){
	// 	log(ship.angle)
	// 	log(p.angle)
	// }
	ship.setAngleSpeed(360)
	// if(p.angle > 90){
	// 	ship.setAngleSpeed(-5)
	// }
	// else if(p.angle < 90){
	// 	ship.setAngleSpeed(5)
	// }
	// log(JSON.stringify(myShips[i]))
	// myShips[i].name = "a"
};

function update(){}

//aaa