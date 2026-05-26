using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public FixedJoystick joystick;
    public float speed = 25f;

    private Animator anim;

    private bool isShooting = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Shoot/Pass/Kick এর সময় movement বন্ধ
        if (isShooting)
        {
            anim.SetBool("isRunning", false);
            return;
        }

        float h = joystick.Horizontal;
        float v = joystick.Vertical;

        // Keyboard Input
        h += Input.GetAxisRaw("Horizontal");
        v += Input.GetAxisRaw("Vertical");

        // Movement
        Vector3 move = new Vector3(h, 0f, v);

        // FAST FOOTBALL MOVEMENT
        transform.position += move * speed * Time.deltaTime;

        // Rotate Player
        if (move.magnitude > 0.05f)
        {
            transform.forward = move;
        }

        // Run Animation
        bool isMoving = move.magnitude > 0.05f;

        anim.SetBool("isRunning", isMoving);
    }

    // SHOOT BUTTON
    public void StartShoot()
    {
        PlayKickAnimation();
    }

    // PASS BUTTON
    public void StartPass()
    {
        PlayKickAnimation();
    }

    // KICK BUTTON
    public void StartKick()
    {
        PlayKickAnimation();
    }

    // SAME ANIMATION
    void PlayKickAnimation()
    {
        if (isShooting) return;

        isShooting = true;

        anim.SetBool("isRunning", false);

        anim.SetTrigger("Shoot");

        Invoke(nameof(StopShoot), 1f);
    }

    void StopShoot()
    {
        isShooting = false;
    }
}