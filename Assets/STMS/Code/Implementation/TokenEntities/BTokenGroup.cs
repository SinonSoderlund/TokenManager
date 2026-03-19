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

        protected override void OnGroupDelete()
        {
            RepeatDownstream();
            Children = null;
            _OnDelete();
        }

        protected override void RepeatDownstream()
        {
            Communication.Outgoing = Children.SendMessageDown(Communication.Incomming);
            if(Communication.Outgoing.Command == Tokens.Communication.ETokenCommands.DeleteHolder)
            {
                ITokenMessage _message = Children.DeleteChild(Communication.Outgoing.Sender);
                if(_message.StatusResponse == true)
                {
                    Children = null;
                    _OnDelete();
                    Communication.Outgoing = _message;
                }
            }
        }
        protected override void AddManagedGroup()
        {
            Communication.Outgoing = Children.AddChild(Communication.Incomming as ITokenCarrier<ITokenChild>);
        }

        protected override void OnTokenStateChange()
        {
            if (Communication.MatchStatus == Id.Utilty.TokenIdMatchStatus.Full)
            {
                switch (Communication.Incomming.Command)
                {
                    case Tokens.Communication.ETokenCommands.GiveToken:
                    case Tokens.Communication.ETokenCommands.TransferToken:
                        _SetToken(true);
                        RepeatDownstream();
                        return;

                    case Tokens.Communication.ETokenCommands.RetractToken:
                        _SetToken(false);
                        RepeatDownstream();
                        return;

                    default:
                        throw new ArgumentException("should never happen");
                }
            }
            else if (Communication.Incomming.Command == Tokens.Communication.ETokenCommands.TransferToken)
            {
                _SetToken(false);
                RepeatDownstream();
                return;
            }
            else throw new ArgumentException("should never happen");
        }

    }
}