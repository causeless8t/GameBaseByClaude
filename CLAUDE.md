# Project Goal

GameBaseByClaude는 작은 Unity 2D RPG GameBase를 단계적으로 구현하는 프로젝트입니다.

- Claude Code는 기존 코드 분석과 구현을 담당합니다.
- 개발자는 요구사항, 아키텍처, 복잡도를 통제합니다.
- 범용 RPG Framework 구축이 목적이 아닙니다.

개발 환경(Unity 버전, 패키지 등)은 `ProjectSettings`와 `Packages/manifest.json`을 직접 확인해서 판단하고, 추측하지 않습니다.

# Working Principles

> 먼저 동작하는 가장 단순한 구현을 만들고, 실제 문제가 발생했을 때 구조를 개선한다.

- 필요한 기능만 구현합니다.
- 실제 필요가 생기기 전에 추상화하지 않습니다.
- 실제 중복, 강한 결합, 테스트 어려움, 확장 어려움이 발생했을 때 리팩터링합니다.
- 미래 요구사항을 추측해서 구조를 만들지 않습니다.
- 작은 기능 때문에 많은 파일이나 계층을 만들지 않습니다.

## 디버깅 원칙

버그가 발생하면 방어 코드를 먼저 추가하지 말고 실제 원인을 먼저 특정합니다.

원인은 코드 로직, Inspector/Prefab 설정, Unity Lifecycle, 입력 또는 호출 경로 등일 수 있습니다. 원인이 확인된 뒤 그 원인에 맞는 최소 수정만 적용합니다.

# Unity Coding Rules

- `MonoBehaviour`는 Unity Lifecycle이 필요한 경우에만 사용합니다.
- Inspector 노출이 필요한 필드는 `[SerializeField] private`을 기본으로 합니다.
- public field는 피하고, 외부에 값을 노출해야 하면 getter-only property나 의도가 드러나는 메서드를 사용합니다.
- `Update`/`FixedUpdate`처럼 반복 호출되는 곳에서 불필요한 작업을 하지 않습니다.
- `GetComponent` 결과는 반복해서 찾지 않고 캐싱합니다.
- `FindObjectOfType`, `FindFirstObjectByType`, `FindAnyObjectByType`, `GameObject.Find`를 일반적인 의존성 해결 방법으로 사용하지 않습니다. 제한적인 초기화 코드에서 사용 이유가 명확한 경우에만 허용합니다.

## Lifecycle 규칙

서로 다른 `MonoBehaviour`의 `Awake`/`Start` 실행 순서에 암묵적으로 의존하지 않습니다.

다른 컴포넌트의 초기화 결과가 필요하면 실행 순서가 실제로 보장되는지 먼저 확인하고, 보장되지 않는다면 참조 캐싱과 런타임 상태를 읽는 시점을 분리합니다.

# C# Style

클래스: `PlayerMovement`, `EnemyMovement`
public property/method: `CurrentHp`, `TakeDamage()`
private field: `_currentHp`, `_moveSpeed`
지역 변수: `currentHp`, `damage`
상수: `MaxLevel`, `DefaultSpeed`

타입이 명확한 경우 `var`를 허용하고, 파악하기 어려워지면 명시적 타입을 사용합니다.

조건문과 반복문은 한 줄이라도 중괄호를 사용합니다.

# Architecture Rules

- `CharacterBase`처럼 Player/Enemy가 공유하는 상속 구조를 미리 만들지 않습니다. 명확한 공통점이 실제로 늘어났을 때 검토합니다.
- Interface나 Abstract Class는 실제로 다형성이 필요해진 뒤에 도입합니다.
- Manager나 Singleton을 역할 없이 만들지 않습니다.
- 작은 기능을 여러 계층이나 파일로 미리 분해하지 않습니다.
- 전투, UI, 데이터 관련 기능은 Unity 기본 기능과 최소 요구사항(공격/데미지/HP/사망 등)으로 시작하고, Skill/Buff/DataTable/UI Framework 같은 시스템은 실제 요구사항이 생긴 뒤에만 도입합니다.

# Dependency Rules

- 외부 패키지는 명시적인 필요와 승인 없이 추가하지 않습니다.
- 기존 Causeless3t 패키지(UnityCore, Unity-DataTable, Unity-UI-Binding-System, AssetManager, HttpBase, SocketBase 등)도 자동으로 도입하지 않습니다.
- "이미 만들어둔 패키지가 있다"는 이유만으로 의존성을 추가하지 않고, 현재 구현에서 실제 필요성이 생겼을 때만 제안합니다.

# Workflow

1. 현재 코드와 프로젝트 구조를 먼저 확인합니다.
2. 필요한 변경과 설계 이유를 설명합니다.
3. 승인된 범위에서 최소 구현을 합니다.
4. 변경 내용을 보고합니다.
5. Unity Editor에서 실제 동작 확인이 필요한 항목을 명시합니다. Claude가 직접 검증할 수 없는 컴파일/Play Mode 확인을 검증했다고 보고하지 않습니다.

## 작업 보고

사용자가 별도의 보고 형식을 요청하면 그것을 우선합니다.

별도 형식이 없으면 최소한 변경한 파일, 변경 이유, Editor에서 확인할 항목만 보고합니다. 고정된 긴 보고 템플릿은 사용하지 않습니다.

# Restrictions

다음은 절대 금지가 아니라, 명확한 필요 없이는 도입하지 않는 구조입니다. 현재 요구사항으로 필요성이 명확해지면 선택지와 장단점을 설명하고 사용자가 결정합니다.

- 불필요한 Interface / Abstract Class / Base Class
- Manager / Singleton
- DI Framework
- Service Locator
- Event Bus
- Factory / Provider / Installer 등 불필요한 계층
- 범용 Framework
- 승인되지 않은 외부 패키지
- 큰 범위의 선제적 리팩터링
