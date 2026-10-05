using System;
using static DragonInTheDark.Program;

namespace DragonInTheDark
{
    internal class GameSetup
    {
        public static Position GetNewRandomPosition()
        {
            Position Position = new Position();
            Random rnd = new Random();

            do
            {
                // (0,0) is rejected because this is the player's
                // starting position in a new game.
                Position.X = rnd.Next(0, MAP_HEIGHT);
                Position.Y = rnd.Next(0, MAP_WIDTH);

            } while (Position.X == 0 &&
                     Position.Y == 0);

            return Position;
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

        public static void SetPositionOfItem(
           char[,] Cavern,
           ref Position ObjectPosition,
           char Item,
           Boolean NewGame)
        {
            Position Position = new Position();

            if (NewGame && Item != '*')
            {
                do
                {
                    Position = GetNewRandomPosition();
                }
                while (Cavern[Position.X, Position.Y] != ' ');

                ObjectPosition = Position;
            }

            Cavern[ObjectPosition.X, ObjectPosition.Y] = Item;
        }

        public static void SetUpGame(
            char[,] Cavern,
            Position[] TrapPositions,
            ref Position DragonPosition,
            ref Position PlayerPosition,
            ref Position PortalStonePosition,
            ref Boolean DragonAwake,
            Boolean NewGame)
        {
            int Count;

            ResetCavern(Cavern);

            if (NewGame)
            {
                PlayerPosition.X = 0;
                PlayerPosition.Y = 0;
                DragonAwake = false;
            }

            for (Count = 0; Count < NO_OF_TRAPS; Count++)
            {
                SetPositionOfItem(Cavern, ref TrapPositions[Count], 'T', NewGame);
            }

            SetPositionOfItem(Cavern, ref DragonPosition, 'D', NewGame);
            SetPositionOfItem(Cavern, ref PortalStonePosition, 'P', NewGame);
            SetPositionOfItem(Cavern, ref PlayerPosition, '*', NewGame);
        }

        public static void SetUpTrainingGame(
            char[,] Cavern,
            Position[] TrapPositions,
            ref Position DragonPosition,
            ref Position PlayerPosition,
            ref Position PortalStonePosition,
            ref Boolean DragonAwake)
        {
            ResetCavern(Cavern);

            PlayerPosition.X = 2;
            PlayerPosition.Y = 4;

            DragonAwake = false;

            TrapPositions[0].X = 1;
            TrapPositions[0].Y = 6;

            TrapPositions[1].X = 3;
            TrapPositions[1].Y = 4;

            DragonPosition.X = 0;
            DragonPosition.Y = 3;

            PortalStonePosition.X = 4;
            PortalStonePosition.Y = 5;

            SetUpGame(
                Cavern,
                TrapPositions,
                ref DragonPosition,
                ref PlayerPosition,
                ref PortalStonePosition,
                ref DragonAwake,
                false);
        }
    }
}
