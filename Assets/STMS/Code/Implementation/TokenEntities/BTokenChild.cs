using System;
using System.ComponentModel;
using Mono.Cecil.Cil;
using STMS.Tokens.Communication.Interfaces;
using STMS.Tokens.Id.Interfaces;
using STMS.Tokens.Id.Utilty;
using STMS.Tokens.TokenEntities.Communication;
using STMS.Tokens.TokenEntities.Communication.Factory;
using STMS.Tokens.TokenEntities.CommunicationHandlers;
using STMS.Tokens.TokenEntities.Contracts;
using STMS.Tokens.TokenEntities.Interfaces;

namespace STMS.Tokens.TokenEntities.Implementation
{
    public abstract class BTokenChild : CTokenChild
    {


        public static bool operator true(BTokenChild _this) => _this.hasToken == true;
        public static bool operator false(BTokenChild _this) => _this.hasToken == false;
        private TokenCommunicationCaseHandler _fullMatch, _groupMatch;

        public BTokenChild(ITokenId _id, ChildMessage _parentMessanger) : base(_id, _parentMessanger)
        {
            _fullMatch = new TokenCommunicationCaseHandler(ReflectAsCarrier, ReflectMessageWithStatus, OnTokenStateChange, OnTokenStateChange,
                    OnTokenStateChange, _OnDelete, _GroupEmptyIsInvalid, OnGroupDelete, ConvertToManagedGroup, ConvertToStandardGroup, AddManagedGroup);

            _groupMatch = new TokenCommunicationCaseHandler(RepeatDownstream, RepeatDownstream, OnTokenStateChange, RepeatDownstream, RepeatDownstream,
            RepeatDownstream, _GroupEmptyIsInvalid, _DoNothing, _DoNothing, _DoNothing, _DoNothing);
        }


        /// <summary>
        /// Makes sure that all calls to  token child are routed correctly
        /// </summary>
        /// <param name="_message">the parent message containing action information</param>
        /// <returns>status reponse message</returns>
        /// <exception cref="InvalidEnumArgumentException">Group empty is an upstream only value</exception>
        protected override ITokenMessage _ParentCallResponse(ITokenCommunication _message)
        {
            Communication = new CommuncationsPackage(_message, ThisId.GetMatchStatus(_message.Receipient));
            switch (Communication.MatchStatus)
            {
                case TokenIdMatchStatus.Full:
                    _fullMatch.HandleCommunication(Communication.Incomming.Command);
                    break;
                case TokenIdMatchStatus.GroupIdOnly:
                    _groupMatch.HandleCommunication(Communication.Incomming.Command);
                    break;
                case TokenIdMatchStatus.None:
                    break;
                default:
                    throw new NotImplementedException("unhandled case in BTokenChild");
            }
            return Communication.Outgoing;
        }

        /// <summary>
        /// Returns incomming communication with correct sender and updated status response
        /// </summary>
        /// <returns></returns>
        protected void ReflectMessageWithStatus()
        {
            Communication.Outgoing = ITokenCommunicationsFactory<object>.SenderReceiverSwapReturnMessage(Communication.Incomming, _statusResponse: GetStatus());
        }

        protected void ReflectAsCarrier()
        {
            Communication.Outgoing = ITokenCommunicationsFactory<ITokenChild>.SenderReceiverSwapReturnCarrier(Communication.Incomming, _statusResponse: GetStatus(), _payload: this);
        }

        private void _GroupEmptyIsInvalid()
        {
            throw new ArgumentException("Group empty is not a valid downstream-facing message");
        }
        private void _DoNothing() { }

        /// <summary>
        /// Repeats parent message to children, with apropriate effects for the given command.
        /// </summary>
        /// <returns></returns>
        protected abstract void RepeatDownstream();

        /// <summary>
        /// Called on group delete command
        /// </summary>
        protected abstract void OnGroupDelete();

        /// Called on group managed conversion command
        protected abstract void ConvertToManagedGroup();

        /// Called on group demanage conversion command
        protected abstract void ConvertToStandardGroup();

        /// <summary>
        /// Called when parent sends in group to be added to child list
        /// </summary>
        protected abstract void AddManagedGroup();

        /// <summary>
        /// Called when the token state is commanded to change
        /// </summary>
        /// <returns></returns>
        protected abstract void OnTokenStateChange();

        protected bool GetStatus()
        {
            switch (Communication.Incomming.Command)
            {
                case Tokens.Communication.ETokenCommands.HasToken:
                    return hasToken;
                case Tokens.Communication.ETokenCommands.Exists:
                    return true;
                    case Tokens.Communication.ETokenCommands.TransferToken:
                    return true;
                    case Tokens.Communication.ETokenCommands.RetractToken:
                    return true;
                default:
                    throw new ArgumentException("should never happen");
            }
        }
    }
}