using UnityEngine;

public class Kick : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Shoot()
    {
        anim.SetTrigger("Shoot");
    }
}