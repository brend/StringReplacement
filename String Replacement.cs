public static class StringReplacement
{
	public static void Replace( 
		string inputFile, 
		string outputFile, 
		string suchtext, 
		string ersatztext,
		int bufferSize = 1024 * 1024,
		StringComparison? inComparison = null
	)
	{
		char[] buffer = new char[bufferSize];
		string s = "";
		StringComparison comparison = inComparison ?? StringComparison.OrdinalIgnoreCase;

		using (StreamReader vReader = new StreamReader(inputFile))
		{
			using (StreamWriter vWriter = new StreamWriter(outputFile))
			{
				while (!vReader.EndOfStream)
				{
					int bytesRead = vReader.ReadBlock(buffer, 0, buffer.Length);

					s += new string(buffer, 0, bytesRead);
					s = s.Replace(suchtext, ersatztext, comparison);

					string match = Matches(s, suchtext);

					vWriter.Write(s.Substring(0, s.Length - match.Length));

					if (!string.IsNullOrEmpty(match))
					{
						s = match;
					}
					else
					{
						s = "";
					}
				}
			}
		}
	}

	private static string Matches(string s, string suchtext)
	{
		while (suchtext.Length > 0)
		{
			if (s.EndsWith(suchtext))
			{
				return suchtext;
			}
			
			suchtext = suchtext.Substring(0, suchtext.Length - 1);
		}
		
		return suchtext;
	}
}