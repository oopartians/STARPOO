using UnityEngine;
using Jurassic;
using Jurassic.Library;
using System.Collections;
using System.IO;

public class JavaScriptLoader : MonoBehaviour {

    SpaceShipHandler spaceShipHandler;
    ScriptEngine engine;
    public string codeString = "";
    public string floatingText = "";

    void Awake()
    {
        Debug.Log("JavaScriptLoader Loaded! in Awake function");
        spaceShipHandler = GetComponent<SpaceShipHandler>();
        //Debug.Log("Try Shoot..");
        //spaceShipHandler.Shoot();

        // Create an instance of the Jurassic engine then expose some stuff to it.
        engine = new ScriptEngine();
        Debug.Log("Create Success ScriptEngine Instance!");

        // Arguments and returns of functions exposed to JavaScript must be of supported types.
        // Supported types are bool, int, double, string, Jurassic.Null, Jurassic.Undefined
        // and Jurassic.Library.ObjectInstance (or a derived type).
        // More info: http://jurassic.codeplex.com/wikipage?title=Supported%20types

        // Examples of exposing some static classes to JavaScript using Jurassic's "seamless .NET interop" feature.
        engine.EnableExposedClrTypes = true; // You must enable this in order to use interop feaure.
        
        // engine.SetGlobalValue("Mathf", typeof(Mathf));
        // engine.SetGlobalValue("Input", typeof(Input));
        // engine.SetGlobalValue("Transform", typeof(Transform));

        // Exposing .NET methods to JavaScript.
        // The generic System.Action delegate is used to define method signatures with no returns;
        //engine.SetGlobalFunction("SetShipSpeed", new System.Action<double>(SetShipSpeed));
        // engine.SetGlobalFunction("SetShipAngleSpeed", new System.Action<double>(SetShipAngleSpeed));
        engine.SetGlobalFunction("Shoot", new System.Action(Shoot));

        // Examples of exposing some .NET methods with return values to JavaScript.
        // The generic System.Func delegate is used to define method signatures with return types;
        //engine.SetGlobalFunction("GetPos", new System.Func<jsVectorInstance>(jsGetPos));
        //engine.SetGlobalFunction("GetX", new System.Func<double>(jsGetX));
        //engine.SetGlobalFunction("GetY", new System.Func<double>(jsGetY));
        //engine.SetGlobalFunction("GetZ", new System.Func<double>(jsGetZ));
        //engine.SetGlobalFunction("GetText", new System.Func<string>(jsGetText));

        // Example of creating a static .NET class to expose to JavaScript
        //engine.SetGlobalValue("Days", new jsDayIterator(engine));

        // Example of creating an instance class with a constructor in JavaScript
        //engine.SetGlobalValue("Vector", new jsVectorConstructor(engine));
        Debug.Log("end Awake");
    }

    #region JS Functions
    // This set of methods implment the functions we exposed to javaScript.

    public void SetShipSpeed(double x)
    {
        spaceShipHandler.SetSpeed((float)x);
    }

    public void SetShipAngleSpeed(double aAngleSpeed)
    {
        spaceShipHandler.SetAngleSpeed((float)aAngleSpeed);
    }

    public void Shoot()
    {
        Debug.Log("[JavaScriptLoader] Shoot Function Called!");
        spaceShipHandler.Shoot();
    }

    #endregion

    // Use this for initialization
    void Start () {
        //transform.localRotation;
        Debug.Log("[JavaScriptLoader] Start Function start!");
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log("[JavaScriptLoader] Update Function start!");

        string stringCode = File.ReadAllText(Directory.GetCurrentDirectory() + @"\Script\" + gameObject.name);
        //Execute the contents of the script every frame if Running is ticked.
        engine.Execute(stringCode);
        Debug.Log(Directory.GetCurrentDirectory() + @"\Script\" + gameObject.name);
    }

    void LateUpdate()
    {

    }
}
