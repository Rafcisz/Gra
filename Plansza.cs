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
            for (int y=1; y<wiersze-1;y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    void BoardSetup()
    {
        boardHolder = new GameObject("Plansza").transform;
        for (int x=-1;x<kolumny+1;x++)
        {
            for (int y=-1;y<wiersze+1;y++)
            {
                GameObject toInstantiate = podloga[Random.Range(0, podloga.Length)];
                if (x == -1 || x == kolumny || y == -1 || y == wiersze)
                    toInstantiate = zewSciany[Random.Range(0, zewSciany.Length)];

                GameObject instace = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                instace.transform.SetParent(boardHolder);

            }
        }

    }

    Vector3 Polozenie()
    {
        int losowe = Random.Range(0, gridPositions.Count);
        Vector3 polozenie = gridPositions[losowe];
        gridPositions.RemoveAt(losowe);
        return polozenie;

    }

    void Ulozenie(GameObject[] tileArray,int minimum, int maximum)
    {
        int ile = Random.Range(minimum, maximum + 1);
        for (int i = 0; i < ile; i++) ;
        {
            Vector3 polozenie = Polozenie();
            GameObject ulozenie = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(ulozenie, polozenie, Quaternion.identity);
        }
    }

    public void Tworzenie()
    {
        BoardSetup();
        InitializeList();
        Ulozenie(sciana, IleScian.minimum, IleScian.maximum);
        Ulozenie(kasa, IleKasy.minimum, IleKasy.maximum);
        int ileWrogow = (int)Mathf.Log(level, 2f);
        Ulozenie(wrogowie, ileWrogow, ileWrogow);
        Instantiate(przejscie, new Vector3(kolumny - 1, wiersze - 1, 0f), Quaternion.identity);
    }
    
}
