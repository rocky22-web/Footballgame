using UnityEngine;

public class SimpleBall : MonoBehaviour
{
    public Transform player;

    // Dribble speed
    public float moveSpeed = 6f;

    // Roll speed
    public float rotateSpeed = 250f;

    // PASS / SHOOT / KICK
    public float passSpeed = 10f;
    public float shootSpeed = 28f;
    public float kickSpeed = 35f;

    // Ball velocity
    private Vector3 shotVelocity;

    // Air velocity
    private float airVelocity = 0f;

    void Update()
    {
        // Distance from player
        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        // DRIBBLE
        if (distance < 0.7f && transform.position.y <= 0.51f)
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

        // Stop if player blocks ball
        if (distance < 0.8f &&
            shotVelocity.magnitude > 1f)
        {
            shotVelocity *= 0.85f;
        }

        // REALISTIC SHOT ROLL
        if (shotVelocity.magnitude > 0.5f)
        {
            Vector3 shotDir =
                shotVelocity.normalized;

            Vector3 rollAxis =
                Vector3.Cross(Vector3.up, shotDir);

            transform.Rotate(
                rollAxis,
                shotVelocity.magnitude *
                250f *
                Time.deltaTime,
                Space.World
            );
        }
        else
        {
            // FULL STOP
            shotVelocity = Vector3.zero;
        }

        // SLOW DOWN
        shotVelocity = Vector3.Lerp(
            shotVelocity,
            Vector3.zero,
            0.5f * Time.deltaTime
        );

        // GRAVITY
        airVelocity -= 20f * Time.deltaTime;

        transform.position +=
            Vector3.up *
            airVelocity *
            Time.deltaTime;

        // GROUND COLLISION + REALISTIC BOUNCE
        if (transform.position.y <= 0.5f)
        {
            transform.position = new Vector3(
                transform.position.x,
                0.5f,
                transform.position.z
            );

            // Small bounce
            if (Mathf.Abs(airVelocity) > 1f)
            {
                airVelocity *= -0.35f;

                // lose speed after bounce
                shotVelocity *= 0.85f;
            }
            else
            {
                airVelocity = 0f;
            }
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
            Invoke(nameof(StartPass), 0.3f);
        }
    }

    void StartPass()
    {
        // Ground pass
        shotVelocity =
            player.forward * passSpeed;

        airVelocity = 0f;
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
            Invoke(nameof(StartShoot), 0.45f);
        }
    }

    void StartShoot()
    {
        // Power shot
        shotVelocity =
            player.forward * shootSpeed;

        // Extra forward push
        shotVelocity *= 1.3f;

        // Air curve
        airVelocity = 10f;
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
            Invoke(nameof(StartKick), 0.5f);
        }
    }

    void StartKick()
    {
        // Big air kick
        shotVelocity =
            player.forward * kickSpeed;

        airVelocity = 12f;
    }
}