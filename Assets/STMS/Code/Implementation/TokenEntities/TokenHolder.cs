using System;
using STMS.Tokens.Communication.Interfaces;
using STMS.Tokens.TokenEntities.Contracts;
using STMS.Tokens.TokenEntities.Implementation;
using STMS.Tokens.TokenEntities.Interfaces;
using STMS.Tokens.Communication;
using STMS.Tokens.Id.Interfaces;
using STMS.Tokens.TokenEntities.Communication;

public class TokenHolder : BTokenChild
{

    public TokenHolder(ITokenId _id, ChildMessage _parentMessanger) : base(_id, _parentMessanger){}

    protected override ITokenMessage AddManagedGroup(ITokenCarrier<ITokenChild> _newGroup)
    {
        throw new InvalidOperationException("should never happen");
    }

    protected override ITokenCarrier<CTokenChild> ConvertToManagedGroup(ITokenCommunication _message)
    {
        throw new InvalidOperationException("should never happen");
    }

    protected override ITokenCarrier<CTokenChild> ConvertToStandardGroup(ITokenCommunication _message)
    {
        throw new InvalidOperationException("should never happen");
    }

    protected override ITokenMessage OnGroupDelete(ITokenCommunication _message)
    {
        return _OnDelete(_message);
    }

    protected override ITokenMessage OnTokenStateChange(ITokenCommunication _message, bool _fullIdMatch = true)
    {
            if (_fullIdMatch)
            {
                switch (_message.Command)
                {
                    case ETokenCommands.GiveToken:
                    case ETokenCommands.TransferToken:
                        _SetToken(true);
                        return RepeatDownstream(_message);

                    case ETokenCommands.RetractToken:
                        _SetToken(false);
                        return RepeatDownstream(_message);

                    default:
                        throw new ArgumentException("should never happen");
                }
            }
            else if (_message.Command == ETokenCommands.TransferToken)
            {
                _SetToken(false);
                return RepeatDownstream(_message);

            }
            else throw new InvalidOperationException("should never happen");
    }

    protected override ITokenMessage RepeatDownstream(ITokenCommunication _message)
    {
        throw new InvalidOperationException("should never happen");
    }
}
