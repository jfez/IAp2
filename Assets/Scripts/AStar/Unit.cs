﻿using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {


	public Transform target;
	public float speed = 2.5f;
	Vector3[] path;
	int targetIndex;

	void Start() {
        //Invoke("Pathing", 0.25f);
    }

    public void Pathing(Transform startPoint, Transform endPoint)
    {
        //PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
		PathRequestManager.RequestPath(startPoint.position, endPoint.position, OnPathFound);
    }

	private void Update(){
		
	}

	public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
		if (pathSuccessful) {
			path = newPath;
			targetIndex = 0;
			StopCoroutine("FollowPath");
			if (this.gameObject.activeInHierarchy){
				StartCoroutine("FollowPath");
			}
		}
	}

	IEnumerator FollowPath() {
		Vector3 currentWaypoint = path[0];

		while (true) {
			if (transform.position == currentWaypoint) {
				targetIndex ++;
				if (targetIndex >= path.Length) {
					yield break;
				}
				currentWaypoint = path[targetIndex];
			}

			transform.position = Vector3.MoveTowards(transform.position,currentWaypoint,speed * Time.deltaTime);
			yield return null;
		}
	}

	public void OnDrawGizmos() {
		if (path != null) {
			for (int i = targetIndex; i < path.Length; i ++) {
				Gizmos.color = Color.black;
				Gizmos.DrawCube(path[i], Vector3.one);

				if (i == targetIndex) {
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else {
					Gizmos.DrawLine(path[i-1],path[i]);
				}
			}
		}
	}
}