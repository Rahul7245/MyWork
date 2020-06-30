using UnityEngine;
using UnityEngine.AI;

public class CustomAgent : MonoBehaviour
{
    public NavMeshAgent PlayerAgent;
    public Transform[] TargetPoint;
    private int CurrDest = 0;

    void Start()
    {
        PlayerAgent.GetComponent<NavMeshAgent>();
        GotoNextPoint();
    }

    private void Update()
    {
        if (!PlayerAgent.pathPending && PlayerAgent.remainingDistance < 0.5f)
            GotoNextPoint();
    }

    void GotoNextPoint()
    {
        if (TargetPoint.Length == 0)
            return;

        CurrDest = Random.Range(0, TargetPoint.Length);
        Debug.Log(CurrDest);
        PlayerAgent.destination = TargetPoint[CurrDest].position;
    }
}