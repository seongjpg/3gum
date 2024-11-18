using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
	[SerializeField] Vector2Int mapSize; //���ϴ� ���� ũ��
	[SerializeField] float minimumDevideRate; //������ �������� �ּ� ����
	[SerializeField] float maximumDivideRate; //������ �������� �ִ� ����
	[SerializeField] private GameObject line; //lineRenderer�� ����ؼ� ������ �������� ���������� �����ֱ� ����
	[SerializeField] private GameObject map; //lineRenderer�� ����ؼ� ù ���� ����� �����ֱ� ����
	[SerializeField] private GameObject roomLine; //lineRenderer�� ����ؼ� ���� ����� �����ֱ� ����
	[SerializeField] private int maximumDepth; //Ʈ���� ����, ���� ���� ���� �� �ڼ��� ������ ��
	void Start()
	{
		Node root = new Node(new RectInt(0, 0, mapSize.x, mapSize.y)); //��ü �� ũ���� ��Ʈ��带 ����
		DrawMap(0, 0);
		Divide(root, 0);
	}



	private void DrawMap(int x, int y) //x y�� ȭ���� �߾���ġ�� ����
	{
		//�⺻������ mapSize/2��� ���� ����ؼ� ���� �ɰǵ�, ȭ���� �߾ӿ��� ȭ���� ũ���� ���� ����� ���� �ϴ���ǥ�� ���� �� �ֱ� �����̴�.
		LineRenderer lineRenderer = Instantiate(map).GetComponent<LineRenderer>();
		lineRenderer.SetPosition(0, new Vector2(x, y) - mapSize / 2); //���� �ϴ�
		lineRenderer.SetPosition(1, new Vector2(x + mapSize.x, y) - mapSize / 2); //���� �ϴ�
		lineRenderer.SetPosition(2, new Vector2(x + mapSize.x, y + mapSize.y) - mapSize / 2);//���� ���
		lineRenderer.SetPosition(3, new Vector2(x, y + mapSize.y) - mapSize / 2); //���� ���

	}
	void Divide(Node tree, int n)
	{
		if (n == maximumDepth) return; //���� ���ϴ� ���̿� �����ϸ� �� �������� �ʴ´�.

		//�� ���� ��쿡��

		int maxLength = Mathf.Max(tree.nodeRect.width, tree.nodeRect.height);
		//���ο� ������ �� ����� ������, ���ΰ� ��ٸ� �� ��, ��� ���ΰ� �� ��ٸ� ��, �Ʒ��� �����ְ� �� ���̴�.
		int split = Mathf.RoundToInt(Random.Range(maxLength * minimumDevideRate, maxLength * maximumDivideRate));
		//���� �� �ִ� �ִ� ���̿� �ּ� �����߿��� �������� �� ���� ����
		if (tree.nodeRect.width >= tree.nodeRect.height) //���ΰ� �� ����� ��쿡�� �� ��� ������ �� ���̸�, �� ��쿡�� ���� ���̴� ������ �ʴ´�.
		{
			tree.leftNode = new Node(new RectInt(tree.nodeRect.x, tree.nodeRect.y, split, tree.nodeRect.height));
			//���� ��忡 ���� ������ 
			//��ġ�� ���� �ϴ� �����̹Ƿ� ������ ������, ���� ���̴� ������ ���� �������� �־��ش�.
			tree.rightNode = new Node(new RectInt(tree.nodeRect.x + split, tree.nodeRect.y, tree.nodeRect.width - split, tree.nodeRect.height));
			//���� ��忡 ���� ������ 
			//��ġ�� ���� �ϴܿ��� ���������� ���� ���̸�ŭ �̵��� ��ġ�̸�, ���� ���̴� ���� ���α��̿��� ���� ���� ���ΰ��� �� ������ �κ��� �ȴ�.
			DrawLine(new Vector2(tree.nodeRect.x + split, tree.nodeRect.y), new Vector2(tree.nodeRect.x + split, tree.nodeRect.y + tree.nodeRect.height));
			//�� �� �� �ΰ��� ��带 ������ ���� �׸��� �Լ��̴�.        
		}
		else
		{
			tree.leftNode = new Node(new RectInt(tree.nodeRect.x, tree.nodeRect.y, tree.nodeRect.width, split));
			tree.rightNode = new Node(new RectInt(tree.nodeRect.x, tree.nodeRect.y + split, tree.nodeRect.width, tree.nodeRect.height - split));
			DrawLine(new Vector2(tree.nodeRect.x, tree.nodeRect.y + split), new Vector2(tree.nodeRect.x + tree.nodeRect.width, tree.nodeRect.y + split));
			//���ΰ� �� ����� ����̴�. �ڼ��� ������ ���ο� ����.
		}
		tree.leftNode.parNode = tree; //�ڽĳ����� �θ��带 �������� ���� ����
		tree.rightNode.parNode = tree;
		Divide(tree.leftNode, n + 1); //����, ������ �ڽ� ���鵵 �����ش�.
		Divide(tree.rightNode, n + 1);//����, ������ �ڽ� ���鵵 �����ش�.
	}
	private void DrawLine(Vector2 from, Vector2 to) //from->to�� �̾����� ���� �׸��� �� ���̴�.
	{
		LineRenderer lineRenderer = Instantiate(line).GetComponent<LineRenderer>();
		lineRenderer.SetPosition(0, from - mapSize / 2);
		lineRenderer.SetPosition(1, to - mapSize / 2);
	}
}