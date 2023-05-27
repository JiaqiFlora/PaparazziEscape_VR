using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class TrackTreeNode
{
    public PathCreator pathCreator;
    public TrackTreeNode left;
    public TrackTreeNode right;
    public TrackTreeNode parent;

    public TrackTreeNode(PathCreator pathCreator, TrackTreeNode left = null, TrackTreeNode right = null, TrackTreeNode parent = null)
    {
        this.pathCreator = pathCreator;
        this.left = left;
        this.right = right;
        this.parent = parent;
    }
}

public class TrackManager : MonoBehaviour
{
    public static TrackManager instance { get; private set; }

    // store all pathCreators in tree's level order(bfs)!!!
    public List<PathCreator> pathCreatorsList;
    public TrackTreeNode pathTreeRoot;

    private List<TrackTreeNode> treeNodeLists = new List<TrackTreeNode>();


    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        } else
        {
            instance = this;
        }
    }

    private void Start()
    {
        ConstructTreeHardCoded();
    }

    // hard code to construct the tree
    private void ConstructTreeHardCoded()
    {
        for(int i = 0; i < pathCreatorsList.Count; ++i)
        {
            TrackTreeNode node = new TrackTreeNode(pathCreatorsList[i]);
            treeNodeLists.Add(node);
        }

        treeNodeLists[0].left = treeNodeLists[1];

        treeNodeLists[1].left = treeNodeLists[2];
        treeNodeLists[1].right = treeNodeLists[3];
        treeNodeLists[1].parent = treeNodeLists[0];

        treeNodeLists[2].left = treeNodeLists[4];
        treeNodeLists[2].parent = treeNodeLists[1];

        treeNodeLists[3].left = treeNodeLists[4];
        treeNodeLists[3].parent = treeNodeLists[1];

        // TODO: - ganjiaqi later to fix the parent changing problem!
        treeNodeLists[4].left = treeNodeLists[5];
        treeNodeLists[4].right = treeNodeLists[6];
        treeNodeLists[4].parent = treeNodeLists[2];

        treeNodeLists[5].parent = treeNodeLists[4];

        treeNodeLists[6].left = treeNodeLists[7];
        treeNodeLists[6].right = treeNodeLists[8];
        //treeNodeLists[6].parent = treeNodeLists[4];

        treeNodeLists[7].left = treeNodeLists[9];
        treeNodeLists[7].right = treeNodeLists[10];
        treeNodeLists[7].parent = treeNodeLists[6];

        treeNodeLists[8].left = treeNodeLists[11];
        treeNodeLists[8].right = treeNodeLists[12];
        treeNodeLists[8].parent = treeNodeLists[6];

        treeNodeLists[9].parent = treeNodeLists[7];

        treeNodeLists[10].left = treeNodeLists[13];
        treeNodeLists[10].right = treeNodeLists[14];
        treeNodeLists[10].parent = treeNodeLists[7];

        treeNodeLists[11].left = treeNodeLists[15];
        treeNodeLists[11].right = treeNodeLists[16];
        treeNodeLists[11].parent = treeNodeLists[8];

        treeNodeLists[12].parent = treeNodeLists[8];

        treeNodeLists[13].parent = treeNodeLists[10];

        treeNodeLists[14].parent = treeNodeLists[10];

        treeNodeLists[15].parent = treeNodeLists[11];

        treeNodeLists[16].parent = treeNodeLists[11];

        // now, start from the middle of the map!
        pathTreeRoot = treeNodeLists[6];
        //pathTreeRoot = treeNodeLists[0];

    }

    // BFS - level search to construct a tree.
    // give up this way, cause there exist recycle!! can fix later for full logic, to fit in the recycle
    private void ConstructTree()
    {
        if(pathCreatorsList == null || pathCreatorsList.Count == 0)
        {
            return;
        }

        TrackTreeNode root = new TrackTreeNode(pathCreatorsList[0]);
        pathTreeRoot = root;
        Queue<TrackTreeNode> queue = new Queue<TrackTreeNode>();
        queue.Enqueue(root);

        for (int i = 1; i < pathCreatorsList.Count; i += 2)
        {
            TrackTreeNode node = queue.Dequeue();

            if (pathCreatorsList[i] != null)
            {
                TrackTreeNode leftNode = new TrackTreeNode(pathCreatorsList[i]);
                leftNode.parent = node;
                node.left = leftNode;

                queue.Enqueue(leftNode);
            }

            if (i + 1 < pathCreatorsList.Count && pathCreatorsList[i + 1] != null)
            {
                TrackTreeNode rightNode = new TrackTreeNode(pathCreatorsList[i + 1]);
                rightNode.parent = node;
                node.right = rightNode;

                queue.Enqueue(rightNode);
            }
        }

        Debug.Log($"finish construct, count: {pathCreatorsList.Count}, root is null: {pathTreeRoot == null}");
    }
}
