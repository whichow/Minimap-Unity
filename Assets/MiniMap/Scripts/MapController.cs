using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public KeyCode mapSwitch = KeyCode.M;
    public KeyCode mapZoomIn = KeyCode.RightBracket;
    public KeyCode mapZoomOut = KeyCode.LeftBracket;
    public KeyCode mapLock = KeyCode.L;

    private MiniMap map;

    // Start is called before the first frame update
    void Start()
    {
        map = FindObjectOfType<MiniMap>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(mapSwitch))
        {
            map.gameObject.SetActive(!map.gameObject.activeSelf);
        }
        if(Input.GetKeyUp(mapZoomIn))
        {
            map.ZoomIn();
        }
        if(Input.GetKeyUp(mapZoomOut))
        {
            map.ZoomOut();
        }
        if(Input.GetKeyUp(mapLock))
        {
            map.lockOrientation = !map.lockOrientation;
        }
    }
}
