using UnityEngine;
using System.Collections;

namespace Completed
{
    
    public class Player : Moving
    {
        public float restartLevelDelay = 1f;        
        public int pointsKasa = 10;             
        public int pointsKlejnoty = 20;              
        public int wallDamage = 1;                  


        private Animator animator;                  
        private int cash;                          

        protected override void Start()
        {
           
            animator = GetComponent<Animator>();
            cash = GameManager.instance.playerCashPoints;
            base.Start();
        }

        private void OnDisable()
        {
            GameManager.instance.playerCashPoints = cash;
        }


        private void Update()
        {
            if (!GameManager.instance.playersTurn) return;

            int horizontal = 0;     
            int vertical = 0;       

            horizontal = (int)(Input.GetAxisRaw("Horizontal"));
            vertical = (int)(Input.GetAxisRaw("Vertical"));

            if (horizontal != 0)
            {
                vertical = 0;
            }

            if (horizontal != 0 || vertical != 0)
            {
                AttemptMove<Wall>(horizontal, vertical);
            }
        }

        protected override void AttemptMove<T>(int xDir, int yDir)
        {
            cash--;
            base.AttemptMove<T>(xDir, yDir);
            RaycastHit2D hit;

            if (Move(xDir, yDir, out hit))
            {

            }

            CheckIfGameOver();

            GameManager.instance.playersTurn = false;
        }


        protected override void OnCantMove<T>(T component)
        {
            Wall hitWall = component as Wall;
            hitWall.DamageWall(wallDamage);
            animator.SetTrigger("GraczKop");
        }

        private void Restart()
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Przejscie")
            {
                Invoke("Restart", restartLevelDelay);
                enabled = false;
            }

            else if (other.tag == "Kasa")
            {
                cash += pointsKasa;
                other.gameObject.SetActive(false);
            }

            else if (other.tag == "Klejnoty")
            {
                cash += pointsKlejnoty;
                other.gameObject.SetActive(false);
            }
        }

        public void LoseCash(int loss)
        {
            animator.SetTrigger("GraczHit");
            cash -= loss;
            CheckIfGameOver();
        }

        private void CheckIfGameOver()
        {
            if (cash <= 0)
            {

                //Call the GameOver function of GameManager.
                GameManager.instance.GameOver();
            }
        }
    }
}