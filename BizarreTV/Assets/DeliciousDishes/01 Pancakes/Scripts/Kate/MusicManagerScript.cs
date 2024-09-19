using UnityEngine;

public class MusicManagerScript : MonoBehaviour
{
    private static MusicManagerScript instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
