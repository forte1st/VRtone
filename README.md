# VRtone

## 概要
Oculus Touch等を用いてVR空間で演奏できる楽器です。見た目は鍵盤ですがビブラートやポルタメントが可能です。Unity2018.2.20f1で動作確認

## 利用規約
「大田マト」のクレジット表記（チャンネルまたは動画またはtwitterアカウントへのリンクもあると嬉しいです）をしていただければ、
商用・非商用に関わらず動画等に利用していただいて構いません。  
ただし、アセット自体の再配布はご遠慮ください。  
また、本アセットは無保証であり、利用によって生じた損害等について当方は責任を負わないものとします。

大田マトチャンネル: https://www.youtube.com/channel/UCY-rzz8F3xdJ9NS3Y5iAGjw  
大田マトtwitter: https://twitter.com/ootamato

## 利用方法
1. [こちら](https://github.com/forte1st/VRtone/releases)からVRtone.unitypackageをダウンロード  

2. ダウンロードしたunitypackageをインポート  

3. ScenesフォルダのSample.unityを開く  
Mallet（バチ）とVRtone（本体）が配置されたシーンです。この時点ではバチは動きません。また、鍵盤は実行時に生成されるので再生するまで表示されません。 
  
4. Malletを手に連動させる  
Oculus Utilities for Unityを使用する場合、OVRCameraRigをシーンに配置して2本のMalletをそれぞれLeftHandAnchorとRightHandAnchorの子にしてください。Malletの位置と角度はいい感じに調整してください。  

本体のインスペクタでキーの数、中央のキーのピッチ、キーの大きさ、黒鍵の位置、アシスト機能の強さをカスタマイズできます。また、Malletの中にあるSphereのAudio Sourceの音源を差し替えることで音色を変更できます。PrefabsフォルダのKeyのインスペクタでキーの色とキーの沈み込み量を変更できます。  

利用方法に関してご不明な点がございましたらお気軽にtwitterでご連絡ください。
