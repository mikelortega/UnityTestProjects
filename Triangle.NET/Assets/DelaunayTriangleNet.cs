using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class DelaunayTriangleNet : MonoBehaviour
{

    void Update()
    {
        var igeom = new TriangleNet.Geometry.InputGeometry(transform.childCount);
        List<int> BoundIndexes = new List<int>();
        List<int> HoleIndexes = new List<int>();

        for (int i = 0; i < transform.childCount; ++i)
        {
            igeom.AddPoint(transform.GetChild(i).position.x, transform.GetChild(i).position.z);

            if (transform.GetChild(i).name == "Bound")
                BoundIndexes.Add(i);

            if (transform.GetChild(i).name == "Hole")
                HoleIndexes.Add(i);
        }

        if (BoundIndexes.Count > 1)
        {
            for (int i=1; i < BoundIndexes.Count; ++i)
                igeom.AddSegment(BoundIndexes[i - 1], BoundIndexes[i]);
            igeom.AddSegment(BoundIndexes[BoundIndexes.Count - 1], BoundIndexes[0]);
        }

        if (HoleIndexes.Count > 1)
        {
            for (int i = 1; i < HoleIndexes.Count; ++i)
                igeom.AddSegment(HoleIndexes[i - 1], HoleIndexes[i]);
            igeom.AddSegment(HoleIndexes[HoleIndexes.Count - 1], HoleIndexes[0]);
            igeom.AddHole(0.0f, 0.0f);
        }

        TriangleNet.Mesh mesh = new TriangleNet.Mesh();
        mesh.Triangulate(igeom);

        float y = 0.0f;
        foreach (TriangleNet.Data.Triangle t in mesh.Triangles)
        {
            Debug.DrawLine(new Vector3((float)t.vertices[0].x, y, (float)t.vertices[0].y), new Vector3((float)t.vertices[1].x, y, (float)t.vertices[1].y));
            Debug.DrawLine(new Vector3((float)t.vertices[1].x, y, (float)t.vertices[1].y), new Vector3((float)t.vertices[2].x, y, (float)t.vertices[2].y));
            Debug.DrawLine(new Vector3((float)t.vertices[2].x, y, (float)t.vertices[2].y), new Vector3((float)t.vertices[0].x, y, (float)t.vertices[0].y));
            y += 0.5f;
        }

    }

}
