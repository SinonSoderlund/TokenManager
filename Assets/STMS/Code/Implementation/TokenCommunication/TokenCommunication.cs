using STMS.Tokens.Communication.Interfaces;
using STMS.Tokens.Id.Interfaces;

namespace STMS.Tokens.Communication.Implementation
{
    public class TokenCommunication : ITokenCommunication
    {
        public ITokenId? Receipient { get; private set; }


        public ETokenCommands Command { get; private set; }


        public bool StatusResponse { get; private set; }

        public TokenCommunication(ITokenId? _receipient, ETokenCommands _command, bool _statusResponse)
        {
            Receipient = _receipient;
            Command = _command;
            StatusResponse = _statusResponse;
        }

        public TokenCommunication(ITokenCommunication _toCopy)
        {
            Receipient = _toCopy.Receipient;
            Command = _toCopy.Command;
            StatusResponse = _toCopy.StatusResponse;
        }
        
        public TokenCommunication(ITokenCommunication _base, ITokenId? _receipient = null, ETokenCommands? _command = null, bool? _statusResponse = null)
        {
            Receipient = _receipient != null ? _receipient : _base.Receipient;
            Command = _command != null ? _command.Value : _base.Command;
            StatusResponse = _statusResponse != null ? _statusResponse.Value : _base.StatusResponse;
        }

    }
}