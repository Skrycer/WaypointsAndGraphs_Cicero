using UnityEngine;

[System.Serializable]
public struct Link
{
    public enum direction { UNI, BI }
    public GameObject node1;
    public GameObject node2;
    public direction dir;
}

public class WPManager : MonoBehaviour
{
    public GameObject[] waypoints;
    public Link[] links;
    public Graphs graphs; // Adiciona a inst�ncia de Graphs

    // Start � chamado uma vez antes da primeira execu��o do Update
    void Start()
    {
        graphs = new Graphs();
        foreach (var wp in waypoints)
        {
            graphs.AddNode(wp); // Adiciona cada waypoint como um n�
        }

        foreach (var link in links)
        {
            graphs.AddEdge(link.node1, link.node2); // Adiciona arestas entre waypoints conforme o array de links
            if (link.dir == Link.direction.BI)
            {
                graphs.AddEdge(link.node2, link.node1); // Adiciona tamb�m a conex�o oposta se for bidirecional
            }
        }
    }
}
