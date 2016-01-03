using UnityEngine;
using Jurassic;
using Jurassic.Library;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.IO;
using System;

public class FleetAILoader : MonoBehaviour {

	public string code;
	public ScriptEngine GetEngine(){return engine;}


	Fleet fleet;
	TeamAIInformation teamAIInfo;
	ScriptEngine engine;
	
	ArrayInstance myShipsJS;
	ArrayInstance allyShipsJS;
	ArrayInstance enemyShipsJS;
	ArrayInstance bulletsJS;
	bool ready = false;
	bool scriptExcuted = false;

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

		engine.SetGlobalValue("dt",engine.Number.Construct(0.02));
		engine.SetGlobalValue("groundRadius", engine.Number.Construct((double)GameValueSetter.groundSize));
		
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
	void FixedUpdate () {
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
