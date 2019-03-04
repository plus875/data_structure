#include <iostream>
using namespace std;

#pragma once
#define BinNodePosi(T) BinNode<T>* //�ڵ�λ��
#define stature(p) ((p) ? p->height : -1) //�ڵ�߶�
typedef enum {RB_RED, RB_BLACK} RBColor;

template <typename T> struct BinNode
{
	T data;
	BinNodePosi(T) parent; 
	BinNodePosi(T) lChild;
	BinNodePosi(T) rChild;

	int height;
	int npl;
	RBColor color;

	BinNode() : parent(NULL), lChild(NULL), rChild(NULL), height(0), npl(1), color(RB_RED) {}
	BinNode(T e, BinNodePosi(T) p = NULL, BinNodePosi(T) lc = NULL, BinNodePosi(T) rc = NULL,
		int h = 0, int l = 1, RBColor c = RB_RED)
		:data(e), parent(p), lChild(lc), rChild(rc), height(h), npl(l), color(c) {}

	int size();
	BinNodePosi(T) insertAsLC(T const&);
	BinNodePosi(T) insertAsRC(T const&);

	BinNodePosi(T) succ(); //ȡ��ǰ�ڵ��ֱ�Ӻ��
	template <typename VST> void travLevel(VST&);//�����ִα���
	template <typename VST> void travPre(VST&);//�����������
	template <typename VST> void travIn(VST&);//�����������
	template <typename VST> void travPost(VST&);//�����������
	 
	bool operator<(BinNode const& bn) {return data < bn.data; }
	bool operator==(BinNode const& bn) {return data == bn.data; }



	/******************************************************************************************
	 * BinNode״̬�����ʵ��ж�
	 ******************************************************************************************/
#define IsRoot(x) (x.parent == NULL)
#define IsLChild(x) (IsRoot(x) && (x == x.parent->lChild))
#define IsRChild(x) (IsRoot(x) && (x == x.parent->rChild))
#define HasParent(x) (!IsRoot(x))
#define HasLChild(x) (x->lChild != NULL)
#define HasRChild(x) (x->rChild != NULL)

#define HasChild(x) (HasLChild(x) || HasRChild(x))
#define HasBothChild(x) (HasLChild(x) && HasRChild(x))
#define IsLeaf(x) (!HasChild(x))

#define sibling(p) ( \
	IsLChild(*(p)) ? (p)->parent->rChild : (p)->parent->lChild)//�ֵ�

#define uncle(x) ( \
   IsLChild(*((x)->parent)) ? (x)->parent->parent->rChild : (x)->parent->parent->lChild) //����

#define FromParentTo(x) ( \
   IsRoot(x) ? _root : ( IsLChild(x) ? (x).parent->lChild : (x).parent->rChild)) //���Ը��׵�ָ��
};
