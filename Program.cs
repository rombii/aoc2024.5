using aoc2024._5;

using var input1Reader = new StreamReader(Path.Join(Directory.GetCurrentDirectory(), "input1.txt"));
    var mapNumberToObject = new Dictionary<int, Page>(); //Map to find an object by a number

while (!input1Reader.EndOfStream) //Add all rules to proper sets
{
    var rule = await input1Reader.ReadLineAsync();
    ArgumentNullException.ThrowIfNull(rule);
    var pages = rule.Split('|').Select(int.Parse).ToArray();
    if (!mapNumberToObject.TryGetValue(pages[0], out var page1))
    {
        page1 = new Page();
        mapNumberToObject[pages[0]] = page1;
    }
    page1.AddNewAfterRule(pages[1]);
    
    
    if (!mapNumberToObject.TryGetValue(pages[1], out var page2))
    {
        page2 = new Page();
        mapNumberToObject[pages[1]] = new Page();
    }
    page2.AddNewBeforeRule(pages[0]);
    
}

using var input2Reader = new StreamReader(Path.Join(Directory.GetCurrentDirectory(), "input2.txt"));
int part1Answer = 0, part2Answer = 0;
while (!input2Reader.EndOfStream)
{
    var rightOrderFlag = true;
    var order = (await input2Reader.ReadLineAsync())!.Split(',').Select(int.Parse).ToArray();

    for (var i = 0; i < order.Length && rightOrderFlag; i++) //Check if order is correct
    {
        for (var l = 0; l < i; l++)
        {
            if (mapNumberToObject.TryGetValue(order[i], out var value) && value.ShouldBeAfter(order[l]))
            {
                rightOrderFlag = false;
            }
        }
        
        for (var r = i + 1; r < order.Length && rightOrderFlag; r++)
        {
            if (mapNumberToObject.TryGetValue(order[i], out var value) && value.ShouldBeBefore(order[r]))
            {
                rightOrderFlag = false;
            }
        }

    }
    if (rightOrderFlag)
    {
        part1Answer += order[order.Length / 2];
    }
    else
    {
        for (var i = 0; i < order.Length; i++) //Fix order
        {
            for (var j = i + 1; j < order.Length; j++)
            {
                if (mapNumberToObject.TryGetValue(order[i], out var page1) && page1.ShouldBeAfter(order[j]))
                {
                    (order[i], order[j]) = (order[j], order[i]);
                }
            }
        }
        part2Answer += order[order.Length / 2];

    }
}

Console.WriteLine($"First part: {part1Answer}");
Console.WriteLine($"Second part: {part2Answer}");