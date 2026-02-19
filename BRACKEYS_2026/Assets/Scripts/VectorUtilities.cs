using System.Collections.Generic;
using UnityEngine;

public static class VectorUtilities {
    public static GameObject GetClosestGameObject(Vector2 targetPoint, List<GameObject> objects) {
        if (objects == null || objects.Count == 0)
            return null;

        GameObject closestObject = null;
        float closestDistanceSqr = Mathf.Infinity;

        foreach (GameObject obj in objects) {
            if (obj == null) continue;

            Vector2 objPosition = obj.transform.position;
            float distanceSqr = (objPosition - targetPoint).sqrMagnitude;

            if (distanceSqr < closestDistanceSqr) {
                closestDistanceSqr = distanceSqr;
                closestObject = obj;
            }
        }

        return closestObject; 
    }
}

