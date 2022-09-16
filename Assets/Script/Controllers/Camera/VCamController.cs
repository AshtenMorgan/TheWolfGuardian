using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cinemachine.Utility;

public class VCamController : MonoBehaviour
{
    private GameManager gm;
    private CinemachineVirtualCamera vcam;
    public float pixelsPerUnit;
    public CinemachineConfiner confiner;

    // Start is called before the first frame update
    void Start()
    {
   
        gm = GameManager.Instance; //assign gm var
        vcam = GetComponent<CinemachineVirtualCamera>(); //find virtual cam component
        gm.cam = vcam;
        vcam.LookAt = gm.player.transform; // set lookat target
        vcam.Follow = gm.player.transform; // set follow target
    }
    

    void Update()
    {
        transform.position = PixelPerfectClamp(gm.player.transform.position, pixelsPerUnit);
    }

    
    private Vector3 PixelPerfectClamp(Vector3 moveVector, float pixelsPerUnit)
    {
        Vector3 vectorInPixels = new Vector3(Mathf.CeilToInt(moveVector.x * pixelsPerUnit), Mathf.CeilToInt(moveVector.y * pixelsPerUnit), Mathf.CeilToInt(moveVector.z * pixelsPerUnit));
        return vectorInPixels / pixelsPerUnit;
    }
    

}
