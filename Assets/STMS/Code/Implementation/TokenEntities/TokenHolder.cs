using System;
using STMS.Tokens.Communication.Interfaces;
using STMS.Tokens.TokenEntities.Contracts;
using STMS.Tokens.TokenEntities.Implementation;
using STMS.Tokens.TokenEntities.Interfaces;
using STMS.Tokens.Communication;
using STMS.Tokens.Id.Interfaces;
using STMS.Tokens.TokenEntities.Communication;
namespace STMS.Tokens.TokenEntities.Implementation
{
    public class TokenHolder : BTokenChild
    {

        public TokenHolder(ITokenId _id, ChildMessage _parentMessanger) : base(_id, _parentMessanger) { }

        protected override void AddManagedGroup()
        {
            throw new InvalidOperationException("should never happen");
        }

        protected override void ConvertToManagedGroup()
        {
            throw new InvalidOperationException("should never happen");
        }

        protected override void ConvertToStandardGroup()
        {
            throw new InvalidOperationException("should never happen");
        }

        protected override void OnGroupDelete()
        {
            _OnDelete();
        }

        protected override void OnTokenStateChange()
        {
            if (Communication.MatchStatus == Id.Utilty.TokenIdMatchStatus.Full)
            {
                switch (Communication.Incomming.Command)
                {
                    case ETokenCommands.GiveToken:
                    case ETokenCommands.TransferToken:
                        _SetToken(true);
                        ReflectMessageWithStatus();
                        return;

                    case ETokenCommands.RetractToken:
                        _SetToken(false);
                        ReflectMessageWithStatus();
                        return;

                    default:
                        throw new ArgumentException("should never happen");
                }
            }
            else if (Communication.Incomming.Command == ETokenCommands.TransferToken)
            {
                _SetToken(false);
            }
            else throw new InvalidOperationException("should never happen");
        }

        protected override void RepeatDownstream()
        {
            throw new InvalidOperationException("should never happen");
        }
    }
}