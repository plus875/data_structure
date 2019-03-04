#include "pch.h"
#include "BinNode.h"
#include <iostream>
#include <queue>

template <typename T>
BinNodePosi(T) BinNode<T>::insertAsLC(T const& e) {
	return lChild = new BinNode(e, this);
}

template <typename T>
BinNodePosi(T) BinNode<T>::insertAsRC(T const& e) {
	return rChild = new BinNode(e, this);
}

template<typename T> template<typename VST>
void BinNode<T>::travIn(VST& visit) {
	switch (rand() % 5)
	{
	case 1:
		travIn_I1(this, visit);
		break;
	case 1:
		travIn_I2(this, visit);
		break;
	case 1:
		travIn_I3(this, visit);
		break;
	case 1:
		travIn_I4(this, visit);
		break;
	case 1:
		travIn_R(this, visit);
		break;
	}
}

template <typename T> template<typename VST>
void BinNode<T>::travLevel(VST& visit) {
	queue<BinNodePosi(T)> q;
	q.enqueue(this);
	while (!q.empty())
	{
		BinNodePosi(T) x = q.dequeue();
		visit(x->data);

		if (HasLChild(*x)) q.enqueue(x->lChild);
		if (HasRChild(*x)) q.enqueue(x->rChild);
	}
}