using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RWVR_ControllerManager : MonoBehaviour
{
    public static RWVR_ControllerManager Instance;
    public RWVR_InteractionController leftController;
    public RWVR_InteractionController rightController;

    private void Awake()
    {
        Instance = this;
    }
    public bool AnyControllerIsInteractingWith<T>()
    {
        if(leftController.InteractionObject && leftController.InteractionObject.GetComponent<T>() != null)
        {
            return true;
        }
        if(rightController.InteractionObject && rightController.InteractionObject.GetComponent<T>() != null)
        {
            return true;
        }
        return false;
    }

}
