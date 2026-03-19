using System.Data;
using STMS.Tokens.Communication;
using STMS.Tokens.Communication.Implementation;
using STMS.Tokens.Communication.Interfaces;
using STMS.Tokens.Id.Implentation;
using STMS.Tokens.Id.Interfaces;

namespace STMS.Tokens.TokenEntities.Communication.Factory
{
    //TODO: might not be a good solution, might need looking over
    public static class ITokenCommunicationsFactory<T> where T: class
    {

        /// <summary>
        /// Returns the ITokenCommunication corresponding to supplied aarguments,see param info for more info, 
        /// </summary>
        /// <param name="_command">always supplied, the semantic meaning of the communication</param>
        /// <param name="_status">command status value, always supplied</param>
        /// <param name="_receipient">meessage receipient, optional, always supplied</param>
        /// <param name="_payload">payload data being sent, optional, Token request and carrier only</param>
        /// <param name="_sender">the sender of the message, optional, token message and carrier only</param>
        /// <returns>if youre returning a more specied type than itokencommunication, 'as cast' the return value </returns>
        public static ITokenCommunication GetITokenCommunication(ETokenCommands _command, bool _status, ITokenId? _receipient = null, T? _payload = null, ITokenId _sender = null)
        {
            if(_payload is null)
            {
                if(_sender is null)
                    return new TokenCommunication(_receipient, _command, _status);
                else
                    return new TokenMessage(_receipient, _command, _status, _sender);
            }
            else if (_sender is null)
                return new TokenRequest<T>(_receipient,_command, _status, _payload);

            return new TokenCarrier<T>(_receipient, _command ,_status, _payload, _sender);
        }
        
        /// <summary>
        /// Modifier constructor, merges base object with supplied replacement values
        /// </summary>
        /// <param name="_base">base data</param>
        /// <param name="_receipient">optional, new recipient adress</param>
        /// <param name="_command">optional, new command data</param>
        /// <param name="_statusResponse">optional, new status value</param>
        /// <param name="_payload">optional, new payload data</param>
        /// <returns>merged data object</returns>
        public static ITokenRequest<T> ModifyRequest(TokenRequest<T> _base, ITokenId? _receipient = null, ETokenCommands? _command = null, bool? _statusResponse = null, T? _payload = null)
        {
            return new TokenRequest<T>(_base,  _receipient ,  _command,  _statusResponse, _payload);
        }

        /// <summary>
        /// Modifier constructor, merges base object with supplied replacement values
        /// </summary>
        /// <param name="_base">base data</param>
        /// <param name="_receipient">optional, new recipient adress</param>
        /// <param name="_command">optional, new command data</param>
        /// <param name="_statusResponse">optional, new status value</param>
        /// <param name="_sender">optional, new sender adress</param>
        /// <returns>merged data object</returns>
        public static ITokenMessage ModifyMessage(TokenMessage _base, ITokenId? _receipient = null, ETokenCommands? _command = null, bool? _statusResponse = null, ITokenId? _sender = null)
        {
            return new TokenMessage( _base,  _receipient,  _command,  _statusResponse,  _sender);
        }

        /// <summary>
        /// Modifier constructor, merges base object with supplied replacement values
        /// </summary>
        /// <param name="_base">base data</param>
        /// <param name="_receipient">optional, new recipient adress</param>
        /// <param name="_command">optional, new command data</param>
        /// <param name="_statusResponse">optional, new status value</param>
        /// <returns>merged data object</returns>
        public static ITokenCommunication ModifyCommunication(TokenCommunication _base, ITokenId? _receipient = null, ETokenCommands? _command = null, bool? _statusResponse = null)
        {
            return new TokenCommunication(_base, _receipient, _command, _statusResponse);
        }

        /// <summary>
        /// Modifier constructor, merges base object with supplied replacement values
        /// </summary>
        /// <param name="_base">base data</param>
        /// <param name="_receipient">optional, new recipient adress</param>
        /// <param name="_command">optional, new command data</param>
        /// <param name="_statusResponse">optional, new status value</param>
        /// <param name="_payload">optional, new payload data</param>
        /// <param name="_sender">optional, new sender adress</param>
        /// <returns>merged data object</returns>
        public static ITokenCarrier<T> ModifyCarrier(TokenCarrier<T> _base, ITokenId _receipient = null, ETokenCommands? _command = null, bool? _statusResponse = null, T? _payload = null,  ITokenId? _sender = null)
        {
            return new TokenCarrier<T>(_base,  _receipient,  _command, _statusResponse, _payload,  _sender);
        }

