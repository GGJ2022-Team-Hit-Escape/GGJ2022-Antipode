using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    public string itemName;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private Collider2D myCollider;

    [SerializeField]
    private UnityEngine.Events.UnityEvent OnFirstPickup;


    [SerializeField]
    private UnityEngine.Events.UnityEvent OnPickupOccurred;


    [SerializeField]
    private UnityEngine.Events.UnityEvent OnDropOccurred;

    private bool firstPickupOcurred = false;

    private void Awake()
    {
    }

    private void Update()
    {
        
    }

    public void Kick(Vector2 force)
    {
        rb.AddForce(force);
    }

    public void TurnOnPhysics()
    {
        rb.isKinematic = false;
        myCollider.enabled = true;
        rb.angularVelocity = 0;
    }

    public void TurnOffPhysics()
    {
        rb.isKinematic = true;
        myCollider.enabled = false;
    }

    public void OnPickup()
    {
        if (!firstPickupOcurred)
            OnFirstPickup?.Invoke();
        firstPickupOcurred = true;
        OnPickupOccurred?.Invoke();
    }

    public void OnDrop()
    {
        OnDropOccurred?.Invoke();
    }
}
