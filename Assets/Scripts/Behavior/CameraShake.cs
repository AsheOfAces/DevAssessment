using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	[SerializeField]private Transform camTransform;
	[SerializeField]private float shakeAmount = 0.7f;
	[HideInInspector]public bool shake = false;
	private Vector3 originalPos;

	void Awake()
	{
		if(camTransform == null)
        {
			camTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        }
		originalPos = camTransform.localPosition;
	}

	void Update()
	{
		if (shake)
		{
			camTransform.localPosition = Vector3.Lerp(camTransform.localPosition, originalPos + Random.insideUnitSphere * shakeAmount, Time.deltaTime);
		}
	}

	public void StopShake()
    {
		camTransform.localPosition = originalPos;
    }
}