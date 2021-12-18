using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Skin : MonoBehaviour
{
    public Player Player;
    public Animator Animator;

    public bool Free;
    public Currency Currency;
    public int Cost;

    public bool Purchased;
}
