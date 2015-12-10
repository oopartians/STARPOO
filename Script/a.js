SetShipSpeed(5);
if(enemyShips.length > 0){
	Shoot();
}
var pos = GetPos();
// log('x : '+pos.x+', y : '+pos.y);
// log(allyShips[0].x);
// log(allyShips.length);


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