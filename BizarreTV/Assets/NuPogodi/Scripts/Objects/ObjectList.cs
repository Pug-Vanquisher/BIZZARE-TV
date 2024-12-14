using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pogodi
{
    [CreateAssetMenu(menuName = "ObjectList")]
    public class ObjectList : ScriptableObject
    {
        [SerializeField] GameObject[] objects;

        public GameObject getObject(int index)
        {
            return objects[index];
        }

        public int getLength()
        {
            return objects.Length;
        }
    }
}