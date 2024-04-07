namespace InterviewProject.Services.Interfaces
{
    public interface ISecurityService
    {
        /// <summary>
        /// Hashes specified string.
        /// </summary>
        /// <param name="value">The target string.</param>
        string GetHashedValue(string value);
    }
}

