using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MapCamera : MonoBehaviour
{
    private float shadowDistance;
    
    public Shader unlitShader;
    public GameObject player;

    private Camera mapCamera;
    private Renderer playerRender;

    void Start()
    {
        mapCamera = GetComponent<Camera>();
        mapCamera.SetReplacementShader(unlitShader, "");
        playerRender = player.GetComponent<Renderer>();
    }

    void OnPreRender()
    {
        shadowDistance = QualitySettings.shadowDistance;
        QualitySettings.shadowDistance = 0;
        playerRender.enabled = false;
    }

    void OnPostRender()
    {
        QualitySettings.shadowDistance = shadowDistance;
        playerRender.enabled = true;
    }
}
