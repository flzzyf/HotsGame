//六边形
using UnityEngine;
using System.Collections.Generic;

public class Hex {
    //外部和内部半径
    public const float outerRadius = .5f;
    public const float innerRadius = outerRadius * .87f;

    //顶点
    public static Vector2[] points = {
        new Vector2(0, outerRadius),
        new Vector2(innerRadius, .5f * outerRadius),
        new Vector2(innerRadius, -.5f * outerRadius),
        new Vector2(0, -outerRadius),
        new Vector2(-innerRadius, -.5f * outerRadius),
        new Vector2(-innerRadius, .5f * outerRadius)
    };

    #region 获取Mesh

    static Mesh hexMesh;
    public static Mesh HexMesh {
        get {
            if(hexMesh == null) {
                hexMesh = new Mesh();
                vertices = new List<Vector3>();
                triangles = new List<int>();

                Vector2 center = Vector2.zero;
                for (int i = 0; i < 6; i++) {
                    int index = i == 5 ? 0 : i + 1;
                    AddTriangle(center, center + Hex.points[i], center + Hex.points[index]);
                }

                hexMesh.vertices = vertices.ToArray();
                hexMesh.triangles = triangles.ToArray();
                hexMesh.RecalculateNormals();
            }

            return hexMesh;
        }
    }

    static List<Vector3> vertices;
    static List<int> triangles;

    static void AddTriangle(Vector2 v1, Vector2 v2, Vector2 v3) {
        int vertexIndex = vertices.Count;
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);
        triangles.Add(vertexIndex);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);
    }

    #endregion
}
