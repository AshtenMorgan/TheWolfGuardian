using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileParallax : MonoBehaviour
{
    [Header("How fast this section moves"), Tooltip("The closer the section, the lower the number should be")]
    public float parallaxFactor;
    [Header("Camera to add parallax to"), Tooltip("This should be the Cinemachine Virtual Camera, NOT Main Camera.")]
    public GameObject cam;
    [Header("PPU"), Tooltip("This should match the pixels per unit of the project")]
    public float pixelsPerUnit;

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.transform.position.x <= -39.9)
            return;

        float distance = cam.transform.position.x * parallaxFactor;

        Vector3 newPosition = new Vector3(distance, transform.position.y, transform.position.z);
        transform.position = PixelPerfectClamp(newPosition, pixelsPerUnit);  
    }
    private Vector3 PixelPerfectClamp(Vector3 locationVector, float pixelsPerUnit)
    {
        Vector3 vectorInPixels = new Vector3(Mathf.CeilToInt(locationVector.x * pixelsPerUnit),
            Mathf.CeilToInt(locationVector.y * pixelsPerUnit),
            Mathf.CeilToInt(locationVector.z * pixelsPerUnit));

        return vectorInPixels / pixelsPerUnit;
    }
}
