using System;
using UnityEngine;

namespace TOZ.ImageEffects {

	[ExecuteInEditMode]
	[AddComponentMenu("Image Effects/TOZ/Wiggle")]
	public sealed class PP_Wiggle : PostProcessBase {
		//Variables//
		public float speed = 10.0f;
		public float amplitude = 0.05f;

		//Mono Methods//
		void Awake() {
			base.shader = Shader.Find("Hidden/TOZ/ImageFX/Wiggle");
		}

		void Start() {
			ApplyVariables();
		}

		void OnRenderImage(RenderTexture src, RenderTexture dest) {
			ApplyVariables();
			Graphics.Blit(src, dest, base.material);
		}

		void ApplyVariables() {
			base.material.SetFloat("_Speed", speed);
			base.material.SetFloat("_Amplitude", amplitude);
		}
	}

}