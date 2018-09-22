using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRtone {
    public class Board : MonoBehaviour {

        public GameObject key;

        public int keyCount;            //生成する鍵盤の数（デフォルト：50）
        public int centerKey;           //ラ（440Hz）を0とした時の中央の鍵盤の音（デフォルト：0）
        public float keyWidth;          //鍵盤の幅（デフォルト：0.045f）
        public float keyDepth;          //鍵盤の深さ（デフォルト：0.05f）
        public float keyLength;         //鍵盤の長さ（デフォルト：0.25f）
        public float blackKeyOffset;    //黒鍵と白鍵の位置の差（デフォルト：0.125f）
        public float assistLevel;       //アシスト機能の強さ（keyWidth×assitLevel[m/s]の速さでピッチが補正されます。デフォルト：0）

        private readonly float keyAppearanceWidthRatio = 0.9f;                      //鍵盤の実際の幅に対する見た目の幅の比率
        private readonly bool[] blackKeys = { false, true, false, false, true, false, true, false, false, true, false, true };  //ラを基準とした黒鍵の位置
        private GameObject[] keys;
        private Key[] keyScripts;
        private List<GameObject> mallets = new List<GameObject>();

        void Start() {
            int leftEnd = -keyCount / 2;
            keys = new GameObject[keyCount];
            keyScripts = new Key[keyCount];

            for (int i = 0; i < keyCount; i++) {
                //鍵盤を生成
                keys[i] = Instantiate(key, transform) as GameObject;
                keyScripts[i] = keys[i].GetComponent<Key>();

                Vector3 keyPos = Vector3.right * (leftEnd + i) * keyWidth + Vector3.down * keyDepth / 2;

                keys[i].transform.localScale = new Vector3(keyWidth * keyAppearanceWidthRatio, keyDepth, keyLength);
                keys[i].transform.position = transform.TransformPoint(keyPos);

                //黒鍵と白鍵で位置と色を変える
                if (blackKeys[Mod(centerKey + leftEnd + i, blackKeys.Length)]) {
                    keys[i].GetComponent<Key>().Initialize(Key.KeyColor.Black, blackKeyOffset);
                } else {
                    keys[i].GetComponent<Key>().Initialize(Key.KeyColor.White, 0);
                }
            }

            //当たり判定の位置と大きさ
            gameObject.GetComponent<BoxCollider>().center = new Vector3(0, -0.5f, blackKeyOffset / 2);
            gameObject.GetComponent<BoxCollider>().size = new Vector3(keyWidth * keyCount, 1, keyLength + blackKeyOffset);
        }

        private void OnTriggerEnter(Collider other) {
            if (other.tag == "Mallet") {
                mallets.Add(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.tag == "Mallet") {
                mallets.Remove(other.gameObject);
            }
        }

        void Update() {
            //各キーのactiveLevelを設定
            foreach (Key keyScript in keyScripts) {
                keyScript.activeLevel = 0;
                foreach (GameObject mallet in mallets) {
                    Vector3 malletPos = keyScript.transform.InverseTransformPoint(mallet.transform.position);
                    keyScript.activeLevel += 1 - Mathf.Clamp01(Mathf.Abs(malletPos.x));
                }
            }
        }

        //正剰余
        int Mod(int a, int b) {
            return a - b * Mathf.FloorToInt((float)a / (float)b);
        }
    }
}
