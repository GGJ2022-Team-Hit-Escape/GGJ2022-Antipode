using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemPickupPoint : MonoBehaviour
{
    private List<Item> itemsInRange = new List<Item>();

    [SerializeField]
    private InputActionReference interactControl;

    [SerializeField]
    private InputActionReference verticalControl;

    private Item currentlyHighlightedItem;

    private Item currentlyHeldItem;

    [SerializeField]
    private GameObject interactionPrompt;

    private bool pickupLocked { get { return DialogSystem.isCurrentlyTalking || InteractionTrigger.interactionTriggerFocused; } }

    private void Start()
    {
        interactControl.action.Enable();
        verticalControl.action.Enable();
    }

    private void Update()
    {
        ClearNullItems();
        SelectHighlightedItem();
        DrawInteractionPrompt();
        DoInput();
        DoHoldObject();
    }
    private void DoHoldObject()
    {
        if(currentlyHeldItem != null)
        {
            currentlyHeldItem.transform.position = Vector3.MoveTowards(currentlyHeldItem.transform.position, this.transform.position, Time.deltaTime * 50f);
        }
    }

    private void DoInput()
    {

        if (pickupLocked)
            return;

        if (currentlyHeldItem == null && interactControl.action.triggered && currentlyHighlightedItem != null)
        {
            currentlyHeldItem = currentlyHighlightedItem;
            currentlyHeldItem.TurnOffPhysics();
        }
        else if (currentlyHeldItem != null)
        {
            if (interactControl.action.triggered)
            {
                currentlyHeldItem.TurnOnPhysics();

                float verticalAxis = verticalControl.action.ReadValue<float>();
                Vector2 throwForce = new Vector2(50, 60 * verticalAxis);
                if (verticalAxis < 0)
                    throwForce = Vector2.right * 10;

                throwForce = this.transform.TransformVector(throwForce);

                currentlyHeldItem.Kick(throwForce);


                currentlyHeldItem = null;
            }
        }
    }

    private void DrawInteractionPrompt()
    {
        bool currentlyHoldingObject = currentlyHeldItem != null;


        //Setting interaction prompt to follow selectable object
        interactionPrompt.SetActive(currentlyHighlightedItem != null && !currentlyHoldingObject && !pickupLocked);
        if (currentlyHighlightedItem != null)
            interactionPrompt.transform.position = currentlyHighlightedItem.transform.position;
    }

    private void SelectHighlightedItem()
    {
        Item closestItem = null;
        float closestItemDistance = float.MaxValue;

        foreach(Item item in itemsInRange)
        {
            float distance = Vector2.Distance(this.transform.position, item.transform.position);
            if (distance < closestItemDistance)
            {
                closestItem = item;
                closestItemDistance = distance;
            }
        }

        currentlyHighlightedItem = closestItem;
    }

    private void ClearNullItems()
    {
        for (int index = 0; index < itemsInRange.Count; index++)
        {
            if (itemsInRange[index] == null)
            {
                itemsInRange.RemoveAt(index);
                index--;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.gameObject.GetComponent<Item>();
        if (item != null)
        {
            itemsInRange.Add(item);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Item item = other.gameObject.GetComponent<Item>();
        if (item != null)
        {
            itemsInRange.RemoveAll(x=>x == item);
        }
    }
}
