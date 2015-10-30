using UnityEngine;
using System.Collections;
using System.IO;

public class TEST_SpaceshipSpawner : MonoBehaviour {

    public string[] javaScripts;

    // Use this for initialization
    void Start () {
        LoadJavaScripts();

        for(int i = 0; i < javaScripts.Length; i++)
        {
            GameObject Ships = (GameObject)Instantiate(Resources.Load("SpaceShip"), new Vector3(3*i, 0, 3), Quaternion.identity);
            // TODO: Ships에 JavaScript 파일 이름 넘기기
            Ships.name = javaScripts[i];
            // Ships.GetComponent<TextMesh>().text = 
        }
        
        // Instantiate(, new Vector3(x, y, 0), Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadJS()
    {
        // Script myScript = Resources.Load("loadTestScript.cs") as Script;
        // Script myScript =  = gameObject.AddCompone nt("loadTestScript.cs") as Script;
    }

    private void LoadJavaScripts()
    {
        javaScripts = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\Script\", "*.js");
        for (int i = 0; i < javaScripts.Length; i++)
            javaScripts[i] = Path.GetFileName(javaScripts[i]);
    }
}
