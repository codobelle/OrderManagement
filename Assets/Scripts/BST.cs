using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace BST 
{
    public class BSTNode
    {
        private int nodeID;
        private object data;

        public T GetData<T>()
        {
            return (T)data;
        }

        public void SetData<T>(T newData, int id)
        {
            data = newData;
            nodeID = id;
        }

        //Right Child
        private BSTNode rightNode;
        public BSTNode RightNode
        {
            get { return rightNode; }
            set { rightNode = value; }
        }

        //Left Child
        private BSTNode leftNode;
        public BSTNode LeftNode
        {
            get { return leftNode; }
            set { leftNode = value; }
        }

        //Node constructor
        public BSTNode(int id, object value)
        {
            data = value;
            nodeID = id;
        }

        //recursively calls insert down the tree until it find an open spot
        public void Insert(int id, object value)
        {
            bool insertRightNode = id >= nodeID;
            //if the value passed in is greater or equal to the data then insert to right node
            if (insertRightNode)
            {   //if right child node is null create one
                if (rightNode == null)
                {
                    rightNode = new BSTNode(id, value);
                }
                else
                {//if right node is not null recursivly call insert on the right node
                    rightNode.Insert(id, value);
                }
            }
            else
            {//if the value passed in is less than the data then insert to left node
                if (leftNode == null)
                {//if the leftnode is null then create a new node
                    leftNode = new BSTNode(id, value);
                }
                else
                {//if the left node is not null then recursively call insert on the left node
                    leftNode.Insert(id, value);
                }
            }
        }

        public BSTNode Find(int id)
        {
            //this node is the starting current node
            BSTNode currentNode = this;
            //loop through this node and all of the children of this node
            while (currentNode != null)
            {
                //if the current nodes data is equal to the value passed in return it
                if (id == currentNode.nodeID)
                {
                    return currentNode;
                }
                else if (id > currentNode.nodeID)//if the value passed in is greater than the current data then go to the right child
                {
                    currentNode = currentNode.rightNode;
                }
                else//otherwise if the value is less than the current nodes data the go to the left child node 
                {
                    currentNode = currentNode.leftNode;
                }
            }
            //Node not found
            return null;
        }

        //Root->Left->Right Nodes recursively of each subtree 
        public void PreOrderTraversal<T>(List<T> nodes)
        {
            //First we print the root node 
            Debug.Log(data + " id: " + nodeID);
            nodes.Add((T)data);
            //Then go to left child its children will be null so we print its data
            if (leftNode != null)
                leftNode.PreOrderTraversal(nodes);

            //Then we go to the right node which will print itself as both its children are null
            if (rightNode != null)
                rightNode.PreOrderTraversal(nodes);
        }
    }

    public class BSTTree
    {
        private BSTNode root;
        public BSTNode Root
        {
            get { return root; }
        }
        
        //O(Log n)
        public BSTNode Find(int id)
        {
            //if the root is not null then we call the find method on the root
            if (root != null)
            {
                // call node method Find
                return root.Find(id);
            }
            else
            {//the root is null so we return null, nothing to find
                return null;
            }
        }

        //O(Log n)
        public void Insert(int id, object data)
        {
            //if the root is not null then we call the Insert method on the root node
            if (root != null)
            {
                root.Insert(id, data);
            }
            else
            {//if the root is null then we set the root to be a new node based on the data passed in
                root = new BSTNode(id, data);
            }
        }

        public void PreorderTraversal<T>(List<T> nodes)
        {
            if (root != null)
                root.PreOrderTraversal(nodes);
        }
    }
}
