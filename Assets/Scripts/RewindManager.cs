using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

public class RewindManager : MonoBehaviour
{
    public static RewindManager Instance;

    public List<ObjectSaver> ObjectsToSave;

    private float saveInterval = 0.25f;
    private float timeElapsed = 0f;

    private Dictionary<ObjectSaver, TypeDataPair[]> dict;
    
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        else
            Instance = this;
        
        ObjectsToSave = new List<ObjectSaver>();
        dict = new Dictionary<ObjectSaver, TypeDataPair[]>();
    }

    private void Update()
    {
        if (timeElapsed >= saveInterval)
        {
            timeElapsed = 0;
            //Save();
        }
        else
        {
            timeElapsed += Time.deltaTime;
        }
    }

    [Button()]
    private void Save()
    {
        foreach (var objectSaver in ObjectsToSave)
        {
            dict[objectSaver] = objectSaver.GetDataToSave();
        }
    }

    [Button()]
    private void Load()
    {
        foreach (var objectSaver in ObjectsToSave)
        {
            objectSaver.LoadData(dict[objectSaver]);
        }
    }
}
