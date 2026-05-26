using UnityEngine;

public class SimpleBall : MonoBehaviour
{
    public Transform player;

    // Dribble speed
    public float moveSpeed = 6f;

    // Roll speed
    public float rotateSpeed = 500f;

    // PASS / SHOOT / KICK
    public float passSpeed = 10f;
    public float shootSpeed = 20f;
    public float kickSpeed = 60f;

    // Ball velocity
    private Vector3 shotVelocity;

    void Update()
    {
        // Distance from player
        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        // DRIBBLE
        if (distance < 0.6f)
        {
            Vector3 dir = player.forward.normalized;

            // Move ball
            transform.position +=
                dir * moveSpeed * Time.deltaTime;

            // REAL BALL ROLL
            Vector3 rollAxis =
                Vector3.Cross(Vector3.up, dir);

            transform.Rotate(
                rollAxis,
                rotateSpeed * Time.deltaTime,
                Space.World
            );
        }

        // SHOT MOVEMENT
        transform.position +=
            shotVelocity * Time.deltaTime;

        // Smooth slow down
        shotVelocity = Vector3.Lerp(
            shotVelocity,
            Vector3.zero,
            0.15f * Time.deltaTime
        );

        // SHOT ROLL
        if (shotVelocity.magnitude > 0.01f)
        {
            Vector3 shotDir =
                shotVelocity.normalized;

            Vector3 rollAxis =
                Vector3.Cross(Vector3.up, shotDir);

            transform.Rotate(
                rollAxis,
                shotVelocity.magnitude *
                900f *
                Time.deltaTime,
                Space.World
            );
        }
    }

    // PASS
    public void Pass()
    {
        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        if (distance < 1.2f)
        {
            Invoke(nameof(StartPass), 0.6f);
        }
    }

    void StartPass()
    {
        shotVelocity =
            player.forward * passSpeed;
    }

    // SHOOT
    public void Shoot()
    {
        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        if (distance < 1.2f)
        {
            Invoke(nameof(StartShoot), 0.6f);
        }
    }

    void StartShoot()
    {
        shotVelocity =
            player.forward * shootSpeed;
    }

    // KICK
    public void Kick()
    {
        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        if (distance < 1.2f)
        {
            Invoke(nameof(StartKick), 0.6f);
        }
    }

    void StartKick()
    {
        shotVelocity =
            player.forward * kickSpeed;
    }
}