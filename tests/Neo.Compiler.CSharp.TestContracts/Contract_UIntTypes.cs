using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Attributes;

namespace Neo.Compiler.CSharp.TestContracts
{
    public class Contract_UIntTypes : SmartContract.Framework.SmartContract
    {
        [Hash160("NiNmXL8FjEUEs1nfX9uHFBNaenxDHJtmuB")]
        static readonly UInt160 Owner = default!;

        public static bool checkOwner(UInt160 owner) { return owner == Owner; }
        public static bool checkZeroStatic(UInt160 owner) { return owner == UInt160.Zero; }
        public static UInt160 constructUInt160(byte[] bytes) { return (UInt160)bytes; }
        public static bool validateAddress(UInt160 address) => address.IsValid && !address.IsZero;
    }
}
