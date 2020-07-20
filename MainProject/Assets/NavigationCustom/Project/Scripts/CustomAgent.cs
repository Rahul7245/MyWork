using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class CustomAgent : MonoBehaviour
{
    public NavMeshAgent PlayerAgent; // Spawn single prefab with multiple properties 
    private int CurrDest = 0;
    private Animator anim;
    private bool isReached = false;
    List<int> NumberPool;
    public Transform GoalParent;
    public String EnName;
    public bool isWalkable = false;
    public Transform[] TargetPoint;
    public int[] PathPoints;
    private int PrevIndex = 0;
    SpawnEnemy spawnEnemy;

    private void Awake()
    {
        GoalParent = GameObject.FindGameObjectWithTag("Respawn").transform;
        spawnEnemy = GameObject.FindObjectOfType<SpawnEnemy>();
        PlayerAgent.GetComponent<NavMeshAgent>();
        PlayerAgent.updateRotation = false; // Making this false as to rotate it manually
        anim = this.GetComponent<Animator>();
        for (int i = 0; i < TargetPoint.Length; i++)
        {
            int index = PathPoints[i];
            TargetPoint[i] = GoalParent.GetChild(index);
        }
    }

    void Start()
    {
        Check();
    }

    public void Check()
    {
        RestartRandom();
        isWalkable = false;
        int len=spawnEnemy.WalkableEn.Length;
        for (int i = 0; i < len; i++)
        {
            if (spawnEnemy.WalkableEn[i] == EnName)
            {
                isWalkable = true;
            }
        }
        if (isWalkable)
        {
            anim.Play("Walk");
            UniqueRandom();
        }
    }

    private void Update()
    {
        if (!isWalkable)
            return;

        if (!PlayerAgent.pathPending && PlayerAgent.remainingDistance < 0.1f && !isReached)
        {
            isReached = true;
            GotoNextPoint();
        }
    }

    void GotoNextPoint()
    {
        if (TargetPoint.Length == 0)
            return;

        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        PlayerAgent.updateRotation = false;
        PlayerAgent.isStopped = true;
        anim.Play("Stand");
        yield return new WaitForSeconds(4f);

        PlayerAgent.isStopped = false;
        anim.Play("Walk");
        UniqueRandom();
        isReached = false;
    }

    private void RotateTowards(Transform target) // Method to turn the navmesh agent manually in place
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 90);
        PlayerAgent.updateRotation = true;
    }

    //Random number generation without repeating in loop 
    public void RestartRandom()
    {
        NumberPool = new List<int>();
        for (int i = 0; i < TargetPoint.Length; i++)
        {
            NumberPool.Add(i);
        }
    }

    public void UniqueRandom()
    {
        if (NumberPool.Count == 0)
            RestartRandom();
        
        int index = Random.Range(0, NumberPool.Count);
        if (PrevIndex == index) //If Previous random number is same as present
        {
            UniqueRandom();
        }
        else
        {
            CurrDest = NumberPool[index];
            NumberPool.RemoveAt(index);
            PrevIndex = index;

            PlayerAgent.destination = TargetPoint[CurrDest].position;
            RotateTowards(TargetPoint[CurrDest]);
        }
    }
}