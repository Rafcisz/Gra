using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Completed
{
    using System.Collections.Generic;       

    public class GameManager : MonoBehaviour
    {
        public float levelStartDelay = 2f;
        public float turnDelay = .1f;
        public static GameManager instance = null;   
        private BoardManager boardScript;                       
        private int level = 1;
        public int playerCashPoints = 100;
        [HideInInspector]
        public bool playersTurn = true;

        private Text levelText;
        private GameObject levelImage;
        private List<Enemy> enemies;
        private bool enemiesMoving;
        private bool doingSetup;

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

        private void OnLevelWasLoaded (int index)
        {
            level++;

            InitGame();
        }

        public void GameOver()
        {
            levelText.text = "You pass " + level + "floors";
            levelImage.SetActive(true);
            enabled = false;
        }
       
        void InitGame()
        {
            doingSetup = true;

            levelImage = GameObject.Find("LevelImage");
            levelText = GameObject.Find("leveltext").GetComponent<Text>();
            levelText.text = "Floor " + level;
            levelImage.SetActive(true);
            Invoke("HideLevelImage", levelStartDelay);
            enemies.Clear();
            boardScript.SetupScene(level);

        }

        private void HideLevelImage()
        {
            levelImage.SetActive(false);
            doingSetup = false;
        }

        void Update()
        {
            if (playersTurn || enemiesMoving || doingSetup)
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