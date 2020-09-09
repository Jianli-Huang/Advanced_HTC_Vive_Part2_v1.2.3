using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RWVR_SpecialObjectSpawner : RWVR_InteractionObject 
{
	public GameObject arrowPrefab;
	public List<GameObject> randomPrefabs = new List<GameObject>();

	private void SpawnObjectInHand(GameObject prefab,RWVR_InteractionController controller)
	{
		GameObject spawnedObject = Instantiate(prefab, controller.snapColliderOrigin.position,
			controller.transform.rotation);
		controller.SwitchInteractionObjectTo(spawnedObject.GetComponent<RWVR_InteractionObject>());
		OnTriggerWasReleased(controller);
	}
	public override void OnTriggerIsBeingPressed(RWVR_InteractionController controller)
	{
		base.OnTriggerIsBeingPressed(controller);
		if (RWVR_ControllerManager.Instance.AnyControllerIsInteractingWith<Bow>())
		{
			SpawnObjectInHand(arrowPrefab, controller);
		}
		else
		{
			SpawnObjectInHand(randomPrefabs[UnityEngine.Random.Range(0, randomPrefabs.Count)], controller);
		}
	}

}
