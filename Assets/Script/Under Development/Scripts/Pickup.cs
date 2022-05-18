using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [Header("Pickup Settings")]
    protected Vector3 axis = Vector3.up; //the axis the object will rotate on
    [SerializeField, Tooltip("This is the speed at which the pickup rotates")]
    protected float rotationSpeed = 90f; // variable for pick up rotation
    [SerializeField, Tooltip("This is how long the pickup remains in the scene before destroying itself")]
    protected float lifespan = 15f; //amount of time the pickup remains until it is destroyed
    [SerializeField, Tooltip("If this boolean is true the pickup will decay after it's designer set lifepsan. If it is false then the pickup will not decay.")]
    protected bool canDecay = true; //boolean for allowing a pickup to not decay
    public virtual void Awake()
    {
        if (canDecay == true)
        {
            StartCoroutine(Decay()); 
        }
    }

    // Start is called before the first frame update
    public virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //rotate the pickup as it sits on the ground
        //transform.rotation *= Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, axis);
    }

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        //check to see if what collided with pickup is a HumanoidPawn
        //GameObject entity = collider.gameObject;
        //if it is
        if (collider.CompareTag("Player"))
        {
            Debug.Log("Health touched");
            //call on pickup
            OnPickUp(collider.gameObject);
        }
    }

    protected virtual void OnPickUp(GameObject entity)
    {
        //GameManager.instance.pickups.Remove(gameObject);
        //GameManager.instance.currentPickups--;
        Destroy(gameObject);
    }
    IEnumerator Decay()
    {
        yield return new WaitForSeconds(lifespan);
        //GameManager.instance.pickups.Remove(gameObject);
        //GameManager.instance.currentPickups--;
        Destroy(gameObject);
    }

}
