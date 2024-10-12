namespace PZCheeseriaWebAPI.Helpers;

public class ExceptionHelper : Exception
{
    public ExceptionHelper(string message) : base(message) { }
}
