using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cinemachine.Utility;

public class Parallax : MonoBehaviour
{
    private GameManager gm;
    protected float 
        lengthx,
        lengthy,
        startposx,
        startposy;
    [Header("How fast this section moves"), Tooltip("The closer the section, the lower the number should be")]
    public float parallaxFactorX,
        parallaxFactorY;
    [Header("Camera to add parallax to"), Tooltip("This should be the Cinemachine Virtual Camera, NOT Main Camera.")]
    public GameObject cam;
    [Header("PPU"), Tooltip("This should match the pixels per unit of the project")]
    public float pixelsPerUnit;
    [SerializeField]
    private float
        tempx,
        tempy,
        distancex,
        distancey;

    private void OnEnable()
    {
        gm = GameManager.Instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        startposx = transform.position.x;
        startposy = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        tempx = cam.transform.position.x * (1 - parallaxFactorX);
        tempy = cam.transform.position.y * (1 - parallaxFactorY);
        distancex = cam.transform.position.x * parallaxFactorX;
        distancey = cam.transform.position.y * parallaxFactorY;

    }
    private void FixedUpdate()
    {
        if (gm.currentRoom != null)
            if (gm.currentRoom.bounds.Contains(gm.cameraMain.transform.position))
            {
                UpdateX();
                UpdateY();
            }
    }
    void UpdateX()
    {
        Vector3 newPosition = new Vector3(startposx + distancex, transform.position.y, transform.position.z);
        transform.position = PixelPerfectClamp(newPosition, pixelsPerUnit);

        if (tempx > startposx + (lengthx / 2))
        {
            startposx += lengthx;
            Debug.Log("Added to x");
        }

        else if (tempx < startposx - (lengthx / 2))
        {
            startposx -= lengthx;
            Debug.Log("subtracted from x");
        }

    }
    void UpdateY()
    {
        Vector3 newPosition = new Vector3(transform.position.x, startposy + distancey, transform.position.z);
        transform.position = PixelPerfectClamp(newPosition, pixelsPerUnit);
       
            if (tempy > startposy + (lengthy / 1.5))
        {
            startposy += lengthy;
            Debug.Log("Added to y");
        }
                
            else if (tempy < startposy - (lengthy / 1.5))
        {
            startposy -= lengthy;
            Debug.Log("subtracted from y");
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
