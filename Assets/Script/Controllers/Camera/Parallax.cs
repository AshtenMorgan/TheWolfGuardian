using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    
    protected float lengthx,
        lengthy,
        startposx,
        startposy;
    [Header("How fast this section moves"), SerializeField, Tooltip("The closer the section, the lower the number should be")]
    public float parallaxFactorX,
        parallaxFactorY;
    [Header("Camera to add parallax to"), SerializeField, Tooltip("This should be the Cinemachine Virtual Camera, NOT Main Camera.")]
    public GameObject cam;
    [Header("PPU"), SerializeField, Tooltip("This should match the pixels per unit of the project")]
    public float pixelsPerUnit;
    // Start is called before the first frame update
    void Start()
    {
        startposx = transform.position.x;
        startposy = transform.position.y;
        lengthx = GetComponent<SpriteRenderer>().bounds.size.x;
        lengthy = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        float tempx = cam.transform.position.x * (1 - parallaxFactorX);
        float distancex = cam.transform.position.x * parallaxFactorX;
        float tempy = cam.transform.position.y * (1 - parallaxFactorY);
        float distancey = cam.transform.position.y * parallaxFactorY;

        Vector3 newPosition = new Vector3(startposx + distancex, startposy + distancey, transform.position.z);
        transform.position = PixelPerfectClamp(newPosition, pixelsPerUnit);

        if (tempx > startposx + (lengthx / 2))
            startposx += lengthx;
        else if (tempx < startposx - (lengthx / 2))
            startposx -= lengthx;
        if (tempy > startposy + (lengthy / 2))
            startposy += lengthy;
        else if (tempy < startposy - (lengthy / 2))
            startposy -= lengthy;


    }
    private Vector3 PixelPerfectClamp(Vector3 locationVector, float pixelsPerUnit)
    {
        Vector3 vectorInPixels = new Vector3(Mathf.CeilToInt(locationVector.x  * pixelsPerUnit),
            Mathf.CeilToInt(locationVector.y * pixelsPerUnit),
            Mathf.CeilToInt(locationVector.z * pixelsPerUnit));

        return vectorInPixels / pixelsPerUnit;
    }
}
