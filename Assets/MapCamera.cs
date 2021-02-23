using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MapCamera : MonoBehaviour
{
    private float shadowDistance;
    
    public Shader unlitShader;
    public GameObject player;

    private Camera camera;
    private Renderer playerRender;

    void Start()
    {
        camera = GetComponent<Camera>();
        camera.SetReplacementShader(unlitShader, "");
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
