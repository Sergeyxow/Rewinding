using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class SaveColor : MonoBehaviour, ISavable
    {
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public ISavableData Save()
        {
            return new Color()
            {
                value = _spriteRenderer.color 
            };
        }

        public void Load(ISavableData data)
        {
            Color color = (Color) data;
            GetComponent<SpriteRenderer>().color = color.value;
        }
        
        public struct Color : ISavableData
        {
            public UnityEngine.Color value;
        }
    }
}