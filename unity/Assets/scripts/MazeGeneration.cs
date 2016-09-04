using UnityEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MazeGeneration {
	public static class Extensions {
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, System.Random rng) {
			var e = source.ToArray();
			for (var i = e.Length - 1; i >= 0; i--) {
				var swapIndex = rng.Next(i + 1);
				yield return e[swapIndex];
				e[swapIndex] = e[i];
			}
		}

		public static CellState OppositeWall(this CellState orig) {
			return (CellState)(((int) orig >> 2) | ((int) orig << 2)) & CellState.Initial;
		}

		public static bool HasFlag(this CellState cs,CellState flag) {
			return ((int)cs & (int)flag) != 0;
		}
	}

	[Flags]
	public enum CellState {
		Top = 1,
		Right = 2,
		Bottom = 4,
		Left = 8,
		Visited = 128,
		Initial = Top | Right | Bottom | Left,
	}

	public struct RemoveWallAction {
		public Point Neighbour;
		public CellState Wall;
	}

	public class Maze {
		private readonly CellState[,] _cells;
		public readonly int _width;
		public readonly int _height;
		private readonly System.Random _rng;

		public Point startPoint = new Point (0,0);

		public Maze(int width, int height, Vector3 start) {
			_width = width;
			_height = height;
			_cells = new CellState[width, height];
			for(var x=0; x<width; x++)
				for(var y=0; y<height; y++)
					_cells[x, y] = CellState.Initial;
			_rng = new System.Random();
			startPoint.X = (int)start.x;
			startPoint.Y = (int)start.y;
			VisitCell(startPoint.X, startPoint.Y);
		}

		public CellState this[int x, int y] {
			get { return _cells[x,y]; }
			set { _cells[x,y] = value; }
		}

		public IEnumerable<RemoveWallAction> GetNeighbours(Point p) {
			if (p.X > 0) yield return new RemoveWallAction {Neighbour = new Point(p.X - 1, p.Y), Wall = CellState.Left};
			if (p.Y > 0) yield return new RemoveWallAction {Neighbour = new Point(p.X, p.Y - 1), Wall = CellState.Top};
			if (p.X < _width-1) yield return new RemoveWallAction {Neighbour = new Point(p.X + 1, p.Y), Wall = CellState.Right};
			if (p.Y < _height-1) yield return new RemoveWallAction {Neighbour = new Point(p.X, p.Y + 1), Wall = CellState.Bottom};
		}

		public void VisitCell(int x, int y) {
			this[x,y] |= CellState.Visited;
			foreach (var p in GetNeighbours(new Point(x, y)).Shuffle(_rng).Where(z => !(this[z.Neighbour.X, z.Neighbour.Y].HasFlag(CellState.Visited)))){
				this[x, y] -= p.Wall;
				this[p.Neighbour.X, p.Neighbour.Y] -= p.Wall.OppositeWall();
				VisitCell(p.Neighbour.X, p.Neighbour.Y);
			}
		}

		public void Display() {
			String result = "";
			var firstLine = string.Empty;
			for (var y = 0; y < _height; y++){
				var sbTop = new StringBuilder();
				var sbMid = new StringBuilder();
				for (var x = 0; x < _width; x++){
					sbTop.Append(this[x, y].HasFlag(CellState.Top) ? "+--" : "+  ");
					sbMid.Append(this[x, y].HasFlag(CellState.Left) ? "|  " : "   ");
				}
				if (firstLine == string.Empty) {
					System.Diagnostics.Debug.WriteLine (firstLine = sbTop.ToString ());
					result += sbTop.ToString () + "\n";
				}
				result += sbTop + "+" + "\n";
				result += sbMid + "|" + "\n";
				result += sbMid + "|" + "\n";
				System.Diagnostics.Debug.WriteLine(sbTop + "+");
				System.Diagnostics.Debug.WriteLine(sbMid + "|");
				System.Diagnostics.Debug.WriteLine(sbMid + "|");
			}
			result += firstLine + "\n";
			System.Diagnostics.Debug.WriteLine(firstLine);
			UnityEngine.Debug.Log (result);
			UnityEngine.Debug.Log ("ENDEEE");
		}
	}

	public class Point{
		public int X;
		public int Y;

		public Point(){

		}

		public Point(int x, int y){
			this.X = x;
			this.Y = y;
		}
	}
}

