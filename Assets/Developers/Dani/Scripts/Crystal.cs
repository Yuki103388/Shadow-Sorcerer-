using UnityEngine;

public enum ElementType
{
    None,
    Electric,
    Fire,
    Ice
}

public class Crystal : Interactable
{
    [Header("Settings")]
    public ElementType elementType = ElementType.None;
    public bool isProjectile;
    public float projectileSpeed = 10f;
    public float explosionRadius = 5f;
    private bool isSlotted;
    //vfx and sfx added here
}