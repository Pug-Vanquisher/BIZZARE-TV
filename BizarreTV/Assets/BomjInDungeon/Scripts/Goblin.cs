using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BID
{
    public class Goblin : MonoBehaviour
    {
        public GameObject player;
        private GameObject foundedPlayer;
        private CharacterController goblin;
        public Vector2 velik;
        public float speed;

        protected RaycastHit2D hit;

        public float attackRange;
        public float AttackInvoke;
        public float vlframes;
        protected float current_vlframes;

        public Collider2D HitCollider;
        public ParticleSystem warn;
        public float rangeOfVision;
        private bool playerFound;
        void Start()
        {
            goblin = GetComponent<CharacterController>();
            current_vlframes = 0;
        }
        public void Update()
        {
            GetComponent<SpriteRenderer>().sortingOrder = (int)(transform.position.y* -10);
            DrawCircle(rangeOfVision, 30);
        }
        public virtual void FixedUpdate()
        {
            velik = Vector3.zero;
            if(playerFound)
            {
                if (Vector2.Distance(transform.position, player.transform.position) <= attackRange & current_vlframes == 0 & !IsInvoking("Attack"))
                {
                    Invoke("Attack", AttackInvoke);
                }
                else if (current_vlframes == 0)
                {
                    Move();
                }
                else if (current_vlframes < 0) { current_vlframes = 0; }

                else { current_vlframes -= Time.deltaTime; }
            }
            else if(!playerFound)
            {
                if(foundedPlayer == null)
                {
                    Stand();
                }
                else
                {
                    if (!IsInvoking("Find"))
                    {
                        Invoke("Find", 0.5f);
                    }
                }
            }
        }
        public virtual void Stand()
        {
            List<GameObject> entities = new List<GameObject>();
            entities.AddRange(GameObject.FindGameObjectsWithTag("Player"));
            entities.AddRange(GameObject.FindGameObjectsWithTag("Goblin"));
            foreach (GameObject entity in entities)
            {
                if (entity != gameObject && (entity.transform.position - transform.position). magnitude <= rangeOfVision)
                {
                    if (entity.tag == "Player" && entity.name == "HitCollider")
                    {
                        warn.Play();
                        foundedPlayer = entity;
                    }
                    else if(entity.tag == "Goblin" && entity.name != "HitCollider")
                    {
                        if(entity.GetComponent<Goblin>() != null)
                        {
                            if (entity.GetComponent<Goblin>().player != null)
                            {
                                warn.Play();
                                foundedPlayer = entity.GetComponent<Goblin>().player;
                            }
                        }
                    }
                }
            }
        }
        void Find()
        {
            playerFound = true;
            player = foundedPlayer;
        }
        public virtual void Attack()
        {

        }
        public virtual void Move()
        {
            Vector2 Distance = player.transform.position - transform.position;

            velik = new Vector2(Mathf.Round(Distance.normalized.x), Mathf.Round(Distance.normalized.y)).normalized;

            /*
            if (Mathf.Abs(Distance.x) > Mathf.Abs(Distance.y))
            {
                if (Distance.x < 0) { velik.x = -1; }
                else { velik.x = 1; }
            }
            else
            {
                if (Distance.y < 0) { velik.y = -1; }
                else { velik.y = 1; }
            }
            
            if (Mathf.Abs(player.transform.position.x - transform.position.x) <= 0.2) { velik.x = 0; }
            if (Mathf.Abs(player.transform.position.y - transform.position.y) <= 0.2) { velik.y = 0; }

            if (player.transform.position.x >= transform.position.x) { velik.x = 1; }
            if (player.transform.position.x < transform.position.x) { velik.x = -1; }
            if (player.transform.position.y >= transform.position.y) { velik.y = 1; }
            if (player.transform.position.y < transform.position.y) { velik.y = -1; }

            */

            goblin.Move(velik.normalized * speed);
            hit = Physics2D.BoxCast(transform.position, Vector2.one, 0, velik, Vector2.Distance(transform.position, new Vector2(transform.position.x, transform.position.y) + velik.normalized * speed), LayerMask.GetMask("Actor", "Blocking"));

            if (hit.collider == null) {}
        }
        void DrawCircle(float radius, int cegments)
        {
            for (int i = 1; i < cegments; i++)
            {
                Vector3 pos1 = new Vector2(Mathf.Cos((Mathf.PI * 2 / cegments) * i), Mathf.Sin((Mathf.PI * 2 / cegments) * i));
                Vector3 pos2 = new Vector2(Mathf.Cos((Mathf.PI * 2 / cegments) * (i - 1)), Mathf.Sin((Mathf.PI * 2 / cegments) * (i - 1)));
                Debug.DrawLine(pos1 * radius + transform.position, pos2 * radius + transform.position, Color.red);
            }
        }
    }
}
