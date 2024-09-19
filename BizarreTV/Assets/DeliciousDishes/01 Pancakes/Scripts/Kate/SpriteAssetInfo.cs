using UnityEngine;
using TMPro;

public class SpriteAssetInfo : MonoBehaviour
{
    public TMP_SpriteAsset spriteAsset;

    void Start()
    {
        /*if (spriteAsset != null && spriteAsset.spriteCharacterLookupTable.Count > 0)
        {
            TMP_SpriteCharacter spriteCharacter = spriteAsset.spriteCharacterLookupTable[0];

            if (spriteCharacter != null)
            {
                int unicode = GetSpriteIndexFromUnicode(spriteCharacter.unicode);
                char character = (char)unicode;

                Debug.Log($"Sprite Unicode: {unicode}, Character: {character}");
            }
        }*/
    }
}
