using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LT
{
    public class PointManager : MonoBehaviour
    {
        private static PointManager _instance;
        public static PointManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject obj = new GameObject("PointManager");
                    _instance = obj.AddComponent<PointManager>();
                }
                return _instance;
            }
        }


        public Vector3 GetPoint(string pointName)
        {
            foreach (Transform point in GameObject.Find("Points").transform)
            {
                if (point.name == pointName)
                {
                    return point.position;
                }
            }
            return Vector3.zero;
        }
    }
}