        /// <summary>
        /// Modifier constructor, merges base object with supplied replacement values, swaps sender and receipient adresses
        /// </summary>
        /// <param name="_base">base data</param>
        /// <param name="_command">optional, new command data</param>
        /// <param name="_statusResponse">optional, new status value</param>
        /// <returns>merged data object</returns>
        public static ITokenMessage SenderReceiverSwapModifyMessage(TokenMessage _base, ETokenCommands? _command = null, bool? _statusResponse = null)
        {
            return new TokenMessage( _base,  _base.Sender,  _command,  _statusResponse,  _base.Receipient);
        }

        /// <summary>
        /// Modifier constructor, merges base object with supplied replacement values, swaps sender and receipient adresses
        /// </summary>
        /// <param name="_base">base data</param>
        /// <param name="_command">optional, new command data</param>
        /// <param name="_statusResponse">optional, new status value</param>
        /// <param name="_payload">optional, new payload data</param>
        /// <returns>merged data object</returns>
        public static ITokenCarrier<T> SenderReceiverSwapModifyCarrier(TokenCarrier<T> _base, ETokenCommands? _command = null, bool? _statusResponse = null, T? _payload = null)
        {
            return new TokenCarrier<T>(_base,  _base.Sender,  _command, _statusResponse, _payload,  _base.Receipient);
        }

        /// <summary>
        /// Modifier constructor, merges base object with supplied replacement values, swaps sender and receipient adresses
        /// </summary>
        /// <param name="_base">base data</param>
        /// <param name="_command">optional, new command data</param>
        /// <param name="_statusResponse">optional, new status value</param>
        /// <param name="_payload">optional, new payload data</param>
        /// <returns>merged data object</returns>
        public static ITokenCarrier<T> SenderReceiverSwapReturnCarrier(ITokenCommunication _base, ETokenCommands? _command = null, bool? _statusResponse = null, T? _payload = null)
        {
            return CommunicationToCarrier(_base,  null,  _command is null ? _base.Command : _command.Value, _statusResponse is null ? _base.StatusResponse :  _statusResponse.Value, _payload,  _base.Receipient);
        }

        //TODO maybe this doesnt make sense and the issue was just the overload was asking for a concrete class instead of interface, dont have mental capacity to check it out rn though
        private static ITokenCarrier<T> CommunicationToCarrier(ITokenCommunication _base, ITokenId _receipient = null, ETokenCommands? _command = null, bool? _statusResponse = null, T? _payload = null,  ITokenId? _sender = null)
        {
            return new TokenCarrier<T>(new TokenRequest<T>(_base, _receipient, _command, _statusResponse, _payload), _sender);
        }

        /// <summary>
        /// takes token communication, changes receipient to sender in returned token message
        /// </summary>
        /// <param name="_base">base data</param>
        /// <param name="_command">optional, new command data</param>
        /// <param name="_statusResponse">optional, new status value</param>
        /// <returns>merged data object</returns>
        public static ITokenMessage SenderReceiverSwapReturnMessage(ITokenCommunication _base, ETokenCommands? _command = null, bool? _statusResponse = null)
        {
            return new TokenMessage(null, _command is null ? _base.Command : _command.Value, _statusResponse is null ? _base.StatusResponse :  _statusResponse.Value, _base.Receipient);
        }

        /// <summary>
        /// Takes a ManageGroup ItokenCommunication, shifts receipient to the Target Nexus
        /// </summary>
        /// <param name="_base"></param>
        /// <returns></returns>
        public static ITokenCommunication GetManageTarget(ITokenCommunication _base)
        {
            return new TokenCommunication(_base, _base.Receipient.TargetAsNexus());
        }
        /// <summary>
        /// Takes a ManageGroup ITokenCommuncation, sets its target to group nexus, adds on payload from itokenrequest
        /// </summary>
        /// <param name="_base"></param>
        /// <param name="_payloadHolder"></param>
        /// <returns></returns>
        public static ITokenCarrier<T> MergeAsCarrierAndTargetGroup(ITokenCommunication _base, ITokenRequest<T> _payloadHolder)
        {
            return new TokenCarrier<T>(_base, _base.Receipient.TargetGroup(), _payload: _payloadHolder.Payload);
        }
    }

}