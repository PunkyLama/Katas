using System.Collections.Generic;
using System.Text;

Random r = new Random();
Dictionary<int, string[]> hashGroups = new Dictionary<int, string[]>();

while(true) 
{
    byte[] tmpSource;
    var s = RandomString();
    tmpSource = ASCIIEncoding.ASCII.GetBytes(s);
    var h = tmpSource.GetHashCode();
    if (!hashGroups.ContainsKey(h))
    {
        hashGroups[h] = new string[3] { s, "", "" };
    }
    else
    {
        if (hashGroups[h][1] == "" && s != hashGroups[h][0])
        {
            hashGroups[h][1] = s;
        }
        else if (hashGroups[h][2] == "" && s != hashGroups[h][1] && s != hashGroups[h][0])
        {
            hashGroups[h][2] = s;
            Console.WriteLine("{0}, {1} and {2} have the same hash code {3} in {4} loops",
                hashGroups[h][0],
                hashGroups[h][1],
                hashGroups[h][2],
                h,
                hashGroups.Count);
            break;
        }
    }
    if (hashGroups.Count % 1000 == 0)
    {
        Console.WriteLine(hashGroups.Count);
    }
}
string RandomString()
{
    StringBuilder sb = new StringBuilder();
    for (int i = 0; i < 6; i++)
    {
        sb.Append((char)r.Next((int)'a', ((int)'z') + 1));
    }
    return sb.ToString();
}