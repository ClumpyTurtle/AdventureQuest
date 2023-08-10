using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Combat : MonoBehaviour
{
    public playerMovement playerMovement;
    public SceneTransitionManager transitionManager;

    public Button attackButton;

    public int playerHealth;
    public int enemyHealth;

    public int playerAttack;
    public int enemyAttack;

    GameObject playerObject;
    GameObject enemyObject;

    int currentPlayerHealth;
    int currentEnemyHealth;

    string currentTurn;
    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();

        playerObject = GameObject.FindGameObjectWithTag("Player");
        enemyObject = GameObject.FindGameObjectWithTag("Enemy");
        
        playerObject.transform.position = new Vector3(3.0f, 0.0f, 1.0f);
        playerObject.transform.localScale = new Vector3(4.0f, 4.0f, 0.0f);

        enemyObject.transform.position = new Vector3(-3.0f, 0.0f, 1.0f);
        enemyObject.transform.localScale = new Vector3(2.0f, 2.0f, 0.0f);

        enemyHealth = enemyObject.GetComponent<enemyInfo>().enemyHealth;
        enemyAttack = enemyObject.GetComponent<enemyInfo>().enemyAttack;

        playerHealth = playerObject.GetComponent<playerInfo>().playerHealth;
        playerAttack = playerObject.GetComponent<playerInfo>().playerAttack;

        currentEnemyHealth = enemyHealth;
        currentPlayerHealth = playerHealth;

        attackButton.onClick.AddListener(delegate () { this.ButtonClicked(); });
        attackButton.enabled = false;

        PlayerTurn();
    }

    void ButtonClicked()
    {
        currentEnemyHealth -= playerAttack;
        Debug.Log("Player has attacked and dealt " + playerAttack + " damage.");
        Debug.Log("Enemy has" + currentEnemyHealth + "health left.");
        currentTurn = "Enemy";
        attackButton.enabled = false;
    }

    private async void PlayerTurn()
    {
        currentTurn = "Player";
        attackButton.enabled = true;
        while (currentTurn == "Player")
        {
            await Task.Delay(100);
        }
        switch (CheckHealth())
        {
            case 2:
                Destroy(enemyObject);
                GameObject.Find("ObjectsToDestroy").GetComponent<CheckingObjects>().objectsToDestroy.Add("Enemy");
                CombatEnded();
                break;
            case 3:
                EnemyTurn();
                break;
        }
    }

    private void EnemyTurn()
    {
        currentPlayerHealth -= enemyAttack;
        Debug.Log("Enemy has attacked and dealt " + enemyAttack + " damage.");
        Debug.Log("Player has" + currentPlayerHealth + "health left.");
        switch (CheckHealth()) {
            case 1:
                Destroy(playerObject);
                GameObject.Find("ObjectsToDestroy").GetComponent<CheckingObjects>().objectsToDestroy.Add("Player");
                CombatEnded();
                break;
            case 3:
                PlayerTurn();
                break;
        }
    }

    private int CheckHealth()
    {
        if (currentPlayerHealth <= 0)
        {
            Debug.Log("Player died");
            return 1;
        }

        if (currentEnemyHealth <= 0)
        {
            Debug.Log("Enemy Died");
            return 2;
        }

        return 3;
    }

    private void CombatEnded()
    {
        ReturnToPlay();
        Destroy(enemyObject);
    }

    private void ReturnToPlay()
    {
        transitionManager.FadeToScene(0);
        Destroy(playerObject);
    }
}
