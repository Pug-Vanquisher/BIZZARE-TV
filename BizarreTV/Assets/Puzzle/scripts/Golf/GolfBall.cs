using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBall : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<GolfHole>())
        {
            if (collision.gameObject.GetComponent<GolfHole>().isActive)
            {
                collision.gameObject.GetComponent<GolfHole>().RiseFlag();
                GolfMinigameController.ballRolledAtHole?.Invoke();
            }

        }

        if (collision.gameObject.GetComponent<GolfRespawnArea>())
        {
            GolfMinigameController.ballRolledAway?.Invoke();
        }
    }

}
