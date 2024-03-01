namespace Logic.TechnicalAssement.App.Exceptions
{
  public class UserFriendlyException : Exception
  {
        public UserFriendlyException(string message) : base(message)
        {

        }

        public UserFriendlyException() : base("Something has gone wrong")
        {

        }
  }
}
