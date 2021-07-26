using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "DefaultEnemy", menuName = "Data/Enemy info", order = 0)]
    public class EnemyInfoSO : ScriptableObject
    {
        [Min(0.0f)]
        public float attackCooldownTime = 0.5f;
        [Min(0.0f)]
        public float attackCooldownTimeMultiplier = 1.0f;
        [Min(0.0f)]
        public float attackMinDistance = 3.0f;
        [Min(0.0f)]
        public float attackMaxDistance = 3.0f;
        [Min(0.0f)]
        public float attackMaxDistanceMultiplier = 1.0f;
        [Min(0.0f)]
        public float sightDistance = 10.0f;
        [HideInInspector] public float sightDistanceFollow;
        [Min(0.0f)]
        public float SightDistanceForWall = 2.0f;
        [Min(0.0f)]
        public float SightDistanceForPlayerTooNear = 0.4f;
        [Min(0.0f)]
        public float overlapCircleRadius;
        public Vector3 overlapCircleOffset;
        [Min(0.0f)]
        public float collisionDamageCooldownTime = 1.0f; // Time in which box collider of enemy is disabled
        [Min(0.0f)]
        public float jumpForce = 40.0f;
        public Vector3 wallDetectionBoxSize;
        public Vector3 wallDetectionBoxOffset;
        [Min(0.0f)]
        public float moveSpeed = 10.0f;
        [Min(0.0f)]
        public float hp = 10;

        public Vector3 obstacleDetectorPosition;
        [Min(0.0f)]
        public float obstacleDetectorDistance;

        public List<string> ignoredObstacleTags;
    }
}