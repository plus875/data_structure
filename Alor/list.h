
class List {
  private:
    int _capacity;
    int _size;

    int* _elements;
  public:
    // int x;
    // int y;
    // int z;
	  List();

	void checkCapacity();

	int size();

    void add(int element);

    void removeAt(int index);

    void replace(int index, int element);

    int get(int index);

    void print();
};
