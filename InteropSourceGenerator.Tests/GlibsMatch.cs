using System.Runtime.InteropServices;

namespace PInvokeInclude;
internal static class GlibsMatch
{
	[DllImport("math.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern double round(double arg1);
}
