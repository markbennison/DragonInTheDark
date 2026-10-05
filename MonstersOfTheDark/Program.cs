using System;
using System.IO;

namespace DragonInTheDark
{
    class Program
    {
        public const int NO_OF_TRAPS = 2;
        public const int MAP_HEIGHT = 5;
        public const int MAP_WIDTH = 7;

        public struct CellReference
        {
            public int GridCellsVertical;
            public int GridCellsHorizontal;
        }

        static void Main(string[] args)
        {
            Console.Title = "Dragon in the Dark";

            char[,] Cavern = new char[MAP_HEIGHT, MAP_WIDTH];
            int Choice = 0;
            Boolean DragonAwake = false;

            CellReference PortalStonePosition = new CellReference();
            CellReference DragonPosition = new CellReference();
            CellReference PlayerPosition = new CellReference();
            CellReference[] TrapPositions = new CellReference[NO_OF_TRAPS];

            while (Choice != 9)
            {
                DisplayMenu();
                Choice = GetMainMenuChoice();

                switch (Choice)
                {
                    case 1:
                        SetUpTrainingGame(
                            Cavern,
                            TrapPositions,
                            ref DragonPosition,
                            ref PlayerPosition,
                            ref PortalStonePosition,
                            ref DragonAwake);

                        PlayGame(
                            Cavern,
                            TrapPositions,
                            ref DragonPosition,
                            ref PlayerPosition,
                            ref PortalStonePosition,
                            ref DragonAwake);
                        break;

                    case 2:
                        SetUpGame(
                            Cavern,
                            TrapPositions,
                            ref DragonPosition,
                            ref PlayerPosition,
                            ref PortalStonePosition,
                            ref DragonAwake,
                            true);

                        PlayGame(
                            Cavern,
                            TrapPositions,
                            ref DragonPosition,
                            ref PlayerPosition,
                            ref PortalStonePosition,
                            ref DragonAwake);
                        break;

                    case 3:
                        LoadGame(
                            TrapPositions,
                            ref DragonPosition,
                            ref PlayerPosition,
                            ref PortalStonePosition,
                            ref DragonAwake);

                        SetUpGame(
                            Cavern,
                            TrapPositions,
                            ref DragonPosition,
                            ref PlayerPosition,
                            ref PortalStonePosition,
                            ref DragonAwake,
                            false);

                        PlayGame(
                            Cavern,
                            TrapPositions,
                            ref DragonPosition,
                            ref PlayerPosition,
                            ref PortalStonePosition,
                            ref DragonAwake);
                        break;

                    case 4:
                        SaveGame(
                            TrapPositions,
                            DragonPosition,
                            PlayerPosition,
                            PortalStonePosition,
                            DragonAwake);
                        break;
                }
            }
        }

        public static void DisplayMenu()
        {
            Console.WriteLine("DRAGON IN THE DARK");
            Console.WriteLine();
            Console.WriteLine("MAIN MENU");
            Console.WriteLine();
            Console.WriteLine("1. Training Game");
            Console.WriteLine("2. New Game");
            Console.WriteLine("3. Load Game");
            Console.WriteLine("4. Save Game");
            Console.WriteLine("9. Quit");
            Console.WriteLine();
            Console.Write("Please enter your choice: ");
        }

        public static int GetMainMenuChoice()
        {
            int Choice;

            Choice = int.Parse(Console.ReadLine());
            Console.WriteLine();

            return Choice;
        }

        public static void ResetCavern(char[,] Cavern)
        {
            int Count1;
            int Count2;

            for (Count1 = 0; Count1 < MAP_HEIGHT; Count1++)
            {
                for (Count2 = 0; Count2 < MAP_WIDTH; Count2++)
                {
                    Cavern[Count1, Count2] = ' ';
                }
            }
        }

        public static CellReference GetNewRandomPosition()
        {
            CellReference Position = new CellReference();
            Random rnd = new Random();

            do
            {
                // (0,0) is rejected because this is the player's
                // starting position in a new game.
                Position.GridCellsVertical = rnd.Next(0, MAP_HEIGHT);
                Position.GridCellsHorizontal = rnd.Next(0, MAP_WIDTH);

            } while (Position.GridCellsVertical == 0 &&
                     Position.GridCellsHorizontal == 0);

            return Position;
        }

