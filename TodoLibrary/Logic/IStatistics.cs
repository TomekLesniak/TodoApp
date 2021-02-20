namespace TodoLibrary.Logic
{
    public interface IStatistics
    {
        int GetTotalAmountOfTasks();
        decimal GetCompletedPercentage();
        decimal GetFailurePercentage();
    }
}