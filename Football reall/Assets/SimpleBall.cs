using UnityEngine;

public class SimpleBall : MonoBehaviour
{
    public Transform player;

    // Dribble speed
    public float moveSpeed = 6f;

    // Roll speed
    public float rotateSpeed = 700f;

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
        if (distance < 0.7f)
        {
            Vector3 dir = player.forward;

            // Move ball
            transform.position +=
                dir * moveSpeed * Time.deltaTime;

            // REALISTIC ROLL
            transform.Rotate(
                new Vector3(
                    -dir.z,
                    0,
                    dir.x
                ),
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

        // REALISTIC SHOT ROLL
        if (shotVelocity.magnitude > 0.01f)
        {
            transform.Rotate(
                new Vector3(
                    -shotVelocity.z,
                    0,
                    shotVelocity.x
                ),
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