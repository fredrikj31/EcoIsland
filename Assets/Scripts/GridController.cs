using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridController : MonoBehaviour
{
    private Grid grid;
	public Tilemap map;
	public TileBase tile;

    // Start is called before the first frame update
    void Start()
    {
        this.grid = this.GetComponent<Grid>();
    }

    // Update is called once per frame
    void Update()
    {
		
    }
}
