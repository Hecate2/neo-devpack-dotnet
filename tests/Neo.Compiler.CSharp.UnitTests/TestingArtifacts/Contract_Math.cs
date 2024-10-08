using Neo.Cryptography.ECC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;

namespace Neo.SmartContract.Testing;

public abstract class Contract_Math(Neo.SmartContract.Testing.SmartContractInitialize initialize) : Neo.SmartContract.Testing.SmartContract(initialize), IContractInfo
{
    #region Compiled data

    public static Neo.SmartContract.Manifest.ContractManifest Manifest => Neo.SmartContract.Manifest.ContractManifest.Parse(@"{""name"":""Contract_Math"",""groups"":[],""features"":{},""supportedstandards"":[],""abi"":{""methods"":[{""name"":""max"",""parameters"":[{""name"":""a"",""type"":""Integer""},{""name"":""b"",""type"":""Integer""}],""returntype"":""Integer"",""offset"":0,""safe"":false},{""name"":""min"",""parameters"":[{""name"":""a"",""type"":""Integer""},{""name"":""b"",""type"":""Integer""}],""returntype"":""Integer"",""offset"":7,""safe"":false},{""name"":""sign"",""parameters"":[{""name"":""a"",""type"":""Integer""}],""returntype"":""Integer"",""offset"":14,""safe"":false},{""name"":""abs"",""parameters"":[{""name"":""a"",""type"":""Integer""}],""returntype"":""Integer"",""offset"":20,""safe"":false},{""name"":""bigMul"",""parameters"":[{""name"":""a"",""type"":""Integer""},{""name"":""b"",""type"":""Integer""}],""returntype"":""Integer"",""offset"":26,""safe"":false},{""name"":""divRemByte"",""parameters"":[{""name"":""left"",""type"":""Integer""},{""name"":""right"",""type"":""Integer""}],""returntype"":""Array"",""offset"":64,""safe"":false},{""name"":""divRemShort"",""parameters"":[{""name"":""left"",""type"":""Integer""},{""name"":""right"",""type"":""Integer""}],""returntype"":""Array"",""offset"":78,""safe"":false},{""name"":""divRemInt"",""parameters"":[{""name"":""left"",""type"":""Integer""},{""name"":""right"",""type"":""Integer""}],""returntype"":""Array"",""offset"":92,""safe"":false},{""name"":""divRemLong"",""parameters"":[{""name"":""left"",""type"":""Integer""},{""name"":""right"",""type"":""Integer""}],""returntype"":""Array"",""offset"":106,""safe"":false},{""name"":""divRemSbyte"",""parameters"":[{""name"":""left"",""type"":""Integer""},{""name"":""right"",""type"":""Integer""}],""returntype"":""Array"",""offset"":120,""safe"":false},{""name"":""divRemUshort"",""parameters"":[{""name"":""left"",""type"":""Integer""},{""name"":""right"",""type"":""Integer""}],""returntype"":""Array"",""offset"":134,""safe"":false},{""name"":""divRemUint"",""parameters"":[{""name"":""left"",""type"":""Integer""},{""name"":""right"",""type"":""Integer""}],""returntype"":""Array"",""offset"":148,""safe"":false},{""name"":""divRemUlong"",""parameters"":[{""name"":""left"",""type"":""Integer""},{""name"":""right"",""type"":""Integer""}],""returntype"":""Array"",""offset"":162,""safe"":false},{""name"":""clampByte"",""parameters"":[{""name"":""value"",""type"":""Integer""},{""name"":""min"",""type"":""Integer""},{""name"":""max"",""type"":""Integer""}],""returntype"":""Integer"",""offset"":176,""safe"":false},{""name"":""clampSByte"",""parameters"":[{""name"":""value"",""type"":""Integer""},{""name"":""min"",""type"":""Integer""},{""name"":""max"",""type"":""Integer""}],""returntype"":""Integer"",""offset"":191,""safe"":false},{""name"":""clampShort"",""parameters"":[{""name"":""value"",""type"":""Integer""},{""name"":""min"",""type"":""Integer""},{""name"":""max"",""type"":""Integer""}],""returntype"":""Integer"",""offset"":206,""safe"":false},{""name"":""clampUShort"",""parameters"":[{""name"":""value"",""type"":""Integer""},{""name"":""min"",""type"":""Integer""},{""name"":""max"",""type"":""Integer""}],""returntype"":""Integer"",""offset"":221,""safe"":false},{""name"":""clampInt"",""parameters"":[{""name"":""value"",""type"":""Integer""},{""name"":""min"",""type"":""Integer""},{""name"":""max"",""type"":""Integer""}],""returntype"":""Integer"",""offset"":236,""safe"":false},{""name"":""clampUInt"",""parameters"":[{""name"":""value"",""type"":""Integer""},{""name"":""min"",""type"":""Integer""},{""name"":""max"",""type"":""Integer""}],""returntype"":""Integer"",""offset"":251,""safe"":false},{""name"":""clampLong"",""parameters"":[{""name"":""value"",""type"":""Integer""},{""name"":""min"",""type"":""Integer""},{""name"":""max"",""type"":""Integer""}],""returntype"":""Integer"",""offset"":266,""safe"":false},{""name"":""clampULong"",""parameters"":[{""name"":""value"",""type"":""Integer""},{""name"":""min"",""type"":""Integer""},{""name"":""max"",""type"":""Integer""}],""returntype"":""Integer"",""offset"":281,""safe"":false}],""events"":[]},""permissions"":[],""trusts"":[],""extra"":{""nef"":{""optimization"":""All""}}}");

