using UnityEngine;
using Jurassic;
using Jurassic.Library;
using System.Collections;
using System.IO;

public class AILoader : MonoBehaviour {

    public string stringCode;
	public string floatingText = "";
	
	SpaceShipHandler ship;
	ScriptEngine engine;
	
	ArrayInstance allyShipsJS;
	ArrayInstance enemyShipsJS;

    //public System.Json.JsonObject ships;
	 	
	void Awake(){
		ship = GetComponent<SpaceShipHandler>();
	}

    void Start()
	{
		engine = new ScriptEngine();
		CacheJSValues();


        // Create an instance of the Jurassic engine then expose some stuff to it.
        engine.SetGlobalValue("console", new Jurassic.Library.FirebugConsole(engine));

        // Arguments and returns of functions exposed to JavaScript must be of supported types.
        // Supported types are bool, int, double, string, Jurassic.Null, Jurassic.Undefined
        // and Jurassic.Library.ObjectInstance (or a derived type).
        // More info: http://jurassic.codeplex.com/wikipage?title=Supported%20types

        // Examples of exposing some static classes to JavaScript using Jurassic's "seamless .NET interop" feature.
        engine.EnableExposedClrTypes = true; // You must enable this in order to use interop feaure.

        // engine.SetGlobalValue("Mathf", typeof(Mathf));
        // engine.SetGlobalValue("Input", typeof(Input));
        // engine.SetGlobalValue("Transform", typeof(Transform));

		/////////////////////////////////////////////// HERE HAS PROBLEM. //////////////////////////////////////////////////
        engine.SetGlobalValue("allyShips", allyShipsJS);
        engine.SetGlobalValue("enemyShips", enemyShipsJS);
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Exposing .NET methods to JavaScript.
        // The generic System.Action delegate is used to define method signatures with no returns;
        engine.SetGlobalFunction("SetShipSpeed", new System.Action<double>(SetShipSpeed));
        engine.SetGlobalFunction("SetShipAngleSpeed", new System.Action<double>(SetShipAngleSpeed));
		engine.SetGlobalFunction("Shoot", new System.Action(Shoot));
		engine.SetGlobalFunction("log", new System.Action<string>(Log));

        // Examples of exposing some .NET methods with return values to JavaScript.
		// The generic System.Func delegate is used to define method signatures with return types;
		engine.SetGlobalFunction("GetPos", new System.Func<jsVectorInstance>(GetPos));
        //engine.SetGlobalFunction("GetX", new System.Func<double>(jsGetX));
        //engine.SetGlobalFunction("GetY", new System.Func<double>(jsGetY));
        //engine.SetGlobalFunction("GetZ", new System.Func<double>(jsGetZ));
        //engine.SetGlobalFunction("GetText", new System.Func<string>(jsGetText));

        // Example of creating a static .NET class to expose to JavaScript
        //engine.SetGlobalValue("Days", new jsDayIterator(engine));

        // Example of creating an instance class with a constructor in JavaScript
        //engine.SetGlobalValue("Vector", new jsVectorConstructor(engine));
        //Debug.Log("end Awake");
        //ships = new System.Json.JsonObject();
    }


    #region JS Functions
    // This set of methods implment the functions we exposed to javaScript.

    public jsVectorInstance GetPos()
    {
        return new jsVectorConstructor(engine).Construct(
            (double)ship.GetPos().x, 
            (double)ship.GetPos().y, 
            (double)ship.GetPos().z
            );
    }

    public void SetShipSpeed(double x)
	{
        ship.SetSpeed((float)x);
    }

    public void SetShipAngleSpeed(double aAngleSpeed)
    {
        ship.SetAngleSpeed((float)aAngleSpeed);
    }

    public void Shoot()
    {
        ship.Shoot();
    }

	public void Log(string str){
		Debug.Log("JS LOG : "+str);
	}

    #endregion

