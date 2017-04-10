using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public int score = 0;
    public GUIText scoreText;    
    public List<Transform> wayPoints;

    private SwapControlScript swapController = new SwapControlScript();
    private Transform activeShooters;

	// Use this for initialization
	void Start () {
        UpdateScore();

        HealthScript[] shooters = FindObjectsOfType<HealthScript>();
        foreach (HealthScript shooter in shooters)
        {
            AddListener(shooter);
        }

        PlayerObjectInteraction playerInteraction = FindObjectOfType<PlayerObjectInteraction>();        
        AddListener(playerInteraction);

        GameObject zeus = GameObject.Find("LightningController");
        swapController.lightningController = zeus.GetComponent<LightningControlScript>();

        // setup AI for enemies
        activeShooters = GameObject.Find("Enemies").transform;
        foreach(StateController shooterStateController in activeShooters.GetComponentsInChildren<StateController>())
        {
            shooterStateController.GetComponent<StateController>().SetupAI(true, wayPoints);
        }
	}

    void Update()
    {
        // TODO: do this with event like system
        if(activeShooters.childCount <= 0)
            gameObject.AddComponent<GameOverScript>();

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit(); 
    }

    private void AddListener(HealthScript shooter)
    {        
        shooter.OnUnitSwapped += HandleOnUnitSwapped;
    }

    private void AddListener(PlayerObjectInteraction player)
    {
        player.OnMultipleSwap += HandleOnMultipleSwap;
    }

    private void RemoveListener(HealthScript shooter)
    {
        shooter.OnUnitSwapped -= HandleOnUnitSwapped;
    }

    private void HandleOnUnitSwapped(GameObject unit1, GameObject unit2)
    {
        // Do something useful here        
        swapController.Swap(unit1, unit2);
    }

    private void HandleOnMultipleSwap(List<SwappingCouple> couples, int damage)
    {
        swapController.SwapMultiple(couples, damage);
    }
		

    public void UpdateScore()
    {
        scoreText.text = "score: " + score;
    }

    public void IncrementScore()
    {
        score++;
        UpdateScore();
    }
}
