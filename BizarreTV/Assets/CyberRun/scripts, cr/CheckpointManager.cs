using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static Vector3 lastCheckpointPosition = Vector3.zero;

    public static void ResetCheckpoint()
    {
        lastCheckpointPosition = Vector3.zero;
    }


}
