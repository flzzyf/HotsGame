using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour {
    public Vector2Int mapSize = new Vector2Int(10, 10);
    public Vector2 nodeSize = Vector2.one;

    public HexNode nodePrefab;

    Vector2 startingPos;

    private void Start() {
        GenerateMap();
    }

    void GenerateMap() {
        startingPos = new Vector2(-mapSize.x / 2 * Hex.innerRadius * 2, -mapSize.y / 2 * Hex.outerRadius * 1.5f);
        for (int y = 0; y < mapSize.y; y++) {
            for (int x = 0; x < mapSize.x; x++) {
                CreateNode(x, y);
            }
        }
    }

    void CreateNode(int x, int y) {
        Vector2 pos = new Vector2((x + y * .5f - y / 2) * Hex.innerRadius * 2, y * Hex.outerRadius * 1.5f);
        pos += startingPos;
        HexNode node = Instantiate(nodePrefab, pos, Quaternion.identity);
        node.transform.SetParent(transform);
    }

    private void OnDrawGizmosSelected() {
        for (int y = 0; y < mapSize.y; y++) {
            for (int x = 0; x < mapSize.x; x++) {
                Vector2 pos = new Vector2((x + y * .5f - y / 2) * Hex.innerRadius * 2, y * Hex.outerRadius * 1.5f);

                Mesh mesh = Hex.HexMesh;
                Gizmos.DrawWireMesh (mesh, pos);
            }
        }
    }
}