        public static void SetPositionOfItem(
            char[,] Cavern,
            CellReference ObjectPosition,
            char Item,
            Boolean NewGame)
        {
            CellReference Position = new CellReference();

            if (NewGame && Item != '*')
            {
                do
                {
                    Position = GetNewRandomPosition();

                } while (Cavern[
                    Position.GridCellsVertical,
                    Position.GridCellsHorizontal] != ' ');

                ObjectPosition = Position;
            }

            Cavern[
                ObjectPosition.GridCellsVertical,
                ObjectPosition.GridCellsHorizontal] = Item;
        }

        public static void SetUpGame(
            char[,] Cavern,
            CellReference[] TrapPositions,
            ref CellReference DragonPosition,
            ref CellReference PlayerPosition,
            ref CellReference PortalStonePosition,
            ref Boolean DragonAwake,
            Boolean NewGame)
        {
            int Count;

            ResetCavern(Cavern);

            if (NewGame)
            {
                PlayerPosition.GridCellsVertical = 0;
                PlayerPosition.GridCellsHorizontal = 0;
                DragonAwake = false;
            }

            for (Count = 0; Count < NO_OF_TRAPS; Count++)
            {
                SetPositionOfItem(
                    Cavern,
                    TrapPositions[Count],
                    'T',
                    NewGame);
            }

            SetPositionOfItem(
                Cavern,
                DragonPosition,
                'D',
                NewGame);

            SetPositionOfItem(
                Cavern,
                PortalStonePosition,
                'P',
                NewGame);

            SetPositionOfItem(
                Cavern,
                PlayerPosition,
                '*',
                NewGame);
        }

        public static void SetUpTrainingGame(
            char[,] Cavern,
            CellReference[] TrapPositions,
            ref CellReference DragonPosition,
            ref CellReference PlayerPosition,
            ref CellReference PortalStonePosition,
            ref Boolean DragonAwake)
        {
            ResetCavern(Cavern);

            PlayerPosition.GridCellsVertical = 2;
            PlayerPosition.GridCellsHorizontal = 4;

            DragonAwake = false;

            TrapPositions[0].GridCellsVertical = 1;
            TrapPositions[0].GridCellsHorizontal = 6;

            TrapPositions[1].GridCellsVertical = 3;
            TrapPositions[1].GridCellsHorizontal = 4;

            DragonPosition.GridCellsVertical = 0;
            DragonPosition.GridCellsHorizontal = 3;

            PortalStonePosition.GridCellsVertical = 4;
            PortalStonePosition.GridCellsHorizontal = 5;

            SetUpGame(
                Cavern,
                TrapPositions,
                ref DragonPosition,
                ref PlayerPosition,
                ref PortalStonePosition,
                ref DragonAwake,
                false);
        }

        public static void LoadGame(
            CellReference[] TrapPositions,
            ref CellReference DragonPosition,
            ref CellReference PlayerPosition,
            ref CellReference PortalStonePosition,
            ref Boolean DragonAwake)
        {
            string Filename;
            BinaryReader br;

            Console.Write("Enter the name of the file to load: ");
            Filename = Console.ReadLine();
            Console.WriteLine();

            br = new BinaryReader(
                new FileStream(Filename, FileMode.Open));

            TrapPositions[0].GridCellsVertical = br.ReadInt32();
            TrapPositions[0].GridCellsHorizontal = br.ReadInt32();

            TrapPositions[1].GridCellsVertical = br.ReadInt32();
            TrapPositions[1].GridCellsHorizontal = br.ReadInt32();

            DragonPosition.GridCellsVertical = br.ReadInt32();
            DragonPosition.GridCellsHorizontal = br.ReadInt32();

            PlayerPosition.GridCellsVertical = br.ReadInt32();
            PlayerPosition.GridCellsHorizontal = br.ReadInt32();

            PortalStonePosition.GridCellsVertical = br.ReadInt32();
            PortalStonePosition.GridCellsHorizontal = br.ReadInt32();

            DragonAwake = br.ReadBoolean();

            br.Close();
        }

