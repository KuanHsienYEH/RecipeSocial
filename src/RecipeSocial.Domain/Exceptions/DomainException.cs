namespace RecipeSocial.Domain.Exceptions;

public class DomainException : Exception{
    public DomainException(string msg) : base(msg){}
}