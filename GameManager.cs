using UnityEngine;
using System.Collections;

namespace Completed
{
    using System.Collections.Generic;       

    public class GameManager : MonoBehaviour
    {
        public float turnDelay = .1f;
        public static GameManager instance = null;   
        private BoardManager boardScript;                       
        private int level = 5;
        public int playerCashPoints = 100;
        [HideInInspector]
        public bool playersTurn = true;

        private List<Enemy> enemies;
        private bool enemiesMoving;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
            enemies = new List<Enemy>();
            boardScript = GetComponent<BoardManager>();
            InitGame();
        }

        public void GameOver()
        {
            enabled = false;
        }
       
        void InitGame()
        {
            enemies.Clear();
            boardScript.SetupScene(level);

        }

        void Update()
        {
            if (playersTurn || enemiesMoving)
                return;

            StartCoroutine(MoveEnemies());
        }

        public void AddEnemyToList(Enemy script)
        {
            enemies.Add(script);
        }

        IEnumerator MoveEnemies()
        {
            enemiesMoving = true;
            yield return new WaitForSeconds(turnDelay);

            if (enemies.Count == 0)
            {
                yield return new WaitForSeconds(turnDelay);
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].MoveEnemy();
                yield return new WaitForSeconds(enemies[i].moveTime);
            }

            playersTurn = true;
            enemiesMoving = false;
        }


        
    }
}