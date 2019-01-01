#include "pch.h"
#include "BinTree.h"

using namespace std;

template <typename T>
int BinTree<T>::updateHeight(BinNodePosi(T) x) {
	x->height = 1 + max(stature(x->lChild), stature(x->rChild));
	return x->height;
}

template <typename T>
void BinTree<T>::updateHeightAbove(BinNodePosi(T) x) {
	while (x)
	{
		updateHeight(x);
		x = x->parent;
	}
}

template <typename T>
BinNodePosi(T) BinTree<T>::insertAsRoot(T const& e) {
	_size = 1;
	return _root = new BinNode<T>(e);
}

template <typename T>
BinNodePosi(T) BinTree<T>::insertAsLC(BinNodePosi(T) x, T const& e) {
	_size++;
	x->insertAsLC(e);
	return x->lChild;
}
template <typename T>
BinNodePosi(T) BinTree<T>::insertAsRC(BinNodePosi(T) x, T const& e) {
	_size++;
	x->insertAsRC(e);
	return x->rChild;
}

template <typename T>
BinNodePosi(T) BinTree<T>::attachAsLC(BinNodePosi(T) x, BinTree<T>* &s) {
	if (x->lChild = s->root) {
		x->lChild->parent = x;
	}
	_size += s->size;
	updateHeightAbove(x);

	s->root = NULL;
	s->size = 0;
	release(s);
	s = NULL;
	return x;
}