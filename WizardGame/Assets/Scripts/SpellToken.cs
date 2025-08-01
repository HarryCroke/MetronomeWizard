using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpellToken : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public string Text;
    public Image Image;
    public SpellType Type;
    private FirstPersonController player;
    private Vector3 InitialPosition;
    
    private SpellSlot currentSlot;
    
    protected bool beingDragged = false;

    private void Start()
    {
        player = GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>();
        InitialPosition = transform.position;
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        beingDragged = true;
        if (currentSlot != null)
        {
            OnEndDrag(null);
            transform.position = InitialPosition;
            currentSlot.Type = SpellType.None;
            currentSlot = null;
        }
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Slot" && beingDragged)
        {
            SpellSlot slot = other.GetComponent<SpellSlot>();
            if(slot.Type == SpellType.None) currentSlot = other.GetComponent<SpellSlot>();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Slot" && beingDragged && player.MenuOpen)
        {
            if (currentSlot == other.GetComponent<SpellSlot>())
            {
                currentSlot.Type = SpellType.None;
                currentSlot = null;
            }
        }
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        if (currentSlot != null)
        {
            currentSlot.ChangeValue(Type);
            transform.position = currentSlot.transform.position;
            print("SNAP!");
        }
    }

}
