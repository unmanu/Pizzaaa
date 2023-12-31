﻿using Pizzaaa.BLL.Models.Exceptions;

namespace Pizzaaa.BLL.Utils;

public static class HexUtils
{
	public static byte[] HexToByteArray(string hex)
	{
		if (hex.Length % 2 == 1)
			throw new BllException("The binary key cannot have an odd number of digits");

		byte[] arr = new byte[hex.Length >> 1];

		for (int i = 0; i < hex.Length >> 1; ++i)
		{
			arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
		}

		return arr;
	}

	public static int GetHexVal(char hex)
	{
		int val = (int)hex;
		//For uppercase A-F letters:
		//return val - (val < 58 ? 48 : 55);
		//For lowercase a-f letters:
		//return val - (val < 58 ? 48 : 87);
		//Or the two combined, but a bit slower:
		int forLowerCase = val < 97 ? 55 : 87;
		return val - (val < 58 ? 48 : forLowerCase);
	}
}