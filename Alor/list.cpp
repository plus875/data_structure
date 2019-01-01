#include "list.h"
#include <iostream>

using namespace std;

List::List() {
	_capacity = 2;
	_size = 0;
	this->_elements = new int[_capacity];
}

void List::checkCapacity(){
  if (_size >= _capacity) {
    _capacity *= 2;
    int* temp = new int[_capacity];
    for (int i = 0; i < _size; i++) {
      temp[i] = _elements[i];
    }
    this->_elements = temp;
  }
}

int List::size(){
  return _size;
}


void List::add(int element)
{
  checkCapacity();
  this->_elements[_size++] = element;
}

void List::removeAt(int index){
  if(index < 0 || index > _size - 1) return;

  for (int i = index; i < _size - 1; i++) {
    _elements[i] = _elements[i + 1];
  }
  _size--;
}

void List::replace(int index, int element){
  if(index < 0 || index > _size - 1) return;

  _elements[index] = element;
}

int List::get(int index){
  if(index < 0 || index > _size - 1) return -1;

  return _elements[index];
}

void List::print(){
  cout << "-----------begin-------\n";
  for (int i = 0; i < _size; i++) {
    cout << ("index:%d, element:%d\n", i, " ", _elements[i]);
  }
  cout << ("\n-----------end-------\n");
}
