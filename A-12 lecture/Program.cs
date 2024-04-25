namespace A_12_lecture
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player().SetInstance(); // Player 싱글톤
            player.Init(); // Player 필드 초기화
            Shop itemList = new Shop(); //ItemList 객체 생성
            Inventory inventory = new Inventory().SetInstance(); //Inventory 싱글톤
            player.ShowMainScreen();

            while (true) // 무한 반복
            {
                ConsoleKey key = Console.ReadKey().Key; // 키 입력
                switch (key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1: // 상태창
                        Thread.Sleep(500);
                        Console.Clear();
                        player.ShowStatusScreen();
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2: // 인벤토리
                        Thread.Sleep(500);
                        Console.Clear();
                        inventory.ShowInventoryScreen();
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3: // 상점
                        Thread.Sleep(500);
                        Console.Clear();
                        itemList.ShowShopScreen();
                        break;
                    default:
                        Console.WriteLine("\n다시 입력해 주세요.");
                        Thread.Sleep(500);
                        Console.Clear();
                        player.ShowMainScreen();
                        break;
                }
            }
        }
    }

    public class MainScreen // Player, Shop, Inventory에 상속해 주기 위해 클래스로 선언
    {
        public void ShowMainScreen() // 메인 메뉴를 표시하는 메서드
        {
            Console.WriteLine("스파르타 마을에 오신 여려분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력하세요.");
            Console.Write(">> ");
        }
    }
    public class Player : MainScreen // 플레이어 클래스
    {
        public static Player instance;

        public int level; // 레벨
        public string name = ""; // 이름
        public string job = ""; // 직업
        public int atk; // 공격력
        public int def; //방어력
        public int hp; // 체력
        public int gold; // 재화
        public string plusAtkStr = ""; // 부여받고있는 공격력 버프
        public string plusDefStr = ""; // 부여받고있는 방어력 버프

        public void Init() // 데이터 초기화 메서드
        {
            level = 1;
            name = "Chad";
            job = "전사";
            atk = 10;
            def = 5;
            hp = 100;
            gold = 20000;
        }
        public Player SetInstance() // 인스턴스화
        {
            if (instance == null)
            {
                instance = new Player();
            }
            return instance;
        }

        public void ShowStatusScreen() // 상태창 화면을 띄워주는 메서드
        {
            while (true)
            {
                Console.WriteLine("상태 보기");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.");
                Console.WriteLine();
                Console.WriteLine($"Lv : {level.ToString("00")}");
                Console.WriteLine($"{name} ({job})");
                Console.WriteLine($"공격력 : {atk} {plusAtkStr}");
                Console.WriteLine($"방여력 : {def} {plusDefStr}");
                Console.WriteLine($"체력 : {hp}");
                Console.WriteLine($"Gold : {gold} G");
                Console.WriteLine();
                Console.WriteLine("0. 메인 메뉴");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");

                ConsoleKey key = Console.ReadKey().Key; // 키 입력
                if (key == ConsoleKey.D0 || key == ConsoleKey.NumPad0) // 0번
                {
                    Thread.Sleep(500);
                    Console.Clear();
                    ShowMainScreen(); // 메인 화면 표시
                    break; // 반복문 탈출
                }
                else // 잘못된 버튼 입력
                {
                    Console.WriteLine("\n다시 입력해 주세요.");
                    Thread.Sleep(500);
                    Console.Clear();
                }
            }
        }
    }

    public class Shop : Item
    {
        public static List<Item> items = new List<Item>(); // 상점의 아이템을 저장할 List

        public Shop() // 생성자
        {
            if (items.Count == 0) // List items가 비어있다면
            {
                
                for (idx = 0; idx < 7; idx++) // 저장된 아이템 갯수만큼
                {
                    Item item = SetItem(idx); //Item 객체 생성 & 데이터 할당
                    items.Add(item); //List에 Item 객체 추가
                }
            }
        }

        public void PayPrice(int idx) // 비용을 지불하는 메서드
        {
            Player.instance.gold -= items[idx].cost; // 플레이어 재화에서 구매하는 아이템의 가격 만큼 감소
            items[idx].isBuy = true; // 해당 아이템의 구매 현황 활성화
        }

        public void ShowShopScreen() // 상점 화면을 띄워주는 메서드
        {
            string type = ""; // 장비의 타입 문자열
            string failToBuyStr = ""; // 구매 실패 문자열
            string equipStr = ""; //장비중? 문자열

            while (true)
            {
                Inventory.instance.UpdateInventory(); // 소지 품목 업데이트
                Console.WriteLine("상점");
                Console.WriteLine();
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{Player.instance.gold} G\n");
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].type == 0) type = "공격력 "; // 장비 타입이 무기인가?
                    else if (items[i].type == 1) type = "방어력 "; // 장비 타입이 갑옷인가?
                    if (items[i].isBuy == true) // 구매가 완료된 장비인가?
                        Console.WriteLine($"- {items[i].name}\t| {type} +{items[i].value}\t| {items[i].description}\t| 구매 완료");
                    else
                        Console.WriteLine($"- {items[i].name}\t| {type} +{items[i].value}\t| {items[i].description}\t| {items[i].cost} G");
                }
                Console.WriteLine("\n0. 메인 메뉴\n1. 아이템 구매\n2. 아이템 판매");
                Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");

                ConsoleKey key = Console.ReadKey().Key; // 키 입력
                if (key == ConsoleKey.D1 || key == ConsoleKey.NumPad1) //1번 (구매 페이지)
                {
                    while (true)
                    {
                        Thread.Sleep(500);
                        Console.Clear();
                        Console.WriteLine("상점 - 아이템 구매");
                        Console.WriteLine();
                        Console.WriteLine("[보유 골드]");
                        Console.WriteLine($"{Player.instance.gold} G\n");
                        for (int i = 0; i < items.Count; i++)
                        {
                            if (items[i].type == 0) type = "공격력 ";
                            else if (items[i].type == 1) type = "방어력 ";
                            if (items[i].isBuy == true)
                                Console.WriteLine($"{i + 1}.- {items[i].name}\t| {type} +{items[i].value}\t| {items[i].description}\t| 구매 완료");
                            else
                                Console.WriteLine($"{i + 1}.- {items[i].name}\t| {type} +{items[i].value}\t| {items[i].description}\t| {items[i].cost} G");
                        }
                        Console.WriteLine("\n" + failToBuyStr);
                        Console.WriteLine($"1. ~ {items.Count} : 아이템 구매, 0. 상점 메뉴\n");
                        Console.Write("구매할 아이템을 선택해 주세요\n>> ");

                        key = Console.ReadKey().Key;
                        if ((key >= ConsoleKey.D1 && key <= ConsoleKey.D7) || (key >= ConsoleKey.NumPad1 && key <= ConsoleKey.NumPad7))
                        {
                            int idx = (int)key % 48 - 1; // 키 입력값을 인덱스 값으로 변환
                            if (items[idx].isBuy) failToBuyStr = "이미 구매한 상품입니다."; // 이미 구매한 장비인가?
                            else if (Player.instance.gold < items[idx].cost) failToBuyStr = "잔액이 부족합니다."; // 현재 재화가 선택한 장비의 가격보다 적은가?
                            else // 아니라면 (장비를 살 수 있는 상태라면)
                            {
                                failToBuyStr = "구매해 주셔서 감사합니다.";
                                PayPrice(idx); // 가격 지불 메서드
                            }
                        }
                        else if (key == ConsoleKey.D0 || key == ConsoleKey.NumPad0) // 0번
                        {
                            Thread.Sleep(500);
                            Console.Clear();
                            ShowShopScreen(); // 상점 메인 화면
                            break;
                        }
                        else Console.WriteLine("\n다시 입력해 주세요");
                    }
                    break;
                }
                else if (key == ConsoleKey.D2 || key == ConsoleKey.NumPad2) // 2번 (아이템 판매 페이지)
                {
                    while (true)
                    {
                        Thread.Sleep(500);
                        Console.Clear();
                        Console.WriteLine("상점 - 아이템 판매");
                        Console.WriteLine();
                        Console.WriteLine("[보유 골드]");
                        Console.WriteLine($"{Player.instance.gold} G\n");
                        Console.WriteLine("[보유 장비]\n");

                        if (Inventory.instance.ownItems.Count <= 0) // 보유 중인 장비가 없는 경우
                        {
                            Console.WriteLine("보유 중인 장비가 없습니다.");
                            Console.Write("0. 상점 메뉴\n>> ");
                        }
                        else // 보유중인 장비가 있는 경우
                        {
                            for (int i = 0; i < Inventory.instance.ownItems.ToList().Count; i++) // 보유 중인 장비 갯수 만큼
                            {
                                if (Inventory.instance.ownItems.ToList()[i].type == 0) type = "공격력 "; // 해당 장비가 무기인가?
                                else if (Inventory.instance.ownItems.ToList()[i].type == 1) type = "방어력 "; // 해당 장비가 갑옷인가?
                                if (Inventory.instance.ownItems.ToList()[i].isEquip == true) equipStr = "[E]"; // 현재 해당 장비를 장비 중인가?
                                else equipStr = ""; // 해당 장비를 장비 중이 아닌가?
                                Console.WriteLine($"{i + 1}.-{equipStr} {Inventory.instance.ownItems.ToList()[i].name}\t| {type} +{Inventory.instance.ownItems.ToList()[i].value}\t| {Inventory.instance.ownItems.ToList()[i].description}\t| {Inventory.instance.ownItems.ToList()[i].cost * 0.85f} G");
                            }
                            if (Inventory.instance.ownItems.Count == 1) Console.Write("\n1. 아이템 판매. "); // 보유 장비가 1개인 경우
                            else Console.Write($"\n1. ~ {Inventory.instance.ownItems.Count}. 아이템 판매. "); // 보유 장비가 2개 이상인 경우
                            Console.WriteLine("0. 상점 메뉴");
                            Console.Write(">> ");
                        }

                        key = Console.ReadKey().Key; // 키 입력
                        if((key >= ConsoleKey.D1 && key <= ConsoleKey.D7) || (key >= ConsoleKey.NumPad1 && key <= ConsoleKey.NumPad7))
                        {
                            int idx = (int)key % 48 - 1; // 키 입력값을 인덱스 값으로 변환
                            if(idx >= Inventory.instance.ownItems.Count) // 해당 인덱스 값이 보유 중인 장비 갯수를 초과하는가? (잘못 입력된 경우)
                            {
                                Console.WriteLine("\n다시 입력해 주세요.");
                            }
                            else // 맞게 입력한 경우
                            {
                                Item sellItem = Inventory.instance.ownItems.ToList()[idx]; // 선택한 장비 저장
                                Player.instance.gold += (int)(sellItem.cost * 0.85f); // 선택한 장비의 원가 * 0.85 만큼 플레이어의 재화에 추가
                                sellItem.isBuy = false; // 해당 장비 구매 상태 초기화
                                if (sellItem.isEquip) Inventory.instance.EquipSwap(idx); // 만약 해당 장비가 지금 장비 중인가? 장비 상태 전환 메서드 호출
                                Inventory.instance.ownItems.Remove(sellItem); // 보유 장비 리스트에서 해당 장비 제거
                            }
                        }
                        else if (key == ConsoleKey.D0 || key == ConsoleKey.NumPad0) //0번 (상점 메인 화면)
                        {
                            Thread.Sleep(500);
                            Console.Clear();
                            break;
                        }
                        else // 잘못 입력된 경우
                        {
                            Console.WriteLine("\n다시 입력해 주세요.");
                        }
                    }
                }
                else if (key == ConsoleKey.D0 || key == ConsoleKey.NumPad0) //0번 (메인 화면)
                {
                    Thread.Sleep(500);
                    Console.Clear();
                    ShowMainScreen(); // 메인 화면 표시
                    break; // 루프 종료
                }
                else // 잘못 입력된 경우
                {
                    Console.WriteLine("\n다시 입력해 주세요.");
                    Thread.Sleep(500);
                    Console.Clear();
                }
            }
        }
    }

    public class Item : MainScreen
    {
        public int idx; // 장비의 고유 번호
        public string name = ""; // 장비의 이름
        public int type; // 장비의 타입  0 : 갑옷 1 : 무기
        public int value; // 장비의 수치 (단일 수치라서 가능한 방법. 장비가 공/방 수치가 다 있는 경우 사용 못함.)
        public string description = ""; // 장비의 설명 문자열
        public int cost; // 장비의 가격
        public bool isBuy = false; // 해당 장비의 구매 현황
        public bool isEquip = false; // 해당 장비의 장착 현황

        public Item SetItem(int idx) // 장비의 정보를 할당해주는 메서드
        {
            Item item = new Item(); // Item 객체 생성
            switch (idx)
            {
                case 0:
                    item.idx = 0;
                    item.name = "수련자 갑옷";
                    item.type = 1;
                    item.value = 5;
                    item.description = "수련에 도움을 주는 갑옷입니다.";
                    item.cost = 1000;
                    break;
                case 1:
                    item.idx = 1;
                    item.name = "무쇠갑옷";
                    item.type = 1;
                    item.value = 9;
                    item.description = "무쇠로 만들어져 튼튼한 갑옷입니다.";
                    item.cost = 2500;
                    break;
                case 2:
                    item.idx = 2;
                    item.name = "스파르타의 갑옷";
                    item.type = 1;
                    item.value = 15;
                    item.description = "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.";
                    item.cost = 3500;
                    break;
                case 3:
                    item.idx = 3;
                    item.name = "낡은 검";
                    item.type = 0;
                    item.value = 2;
                    item.description = "쉽게 볼 수 있는 낡은 검입니다.";
                    item.cost = 600;
                    break;
                case 4:
                    item.idx = 4;
                    item.name = "청동 도끼";
                    item.type = 0;
                    item.value = 5;
                    item.description = "어디선가 사용됐던거 같은 도끼입니다.";
                    item.cost = 1500;
                    break;
                case 5:
                    item.idx = 5;
                    item.name = "스파르타의 창";
                    item.type = 0;
                    item.value = 7;
                    item.description = "스파르타의 전사들이 사용했다는 전설의 창입니다.";
                    item.cost = 3000;
                    break;
                case 6:
                    item.idx = 6;
                    item.name = "묠니르";
                    item.type = 0;
                    item.value = 35;
                    item.description = "천둥의 신의 유산입니다.";
                    item.cost = 8000;
                    break;
                default:
                    break;
            }
            return item; // 데이터를 저장한 Item 객체를 반환
        }
    }
    public class Inventory : Shop
    {
        public static Inventory instance;

        public int afkGap = 0; // 공격력 버프 수치
        public int defGap = 0; // 방어력 버프 수치
        public string typeStr = ""; // 장비 타입 문자열
        public string equipStr = ""; // 장착 상태 문자열
        public bool isWeaponEquip = false; // 현재 무기를 장착 중인가?
        public bool isArmorEquip = false; // 현재 갑옷을 장착 중인가?
        public HashSet<Item> ownItems = new HashSet<Item>(); // 보유 중인 아이템 목록 (중복 값을 제외하기 위해 HashSet 사용)


        public Inventory SetInstance()
        {
            if (instance == null)
            {
                instance = new Inventory();
            }
            return instance;
        }
        public void UpdateInventory() // 보유 아이템 목록을 업데이트 하는 메서드
        {
            for (int i = 0; i < items.Count; i++) // 전체 아이템 갯수 만큼
            {
                if (items[i].isBuy == true) ownItems.Add(items[i]); // 해당 장비가 구매되었는가? 보유 장비 목록에 추가 (중복일 경우 제외)
            }
        }

        public void EquipSwap(int idx) // 장비 착용 상태를 반전시키는 메서드
        {
            if (ownItems.ToList()[idx].isEquip == false) // 현재 선택한 장비를 장착 중이 아닌가?
            {
                if (ownItems.ToList()[idx].type == 0) // 해당 장비가 무기인가?
                {
                    if(isWeaponEquip) // 현재 다른 무기를 장착 중인가?
                    {
                        for(int i = 0; i<ownItems.Count; i++) // 보유 장비 갯수 만큼
                        {
                            if(ownItems.ToList()[i].type == 0 && ownItems.ToList()[i].isEquip) // 현재 장착 중인 무기인가?
                            {
                                EquipSwap(i); //재귀 호출
                                break; // 루프 중간에 검색된 경우 for문 탈출
                            }
                        }
                    }
                    Player.instance.atk += ownItems.ToList()[idx].value; // 플레이어의 공격력에 장비 수치 추가
                    afkGap += ownItems.ToList()[idx].value; // 공격력 버프 수치에도 추가
                    Player.instance.plusAtkStr = $"(+{afkGap})"; // 공격력 버프 수치 문자열 할당
                    isWeaponEquip = true; // 지금 무기를 장착 중이다.
                }
                else if (ownItems.ToList()[idx].type == 1) // 갑옷도 동일
                {
                    if(isArmorEquip)
                    {
                        for (int i = 0; i < ownItems.Count; i++)
                        {
                            if (ownItems.ToList()[i].type == 1 && ownItems.ToList()[i].isEquip)
                            {
                                EquipSwap(i);
                                break;
                            }
                        }
                    }
                    Player.instance.def += ownItems.ToList()[idx].value;
                    defGap += ownItems.ToList()[idx].value;
                    Player.instance.plusDefStr = $"(+{defGap})";
                    isArmorEquip = true;
                }
                ownItems.ToList()[idx].isEquip = true; //현재 이 장비는 장착 중이다.
            }
            else // 지금 선택한 장비가 장착 중인가?
            {
                if (ownItems.ToList()[idx].type == 0) // 타입이 무기인가?
                {
                    Player.instance.atk -= ownItems.ToList()[idx].value; // 현재 플레이어 공격력 수치에서 해당 장비 수치만큼 감소
                    afkGap -= ownItems.ToList()[idx].value; // 공격력 버프 수치에서도 감소
                    if (afkGap == 0) Player.instance.plusAtkStr = ""; // 공격력 수치가 0인가? 공격력 버프 수치 문자열 초기화
                    else Player.instance.plusAtkStr = $"(+{afkGap})"; // 아닌 경우 공격력 버프 수치 문자열 할당
                    isWeaponEquip = false; // 현재 무기를 장착 중이 아니다.
                }
                else if (ownItems.ToList()[idx].type == 1) // 갑옷도 동일
                {
                    Player.instance.def -= ownItems.ToList()[idx].value;
                    defGap -= ownItems.ToList()[idx].value;
                    if (defGap == 0) Player.instance.plusDefStr = "";
                    else Player.instance.plusDefStr = $"(+{defGap})";
                    isArmorEquip = false;
                }
                ownItems.ToList()[idx].isEquip = false; // 현재 이 장비는 장착 중이 아니다.
            }
        }

        public void ShowInventoryScreen() // 인벤토리 화면을 띄워주는 메서드
        {
            while (true) // 무한 반복
            {
                UpdateInventory(); // 보유 장비 목록 업데이트
                Console.WriteLine("인벤토리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
                Console.WriteLine("[아이템 목록]");

                for (int i = 0; i < ownItems.Count; i++) // 보유 장비 갯수 만큼
                {
                    if (ownItems.ToList()[i].type == 0) typeStr = "공격력 "; // 무기인가?
                    else if (ownItems.ToList()[i].type == 1) typeStr = "방어력 "; // 갑옷인가?
                    if (ownItems.ToList()[i].isEquip == true) equipStr = "[E]"; // 장비중인가?
                    else equipStr = ""; // 장비중이 아닌가?
                    Console.WriteLine($"-{equipStr} {ownItems.ToList()[i].name}\t| {typeStr} +{ownItems.ToList()[i].value}\t| {ownItems.ToList()[i].description}\t|");
                }

                Console.WriteLine("\n1. 장비 장착 화면\n0. 메인 메뉴\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");


                ConsoleKey key = Console.ReadKey().Key; // 키 입력
                if (key == ConsoleKey.D1 || key == ConsoleKey.NumPad1) //1번 (장착 화면)
                {
                    while (true) // 무한 반복
                    {

                        Thread.Sleep(500);
                        Console.Clear();
                        Console.WriteLine("인벤토리 - 장비 장착");
                        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
                        Console.WriteLine("[아이템 목록]");

                        for (int i = 0; i < ownItems.Count; i++)
                        {
                            if (ownItems.ToList()[i].type == 0) typeStr = "공격력 ";
                            else if (ownItems.ToList()[i].type == 1) typeStr = "방어력 ";
                            if (ownItems.ToList()[i].isEquip == true) equipStr = "[E]";
                            else equipStr = "";
                            Console.WriteLine($"{i + 1}- {equipStr}{ownItems.ToList()[i].name}\t| {typeStr} +{ownItems.ToList()[i].value}\t| {ownItems.ToList()[i].description}\t|");
                        }
                        if (ownItems.Count <= 0) Console.WriteLine("\n현재 보유중인 장비가 없습니다."); // 보유 장비가 없는 경우
                        else if (ownItems.Count == 1) Console.WriteLine("\n1. 장비 장착"); // 보유 장비가 1개인 경우
                        else Console.WriteLine($"\n1. ~ {ownItems.Count}. 장비 장착 / 해제"); // 보유 장비가 2개 이상인 경우
                        Console.WriteLine("0. 인벤토리\n");
                        Console.WriteLine("원하시는 행동을 입력해주세요.");
                        Console.Write(">> ");

                        key = Console.ReadKey().Key; // 키 입력
                        if ((key >= ConsoleKey.D1 && key <= ConsoleKey.D7) || (key >= ConsoleKey.NumPad1 && key <= ConsoleKey.NumPad7))
                        {
                            int idx = (int)key % 48 - 1; // 키 입력값을 인덱스로 변환
                            if (idx >= ownItems.Count) // 키 입력값이 보유 장비 갯수를 초과하는가? (잘못 입력된 경우)
                            {
                                Console.WriteLine("\n다시 입력해 주세요");
                            }
                            else // 제대로 입력된 경우
                            {
                                EquipSwap(idx); // 장비 장착 현황 반전
                            }
                        }
                        else if (key == ConsoleKey.D0 || key == ConsoleKey.NumPad0) //0번 (인벤토리 메인 화면)
                        {
                            Thread.Sleep(500);
                            Console.Clear();
                            ShowInventoryScreen(); // 인벤토리 메인 화면 띄우기
                            break; // 무한 반복 탈출
                        }
                        else Console.WriteLine("\n다시 입력해 주세요");
                    }
                    break;
                }
                else if (key == ConsoleKey.D0 || key == ConsoleKey.NumPad0) //0번 메인 메뉴
                {
                    Thread.Sleep(500);
                    Console.Clear();
                    ShowMainScreen(); // 메인 화면 띄우기
                    break; // 무한 반복 탈출
                }
                else
                {
                    Console.WriteLine("\n다시 입력해 주세요.");
                    Thread.Sleep(500);
                    Console.Clear();
                }
            }
        }
    }
}
