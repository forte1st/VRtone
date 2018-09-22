using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRtone {
    public class Key : MonoBehaviour {

        public Color whiteKeyColor;     //白鍵の色（デフォルト：new Color32(0, 200, 255, 128)）
        public Color blackKeyColor;     //黒鍵の色（デフォルト：new Color32(0, 150, 255, 128)）
        public Color activeKeyColor;    //マレットが触れている時の色（デフォルト：new Color32(255, 255, 0, 128)）
        public float pressDepth;        //鍵盤に触れた時の沈み込み（デフォルト：0.2f）

        public enum KeyColor {
            White, Black
        }

        [System.NonSerialized]
        public float activeLevel = 0;

        private Renderer ren;
        private Vector3 defaultLocalPos;
        private Color inactiveKeyColor;

        //色と位置の初期化
        public void Initialize(KeyColor keyColor, float offset) {
            ren = gameObject.GetComponent<Renderer>();
            if (keyColor == KeyColor.White) {
                inactiveKeyColor = whiteKeyColor;
            } else {
                inactiveKeyColor = blackKeyColor;
            }
            ren.material.color = inactiveKeyColor;
            transform.localPosition += Vector3.forward * offset;
            defaultLocalPos = transform.localPosition;
        }

        void Update() {
            //本体で指定されたactiveLevelに従って色と沈み込みが変化
            transform.localPosition = defaultLocalPos + Vector3.down * activeLevel * transform.localScale.y * pressDepth;
            ren.material.color = Color.Lerp(inactiveKeyColor, activeKeyColor, activeLevel);
        }
    }
}
