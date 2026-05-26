using UnityEngine;

public class SimpleBall : MonoBehaviour
{
    public Transform player;

    // Ball move
    Vector3 move;

    // Roll speed
    public float rollSpeed = 500f;

    void Update()
    {
        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        // Ball player এর সাথে যাবে
        if (distance < 0.7f)
        {
            Vector3 dir =
                player.forward.normalized;

            transform.position +=
                dir *
                6f *
                Time.deltaTime;

            // Roll
            Vector3 rollAxis =
                Vector3.Cross(Vector3.up, dir);

            transform.Rotate(
                rollAxis,
                rollSpeed *
                Time.deltaTime,
                Space.World
            );
        }

        // Ball move
        transform.position +=
            move *
            Time.deltaTime;

        // Roll while moving
        if (move.magnitude > 0.1f)
        {
            Vector3 dir =
                move.normalized;

            Vector3 rollAxis =
                Vector3.Cross(Vector3.up, dir);

            transform.Rotate(
                rollAxis,
                move.magnitude *
                40f *
                Time.deltaTime,
                Space.World
            );
        }

        // Slow down
        move = Vector3.Lerp(
            move,
            Vector3.zero,
            0.2f * Time.deltaTime
        );

        // Stop if player blocks ball
        if (distance < 0.6f &&
            move.magnitude > 0.1f)
        {
            move = Vector3.zero;
        }
    }

    // PASS
    public void Pass()
    {
        move = player.forward * 5f;
    }

    // SHOOT
    public void Shoot()
    {
        move = player.forward * 10f;
    }

    // KICK
    public void Kick()
    {
        move = player.forward * 20f;
    }

    // STOP BALL
    public void StopBall()
    {
        move = Vector3.zero;
    }
}