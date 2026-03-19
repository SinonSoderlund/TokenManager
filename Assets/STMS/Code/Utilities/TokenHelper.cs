using STMS.Tokens.Communication.Implementation;
using STMS.Tokens.Id.Implentation;
using STMS.Tokens.Id.Interfaces;
using STMS.Tokens.TokenEntities.Implementation;
using STMS.Tokens.TokenEntities.Interfaces;
using UnityEngine;

public static class TokenHelper
{
    public static ITokenChild GetTokenHolder(GameObject _host, string _name)
    {
        ITokenId _target = GetTokenId(_host, _name);
        return TokenManager.tokenManager.Requests(new TokenCommunication(_target, STMS.Tokens.Communication.ETokenCommands.Exists, true)).Payload;
    }

    public static void ToggleTokenValue(GameObject _host, string _name)
    {
        ITokenId _target = GetTokenId(_host, _name);
        var _status = TokenManager.tokenManager.Requests(new TokenCommunication(_target, STMS.Tokens.Communication.ETokenCommands.HasToken, true));
        if (_status.StatusResponse)
            TokenManager.tokenManager.Requests(new TokenCommunication(_target, STMS.Tokens.Communication.ETokenCommands.RetractToken, true));
        else
            TokenManager.tokenManager.Requests(new TokenCommunication(_target, STMS.Tokens.Communication.ETokenCommands.GiveToken, true));
    }

    public static TokenId GetTokenId(GameObject _host, string _target)
    {
        return new TokenId(_host.GetInstanceID().ToString(), _target);
    }
}
