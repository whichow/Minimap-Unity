using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[ExecuteInEditMode]
public class MiniMap : MonoBehaviour {

	public Camera mapCamera;
	public Button zoomInButton;
	public Button zoomOutButton;
	public Text coordText;
	public GameObject player;
	public GameObject playerIcon;
	public List<GameObject> targets;
	public List<GameObject> targetInvisiableIcons;
	public List<GameObject> targetVisiableIcons;

	private Vector3 playerPosition;
	private Quaternion playerRotation;

	void Awake() {
		zoomInButton.onClick.AddListener(OnZoomIn);
		zoomOutButton.onClick.AddListener(OnZoomOut);
	}

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		UpdatePlayer();
		UpdateCoordText();
		UpdateCameraPosition();
		UpdatePlayerIcon();
		UpdateTargetIcons();
	}

	private void UpdatePlayer() 
	{
		playerPosition = player.transform.position;
		playerRotation = player.transform.rotation;
	}

	private void UpdateCoordText()
	{
		coordText.text = string.Format("({0:0.00},{1:0.00})", playerPosition.x, playerPosition.z);
	}

	private void UpdateCameraPosition()
	{
		var cameraPos = mapCamera.transform.position;
		cameraPos = new Vector3(playerPosition.x, cameraPos.y, playerPosition.z);
		mapCamera.transform.position = cameraPos;
	}

	private void UpdatePlayerIcon()
	{
		playerIcon.transform.localEulerAngles = new Vector3(0, 0, -playerRotation.eulerAngles.y);
	}

	private void UpdateTargetIcons()
	{
		for(int i = 0; i < targets.Count; i++)
		{
			var point = mapCamera.WorldToViewportPoint(targets[i].transform.position);
			Debug.Log(point);
			if(point.x < 0.05f || point.x > 0.95f || point.y < 0.05f || point.y > 0.95f)
			{
				targetInvisiableIcons[i].SetActive(true);
				targetVisiableIcons[i].SetActive(false);
				targetInvisiableIcons[i].transform.localPosition = new Vector3(Mathf.Clamp(point.x, 0.05f, 0.95f) - 0.5f, Mathf.Clamp(point.y, 0.05f, 0.95f) - 0.5f) * 2 * 100f;
				targetInvisiableIcons[i].transform.localRotation = Quaternion.FromToRotation(Vector3.up, new Vector3(point.x - 0.5f, point.y - 0.5f) * 2);
			}
			else
			{
				targetVisiableIcons[i].SetActive(true);
				targetInvisiableIcons[i].SetActive(false);
				targetVisiableIcons[i].transform.localPosition = new Vector3(point.x - 0.5f, point.y - 0.5f) * 2 * 100f;
			}
		}
	}

	private void OnZoomIn()
    {
		float value = 0.01f;
		if(mapCamera.fieldOfView > 10) {
			value = mapCamera.fieldOfView - 10;
		}
		DOTween.To(()=>mapCamera.fieldOfView, x=>mapCamera.fieldOfView = x, value, 0.5f);
    }
	
    private void OnZoomOut()
    {
		float value = mapCamera.fieldOfView + 10;
		DOTween.To(()=>mapCamera.fieldOfView, x=>mapCamera.fieldOfView = x, value, 0.5f);
    }
}
