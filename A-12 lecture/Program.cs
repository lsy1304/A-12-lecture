namespace A_12_lecture
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player().SetInstance(); // Player 싱글톤
            player.Init(); // Player 필드 초기화
            ItemList itemList = new ItemList(); //ItemList 객체 생성
            Inventory inventory = new Inventory().SetInstance(); //Inventory 싱글톤
            player.ShowMainScreen();

            while (true)
            {
                ConsoleKey key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Thread.Sleep(500);
                        Console.Clear();
                        player.ShowStatusScreen();
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Thread.Sleep(500);
                        Console.Clear();
                        inventory.ShowInventoryScreen();
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
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

    public class MainScreen
    {
        public void ShowMainScreen()
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
    public class Player : MainScreen
    {
        public static Player instance;

        public int level;
        public string name = "";
        public string job = "";
        public int atk;
        public int def;
        public int hp;
        public int gold;
        public string plusAtkStr = "";
        public string plusDefStr = "";

        public void Init()
        {
            level = 1;
            name = "Chad";
            job = "전사";
            atk = 10;
            def = 5;
            hp = 100;
            gold = 20000;
        }
        public Player SetInstance()
        {
            if (instance == null)
            {
                instance = new Player();
            }
            return instance;
        }

        public void ShowStatusScreen()
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

                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.D0 || key == ConsoleKey.NumPad0)
                {
                    Thread.Sleep(500);
                    Console.Clear();
                    ShowMainScreen();
                    break;
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

    public class ItemList : Item
    {
        public static List<Item> items = new List<Item>();

        public ItemList()
        {
            if (items.Count == 0)
            {
                
                for (idx = 0; idx < 7; idx++)
                {
                    Item item = SetItem(idx);
                    items.Add(item);
                }
            }
        }

        public void PayPrice(int idx)
        {

            if (!items[idx].isBuy)
            {
                Player.instance.gold -= items[idx].cost;
                items[idx].isBuy = true;
            }
        }

        public void ShowShopScreen()
        {
            string type = "";
            string alreadyBought = "";
            string equipStr = "";

            while (true)
            {
                Inventory.instance.UpdateInventory();
                Console.WriteLine("상점");
                Console.WriteLine();
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{Player.instance.gold} G\n");
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].type == 0) type = "공격력 ";
                    else if (items[i].type == 1) type = "방어력 ";
                    if (items[i].isBuy == true)
                        Console.WriteLine($"- {items[i].name}\t| {type} +{items[i].value}\t| {items[i].description}\t| 구매 완료");
                    else
                        Console.WriteLine($"- {items[i].name}\t| {type} +{items[i].value}\t| {items[i].description}\t| {items[i].cost} G");
                }
                Console.WriteLine("\n0. 메인 메뉴\n1. 아이템 구매\n2. 아이템 판매");
                Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");

                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.D1 || key == ConsoleKey.NumPad1)
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
                        Console.WriteLine("\n" + alreadyBought);
                        Console.WriteLine($"1. ~ {items.Count} : 아이템 구매, 0. 상점 메뉴\n");
                        Console.Write("구매할 아이템을 선택해 주세요\n>> ");

                        key = Console.ReadKey().Key;
                        if ((key >= ConsoleKey.D1 && key <= ConsoleKey.D7) || (key >= ConsoleKey.NumPad1 && key <= ConsoleKey.NumPad7))
                        {
                            int idx = (int)key % 48 - 1;
                            if (items[idx].isBuy) alreadyBought = "이미 구매한 상품입니다.";
                            else if (Player.instance.gold < items[idx].cost) alreadyBought = "잔액이 부족합니다.";
                            else
                            {
                                alreadyBought = "구매해 주셔서 감사합니다.";
                                PayPrice(idx);
                            }
                        }
                        else if (key == ConsoleKey.D0 || key == ConsoleKey.NumPad0)
                        {
                            Thread.Sleep(500);
                            Console.Clear();
                            ShowShopScreen();
                            break;
                        }
                        else Console.WriteLine("\n다시 입력해 주세요");
                    }
                    break;
                }
                else if (key == ConsoleKey.D2 || key == ConsoleKey.NumPad2)
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

                        if (Inventory.instance.ownItems.Count <= 0)
                        {
                            Console.WriteLine("보유 중인 장비가 없습니다.");
                            Console.Write("0. 상점 메뉴\n>> ");
                        }
                        else
                        {
                            for (int i = 0; i < Inventory.instance.ownItems.ToList().Count; i++)
                            {
                                if (Inventory.instance.ownItems.ToList()[i].type == 0) type = "공격력 ";
                                else if (Inventory.instance.ownItems.ToList()[i].type == 1) type = "방어력 ";
                                if (Inventory.instance.ownItems.ToList()[i].isEquip == true) equipStr = "[E]";
                                else equipStr = "";
                                Console.WriteLine($"{i + 1}.-{equipStr} {Inventory.instance.ownItems.ToList()[i].name}\t| {type} +{Inventory.instance.ownItems.ToList()[i].value}\t| {Inventory.instance.ownItems.ToList()[i].description}\t| {Inventory.instance.ownItems.ToList()[i].cost * 0.85f} G");
                            }
                            if (Inventory.instance.ownItems.Count == 1) Console.Write("\n1. 아이템 판매. ");
                            else Console.Write($"\n1. ~ {Inventory.instance.ownItems.Count}. 아이템 판매. ");
                            Console.WriteLine("0. 상점 메뉴");
                            Console.Write(">> ");
                        }

                        key = Console.ReadKey().Key;
                        if((key >= ConsoleKey.D1 && key <= ConsoleKey.D7) || (key >= ConsoleKey.NumPad1 && key <= ConsoleKey.NumPad7))
                        {
                            int idx = (int)key % 48 - 1;
                            if(idx >= Inventory.instance.ownItems.Count)
                            {
                                Console.WriteLine("\n다시 입력해 주세요.");
                            }
                            else
                            {
                                Item sellItem = Inventory.instance.ownItems.ToList()[idx];
                                Player.instance.gold += (int)(sellItem.cost * 0.85f);
                                sellItem.isBuy = false;
                                if (sellItem.isEquip) Inventory.instance.EquipSwap(idx);
                                Inventory.instance.ownItems.Remove(sellItem);
                            }
                        }
                        else if (key == ConsoleKey.D0 || key == ConsoleKey.NumPad0)
                        {
                            Thread.Sleep(500);
                            Console.Clear();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\n다시 입력해 주세요.");
                        }
                    }
                }
                else if (key == ConsoleKey.D0 || key == ConsoleKey.NumPad0)
                {
                    Thread.Sleep(500);
                    Console.Clear();
                    ShowMainScreen();
                    break;
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

    public class Item : MainScreen
    {
        public int idx;
        public string name = "";
        public int type; // 0 : 갑옷 1 : 무기
        public int value;
        public string description = "";
        public int cost;
        public bool isBuy = false;
        public bool isEquip = false;

        public Item SetItem(int idx)
        {
            Item item = new Item();
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
            return item;
        }
    }
    public class Inventory : ItemList
    {
        public static Inventory instance;

        public int afkGap = 0;
        public int defGap = 0;
        public string typeStr = "";
        public string equipStr = "";
        public bool isWeaponEquip = false;
        public bool isArmorEquip = false;
        public HashSet<Item> ownItems = new HashSet<Item>();


        public Inventory SetInstance()
        {
            if (instance == null)
            {
                instance = new Inventory();
            }
            return instance;
        }
        public void UpdateInventory()
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].isBuy == true) ownItems.Add(items[i]);
            }
        }

        public void EquipSwap(int idx)
        {
            if (ownItems.ToList()[idx].isEquip == false)
            {
                if (ownItems.ToList()[idx].type == 0)
                {
                    if(isWeaponEquip)
                    {
                        for(int i = 0; i<ownItems.Count; i++)
                        {
                            if(ownItems.ToList()[i].type == 0 && ownItems.ToList()[i].isEquip)
                            {
                                EquipSwap(i);
                                break;
                            }
                        }
                    }
                    Player.instance.atk += ownItems.ToList()[idx].value;
                    afkGap += ownItems.ToList()[idx].value;
                    Player.instance.plusAtkStr = $"(+{afkGap})";
                    isWeaponEquip = true;
                }
                else if (ownItems.ToList()[idx].type == 1)
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
                ownItems.ToList()[idx].isEquip = true;
            }
            else
            {
                if (ownItems.ToList()[idx].type == 0)
                {
                    Player.instance.atk -= ownItems.ToList()[idx].value;
                    afkGap -= ownItems.ToList()[idx].value;
                    if (afkGap == 0) Player.instance.plusAtkStr = "";
                    else Player.instance.plusAtkStr = $"(+{afkGap})";
                    isWeaponEquip = false;
                }
                else if (ownItems.ToList()[idx].type == 1)
                {
                    Player.instance.def -= ownItems.ToList()[idx].value;
                    defGap -= ownItems.ToList()[idx].value;
                    if (defGap == 0) Player.instance.plusDefStr = "";
                    else Player.instance.plusDefStr = $"(+{defGap})";
                    isArmorEquip = false;
                }
                ownItems.ToList()[idx].isEquip = false;
            }
        }

        public void ShowInventoryScreen()
        {
            while (true)
            {
                UpdateInventory();
                Console.WriteLine("인벤토리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
                Console.WriteLine("[아이템 목록]");

                for (int i = 0; i < ownItems.Count; i++)
                {
                    if (ownItems.ToList()[i].type == 0) typeStr = "공격력 ";
                    else if (ownItems.ToList()[i].type == 1) typeStr = "방어력 ";
                    if (ownItems.ToList()[i].isEquip == true) equipStr = "[E]";
                    else equipStr = "";
                    Console.WriteLine($"-{equipStr} {ownItems.ToList()[i].name}\t| {typeStr} +{ownItems.ToList()[i].value}\t| {ownItems.ToList()[i].description}\t|");
                }

                Console.WriteLine("\n1. 장비 장착 화면\n0. 메인 메뉴\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");


                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.D1 || key == ConsoleKey.NumPad1)
                {
                    while (true)
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
                        if (ownItems.Count <= 0) Console.WriteLine("\n현재 보유중인 장비가 없습니다.");
                        else if (ownItems.Count == 1) Console.WriteLine("\n1. 장비 장착");
                        else Console.WriteLine($"\n1. ~ {ownItems.Count}. 장비 장착 / 해제");
                        Console.WriteLine("0. 인벤토리\n");
                        Console.WriteLine("원하시는 행동을 입력해주세요.");
                        Console.Write(">> ");

                        key = Console.ReadKey().Key;
                        if ((key >= ConsoleKey.D1 && key <= ConsoleKey.D7) || (key >= ConsoleKey.NumPad1 && key <= ConsoleKey.NumPad7))
                        {
                            int idx = (int)key % 48 - 1;
                            if (idx >= ownItems.Count)
                            {
                                Console.WriteLine("\n다시 입력해 주세요");
                            }
                            else
                            {
                                EquipSwap(idx);
                            }
                        }
                        else if (key == ConsoleKey.D0 || key == ConsoleKey.NumPad0)
                        {
                            Thread.Sleep(500);
                            Console.Clear();
                            ShowInventoryScreen();
                            break;
                        }
                        else Console.WriteLine("\n다시 입력해 주세요");
                    }
                    break;
                }
                else if (key == ConsoleKey.D0 || key == ConsoleKey.NumPad0)
                {
                    Thread.Sleep(500);
                    Console.Clear();
                    ShowMainScreen();
                    break;
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
