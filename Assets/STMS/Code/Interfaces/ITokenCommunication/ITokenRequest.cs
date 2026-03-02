namespace STMS.Tokens.Communication.Interfaces
{
    public interface ITokenRequest<T>: ITokenCommunication
    {
        T Payload { get; }
    }
}