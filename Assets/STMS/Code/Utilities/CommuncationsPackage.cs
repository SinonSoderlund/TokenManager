using STMS.Tokens.Communication.Interfaces;
using STMS.Tokens.Id.Utilty;

public class CommuncationsPackage
{
    public ITokenCommunication Incomming;
    public ITokenMessage Outgoing;
    public TokenIdMatchStatus MatchStatus;
    public CommuncationsPackage(ITokenCommunication _incomming, TokenIdMatchStatus _matchStatus = TokenIdMatchStatus.None)
    {
        Incomming = _incomming;
        MatchStatus = _matchStatus;
    }
}
