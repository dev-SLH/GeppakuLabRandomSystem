[![JP](https://img.shields.io/badge/Language-Japanese-blue)](https://github.com/dev-SLH/GeppakuLabRandomSystem/blob/main/README.md) [![EN](https://img.shields.io/badge/Language-English-red)](https://github.com/dev-SLH/GeppakuLabRandomSystem/blob/main/Readme%20Kr.md)

# GeppakuLab Random System 프로젝트

🌙 **GeppakuLab Random System** 은 버추얼 유튜버 **月白 累 (Geppaku Lui)** 를 위해 제작한 난수 생성 프로그램입니다.

## 🎯 특징
- **난수 생성:** 범위와 개수를 사용자 지정할 수 있는 난수 생성.
- **정렬 설정:** 오름차순 및 내림차순 정렬 가능.
- **중복 제어:** 중복 허용/금지 설정 가능.
- **최소 해상도 제한:** 사용자가 정의한 최소 해상도 이하로 창 크기 조정 방지.

---

## 📽️ 시연 영상

### 🎥 **난수 생성 프로그램 시연**
[![GeppakuLab ランダム·システム Showcase](https://img.youtube.com/vi/j1sl5BKaaOg/0.jpg)](https://youtu.be/j1sl5BKaaOg)

### 🎥 **설정 및 옵션 시연**
[![Settings Showcase](https://img.youtube.com/vi/RKB7lpRkYCU/0.jpg)](https://youtu.be/RKB7lpRkYCU)

---

## 📦 설치 방법
1. 최신 버전을 [Releases](https://github.com/dev-SLH/GeppakuLabRandomSystem/releases) 섹션에서 다운로드하세요.
2. 제공된 `GeppakuLab_RandomSystem_Installer.exe` 파일을 실행하세요.
3. 화면의 지시에 따라 설치를 완료하세요.

---

## 🛠️ 시스템 요구 사항
- **운영체제:** Windows 10 이상
- **런타임:** .NET Framework 4.7.2 이상

---

## 📖 사용 방법

1. **범위 설정:** 난수의 최소값과 최대값을 설정합니다.
2. **개수 설정:** 생성할 난수의 개수를 선택합니다.
3. **생성:** `생성` 버튼을 클릭하여 난수를 생성합니다.
4. **정렬 및 재정렬:** 드롭다운에서 정렬 옵션(오름차순/내림차순)을 선택합니다.
5. **결과 복사:** 생성된 난수를 클립보드에 복사합니다.

---

## 🛡️ 라이선스
이 프로젝트는 **MIT 라이선스**로 공개되어 있습니다. 자세한 내용은 `LICENSE` 파일을 참조하세요.

---

## 💌 크레딧
- **개발자:** [설령화(雪霊花_ソルリョンファ)-SLH](https://x.com/slh3951)
- **Developed for:** [Geppaku Lui (月白 累)](https://www.youtube.com/@Geppaku_Lui)

---

## 🚀 Unity 프로젝트 시작하기

### 1. 필요한 에셋 임포트
1. [Modern UI Pack](https://assetstore.unity.com/packages/tools/gui/modern-ui-pack-201717) 을 Unity Asset Store에서 구매하고 다운로드하세요.
2. Unity 프로젝트에 Modern UI Pack을 임포트합니다.

### 2. 프리셋 적용
1. Unity 에디터 상단 메뉴에서 **Tools > Modern UI Pack > Apply UIManager Preset**을 클릭하세요.
2. **프리셋이 자동으로 적용되지 않을 경우:**
   - `Assets/Modern UI Pack/Resources/` 경로에서 `MUIP Manager.asset`을 선택하고,
     **Inspector**에서 `Preset` 파일을 직접 드래그하여 적용하세요.

### 3. 샘플 씬 실행
1. 제공된 샘플 씬(`Assets/Scenes/GeppakuLabRandomSystem.unity`)을 열어주세요.
2. Unity Editor에서 **Play** 버튼을 눌러 프로젝트를 실행하세요.


## ⚠️ Unity 프로젝트 사용 시 주의점

### 📌 필수 에셋 확인
- 프로젝트에 필요한 모든 패키지와 에셋을 반드시 임포트하세요.
- 필수 에셋: [Modern UI Pack](https://assetstore.unity.com/packages/tools/gui/modern-ui-pack-201717) (구매 후 임포트 필요)

### 📌 Unity 버전 호환성
- 권장 Unity 버전: **6000.0.32f1 이상**
- 더 낮은 버전에서는 일부 기능이 제대로 작동하지 않을 수 있습니다.

### 📌 씬 설정 변경 주의
- 제공된 샘플 씬을 그대로 사용하세요. (샘플 씬 위치: `Assets/Scenes/GeppakuLabRandomSystem.unity`)
- ⚠️ **주의:** 씬 설정을 변경하면 의도한 대로 동작하지 않을 가능성이 있습니다.

### 📌 플러그인 설정 변경 금지
- 프로젝트에서 사용 중인 플러그인의 설정을 수정하지 마세요.

### 📌 정기 저장 권장
- 작업 중 데이터 손실을 방지하기 위해 **정기적으로 프로젝트를 저장**하세요.

---

## 📦 Modern UI Pack 사용 안내

이 프로젝트는 Unity 에셋 스토어의 [**Modern UI Pack**](https://assetstore.unity.com/packages/tools/gui/modern-ui-pack-201717) 을 사용하고 있습니다.

### ✅ **UIManager 프리셋 적용 가이드**

프로젝트를 처음 열었을 때, **MUIP Manager** 프리셋이 제대로 적용되지 않을 수 있습니다. 다음 단계를 따라 프리셋을 적용해주세요.

### 📌 **적용 방법:**

1. **Unity 에디터 상단 메뉴에서:**
   - `Tools > Modern UI Pack > Apply UIManager Preset` 클릭
2. **자동으로 프리셋 적용:**
   - `MUIP Manager` ScriptableObject에 `UIManager` 프리셋이 자동으로 적용됩니다.
3. **성공 메시지 확인:**
   - 콘솔에 `Successfully applied 'UIManager' to 'MUIP Manager'` 메시지가 출력됩니다.

### 🛠️ **수동 적용 방법:**

- 프리셋이 자동으로 적용되지 않을 경우:
  1. `Assets/Modern UI Pack/Resources/` 경로에서 `MUIP Manager.asset` 수동 선택
  2. **Inspector**에서 `Preset` 파일을 직접 드래그하여 적용

### 🚀 **추가 정보:**

- `.gitignore` 설정에 의해 **유료 에셋의 원본 파일은 포함되지 않고**, `.meta` 파일만 제공됩니다.
- 프로젝트를 원활히 실행하려면, Unity Asset Store에서 [**Modern UI Pack**](https://assetstore.unity.com/packages/tools/gui/modern-ui-pack-201717) 을 구매 후 임포트하세요.

---

## 📝 프로젝트 구조
```plaintext
Assets/
├── Scripts/                 # 난수 시스템의 핵심 로직
├── UIManager                # UI 일관성을 위한 프리셋 파일
└── Scenes/                  # 샘플 씬
```

---

## 🛠️ 개발 노트
### 주요 도전과 해결책
- **버튼 스팸 문제:** 빠른 반응성을 유지하기 위해 디바운싱 로직을 구현하여 해결.
- **UI 초기화 로직:** 활성 객체만 선택적으로 초기화할 수 있도록 `ClearScrollArea` 메서드 개선.
- **프리셋 자동 적용:** 공유 프로젝트에서 UI 프리셋 일관성을 유지하기 위한 자동화 스크립트 구현.

---

## 💬 기여하기
새로운 기능 추가나 버그 수정을 위해 리포지토리를 포크하고 풀 리퀘스트를 생성하세요. 중요한 변경 사항의 경우, 아이디어를 먼저 논의하기 위해 이슈를 열어주세요.

---

## 📝 라이선스
이 프로젝트는 MIT 라이선스로 제공됩니다. 자세한 내용은 `LICENSE` 파일을 참조하세요.

---

## 📞 연락하기
문의 사항이 있으면 다음 이메일로 연락해주세요: [slh3951@gmail.com]

---

**GeppakuLab Random System을 이용해주셔서 감사합니다!** 🌙

⚠️ **주의:** 현재 해당 프로그램에 한국어는 포함되어 있지않고 한국어 업데이트 일정은 미정입니다.
