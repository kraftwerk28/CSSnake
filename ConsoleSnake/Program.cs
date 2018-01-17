using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleSnake
{
	class Program
	{
		static List<int[]> body = new List<int[]>();
		static bool[,] grid;

		static int Length = 4;

		static int width = 50;
		static int height = 30;
		static int dx = 0;
		static int dy = 0;
		static int posx = width / 2;
		static int posy = height / 2;
		static int fx;
		static int fy;
		static Random rnd = new Random();

		static Timer MainClock = new Timer(200);
		static Timer ConsClear = new Timer(5000);

		static void Main(string[] args)
		{
			Console.Title = "Console SNAKE";
			Console.CursorVisible = false;
			Console.SetWindowSize((width - 1) * 2 + 20, height + 1);
			Console.ForegroundColor = ConsoleColor.Green;
			
			grid = new bool[width, height];

			MainClock.Elapsed += MainClock_Elapsed;
			MainClock.Start();


			ConsClear.Elapsed += ConsClear_Elapsed;
			ConsClear.Start();

			fx = rnd.Next(width);
			fy = rnd.Next(height);
			while (true)
			{
				ConsoleKey e = Console.ReadKey(true).Key;
				switch (e)
				{
					case ConsoleKey.UpArrow:
						if (dy != 1)
						{
							//Draw();
							dx = 0;
							dy = -1;
						}
						break;
					case ConsoleKey.DownArrow:
						if (dy != -1)
						{
							//Draw();
							dx = 0;
							dy = 1;
						}
						break;
					case ConsoleKey.LeftArrow:
						if (dx != 1)
						{
							//Draw();
							dx = -1;
							dy = 0;
						}
						break;
					case ConsoleKey.RightArrow:
						if (dx != -1)
						{
							//Draw();
							dx = 1;
							dy = 0;
						}
						break;
				}

			}
		}

		private static void ConsClear_Elapsed(object sender, ElapsedEventArgs e)
		{
			//throw new NotImplementedException();
			Console.Clear();
		}

		private static void MainClock_Elapsed(object sender, ElapsedEventArgs e)
		{
			//throw new NotImplementedException();
			CheckCol();

			int[] cell = { posx + dx, posy + dy };
			posx += dx;
			posy += dy;
			body.Add(cell);

			if (body.Count > Length)
				body.Remove(body[0]);
			Draw();
		}

		private static void CheckCol()
		{
			if (posx + dx < 0)
			{
				GameOver();
			}
			if (posx + dx > width - 1)
			{
				GameOver();
			}
			if (posy + dy < 0)
			{
				GameOver();
			}
			if (posy + dy > height - 1)
			{
				GameOver();
			}

			//if (posx + dx < 0)
			//	posx = width;
			//if (posx + dx > width - 1)
			//	posx = -1;
			//if (posy + dy < 0)
			//	posy = height;
			//if (posy + dy > height - 1)
			//	posy = -1;
			if (dx != 0 || dy != 0)
			{
				for (int i = 0; i < body.Count - 2; i++)
				{
					if (posx + dx == body[i][0] && posy + dy == body[i][1])
					{
						GameOver();
					}
				}
			}
            


			if (posx + dx == fx && posy + dy == fy)
			{
				SpawnFood();
				Length++;
			}
		}

		private static void Draw()
		{
			//Console.Clear();
			Console.SetCursorPosition(0, 0);
			grid = new bool[height, width];
			foreach (int[] c in body)
			{
				grid[c[1], c[0]] = true;
			}
			Console.ForegroundColor = ConsoleColor.Green;
			for (int y = 0; y < grid.GetLength(0); y++)
			{
				for(int x = 0; x < grid.GetLength(1); x++)
				{
					if (grid[y, x])
					{
						Console.Write("██");
					}
					else
					{
						Console.Write("░░");
					}
				}
				Console.Write("\n");
			}

			Console.SetCursorPosition(2 * fx, fy);
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("▓▓");

			Console.SetCursorPosition(2 * posx, posy);
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write("██");

			Console.SetCursorPosition(2 * width + 1, 1);
			Console.Write("Score: {0}", Length - 4);
		}

		private static void SpawnFood()
		{
			bool onBody;
			do
			{
				onBody = false;
				fx = rnd.Next(width);
				fy = rnd.Next(height);
				foreach (int[] b in body)
				{
					if (b[0] == fx && b[1] == fy)
					{
						onBody = true;
						break;
					}

				}

			} while (onBody);

		}

		private static void GameOver()
		{
			MainClock.Stop();
			Console.Beep(1000, 1000);
			//System.Threading.Thread.Sleep(1000);
			Environment.Exit(0);
		}
	}
}