    /// <summary>
    /// Optimization: "All"
    /// </summary>
    public static Neo.SmartContract.NefFile Nef => Neo.IO.Helper.AsSerializable<Neo.SmartContract.NefFile>(Convert.FromBase64String(@"TkVGM1Rlc3RpbmdFbmdpbmUAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAP0oAVcAAnl4ukBXAAJ5eLlAVwABeJlAVwABeJpAVwACeXigSgMAAAAAAAAAgAQAAAAAAAAAgAAAAAAAAAAAuyQDOkBXAAJ5eEoSTaFTohLAQFcAAnl4ShJNoVOiEsBAVwACeXhKEk2hU6ISwEBXAAJ5eEoSTaFTohLAQFcAAnl4ShJNoVOiEsBAVwACeXhKEk2hU6ISwEBXAAJ5eEoSTaFTohLAQFcAAnl4ShJNoVOiEsBAVwADeHl6S0syAzpTurlAVwADeHl6S0syAzpTurlAVwADeHl6S0syAzpTurlAVwADeHl6S0syAzpTurlAVwADeHl6S0syAzpTurlAVwADeHl6S0syAzpTurlAVwADeHl6S0syAzpTurlAVwADeHl6S0syAzpTurlA6UPMmg=="));

    #endregion

    #region Unsafe methods

    /// <summary>
    /// Unsafe method
    /// </summary>
    [DisplayName("abs")]
    public abstract BigInteger? Abs(BigInteger? a);

    /// <summary>
    /// Unsafe method
    /// </summary>
    [DisplayName("bigMul")]
    public abstract BigInteger? BigMul(BigInteger? a, BigInteger? b);

    /// <summary>
    /// Unsafe method
    /// </summary>
    /// <remarks>
    /// Script: VwADeHl6S0syAzpTurlA
    /// 00 : OpCode.INITSLOT 0003
    /// 03 : OpCode.LDARG0
    /// 04 : OpCode.LDARG1
    /// 05 : OpCode.LDARG2
    /// 06 : OpCode.OVER
    /// 07 : OpCode.OVER
    /// 08 : OpCode.JMPLE 03
    /// 0A : OpCode.THROW
    /// 0B : OpCode.REVERSE3
    /// 0C : OpCode.MAX
    /// 0D : OpCode.MIN
    /// 0E : OpCode.RET
    /// </remarks>
    [DisplayName("clampByte")]
    public abstract BigInteger? ClampByte(BigInteger? value, BigInteger? min, BigInteger? max);

