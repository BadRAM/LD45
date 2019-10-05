using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseCharacter : MonoBehaviour
{
    [SerializeField] private Vector3 defaultOffset = new Vector3(0, 10, -10);
    private Camera camera;
    private Vector3 offsetForOffset;
    [SerializeField] private float maxDistance = 3.0f; //This is to make the camera not go all the way to the mouse cursor position, tweak it until it feels right.
    [SerializeField] private float scaleFactor = 0.5f; //This limits how far the camera can go from the player, tweak it until it feels right.
    [SerializeField] private Transform lookAt;
    [SerializeField] private Collider castPlane;

    [SerializeField] private bool smooth = true;
    private float smoothSpeed = 0.125f;
    private Vector3 offset = new Vector3(0, 10, -10);

    void Start()
    {
        transform.parent = null;
        camera = GetComponent<Camera>();
    }



    void LateUpdate()
    {
        
        /*
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (castPlane.Raycast(ray, out hit, 10000))
        {
            offsetForOffset = (hit.point - lookAt.position) * scaleFactor;
        }
        else offsetForOffset = Vector3.zero; // This makes it so that if the camera raycast doesn't hit, we go to directly over the player.
        */
        
        offsetForOffset = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        offsetForOffset = new Vector3(offsetForOffset.x - 0.5f, 0, offsetForOffset.y - 0.5f) * scaleFactor;
        
        if (offsetForOffset.magnitude > maxDistance)
        {
            offsetForOffset.Normalize(); // Make the vector3 have a magnitude of 1
            offsetForOffset = offsetForOffset * maxDistance;
        }
        offset = defaultOffset + offsetForOffset;

        Vector3 desiredPosition = lookAt.transform.position + offset;

        if (smooth)
        {
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }
    }

    public Vector3 GetMouseCastPos()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        castPlane.Raycast(ray, out hit, 10000);

        return hit.point;
    }
}