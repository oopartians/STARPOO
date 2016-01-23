using UnityEngine;
using UnityEngine.Events;
using Jurassic;
using Jurassic.Library;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.IO;
using System;


public class FleetAILoader : MonoBehaviour {

	public string code;
	public bool isMine;
	public string consoleText;
	[Serializable]
	public class UnityEventString : UnityEvent<string>{};
	public UnityEventString onLog = new UnityEventString();

	public ScriptEngine GetEngine(){return engine;}


	public Fleet fleet;
	TeamAIInformation teamAIInfo;
	ScriptEngine engine;
	
	ArrayInstance myShipsJS;
	ArrayInstance allyShipsJS;
	ArrayInstance enemyShipsJS;
	ArrayInstance bulletsJS;
	bool ready = false;
	bool scriptExcuted = false;

	int randomI = 0;
	double[] randoms = {0.67875,0.32222,0.65738,0.38630,0.46512,0.96310,0.73474,0.68538,0.56789,0.92037,0.86292,0.30085,0.72147,0.19404,0.30749,0.23272,0.45703,0.51853,0.80288,0.21540,0.24340,0.42498,0.61466,0.51448,0.08059,0.38226,0.74283,0.12995,0.49975,0.39035,0.18740,0.79624,0.96973,0.98041,0.94837,0.04450,0.75611,0.62129,0.76015,0.34358,0.59329,0.57857,0.88833,0.95905,0.01246,0.85224,0.33290,0.62534,0.48244,0.17672,0.82828,0.87360,0.26881,0.81760,0.90564,0.17268,0.37158,0.77488,0.03382,0.78815,0.52516,0.04855,0.47839,0.45444,0.99514,0.63861,0.97378,0.70270,0.78151,0.27949,0.78556,0.00178,0.88428,0.23677,0.17931,0.61725,0.03787,0.51043,0.35831,0.28354,0.50784,0.83492,0.11927,0.53180,0.29422,0.47580,0.93105,0.83087,0.39699,0.43307,0.05923,0.44635,0.55057,0.70674,0.50380,0.11263,0.28613,0.29681,0.26476,0.36090};

	public void Ready()
	{
		fleet = GetComponent<Fleet> ();
		teamAIInfo = fleet.team.aiInfor;
		engine = new ScriptEngine();
		InitJSValues();
		
		
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
		engine.SetGlobalValue("myShips", myShipsJS);
		engine.SetGlobalValue("allyShips", allyShipsJS);
		engine.SetGlobalValue("enemyShips", enemyShipsJS);
		engine.SetGlobalValue("bullets", bulletsJS);
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		
		// Exposing .NET methods to JavaScript.
		// The generic System.Action delegate is used to define method signatures with no returns;
		//engine.SetGlobalFunction("SetShipSpeed", new System.Action<double>(SetShipSpeed));
		//engine.SetGlobalFunction("SetShipAngleSpeed", new System.Action<double>(SetShipAngleSpeed));
		//engine.SetGlobalFunction("Shoot", new System.Action(Shoot));
		engine.SetGlobalFunction("log", new System.Action<string>(Log));
		engine.SetGlobalFunction("polar", new Func<ObjectInstance,ObjectInstance>(Polar));
		engine.SetGlobalFunction("polarFrom", new Func<ObjectInstance,ObjectInstance,ObjectInstance>(PolarFrom));
		engine.SetGlobalFunction("cos", new Func<double,double>(Cos));
		engine.SetGlobalFunction("sin", new Func<double,double>(Sin));
		engine.SetGlobalFunction("d2r", new Func<double,double>(D2R));
		engine.SetGlobalFunction("r2d", new Func<double,double>(R2D));
		engine.SetGlobalFunction("dist", new Func<ObjectInstance,ObjectInstance,double>(Distance));
		engine.SetGlobalFunction("random", new Func<double>(Random));

		engine.SetGlobalValue("dt",engine.Number.Construct(0.02));
		engine.SetGlobalValue("groundRadius", engine.Number.Construct((double)GameValueSetter.groundSize));

		engine.SetGlobalValue("shipMaxAmmo", engine.Number.Construct((double)Ship.maxAmmo));
		engine.SetGlobalValue("shipMaxHp", engine.Number.Construct((double)Ship.maxHp));
		engine.SetGlobalValue("shipMaxSpeed", engine.Number.Construct((double)Ship.maxSpeed));
		engine.SetGlobalValue("shipMaxAngleSpeed", engine.Number.Construct((double)Ship.maxAngleSpeed));
		engine.SetGlobalValue("bulletSpeed", engine.Number.Construct((double)Bullet.speed));
		
		// Examples of exposing some .NET methods with return values to JavaScript.
		// The generic System.Func delegate is used to define method signatures with return types;
		//engine.SetGlobalFunction("GetPos", new System.Func<jsVectorInstance>(GetPos));
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
		ready = true;
	}
	#region JS Functions
	// This set of methods implment the functions we exposed to javaScript.
	public void Log(string str){
		Debug.Log("JS LOG : "+str);
        onLog.Invoke("JS LOG : " + str);
	}

