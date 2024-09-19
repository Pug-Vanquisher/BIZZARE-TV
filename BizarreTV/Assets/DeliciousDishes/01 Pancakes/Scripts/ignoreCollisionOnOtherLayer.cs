using UnityEngine;

public class ignoreCollisionOnOtherLayer : MonoBehaviour
{
    public int ignoreLayerNum = 15;
    private void Start() {
        Physics2D.IgnoreLayerCollision(gameObject.layer, ignoreLayerNum);
    }
}
