using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using Assets.Scripts.Level;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof (InputController))]
    public class PlayerController : AbstractMovable
    {
        [SerializeField] private int sightRadius = 4;
        public int facing = 1;
        private int damageDealt = 1;
        private int playerHealth = 5;
        private int maxHealth = 5;
        private int potionRestore = 5;
        private int healthPotions = 0;
        private bool canAct = false;

        private Sprite fullHeart = Resources.Load< Sprite >("FullHeart");
        private Sprite emptyHeart = Resources.Load<Sprite>("EmptyHeart");

        private HealthList healthList;

        // Use this for initialization
        public void Start()
        {
            base.Start();
            GameObject.DontDestroyOnLoad(gameObject);
            damage = damageDealt;
            health = playerHealth;
            healthList = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthList>();
            checkHealth();
        }

        // Update is called once per frame
        public void Update()
        {
            ClearFog();
            for (int i = 0; i < health; i++)
            {
                healthList.containers[health - i].GetComponent<Image>().sprite = fullHeart;
            }
            for (int i = 0; i < maxHealth - health; i++)
            {
                healthList.containers[maxHealth - i].GetComponent<Image>().sprite = emptyHeart;
            }
        }

        public void EnableAction()
        {
            canAct = true;
        }

        public override void Move()
        {
            
        }

        public void Move(int x, int y)
        {
            // Only execute if move ready and control has been input
            if (!(Mathf.Abs(x) > 0 || Mathf.Abs(y) > 0) || !canAct)
            {
                return;
            }

            // Execute movement
            if (Mathf.Abs(x) > 0)
            {
                if (TryMove(new Vector2(transform.position.x + LevelManager.UnitSize*x, transform.position.y),
                    new Vector2(x, 0)))
                {
                    ClearFog();
                }
            }
            else if (Mathf.Abs(y) > 0)
            {
                if (TryMove(new Vector2(transform.position.x, transform.position.y + LevelManager.UnitSize*y),
                    new Vector2(0, y)))
                {
                    ClearFog();
                }
            }
            // Start the LevelManager counting down to the next move
            levelManager.EnableCount();
            canAct = false;
        }

        protected override bool TryMove(Vector2 newPosition, Vector2 direction)
        {
            Animate(direction);
            // For figuring out direction when switching equipment
            if (direction.x > 0)
            {
                facing = 0;
            }
            else if (direction.x < 0)
            {
                facing = 2;
            }
            else if (direction.y > 0)
            {
                facing = 1;
            }
            else if (direction.y < 0)
            {
                facing = 3;
            }
            RaycastHit2D raycast = Physics2D.Raycast(transform.position, direction, 16f, levelManager.WallLayer);
            if (raycast.transform == null)
            {
                // Check for staircase
                raycast = Physics2D.Raycast(transform.position, direction, 16f, levelManager.StaircaseLayer);
                if (raycast.transform != null)
                {
                    Application.LoadLevel(raycast.collider.GetComponent<Staircase>().nextLevel);
                }
                // Check for item
                RaycastHit2D[] raycasts = Physics2D.RaycastAll(transform.position, direction, 16f, levelManager.ItemLayer);
                bool encounteredItem = false;
                foreach (RaycastHit2D item in raycasts.Where(item => item.collider != null))
                {
                    item.collider.GetComponent<IItem>().ReceiveItem(this);
                    encounteredItem = true;
                }
                if (encounteredItem)
                {
                    return false;
                }
                collider.enabled = false;
                raycast = Physics2D.Raycast(transform.position, direction, 16f, levelManager.EnemyLayer);
                collider.enabled = true;
                if (raycast.transform == null)
                {
                    transform.position = newPosition;

                    return true;
                }
                if (raycast.collider.GetComponent<AbstractMovable>() != null)
                {
                    AttackEnemy(raycast.collider.GetComponent<AbstractMovable>());
                }
                return false;
            }
            return false;
        }

        public void Equip(string itemName)
        {
            if (itemName == "BlueSword")
            {
                animator.SetBool("BlueSword", true);
                animator.SetBool("GoldSword", false);
            }
            else if (itemName == "GoldSword")
            {
                animator.SetBool("GoldSword", true);
                animator.SetBool("BlueSword", false);
            }
            if (facing == 0 || facing == 2)
            {
                animator.SetTrigger("Side");
            }
            else if (facing == 1)
            {
                animator.SetTrigger("Up");
            }
            else if (facing == 3)
            {
                animator.SetTrigger("Down");
            }
        }

        public void GetPotion()
        {
            healthPotions += 1;
        }

        public void UsePotion()
        {
            if (healthPotions > 0)
            {
                healthPotions -= 1;
                health += potionRestore;
                if (health > maxHealth)
                {
                    health = maxHealth;
                }
                print(health);
                checkHealth();
            }
        }

        private void checkHealth()
        {
            for (int i = 0; i < maxHealth; i++)
            {
                healthList.containers[i].gameObject.GetComponent<Image>().sprite = i < health ? fullHeart : emptyHeat;
            }
        }

        private void ClearFog()
        {
            // Remove fog close to player
            Collider2D[] revealedFog = Physics2D.OverlapCircleAll(transform.position, sightRadius*LevelManager.UnitSize,
                levelManager.FogLayer);
            foreach (GameObject fog in revealedFog.Select(item => item.gameObject))
            {
                fog.SetActive(false);
            }
            // Reduce opacity of fog far from player
            revealedFog = Physics2D.OverlapCircleAll(transform.position, (sightRadius + 2)*LevelManager.UnitSize,
                levelManager.FogLayer);
            foreach (GameObject fog in revealedFog.Select(item => item.gameObject))
            {
                if (fog.activeSelf)
                {
                    fog.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, .5f);
                }
            }
        }

        protected override void Die()
        {
            Application.LoadLevel("TestScene");
            levelManager.DestroyObject(this);
        }
    }
}