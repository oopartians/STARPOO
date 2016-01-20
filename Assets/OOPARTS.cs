using UnityEngine;
using System.Collections.Generic;

public class OOPARTS : MonoBehaviour
{
    public Transform[] objectsParents;
    public float resetTime = 3;

    struct Block{
        public Vector3 pos;
        public Quaternion rotation;
    }

    Dictionary<GameObject, Block> pieces = new Dictionary<GameObject, Block>();


    float t = 0;
    bool reset = false;

	void Start () {
        foreach (Transform objsPT in objectsParents)
        {
            AddObjects(objsPT);
        }
	}

    void AddObjects(Transform objects)
    {
        foreach (Transform objt in objects)
        {
            var obj = objt.gameObject;
            Block block;
            block.pos = obj.transform.position;
            block.rotation = obj.transform.rotation;
            pieces.Add(obj, block);


            float force = 1000;
            obj.GetComponent<Rigidbody>().velocity = (new Vector3(GetRandom() * force - force * 0.75f, GetRandom() * force * 0.5f, GetRandom() * force - force * 0.5f));
            force = 20000;
            obj.GetComponent<Rigidbody>().angularVelocity = (new Vector3(GetRandom() * force - force * 0.75f, GetRandom() * force - force * 0.75f, GetRandom() * force - force * 0.75f));

            force = 200;
            obj.transform.position = obj.transform.position + (new Vector3(GetRandom() * force - force * 0.75f, 400f, GetRandom() * force - force * 0.75f));
        }
    }

    float GetRandom()
    {
        return Random.value * 0.5f + 0.5f;
    }
	
	// Update is called once per frame
    bool gravity = true;
    void OffGravity()
    {
        foreach (GameObject obj in pieces.Keys)
        {
            Block block = pieces[obj];
            obj.GetComponent<Rigidbody>().useGravity = false;
            obj.GetComponent<BoxCollider>().enabled = false;
            if (obj.transform.childCount == 1)
            {
                obj.transform.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

	void Update () {
        t += Time.deltaTime;

        if (t >= resetTime)
        {
            if (gravity)
            {
                OffGravity();
                t = resetTime;
                gravity = false;
                return;
            }
            foreach (GameObject obj in pieces.Keys)
            {
                Block block = pieces[obj];
                float rate = Mathf.Min(1,(t-resetTime)/1.2f);
                obj.transform.position = Vector3.Lerp(obj.transform.position, block.pos, rate);
                obj.transform.rotation = Quaternion.Lerp(obj.transform.rotation, block.rotation, rate);
            }
        }
	}
}
