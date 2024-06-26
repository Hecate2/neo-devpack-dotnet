using System.Threading.Tasks;
using Xunit;
using Verifier = Microsoft.CodeAnalysis.CSharp.Testing.XUnit.CodeFixVerifier<
    Neo.SmartContract.Analyzer.BigIntegerUsingUsageAnalyzer,
    Neo.SmartContract.Analyzer.BigIntegerUsingUsageCodeFixProvider>;

namespace Neo.SmartContract.Analyzer.UnitTests
{

    public class BigIntegerUsingUsageAnalyzerUnitTest
    {
        [Fact]
        public async Task BigIntegerUsingUsageAnalyzer_IncorrectUsing_ShouldReportDiagnostic()
        {
            const string originalCode = """
                                        using BigInteger = System.Int64;

                                        public class TestClass
                                        {
                                            public void TestMethod()
                                            {
                                                BigInteger value = 42;
                                            }
                                        }
                                        """;
            var expectedDiagnostic = Verifier.Diagnostic(BigIntegerUsingUsageAnalyzer.DiagnosticId)
                .WithSpan(1, 1, 1, 33).WithArguments("using BigInteger = System.Int64;");

            await Verifier.VerifyAnalyzerAsync(originalCode, expectedDiagnostic).ConfigureAwait(false);
        }

        [Fact]
        public async Task BigIntegerUsingUsageAnalyzer_CorrectUsing_ShouldNotReportDiagnostic()
        {
            const string originalCode = """
                                        using BigInteger = System.Numerics.BigInteger;

                                        public class TestClass
                                        {
                                            public void TestMethod()
                                            {
                                                BigInteger value = 42;
                                            }
                                        }
                                        """;
            await Verifier.VerifyAnalyzerAsync(originalCode).ConfigureAwait(false);
        }
    }
}
