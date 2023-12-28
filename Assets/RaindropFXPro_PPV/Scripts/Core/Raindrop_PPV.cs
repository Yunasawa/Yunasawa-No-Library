using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RaindropFX;

namespace RaindropFX {
    public class Raindrop_PPV {

        #region public parameters
        public bool isStatic;
        public Vector2 position;
        public Vector2 realSize;
        public Vector2 velocity;
        public Vector2 acceleration;
        public Vector2 externalForce;
        public float size;
        public float weight;
        public int lifeTime;
        public float sFriction = 0.8f, fFriction = 0.5f;
        public Vector2Int raindropTexSize;
        #endregion

        public Raindrop_PPV() { }

        public Raindrop_PPV(bool isStatic, Vector2 initPosition, float size, int lifeTime) {
            this.lifeTime = lifeTime;
            this.isStatic = isStatic;
            this.size = size;
            position = initPosition;
            externalForce = Vector2.zero;
            acceleration = Vector2.zero;
            velocity = Vector2.zero;
            UpdateWeight();
        }

        public void UpdateProp(Vector2Int raindropTexSize, float sFriction) {
            this.raindropTexSize = raindropTexSize;
            this.sFriction = sFriction;
        }

        public void LoseWeight(float percent) {
            size -= size * percent;
            UpdateWeight();
        }

        public void Reuse(bool isStatic, Vector2 initPosition, float size, int lifeTime, Vector2Int raindropTexSize, float sFriction) {
            this.lifeTime = lifeTime;
            this.isStatic = isStatic;
            this.size = size;
            position = initPosition;
            externalForce = Vector2.zero;
            acceleration = Vector2.zero;
            velocity = Vector2.zero;
            UpdateProp(raindropTexSize, sFriction);
            UpdateWeight();
        }

        public void UpdateWeight() {
            realSize = new Vector2((int)(size * raindropTexSize.x), (int)(size * raindropTexSize.y));
            weight = size;
        }

        public void updateForce(Vector2 externalForce) {
            this.externalForce = externalForce;
            acceleration = this.externalForce / weight;
        }

        public void CalcNewPos(float timeStep) {
            Vector2 deltaPos = (velocity + 0.5f * acceleration * timeStep) * timeStep;
            position += deltaPos;
        }

        public void updateVelocity(float timeStep) {
            velocity += acceleration * timeStep;
        }

        public void ApplyFriction() {
            if (sFriction >= externalForce.magnitude) velocity = acceleration = Vector2.zero;
            else acceleration -= acceleration.normalized * fFriction / weight;
        }

        public void ApplyRandomForce(float maxRandomDynamicForce) {
            float x = Random.Range(-maxRandomDynamicForce, maxRandomDynamicForce);
            velocity.x += x;
            float y = Random.Range(-maxRandomDynamicForce, maxRandomDynamicForce);
            velocity.y += y;
        }

    }
}
