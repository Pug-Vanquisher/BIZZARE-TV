using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pogodi
{
    public class Player : MonoBehaviour
    {
        [SerializeField] protected GameManager gm;
        [SerializeField] GameObject playerCube;
        [SerializeField] Transform playerCoord;
        [SerializeField] float playerSpeed;
        float xMovement;


        private void Move(float xMovement)
        {
            playerCoord.Translate(xMovement, 0, 0);
        }

        private void TeleporToAnotherBound(float CoordX)
        {
            if (CoordX > 14)
            {
                playerCoord.position = new Vector3(-13.5f, 0, 0);
            }
            else
            {
                playerCoord.position = new Vector3(13.5f, 0, 0);
            }
        }

        void Awake()
        {
            gm.OnGameRestarted += OnGameRestarted;
            gm.OnPlayerSpeedBuffed += OnPlayerSpeedBuffed;
            playerCube = GetComponent<GameObject>();
            playerCoord = GetComponent<Transform>();
            playerSpeed = 1f;
        }

        void FixedUpdate()
        {
            xMovement = Input.GetAxis("Horizontal");
            Move(xMovement * playerSpeed);
            if (playerCoord.position.x > 14f || playerCoord.position.x < -14f)
            {
                TeleporToAnotherBound(playerCoord.position.x);
            }
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        void OnPlayerSpeedBuffed()
        {
            playerSpeed = 1.3f;
            Timer.StartTimer(this, 15f, () =>
            {
                playerSpeed = 1f;
            });
        }
        void OnGameRestarted()
        {
            playerCoord.position = new Vector3(0, 0, 0);
        }
    }
}