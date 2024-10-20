using Jupiter731;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jupiter731
{
    public class HPBar : MonoBehaviour
    {
        [SerializeField] BaseUnit unit;
        [SerializeField] Image HP;
        private float _maxHP;


        private void Awake()
        {
            StartCoroutine(Init());
            
        }


        private void Update()
        {
            //StartCoroutine(Init());
            HP.fillAmount =  unit.PublicHP / _maxHP;
            Debug.Log(HP.fillAmount);
        }

        IEnumerator Init()
        {
            yield return new WaitForSeconds(0.1f);
            HP.type = Image.Type.Filled;
            HP.fillMethod = Image.FillMethod.Horizontal;
            _maxHP = unit.PublicHP;

        }
    }
}
