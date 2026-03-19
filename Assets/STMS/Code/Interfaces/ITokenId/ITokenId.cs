using System;

namespace STMS.Tokens.Id.Interfaces
{
    public interface ITokenId :IComparable<ITokenId>
    {
        string GUID { get; }
        string Name { get; }

        public ITokenId TargetAsNexus();
        public ITokenId TargetGroup();
    }
}