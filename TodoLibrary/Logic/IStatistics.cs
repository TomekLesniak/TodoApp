namespace TodoLibrary.Logic
{
    public interface IStatistics
    {
        /// <summary>
        /// Get amount of tasks that the user created
        /// </summary>
        /// <returns>integer value of tasks amount</returns>
        int GetTotalAmountOfTasks();
        
        /// <summary>
        /// Calculates the completion of tasks
        /// </summary>
        /// <returns>Calculated decimal value expressed in percentages</returns>
        decimal GetCompletedPercentage();
        
        /// <summary>
        /// Calculates the failure of tasks
        /// </summary>
        /// <returns>Calculated decimal value expressed in percentages</returns>
        decimal GetFailurePercentage();
    }
}