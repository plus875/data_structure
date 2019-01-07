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

template <typename T>
BinNodePosi(T) BinTree<T>::attachAsRC(BinNodePosi(T) x, BinTree<T>* &s) {
	if (x->rChild = s->root) {
		x->rChild->parent = x;
	}
	_size += x->rChild->size;
	updateHeightAbove(s);

	s->root = NULL;
	s->size = 0;
	s = NULL;
	return x;
}

template <typename T>
int BinTree<T>::remove(BinNodePosi(T) x) {
	FromParentTo(*x) = NULL;
	updateHeight(x->parent);
	int n = removeAt(x);
	_size -= n;
	return n;
}

template <typename T>
static int removeAt(BinNodePosi(T) x) {
	if (!x) return 0;

	int n = 1 + removeAt(x->lChild) + removeAt(x->rChild);
	release(x->data);
	release(x);
	return n;
}

template <typename T>
BinTree<T>* BinTree<T>::secede(BinNodePosi(T) x) {
	FromParentTo(*x) = NULL;
	updateHeightAbove(x->parent);
	BinTree<T>* S = new BinTree();
	S->root = x;
	x->parent = NULL;
	return S;
}

template <typename T, typename VST>
void travPre_R(BinNodePosi(T) x, VST& visit) {
	if (!x) return;

	visit(x);
	travPre_R(x->lChild);
	travPre_R(x->rChild);
}

template <typename T, typename VST>
void travPost_R(BinNodePosi(T) x, VST& visit) {
	if (!x) return;

	travPost_R(x->lChild)
	travPost_R(x->rChild);
	visit(x->data);
}

template <typename T, typename VST>
void travIn_R(BinNodePosi(T) x, VST& visit) {
	if (!x) return;

	travIn_R(x->lChild);
	visit(x->data);
	travIn_R(x->rChild);
}

template <typename T, typename VST>
static void visitAlongLeftBranch(BinNodePosi(T) x, VST& visit, stack<BinNodePosi(T)>& S) {
	while (x)
	{
		visit(x->data);
		S.push(x->rChild);
		x = x->lChild;
	}
}

template <typename T, typename VST>
static void travPre_I2(BinNodePosi(T) x, VST& visit) {
	stack<BinNodePosi(T)> stack;
	while (true)
	{
		visitAlongLeftBranch(x, visit, stack);
		if (stack.empty()) break;
		x = stack.pop();
	}
}