using System;
using System.Diagnostics;
using System.Runtime.ExceptionServices;

namespace Lib.UniversalCore.Extensions;

public static class ExceptionExtensions
{
    public static Exception ThrowMe(this Exception ex)
    {
        ExceptionDispatchInfo.Capture(ex).Throw();

        return new UnreachableException("The Compiler doesn't understand the semantics of the above line, so this is required to make the compiler happy for the users of this extension method.");
    }
}
