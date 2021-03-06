using UnityEngine;

namespace utils {
    public class TargetFinder {
        public static GameObject FindTarget(Transform me, float range) {
            if (!me) {
                return null;
            }

            var ctl = Object.FindObjectOfType<PlayerControl>();
            var player = ctl ? ctl.gameObject : null;
            var loot = GetClosestEnemy(me, GameObject.FindGameObjectsWithTag("DroppedLoot"), range);
            if (loot) {
                player = loot;
            }

            return player;
        }

        static GameObject GetClosestEnemy(Transform transform, GameObject[] enemies, float range)
        {
            GameObject tMin = null;
            float minDist = Mathf.Infinity;
            Vector3 currentPos = transform.position;
            foreach (GameObject t in enemies)
            {
                float dist = Vector3.Distance(t.transform.position, currentPos);
                if (dist < range && dist < minDist)
                {
                    tMin = t;
                    minDist = dist;
                }
            }
            return tMin;
        }
    }
}