        public static void SaveGame(
            CellReference[] TrapPositions,
            CellReference DragonPosition,
            CellReference PlayerPosition,
            CellReference PortalStonePosition,
            Boolean DragonAwake)
        {
            string Filename;
            BinaryWriter bw;

            Console.Write("Enter new file name: ");
            Filename = Console.ReadLine();
            Console.WriteLine();

            bw = new BinaryWriter(
                new FileStream(Filename, FileMode.Create));

            bw.Write(TrapPositions[0].GridCellsVertical);
            bw.Write(TrapPositions[0].GridCellsHorizontal);

            bw.Write(TrapPositions[1].GridCellsVertical);
            bw.Write(TrapPositions[1].GridCellsHorizontal);

            bw.Write(DragonPosition.GridCellsVertical);
            bw.Write(DragonPosition.GridCellsHorizontal);

            bw.Write(PlayerPosition.GridCellsVertical);
            bw.Write(PlayerPosition.GridCellsHorizontal);

            bw.Write(PortalStonePosition.GridCellsVertical);
            bw.Write(PortalStonePosition.GridCellsHorizontal);

            bw.Write(DragonAwake);

            bw.Close();
        }

        public static void DisplayCavern(
            char[,] Cavern,
            Boolean DragonAwake)
        {
            int Count1;
            int Count2;

            for (Count1 = 0; Count1 < MAP_HEIGHT; Count1++)
            {
                Console.WriteLine(
                    "  --------------------------- ");

                for (Count2 = 0; Count2 < MAP_WIDTH; Count2++)
                {
                    if (
                        Cavern[Count1, Count2] == ' ' ||
                        Cavern[Count1, Count2] == '*' ||
                        (Cavern[Count1, Count2] == 'D' &&
                         DragonAwake))
                    {
                        Console.Write(
                            " | " + Cavern[Count1, Count2]);
                    }
                    else
                    {
                        // Traps and the Portal Stone remain hidden.
                        Console.Write(" |  ");
                    }
                }

                Console.WriteLine(" | ");
            }

            Console.WriteLine(
                "  --------------------------- ");
            Console.WriteLine();
        }

        public static void DisplayMoveOptions()
        {
            Console.WriteLine();
            Console.WriteLine("Enter W to move UP");
            Console.WriteLine("Enter S to move DOWN");
            Console.WriteLine("Enter A to move LEFT");
            Console.WriteLine("Enter D to move RIGHT");
            Console.WriteLine("Enter M to return to the Main Menu");
            Console.WriteLine();
        }

        public static char GetMove()
        {
            char Move;

            Move = char.Parse(Console.ReadLine());
            Console.WriteLine();

            return Move;
        }

        public static void MakeMove(
            char[,] Cavern,
            char Direction,
            ref CellReference PlayerPosition)
        {
            Cavern[
                PlayerPosition.GridCellsVertical,
                PlayerPosition.GridCellsHorizontal] = ' ';

            switch (Direction)
            {
                case 'W':
                    PlayerPosition.GridCellsVertical =
                        PlayerPosition.GridCellsVertical - 1;
                    break;

                case 'S':
                    PlayerPosition.GridCellsVertical =
                        PlayerPosition.GridCellsVertical + 1;
                    break;

                case 'A':
                    PlayerPosition.GridCellsHorizontal =
                        PlayerPosition.GridCellsHorizontal - 1;
                    break;

                case 'D':
                    PlayerPosition.GridCellsHorizontal =
                        PlayerPosition.GridCellsHorizontal + 1;
                    break;
            }

            Cavern[
                PlayerPosition.GridCellsVertical,
                PlayerPosition.GridCellsHorizontal] = '*';
        }

