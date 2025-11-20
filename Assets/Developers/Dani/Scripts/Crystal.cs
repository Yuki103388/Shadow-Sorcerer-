using UnityEngine;

public enum ElementType
{
    None,
    Electric
}

public class Crystal : Interactable
{
    [Header("Settings")]
    public ElementType elementType = ElementType.None;
    private bool isSlotted;
}