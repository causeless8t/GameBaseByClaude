# GameBaseByClaude

## 프로젝트 개요

GameBaseByClaude는 Claude Code를 활용하여 개발하는 간단한 Unity RPG 프로젝트입니다.

이 프로젝트의 목적은 완성도 높은 대규모 RPG를 만드는 것이 아니라, Claude Code를 실제 Unity 개발 과정에 활용하면서 유지보수 가능한 게임 구조를 단계적으로 구축하는 것입니다.

개발자는 프로젝트의 방향과 아키텍처를 결정하고, Claude Code는 이를 기반으로 구현과 리팩터링을 지원합니다.

---

## 개발 환경

* Engine: Unity
* Language: C#
* Rendering: URP
* Version Control: Git

Unity 버전 및 패키지 정보는 프로젝트의 `ProjectSettings`와 `Packages/manifest.json`을 직접 확인하여 판단합니다.

추측으로 Unity 버전이나 설치된 패키지를 결정하지 않습니다.

---

## 기본 개발 원칙

### 1. 기존 코드를 먼저 확인한다

코드를 수정하기 전에 반드시 관련 파일과 주변 구조를 먼저 확인합니다.

새로운 시스템을 만들기 전에 다음을 확인합니다.

* 이미 비슷한 기능이 존재하는지
* 재사용 가능한 클래스가 존재하는지
* 현재 프로젝트의 네이밍 규칙
* 현재 프로젝트의 폴더 구조
* 기존 시스템과의 의존 관계

기존 구현을 확인하지 않고 새로운 시스템을 중복해서 만들지 않습니다.

---

### 2. 필요한 만큼만 구현한다

현재 요구사항을 해결하는 데 필요한 최소한의 구조만 구현합니다.

미래에 필요할 가능성만으로 다음과 같은 구조를 미리 만들지 않습니다.

* 과도한 인터페이스
* 불필요한 추상 클래스
* 복잡한 DI 구조
* Service Locator
* Event Bus
* 범용 프레임워크
* 사용되지 않는 확장 포인트

실제 요구사항이 생겼을 때 리팩터링합니다.

---

### 3. 작은 단위로 작업한다

한 번의 작업에서 프로젝트 전체를 변경하지 않습니다.

가능하면 다음 순서로 진행합니다.

1. 현재 코드 분석
2. 변경 계획 설명
3. 최소 구현
4. 컴파일 오류 확인
5. 기존 코드와의 충돌 확인
6. 필요하면 리팩터링

대규모 변경이 필요한 경우 바로 수정하지 말고 먼저 변경 범위를 설명합니다.

---

## Unity 개발 규칙

### MonoBehaviour

`MonoBehaviour`는 Unity 생명주기가 필요한 객체에만 사용합니다.

게임 로직을 무조건 `MonoBehaviour`로 구현하지 않습니다.

가능하면 일반 C# 객체와 Unity Component의 역할을 분리합니다.

---

### SerializeField

Inspector에서 설정해야 하는 필드는 기본적으로 다음 형태를 사용합니다.

```csharp
[SerializeField]
private float _moveSpeed;
```

Inspector 노출이 필요하지 않은 필드는 `[SerializeField]`를 사용하지 않습니다.

특별한 이유가 없다면 public field를 사용하지 않습니다.

---

### 접근 제한자

접근 범위는 가능한 작게 유지합니다.

기본적으로 다음 우선순위를 고려합니다.

```text
private
protected
internal
public
```

외부에서 사용할 이유가 없는 멤버를 public으로 만들지 않습니다.

---

### Update

`Update()` 사용 자체를 피하지는 않습니다.

다만 모든 시스템이 각각 `Update()`를 가지는 구조를 무조건 만들지 않습니다.

프레임마다 실행할 필요가 없는 로직을 `Update()`에서 실행하지 않습니다.

---

### GetComponent

초기화 과정에서 필요한 `GetComponent` 사용은 허용합니다.

매 프레임 반복되는 `GetComponent` 호출은 피합니다.

필요한 Component는 캐싱합니다.

---

### Find 계열 API

다음 API를 게임 로직의 일반적인 의존성 해결 방법으로 사용하지 않습니다.

```csharp
FindObjectOfType
FindFirstObjectByType
FindAnyObjectByType
GameObject.Find
```

샘플이나 매우 제한적인 초기화 코드에서 사용할 경우 사용 이유가 명확해야 합니다.

---

## C# 스타일

### 네이밍

클래스:

```csharp
PlayerController
EnemyController
CombatSystem
```

public property / method:

```csharp
CurrentHp
TakeDamage()
Attack()
```

private field:

```csharp
_currentHp
_moveSpeed
_target
```

지역 변수:

```csharp
currentHp
target
damage
```

상수:

```csharp
MaxLevel
DefaultSpeed
```

---

### var

타입이 명확한 경우 `var` 사용을 허용합니다.

```csharp
var player = GetComponent<Player>();
```

타입을 파악하기 어려워지는 경우 명시적인 타입을 사용합니다.

---

### 중괄호

