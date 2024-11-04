using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BID
{
    public class Goblin : MonoBehaviour
    {
        public GameObject player;

        private CharacterController goblin;
        public Vector2 velik;
        public float speed;

        public float attackRange;
        public float AttackInvoke;
        public float aggroRange;
        public float vlframes;
        protected float current_vlframes;

        public Collider2D HitCollider;

        void Start()
        {
            goblin = GetComponent<CharacterController>();
            HitCollider = GetComponentInChildren<Collider2D>();
            current_vlframes = 0;
        }
        public void Update()
        {
            var sprRender = GetComponent<SpriteRenderer>().sortingOrder = (int)(transform.position.y* -10);
        }
        public virtual void FixedUpdate()
        {
            velik = Vector3.zero;

            if (player == null) { Look(); }

            else if (Vector2.Distance(transform.position, player.transform.position) <= attackRange & current_vlframes == 0 & !IsInvoking("Attack")) { Invoke("Attack", AttackInvoke); }

            else if (current_vlframes == 0) { Move(); }

            else if (current_vlframes < 0) { current_vlframes = 0; }

            else { current_vlframes -= Time.deltaTime; }
        }

        public virtual void Attack()
        {

        }

        public void Look()
        {
            if ((GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).magnitude <= aggroRange) { player = GameObject.FindGameObjectWithTag("Player"); }

            else foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Goblin"))
                {
                    if ((obj.transform.position - transform.position).magnitude <= aggroRange && obj.GetComponent<Goblin>().player != null) { player = obj.GetComponent<Goblin>().player; }
                }

            current_vlframes = vlframes;
        }

        public virtual void Move()
        {
            Vector2 Distance = player.transform.position - transform.position;

            velik = new Vector2(Mathf.Round(Distance.normalized.x), Mathf.Round(Distance.normalized.y)).normalized;

            goblin.Move(velik.normalized * speed);
        }

    }
}