    /// <summary>
    /// Unsafe method
    /// </summary>
    /// <remarks>
    /// Script: VwADeHl6S0syAzpTurlA
    /// 00 : OpCode.INITSLOT 0003
    /// 03 : OpCode.LDARG0
    /// 04 : OpCode.LDARG1
    /// 05 : OpCode.LDARG2
    /// 06 : OpCode.OVER
    /// 07 : OpCode.OVER
    /// 08 : OpCode.JMPLE 03
    /// 0A : OpCode.THROW
    /// 0B : OpCode.REVERSE3
    /// 0C : OpCode.MAX
    /// 0D : OpCode.MIN
    /// 0E : OpCode.RET
    /// </remarks>
    [DisplayName("clampInt")]
    public abstract BigInteger? ClampInt(BigInteger? value, BigInteger? min, BigInteger? max);

    /// <summary>
    /// Unsafe method
    /// </summary>
    /// <remarks>
    /// Script: VwADeHl6S0syAzpTurlA
    /// 00 : OpCode.INITSLOT 0003
    /// 03 : OpCode.LDARG0
    /// 04 : OpCode.LDARG1
    /// 05 : OpCode.LDARG2
    /// 06 : OpCode.OVER
    /// 07 : OpCode.OVER
    /// 08 : OpCode.JMPLE 03
    /// 0A : OpCode.THROW
    /// 0B : OpCode.REVERSE3
    /// 0C : OpCode.MAX
    /// 0D : OpCode.MIN
    /// 0E : OpCode.RET
    /// </remarks>
    [DisplayName("clampLong")]
    public abstract BigInteger? ClampLong(BigInteger? value, BigInteger? min, BigInteger? max);

    /// <summary>
    /// Unsafe method
    /// </summary>
    /// <remarks>
    /// Script: VwADeHl6S0syAzpTurlA
    /// 00 : OpCode.INITSLOT 0003
    /// 03 : OpCode.LDARG0
    /// 04 : OpCode.LDARG1
    /// 05 : OpCode.LDARG2
    /// 06 : OpCode.OVER
    /// 07 : OpCode.OVER
    /// 08 : OpCode.JMPLE 03
    /// 0A : OpCode.THROW
    /// 0B : OpCode.REVERSE3
    /// 0C : OpCode.MAX
    /// 0D : OpCode.MIN
    /// 0E : OpCode.RET
    /// </remarks>
    [DisplayName("clampSByte")]
    public abstract BigInteger? ClampSByte(BigInteger? value, BigInteger? min, BigInteger? max);

    /// <summary>
    /// Unsafe method
    /// </summary>
    /// <remarks>
    /// Script: VwADeHl6S0syAzpTurlA
    /// 00 : OpCode.INITSLOT 0003
    /// 03 : OpCode.LDARG0
    /// 04 : OpCode.LDARG1
    /// 05 : OpCode.LDARG2
    /// 06 : OpCode.OVER
    /// 07 : OpCode.OVER
    /// 08 : OpCode.JMPLE 03
    /// 0A : OpCode.THROW
    /// 0B : OpCode.REVERSE3
    /// 0C : OpCode.MAX
    /// 0D : OpCode.MIN
    /// 0E : OpCode.RET
    /// </remarks>
    [DisplayName("clampShort")]
    public abstract BigInteger? ClampShort(BigInteger? value, BigInteger? min, BigInteger? max);

    /// <summary>
    /// Unsafe method
    /// </summary>
    /// <remarks>
    /// Script: VwADeHl6S0syAzpTurlA
    /// 00 : OpCode.INITSLOT 0003
    /// 03 : OpCode.LDARG0
    /// 04 : OpCode.LDARG1
    /// 05 : OpCode.LDARG2
    /// 06 : OpCode.OVER
    /// 07 : OpCode.OVER
    /// 08 : OpCode.JMPLE 03
    /// 0A : OpCode.THROW
    /// 0B : OpCode.REVERSE3
    /// 0C : OpCode.MAX
    /// 0D : OpCode.MIN
    /// 0E : OpCode.RET
    /// </remarks>
    [DisplayName("clampUInt")]
    public abstract BigInteger? ClampUInt(BigInteger? value, BigInteger? min, BigInteger? max);

