using UnityEngine;
using System.Collections;

public class WorldWidth : Singleton<WorldWidth> {

    protected WorldWidth() { }
    private LineSegment _lineSegment;
    public LineSegment lineSegment {
        get {
            if (_lineSegment == null) {
                _lineSegment = GetComponent<LineSegment>();
            }
            return _lineSegment;
        }
    }
    protected float _width = -1;
    public float width {
        get {
            if (_width < 0f) {
                _width = lineSegment.distance.magnitude;
            }
            return _width;
        }
    }

    public float normalizedXPosition01(float xPos) {
        return (Mathf.Clamp(xPos, lineSegment.start.position.x, lineSegment.end.position.x) - lineSegment.start.position.x) / width;
    }
}
