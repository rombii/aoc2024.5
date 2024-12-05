namespace aoc2024._5;

internal class Page //Class that represents page with associated rules
{
    private readonly HashSet<int> _numbersBefore = [];
    private readonly HashSet<int> _numbersAfter = [];

    public void AddNewBeforeRule(int number)
    {
        _numbersBefore.Add(number);
    }
    
    public void AddNewAfterRule(int number)
    {
        _numbersAfter.Add(number);
    }

    public bool ShouldBeAfter(int number)
    {
        return _numbersAfter.Contains(number);
    }

    public bool ShouldBeBefore(int number)
    {
        return _numbersBefore.Contains(number);
    }
    
}