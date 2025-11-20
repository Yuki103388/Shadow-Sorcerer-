using UnityEngine;

public enum ElementType
{
    None,
    Electric
}

public class Crystal : Interactable
{
    public ElementType elementType = ElementType.None;
}