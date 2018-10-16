using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoFurniture : MonoBehaviour {
    [SerializeField]
    private int width;
    [SerializeField]
    private int height;
    [SerializeField]
    private bool walkable;
    [SerializeField]
    private float extraWeightTo;
    [SerializeField]
    private float extraWeightFrom;
    [SerializeField]
    private int shelterHeight;

    public int Height
    {
        get
        {
            return height;
        }
    }

    public int Width
    {
        get
        {
            return width;
        }
    }

    public bool Walkable
    {
        get
        {
            return walkable;
        }
    }

    public float ExtraWeightTo
    {
        get
        {
            return extraWeightTo;
        }
    }

    public float ExtraWeightFrom
    {
        get
        {
            return extraWeightFrom;
        }
    }

    public int ShelterHeight
    {
        get
        {
            return shelterHeight;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
