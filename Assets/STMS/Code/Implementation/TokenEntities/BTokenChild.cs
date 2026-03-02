using System;
using System.ComponentModel;
using STMS.Tokens.Communication.Interfaces;
using STMS.Tokens.Id.Interfaces;
using STMS.Tokens.Id.Utilty;
using STMS.Tokens.TokenEntities.Communication;
using STMS.Tokens.TokenEntities.Communication.Factory;
using STMS.Tokens.TokenEntities.Contracts;
using STMS.Tokens.TokenEntities.Interfaces;

namespace STMS.Tokens.TokenEntities.Implementation
{
    public abstract class BTokenChild : CTokenChild
    {
        public BTokenChild(ITokenId _id, ChildMessage _parentMessanger) : base(_id, _parentMessanger) { }

        public static bool operator true(BTokenChild _this) => _this.hasToken == true;
        public static bool operator false(BTokenChild _this) => _this.hasToken == false;


        /// <summary>
        /// Makes sure that all calls to  token child are routed correctly
        /// </summary>
        /// <param name="_message">the parent message containing action information</param>
        /// <returns>status reponse message</returns>
        /// <exception cref="InvalidEnumArgumentException">Group empty is an upstream only value</exception>
        protected override ITokenMessage _ParentCallResponse(ITokenCommunication _message)
        {
            switch (ThisId.GetMatchStatus(_message.Receipient))
            {
                case TokenIdMatchStatus.Full:
                    switch (_message.Command)
                    {
                        case Tokens.Communication.ETokenCommands.Exists:
                            return ReflectAsCarrier(_message, true, this);

                        case Tokens.Communication.ETokenCommands.HasToken:
                            return ReflectMessageWithStatus(_message, hasToken);

                        case Tokens.Communication.ETokenCommands.TransferToken:
                            return OnTokenStateChange(_message);

                        case Tokens.Communication.ETokenCommands.RetractToken:
                            return OnTokenStateChange(_message);

                        case Tokens.Communication.ETokenCommands.GiveToken:
                            return OnTokenStateChange(_message);

                        case Tokens.Communication.ETokenCommands.DeleteHolder:
                            return _OnDelete(_message);

                        case Tokens.Communication.ETokenCommands.GroupEmpty:
                            throw new InvalidEnumArgumentException("Group empty is not a valid downstream-facing message");

                        case Tokens.Communication.ETokenCommands.GroupDelete:
                            return OnGroupDelete(_message);

                        case Tokens.Communication.ETokenCommands.ManageGroup:
                            return ConvertToManagedGroup(_message);

                        case Tokens.Communication.ETokenCommands.RemoveManagedGroup:
                            return ConvertToStandardGroup(_message);

                        case Tokens.Communication.ETokenCommands.ManagedGroupCarrier:
                            return AddManagedGroup(_message as ITokenCarrier<ITokenChild>);

                        default:
                            throw new NotImplementedException("unhandled case in BTokenChild");
                    }
                case TokenIdMatchStatus.GroupIdOnly:
                    switch (_message.Command)
                    {
                        case Tokens.Communication.ETokenCommands.Exists:
                            return RepeatDownstream(_message);

                        case Tokens.Communication.ETokenCommands.HasToken:
                            return RepeatDownstream(_message);

                        case Tokens.Communication.ETokenCommands.TransferToken:
                            return OnTokenStateChange(_message, false);

                        case Tokens.Communication.ETokenCommands.RetractToken:
                            return RepeatDownstream(_message);

                        case Tokens.Communication.ETokenCommands.GiveToken:
                            return RepeatDownstream(_message);

                        case Tokens.Communication.ETokenCommands.DeleteHolder:
                            return RepeatDownstream(_message);

                        case Tokens.Communication.ETokenCommands.GroupEmpty:
                            throw new InvalidEnumArgumentException("Group empty is not a valid downstream-facing message");
                        case Tokens.Communication.ETokenCommands.GroupDelete:
                            break;
                        case Tokens.Communication.ETokenCommands.ManageGroup:
                            break;
                        case Tokens.Communication.ETokenCommands.RemoveManagedGroup:
                            break;
                        case Tokens.Communication.ETokenCommands.ManagedGroupCarrier:
                            break;
                        default:
                            throw new NotImplementedException("unhandled case in BTokenChild");
                    }
                    break;
                case TokenIdMatchStatus.None:
                    break;
                default:
                    throw new NotImplementedException("unhandled case in BTokenChild");
            }
            return null;
        }

        /// <summary>
        /// Returns incomming communication with correct sender and updated status response
        /// </summary>
        /// <param name="_message">message from parent</param>
        /// <param name="_status">the command status response</param>
        /// <returns>message for parent</returns>
        protected ITokenMessage ReflectMessageWithStatus(ITokenCommunication _message, bool _status)
        {
            return ITokenCommunicationsFactory<object>.SenderReceiverSwapReturnMessage(_message, _statusResponse: _status);
        }

        protected ITokenCarrier<ITokenChild> ReflectAsCarrier(ITokenCommunication _message, bool _status, ITokenChild _payload)
        {
            return ITokenCommunicationsFactory<ITokenChild>.SenderReceiverSwapReturnCarrier(_message, _statusResponse: _status, _payload: _payload);
        }

        /// <summary>
        /// Repeats paren message to children, with apropiate effects for the given command.
        /// </summary>
        /// <param name="_message">parent message</param>
        /// <returns>child repsonse</returns>
        protected abstract ITokenMessage RepeatDownstream(ITokenCommunication _message);

        /// <summary>
        /// Called on group delete command
        /// </summary>
        protected abstract ITokenMessage OnGroupDelete(ITokenCommunication _message);

        /// Called on group managed conversion command
        protected abstract ITokenCarrier<CTokenChild> ConvertToManagedGroup(ITokenCommunication _message);

        /// Called on group demanage conversion command
        protected abstract ITokenCarrier<CTokenChild> ConvertToStandardGroup(ITokenCommunication _message);

        /// <summary>
        /// Called when parent sends in group to be added to child list
        /// </summary>
        protected abstract ITokenMessage AddManagedGroup(ITokenCarrier<ITokenChild> _newGroup);

        /// <summary>
        /// Called when the token state is commanded to change
        /// </summary>
        /// <param name="_message">message from parent</param>
        /// <returns>child response messages</returns>
        protected abstract ITokenMessage OnTokenStateChange(ITokenCommunication _message, bool _fullIdMatch = true);

    }
}