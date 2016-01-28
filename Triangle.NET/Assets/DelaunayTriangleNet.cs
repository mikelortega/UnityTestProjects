using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class DelaunayTriangleNet : MonoBehaviour
{

    void Update()
    {
        var igeom = new TriangleNet.Geometry.InputGeometry(transform.childCount);
        List<int> SegmentIndexes = new List<int>();

        for (int i = 0; i < transform.childCount; ++i)
        {
            igeom.AddPoint(transform.GetChild(i).position.x, transform.GetChild(i).position.z);

            if (transform.GetChild(i).GetComponent<MeshRenderer>().sharedMaterial.name == "SegmentMaterial")
                SegmentIndexes.Add(i);

            if (transform.GetChild(i).GetComponent<MeshRenderer>().sharedMaterial.name == "HoleMaterial")
                igeom.AddHole(transform.GetChild(i).position.x, transform.GetChild(i).position.z);
        }

        if (SegmentIndexes.Count > 1)
        {
            for (int i = 1; i < SegmentIndexes.Count; ++i)
                igeom.AddSegment(SegmentIndexes[i - 1], SegmentIndexes[i]);
            igeom.AddSegment(SegmentIndexes[SegmentIndexes.Count - 1], SegmentIndexes[0]);
        }

        TriangleNet.Mesh mesh = new TriangleNet.Mesh();
        mesh.Triangulate(igeom);

        foreach (TriangleNet.Data.Triangle t in mesh.Triangles)
        {
            Debug.DrawLine(transform.GetChild(t.vertices[0].ID).position, transform.GetChild(t.vertices[1].ID).position);
            Debug.DrawLine(transform.GetChild(t.vertices[1].ID).position, transform.GetChild(t.vertices[2].ID).position);
            Debug.DrawLine(transform.GetChild(t.vertices[2].ID).position, transform.GetChild(t.vertices[0].ID).position);
        }

    }

}
