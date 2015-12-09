using UnityEngine;
using Jurassic;
using Jurassic.Library;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.IO;

public class AILoader : MonoBehaviour {

    public string stringCode;
	public string floatingText = "";
	
	SpaceShipHandler ship;
	ScriptEngine engine;
	
	ArrayInstance allyShipsJS;
	ArrayInstance enemyShipsJS;
	ArrayInstance bulletsJS;

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
		engine.SetGlobalValue("bullets", bulletsJS);
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
		allyShipsJS = engine.Array.New();
		enemyShipsJS = engine.Array.New();
		bulletsJS = engine.Array.New();
	}

	// Update is called once per frame
	void FixedUpdate () {
		ExportCollectionToJS (ship.fleet.team.aiInfor.allyShips, allyShipsJS);
		ExportCollectionToJS (ship.fleet.team.aiInfor.scannedEnemyShips.Keys, enemyShipsJS);
		ExportCollectionToJS (ship.fleet.team.aiInfor.scannedBullets.Keys, bulletsJS);
        //Execute the contents of the script every frame if Running is ticked.
        engine.Execute(stringCode);
	}

	void ExportCollectionToJS(ICollection<IJSONExportable> col, ArrayInstance arrjs){
		arrjs.Length = (uint)col.Count;
		int i = 0;
		foreach(IJSONExportable obj in col)
		{
			var json = arrjs[i++] as ObjectInstance;
			if(json == null){
				json = (arrjs[i-1] = ObjectConstructor.Create(engine,engine.Object.InstancePrototype)) as ObjectInstance;
			}

			Dictionary<string,double> values = obj.GetExportableValues();
			foreach(string key in values.Keys){
				json.SetPropertyValue(key,values[key],true);
			}
		}
	}
	
	public void SetJavaScriptPath(string path)
	{
		stringCode = File.ReadAllText(path);
		Debug.Log("stringCode : " + stringCode);
	}
}