    /// <summary>
    /// Unsafe method
    /// </summary>
    /// <remarks>
    /// Script: VwADeHl6S0syAzpTurlA
    /// 00 : OpCode.INITSLOT 0003
    /// 03 : OpCode.LDARG0
    /// 04 : OpCode.LDARG1
    /// 05 : OpCode.LDARG2
    /// 06 : OpCode.OVER
    /// 07 : OpCode.OVER
    /// 08 : OpCode.JMPLE 03
    /// 0A : OpCode.THROW
    /// 0B : OpCode.REVERSE3
    /// 0C : OpCode.MAX
    /// 0D : OpCode.MIN
    /// 0E : OpCode.RET
    /// </remarks>
    [DisplayName("clampULong")]
    public abstract BigInteger? ClampULong(BigInteger? value, BigInteger? min, BigInteger? max);

    /// <summary>
    /// Unsafe method
    /// </summary>
    /// <remarks>
    /// Script: VwADeHl6S0syAzpTurlA
    /// 00 : OpCode.INITSLOT 0003
    /// 03 : OpCode.LDARG0
    /// 04 : OpCode.LDARG1
    /// 05 : OpCode.LDARG2
    /// 06 : OpCode.OVER
    /// 07 : OpCode.OVER
    /// 08 : OpCode.JMPLE 03
    /// 0A : OpCode.THROW
    /// 0B : OpCode.REVERSE3
    /// 0C : OpCode.MAX
    /// 0D : OpCode.MIN
    /// 0E : OpCode.RET
    /// </remarks>
    [DisplayName("clampUShort")]
    public abstract BigInteger? ClampUShort(BigInteger? value, BigInteger? min, BigInteger? max);

    /// <summary>
    /// Unsafe method
    /// </summary>
    /// <remarks>
    /// Script: VwACeXhKEk2hU6ISwEA=
    /// 00 : OpCode.INITSLOT 0002
    /// 03 : OpCode.LDARG1
    /// 04 : OpCode.LDARG0
    /// 05 : OpCode.DUP
    /// 06 : OpCode.PUSH2
    /// 07 : OpCode.PICK
    /// 08 : OpCode.DIV
    /// 09 : OpCode.REVERSE3
    /// 0A : OpCode.MOD
    /// 0B : OpCode.PUSH2
    /// 0C : OpCode.PACK
    /// 0D : OpCode.RET
    /// </remarks>
    [DisplayName("divRemByte")]
    public abstract IList<object>? DivRemByte(BigInteger? left, BigInteger? right);

    /// <summary>
    /// Unsafe method
    /// </summary>
    /// <remarks>
    /// Script: VwACeXhKEk2hU6ISwEA=
    /// 00 : OpCode.INITSLOT 0002
    /// 03 : OpCode.LDARG1
    /// 04 : OpCode.LDARG0
    /// 05 : OpCode.DUP
    /// 06 : OpCode.PUSH2
    /// 07 : OpCode.PICK
    /// 08 : OpCode.DIV
    /// 09 : OpCode.REVERSE3
    /// 0A : OpCode.MOD
    /// 0B : OpCode.PUSH2
    /// 0C : OpCode.PACK
    /// 0D : OpCode.RET
    /// </remarks>
    [DisplayName("divRemInt")]
    public abstract IList<object>? DivRemInt(BigInteger? left, BigInteger? right);

    /// <summary>
    /// Unsafe method
    /// </summary>
    /// <remarks>
    /// Script: VwACeXhKEk2hU6ISwEA=
    /// 00 : OpCode.INITSLOT 0002
    /// 03 : OpCode.LDARG1
    /// 04 : OpCode.LDARG0
    /// 05 : OpCode.DUP
    /// 06 : OpCode.PUSH2
    /// 07 : OpCode.PICK
    /// 08 : OpCode.DIV
    /// 09 : OpCode.REVERSE3
    /// 0A : OpCode.MOD
    /// 0B : OpCode.PUSH2
    /// 0C : OpCode.PACK
    /// 0D : OpCode.RET
    /// </remarks>
    [DisplayName("divRemLong")]
    public abstract IList<object>? DivRemLong(BigInteger? left, BigInteger? right);

    /// <summary>
    /// Unsafe method
    /// </summary>
    /// <remarks>
    /// Script: VwACeXhKEk2hU6ISwEA=
    /// 00 : OpCode.INITSLOT 0002
    /// 03 : OpCode.LDARG1
    /// 04 : OpCode.LDARG0
    /// 05 : OpCode.DUP
    /// 06 : OpCode.PUSH2
    /// 07 : OpCode.PICK
    /// 08 : OpCode.DIV
    /// 09 : OpCode.REVERSE3
    /// 0A : OpCode.MOD
    /// 0B : OpCode.PUSH2
    /// 0C : OpCode.PACK
    /// 0D : OpCode.RET
    /// </remarks>
    [DisplayName("divRemSbyte")]
    public abstract IList<object>? DivRemSbyte(BigInteger? left, BigInteger? right);

