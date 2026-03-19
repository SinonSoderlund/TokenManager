using System;
using STMS.Tokens.Communication;

namespace STMS.Tokens.TokenEntities.CommunicationHandlers
{
    public class TokenCommunicationCaseHandler
    {
        private Action _exists, _hasToken, _transfer, _retract, _give, _delete, _groupEmpty, _groupDelete, _manageGroup, _unmanageGroup, _groupCarrier;
        /// <summary>
        /// Each argument corrresponds witht he action that should be taken upon a given case in etokencommands
        /// </summary>
        /// <param name="_onExists"></param>
        /// <param name="_onHasToken"></param>
        /// <param name="_onTransfer"></param>
        /// <param name="_onRetract"></param>
        /// <param name="_onGive"></param>
        /// <param name="_onDelete"></param>
        /// <param name="_onGroupEmpty"></param>
        /// <param name="_onGroupDelete"></param>
        /// <param name="_onManageGroup"></param>
        /// <param name="_onUnmanageGroup"></param>
        /// <param name="_onGroupCarrier"></param>
        public TokenCommunicationCaseHandler(Action _onExists, Action _onHasToken, Action _onTransfer, Action _onRetract, Action _onGive, Action _onDelete, Action _onGroupEmpty, Action _onGroupDelete, Action _onManageGroup, Action _onUnmanageGroup, Action _onGroupCarrier)
        {
            _exists = _onExists;
            _hasToken = _onHasToken;
            _transfer = _onTransfer;
            _retract = _onRetract;
            _give = _onGive;
            _delete = _onDelete;
            _groupEmpty = _onGroupDelete;
            _groupDelete = _onGroupDelete;
            _manageGroup = _onManageGroup;
            _unmanageGroup = _onUnmanageGroup;
            _groupCarrier = _onGroupCarrier;
        }

        public void HandleCommunication(ETokenCommands _message)
        {
            switch (_message)
            {
                case Tokens.Communication.ETokenCommands.Exists:
                    _exists();
                    return;

                case Tokens.Communication.ETokenCommands.HasToken:
                    _hasToken();
                    return;
                case Tokens.Communication.ETokenCommands.TransferToken:
                    _transfer();
                    return;
                case Tokens.Communication.ETokenCommands.RetractToken:
                    _retract();
                    return;
                case Tokens.Communication.ETokenCommands.GiveToken:
                    _give();
                    return;
                case Tokens.Communication.ETokenCommands.DeleteHolder:
                    _delete();
                    return;
                case Tokens.Communication.ETokenCommands.GroupEmpty:
                    _groupEmpty();
                    return;
                case Tokens.Communication.ETokenCommands.GroupDelete:
                    _groupDelete();
                    return;                    
                case Tokens.Communication.ETokenCommands.ManageGroup:
                    _manageGroup();
                    return;                    
                case Tokens.Communication.ETokenCommands.RemoveManagedGroup:
                    _unmanageGroup();
                    return;                    
                case Tokens.Communication.ETokenCommands.ManagedGroupCarrier:
                    _groupCarrier();
                    return;                    
                default:
                    throw new NotImplementedException("unhandled case in TokenCommunicationCaseHandler");
            }
        }
    }
}