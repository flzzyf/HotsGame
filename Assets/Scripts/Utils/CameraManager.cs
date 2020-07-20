using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour{
	//镜头跟随目标
	public Transform followedTarget;
	//偏移
	public Vector3 followOffset;
	//跟随平滑度
	public float followSmooth = .1f;

	Camera cam;

	private void Awake() {
		cam = Camera.main;
	}

	private void Start() {
		//瞬间移动镜头到目标位置
		MoveToTarget(true);
	}

	private void FixedUpdate() {
		MoveToTarget();
	}

	/// <summary>
	/// 移动镜头到目标位置
	/// </summary>
	/// <param name="instant">是否瞬间移动</param>
	void MoveToTarget(bool instant = false) {
		if (followedTarget != null) {
			Vector3 targetPos = followedTarget.position;
			targetPos += followOffset;

			if (instant) {
				cam.transform.position = targetPos;
			} else {
				cam.transform.position = Vector3.Lerp(cam.transform.position, targetPos, followSmooth);
			}
		}
	}

}
