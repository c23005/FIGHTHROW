# FIGHTHROWプロジェクト

## ファイル構成

* [Unityデータ](https://github.com/c23005/FIGHTHROW/tree/main/ProjectData/Team2)
* [Buildデータ](https://github.com/c23005/FIGHTHROW/tree/main/BuildData)

## ジャンル

対戦アクションゲーム

## プラットフォーム

* [Windousビルドデータ](https://github.com/c23005/FIGHTHROW/tree/main/BuildData/20240627_FIGHTHROW)
※ プレイステーションのコントローラーを2つ使用して遊ぶゲームです

## 担当箇所

プログラム全般

## こだわった箇所

* [画面外のプレイヤーを追跡するカーソル](https://github.com/c23005/FIGHTHROW/blob/main/ProjectData/Team2/Assets/Scripts/InOutScript.cs)

* <dt>ステージギミック</dt>

   * [ステージ1](https://github.com/c23005/FIGHTHROW/blob/main/ProjectData/Team2/Assets/Scripts/Gimmiks/ForestStageGimmickScript.cs)

   * [ステージ2](https://github.com/c23005/FIGHTHROW/blob/main/ProjectData/Team2/Assets/Scripts/Gimmiks/IceStageGimmickScript.cs)

   * [ステージ3](https://github.com/c23005/FIGHTHROW/blob/main/ProjectData/Team2/Assets/Scripts/Gimmiks/VolcanoStageGimmickScript.cs)

* [アイテムの種類の区別とダメージ判定](https://github.com/c23005/FIGHTHROW/blob/main/ProjectData/Team2/Assets/Scripts/Items/ItemDamageScript.cs)

## ゲームルール

×でジャンプ、□で攻撃、△でガード、〇でアイテムを投げることができ、アイテムはジャストガード(ガードをし始めて0.25秒までの判定)をすることでアイテムを取ることができる。
戦闘開始から20秒ほど経過するとステージギミックがランダムに起き、攻撃力が上がったり、スピードが遅くなったりなど、さまざまなギミックがある。
また、ステージそれぞれに固有のギミックがある。
対戦相手の体力を0にしたら勝ちになり、自分の体力が0になると負けになる。
アイテムを駆使して相手の体力を0にしよう!

## Unityバージョン

Unity 2022.3.7f1

## 製作期間

2か月

## メンバー

* 兼島一穂(プログラマ)
* 大城侑(プランナー)
* 山城伍輝(アニメーター)

## ゲームスクリーンショット

<img src="https://github.com/c23005/FIGHTHROW/blob/main/ScreenShot/Title.png" width="600px">
<img src="https://github.com/c23005/FIGHTHROW/blob/main/ScreenShot/StageSelectScene.png" width="600px">
<img src="https://github.com/c23005/FIGHTHROW/blob/main/ScreenShot/Battle.png" width="600px">
<img src="https://github.com/c23005/FIGHTHROW/blob/main/ScreenShot/UIMiniMap.png" width="600px">
<p>
<img src="https://github.com/c23005/FIGHTHROW/blob/main/ScreenShot/1pWin.png" width="300px">
<img src="https://github.com/c23005/FIGHTHROW/blob/main/ScreenShot/2pWin.png" width="300px">
</p>