    #region Implementation of our custom JavaScript Vector class
    // Objects that can be instantiated, like the built-in Number, String, Array and RegExp objects, require two .NET classes,
    // one for the constructor and one for the instance. For more info see "Building an instance class" here:
    // http://jurassic.codeplex.com/wikipage?title=Exposing%20a%20.NET%20class%20to%20JavaScript

    //This is the constructor class for the JS Vector class.
    public class jsVectorConstructor : ClrFunction
    {
        public jsVectorConstructor(ScriptEngine engine)
            : base(engine.Function.InstancePrototype, "Vector", new jsVectorInstance(engine.Object.InstancePrototype))
        { }

        //The JSConstructorFunction attribute marks the method that is called when the new operator is used to create an instance in a JavaScript
        [JSConstructorFunction]
        public jsVectorInstance Construct(double x, double y, double z)
        {
            return new jsVectorInstance(this.InstancePrototype, x, y, z);
        }
    }

    //This is the instance class for the JS Vector class.
    public class jsVectorInstance : ObjectInstance
    {
        public jsVectorInstance(ObjectInstance prototype)
            : base(prototype)
        {
            this.PopulateFunctions();
        }

        public jsVectorInstance(ObjectInstance prototype, double x, double y, double z) : base(prototype)
        {
            this.SetPropertyValue("x", x, true);
            this.SetPropertyValue("y", y, true);
            this.SetPropertyValue("z", z, true);
        }

        [JSFunction]
        public void Reset()
        {
            this.SetPropertyValue("x", 0, true);
            this.SetPropertyValue("y", 0, true);
            this.SetPropertyValue("z", 0, true);
        }
    }

    #endregion

	void CacheJSValues(){
		int i = 0;
		allyShipsJS = engine.Array.New();
		foreach(SpaceShipHandler allyShip in ship.fleet.team.allyShips)
		{
			var allyShipJS = ObjectConstructor.Create(engine,engine.Object.InstancePrototype);
			allyShipsJS[i++] = allyShipJS;
		}
		i = 0;
		enemyShipsJS = engine.Array.New();
		foreach(SpaceShipHandler enemyShip in ship.fleet.team.scannedEnemyShips)
		{
			var enemyShipJS = ObjectConstructor.Create(engine,engine.Object.InstancePrototype);
			enemyShipsJS[i++] = enemyShipJS;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		int i = 0;
		allyShipsJS.Length = (uint)ship.fleet.team.allyShips.Count;
		foreach(SpaceShipHandler allyShip in ship.fleet.team.allyShips)
		{
			var allyShipJS = allyShipsJS[i++] as ObjectInstance;
			var allyShipPos = allyShip.GetPos();
			allyShipJS.SetPropertyValue("x",engine.Number.Construct((double)allyShipPos.x),true);
			allyShipJS.SetPropertyValue("y",engine.Number.Construct((double)allyShipPos.y),true);
			allyShipJS.SetPropertyValue("rotation",engine.Number.Construct((double)allyShip.angle),true);
			allyShipJS.SetPropertyValue("hp",engine.Number.Construct((double)allyShip.hp),true);
		}

		i = 0;
		enemyShipsJS.Length = (uint)ship.fleet.team.scannedEnemyShips.Count;
		foreach(SpaceShipHandler enemyShip in ship.fleet.team.scannedEnemyShips)
		{
			var enemyShipJS = enemyShipsJS[i++] as ObjectInstance;
			var enemyShipPos = enemyShip.GetPos();
			enemyShipJS.SetPropertyValue("x",engine.Number.Construct((double)enemyShipPos.x),true);
			enemyShipJS.SetPropertyValue("y",engine.Number.Construct((double)enemyShipPos.y),true);
			enemyShipJS.SetPropertyValue("rotation",engine.Number.Construct((double)enemyShip.angle),true);
			enemyShipJS.SetPropertyValue("hp",engine.Number.Construct((double)enemyShip.hp),true);
		}
        //Execute the contents of the script every frame if Running is ticked.
        engine.Execute(stringCode);
	}
	
	public void SetJavaScriptPath(string path)
	{
		stringCode = File.ReadAllText(path);
		Debug.Log("stringCode : " + stringCode);
	}
}
