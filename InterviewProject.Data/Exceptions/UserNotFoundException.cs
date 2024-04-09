namespace InterviewProject.Data.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("User is not found")
        {
            
        }

        public UserNotFoundException(string message) : base(message)
        {
            
        }
    }
}

