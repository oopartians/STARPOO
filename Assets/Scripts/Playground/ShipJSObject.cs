using UnityEngine;
using System.Collections;
using Jurassic;
using Jurassic.Library;

public class ShipJSObject : MonoBehaviour {
	public JSObject jsobj;
	FleetAILoader fleetAILoader;
	Ship ship;
	// Use this for initialization
	public void Create(){
		ship = GetComponent<Ship> ();
		fleetAILoader = ship.fleet.GetComponent<FleetAILoader>();
		                                             
		jsobj = new JSObject(fleetAILoader.GetEngine(),ship);
		Debug.Log ("!!!");
	}

	void OnDisable(){

	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

//	public class jsShip : ObjectInstance
//	{
//		public jsShip(ScriptEngine engine)
//		{
//			
//		}
//	}

	
	public void UpdateProperties(){
		jsobj.UpdateProperties ();
	}
	public void UpdateProperty(string key, float value){
		jsobj.UpdateProperty(key,value);
	}

	public class JSObject : ObjectInstance
	{
		Ship ship;
		public JSObject(ScriptEngine engine,Ship ship)
			: base(engine)
		{
			this.ship = ship;
			// Read-write property (name).
			this["name"] = "Test Application";
			
			// Read-only property (version).
			this.DefineProperty("version", new PropertyDescriptor(5, PropertyAttributes.Writable), true);
			this["x"] = 666;
			this["y"] = 666;
			this["angle"] = 666;
			this["speed"] = 666;
			this["angleSpeed"] = 666;
			this["hp"] = 666;
			this["ammo"] = 666;
			this.PopulateFunctions();
		}
		
		[JSFunction(Name = "shoot")]
		public void Shoot()
		{
			ship.Shoot();
		}
		[JSFunction(Name = "setSpeed")]
		public void SetSpeed(double speed)
		{
			ship.SetSpeed ((float)speed);
		}
		[JSFunction(Name = "setAngleSpeed")]
		public void SetAngleSpeed(double angleSpeed)
		{
			ship.SetAngleSpeed ((float)angleSpeed);
		}

		public void UpdateProperties(){
			UpdateProperty ("x", ship.GetPos ().x);
			UpdateProperty ("y", ship.GetPos ().y);
			UpdateProperty ("angle", ship.angle);
			UpdateProperty ("speed", ship.speed);
			UpdateProperty ("angleSpeed", ship.angleSpeed);
			UpdateProperty ("hp", ship.hp);
			UpdateProperty ("ammo", ship.ammo);
		}
		public void UpdateProperty(string key, float value){
			this[key] = (double)value;
		}
	}
}
