using UnityEngine;
using System.Collections;
using System.IO;

public class SpaceshipSpawner : MonoBehaviour {

    public string[] javaScripts;

    // Use this for initialization
    void Start () {
        LoadJavaScripts();

        for(int i = 0; i < javaScripts.Length; i++)
        {
            GameObject Ships = (GameObject)Instantiate(Resources.Load("SpaceShip"), new Vector3(3*i, 3*i, 0), Quaternion.identity);
            // Ships에 JavaScript 파일 이름 넘기기
            Ships.name = javaScripts[i];
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void LoadJavaScripts()
    {
        javaScripts = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\Script\", "*.js");
        for (int i = 0; i < javaScripts.Length; i++)
            javaScripts[i] = Path.GetFileName(javaScripts[i]);
    }
}