조건문과 반복문에는 한 줄 코드라도 중괄호를 사용합니다.

```csharp
if (_currentHp <= 0)
{
    Die();
}
```

---

## 아키텍처 원칙

초기 프로젝트에서는 복잡한 아키텍처를 사용하지 않습니다.

기본적인 관심사는 다음 정도로 구분합니다.

```text
Character
Combat
Data
UI
```

실제 코드가 증가하면서 필요성이 확인될 경우 구조를 확장합니다.

폴더 구조를 먼저 크게 설계한 뒤 코드를 끼워 넣지 않습니다.

---

## Character

Player와 Enemy가 공통으로 사용하는 기능이 실제로 발생하면 공통 Character 구조를 고려합니다.

처음부터 모든 캐릭터를 위한 거대한 상속 구조를 만들지 않습니다.

예:

```text
Player
Enemy
```

에서 시작하고 HP, Damage 처리 등 명확한 공통점이 증가하면

```text
Character
 ├── Player
 └── Enemy
```

구조를 검토합니다.

---

## Combat

전투 시스템은 처음에는 단순하게 유지합니다.

초기 요구사항:

* 공격
* 데미지
* HP
* 사망

Skill, Buff, StatusEffect, DamagePipeline 등의 시스템은 실제 요구사항이 생기기 전까지 만들지 않습니다.

---

## Data

초기 단계에서는 Unity 기본 기능을 우선 사용합니다.

필요하면 `ScriptableObject` 등을 사용할 수 있습니다.

외부 데이터 시스템이나 별도 데이터 프레임워크는 요구사항이 발생한 이후 도입합니다.

---

## UI

초기 UI는 Unity 기본 UI 구조를 사용합니다.

UI Binding Framework, MVVM 또는 별도의 UI Framework를 처음부터 도입하지 않습니다.

반복적인 UI 연결 코드가 실제로 문제가 되기 시작하면 별도 시스템 도입을 검토합니다.

---

## 외부 패키지

새로운 외부 패키지를 임의로 추가하지 않습니다.

패키지가 필요하다고 판단되는 경우 먼저 다음 내용을 설명합니다.

* 패키지가 필요한 이유
* Unity 기본 기능으로 해결할 수 없는 이유
* 추가되는 의존성
* 대안

사용자의 동의 없이 `Packages/manifest.json`에 새로운 외부 패키지를 추가하지 않습니다.

---

## 기존 Causeless3t 패키지

다음과 같은 기존 라이브러리가 존재할 수 있지만 초기 구현에서는 자동으로 사용하지 않습니다.

* UnityCore
* Unity-DataTable
* Unity-UI-Binding-System
* AssetManager
* HttpBase
* SocketBase

프로젝트에서 실제 문제가 발생하고 해당 라이브러리가 해결책이 될 경우 도입을 제안합니다.

사용자의 요청 없이 자동으로 추가하지 않습니다.

---

## 파일 생성 규칙

새로운 파일을 만들기 전에 기존 파일로 해결할 수 있는지 확인합니다.

하나의 작은 기능 때문에 다음과 같은 파일을 여러 개 생성하지 않습니다.

```text
IPlayerService
PlayerService
PlayerServiceFactory
PlayerServiceProvider
PlayerServiceInstaller
```

단순한 기능이라면 단순한 구현을 유지합니다.

---

## 리팩터링

동작하는 코드를 단순히 더 세련된 패턴으로 바꾸기 위한 리팩터링은 하지 않습니다.

다음과 같은 명확한 이유가 있을 때 리팩터링합니다.

* 중복 코드
* 강한 결합
* 테스트 어려움
* 기능 확장 어려움
* 버그 발생 가능성
* 가독성 저하

---

## 작업 보고

작업을 완료하면 다음 내용을 간단히 설명합니다.

### 변경한 내용

어떤 파일을 생성하거나 수정했는지 설명합니다.

### 설계 이유

왜 해당 구조를 선택했는지 설명합니다.

### 확인할 사항

Unity Editor에서 사용자가 확인해야 하는 사항이 있다면 설명합니다.

### 다음 단계

현재 구현 이후 자연스럽게 진행할 수 있는 작업을 제안합니다.

---

## 금지 사항

사용자의 명시적인 요청 없이 다음 작업을 하지 않습니다.

* 대규모 프로젝트 구조 변경
* 기존 시스템 전체 재작성
* 외부 패키지 추가
* 새로운 아키텍처 프레임워크 도입
* DI Framework 도입
* Event Bus 도입
* Service Locator 도입
* Singleton 남용
* 불필요한 Manager 클래스 생성
* 미래 기능을 예상한 대규모 추상화

---

## 최우선 원칙

이 프로젝트에서 가장 중요한 원칙은 다음과 같습니다.

> 먼저 동작하는 가장 단순한 구현을 만들고, 실제 문제가 발생했을 때 구조를 개선한다.

Claude Code는 프로젝트의 아키텍처를 독단적으로 결정하지 않습니다.

중요한 구조적 결정이 필요한 경우 선택지와 장단점을 설명하고 사용자가 결정할 수 있도록 합니다.
