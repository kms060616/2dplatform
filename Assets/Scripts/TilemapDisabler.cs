using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapDisabler : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        GetComponent<TilemapRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
