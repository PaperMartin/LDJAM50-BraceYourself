using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject DefaultSection;
    [SerializeField]
    private List<GameObject> LevelPrefabs = new List<GameObject>();
    [SerializeField]
    private float SectionLength = 16f;
    [SerializeField]
    private float OffsetFromPlayer = 64;

    private Queue<GameObject> CurrentQueue = new Queue<GameObject>();
    private PlaneController controller;

    private float TargetZ = -16;

    private void OnEnable()
    {
        controller = FindObjectOfType<PlaneController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        RefillQueue();
    }

    private void RefillQueue()
    {
        LevelPrefabs.Shuffle();
        for (int i = 0; i < LevelPrefabs.Count; i++)
        {
            CurrentQueue.Enqueue(LevelPrefabs[i]);
        }
    }

    private void SpawnLevelSection()
    {
        GameObject prefab = CurrentQueue.Dequeue();
        Instantiate(prefab, new Vector3(0, 0, TargetZ), Quaternion.identity);

        if (CurrentQueue.Count == 0)
        {
            RefillQueue();
        }
    }

    private void SpawnDefaultSection()
    {
        Instantiate(DefaultSection, new Vector3(0, 0, TargetZ), Quaternion.identity);
    }

    public void OnGameFixedUpdate()
    {
        while (controller.transform.position.z + OffsetFromPlayer >= TargetZ)
        {
            SpawnLevelSection();
            TargetZ += SectionLength;
        }
    }

    public void OnGameReset()
    {
        TargetZ = -16;
    }
}
