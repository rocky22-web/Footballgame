using UnityEngine;
using TMPro;

public class Goal : MonoBehaviour
{
    public Transform player;
    public Transform ball;

    public Transform playerSpawn;
    public Transform ballSpawn;

    public TextMeshProUGUI scoreText;

    int leftScore = 0;
    int rightScore = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            leftScore++;

            scoreText.text =
                leftScore + "  " + rightScore;

            player.position =
                playerSpawn.position;

            ball.position =
                ballSpawn.position;

            ball.GetComponent<SimpleBall>()
                .StopBall();
        }
    }
}