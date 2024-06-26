<h1>A-12 이승영 개인 과제</h1>
<h2>구현 성공 과제</h2>

* 필수 구현 과제
  * 메인 메뉴 & 상태창 & 인벤토리 & 상점 UI
    * 버튼 입력에 맞는 UI 표시
    * 잘못된 입력인 경우 재입력
  * 상태창
    * 현재 플레이어의 스테이터스 표시
    * 장비 착용 / 해제 시 효과 부여 / 제거 & 증감치 적용 / 표시
  * 인벤토리
    * 현재 보유중인 장비 표시
    * 장비 장착 / 해제 기능 & 장착 중인 장비 "```[E]```" 표시
  * 상점
    * 장비 구매 기능
    * 구매 시 gold 차감 & 구매 완료 표시 (이미 보유한 장비 & gold 불충분 시 구매 실패)
   
* 추가 구현 과제
  * 장비를 자료 구조로 사용해서 관리하기 (```List<Item>```)
  * 나만의 장비 추가
  * 상점 - 아이템 판매 기능 추가
    * 상점 UI에 아이템 판매 UI 추가
    * 현재 보유 중인 장비들만 추려서 리스트에 표시
    * 착용 중인 경우 "```[E]```" 표시
    * 판매 가격 표시 (구매 가격의 85%)
    * 판매 시 보유 아이템 리스트 / 판매 UI에서 제외 & 판매가 획득
    * 착용 중인 아이템인 경우 판매와 동시에 장착 해제
  * 인벤토리 - 장비 중첩 장착 방지 기능 추가
    * 이미 같은 타입의 다른 장비를 착용 중인 경우, 장착 중인 장비를 먼저 해제 후, 선택한 장비 착용
    * 효과 증감치 적용 개선
