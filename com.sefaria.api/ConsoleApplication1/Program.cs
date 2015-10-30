﻿#region Copyright (C) 2015 Oren Fromberg

//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see http://www.gnu.org/licenses/.

#endregion

using System;
using System.Diagnostics;
using com.sefaria.api.Clients;
using Sefaria = com.sefaria.api;

namespace ConsoleApplication1
{
	/// <summary>
	/// test program. once there is a nuget package, i'll remove this from the repo.
	/// </summary>
	class Program
	{
		static void Main(string[] args)
		{
			var client = new SefariaClient();

			Console.WriteLine(client.ToString());

			while(true)
			{
				Console.Write("> ");
				string input = Console.ReadLine();
				if (input != null)
				{
					if (input.Equals("quit"))
						break;

					if (input.Equals("titles"))
					{
						var index = client.Index.GetTitles();
						foreach (var title in index.Books)
						{
							Console.WriteLine(title);
						}
						continue;
					}

					if (input.Equals("contents"))
					{
						var indices = client.Index.GetContents();
						//todo print out index
						Console.WriteLine(indices.ToString());
						continue;
					}

					// todo output specific indices (e.g. genesis)
				}

				try
				{
					var text = client.Texts.GetText(input);
					var links = client.Links.GetLinks(input);
					if (text.Error != null)
					{
						Console.WriteLine(text.Error);
					}
					else
					{
						// texts
						for (int i = 0; i < text.TextList.Count; i++)
						{
							Console.WriteLine(text.TextList[i]);
							Debug.WriteLine(text.HebrewTextList[i]);
						}	
					
						// links
						foreach (var link in links)
						{
							Console.WriteLine(link.Ref);
						}
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
		}
	}
}
