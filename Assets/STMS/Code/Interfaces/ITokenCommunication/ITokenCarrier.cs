namespace STMS.Tokens.Communication.Interfaces
{
    public interface ITokenCarrier<T>: ITokenMessage, ITokenRequest<T>
    {
        
    }
}