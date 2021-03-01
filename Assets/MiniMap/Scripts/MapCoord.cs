using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapCoord : MonoBehaviour
{
    public Transform playerTrans;
    private Text coordText;
    
    // Start is called before the first frame update
    void Start()
    {
        coordText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCoordText();
    }
    
    private void UpdateCoordText()
	{
		coordText.text = string.Format("({0:0.00},{1:0.00})", playerTrans.position.x, playerTrans.position.z);
	}
}
