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

    private int queueLength = 4 * 5;


    private Queue<Dictionary<ObjectSaver, TypeDataPair[]>> _queue;
    
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
        _queue = new Queue<Dictionary<ObjectSaver, TypeDataPair[]>>(queueLength);
        
    }

    private void Update()
    {
        if (timeElapsed >= saveInterval)
        {
            timeElapsed = 0;
            Save();
        }
        else
        {
            timeElapsed += Time.deltaTime;
        }
        
    }

    [Button()]
    private void Save()
    {
        Dictionary<ObjectSaver, TypeDataPair[]> dict = new Dictionary<ObjectSaver, TypeDataPair[]>();
        foreach (var objectSaver in ObjectsToSave)
        {
            dict[objectSaver] = objectSaver.GetDataToSave();
        }

        if (_queue.Count >= queueLength)
            _queue.Dequeue();
        
        _queue.Enqueue(dict);
    }

    [Button()]
    private void Load()
    {
        foreach (var objectSaver in ObjectsToSave)
        {
            var dict = _queue.Peek();
            objectSaver.LoadData(dict[objectSaver]);
        }
    }
}
