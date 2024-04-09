namespace InterviewProject.Data.Exceptions
{
    public class ObjectNotFoundException: Exception
    {
        public ObjectNotFoundException(string entityName): base($"{entityName} is not found")
        {
            
        }
    }
}

