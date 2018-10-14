using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoFurniture : MonoBehaviour {
    [SerializeField]
    private int width;
    [SerializeField]
    private int height;

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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
