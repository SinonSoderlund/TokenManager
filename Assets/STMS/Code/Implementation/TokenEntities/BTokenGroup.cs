using System;
using STMS.Tokens.Communication.Interfaces;
using STMS.Tokens.Id.Interfaces;
using STMS.Tokens.TokenEntities.Communication;
using STMS.Tokens.TokenEntities.Contracts;
using STMS.Tokens.TokenEntities.Interfaces;
using STMS.Tokens.TokenEntities.Utilites;

namespace STMS.Tokens.TokenEntities.Implementation
{
    public abstract class BtokenGroup : CTokenGroup
    {
        protected BtokenGroup(ITokenId _id, ChildMessage _parentMessanger) : base(_id, _parentMessanger)
        {
            Children = new TokenChildSet(_id);
        }
        protected BtokenGroup(ITokenId _id, ChildMessage _parentMessanger, TokenChildSet _children) : base(_id, _parentMessanger)
        {
            Children = _children;
        }

        protected override ITokenMessage OnGroupDelete(ITokenCommunication _message)
        {
            RepeatDownstream(_message);
            Children = null;
            return _OnDelete(_message);
        }

        protected override ITokenMessage RepeatDownstream(ITokenCommunication _message)
        {
            return Children.SendMessageDown(_message);
        }
        protected override ITokenMessage AddManagedGroup(ITokenCarrier<ITokenChild> _newGroup)
        {
            return Children.AddChild(_newGroup);
        }

        protected override ITokenMessage OnTokenStateChange(ITokenCommunication _message, bool _fullIdMatch = true)
        {
            if (_fullIdMatch)
            {
                switch (_message.Command)
                {
                    case Tokens.Communication.ETokenCommands.GiveToken:
                    case Tokens.Communication.ETokenCommands.TransferToken:
                        _SetToken(true);
                        return RepeatDownstream(_message);

                    case Tokens.Communication.ETokenCommands.RetractToken:
                        _SetToken(false);
                        return RepeatDownstream(_message);

                    default:
                        throw new ArgumentException("should never happen");
                }
            }
            else if (_message.Command == Tokens.Communication.ETokenCommands.TransferToken)
            {
                _SetToken(false);
                return RepeatDownstream(_message);

            }
            else throw new ArgumentException("should never happen");
        }

    }
}