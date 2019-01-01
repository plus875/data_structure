#include "pch.h"
#include "BinNode.h"
#include <iostream>

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