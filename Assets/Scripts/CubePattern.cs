using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubePattern : MonoBehaviour
{
    private int cubeCount = 6;
    List<int> cubeNumbers = new();
    List<int> playerNumbers = new();
    [SerializeField] GameObject[] cubes;
    [SerializeField] private Material activatedMaterial;
    [SerializeField] private Material defaultMaterial;
    public bool isPlayed;
    private static readonly int Door = Animator.StringToHash("OpenDoor");
    [SerializeField] private Animator animator;
    private bool canRecord;
    private Spawner spawner;

    void Start()
    {
        for (int i = 0; i < cubeCount; i++)
            cubeNumbers.Add(i);

        Shuffle(cubeNumbers);
        spawner = FindObjectOfType<Spawner>();
    }

    public void StartCoroutinePatternGame()
    {
        ResetPattern();
        isPlayed = true;
        StartCoroutine(StartPatternGame());
    }

    public void ResetPattern()
    {
        isPlayed = false;
        playerNumbers = new ();
    }

    IEnumerator StartPatternGame()
    {
        foreach (var num in cubeNumbers)
        {
            Renderer component = cubes[num].transform.GetComponent<Renderer>();
            component.material = activatedMaterial;
            yield return StartCoroutine(DeActivateCube(component));
        }
    }

    public void ActivateCube(Transform cubeTransform, int orderNum)
    {
        
        if (orderNum == default(int)) return;
        Renderer component = cubeTransform.GetComponent<Renderer>();
        component.material = activatedMaterial;
        playerNumbers.Add(orderNum - 1);
        //StartCoroutine(DeActivateCube(component));

        if (playerNumbers.Count != cubeNumbers.Count) return;
        if (!playerNumbers.SequenceEqual(cubeNumbers))
        {
            Debug.Log("reset - wrong answer");
            ResetPattern();
        }
        else if (playerNumbers.SequenceEqual(cubeNumbers))
        {
            animator.SetBool(Door, true);
            spawner.StartSpawning();
            
            Debug.Log("reset - right answer");
            ResetPattern();
        }
    }

    IEnumerator DeActivateCube(Renderer rendererComponent)
    {
        yield return new WaitForSeconds(1f);
        rendererComponent.material = defaultMaterial;
    }

    void Shuffle<T>(List<T> list)
    {
        var random = new System.Random();

        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
}