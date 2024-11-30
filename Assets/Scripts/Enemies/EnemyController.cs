using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    public Player player;// Reference to the player
    public Enemy[] enemies;  // The dialogue graph if the player has a specific item

    private NodeParser nodeParser; // Reference to the NodeParser script

    private void Start()
    {
        enemies = GetComponentsInChildren<Enemy>();
        nodeParser = GetComponent<NodeParser>();
        if (nodeParser == null)
        {
            Debug.LogError("NodeParser script is missing on this NPC!");
        }
    }
    private void Update()
    {
        if (AreAllEnemiesDead())
        {
           player.allEnemiesKilled = true;
           player.northQuestComplete = true;
            // Add your logic for when all enemies are dead
        }
    }

    private bool AreAllEnemiesDead()
    {
        // Check if all enemies' isDead property is true
        return enemies != null && enemies.All(enemy => enemy.isDead);
    }
}