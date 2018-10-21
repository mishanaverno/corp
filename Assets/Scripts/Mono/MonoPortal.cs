using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoPortal : MonoBehaviour {
    [SerializeField]
    private bool opened;
    [SerializeField]
    private bool openable;
    [SerializeField]
    private bool locked;
    [SerializeField]
    private float extraWeight;
    [SerializeField]
    private int shelterHeight;

    public bool Openable
    {
        get
        {
            return openable;
        }
    }
    public bool Opened
    {
        get
        {
            return opened;
        }

        set
        {
            opened = value;
        }
    }

    public int ShelterHeight
    {
        get
        {
            return shelterHeight;
        }

        set
        {
            shelterHeight = value;
        }
    }

    public float ExtraWeight
    {
        get
        {
            return extraWeight;
        }

        set
        {
            extraWeight = value;
        }
    }

    public bool Locked
    {
        get
        {
            return locked;
        }

        set
        {
            locked = value;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
