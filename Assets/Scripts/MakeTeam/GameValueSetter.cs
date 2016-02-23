using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameValueSetter : MonoBehaviour {
	
	public static int groundSize = 60;
	public static int numShipsPerFleet = 16;
	public static bool paused = false;
	public static bool fogmode = false;

    public Text groundSizeText;
    public Text numShipPerFleetText;
    public InputField groundSizeInput;
    public InputField numShipPerFleetInput;

    void Start()
    {
        groundSizeText.text = groundSize.ToString();
        numShipPerFleetText.text = numShipsPerFleet.ToString();
        groundSizeInput.text = groundSize.ToString();
        numShipPerFleetInput.text = numShipsPerFleet.ToString();
    }

	public void SetGroundSize(string size){
		groundSize = int.Parse(size);
	}
	public void SetNumShipsPerFleet(string num){
		numShipsPerFleet = int.Parse(num);
	}
}
