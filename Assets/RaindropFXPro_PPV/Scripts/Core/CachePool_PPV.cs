using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RaindropFX;

namespace RaindropFX {
    public class CachePool_PPV : MonoBehaviour {

        public int counter = 0;

        List<Raindrop_PPV> raindrops = new List<Raindrop_PPV>();

        public void Init() {
            counter = 0;
            raindrops.Clear();
        }

        public void Recycle(Raindrop_PPV raindrop) {
            raindrops.Add(raindrop);
            counter = raindrops.Count;
        }

        public Raindrop_PPV GetRaindrop() {
            if (counter > 0) {
                Raindrop_PPV temp = raindrops[0];
                raindrops.RemoveAt(0);
                counter = raindrops.Count;
                return temp;
            } else return new Raindrop_PPV();
        }

    }
}
