using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : Pickups
{
    #region Variables
    #region Events
    [Header("Events")]
    [SerializeField, Tooltip("Raised when object is healed")]
    private UnityEvent onHeal;
    [SerializeField, Tooltip("Raised when object is damaged")]
    private UnityEvent onDamage;
    [SerializeField, Tooltip("Raised when object dies")]
    private UnityEvent onDie;
    //[SerializeField, Tooltip("Seconds before death")]
    //private float remTimer = 5.0f;
    [SerializeField, Tooltip("Raised when object Respawns")]
    private UnityEvent onRespawn;
    #endregion
    #region Pawn Variables
    private float _health;
    [SerializeField, Tooltip("Max Health")]
    private float _maxHealth = 100;//settable in inspector, should determine current health at start
    public float percent;//used to get percent of total health for health bar/tracker
    #endregion
    #region Combat Vars
    private float overKill;//to hold overkill value (if used)
    private float overHeal;//to hold overheal value (if used)
    public bool isDead = false;//death monitoring
    private Pawn pawn;//reference parent class
    #endregion
    #region Audio
    [Header("Sounds")]
    public AudioSource audiosource;//audio source (usually going to be on this player/enemy)
    public AudioClip deathSound;//clip to play on death
    #endregion
    #endregion
    [SerializeField]
    public float currentHealth //the accessor for _currentHealth
    {
        get => _health;
        set { _health = value; }
    }
    [SerializeField]
    public float maxHealth //the accessor for _currentHealth
    {
        get => _maxHealth;
        set { _maxHealth = value; }
    }
    #region Functions
    //called when script instance is being loaded
    void Awake()
    {
        pawn = GetComponent<Pawn>();    //get pawn from object this script is attached to
        _health = _maxHealth;
  
    }
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    //how to handle damage
    public void Damage(float damage)
    {
        Debug.Log("Hit");
        damage = Mathf.Max(damage, 0);//make sure damage is a positive number

        if (damage > currentHealth)//if damage is greater than current health
        {
            overKill = damage - currentHealth;//get the amount of overkill damage
            currentHealth = Mathf.Clamp(currentHealth - damage, 0f, currentHealth);//subtract damage from health, making sure not to subtract more than current health value
        }
        else//damage not more than current health
        {
            overKill = 0;//output 0
            currentHealth = Mathf.Clamp(currentHealth - damage, 0f, currentHealth);//subtract damage from health, making sure not to subtract more than current health value
        }

        SendMessage("OnDamage", SendMessageOptions.DontRequireReceiver);
        onDamage.Invoke();

        if (_health == 0)//if health reaches 0
        {
            SendMessage("onDie", SendMessageOptions.DontRequireReceiver);//tell every object this is attched to to look for its onDie method -dont error if none
            onDie.Invoke();//call onDie
        }
    }

    //how to handle healing
    public void Heal(float heal)
    {
        heal = Mathf.Max(heal, 0);//make sure the number is positive

        if (heal > (maxHealth - currentHealth))//if the ammount healed would put the target over max health
        {
            overHeal = heal - (maxHealth - currentHealth);//get amount of overhealing
        }
        else//if healing does not result in over heal
        {
            overHeal = 0;//no overheal
        }
        currentHealth = Mathf.Clamp(currentHealth + heal, 0, maxHealth);//heal for an ammount not to exceed max health
        SendMessage("OnHeal", SendMessageOptions.DontRequireReceiver);//tell every object this is attched to to look for its onDie method no error if not found
        onHeal.Invoke();
    }

    public void Death()
    {
        isDead = true;//let other things know this is dead.

        audiosource.PlayOneShot(deathSound);  //play death sound

        gameObject.SetActive(false);//set object inactive
    }

    public float GetPercent()
    {
        float percent = maxHealth / currentHealth;
        return percent;
    }
    public void Respawn()
    {
        currentHealth = maxHealth;
    }
    #endregion
}
