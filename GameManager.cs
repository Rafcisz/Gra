using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Plansza skryptPlanszy;

    private int level = 5;

	// Use this for initialization
	void Awake ()
    {
        skryptPlanszy = GetComponent<Plansza>();
        InitGame();
	}

    void InitGame()
    {
        skryptPlanszy.Tworzenie(level);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
