using System;
using System.Collections.Generic;
using Rogalik.Scripts.Buff.Base;
using Rogalik.Scripts.Player;
using UnityEditor;
using UnityEngine;

namespace Rogalik.Scripts.Buff
{
    public class AddBuff : MonoBehaviour
    {
        [HideInInspector] [SerializeField] private bool isTemporary;
        [HideInInspector] [SerializeField] private float temporaryTime;
        [HideInInspector] [SerializeField] private BuffType buffType;
        [HideInInspector] [SerializeField] private float buffValue;

        private static readonly Dictionary<BuffType, Func<float, IBuff>> BuffCreators = new()
        {
            { BuffType.Speed, value => new SpeedBuff((int)value) },
            { BuffType.Damage, value => new DamageBuff((int)value) },
            { BuffType.Heal, value => new HealBuff((int)value, RoguelikePlayer.Instance) },
            { BuffType.Immortal, _ => new ImmortalBuff() }
        };

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                IBuff buff = CreateBuff();
                if (isTemporary)
                {
                    buff = new TemporaryBuff(RoguelikePlayer.Instance, buff, temporaryTime);
                    TempBuffLogger.Instance.LogBuff(buff.TempBuffInfo, temporaryTime);
                }

                RoguelikePlayer.Instance.AddBuff(buff);
                BuffLogger.Instance.LogBuff(buff.BuffInfo);

                if (buffType is BuffType.Heal)
                    RoguelikePlayer.Instance.RemoveBuff(buff);

                Destroy(gameObject);
            }
        }

        private IBuff CreateBuff()
        {
            if (!BuffCreators.TryGetValue(buffType, out var buffCreator))
            {
                throw new ArgumentException("Unknown buff type.");
            }

            return buffCreator(buffValue);
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(AddBuff))]
    public class AddBuffEditor : Editor
    {
        private SerializedProperty isTemporary;
        private SerializedProperty temporaryTime;
        private SerializedProperty buffType;
        private SerializedProperty buffValue;

        private void OnEnable()
        {
            isTemporary = serializedObject.FindProperty("isTemporary");
            temporaryTime = serializedObject.FindProperty("temporaryTime");
            buffType = serializedObject.FindProperty("buffType");
            buffValue = serializedObject.FindProperty("buffValue");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(buffType);
            if (buffType.enumValueIndex is (int)BuffType.Speed or (int)BuffType.Damage or (int)BuffType.Heal)
                EditorGUILayout.PropertyField(buffValue);

            if (buffType.enumValueIndex is (int)BuffType.Speed or (int)BuffType.Damage or (int)BuffType.Immortal)
                EditorGUILayout.PropertyField(isTemporary);

            if (isTemporary.boolValue)
                EditorGUILayout.PropertyField(temporaryTime);

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}