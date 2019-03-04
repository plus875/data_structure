#include "iostream"
#include "stdio.h"

class LinkNode
{
public:
	LinkNode(int v) { value = v; }

	LinkNode* next;
	int value;
};

LinkNode* reverse(LinkNode* head) {
	LinkNode* pre = NULL;
	LinkNode* next = NULL;

	while (head != NULL)
	{
		next = head->next;
		head->next = pre;
		pre = head;
		head = next;
	}
	return pre;
}

int main_00() {
	LinkNode* node1 = new LinkNode(1);
	LinkNode* node2 = new LinkNode(8);
	LinkNode* node3 = new LinkNode(10);
	LinkNode* node4 = new LinkNode(11);
	LinkNode* node5= new LinkNode(15);
	LinkNode* node6 = new LinkNode(20);
	node1->next = node2;
	node2->next = node3;
	node3->next = node4;
	node4->next = node5;
	node5->next = node6;

	LinkNode* head = new LinkNode(-1);;
	head->next = node1;
	head = reverse(head);

	while (head->next != NULL)
	{
		printf("%d, ", head->value);
		head = head->next;
	}

	system("pause");

	return 0;
}

LinkNode* reverseLink(LinkNode* head)
{
	LinkNode* pre = NULL;
	LinkNode* next = NULL;

	while (head != NULL)
	{
		next = head->next;
		head->next = pre;
		pre = head;
		head = next;
	}
	return pre;
}

LinkNode* new_head = NULL;

LinkNode* reverseByRecursion(LinkNode* head) {
	if (head->next == NULL)
	{
		new_head = head;
		return head;
	}

	LinkNode* new_tail = reverseByRecursion(head->next);
	new_tail->next = head;
	head->next = NULL;
	return head;
}

LinkNode* recur(LinkNode* head) {

	if (head->next == NULL) {
		new_head = head;
		return head;
	}

	LinkNode* tail = recur(head->next);
	tail->next = head;
	head->next = NULL;
	return head;
}

int main() {
	LinkNode* node1 = new LinkNode(1);
	LinkNode* node2 = new LinkNode(5);
	LinkNode* node3 = new LinkNode(10);
	LinkNode* node4 = new LinkNode(15);
	LinkNode* node5 = new LinkNode(20);
	node1->next = node2;
	node2->next = node3;
	node3->next = node4;
	node4->next = node5;

	LinkNode* head = node1;

	//head = reverseLink(head);

	//while (head != NULL)
	//{
	//	printf("%d, ", head->value);
	//	head = head->next;
	//}


	//reverseByRecursion(head);
	recur(head);
	while (new_head != NULL)
	{
		printf("%d, ", new_head->value);
		new_head = new_head->next;
	}

	system("pause");

	return 0;
}
