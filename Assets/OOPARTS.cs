using UnityEngine;
using System.Collections.Generic;

public class OOPARTS : MonoBehaviour
{
    public Transform[] objectsParents;
    public float resetTime = 3;

    struct Block{
		public GameObject obj;
        public Vector3 pos;
		public Quaternion rotation;
		public float randomRate;
		public float resetTime;
		public bool gravity;
		public BoxCollider col;
		public Rigidbody body;
    }

    Dictionary<GameObject, Block> pieces = new Dictionary<GameObject, Block>();
	List<Block> blocks = new List<Block>();
	List<Block> resetBlocks = new List<Block>();


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
		float t = resetTime;
        foreach (Transform objt in objects)
        {
            var obj = objt.gameObject;
			Block block;
			block.obj = obj;
			block.gravity = true;
			block.col = obj.GetComponent<BoxCollider>();
			block.body = obj.GetComponent<Rigidbody>();
            block.pos = obj.transform.position;
            block.rotation = obj.transform.rotation;
			block.randomRate = 0.8f;//GetRandom();
			block.resetTime = resetTime+t;
			t += 0.02f;
			blocks.Add(block);


            float force = 1000;
            obj.GetComponent<Rigidbody>().velocity = (new Vector3(GetRandom() * force - force * 0.75f, GetRandom() * force, GetRandom() * force - force * 0.5f));
            force = 20000;
            obj.GetComponent<Rigidbody>().angularVelocity = (new Vector3(GetRandom() * force - force * 0.75f, GetRandom() * force - force * 0.75f, GetRandom() * force - force * 0.75f));

            force = 200;
			obj.transform.position = obj.transform.position + (new Vector3(GetRandom() * force - force * 0.75f, GetRandom() *300f+200f, GetRandom() * force - force * 0.75f));
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
			obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
			obj.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            if (obj.transform.childCount == 1)
            {
                obj.transform.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

	void Update () {
        t += Time.deltaTime;

		for (int i = 0; i < blocks.Count; ++i)
        {
			var blk = blocks[i];
			if(t >= blk.resetTime){
				if(blk.gravity){
					blk.gravity = false;
					blk.col.enabled = false;
					blk.body.useGravity = false;
					blk.body.velocity = Vector3.zero;
					blk.body.angularVelocity = Vector3.zero;
				}
			}
			else{
				continue;
			}
			float rate = Mathf.Max(0,Mathf.Min(1,blk.randomRate*(t-blk.resetTime)/1.2f));
			blk.obj.transform.position = Vector3.Lerp(blk.obj.transform.position, blk.pos, rate);
			blk.obj.transform.rotation = Quaternion.Lerp(blk.obj.transform.rotation, blk.rotation, rate);
        }
	}
}
