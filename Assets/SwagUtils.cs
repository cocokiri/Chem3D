using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Swag
{
public class SwagUtils : MonoBehaviour {
        public LineRenderer lRender;

        public void CreateLine(Transform a, Transform b)
        {
            LineRenderer line = GameObject.Instantiate(lRender as LineRenderer);
            line.transform.parent = a;
            int segments = 30;
            line.positionCount = segments;
            // line.useWorldSpace = true;
            Vector3 deltaVec = b.position - a.position;
            Vector3 step = deltaVec / segments;

            for (int i = 1; i < segments; i++)
            {
                line.SetPosition(i, a.position + (step * i));
            }
        }
}
}

