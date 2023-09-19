﻿using System;
using System.Collections.Generic;

/*--------脚本描述-----------
				
电子邮箱：
	1607388033@qq.com
作者:
	暗沉
描述:
	有限状态机

-----------------------*/

namespace Core
{
    public class StateMachineSystem
	{
		private readonly Dictionary<string, IStateNode> _nodes = new Dictionary<string, IStateNode>(100);
		private IStateNode _curNode;
		private IStateNode _preNode;



		/// <summary>
		/// 状态机持有者
		/// </summary>
		public System.Object Owner { private set; get; }
		/// <summary>
		/// 当前运行的节点名称
		/// </summary>
		public string CurrentNode
		{
			get { return _curNode != null ? _curNode.GetType().FullName : string.Empty; }
		}
		/// <summary>
		/// 之前运行的节点名称
		/// </summary>
		public string PreviousNode
		{
			get { return _preNode != null ? _preNode.GetType().FullName : string.Empty; }
		}


		private StateMachineSystem() { }
		public StateMachineSystem(System.Object owner)
		{
			Owner = owner;
		}



        //更新状态机
        public void Update()
		{
			if (_curNode != null)
				_curNode.OnUpdate();
		}



        //启动状态机
        public void Run<TNode>() where TNode : IStateNode
		{
			var nodeType = typeof(TNode);
			var nodeName = nodeType.FullName;
			Run(nodeName);
		}
		public void Run(Type entryNode)
		{
			var nodeName = entryNode.FullName;
			Run(nodeName);
		}
		public void Run(string entryNode)
		{
			_curNode = TryGetNode(entryNode);
			_preNode = _curNode;

			if (_curNode == null)
				throw new Exception($"Not found entry node: {entryNode }");

			_curNode.OnEnter();
		}



        //加入一个节点
        public void AddNode<TNode>() where TNode : IStateNode
		{
			var nodeType = typeof(TNode);
			var stateNode = Activator.CreateInstance(nodeType) as IStateNode;
			AddNode(stateNode);
		}
		public void AddNode(IStateNode stateNode)
		{
			if (stateNode == null)
				throw new ArgumentNullException();

			var nodeType = stateNode.GetType();
			var nodeName = nodeType.FullName;

			if (_nodes.ContainsKey(nodeName) == false)
			{
				stateNode.OnCreate(this);
				_nodes.Add(nodeName, stateNode);
			}
			else
			{
                Debug.Error($"State node already existed : {nodeName}");
			}
		}



        //转换状态节点
        public void ChangeState<TNode>() where TNode : IStateNode
		{
			var nodeType = typeof(TNode);
			var nodeName = nodeType.FullName;
			ChangeState(nodeName);
		}
		public void ChangeState(Type nodeType)
		{
			var nodeName = nodeType.FullName;
			ChangeState(nodeName);
		}
		public void ChangeState(string nodeName)
		{
			if (string.IsNullOrEmpty(nodeName))
				throw new ArgumentNullException();

			IStateNode node = TryGetNode(nodeName);
			if (node == null)
			{
                Debug.Error($"Can not found state node : {nodeName}");
				return;
			}

			Debug.Log($"{_curNode.GetType().FullName} --> {node.GetType().FullName}");
			_preNode = _curNode;
			_curNode.OnExit();
			_curNode = node;
			_curNode.OnEnter();
		}

		private IStateNode TryGetNode(string nodeName)
		{
			_nodes.TryGetValue(nodeName, out IStateNode result);
			return result;
		}
	}
}