        public static Boolean CheckValidMove(
            CellReference PlayerPosition,
            char Direction)
        {
            Boolean ValidMove;

            ValidMove = true;

            if (!(Direction == 'W' ||
                  Direction == 'S' ||
                  Direction == 'A' ||
                  Direction == 'D' ||
                  Direction == 'M'))
            {
                ValidMove = false;
            }

            switch (Direction)
            {
                case 'W':
                    if (PlayerPosition.GridCellsVertical == 0)
                    {
                        ValidMove = false;
                    }
                    break;

                case 'S':
                    if (PlayerPosition.GridCellsVertical ==
                        MAP_HEIGHT - 1)
                    {
                        ValidMove = false;
                    }
                    break;

                case 'A':
                    if (PlayerPosition.GridCellsHorizontal == 0)
                    {
                        ValidMove = false;
                    }
                    break;

                case 'D':
                    if (PlayerPosition.GridCellsHorizontal ==
                        MAP_WIDTH - 1)
                    {
                        ValidMove = false;
                    }
                    break;
            }

            return ValidMove;
        }

        public static Boolean CheckIfSameCell(
            CellReference FirstCellPosition,
            CellReference SecondCellPosition)
        {
            Boolean InSameCell = false;

            if (
                FirstCellPosition.GridCellsVertical ==
                    SecondCellPosition.GridCellsVertical &&
                FirstCellPosition.GridCellsHorizontal ==
                    SecondCellPosition.GridCellsHorizontal)
            {
                InSameCell = true;
            }

            return InSameCell;
        }

        public static void DisplayWonGameMessage()
        {
            Console.WriteLine();
            Console.WriteLine(
                "You have found the legendary Portal Stone!");
            Console.WriteLine(
                "Its ancient magic flares to life and you escape the dungeon.");
            Console.WriteLine();
            Console.WriteLine(
                "Congratulations - you survived DRAGON IN THE DARK!");
            Console.WriteLine();
        }

        public static void DisplayTrapMessage()
        {
            Console.WriteLine();
            Console.WriteLine(
                "CLICK! You have triggered an ancient trap!");
            Console.WriteLine();
            Console.WriteLine(
                "A pair of glowing eyes opens somewhere in the darkness...");
            Console.WriteLine(
                "The dragon is awake - and its blindsight means it knows exactly where you are!");
            Console.WriteLine();
            Console.WriteLine("RUN!");
            Console.WriteLine();
        }

        public static void MovePortalStone(
            char[,] Cavern,
            CellReference NewCellForPortalStone,
            ref CellReference PortalStonePosition)
        {
            Cavern[
                NewCellForPortalStone.GridCellsVertical,
                NewCellForPortalStone.GridCellsHorizontal] = 'P';

            Cavern[
                PortalStonePosition.GridCellsVertical,
                PortalStonePosition.GridCellsHorizontal] = ' ';

            PortalStonePosition = NewCellForPortalStone;
        }

        public static void MakeDragonMove(
            char[,] Cavern,
            ref CellReference DragonPosition,
            ref CellReference PortalStonePosition,
            CellReference PlayerPosition)
        {
            CellReference OriginalDragonPosition =
                new CellReference();

            Boolean DragonMovedToSameCellAsPortalStone = false;

            OriginalDragonPosition.GridCellsVertical =
                DragonPosition.GridCellsVertical;

            OriginalDragonPosition.GridCellsHorizontal =
                DragonPosition.GridCellsHorizontal;

            Cavern[
                DragonPosition.GridCellsVertical,
                DragonPosition.GridCellsHorizontal] = ' ';

            // The dragon has blindsight and always knows
            // where the player is.
            if (DragonPosition.GridCellsVertical <
                PlayerPosition.GridCellsVertical)
            {
                DragonPosition.GridCellsVertical =
                    DragonPosition.GridCellsVertical + 1;
            }
            else if (DragonPosition.GridCellsVertical >
                     PlayerPosition.GridCellsVertical)
            {
                DragonPosition.GridCellsVertical =
                    DragonPosition.GridCellsVertical - 1;
            }
            else if (DragonPosition.GridCellsHorizontal <
                     PlayerPosition.GridCellsHorizontal)
            {
                DragonPosition.GridCellsHorizontal =
                    DragonPosition.GridCellsHorizontal + 1;
            }
            else if (DragonPosition.GridCellsHorizontal >
                     PlayerPosition.GridCellsHorizontal)
            {
                DragonPosition.GridCellsHorizontal =
                    DragonPosition.GridCellsHorizontal - 1;
            }

            DragonMovedToSameCellAsPortalStone =
                CheckIfSameCell(
                    DragonPosition,
                    PortalStonePosition);

            if (DragonMovedToSameCellAsPortalStone)
            {
                // The dragon kicks the Portal Stone into
                // the cell it has just moved from.
                MovePortalStone(
                    Cavern,
                    OriginalDragonPosition,
                    ref PortalStonePosition);
            }

            Cavern[
                DragonPosition.GridCellsVertical,
                DragonPosition.GridCellsHorizontal] = 'D';
        }

