namespace _250319_박종원_콘솔_과제
{
    internal class Program
    {
        // 해야 할 것 맵 구현, 캐릭터 이동, 박스충돌, 콘솔 지우기, 타이틀 만들기
        // 1. 게임 준비Start, Render, Input, Update, End
        // 2. 플레이어 초기 위치 설정, 목적지 위치 설정
        // 3. 맵, 타이틀 만들기
        // 4. 박스들 위치, 부딪혔을 때, 목적지에 도착했을 때,
        // 5. 

        // 구조체
        struct Position
        {
            public int x;
            public int y;
        }

        // 게임 
        static void Main(string[] args)
        {
            bool gameOver = false;
            Position playerPos;
            char[,] map;
            Start(out playerPos, out map);

            // 게임 순서
            while (gameOver == false)
            {
                Render(playerPos, map);
                ConsoleKey key = Input();
                Update(key, ref playerPos, map, ref gameOver);
            }
            End();
        }

        static void Start(out Position playerPos, out char[,] map)
        {
            // 플레이어 시작 위치
            playerPos.x = 4;
            playerPos.y = 4;

            // 맵
            map = new char[8, 8]
        {
            { '*', '*', '*', '*', '*', '*', '*', '*' },
            { '*', '☆', '*', '☆', '★', ' ', ' ', '*' },
            { '*', '★', '*', '*', '*', '*', ' ', '*' },
            { '*', ' ', ' ', ' ', ' ', ' ', ' ', '*' },
            { '*', '*', '*', ' ', ' ', '*', '*', '*' },
            { '*', '*', '*', ' ', '*', '*', '*', '*' },
            { '*', '☆', '★', ' ', '★', ' ', '☆', '*' },
            { '*', '*', '*', '*', '*', '*', '*', '*' },
        };
            ShowTitle();
        }

        // 타이틀
        static void ShowTitle()
        {
            Console.WriteLine("-----------");
            Console.WriteLine("|소코반 게임|");
            Console.WriteLine("------------");
            Console.WriteLine("아무키나 눌러서 시작");

            Console.ReadKey();
            Console.Clear();
        }

        // Render
        static void Render(Position playerPos, char[,] map)
        {
            Console.SetCursorPosition(0, 0);
            Printmap(map);
            Printplayer(playerPos);
        }

        // 맵 구현
        static void Printmap(char[,] map)
        {
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    Console.Write(map[y, x]);
                }
                Console.WriteLine();
            }
        }
        // 플레이어 표시
        static void Printplayer(Position playerPos)
        {
            Console.SetCursorPosition(playerPos.x, playerPos.y);
            Console.Write('●');
        }

        // Input
        static ConsoleKey Input()
        {
            return Console.ReadKey(true).Key;
        }

        static void Update(ConsoleKey key, ref Position playerPos, char[,] map, ref bool gameOver)
        {
            // 클리어 조건?, 움직일 때마다 삭제 - 나중에
            Move(key, ref playerPos, map);
            bool isClear = IsClear(map);
            if (isClear)
            {
                gameOver = true;
            }
        }

        // 키 입력
        static void Move(ConsoleKey key, ref Position playerPos, char[,] map)
        {
            Position targetPos;
            Position overPos;
            switch (key)
            {
                case ConsoleKey.A:
                    targetPos.x = playerPos.x - 1;
                    targetPos.y = playerPos.y;
                    overPos.x = playerPos.x - 2;
                    overPos.y = playerPos.y;
                    break;

                case ConsoleKey.D:
                    targetPos.x = playerPos.x + 1;
                    targetPos.y = playerPos.y;
                    overPos.x = playerPos.x + 2;
                    overPos.y = playerPos.y;
                    break;

                case ConsoleKey.W:
                    targetPos.x = playerPos.x;
                    targetPos.y = playerPos.y - 1;
                    overPos.x = playerPos.x;
                    overPos.y = playerPos.y - 2;
                    break;

                case ConsoleKey.S:

                    targetPos.x = playerPos.x;
                    targetPos.y = playerPos.y + 1;
                    overPos.x = playerPos.x;
                    overPos.y = playerPos.y + 2;
                    break;
                default:
                    return;
            }
            // 벽이 있을 때
            if (map[targetPos.y, targetPos.x] == '*')
                return;

            // 박스가 있을 때
            if (map[targetPos.y, targetPos.x] == '★')
            {
                if (map[overPos.y, overPos.x] == '☆')
                {
                    map[overPos.y, overPos.x] = '◆';
                    map[targetPos.y, targetPos.x] = ' ';
                    playerPos.x = targetPos.x;
                    playerPos.y = targetPos.y;
                }
                else if (map[overPos.y, overPos.x] == ' ')
                {
                    map[overPos.y, overPos.x] = '★';
                    map[targetPos.y, targetPos.x] = ' ';
                    playerPos.x = targetPos.x;
                    playerPos.y = targetPos.y;
                }
            }
            else if (map[overPos.y, overPos.x] == '☆')
            {
                playerPos.x = targetPos.x;
                playerPos.y = targetPos.y;
            }
            else if (map[overPos.y, overPos.x] == '◆')
            {
                if (map[overPos.y, overPos.x] == '☆')
                {
                    map[overPos.y, overPos.x] = '◆';
                    map[targetPos.y, targetPos.x] = '☆';
                    playerPos.x = targetPos.x;
                    playerPos.y = targetPos.y;
                }
                else if (map[overPos.y, overPos.x] == ' ')
                {
                    map[overPos.y, overPos.x] = '★';
                    map[targetPos.y, targetPos.x] = '☆';
                    playerPos.x = targetPos.x;
                    playerPos.y = targetPos.y;
                }
            }
            else if (map[targetPos.y, targetPos.x] == ' ')
            {
                playerPos.x = targetPos.x;
                playerPos.y = targetPos.y;
            }
            else if (map[targetPos.y, targetPos.x] == '*')
            {

            }
        }

        // 클리어 조건
        static bool IsClear(char[,] map)
        {
            int goalCount = 0;
            foreach (char tile in map)
            {
                if (tile == '☆')
                {
                    goalCount++;
                    break;
                }
            }
            if (goalCount == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // End
        static void End()
        {
            Console.Clear();
            Console.WriteLine("게임을 클리어 하셨습니다. 축하합니다");
        }
    }
}
