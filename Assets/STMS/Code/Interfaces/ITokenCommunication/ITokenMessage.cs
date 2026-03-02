using STMS.Tokens.Id.Interfaces;

namespace STMS.Tokens.Communication.Interfaces
{
    public interface ITokenMessage: ITokenCommunication
    {
        ITokenId? Sender { get; }
    }
}