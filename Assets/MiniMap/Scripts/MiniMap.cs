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
	public GameObject player;
	public GameObject playerIcon;
	public GameObject northIcon;
	public List<GameObject> targets;
	public List<GameObject> targetInvisiableIcons;
	public List<GameObject> targetVisiableIcons;
	public bool lockOrientation;

	private Vector3 playerPosition;
	private Quaternion playerRotation;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		UpdatePlayer();
		UpdateCamera();
		UpdatePlayerIcon();
		UpdateTargetIcons();
	}

	private void UpdatePlayer() 
	{
		playerPosition = player.transform.position;
		playerRotation = player.transform.rotation;
	}

	private void UpdateCamera()
	{
		var cameraPos = mapCamera.transform.position;
		cameraPos = new Vector3(playerPosition.x, cameraPos.y, playerPosition.z);
		mapCamera.transform.position = cameraPos;

		var cameraEulerAngles = mapCamera.transform.eulerAngles;
		if(lockOrientation)
		{
			cameraEulerAngles.y = playerRotation.eulerAngles.y;
			mapCamera.transform.eulerAngles = cameraEulerAngles;
		}
		else
		{
			cameraEulerAngles.y = 0;
			mapCamera.transform.eulerAngles = cameraEulerAngles;
		}
	}

	private void UpdatePlayerIcon()
	{
		if(lockOrientation)
		{
			northIcon.transform.localEulerAngles = new Vector3(0, 0, playerRotation.eulerAngles.y);
			playerIcon.transform.localEulerAngles = new Vector3(0, 0, 0);
		}
		else
		{
			playerIcon.transform.localEulerAngles = new Vector3(0, 0, -playerRotation.eulerAngles.y);
			northIcon.transform.localEulerAngles = new Vector3(0, 0, 0);
		}
	}

	private void UpdateTargetIcons()
	{
		for(int i = 0; i < targets.Count; i++)
		{
			var point = mapCamera.WorldToViewportPoint(targets[i].transform.position);
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

	public void ZoomIn()
    {
		if(mapCamera.orthographic)
		{
			float value = mapCamera.orthographicSize - 1;
			if(mapCamera.orthographicSize > 2)
			{
				DOTween.To(()=>mapCamera.orthographicSize, x=>mapCamera.orthographicSize = x, value, 0.5f);
			}
		}
		else
		{
			float value = mapCamera.fieldOfView - 5;
			if(mapCamera.fieldOfView > 10)
			{
				DOTween.To(()=>mapCamera.fieldOfView, x=>mapCamera.fieldOfView = x, value, 0.5f);
			}
		}
		
		// float value = 0.01f;
		// if(mapCamera.fieldOfView > 10) {
		// 	value = mapCamera.fieldOfView - 10;
		// }
		// DOTween.To(()=>mapCamera.fieldOfView, x=>mapCamera.fieldOfView = x, value, 0.5f);

		// float value = mapCamera.transform.position.y - 5;
		// if(mapCamera.transform.position.y > 5)
		// {
		// 	mapCamera.transform.DOMoveY(value, 0.5f);
		// }
    }
	
    public void ZoomOut()
    {
		if(mapCamera.orthographic)
		{
			float value = mapCamera.orthographicSize + 1;
			if(mapCamera.orthographicSize < 20)
			{
				DOTween.To(()=>mapCamera.orthographicSize, x=>mapCamera.orthographicSize = x, value, 0.5f);
			}
		}
		else
		{
			float value = mapCamera.fieldOfView + 5;
			if(mapCamera.fieldOfView < 100)
			{
				DOTween.To(()=>mapCamera.fieldOfView, x=>mapCamera.fieldOfView = x, value, 0.5f);
			}
		}

		// float value = mapCamera.fieldOfView + 10;
		// DOTween.To(()=>mapCamera.fieldOfView, x=>mapCamera.fieldOfView = x, value, 0.5f);
		
		// float value = mapCamera.transform.position.y + 5;
		// if(mapCamera.transform.position.y < 50)
		// {
		// 	mapCamera.transform.DOMoveY(value, 0.5f);
		// }
    }
}
