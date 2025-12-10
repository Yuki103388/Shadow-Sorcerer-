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
    [Header("References")]
    public ParticleSystem VFX;
    [ShowIf("isProjectile", false)] public GameObject projectilePrefab;
    [ShowIf("isProjectile", true)] public Material lineRendererMaterial;
    [Header("Settings")]
    public ElementType elementType = ElementType.None;
    public bool isProjectile;
    [ShowIf("isProjectile", false)] public float projectileSpeed = 10f;
    [ShowIf("isProjectile", false)] public float explosionRadius = 5f;
    [ShowIf("isProjectile", false)] public float explosionForce = 5f;
    private bool isSlotted;
    //vfx and sfx added here
}