    /// <summary>
    /// Unsafe method
    /// </summary>
    /// <remarks>
    /// Script: VwACeXhKEk2hU6ISwEA=
    /// 00 : OpCode.INITSLOT 0002
    /// 03 : OpCode.LDARG1
    /// 04 : OpCode.LDARG0
    /// 05 : OpCode.DUP
    /// 06 : OpCode.PUSH2
    /// 07 : OpCode.PICK
    /// 08 : OpCode.DIV
    /// 09 : OpCode.REVERSE3
    /// 0A : OpCode.MOD
    /// 0B : OpCode.PUSH2
    /// 0C : OpCode.PACK
    /// 0D : OpCode.RET
    /// </remarks>
    [DisplayName("divRemShort")]
    public abstract IList<object>? DivRemShort(BigInteger? left, BigInteger? right);

    /// <summary>
    /// Unsafe method
    /// </summary>
    /// <remarks>
    /// Script: VwACeXhKEk2hU6ISwEA=
    /// 00 : OpCode.INITSLOT 0002
    /// 03 : OpCode.LDARG1
    /// 04 : OpCode.LDARG0
    /// 05 : OpCode.DUP
    /// 06 : OpCode.PUSH2
    /// 07 : OpCode.PICK
    /// 08 : OpCode.DIV
    /// 09 : OpCode.REVERSE3
    /// 0A : OpCode.MOD
    /// 0B : OpCode.PUSH2
    /// 0C : OpCode.PACK
    /// 0D : OpCode.RET
    /// </remarks>
    [DisplayName("divRemUint")]
    public abstract IList<object>? DivRemUint(BigInteger? left, BigInteger? right);

    /// <summary>
    /// Unsafe method
    /// </summary>
    /// <remarks>
    /// Script: VwACeXhKEk2hU6ISwEA=
    /// 00 : OpCode.INITSLOT 0002
    /// 03 : OpCode.LDARG1
    /// 04 : OpCode.LDARG0
    /// 05 : OpCode.DUP
    /// 06 : OpCode.PUSH2
    /// 07 : OpCode.PICK
    /// 08 : OpCode.DIV
    /// 09 : OpCode.REVERSE3
    /// 0A : OpCode.MOD
    /// 0B : OpCode.PUSH2
    /// 0C : OpCode.PACK
    /// 0D : OpCode.RET
    /// </remarks>
    [DisplayName("divRemUlong")]
    public abstract IList<object>? DivRemUlong(BigInteger? left, BigInteger? right);

    /// <summary>
    /// Unsafe method
    /// </summary>
    /// <remarks>
    /// Script: VwACeXhKEk2hU6ISwEA=
    /// 00 : OpCode.INITSLOT 0002
    /// 03 : OpCode.LDARG1
    /// 04 : OpCode.LDARG0
    /// 05 : OpCode.DUP
    /// 06 : OpCode.PUSH2
    /// 07 : OpCode.PICK
    /// 08 : OpCode.DIV
    /// 09 : OpCode.REVERSE3
    /// 0A : OpCode.MOD
    /// 0B : OpCode.PUSH2
    /// 0C : OpCode.PACK
    /// 0D : OpCode.RET
    /// </remarks>
    [DisplayName("divRemUshort")]
    public abstract IList<object>? DivRemUshort(BigInteger? left, BigInteger? right);

    /// <summary>
    /// Unsafe method
    /// </summary>
    [DisplayName("max")]
    public abstract BigInteger? Max(BigInteger? a, BigInteger? b);

    /// <summary>
    /// Unsafe method
    /// </summary>
    [DisplayName("min")]
    public abstract BigInteger? Min(BigInteger? a, BigInteger? b);

    /// <summary>
    /// Unsafe method
    /// </summary>
    [DisplayName("sign")]
    public abstract BigInteger? Sign(BigInteger? a);

    #endregion
}