	public double Random(){
		return randoms[randomI++%randoms.Length];
	}
	public double Cos(double v){
		return (double)Mathf.Cos (Mathf.Deg2Rad * (float)v);
	}
	public double Sin(double v){
		return (double)Mathf.Sin (Mathf.Deg2Rad * (float)v);
	}
	public double D2R(double v){
		return (double)(Mathf.Deg2Rad * (float)v);
	}
	public double R2D(double v){
		return (double)(Mathf.Rad2Deg * (float)v);
	}
	public double Distance(ObjectInstance a,ObjectInstance b){
		float x1 = (float)(double)a["x"];
		float y1 = (float)(double)a["y"];

		float x2 = (float)(double)b["x"];
		float y2 = (float)(double)b["y"];

		float x = x1 - x2;
		float y = y1 - y2;

		return (double)Mathf.Sqrt (x * x + y * y);
	}

	public ObjectInstance Polar(ObjectInstance target){
		ObjectInstance ret = PolarFrom(null,target);
		return ret;
	}
	public ObjectInstance PolarFrom(ObjectInstance center, ObjectInstance target){
//		Debug.Log(target["x"]);
		//Debug.Log((float)target["x"]);
		float x,y;
//		float x = (float)(double)target["x"];//(float)((PropertyDescriptor)target["x"]).Value;
//		float y = (float)(double)target["y"];//(float)((PropertyDescriptor)target["y"]).Value;
		if(target["x"] is System.Int32){
			
			x = (float)(int)target["x"];
			y = (float)(int)target["y"];
		}
		else {
			x = (float)(double)target["x"];
			y = (float)(double)target["y"];
		}
		ObjectInstance ret = engine.Object.Construct();
		if(center == null){
			var angle = Mathf.Atan2(y,x)*Mathf.Rad2Deg;
			Debug.Log(angle);
			angle %= 360;
			var r = Vector2.Distance(new Vector2(x,y),Vector2.zero);
			ret["r"] = (double)r;
			ret["angle"] = (double)angle;
		}
		else{
			if(center["x"] is System.Int32){
				
				x -= (float)(int)center["x"];
				y -= (float)(int)center["y"];
			}
			else {
				x -= (float)(double)center["x"];
				y -= (float)(double)center["y"];
			}
			var angle = Mathf.Atan2(y,x)*Mathf.Rad2Deg;
			if(center.HasProperty("angle")){
				if(center["angle"] is System.Int32){
					angle -= (float)(int)center["angle"];
				}
				else{
					angle -= (float)(double)center["angle"];
				}
				angle %= 360;
				
				if(angle > 180){
					angle -= 360;
				}
				if(angle < -180){
					angle += 360;
				}

			}
			
			var r = Vector2.Distance(new Vector2(x,y),Vector2.zero);
			ret["r"] = (double)r;
			ret["angle"] = (double)angle;
		}
		
		return ret;
	}
	
	#endregion

	public void ExcuteCommand(string command){
        try
        {
            engine.Execute(command);
        }
        catch (System.Exception e)
        {
            Log(e.ToString());
            throw;
        }
		
	}

	void InitJSValues(){
		myShipsJS = engine.Array.New();
		allyShipsJS = engine.Array.New();
		enemyShipsJS = engine.Array.New();
		bulletsJS = engine.Array.New();

		foreach (Ship ship in fleet.ships) {
			ship.GetComponent<ShipJSObject>().Create();
		}
	}
	
	// Update is called once per frame
	public void FixedUpdate2 () {
		if (!ready)
			return;
		ExportMyShips ();
		ExportCollectionToJS (teamAIInfo.allyShips, allyShipsJS);
		ExportCollectionToJS (teamAIInfo.scannedEnemyShips.Keys, enemyShipsJS);
		ExportCollectionToJS (teamAIInfo.scannedBullets.Keys, bulletsJS);
		ExcuteScript();
	}
	
	void ExcuteScript(){
		if(!scriptExcuted){

            Debug.Log("code=================");
            Debug.Log(code);
            engine.Execute(code);
			scriptExcuted = true;
			return;
		}
		engine.CallGlobalFunction("update");
	}

	void ExportMyShips(){
		if (myShipsJS.Length == fleet.ships.Count) {
			return;
		}
		int i = 0;
		myShipsJS.Length = (uint)fleet.ships.Count;
		foreach (Ship ship in fleet.ships) {
			myShipsJS[i++] = ship.GetComponent<ShipJSObject>().jsobj as ObjectInstance;
		}
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
}
