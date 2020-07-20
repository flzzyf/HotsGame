using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HexNode : MonoBehaviour {
    Mesh mesh;

    public MeshFilter meshFilter;
    public MeshFilter borderMeshFilter;

    MeshRenderer meshRenderer;
    Color defaultColor;
    public Color color_Highlighted;

    void Awake() {
        mesh = Hex.HexMesh;
        meshFilter.mesh = mesh;
        borderMeshFilter.mesh = mesh;

        GetComponent<MeshCollider>().sharedMesh = mesh;
        meshRenderer = meshFilter.GetComponent<MeshRenderer>();
        defaultColor = meshRenderer.material.GetColor("_Color");
    }

    private void OnMouseEnter() {
        SetColor(color_Highlighted);
    }

    private void OnMouseExit() {
        SetColor(defaultColor);
    }

    void SetColor(Color color) {
        meshRenderer.material.SetColor("_Color", color);
    }
}
