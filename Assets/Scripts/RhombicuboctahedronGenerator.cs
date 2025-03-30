#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections.Generic;

[ExecuteAlways]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class RhombicuboctahedronGenerator : MonoBehaviour {
    [Range(0f, 1f)]
    public float t = 0.5f;

    private float _lastT = -1f;

    void OnEnable() {
        UpdateMeshIfNeeded(force: true);
    }

    void Update() {
        UpdateMeshIfNeeded();
    }

#if UNITY_EDITOR
    void OnValidate() {
        if (!Application.isPlaying) {
            EditorApplication.delayCall += () => {
                if (this != null) { // Check if object is still alive
                    UpdateMeshIfNeeded();
                }
            };
        }
    }
#endif

    private void UpdateMeshIfNeeded(bool force = false) {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (force || t != _lastT || meshFilter.sharedMesh == null) {
            _lastT = t;
            GenerateMesh();
        }
    }

    [ContextMenu("Generate Mesh")]
    public void GenerateMesh()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();

        Mesh mesh = new Mesh();
        mesh.name = "Rhombicuboctahedron";

        float sqrt2 = Mathf.Sqrt(2f);
        float extended, other;
        if (t <= 0.5f)
        {
            float u = t * 2f;
            extended = 1f + sqrt2 * u;
            other = 1f;
        }
        else
        {
            float v = (t - 0.5f) * 2f;
            extended = 1f + sqrt2 - v;
            other = 1f - v;
        }

        Vector3[] vertices = new Vector3[24];
        Color[] colors = new Color[24];
        int index = 0;
        for (int sx = -1; sx <= 1; sx += 2)
        {
            for (int sy = -1; sy <= 1; sy += 2)
            {
                for (int sz = -1; sz <= 1; sz += 2)
                {
                    vertices[index] = new Vector3(sx * extended, sy * other, sz * other);
                    colors[index++] = Color.white; // X-extended (bevels)
                    vertices[index] = new Vector3(sx * other, sy * extended, sz * other);
                    colors[index++] = Color.white; // Y-extended (bevels)
                    vertices[index] = new Vector3(sx * other, sy * other, sz * extended);
                    colors[index++] = Color.white; // Z-extended (bevels)
                }
            }
        }

        List<int[]> faceList = new List<int[]>();
        List<Color> faceColors = new List<Color>();
        index = 0;
        for (int sx = -1; sx <= 1; sx += 2)
        {
            for (int sy = -1; sy <= 1; sy += 2)
            {
                for (int sz = -1; sz <= 1; sz += 2)
                {
                    int cornerID = ((sx == 1) ? 1 : 0) * 4 + ((sy == 1) ? 1 : 0) * 2 + ((sz == 1) ? 1 : 0);
                    int baseIndex = cornerID * 3;
                    faceList.Add(new int[] { baseIndex + 0, baseIndex + 1, baseIndex + 2 });
                    faceColors.Add(Color.red); // Corner triangles
                }
            }
        }

        void AddFace(int[] verts, Color color)
        {
            faceList.Add(verts);
            faceColors.Add(color);
        }

        AddFace(new int[] {
            GetVertexIndex(1,  1,  1, 0),
            GetVertexIndex(1, -1,  1, 0),
            GetVertexIndex(1, -1, -1, 0),
            GetVertexIndex(1,  1, -1, 0)
        }, Color.blue);

        AddFace(new int[] {
            GetVertexIndex(-1,  1,  1, 0),
            GetVertexIndex(-1,  1, -1, 0),
            GetVertexIndex(-1, -1, -1, 0),
            GetVertexIndex(-1, -1,  1, 0)
        }, Color.blue);

        AddFace(new int[] {
            GetVertexIndex( 1, 1,  1, 1),
            GetVertexIndex( 1, 1, -1, 1),
            GetVertexIndex(-1, 1, -1, 1),
            GetVertexIndex(-1, 1,  1, 1)
        }, Color.blue);

        AddFace(new int[] {
            GetVertexIndex( 1, -1,  1, 1),
            GetVertexIndex(-1, -1,  1, 1),
            GetVertexIndex(-1, -1, -1, 1),
            GetVertexIndex( 1, -1, -1, 1)
        }, Color.blue);

        AddFace(new int[] {
            GetVertexIndex( 1,  1, 1, 2),
            GetVertexIndex(-1,  1, 1, 2),
            GetVertexIndex(-1, -1, 1, 2),
            GetVertexIndex( 1, -1, 1, 2)
        }, Color.blue);

        AddFace(new int[] {
            GetVertexIndex( 1,  1, -1, 2),
            GetVertexIndex( 1, -1, -1, 2),
            GetVertexIndex(-1, -1, -1, 2),
            GetVertexIndex(-1,  1, -1, 2)
        }, Color.blue);

        void AddBridgeFace(char axisA, int sA, char axisB, int sB)
        {
            char axisC = 'X';
            if ((axisA == 'X' && axisB == 'Y') || (axisA == 'Y' && axisB == 'X')) axisC = 'Z';
            if ((axisA == 'X' && axisB == 'Z') || (axisA == 'Z' && axisB == 'X')) axisC = 'Y';
            if ((axisA == 'Y' && axisB == 'Z') || (axisA == 'Z' && axisB == 'Y')) axisC = 'X';
            int sC1 = 1, sC2 = -1;
            int sx1 = (axisA == 'X') ? sA : (axisB == 'X' ? sB : sC1);
            int sy1 = (axisA == 'Y') ? sA : (axisB == 'Y' ? sB : sC1);
            int sz1 = (axisA == 'Z') ? sA : (axisB == 'Z' ? sB : sC1);
            int sx2 = (axisA == 'X') ? sA : (axisB == 'X' ? sB : sC2);
            int sy2 = (axisA == 'Y') ? sA : (axisB == 'Y' ? sB : sC2);
            int sz2 = (axisA == 'Z') ? sA : (axisB == 'Z' ? sB : sC2);
            int axisAIndex = (axisA == 'X' ? 0 : (axisA == 'Y' ? 1 : 2));
            int axisBIndex = (axisB == 'X' ? 0 : (axisB == 'Y' ? 1 : 2));
            AddFace(new int[] {
                GetVertexIndex(sx1, sy1, sz1, axisAIndex),
                GetVertexIndex(sx2, sy2, sz2, axisAIndex),
                GetVertexIndex(sx2, sy2, sz2, axisBIndex),
                GetVertexIndex(sx1, sy1, sz1, axisBIndex)
            }, Color.white);
        }

        AddBridgeFace('X', 1, 'Y', 1);
        AddBridgeFace('X', 1, 'Y', -1);
        AddBridgeFace('X', -1, 'Y', 1);
        AddBridgeFace('X', -1, 'Y', -1);
        AddBridgeFace('X', 1, 'Z', 1);
        AddBridgeFace('X', 1, 'Z', -1);
        AddBridgeFace('X', -1, 'Z', 1);
        AddBridgeFace('X', -1, 'Z', -1);
        AddBridgeFace('Y', 1, 'Z', 1);
        AddBridgeFace('Y', 1, 'Z', -1);
        AddBridgeFace('Y', -1, 'Z', 1);
        AddBridgeFace('Y', -1, 'Z', -1);

        int GetVertexIndex(int sx, int sy, int sz, int axis)
        {
            int cid = ((sx == 1) ? 1 : 0) * 4 + ((sy == 1) ? 1 : 0) * 2 + ((sz == 1) ? 1 : 0);
            return cid * 3 + axis;
        }

        Vector3[] verts = vertices;
        for (int f = 0; f < faceList.Count; ++f)
        {
            int[] face = faceList[f];
            Vector3 center = Vector3.zero;
            foreach (int vid in face) center += verts[vid];
            center /= face.Length;
            Vector3 v0 = verts[face[0]];
            Vector3 v1 = verts[face[1]];
            Vector3 v2 = verts[face[2]];
            Vector3 normal = Vector3.Cross(v1 - v0, v2 - v0);
            if (Vector3.Dot(normal, center) < 0f)
            {
                System.Array.Reverse(face);
                faceList[f] = face;
            }
        }

        List<Vector3> finalVertices = new List<Vector3>();
        List<Color> finalColors = new List<Color>();
        List<int> triangles = new List<int>();

        for (int i = 0; i < faceList.Count; i++) {
            var face = faceList[i];
            var color = faceColors[i];

            if (face.Length == 3) {
                int startIndex = finalVertices.Count;
                foreach (var idx in face) {
                    finalVertices.Add(vertices[idx]);
                    finalColors.Add(color);
                }
                triangles.AddRange(new[] { startIndex, startIndex + 1, startIndex + 2 });
            }
            else if (face.Length == 4) {
                int startIndex = finalVertices.Count;
                finalVertices.Add(vertices[face[0]]);
                finalVertices.Add(vertices[face[1]]);
                finalVertices.Add(vertices[face[2]]);
                finalVertices.Add(vertices[face[3]]);
                finalColors.AddRange(new[] { color, color, color, color });

                triangles.AddRange(new[] {
                    startIndex, startIndex + 1, startIndex + 2,
                    startIndex, startIndex + 2, startIndex + 3
                });
            }
        }

        mesh.vertices = finalVertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.colors = finalColors.ToArray();
        mesh.RecalculateNormals();
        meshFilter.sharedMesh = mesh;
    }
}
