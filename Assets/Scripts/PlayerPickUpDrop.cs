using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpDrop : MonoBehaviour
{
    [SerializeField] private Transform playerHeadTransform;
    [SerializeField] private float pickupDistance = 4f;
    [SerializeField] private LayerMask bluePickupLayer;
    [SerializeField] private LayerMask redPickupLayer;
    [SerializeField] private Transform blueObjectGrabPointTransform;
    [SerializeField] private Transform redObjectGrabPointTransform;

    private ObjectGrabbable blueObjectGrabbable;
    private ObjectGrabbable redObjectGrabbable;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            TryGrab("Blue");
        }
        if (Input.GetMouseButtonUp(0)){
            if (blueObjectGrabbable != null){
                Drop("Blue");
            }
        }

        if (Input.GetMouseButtonDown(1)){
            TryGrab("Red");
        }
        if (Input.GetMouseButtonUp(1)){
            if (redObjectGrabbable != null){
                Drop("Red");
            }
        }
    }

    private void TryGrab(string dimension){
        switch (dimension){
            case "Blue":
                if (Physics.Raycast(playerHeadTransform.position , blueObjectGrabPointTransform.position - playerHeadTransform.position, out RaycastHit bHit, pickupDistance , bluePickupLayer)){
                    if (bHit.collider.gameObject.TryGetComponent(out blueObjectGrabbable)){
                        blueObjectGrabbable.Grab(blueObjectGrabPointTransform);
                        Debug.Log("Picked up " + bHit.collider.gameObject.name + " in " + dimension + " dimension");
                    }
                }
                break;
            case "Red":
                if (Physics.Raycast(playerHeadTransform.position , redObjectGrabPointTransform.position - playerHeadTransform.position, out RaycastHit rHit, pickupDistance , redPickupLayer)){
                    if (rHit.collider.gameObject.TryGetComponent(out redObjectGrabbable)){
                        redObjectGrabbable.Grab(redObjectGrabPointTransform);
                        Debug.Log("Picked up " + rHit.collider.gameObject.name + " in " + dimension + " dimension");
                    }
                }
                break;
            default:
                Debug.LogError("Invalid dimension");
                break;
        }

        
    }

    private void Drop(string dimension){
        switch (dimension){
            case "Blue":
                blueObjectGrabbable.Drop();
                blueObjectGrabbable = null;
                break;
            case "Red":
                redObjectGrabbable.Drop();
                redObjectGrabbable = null;
                break;
            default:
                Debug.LogError("Invalid dimension");
                break;
        }
    }
}
