using UnityEngine;

public class FollowAP : MonoBehaviour
{
    Transform goal;
    float speed = 5.0f;
    float accuracy = 5.0f;
    float rotSpeed = 2.0f;

    public GameObject wpManager;
    GameObject[] wps;
    GameObject currentNode;
    int currentWP = 0;
    Graphs g;

    // Start é chamado uma vez antes da primeira execução do Update
    void Start()
    {
        wps = wpManager.GetComponent<WPManager>().waypoints;
        g = wpManager.GetComponent<WPManager>().graphs;
        if (g == null)
            currentNode = wps[0]; // Inicia no primeiro waypoint

        Invoke("GoToRuin", 2);
    }

    public void GoToHeli()
    {
        g.AStar(currentNode, wps[0]);
        currentWP = 0;
    }

    public void GoToRuin()
    {
        g.AStar(currentNode, wps[1]);
        currentWP = 0;
    }

    // Update é chamado uma vez por frame
    void LateUpdate()
    {
        if (g.PathList.Count == 0 || currentWP == g.PathList.Count)
            return;

        if (Vector3.Distance(g.PathList[currentWP].getId().transform.position,
            this.transform.position) < accuracy)
        {
            currentNode = g.PathList[currentWP].getId();
            currentWP++;
        }

        if (currentWP < g.PathList.Count)
        {
            goal = g.PathList[currentWP].getId().transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x,
                this.transform.position.y,
                goal.position.z);

            Vector3 direction = lookAtGoal - this.transform.position;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                Quaternion.LookRotation(direction),
                Time.deltaTime * rotSpeed);
            this.transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }
}
