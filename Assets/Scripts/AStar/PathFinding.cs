using UnityEngine;
using System.Collections;
using System.IO;
using System;
using UnityEngine.UI;

public class PathFinding : MonoBehaviour {

    public GameObject en;
    public GameObject map;
    public Sprite sprite1, sprite2, sprite3;

	// Use this for initialization
	void Start () {
	    int[,] map = new int[6, 6]{
                {0, 1, 0, 2, 0, 0},
                {0, 1, 0, 2, 0, 0},
                {0, 1, 0, 2, 0, 0},
                {0, 1, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0}
        };

        var graph = new Graph(map);
        var search = new Search(graph);
        search.Start(graph.nodes[0], graph.nodes[4]);

        Debug.Log(graph.nodes[3].label);
        while (!search.finished) {
            search.Step();
        }
        print("Done" + search.path.Count + "    " + search.interations);
        ResetMap(graph);

        foreach (var node in search.path) {
            GetImage(node.label).color = Color.red;
        }
	}

    Image GetImage(string label) {
        var id = Int32.Parse(label);
        var go = map.transform.GetChild(id).gameObject;
        return go.GetComponent<Image>();
    }

    void ResetMap(Graph graph) {

        foreach (var node in graph.nodes) {
            if (node.label == "1") {
                GetImage(node.label).sprite = sprite1;
            }
            if (node.label == "2")
            {
                GetImage(node.label).sprite = sprite2;
            }
            if (node.label == "3")
            {
                GetImage(node.label).sprite = sprite3;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
