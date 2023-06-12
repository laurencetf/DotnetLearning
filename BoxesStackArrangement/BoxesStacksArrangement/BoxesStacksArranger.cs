
namespace BoxesStackArrangement;

public static class BoxesStacksArranger
{
    private const int MaxMoves = 200;

    public static bool ArrangeBoxesStack(int clawPosition, int[] boxesStacks)
    {
        var moves = 0;
        var boxInClaw = false;
        
        var numberOfBoxesPerStack = (int) boxesStacks.Average();

        while (moves < MaxMoves && !IsArranged(boxesStacks, boxInClaw))
        {
            var move = Solve(clawPosition, boxesStacks, boxInClaw);
            switch (move)
            {
                case "PICK" when !boxInClaw:
                    boxInClaw = true;
                    boxesStacks[clawPosition]--;
                    break;
                case "PLACE" when boxInClaw:
                    boxInClaw = false;
                    boxesStacks[clawPosition]++;
                    break;
                case "RIGHT" when clawPosition < boxesStacks.Length - 1:
                    clawPosition++;
                    break;
                case "LEFT" when clawPosition > 0:
                    clawPosition--;
                    break;
            }

            moves++;
        }
        return IsArranged(boxesStacks, boxInClaw);
    }

    private static bool IsArranged(IReadOnlyList<int> boxesStacks, bool boxInClaw)
    {
        var totalBoxes = boxesStacks.Sum() + (boxInClaw ? 1 : 0);
        return !boxInClaw && totalBoxes % boxesStacks.Count == 0
            ? boxesStacks.All(stack => stack == boxesStacks[0])
            : boxesStacks.Take(totalBoxes % boxesStacks.Count).All(value => value == boxesStacks.Max());
    }

    private static string Solve(int clawPosition, IReadOnlyList<int> boxesStacks, bool boxInClaw)
    {
        var firstMinIndex = boxesStacks
            .Select((v, i) => new { Value = v, Index = i })
            .First(boxesNumber => boxesNumber.Value == boxesStacks.Min())
            .Index;
        
        var lastMaxIndex = boxesStacks
            .Select((v, i) => new { Value = v, Index = i})
            .Last( boxesNumber => boxesNumber.Value  == boxesStacks.Max())
            .Index;
        
        if(clawPosition == firstMinIndex && boxInClaw)
            return "PLACE";
        
        if(clawPosition == lastMaxIndex && !boxInClaw)
            return "PICK";


        var nextPosition = boxInClaw
            ? firstMinIndex 
            : lastMaxIndex;

        return nextPosition > clawPosition ? "RIGHT" : "LEFT";
    }
}