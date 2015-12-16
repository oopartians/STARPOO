using UnityEngine;
using System.Collections;

public class GameValueSetter : MonoBehaviour {
	
	public static int groundSize = 60;
	public static int numShipsPerFleet = 6;

	
	public void SetGroundSize(string size){
		groundSize = int.Parse(size);
	}
	public void SetNumShipsPerFleet(string num){
		numShipsPerFleet = int.Parse(num);
	}
}
