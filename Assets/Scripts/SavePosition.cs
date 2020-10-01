using UnityEngine;

namespace DefaultNamespace
{
    public class SavePosition : MonoBehaviour, ISavable
    {
        public ISavableData Save()
        {
            return new Position()
            {
                Value = transform.position
            };
        }

        public void Load(ISavableData data)
        {
            Position position = (Position) data;
            transform.position = position.Value;
        }

        public struct Position : ISavableData
        {
            public Vector3 Value;
        }
    }
}