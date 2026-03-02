using STMS.Tokens.Id.Interfaces;

namespace STMS.Tokens.Communication.Interfaces
{
    public interface ITokenCommunication
    {
        ITokenId? Receipient { get; }
        ETokenCommands Command { get; }
        bool StatusResponse { get; }
    }
}