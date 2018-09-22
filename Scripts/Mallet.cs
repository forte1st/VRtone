using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRtone {
    public class Mallet : MonoBehaviour {
        private Board board;                //本体（触れていない場合nullになる）
        private AudioSource audioSource;
        private float assistOffset = 0;

        private readonly float frequencyRatio = Mathf.Pow(2, 1.0f / 12.0f);     //隣り合う音の周波数比

        void Start() {
            audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.volume = 0;
        }

        private void OnTriggerEnter(Collider other) {
            //Boardスクリプトを持つColliderに触れたら本体として認識
            Board otherScript = other.GetComponent<Board>();
            if (otherScript) {
                board = otherScript;
            }
        }

        private void OnTriggerExit(Collider other) {
            //本体から離れたらboardをnullにする
            if (board && other.gameObject == board.gameObject) {
                board = null;
            }
        }

        void Update() {
            //本体に触れているなら音を鳴らす
            if (board) {
                Vector3 localPos = board.transform.InverseTransformPoint(transform.position);

                //アシスト機能（押しているキーの中央に向かって一定速度でピッチを補正）
                Vector3 assistedLocalPos = localPos + Vector3.right * assistOffset;
                float assistTarget = Mathf.Round(localPos.x / board.keyWidth) * board.keyWidth;
                float assistDiff = board.keyWidth * board.assistLevel * Time.deltaTime;

                if (assistDiff < Mathf.Abs(assistTarget - assistedLocalPos.x)) {
                    assistOffset += Mathf.Sign(assistTarget - assistedLocalPos.x) * assistDiff;
                } else {
                    assistOffset = assistTarget - localPos.x;
                }

                //ピッチと音量を決定
                audioSource.pitch = Mathf.Pow(frequencyRatio, assistedLocalPos.x / board.keyWidth + board.centerKey);
                audioSource.volume = Mathf.Clamp01(-localPos.y / board.keyDepth);
            } else {
                //本体の当たり判定から外れている場合、音量を0にしてアシスト機能をリセット
                audioSource.volume = 0;
                assistOffset = 0;
            }
        }
    }
}
