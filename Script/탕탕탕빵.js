var shipSpeedMax = 5
var bulletSpeed = 10
var funckinglog = ''

function update()
{
	
	for(var i = myShips.length - 1; i >= 0; i--)
	{
		var boolHi = false
		// funckinglog += 'Ship num: ' 
		// 				+ i.toString() 
		// 				+ ', speed: ' 
		// 				+ myShips[i].speed 
		// 				+ ', angle : ' 
		// 				+ myShips[i].angle 
		// 				+ ', '
		if(bullets.length > 0)
		{
			var angleSpeed = 0;
			for(var j = 0; j < bullets.length; j++)
			{
				if(checkDanger(myShips[i], bullets[j])) {
					angleSpeed = 180;
					boolHi = true;
					break;
				}
			}
			myShips[i].setAngleSpeed(angleSpeed);
		}
		myShips[i].setSpeed(5);

		myShips[i].shoot();
		// myShips[i].setAngleSpeed(360);
		// 첫째 함선 주변에 총알 정보를 통해 위험한지 아닌지 파악후 이동

		// 현재 미사일을 발사할수있는지 여부 체크
			// 만약 위험한게 아니라면 레이더상에 적을 탐지하여 예측경로에 shoot

		// 
		//if(boolHi)
			//log('Hi');
		//funckinglog += '\n';
	}
	//if(funckinglog)
		//log(funckinglog);
}

function checkDanger(ship, bullet)
{
	if(ship.angle == bullet.angle)
	{
		// TODO: 자기 자신이 발사한 총알 예외 처리 및 뒤에서 날라오는 총알 회피 기동

		return true
	}
	else if(ship.anlge == -bullet.angle)
	{
		// TODO: 마찬가지로 정면으로 오는거랑 이미 지나간것 체크
		return true
	}
	else
	 {
		//funckinglog += new String('ship info : ' + ship.speed.toString() + ', ')
		var expectShip = getExpectPos(ship, 5);
		// funckinglog += new String('expectShip info#\n speed: ' + expectShip.speed.toString() 
		// 	+ ', x: ' + expectShip.x + ', y: ' + expectShip.y + '\n')
		var expectBullet = getExpectPos(bullet, 5);
		//funckinglog += new String('bullet : x: ' + expectBullet.x + ', y: ' + expectBullet.y + '\n')

		var a = (expectShip.x - expectBullet.x);
		var b = (expectShip.y - expectBullet.y);
		var distance = Math.sqrt(a*a +b*b);
		//funckinglog += 'a: ' + a.toString())
		funckinglog += new String('distance : ' + distance)
		if(distance < 10)
			return true
		return false
	}
}

// 재귀를 통해 count Frame 이후 위치를 찾는다.
// TODO: 나중에 재귀가아닌 한번의 계산으로 찾는 함수 구현 필요.
function getExpectPos(object, count)
{
	var expectObject = new Object();
	expectObject.x = object.x
	expectObject.y = object.y
	expectObject.speed = object.speed 
	expectObject.angle = object.angle

	if(count>0)
	{
		expectObject.x = object.x + sin(object.angle) * object.speed * dt
		expectObject.y = object.y + cos(object.angle) * object.speed * dt
		return getExpectPos(expectObject, --count)
	}

	return {x: expectObject.x, y: expectObject.y ,speed: expectObject.speed, angle: expectObject.angle}
}

function a2r(angle){
	return angle*Math.PI/180;
}

function cos(angle){
	return Math.cos(a2r(angle));
}

function sin(angle){
	return Math.sin(a2r(angle));
}