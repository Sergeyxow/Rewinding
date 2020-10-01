using System;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class ObjectSaver : MonoBehaviour
    {
        private void Start()
        {
            RewindManager.Instance.ObjectsToSave.Add(this);
        }

        public TypeDataPair[] GetDataToSave()
        {
            var savables = GetComponents<ISavable>();

            if (savables.Length < 1)
                throw new Exception("No savables was found");
            
            TypeDataPair[] data = new TypeDataPair[savables.Length];

            for (var i = 0; i < savables.Length; i++)
            {
                TypeDataPair pair = new TypeDataPair()
                {
                    Type = savables[i].GetType(),
                    Data = savables[i].Save()
                };
                data[i] = pair;
            }

            return data;
        }

        public void LoadData(TypeDataPair[] typeDataPairs)
        {
            var savables = GetComponents<ISavable>();

            if (savables.Length < 1)
                throw new Exception("No savables was found");

            foreach (var pair in typeDataPairs)
            {
                var savable = savables.First(x => x.GetType() == pair.Type);
                
                if (savable == null)
                    throw new Exception("Savable was not found");
                
                savable.Load(pair.Data);
            }
        }
    }
}