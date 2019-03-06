using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeCard
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] a = new int[] {13, 14, 15, 16, 17, 18, 21 };
            JudgeFourAndTwo(a);
            Console.ReadKey();
        }


        static void JudgeFourAndTwo(int[] cards)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                int count = 1;
                int[] newCards = new int[4];
                newCards[0] = cards[i];

                int[] two = new int[] {-1, -1};
                for (int j = i + 1; j < cards.Length; j++)
                {
                    if (IsSameCard(cards[i], cards[j]))
                    {
                        newCards[count] = cards[j];
                        count++;
                    }
                    else if(two[two.Length - 1] == -1)
                    {
                        for (int k = 0; k < two.Length; k++)
                        {
                            if (k == 0 && two[k] == -1)
                            {
                                two[k] = cards[j];
                                break;
                            }
                            else if(k > 0 && !IsSameCard(two[k - 1], cards[j]))
                            {
                                two[k] = cards[j];
                                break;
                            }
                        }
                    }

                    if (count >= 4 && two[two.Length - 1] != -1)
                    {
                        for (int k = 0; k < newCards.Length; k++)
                        {
                            Console.Write( newCards[k] + " ");
                        }
                        for (int k = 0; k < two.Length; k++)
                        {
                            Console.Write(two[k] + " ");
                        }
                        break;
                    }
                }    
            }
        }

        static bool IsSameCard(int cur, int compare)
        {
            return cur / 4 == compare / 4 || (cur / 4 + 1 == compare / 4 && compare % 4 == 0);
        }
    }
}
