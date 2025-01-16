[![EN](https://img.shields.io/badge/Language-English-blue)](https://github.com/dev-SLH/GeppakuLabRandomSystem/blob/main/Readme%20En.md) [![KR](https://img.shields.io/badge/Language-Korean-red)](https://github.com/dev-SLH/GeppakuLabRandomSystem/blob/main/Readme%20Kr.md)

# GeppakuLab Random System プロジェクト

🌙 **GeppakuLab Random System** は、バーチャルユーチューバー **月白 累 (Geppaku Lui)** のために特別に開発された乱数生成プログラムです。

## 🎯 特徴
- **乱数生成:** 範囲と数をカスタマイズ可能な乱数生成。
- **ソートオプション:** 昇順または降順で結果を並び替え可能。
- **重複制御:** 結果の重複を許可または禁止。
- **最小解像度ロック:** 定義された最小解像度以下にウィンドウをリサイズすることを防止。

---

## 📽️ デモ動画

### 🎥 **乱数生成プログラムの紹介**
[![GeppakuLab ランダム·システム Showcase](https://img.youtube.com/vi/j1sl5BKaaOg/0.jpg)](https://youtu.be/j1sl5BKaaOg)

### 🎥 **設定とオプションの紹介**
[![Settings Showcase](https://img.youtube.com/vi/RKB7lpRkYCU/0.jpg)](https://youtu.be/RKB7lpRkYCU)

---

## 📦 インストール方法
1. 最新バージョンを [Releases](https://github.com/dev-SLH/GeppakuLabRandomSystem/releases) セクションからダウンロードしてください。
2. 提供された `GeppakuLab_RandomSystem_Installer.exe` ファイルを実行してください。
3. 画面の指示に従ってインストールを完了してください。

---

## 🛠️ システム要件
- **オペレーティングシステム:** Windows 10 以上
- **ランタイム:** .NET Framework 4.7.2 以上

---

## 📖 使用方法

1. **範囲設定:** 乱数の最小値と最大値を設定します。
2. **数設定:** 生成する乱数の数を選択します。
3. **生成:** `生成` ボタンをクリックして乱数を生成します。
4. **ソートと再ソート:** ドロップダウンメニューで並び替えオプション（昇順または降順）を選択します。
5. **結果のコピー:** 生成された乱数をクリップボードにコピーします。

---

## 🛡️ ライセンス
このプロジェクトは **MIT ライセンス** の下で公開されています。詳細については `LICENSE` ファイルをご覧ください。

---

## 💌 クレジット
- **開発者:** [설령화(雪霊花)-SLH](https://x.com/slh3951)
- **Designed for:** [Geppaku Lui (月白 累)](https://www.youtube.com/@Geppaku_Lui)

---

## 🚀 Unity プロジェクトの始め方

### 1. 必要なアセットのインポート
1. [Modern UI Pack](https://assetstore.unity.com/packages/tools/gui/modern-ui-pack-201717) を Unity Asset Store から購入してダウンロードしてください。
2. Unity プロジェクトに Modern UI Pack をインポートしてください。

### 2. プリセットの適用
1. Unity エディタの上部メニューから **Tools > Modern UI Pack > Apply UIManager Preset** をクリックしてください。
2. **プリセットが自動適用されない場合:**
   - `Assets/Modern UI Pack/Resources/` ディレクトリに移動し、`MUIP Manager.asset` を選択。
   - **Inspector** にプリセットファイルをドラッグ＆ドロップしてください。

### 3. サンプルシーンを実行
1. 提供されたサンプルシーン (`Assets/Scenes/GeppakuLabRandomSystem.unity`) を開いてください。
2. Unity エディタで **Play** ボタンを押してプロジェクトを実行してください。

---

## ⚠️ Unity プロジェクト使用時の注意

### 📌 必須アセットの確認
- 必要なすべてのパッケージとアセットがインポートされていることを確認してください。
- 必須アセット: [Modern UI Pack](https://assetstore.unity.com/packages/tools/gui/modern-ui-pack-201717)（購入が必要）。

### 📌 Unity バージョン互換性
- 推奨 Unity バージョン: **6000.0.32f1 以上**。
- それより古いバージョンでは一部の機能が正しく動作しない可能性があります。

### 📌 シーン設定の注意
- 提供されたサンプルシーンをそのまま使用してください。
- ⚠️ **注意:** シーン設定を変更すると、予期しない動作が発生する可能性があります。

### 📌 プラグイン設定
- プロジェクトで使用されているプラグインの設定を変更しないでください。

### 📌 定期保存を推奨
- データ損失を防ぐため、定期的にプロジェクトを保存してください。

---

## 📦 Modern UI Pack の使用ガイド

このプロジェクトでは、Unity Asset Store の [**Modern UI Pack**](https://assetstore.unity.com/packages/tools/gui/modern-ui-pack-201717) を使用しています。

### ✅ **UIManager プリセット適用ガイド**

**MUIP Manager** プリセットが自動的に適用されない場合、以下の手順を実行してください。

### 📌 **適用方法:**

1. **Unity エディタメニュー:**
   - `Tools > Modern UI Pack > Apply UIManager Preset` に移動。
2. **自動適用:**
   - `UIManager` プリセットが `MUIP Manager` スクリプタブルオブジェクトに適用されます。
3. **成功確認:**
   - コンソールに `Successfully applied 'UIManager' to 'MUIP Manager'` メッセージが表示されることを確認してください。

### 🛠️ **手動適用方法:**

- プリセットが自動適用されない場合:
  1. `Assets/Modern UI Pack/Resources/` に移動。
  2. `MUIP Manager.asset` を選択し、**Inspector** にプリセットファイルを手動でドラッグしてください。

### 🚀 **追加情報:**

- `.gitignore` 設定により、有料アセットの元ファイルは含まれておらず、`.meta` ファイルのみが提供されます。
- プロジェクトを正常に実行するには、[**Modern UI Pack**](https://assetstore.unity.com/packages/tools/gui/modern-ui-pack-201717) を購入してインポートしてください。

---

## 📝 プロジェクト構成
```plaintext
Assets/
├── Scripts/                 # 乱数システムのコアロジック
├── UIManager                # UI 一貫性のためのプリセットファイル
└── Scenes/                  # サンプルシーン
```

---

## 🛠️ 開発ノート
### 主な課題と解決策
- **ボタンスパム問題:** レスポンスの良い UI 操作を維持するためにデバウンスロジックを実装。
- **UI リセットロジック:** アクティブなオブジェクトのみ選択的にクリアできるよう `ClearScrollArea` メソッドを強化。
- **プリセット自動適用:** 共有プロジェクトで UI プリセットの一貫性を保つため、自動化スクリプトを実装。

---

## 💬 貢献方法
新機能の追加やバグ修正のためにリポジトリをフォークし、プルリクエストを作成してください。大幅な変更の場合は、アイデアを議論するために最初に issue を作成してください。

---

## 📝 ライセンス
このプロジェクトは MIT ライセンスの下で提供されています。詳細については `LICENSE` ファイルをご覧ください。

---

## 📞 お問い合わせ
お問い合わせは以下のメールアドレスにご連絡ください: [slh3951@gmail.com]

---

**GeppakuLab Random System をご利用いただきありがとうございます！** 🌙

