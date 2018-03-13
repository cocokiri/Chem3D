using System.Collections.Generic;
using System;
using UnityEngine;
using AtomConfig;
using Swag;

[System.Serializable] //show Lists in inspector
public class Atom : MonoBehaviour {
    public Config state;
    public List<Transform> to = new List<Transform>();
    public Bond bond;
    public SwagUtils swagger;
    GameObject myParent;
   // public LineRenderer lRender;


    // Use this for initialization
    void Start() {
        //get data from chemical table
        Init(1);
        swagger = GameObject.Find("Swag").GetComponent<SwagUtils>();
    }
    void Init(int atomNumber)
    {
        state = AtomConfig.PTable.config[atomNumber];
    }

    public void Bond(Transform other)
    {
        if (!isSame(transform, other) && CanConnect(other.GetComponent<Atom>()))
        {
            to.Add(other);
            Debug.Log(transform.name + "  bonded with  " + other.name);
                
            swagger.CreateLine(transform, other);
            addToMolecule(other.GetComponent<Atom>());
        }
        else
        {
            //no bonding
            Debug.LogError("Tried to bond two Atoms wrongly");
        }
       
    }

    bool isSame(Transform t1, Transform t2)
    {
        return (t1.GetInstanceID() == t2.GetInstanceID());
    }

    bool isBonded()
    {
        return transform.parent != null;
    }

    void addToMolecule(Atom B)
    {
        if (!isBonded() && !B.isBonded())
        {
            myParent = new GameObject("Molec");
            myParent.AddComponent<Molecule>();
            transform.parent = myParent.transform;
            B.transform.parent = myParent.transform;
        }
        if (isBonded() && !B.isBonded())
        {
            B.transform.parent = transform.parent;
        }
        if (!isBonded() && B.isBonded())
        {
            transform.parent = B.transform.parent;
        }
        //if both are bonded ... make A ingests Molecule Badsaasdsasdsa
        //molecule loops through kids => if ATom => new Parent is the other molecule ... then Destroy mole
    }


    bool isFull()
    {
        return (state.valence == state.capacity) || state.valence == 0;
    }
    bool CanConnect (Atom B)
    {
        return !B.isFull() && !isFull(); 
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("Space pressed");
            Bond(GameObject.Find("Atom (1)").transform);
            
        }
		
	}
}
