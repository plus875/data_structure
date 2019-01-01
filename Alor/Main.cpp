#include "list.h"
#include <iostream>

using namespace std;

void main() {
	List l;

	l.add(1);
	l.add(2);
	l.add(3);
	l.print();

	l.removeAt(1);
	l.print();

	cout << l.size();
}