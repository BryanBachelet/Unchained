using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

namespace GK {
	public class Rocket : MonoBehaviour {

        public CameraShake myCS;
		public float ExplosionRadius = 0.5f;
		public float ExplosionForce = 10.0f;

		Rigidbody rb;

        RotationPlayer myRotPlay;
		void Start() {
            if (myRotPlay == null) myRotPlay = gameObject.GetComponent<RotationPlayer>();

        }

		void FixedUpdate() {
		}

		void OnCollisionEnter(Collision collision) {
			var fracture = collision.gameObject.GetComponent<Fracture>();

			if (fracture != null && fracture.InitialMesh == true) {
				var world = collision.contacts[0].point;
				var local = fracture.transform.InverseTransformPoint(world);

				Profiler.BeginSample("Do fracture call");

				//fracture.DoFracture(local);
				fracture.DoFracture(myRotPlay.newDir * ExplosionForce);

				Profiler.EndSample();

                myCS.shakeDuration = 1;
				StartCoroutine(Stupid(world));
			}
		}

		IEnumerator Stupid(Vector3 worldPoint) {
			yield return null;
			yield return null;
			yield return null;

			foreach (var coll in Physics.OverlapSphere(worldPoint, ExplosionRadius)) {
				var otherRb = coll.GetComponent<Rigidbody>();

				if (otherRb != null) {
					otherRb.AddExplosionForce(ExplosionForce, worldPoint, ExplosionRadius);
				}
			}

		}
	}
}
