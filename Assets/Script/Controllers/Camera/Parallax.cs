using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    protected float length,
        startpos;
    [Header("How fast this section moves"), SerializeField, Tooltip("The closer the section, the lower the number should be")]
    public float parallaxFactor;
    [Header("Camera to add parallax to"), SerializeField, Tooltip("This should be the Cinemachine Virtual Camera, NOT Main Camera.")]
    public GameObject cam;
    [Header("PPU"), SerializeField, Tooltip("This should match the pixels per unit of the project")]
    public float pixelsPerUnit;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = cam.transform.position.x * (1 - parallaxFactor);
        float distance = cam.transform.position.x * parallaxFactor;

        Vector3 newPosition = new Vector3(startpos + distance, transform.position.y, transform.position.z);
        transform.position = PixelPerfectClamp(newPosition, pixelsPerUnit);

        if (temp > startpos + (length / 2))
        {
            startpos += length;
        }
        else if (temp < startpos - (length / 2))
        {
            startpos -= length;
        }
    }
    private Vector3 PixelPerfectClamp(Vector3 locationVector, float pixelsPerUnit)
    {
        Vector3 vectorInPixels = new Vector3(Mathf.CeilToInt(locationVector.x  * pixelsPerUnit),
            Mathf.CeilToInt(locationVector.y * pixelsPerUnit),
            Mathf.CeilToInt(locationVector.z * pixelsPerUnit));

        return vectorInPixels / pixelsPerUnit;
    }
}