        public static void DisplayLostGameMessage()
        {
            Console.WriteLine();
            Console.WriteLine(
                "ARGHHHHHH! The dragon has found you!");
            Console.WriteLine(
                "Unfortunately, adventurers are a dragon's favourite snack.");
            Console.WriteLine();
            Console.WriteLine("GAME OVER.");
            Console.WriteLine(
                "Perhaps you'll have better luck next time in DRAGON IN THE DARK!");
            Console.WriteLine();
        }

        public static void PlayGame(
            char[,] Cavern,
            CellReference[] TrapPositions,
            ref CellReference DragonPosition,
            ref CellReference PlayerPosition,
            ref CellReference PortalStonePosition,
            ref Boolean DragonAwake)
        {
            Boolean Eaten = false;
            Boolean PortalStoneFound = false;
            Boolean ValidMove = false;

            int Count = 0;
            char MoveDirection = ' ';

            DisplayCavern(Cavern, DragonAwake);

            while (!(Eaten ||
                     PortalStoneFound ||
                     MoveDirection == 'M'))
            {
                ValidMove = false;

                while (!ValidMove)
                {
                    DisplayMoveOptions();
                    MoveDirection = GetMove();

                    ValidMove =
                        CheckValidMove(
                            PlayerPosition,
                            MoveDirection);
                }

                if (MoveDirection != 'M')
                {
                    MakeMove(
                        Cavern,
                        MoveDirection,
                        ref PlayerPosition);

                    DisplayCavern(
                        Cavern,
                        DragonAwake);

                    PortalStoneFound =
                        CheckIfSameCell(
                            PlayerPosition,
                            PortalStonePosition);

                    if (PortalStoneFound)
                    {
                        DisplayWonGameMessage();
                    }

                    Eaten =
                        CheckIfSameCell(
                            DragonPosition,
                            PlayerPosition);

                    if (!DragonAwake &&
                        !PortalStoneFound &&
                        !Eaten)
                    {
                        DragonAwake =
                            CheckIfSameCell(
                                PlayerPosition,
                                TrapPositions[0]);

                        if (!DragonAwake)
                        {
                            DragonAwake =
                                CheckIfSameCell(
                                    PlayerPosition,
                                    TrapPositions[1]);
                        }

                        if (DragonAwake)
                        {
                            DisplayTrapMessage();

                            DisplayCavern(
                                Cavern,
                                DragonAwake);
                        }
                    }

                    if (DragonAwake &&
                        !Eaten &&
                        !PortalStoneFound)
                    {
                        Count = 0;

                        // The dragon moves twice for every
                        // one move made by the player.
                        while (Count < 2 && !Eaten)
                        {
                            MakeDragonMove(
                                Cavern,
                                ref DragonPosition,
                                ref PortalStonePosition,
                                PlayerPosition);

                            Eaten =
                                CheckIfSameCell(
                                    DragonPosition,
                                    PlayerPosition);

                            Console.WriteLine();
                            Console.WriteLine(
                                "The dragon moves through the darkness...");
                            Console.WriteLine(
                                "Press Enter to continue");

                            Console.ReadLine();

                            DisplayCavern(
                                Cavern,
                                DragonAwake);

                            Count = Count + 1;
                        }
                    }

                    if (Eaten)
                    {
                        DisplayLostGameMessage();
                    }
                }
            }
        }
    }
}