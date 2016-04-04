using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Plansza : MonoBehaviour {

    [Serializable]
    public class Ile
    {
        public int minimum;
        public int maximum;

        public Ile (int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public int kolumny=8;
    public int wiersze=8;
    public Ile IleScian = new Ile(5, 9);
    public Ile IleKasy = new Ile(2, 4);
    public GameObject przejscie;
    public GameObject[] podloga;
    public GameObject[] sciana;
    public GameObject[] kasa;
    public GameObject[] wrogowie;
    public GameObject[] zewSciany;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    void InitializeList()
    {
        gridPositions.Clear();

        for (int x=1; x<kolumny-1;x++)
        {
            for (y=1; y<wiersze-1;y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    void BoardSetup()
    {
        boardHolder = new GameObject("Plansza").transform;


    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
