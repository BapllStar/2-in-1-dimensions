using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    private Rigidbody rb;
    private Transform objectGrabPointTransform;
    [SerializeField] private float grabStrength = 20f;
    [SerializeField] private float throwModifier = .25f;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        //lineRenderer.material = GetComponent<Renderer>().material; // Set the LineRenderer's material to the material of the object
        
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.06f;
        lineRenderer.numCapVertices = 5;
    }

    public void Grab(Transform objectGrabPointTransform){
        this.objectGrabPointTransform = objectGrabPointTransform;
        rb.drag = 5f;
    }

    public void Drop(){
        rb.velocity = rb.velocity * throwModifier;
        this.objectGrabPointTransform = null;
        rb.drag = 0;
    }

    private void Update(){
        if (objectGrabPointTransform != null){
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, objectGrabPointTransform.position);
        }
        else{
            lineRenderer.positionCount = 0;
        }
    }

    private void FixedUpdate()
    {
        if (objectGrabPointTransform != null){
            Vector3 vel = objectGrabPointTransform.position - transform.position;
            rb.velocity += vel * grabStrength * Time.fixedDeltaTime * Mathf.Pow(vel.magnitude, 2) + vel * grabStrength * Time.fixedDeltaTime + Physics.gravity * Time.fixedDeltaTime;
        }
    }
}
