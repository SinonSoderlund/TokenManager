using System;
using NUnit.Framework.Constraints;
using STMS.Tokens.Id.Interfaces;

namespace STMS.Tokens.TokenEntities.Interfaces
{
    public interface ITokenChild : IComparable<ITokenId>
    {
        ITokenId ThisId { get; }
        public static bool operator true(ITokenChild token) => throw new NotImplementedException();
        public static bool operator false(ITokenChild token) => throw new NotImplementedException();
    }
}