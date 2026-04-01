🎮 Eating Nightmare
43MB 저사양 서바이벌 액션

📌 Project Overview
개발 기간: 2026.01 ~ 2026.03 (약 2개월)

개발 환경: Unity 2022.3.x (URP), C#

핵심 목표: 카메라 추적, 오브젝트 풀링, 캐릭터 컨트롤러 등 게임의 핵심 로직을 에셋이나 플러그인 없이 순수 C#과 유니티 API만으로 설계 및 구현

🚀 Key Technical Features
1. 효율적인 자원 관리를 위한 PoolManager
대량의 아이템과 적이 등장하는 게임 특성을 고려하여, 런타임 부하를 최소화하는 범용 오브젝트 풀링 시스템을 구축했습니다.

  Dictionary & Queue 구조: string 키를 활용해 11종 이상의 프리팹을 독립적인 큐로 관리, 데이터 접근 및 삽입 속도 최적화 (O(1)).
  
  Pre-warm(예열) 시스템: 게임 시작 시 설정된 size만큼 객체를 사전 생성하여 플레이 중 발생하는 프레임 드랍(GC) 방지.
  
  Dynamic Expansion: 초기 할당량을 초과하는 상황에서도 유연하게 객체를 추가 생성하고 반납받는 방어적 로직 설계.

2. SmoothDamp 기반의 안정적인 카메라 시스템

  Jittering(흔들림) 방지: LateUpdate를 활용하여 캐릭터 이동 연산 완료 후 카메라 위치를 갱신, 화면 떨림 현상 제거.

  SmoothDamp 적용: 물리 기반의 감속 로직을 적용하여 Lerp 특유의 끊김 현상 없이 부드러운 쿼터뷰 시점 유지.

🛠 Troubleshooting: "The Ghost Event" 해결
문제: 씬 전환 및 게임 재시작 시, 파괴된 오브젝트의 이벤트 핸들러가 남아서 NullReferenceException 및 로직 오작동 발생.

원인 분석: 이벤트 구독(Subscribe) 후 명시적인 해제(Unsubscribe)가 이루어지지 않아 메모리 누수 발생.

해결 방법: 유니티 라이프사이클 함수인 OnDisable() 및 OnDestroy()에서 모든 이벤트를 명시적으로 해제하는 구조로 리팩토링.

결과: 수차례의 씬 반복 전환 시에도 메모리 점유율을 일정하게 유지하며 런타임 안정성 확보.

📂 Project Structure
Plaintext
Assets/Scripts/

├── Cameras/        # CameraController (SmoothDamp logic)

├── Managers/       # PoolManager (Singleton, Optimization)

├── Objects/        # Item, Enemy, Player logic

└── UI/             # Game HUD & Menu Systems
