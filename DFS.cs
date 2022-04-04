using System;
using System.Collections.Generic;

public class Program
{
	public static void Main()
	{
		Queue<int> que = new Queue<int>();
		Graph g = new Graph(4);
		g.addEdge(0, 1);
		g.addEdge(0, 2);
		g.addEdge(1, 2);
		g.addEdge(2, 0);
		g.addEdge(2, 3);
		g.addEdge(3, 3);
		g.BFS(2);
	}
}

public class Graph
{
	int V; //počet vrcholů
	List<int>[] adj;
	public Graph(int v)
	{
		V = v;
		adj = new List<int>[v];
		for (int i = 0; i < v; i++)
		{
			adj[i] = new List<int>();
		}
	}

	public void addEdge(int v, int w)
	{
		adj[v].Add(w);
	}

	public void BFS(int s)
	{
		bool[] visited = new bool[V];
		for (int i = 0; i < V; i++)
			visited[i] = false;
		Queue<int> queue = new Queue<int>();
		queue.Enqueue(s);
		visited[s] = true;
		while (queue.Count != 0)
		{
			int pop = queue.Dequeue();
			Console.Write(pop + " ");
			foreach (var vert in adj[pop])
			{
				if (!visited[vert])
				{
					visited[vert] = true;
					queue.Enqueue(vert);
				}
			}
		}
	}
}