using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Bow : MonoBehaviour
{
	public Transform attachedArrow;
	public SkinnedMeshRenderer BowSkinnedMesh;

	public float blendMultiplier = 255f;
	public GameObject realArrowPrefab;

	public float maxShootSpeed = 50;

	public AudioClip fireSound;

	bool IsArmed()
	{
		return attachedArrow.gameObject.activeSelf;
	}
	
	void Update () 
	{
		float distance = Vector3.Distance(transform.position, attachedArrow.position);
		BowSkinnedMesh.SetBlendShapeWeight(0, Mathf.Max(0, distance * blendMultiplier));
	}

	private void Arm()
	{
		attachedArrow.gameObject.SetActive(true);
	}
	private void Disarm()
	{
		BowSkinnedMesh.SetBlendShapeWeight(0, 0);
		attachedArrow.position = transform.position;
		attachedArrow.gameObject.SetActive(false);
	}

	private void OnTriggerEnter(Collider other)
	{
		if(!IsArmed() && other.CompareTag("InteractionObject") && other.GetComponent<RealArrow>()
			&& !other.GetComponent<RWVR_InteractionObject>().IsFree())
		{
			Destroy(other.gameObject);
			Arm();
		}
	}

	public void ShootArrow()
	{
		GameObject arrow = Instantiate(realArrowPrefab, transform.position, transform.rotation);
		float distance = Vector3.Distance(transform.position, attachedArrow.position);
		arrow.GetComponent<Rigidbody>().velocity = arrow.transform.forward * distance * maxShootSpeed;
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
		GetComponent<RWVR_InteractionObject>().currentController.Vibrate(3500);
		arrow.GetComponent<RealArrow>().Launch();
		Disarm();
	}
}
